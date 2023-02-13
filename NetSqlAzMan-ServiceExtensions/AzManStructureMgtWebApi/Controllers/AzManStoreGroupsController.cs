using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManStoreGroupsController :BaseApiController
	{
		internal AzManStoreGroupsController() : base() {
		}

		#region Private members
		internal NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup getSBOFromStoreGroup(NetSqlAzMan.Interfaces.IAzManStoreGroup storeGroup, bool loadStoreGroupMembers) {
			var _dict = new List<NetSqlAzMan.Interfaces.IAzManStoreGroup>() { storeGroup };

			return getSBOFromListOfStoreGroups(_dict, storeGroup.Store, loadStoreGroupMembers).First();
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup> getSBOFromListOfStoreGroups(IEnumerable<NetSqlAzMan.Interfaces.IAzManStoreGroup> storeGroups, NetSqlAzMan.Interfaces.IAzManStore parentStore, bool loadStoreGroupMembers) {
			List<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup> _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>();

			var _sboParentStore = new AzManStoresController().getSBOFromStore(parentStore, false, false, false);

			foreach (var _storeGroup in storeGroups) {
				var _sboStoreGroup = new NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup {
					Store = _sboParentStore,
					StoreGroupId = _storeGroup.StoreGroupId,
					SID = new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_storeGroup.SID.BinaryValue),
					Name = _storeGroup.Name,
					Description = _storeGroup.Description,
					GroupType = (NetSqlAzMan.ServiceBusinessObjects.GroupType)_storeGroup.GroupType,
					LDAPQuery = _storeGroup.LDAPQuery
				};

				if (loadStoreGroupMembers) {
					if (_storeGroup.GroupType == NetSqlAzMan.Interfaces.GroupType.Basic)
						_sboStoreGroup.Members = new AzManStoreGroupMembersController().getSBOFromListOfStoreGroupMembers(_storeGroup.GetStoreGroupAllMembers(), _storeGroup);
				}

				_listSBO.Add(_sboStoreGroup);
			}

			return _listSBO;
		}
		#endregion

		[HttpGet]
		[ActionName("GetIsStoreGroupMember")]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.StructContent<bool>>))]
		public async Task<IHttpActionResult> GetIsStoreGroupMemberAsync(string store, string storeGroup, string userNameToCheck, string domainProfile = null) {
			if (string.IsNullOrEmpty(userNameToCheck))
				throw new ArgumentNullException("userNameToCheck");

			var _storeGroup = await Task.Run(() => _storage.Stores[store].StoreGroups[storeGroup]);

			bool _check;
			Exception _exce = null;
			NetSqlAzMan.Interfaces.IAzManDBUser _azUser = null;
			if (string.IsNullOrEmpty(domainProfile)) {
				_azUser = await Task.Run(() => _storage.GetDBUser(userNameToCheck));

				_check = await Task.Run(() => _storeGroup.IsInGroup(_azUser));
			}
			else {
				if (!await Task.Run(() => _storage.GetLDAPUser(domainProfile, userNameToCheck, out _azUser, out _exce)))
					throw _exce;

				_check = await Task.Run(() => _storeGroup.IsInGroup(_azUser));
			}

			return Ok(new NetSqlAzMan.ServiceBusinessObjects.StructContent<bool>() { Value = _check });
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup))]
		public async Task<IHttpActionResult> GetByIdAsync(string id, string store, bool loadStoreGroupMembers = false) {
			var _storeGroup = await Task.Run(() => _storage.Stores[store].GetStoreGroup(id));
			if (_storeGroup == null)
				return NotFound();

			var _sbo = getSBOFromStoreGroup(_storeGroup, loadStoreGroupMembers);

			return Ok(_sbo);
		}

		[HttpGet]
		//[ActionName("GetAllByStore")]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>))]
		public async Task<IHttpActionResult> GetAllByStoreAsync(string store, bool loadStoreGroupMembers = false) {
			var _parentStore = await Task.Run(() => _storage.Stores[store]);

			var _storeGroups = await Task.Run(() => _storage.Stores[store].GetStoreGroups());

			var _sbos = getSBOFromListOfStoreGroups(_storeGroups, _parentStore, loadStoreGroupMembers);

			return Ok(_sbos);
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup))]
		public async Task<IHttpActionResult> GetByStoreGroupIdAsync(string store, string storeGroupBinaryId, string storeGroupStringId, bool loadStoreGroupMembers = false) {
			var _storeGroup = await Task.Run(() => {
				NetSqlAzMan.SqlAzManSID _sid = new NetSqlAzMan.SqlAzManSID(Convert.FromBase64String(storeGroupBinaryId));
				NetSqlAzMan.SqlAzManSID _sidb = new NetSqlAzMan.SqlAzManSID(storeGroupStringId);

				if (!_sid.Equals(_sidb))
					throw new InvalidOperationException("los identificadores del Store Group no corresponden.");

				return _storage.Stores[store].GetStoreGroup(_sid);
			});

			if (_storeGroup != null) {
				var _return = getSBOFromStoreGroup(_storeGroup, loadStoreGroupMembers);

				return Ok(_return);
			}
			else {
				return NotFound();
			}
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>))]
		public async Task<IHttpActionResult> GetLdapQueryResultAsync(string store, string storeGroupName, string ldapQuery) {
			if (string.IsNullOrEmpty(ldapQuery))
				return BadRequest("Debe de proporcionar la consulta LDAP.");

			var _storeGroup = await Task.Run(() => _storage.Stores[store].GetStoreGroup(storeGroupName));

			var _return = new List<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>();
			if (_storeGroup != null) {
				var _resultColl = _storeGroup.ExecuteLDAPQuery(ldapQuery);
				foreach (System.DirectoryServices.SearchResult _rs in _resultColl) {
					_return.Add(Helpers.LDAPHelper.mapToObject(_rs.Path, null, null, _rs));
				}

				return Ok(_return);
			}
			else {
				return NotFound();
			}
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup))]
		public async Task<HttpResponseMessage> RegisterAsync(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup bso) {
			HttpResponseMessage _respMsg;
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = bso.Name });

			/*Calling to NetSqlAzMan BL*/
			var _newStoreGroup = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _created = _storage.GetStore(bso.Store.Name).CreateStoreGroup(NetSqlAzMan.SqlAzManSID.NewSqlAzManSid(), bso.Name, bso.Description, bso.LDAPQuery, (NetSqlAzMan.Interfaces.GroupType)bso.GroupType);

					_storage.CommitTransaction();

					return _created;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _sbo = getSBOFromStoreGroup(_newStoreGroup, false);

			_respMsg = Request.CreateResponse(HttpStatusCode.Created, _sbo);
			_respMsg.Headers.Location = _locationUri;
			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup))]
		public async Task<HttpResponseMessage> UpdateAsync(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup bso) {
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Esta validación no va, el id puede ser distinto al nuevo nombre del grupo
			//if (!id.Equals(bso.Name))
			//	return GetResponseMessageForMismatchingIds();

			/*Calling to NetSqlAzMan BL*/
			var _updated = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _modified = _storage.GetStore(bso.Store.Name).GetStoreGroup(id);
					_modified.Rename(bso.Name);
					_modified.Update(bso.Description, (NetSqlAzMan.Interfaces.GroupType)bso.GroupType);

					if (_modified.GroupType == NetSqlAzMan.Interfaces.GroupType.Basic) {
						//Members To Add
						foreach (var member in bso.MembersToAdd) {
							_modified.CreateStoreGroupMemberCustom(new NetSqlAzMan.SqlAzManSID(member.sid.BinaryValue, (member.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database)), (NetSqlAzMan.Interfaces.WhereDefined)member.WhereDefined, true, member.DomainProfile, member.samAccountName, member.cn, member.displayName, member.objectSidString, member.distinguishedName, member.objectClass);
						}
						//Members To Remove
						foreach (var member in bso.MembersToRemove) {
							_modified.GetStoreGroupMember(new NetSqlAzMan.SqlAzManSID(member.sid.BinaryValue, (member.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database))).Delete();
						}
						//Non Members
						//Non Members To Add
						foreach (var nonMember in bso.NonMembersToAdd) {
							_modified.CreateStoreGroupMemberCustom(new NetSqlAzMan.SqlAzManSID(nonMember.sid.BinaryValue, (nonMember.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database)), (NetSqlAzMan.Interfaces.WhereDefined)nonMember.WhereDefined, false, nonMember.DomainProfile, nonMember.samAccountName, nonMember.cn, nonMember.displayName, nonMember.objectSidString, nonMember.distinguishedName, nonMember.objectClass);
						}
						//Non Members To Remove
						foreach (var nonMember in bso.NonMembersToRemove) {
							_modified.GetStoreGroupMember(new NetSqlAzMan.SqlAzManSID(nonMember.sid.BinaryValue, (nonMember.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database))).Delete();
						}
					}
					else {
						_modified.UpdateLDapQuery(bso.LDAPQuery);
					}

					_storage.CommitTransaction();

					return _modified;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _updatedBso = getSBOFromStoreGroup(_updated, true);

			var _respMsg = Request.CreateResponse(HttpStatusCode.OK, _updatedBso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del StoreGroup {0}: {1}", _updatedBso.SID.StringValue, _updatedBso.Name));

			return _respMsg;
		}

		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup))]
		public async Task<HttpResponseMessage> DeleteAsync(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup bso) {
			if (bso == null)
				return GetResponseMessageForInvalidModel(null);

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			if (!id.Equals(bso.Name))
				return this.GetResponseMessageForMismatchingIds();

			/*Calling to NetSqlAzMan BL*/
			await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					_storage.GetStore(bso.Store.Name).GetStoreGroup(bso.Name).Delete();

					_storage.CommitTransaction();
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, bso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó el StoreGroup: \"{0}\"", id));

			return _respMsg;
		}
	}
}