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
	public class AzManApplicationGroupsController :BaseApiController
	{
		internal AzManApplicationGroupsController() : base() {
		}

		#region Private members
		internal NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup getSBOFromApplicationGroup(NetSqlAzMan.Interfaces.IAzManApplicationGroup applicationGroup, bool loadApplicationGroupMembers) {
			var _applicationGroup = new List<NetSqlAzMan.Interfaces.IAzManApplicationGroup>() { applicationGroup };

			return getSBOFromListOfApplicationGroups(_applicationGroup, applicationGroup.Application, loadApplicationGroupMembers).First();
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup> getSBOFromListOfApplicationGroups(IEnumerable<NetSqlAzMan.Interfaces.IAzManApplicationGroup> applicationGroups, NetSqlAzMan.Interfaces.IAzManApplication parentApplication, bool loadApplicationGroupMembers) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>();

			var _sboParentApplication = new AzManApplicationsController().getSBOFromApplication(parentApplication, false, false, false);

			foreach (var _applicationGroup in applicationGroups) {
				var _sboApplicationGroup = new NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup {
					Application = _sboParentApplication,
					ApplicationGroupId = _applicationGroup.ApplicationGroupId,
					SID = new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_applicationGroup.SID.BinaryValue),
					Name = _applicationGroup.Name,
					Description = _applicationGroup.Description,
					GroupType = (NetSqlAzMan.ServiceBusinessObjects.GroupType)_applicationGroup.GroupType,
					LDAPQuery = _applicationGroup.LDAPQuery
				};

				if (loadApplicationGroupMembers) {
					if (_applicationGroup.GroupType == NetSqlAzMan.Interfaces.GroupType.Basic)
						_sboApplicationGroup.Members = new AzManApplicationGroupMembersController().getSBOFromListOfApplicationGroupMembers(_applicationGroup.GetApplicationGroupAllMembers(), _applicationGroup);
				}

				_listSBO.Add(_sboApplicationGroup);
			}

			return _listSBO;
		}
		#endregion

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>))]
		public async Task<IHttpActionResult> GetAllByApplicationAsync(string store, string application, bool loadApplicationGroupMembers = true) {
			var _parentApplication = await Task.Run(() => _storage.Stores[store].Applications[application]);

			var _applicationGroups = await Task.Run(() => _storage.Stores[store].Applications[application].GetApplicationGroups());

			var _sbos = getSBOFromListOfApplicationGroups(_applicationGroups, _parentApplication, loadApplicationGroupMembers);

			return Ok(_sbos);
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup))]
		public async Task<IHttpActionResult> GetByNameAsync(string id, string store, string application, bool loadApplicationGroupMembers = false) {
			var _applicationGroup = await Task.Run(() => _storage.Stores[store].Applications[application].GetApplicationGroup(id));
			if (_applicationGroup != null) {
				var _sbo = getSBOFromApplicationGroup(_applicationGroup, loadApplicationGroupMembers);

				return Ok(_sbo);
			}
			else {
				return NotFound();
			}
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup))]
		public async Task<IHttpActionResult> GetByApplicationGroupIdAsync(string store, string application, string applicationGroupBinaryId, string applicationGroupStringId, bool loadApplicationGroupMembers = false) {
			var _applicationGroup = await Task.Run(() => {
				NetSqlAzMan.SqlAzManSID _sid = new NetSqlAzMan.SqlAzManSID(Convert.FromBase64String(applicationGroupBinaryId));
				NetSqlAzMan.SqlAzManSID _sidb = new NetSqlAzMan.SqlAzManSID(applicationGroupStringId);

				if (!_sid.Equals(_sidb))
					throw new InvalidOperationException("los identificadores del Application Group no corresponden.");

				return _storage.Stores[store].Applications[application].GetApplicationGroup(_sid);
			});

			if (_applicationGroup != null) {
				var _return = getSBOFromApplicationGroup(_applicationGroup, loadApplicationGroupMembers);

				return Ok(_return);
			}
			else {
				return NotFound();
			}
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>))]
		public async Task<IHttpActionResult> GetLdapQueryResultAsync(string store, string application, string applicationGroupName, string ldapQuery) {
			if (string.IsNullOrEmpty(ldapQuery))
				return BadRequest("Debe de proporcionar la consulta LDAP.");

			var _applicationGroup = await Task.Run(() => _storage.Stores[store].Applications[application].GetApplicationGroup(applicationGroupName));

			var _return = new List<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>();
			if (_applicationGroup != null) {
				var _resultColl = _applicationGroup.ExecuteLDAPQuery(ldapQuery);
				foreach (System.DirectoryServices.SearchResult _rs in _resultColl) {
					_return.Add(Helpers.LDAPHelper.mapToObject(_rs.Path, null, null, _rs));
				}

				return Ok(_return);
			}
			else {
				return NotFound();
			}
		}

		[HttpGet]
		[ActionName("GetIsApplicationGroupMember")]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.StructContent<bool>>))]
		public async Task<IHttpActionResult> GetIsApplicationGroupMemberAsync(string store, string application, string applicationGroup, string userNameToCheck, string domainProfile = null) {
			if (string.IsNullOrEmpty(userNameToCheck))
				throw new ArgumentNullException("userName");

			var _applicationGroup = await Task.Run(() => _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup]);

			bool _check;
			Exception _exce = null;
			NetSqlAzMan.Interfaces.IAzManDBUser _azUser = null;
			if (string.IsNullOrEmpty(domainProfile)) {
				//if (!await Task.Run(() => _storage.GetAzManDBUser(userNameToCheck, out _azUser, out _exce)))
				//    throw _exce;
				_azUser = await Task.Run(() => _storage.GetDBUser(userNameToCheck));

				_check = await Task.Run(() => _applicationGroup.IsInGroup(_azUser));
			}
			else {
				if (!await Task.Run(() => _storage.GetLDAPUser(domainProfile, userNameToCheck, out _azUser, out _exce)))
					throw _exce;

				_check = await Task.Run(() => _applicationGroup.IsInGroup(_azUser));
			}

			return Ok(new NetSqlAzMan.ServiceBusinessObjects.StructContent<bool>() { Value = _check });
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup bso) {
			HttpResponseMessage _respMsg;
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = bso.Name });

			/*Calling to NetSqlAzMan BL*/
			var _newStoreGroup = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _created = _storage.GetStore(bso.Application.Store.Name).GetApplication(bso.Application.Name).CreateApplicationGroup(NetSqlAzMan.SqlAzManSID.NewSqlAzManSid(), bso.Name, bso.Description, bso.LDAPQuery, (NetSqlAzMan.Interfaces.GroupType)bso.GroupType);

					_storage.CommitTransaction();

					return _created;
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _sbo = getSBOFromApplicationGroup(_newStoreGroup, false);

			_respMsg = Request.CreateResponse(HttpStatusCode.Created, _sbo);
			_respMsg.Headers.Location = _locationUri;
			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup))]
		public async Task<HttpResponseMessage> Put(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup bso) {
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			//Esta validación no va, el id puede ser distinto al nuevo nombre del grupo
			//if (!id.Equals(bso.Name))
			//	return GetResponseMessageForMismatchingIds();

			/*Calling to NetSqlAzMan BL*/
			var _updated = await Task.Run(() => {
				try {
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);

					var _modified = _storage.GetStore(bso.Application.Store.Name).GetApplication(bso.Application.Name).GetApplicationGroup(id);
					_modified.Rename(bso.Name);
					_modified.Update(bso.Description, (NetSqlAzMan.Interfaces.GroupType)bso.GroupType);

					if (_modified.GroupType == NetSqlAzMan.Interfaces.GroupType.Basic) {
						//Members To Add
						foreach (var member in bso.MembersToAdd) {
							_modified.CreateApplicationGroupMemberCustom(new NetSqlAzMan.SqlAzManSID(member.sid.BinaryValue, (member.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database)), (NetSqlAzMan.Interfaces.WhereDefined)member.WhereDefined, true, member.DomainProfile, member.samAccountName, member.cn, member.displayName, member.objectSidString, member.distinguishedName, member.objectClass);
						}
						//Members To Remove
						foreach (var member in bso.MembersToRemove) {
							_modified.GetApplicationGroupMember(new NetSqlAzMan.SqlAzManSID(member.sid.BinaryValue, (member.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database))).Delete();
						}
						//Non Members
						//Non Members To Add
						foreach (var nonMember in bso.NonMembersToAdd) {
							_modified.CreateApplicationGroupMemberCustom(new NetSqlAzMan.SqlAzManSID(nonMember.sid.BinaryValue, (nonMember.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database)), (NetSqlAzMan.Interfaces.WhereDefined)nonMember.WhereDefined, false, nonMember.DomainProfile, nonMember.samAccountName, nonMember.cn, nonMember.displayName, nonMember.objectSidString, nonMember.distinguishedName, nonMember.objectClass);
						}
						//Non Members To Remove
						foreach (var nonMember in bso.NonMembersToRemove) {
							_modified.GetApplicationGroupMember(new NetSqlAzMan.SqlAzManSID(nonMember.sid.BinaryValue, (nonMember.WhereDefined == NetSqlAzMan.ServiceBusinessObjects.WhereDefined.Database))).Delete();
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

			var _updatedBso = getSBOFromApplicationGroup(_updated, true);

			var _respMsg = Request.CreateResponse(HttpStatusCode.OK, _updatedBso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del StoreGroup {0}: {1}", _updatedBso.SID.StringValue, _updatedBso.Name));

			return _respMsg;
		}

		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup))]
		public async Task<HttpResponseMessage> Delete(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup bso) {
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

					_storage.GetStore(bso.Application.Store.Name).GetApplication(bso.Application.Name).GetApplicationGroup(bso.Name).Delete();

					_storage.CommitTransaction();
				}
				catch {
					_storage.RollBackTransaction();
					throw;
				}
			});

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, bso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó el ApplicationGroup: \"{0}\"", id));

			return _respMsg;
		}
	}
}
