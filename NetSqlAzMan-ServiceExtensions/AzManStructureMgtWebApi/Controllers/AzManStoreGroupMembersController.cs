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
	public class AzManStoreGroupMembersController :BaseApiController
	{
		internal AzManStoreGroupMembersController() : base() {
		}

		#region Private methods
		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember> getSBOFromListOfStoreGroupMembers(IEnumerable<NetSqlAzMan.Interfaces.IAzManStoreGroupMember> members, NetSqlAzMan.Interfaces.IAzManStoreGroup parentStoreGroup) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>();

			var _sboParentStoreGroup = new AzManStoreGroupsController().getSBOFromStoreGroup(parentStoreGroup, false);

			foreach (var _member in members) {
				var _gm = new NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember {
					StoreGroup = _sboParentStoreGroup,
					StoreGroupMemberId = _member.StoreGroupMemberId,
					SID = _member.WhereDefined == NetSqlAzMan.Interfaces.WhereDefined.Database ? new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_member.SID.StringValue, true) : new NetSqlAzMan.ServiceBusinessObjects.AzManSid(_member.SID.BinaryValue),
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

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>))]
		public async Task<IHttpActionResult> GetAllByStoreGroup(string store, string storeGroup) {
			var _parentStoreGroup = await Task.Run(() => _storage.Stores[store].StoreGroups[storeGroup]);

			var _members = await Task.Run(() => _storage.Stores[store].StoreGroups[storeGroup].GetStoreGroupAllMembers());

			var _return = getSBOFromListOfStoreGroupMembers(_members, _parentStoreGroup);

			return Ok(_return);
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMemberDisplayInfo))]
		public async Task<IHttpActionResult> Get(string store, string storeGroup, int storeGroupMemberId) {
			string _display;
			NetSqlAzMan.Interfaces.MemberType _type;
			var _return = await Task.Run(() => {
				var _sgm = _storage.Stores[store].StoreGroups[storeGroup].GetStoreGroupMemberById(storeGroupMemberId);
				if (_sgm == null)
					return null;

				//var _sgm = _storage.Stores[store].StoreGroups[storeGroup].Members.Where(f => f.Value.StoreGroupMemberId.Equals(storeGroupMemberId));
				//if (_sgm.Count().Equals(0))
				//	return null;

				//_type = _sgm.ToArray()[0].Value.GetMemberInfo(out _display);
				_type = _sgm.GetMemberInfo(out _display);

				return new NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMemberDisplayInfo() {
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
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>))]
		public async Task<IHttpActionResult> Get(string store, string storeGroup, bool isMember) {
			var _return = await Task.Run(() => {
				var _parentStoreGroup = _storage.Stores[store].StoreGroups[storeGroup];

				NetSqlAzMan.Interfaces.IAzManStoreGroupMember[] _sgm;
				if (isMember)
					_sgm = _storage.Stores[store].StoreGroups[storeGroup].GetStoreGroupMembers();
				else
					_sgm = _storage.Stores[store].StoreGroups[storeGroup].GetStoreGroupNonMembers();

				return this.getSBOFromListOfStoreGroupMembers(_sgm, _parentStoreGroup);
			});

			if (_return == null)
				return NotFound();
			else
				return Ok(_return);
		}
	}
}