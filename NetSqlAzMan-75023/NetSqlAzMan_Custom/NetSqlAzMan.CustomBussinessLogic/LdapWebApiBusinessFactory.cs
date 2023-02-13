using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class LdapWebApiBusinessFactory : BaseBusinessFactory {
		#region Authentication
		public async Task<LdapHelperDTO.AuthenticationStatus> AuthenticateUserAsync(string domainProfile, Nullable<bool> useGC, string userName, string password) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentNullException("domainProfile");

			if (useGC == null)
				throw new ArgumentNullException("useGC");

			if (string.IsNullOrEmpty(userName))
				throw new ArgumentNullException("userName");

			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			var _domainProfile = await new LdapWacDomainProfileBusinessFactory().GetLdapDomainProfileByDomainProfileAsync(domainProfile);
			if (_domainProfile == null)
				throw new Exception(string.Format("El DomainProfile: {0} no existe.", domainProfile));

			LdapHelperDTO.AuthenticationStatus _status = null;
			#region Call WebApi: Autenticar usuario y contraseña			
			var _h = new LdapWebApiClientHelpers.LdapAuthenticationHelper<LdapHelperDTO.AuthenticationStatus>(_domainProfile.LdapProxyWebApiUri);
			var _return = await _h.GetAuthenticationAsync(_domainProfile.LdapServerProfile, useGC.Value, _domainProfile.DomainProfile, _domainProfile.DomainName, userName, password);
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_status = _h.GetSBOFromReturnedContent(_return);
			#endregion

			return _status;
		}
		#endregion


		#region User and Groups
		public async Task<LdapHelperDTO.AsyncResult> SearchUsersAndGroupsAsyncModeAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, string filter, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentNullException("domainProfile");

			if (useGC == null)
				throw new ArgumentNullException("useGC");

			if (baseDNOrder == null)
				throw new ArgumentNullException("baseDNOrder");

			if (string.IsNullOrEmpty(filter))
				throw new ArgumentNullException("filter");

			if (requiredEntryAttributes == null)
				throw new ArgumentNullException("requiredEntryAttributes");

			var _serverBaseDN = await new LdapWacServerBaseDNBusinessFactory().GetLdapServerBaseDNByDomainProfileAndBaseDNOrderAsync(domainProfile, baseDNOrder);

			LdapHelperDTO.AsyncResult _asyncResult = null;
			#region Call WebApi				
			var _h = new LdapWebApiClientHelpers.LdapUserAndGroupHelper<LdapHelperDTO.AsyncResult>(_serverBaseDN.DomainProfile.LdapProxyWebApiUri);
			var _return = await _h.GetUserAndGroupsByAttributesAsyncModeAsync(_serverBaseDN.DomainProfile.LdapServerProfile, useGC.Value, domainProfile, _serverBaseDN.BaseDN, LdapHelperDTO.EntryAttribute.sAMAccountName, filter, false, LdapHelperDTO.EntryAttribute.cn, filter, requiredEntryAttributes);
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_asyncResult = _h.GetSBOFromReturnedContent(_return);
			#endregion

			return _asyncResult;
		}
		#endregion


		#region Entries
		public async Task<LdapHelperDTO.AsyncResult> SearchEntriesAsyncModeAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, Nullable<LdapHelperDTO.EntryAttribute> attribute, string filter, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentOutOfRangeException("domainProfile");

			if (useGC == null)
				throw new ArgumentOutOfRangeException("useGC");

			if (baseDNOrder == null)
				throw new ArgumentOutOfRangeException("baseDNOrder");

			if (attribute == null)
				throw new ArgumentOutOfRangeException("attribute");

			if (string.IsNullOrEmpty(filter))
				throw new ArgumentOutOfRangeException("criteria");

			if (requiredEntryAttributes == null)
				throw new ArgumentNullException("requiredEntryAttributes");

			var _serverBaseDN = await new LdapWacServerBaseDNBusinessFactory().GetLdapServerBaseDNByDomainProfileAndBaseDNOrderAsync(domainProfile, baseDNOrder);

			LdapHelperDTO.AsyncResult _asyncResult = null;
			#region Call WebApi
			var _h = new LdapWebApiClientHelpers.LdapEntryHelper<LdapHelperDTO.AsyncResult>(_serverBaseDN.DomainProfile.LdapProxyWebApiUri);
			var _return = await _h.GetEntriesByAttributeAsyncModeAsync(_serverBaseDN.DomainProfile.LdapServerProfile, useGC.Value, domainProfile, _serverBaseDN.BaseDN, attribute.Value, filter, requiredEntryAttributes);
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_asyncResult = _h.GetSBOFromReturnedContent(_return);
			#endregion

			return _asyncResult;
		}
		#endregion


		#region Group Memebership
		public async Task<IEnumerable<LdapHelperDTO.LdapEntry>> SearchGroupMembershipAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, Nullable<LdapHelperDTO.KeyEntryAttribute> attributeFilter, string filterValue, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentNullException(domainProfile);

			if (useGC == null)
				throw new ArgumentOutOfRangeException("useGC");

			if (baseDNOrder == null)
				throw new ArgumentOutOfRangeException("baseDNOrder");

			if (attributeFilter == null)
				throw new ArgumentOutOfRangeException("attributeFilter");

			if (string.IsNullOrEmpty(filterValue))
				throw new ArgumentNullException("filterValue");

			if (requiredEntryAttributes == null)
				throw new ArgumentNullException("requiredEntryAttributes");

			LdapHelperDTO.EntryAttribute _attribute;
			switch (attributeFilter) {
				case LdapHelperDTO.KeyEntryAttribute.distinguishedName:
					_attribute = LdapHelperDTO.EntryAttribute.distinguishedName;
					break;
				case LdapHelperDTO.KeyEntryAttribute.objectSid:
					_attribute = LdapHelperDTO.EntryAttribute.objectSid;
					break;
				case LdapHelperDTO.KeyEntryAttribute.sAMAccountName:
					_attribute = LdapHelperDTO.EntryAttribute.sAMAccountName;
					break;
				default:
					throw new ArgumentOutOfRangeException("attributeFilter");
			}

			var _serverBaseDN = await new LdapWacServerBaseDNBusinessFactory().GetLdapServerBaseDNByDomainProfileAndBaseDNOrderAsync(domainProfile, baseDNOrder);

			IEnumerable<LdapHelperDTO.LdapEntry> _entries = null;
			#region Call WebApi			
			var _h = new LdapWebApiClientHelpers.LdapGroupMembershipHelper<LdapHelperDTO.LdapEntry>(_serverBaseDN.DomainProfile.LdapProxyWebApiUri);
			var _return = await _h.GetGroupMembershipAsync(_serverBaseDN.DomainProfile.LdapServerProfile, useGC.Value, domainProfile, _serverBaseDN.BaseDN, _attribute, filterValue, requiredEntryAttributes);
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_entries = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion

			return _entries;
		}
		#endregion
	}
}