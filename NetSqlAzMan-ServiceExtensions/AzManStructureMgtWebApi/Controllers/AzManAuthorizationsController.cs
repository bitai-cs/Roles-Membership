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
	public class AzManAuthorizationsController :BaseApiController
	{
		#region Constructor
		public AzManAuthorizationsController() : base() {
		}
		#endregion

		#region Private methods
		internal NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization getSBOFromAuthorization(NetSqlAzMan.Interfaces.IAzManAuthorization authorization, bool loadAttributes) {
			var _authorizations = new NetSqlAzMan.Interfaces.IAzManAuthorization[] { authorization };

			return getSBOFromListOfAuthorizations(_authorizations, authorization.Item, loadAttributes).First();
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization> getSBOFromListOfAuthorizations(IEnumerable<NetSqlAzMan.Interfaces.IAzManAuthorization> authorizations, NetSqlAzMan.Interfaces.IAzManItem parentItem, bool loadAttributes) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>();

			var _sboParentItem = new AzManItemsController().getSBOFromItem(parentItem, false, false, false, false);

			foreach (var _authorization in authorizations) {
				var _sboAuthorization = new NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization {
					/*Attributes = Se carga opcionalmente*/
					AuthorizationId = _authorization.AuthorizationId,
					AuthorizationType = (NetSqlAzMan.ServiceBusinessObjects.AuthorizationType)_authorization.AuthorizationType,
					Item = _sboParentItem,
					DomainProfile = _authorization.DomainProfile,
					Owner = new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_authorization.Owner.StringValue),
					OwnerSidWhereDefined = (NetSqlAzMan.ServiceBusinessObjects.WhereDefined)_authorization.OwnerSidWhereDefined,
					SID = new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_authorization.SID.StringValue, (_authorization.SidWhereDefined == NetSqlAzMan.Interfaces.WhereDefined.Database)),
					SidWhereDefined = (NetSqlAzMan.ServiceBusinessObjects.WhereDefined)_authorization.SidWhereDefined,
					ValidFrom = _authorization.ValidFrom,
					ValidTo = _authorization.ValidTo,
					samAccountName = _authorization.samAccountName,
					cn = _authorization.cn,
					displayName = _authorization.displayName,
					objectSidString = _authorization.objectSidString,
					distinguishedName = _authorization.distinguishedName,
					objectClass = _authorization.objectClass
				};

				if (loadAttributes) {
					var _sboAttributes = new List<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationAttribute>();
					foreach (var _attributesKvp in _authorization.GetAttributes()) {
						_sboAttributes.Add(new NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationAttribute() {
							AttributeId = _attributesKvp.AttributeId,
							Key = _attributesKvp.Key,
							Value = _attributesKvp.Value,
							Owner = _sboAuthorization.Clone()
						});
					}

					_sboAuthorization.Attributes = _sboAttributes;
				}

				_listSBO.Add(_sboAuthorization);
			}

			return _listSBO;
		}
		#endregion

		/// <summary>
		/// Get All Authorizations from an Item
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="item"></param>
		/// <param name="loadAttributes"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>))]
		public async Task<HttpResponseMessage> Get(string store, string application, string item, bool loadAttributes) {
			HttpResponseMessage _respMsg;
			_respMsg = await Task.Run(() => {
				var _parentItem = _storage.Stores[store].Applications[application].Items[item];
				var _auths = _parentItem.GetAuthorizations();

				var _sbos = this.getSBOFromListOfAuthorizations(_auths, _parentItem, loadAttributes);

				var _return = this.Request.CreateResponse<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>>(HttpStatusCode.OK, _sbos);
				_return.Headers.Add(Global.RESPONSE_HEADER_RecordCount, _sbos.Count().ToString());

				return _return;
			});

			return _respMsg;
		}

		/// <summary>
		/// For an Item, get authorizations by AuthorizationType
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="item"></param>
		/// <param name="authorizationType"></param>
		/// <param name="loadAttributes"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>))]
		public async Task<HttpResponseMessage> Get(string store, string application, string item, NetSqlAzMan.ServiceBusinessObjects.AuthorizationType authorizationType, bool loadAttributes) {
			HttpResponseMessage _respMsg;
			_respMsg = await Task.Run(() => {
				var _parentItem = _storage.Stores[store].Applications[application].Items[item];
				var _auths = _parentItem.GetAuthorizations((NetSqlAzMan.Interfaces.AuthorizationType)authorizationType);

				var _sbos = this.getSBOFromListOfAuthorizations(_auths, _parentItem, loadAttributes);

				var _return = this.Request.CreateResponse<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>>(HttpStatusCode.OK, _sbos);
				_return.Headers.Add(Global.RESPONSE_HEADER_RecordCount, _sbos.Count().ToString());

				return _return;
			});

			return _respMsg;
		}

		/// <summary>
		/// Get by Id of Authorization
		/// </summary>
		/// <param name="id"></param>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="item"></param>
		/// <param name="loadAttributes"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization))]
		public async Task<HttpResponseMessage> Get(int id, string store, string application, string item, bool loadAttributes) {
			HttpResponseMessage _respMsg;
			var _result = await Task.Run(() => {
				var _parentItem = _storage.Stores[store].Applications[application].Items[item];
				var _auth = _parentItem.Authorizations.Where(f => f.AuthorizationId.Equals(id)).FirstOrDefault();

				if (_auth == null)
					return null;

				var _sbo = this.getSBOFromAuthorization(_auth, loadAttributes);

				return _sbo;
			});

			if (_result != null) {
				_respMsg = this.Request.CreateResponse<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>(HttpStatusCode.OK, _result);
			}
			else {
				_respMsg = GetResponseMessageForNotFoundId(id.ToString());
			}

			return _respMsg;
		}

		/// <summary>
		/// Get Member or Owner Info
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="item"></param>
		/// <param name="authorizationId"></param>
		/// <param name="getMemberInfo"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo))]
		public async Task<IHttpActionResult> Get(string store, string application, string item, int authorizationId, bool getMemberInfo) {
			string _display;
			NetSqlAzMan.Interfaces.MemberType _type;

			var _return = await Task.Run(() => {
				var _authorization = _storage.GetStore(store).GetApplication(application).GetItem(item).GetAuthorization(authorizationId);

				if (getMemberInfo) {
					_type = _authorization.GetMemberInfo(out _display);
				}
				else
					_type = _authorization.GetOwnerInfo(out _display);

				return new NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo() {
					DisplayName = _display,
					MemberType = (NetSqlAzMan.ServiceBusinessObjects.MemberType)_type
				};
			});

			return Ok(_return);
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization bso) {
			HttpResponseMessage _respMsg;
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = bso.AuthorizationId });

			/*Calling to NetSqlAzMan BL*/
			var _created = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _authorization = _storage.Stores[bso.Item.Application.Store.Name].Applications[bso.Item.Application.Name].Items[bso.Item.Name].CreateAuthorizationCustom(new NetSqlAzMan.SqlAzManSID(bso.Owner.StringValue, bso.OwnerSidWhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database), (NetSqlAzMan.Interfaces.WhereDefined)bso.OwnerSidWhereDefined, new NetSqlAzMan.SqlAzManSID(bso.SID.StringValue, bso.SidWhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database), (NetSqlAzMan.Interfaces.WhereDefined)bso.SidWhereDefined, (NetSqlAzMan.Interfaces.AuthorizationType)bso.AuthorizationType, bso.ValidFrom, bso.ValidTo, bso.DomainProfile, bso.samAccountName, bso.cn, bso.displayName, bso.objectSidString, bso.distinguishedName, bso.objectClass);

					_storage.CommitTransaction();

					return _authorization;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _sbo = await Task.Run(() => this.getSBOFromAuthorization(_created, false));

			_respMsg = Request.CreateResponse(HttpStatusCode.Created, _sbo);
			_respMsg.Headers.Location = _locationUri;
			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization))]
		public async Task<HttpResponseMessage> Put(int id, NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization bso) {
			HttpResponseMessage _respMsg;
			if (!id.Equals(bso.AuthorizationId))
				return this.GetResponseMessageForMismatchingIds();

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _authorization = await Task.Run(() => _storage.Stores[bso.Item.Application.Store.Name].Applications[bso.Item.Application.Name].Items[bso.Item.Name].GetAuthorization(bso.AuthorizationId));

			if (_authorization == null)
				return this.GetResponseMessageForNotFoundId(id.ToString());

			/*Calling to NetSqlAzMan BL*/
			var _updated = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_authorization.Update(new NetSqlAzMan.SqlAzManSID(bso.Owner.StringValue, bso.OwnerSidWhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database), new NetSqlAzMan.SqlAzManSID(bso.SID.StringValue, bso.SidWhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database), (NetSqlAzMan.Interfaces.WhereDefined)bso.SidWhereDefined, (NetSqlAzMan.Interfaces.AuthorizationType)bso.AuthorizationType, bso.ValidFrom, bso.ValidTo);

					_storage.CommitTransaction();

					return _authorization;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _updatedBso = await Task.Run(() => this.getSBOFromAuthorization(_updated, false));

			_respMsg = Request.CreateResponse(HttpStatusCode.OK, _updatedBso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó la Authorization: \"{0}\\{1}\\{2}\\{3}\"", bso.GetStoreName(), bso.GetApplicationName(), bso.GetItemName(), bso.AuthorizationId));
			return _respMsg;
		}

		/// <summary>
		/// Delete Item Authorization by Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization))]
		public async Task<HttpResponseMessage> Delete(int id, string store, string application, string item) {
			NetSqlAzMan.Interfaces.IAzManAuthorization _authorization =
			await Task.Run(() => {
				return _storage.GetStore(store).GetApplication(application).GetItem(item).GetAuthorization(id);
			});

			if (_authorization == null)
				return GetResponseMessageForNotFoundId(id.ToString());

			/*Calling to NetSqlAzMan BL*/
			var _deletedBso = await Task.Run(() => {
				try {
					var _toDeleteBso = this.getSBOFromAuthorization(_authorization, false);

					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_authorization.Delete();

					_storage.CommitTransaction();

					return _toDeleteBso;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _deletedBso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó la Authorization: \"{0}\\{1}\\{2}\\{3}\"", store, application, item, id));

			return _respMsg;
		}
	}
}