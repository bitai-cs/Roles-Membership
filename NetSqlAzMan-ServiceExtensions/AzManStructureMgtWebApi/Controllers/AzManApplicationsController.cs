using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManApplicationsController :BaseApiController
	{
		internal AzManApplicationsController() : base() {
		}

		#region Private methods
		internal NetSqlAzMan.ServiceBusinessObjects.AzManApplication getSBOFromApplication(NetSqlAzMan.Interfaces.IAzManApplication application, bool loadAttributes, bool loadItems, bool loadApplicationGroups) {
			var _applications = new List<NetSqlAzMan.Interfaces.IAzManApplication>() { application };

			return getSBOFromListOfApplications(_applications, application.Store, loadAttributes, loadItems, loadApplicationGroups).First();
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplication> getSBOFromListOfApplications(IEnumerable<NetSqlAzMan.Interfaces.IAzManApplication> applications, NetSqlAzMan.Interfaces.IAzManStore parentStore, bool loadAttributes, bool loadItems, bool loadApplicationGroups) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>();

			var _sboParentStore = new AzManStoresController().getSBOFromStore(parentStore, false, false, false);

			foreach (var _application in applications) {
				var _sboApplication = new NetSqlAzMan.ServiceBusinessObjects.AzManApplication {
					Store = _sboParentStore,
					ApplicationId = _application.ApplicationId,
					Name = _application.Name,
					Description = _application.Description,
					IAmAdmin = _application.IAmAdmin,
					IAmManager = _application.IAmManager,
					IAmReader = _application.IAmReader,
					IAmUser = _application.IAmUser
				};

				if (loadAttributes) {
					var _sboAttributes = new List<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationAttribute>();
					foreach (var _attributesKvp in _application.GetAttributes()) {
						_sboAttributes.Add(new NetSqlAzMan.ServiceBusinessObjects.AzManApplicationAttribute() {
							AttributeId = _attributesKvp.AttributeId,
							Key = _attributesKvp.Key,
							Value = _attributesKvp.Value,
							Owner = _sboApplication.Clone()
						});
					}

					_sboApplication.Attributes = _sboAttributes;
				}

				if (loadItems) {
					var _sboItems = new AzManItemsController().getSBOFromListOfItems(_application.GetItems(), _application, false, false, false, false);
					_sboApplication.Items = _sboItems;
				}

				if (loadApplicationGroups) {
					var _sboApplicationGroups = new AzManApplicationGroupsController().getSBOFromListOfApplicationGroups(_application.GetApplicationGroups(), _application, false);
					_sboApplication.ApplicationGroups = _sboApplicationGroups;
				}

				_listSBO.Add(_sboApplication);
			}

			return _listSBO;
		}
		#endregion

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>))]
		public async Task<HttpResponseMessage> Get(string store, bool loadAttributes = true, bool loadItems = false, bool loadApplicationGroups = false) {
			HttpResponseMessage _respMsg;
			var _parentStore = await Task.Run(() => _storage.Stores[store]);
			var _apps = await Task.Run(() => _storage.Stores[store].GetApplications());

			var _return = getSBOFromListOfApplications(_apps, _parentStore, loadAttributes, loadItems, loadApplicationGroups);

			_respMsg = Request.CreateResponse(HttpStatusCode.OK, _return);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_RecordCount, _return.Count().ToString());
			return _respMsg;
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplication))]
		public async Task<HttpResponseMessage> Post(string store, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManApplication data) {
			HttpResponseMessage _respMsg;
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = data.Name, store });

			/*Calling to NetSqlAzMan BL*/
			var _newApp = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _created = _storage.GetStore(store).CreateApplication(data.Name, data.Description);

					foreach (var _sboAttr in data.Attributes) {
						_created.CreateAttribute(_sboAttr.Key, _sboAttr.Value);
					}

					_storage.CommitTransaction();

					return _created;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _sbo = getSBOFromApplication(_newApp, true, false, false);

			_respMsg = Request.CreateResponse(HttpStatusCode.Created, _sbo);
			_respMsg.Headers.Location = _locationUri;
			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplication))]
		public async Task<HttpResponseMessage> Put(string id, string store, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManApplication data) {
			////VBASTIDAS: Se puede actualizar el Id (Nombre) a otro valor
			//if (!id.Equals(data.Name))
			//	return GetResponseMessageForMismatchingIds();

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _application = _storage.Stores[store].GetApplication(id);
			if (_application == null)
				return GetResponseMessageForNotFoundId(id);

			try {
				/*Calling to NetSqlAzMan BL*/
				await Task.Run(() => {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_application.Rename(data.Name);
					_application.Update(data.Description);

					//Create or update Attributes
					foreach (var _sboAttr in data.Attributes) {
						if (_application.Attributes.Keys.Contains(_sboAttr.Key)) {
							_application.GetAttribute(_sboAttr.Key).Update(_sboAttr.Key, _sboAttr.Value);
						}
						else {
							_application.CreateAttribute(_sboAttr.Key, _sboAttr.Value);
						}
					}

					//Delete Attributes
					var _q = from a in _application.GetAttributes()
								where !(from s in data.Attributes
										  select s.Key).Contains(a.Key)
								select a;
					foreach (var _i in _q) {
						_i.Delete();
					}

					_storage.CommitTransaction();
				});
			}
			catch {
				_storage.RollBackTransaction();
				throw;
			}

			var _sbo = getSBOFromApplication(_application, true, false, false);

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _sbo);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del Application: \"{0}\"/\"{1}\"", store, id));

			return _respMsg;
		}

		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplication))]
		public async Task<HttpResponseMessage> Delete(string id, string store) {
			var _application = _storage.GetStore(store).GetApplication(id);
			if (_application == null)
				return GetResponseMessageForNotFoundId(id);

			var _sbo = getSBOFromApplication(_application, true, false, false);

			try {
				/*Calling to NetSqlAzMan BL*/
				await Task.Run(() => {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_application.Delete();

					_storage.CommitTransaction();
				}); ;
			}
			catch {
				_storage.RollBackTransaction();
				throw;
			}

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _sbo);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó el Application: \"{0}\"/\"{1}\"", store, id));

			return _respMsg;
		}
	}
}