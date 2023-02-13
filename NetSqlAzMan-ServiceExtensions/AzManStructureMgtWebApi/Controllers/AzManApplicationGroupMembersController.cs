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
	public class AzManApplicationGroupMembersController :BaseApiController
	{
		internal AzManApplicationGroupMembersController() : base() {
		}

		#region Private methods
		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember> getSBOFromListOfApplicationGroupMembers(IEnumerable<NetSqlAzMan.Interfaces.IAzManApplicationGroupMember> members, NetSqlAzMan.Interfaces.IAzManApplicationGroup parentApplicationGroup) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember>();

			var _sboApplicationStoreGroup = new AzManApplicationGroupsController().getSBOFromApplicationGroup(parentApplicationGroup, false);

			foreach (var _member in members) {
				var _gm = new NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember {
					ApplicationGroup = _sboApplicationStoreGroup,
					ApplicationGroupMemberId = _member.ApplicationGroupMemberId,
					SID = _member.WhereDefined == NetSqlAzMan.Interfaces.WhereDefined.Database ? new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_member.SID.BinaryValue, true) : new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_member.SID.BinaryValue),
					IsMember = _member.IsMember,
					DomainProfile = _member.DomainProfile,
					WhereDefined = (NetSqlAzMan.ServiceBusinessObjects.WhereDefined)_member.WhereDefined,
					samAccountName = _member.samAccountName,
					cn = _member.cn,
					displayName = _member.displayName,
					objectSidString = _member.objectSidString,
					distinguishedName = _member.distinguishedName,
					objectClass = _member.objectClass
				};

				_listSBO.Add(_gm);
			}

			return _listSBO;
		}
		#endregion

		/// <summary>
		/// GetAllMembersByApplicationGroup
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="applicationGroup"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember>))]
		public async Task<IHttpActionResult> Get(string store, string application, string applicationGroup) {
			var _parentApplicationGroup = await Task.Run(() => _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup]);

			var _members = await Task.Run(() => _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup].GetApplicationGroupAllMembers());

			var _return = getSBOFromListOfApplicationGroupMembers(_members, _parentApplicationGroup);

			return Ok(_return);
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo))]
		public async Task<IHttpActionResult> Get(string store, string application, string applicationGroup, int applicationGroupMemberId) {
			string _display;
			NetSqlAzMan.Interfaces.MemberType _type;
			var _return = await Task.Run(() => {
				var _agm = _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup].GetApplicationGroupMemberById(applicationGroupMemberId);

				if (_agm == null)
					return null;

				_type = _agm.GetMemberInfo(out _display);

				return new NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo() {
					DisplayName = _display,
					MemberType = (NetSqlAzMan.ServiceBusinessObjects.MemberType)_type
				};
			});

			if (_return == null)
				return NotFound();
			else
				return Ok(_return);
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember>))]
		public async Task<IHttpActionResult> Get(string store, string application, string applicationGroup, bool isMember) {
			var _return = await Task.Run(() => {
				var _parentApplicationGroup = _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup];

				NetSqlAzMan.Interfaces.IAzManApplicationGroupMember[] _sgm;
				if (isMember)
					_sgm = _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup].GetApplicationGroupMembers();
				else
					_sgm = _storage.Stores[store].Applications[application].ApplicationGroups[applicationGroup].GetApplicationGroupNonMembers();

				return this.getSBOFromListOfApplicationGroupMembers(_sgm, _parentApplicationGroup);
			});

			if (_return == null)
				return NotFound();
			else
				return Ok(_return);
		}
	}
}