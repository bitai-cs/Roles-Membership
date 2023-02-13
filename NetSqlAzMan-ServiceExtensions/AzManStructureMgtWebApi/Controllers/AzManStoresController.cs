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
	public class AzManStoresController :BaseApiController
	{
		internal AzManStoresController() : base() {
		}

		#region Internal methods
		internal NetSqlAzMan.ServiceBusinessObjects.AzManStore getSBOFromStore(NetSqlAzMan.Interfaces.IAzManStore store, bool loadAttributes, bool loadStoreGroups, bool loadStoreApplications) {
			var _stores = new List<NetSqlAzMan.Interfaces.IAzManStore>() { store };

			return getSBOFromListOfStores(_stores, store.Storage, loadAttributes, loadStoreGroups, loadStoreApplications).First();
		}

		//internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStore> getSBOFromListOfStores(IDictionary<string, NetSqlAzMan.Interfaces.IAzManStore> stores, NetSqlAzMan.Interfaces.IAzManStorage parentStorage, bool loadAttributes, bool loadStoreGroups, bool loadStoreApplications) {
		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStore> getSBOFromListOfStores(IEnumerable<NetSqlAzMan.Interfaces.IAzManStore> stores, NetSqlAzMan.Interfaces.IAzManStorage parentStorage, bool loadAttributes, bool loadStoreGroups, bool loadStoreApplications) {
			List<NetSqlAzMan.ServiceBusinessObjects.AzManStore> _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManStore>();

			var _sboParentStorage = new AzManStorageController().getSBOFromStorage(parentStorage, false);

			foreach (var _store in stores) {
				var _sboStore = new NetSqlAzMan.ServiceBusinessObjects.AzManStore {
					Storage = _sboParentStorage,
					StoreId = _store.StoreId,
					Name = _store.Name,
					Description = _store.Description,
					IAmAdmin = _store.IAmAdmin,
					IAmManager = _store.IAmManager,
					IAmUser = _store.IAmUser,
					IAmReader = _store.IAmReader
				};

				if (loadAttributes) {
					var _sboAttributes = new List<NetSqlAzMan.ServiceBusinessObjects.AzManStoreAttribute>();
					foreach (var _attributeKvp in _store.GetAttributes()) {
						_sboAttributes.Add(new NetSqlAzMan.ServiceBusinessObjects.AzManStoreAttribute() {
							AttributeId = _attributeKvp.AttributeId,
							Key = _attributeKvp.Key,
							Value = _attributeKvp.Value,
							Owner = _sboStore.Clone()
						});
					}

					_sboStore.Attributes = _sboAttributes;
				}

				if (loadStoreGroups) {
					_sboStore.StoreGroups = new AzManStoreGroupsController().getSBOFromListOfStoreGroups(_store.GetStoreGroups(), _store, false);
				}

				if (loadStoreApplications) {
					_sboStore.Applications = new AzManApplicationsController().getSBOFromListOfApplications(_store.GetApplications(), _store, false, false, false);
				}

				_listSBO.Add(_sboStore);
			}

			return _listSBO;
		}
		#endregion

		/// <summary>
		/// Get a AzManStore by his name
		/// </summary>
		/// <param name="id">Store name</param>
		/// <param name="loadAttributes">True or False</param>
		/// <param name="loadStoreGroups">True or False</param>
		/// <param name="loadStoreApplications">True or False</param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStore))]
		public async Task<HttpResponseMessage> Get(string id, bool loadAttributes = true, bool loadStoreGroups = false, bool loadStoreApplications = false) {
			HttpResponseMessage _respMsg;
			var _store = await Task.Run(() => _storage.GetStores().Where(f => f.Name.Equals(id)).FirstOrDefault());

			if (_store == null)
				return GetResponseMessageForNotFoundId(id);

			var _sbo = getSBOFromStore(_store, loadAttributes, loadStoreGroups, loadStoreApplications);

			_respMsg = Request.CreateResponse(HttpStatusCode.OK, _sbo);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se encontró el Store  {0}", id));
			return _respMsg;
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStore>))]
		public async Task<HttpResponseMessage> Get(bool loadAttributes = true, bool loadStoreGroups = false, bool loadStoreApplications = false) {
			HttpResponseMessage _respMsg;
			var _stores = await Task.Run(() => _storage.GetStores());
			var _return = getSBOFromListOfStores(_stores, _storage, loadAttributes, loadStoreGroups, loadStoreApplications);

			_respMsg = Request.CreateResponse(HttpStatusCode.OK, _return);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_RecordCount, _return.Count().ToString());
			return _respMsg;
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStore))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.AzManStore data) {
			HttpResponseMessage _respMsg;
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = data.Name });

			/*Calling to NetSqlAzMan BL*/
			var _newStore = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _created = _storage.CreateStore(data.Name, data.Description);

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

			var _sbo = getSBOFromStore(_newStore, false, false, false);

			_respMsg = Request.CreateResponse(HttpStatusCode.Created, _sbo);
			_respMsg.Headers.Location = _locationUri;
			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStore))]
		public async Task<HttpResponseMessage> Put(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManStore bso) {
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _store = _storage.GetStore(id);
			if (_store == null)
				return GetResponseMessageForNotFoundId(id);

			await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_store.Rename(bso.Name);
					_store.Update(bso.Description);

					//Create or update Attributes
					foreach (var _sboAttr in bso.Attributes) {
						if (_store.Attributes.Keys.Contains(_sboAttr.Key)) {
							_store.GetAttribute(_sboAttr.Key).Update(_sboAttr.Key, _sboAttr.Value);
						}
						else {
							_store.CreateAttribute(_sboAttr.Key, _sboAttr.Value);
						}
					}

					//Delete Attributes
					var _q = from a in _store.GetAttributes()
								where !(from s in bso.Attributes
										  select s.Key).Contains(a.Key)
								select a;
					foreach (var _i in _q) {
						_i.Delete();
					}

					_storage.CommitTransaction();
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _updated = getSBOFromStore(_store, true, false, false);

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _updated);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del Store \"{0}\"", bso.Name));

			return _respMsg;
		}

		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStore))]
		public async Task<HttpResponseMessage> Delete(string id) {
			var _store = _storage.GetStore(id);
			if (_store == null)
				return GetResponseMessageForNotFoundId(id);

			var _sbo = getSBOFromStore(_store, true, false, false);

			try {
				await Task.Run(() => {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_store.Delete();

					_storage.CommitTransaction();
				}); ;
			}
			catch {
				_storage.RollBackTransaction();
				throw;
			}

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _sbo);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó el Store: \"{0}\"", id));

			return _respMsg;
		}
	}
}
