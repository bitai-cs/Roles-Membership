using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace AzManStructureMgtWebApi.Controllers.Helpers {
	public class LDAPHelper {
		#region Enums
		/// <summary>
		/// Commob Object properties
		/// </summary>
		public enum ADProperties {
			objectGuid,
			objectSid,
			telephoneNumber,
			samAccountName,
			manager,
			title,
			givenName,
			sn,
			cn,
			canonicalName,
			userPrincipalName,
			/// <summary>
			/// country abrev.
			/// </summary>
			c,
			company,
			createTimeStamp,
			defaultClassStore,
			department,
			description,
			displayName,
			distinguishedName,
			isDeleted,
			lastLogon,
			objectCategory,
			objectClass,
			memberOf,
			mail
		}
		#endregion

		internal static NetSqlAzMan.ServiceBusinessObjects.LDAPResult mapToObject(string directoryEntryPath, string user, string password, SearchResult result) {
			NetSqlAzMan.ServiceBusinessObjects.LDAPResult _ldapResult = new NetSqlAzMan.ServiceBusinessObjects.LDAPResult();
			_ldapResult.DirectoryEntryPath = directoryEntryPath;

			if (result.Properties[ADProperties.objectGuid.ToString()].Count > 0) {
				byte[] _bytes = (byte[])result.Properties[ADProperties.objectGuid.ToString()][0];
				Guid _guid = new Guid(_bytes);
				_ldapResult.ObjectGuid = _guid.ToString();
				_ldapResult.ObjectGuidBytes = _bytes;
			}

			if (result.Properties[ADProperties.objectSid.ToString()].Count > 0) {
				byte[] _bytes = (byte[])result.Properties[ADProperties.objectSid.ToString()][0];
				System.Security.Principal.SecurityIdentifier _sid = new System.Security.Principal.SecurityIdentifier(_bytes, 0);
				_ldapResult.ObjectSid = _sid.ToString();
				_ldapResult.ObjectSidBytes = _bytes;
			}

			if (result.Properties[ADProperties.title.ToString()].Count > 0)
				_ldapResult.Title = result.Properties[ADProperties.title.ToString()][0].ToString();

			if (result.Properties[ADProperties.distinguishedName.ToString()].Count > 0)
				_ldapResult.DistinguishedName = result.Properties[ADProperties.distinguishedName.ToString()][0].ToString();

			if (result.Properties[ADProperties.displayName.ToString()].Count > 0)
				_ldapResult.DisplayName = result.Properties[ADProperties.displayName.ToString()][0].ToString();

			if (result.Properties[ADProperties.telephoneNumber.ToString()].Count > 0)
				_ldapResult.TelephoneNumber = result.Properties[ADProperties.telephoneNumber.ToString()][0].ToString();

			if (result.Properties[ADProperties.samAccountName.ToString()].Count > 0)
				_ldapResult.samAccountName = result.Properties[ADProperties.samAccountName.ToString()][0].ToString();

			if (result.Properties[ADProperties.manager.ToString()].Count > 0)
				_ldapResult.Manager = result.Properties[ADProperties.manager.ToString()][0].ToString();

			if (result.Properties[ADProperties.department.ToString()].Count > 0)
				_ldapResult.Department = result.Properties[ADProperties.department.ToString()][0].ToString();

			if (result.Properties[ADProperties.givenName.ToString()].Count > 0)
				_ldapResult.GivenName = result.Properties[ADProperties.givenName.ToString()][0].ToString();

			if (result.Properties[ADProperties.sn.ToString()].Count > 0)
				_ldapResult.SN = result.Properties[ADProperties.sn.ToString()][0].ToString();

			if (result.Properties[ADProperties.cn.ToString()].Count > 0)
				_ldapResult.CN = result.Properties[ADProperties.cn.ToString()][0].ToString();

			if (result.Properties[ADProperties.userPrincipalName.ToString()].Count > 0)
				_ldapResult.UserPrincipalName = result.Properties[ADProperties.userPrincipalName.ToString()][0].ToString();

			if (result.Properties[ADProperties.c.ToString()].Count > 0)
				_ldapResult.C = result.Properties[ADProperties.c.ToString()][0].ToString();

			if (result.Properties[ADProperties.company.ToString()].Count > 0)
				_ldapResult.Company = result.Properties[ADProperties.company.ToString()][0].ToString();

			if (result.Properties[ADProperties.createTimeStamp.ToString()].Count > 0)
				_ldapResult.CreateTimeStamp = Convert.ToDateTime(result.Properties[ADProperties.createTimeStamp.ToString()][0]);

			if (result.Properties[ADProperties.defaultClassStore.ToString()].Count > 0)
				_ldapResult.defaultClassStore = result.Properties[ADProperties.defaultClassStore.ToString()][0].ToString();

			if (result.Properties[ADProperties.description.ToString()].Count > 0)
				_ldapResult.Description = result.Properties[ADProperties.description.ToString()][0].ToString();

			if (result.Properties[ADProperties.isDeleted.ToString()].Count > 0)
				_ldapResult.IsDeleted = (bool)result.Properties[ADProperties.isDeleted.ToString()][0];

			if (result.Properties[ADProperties.lastLogon.ToString()].Count > 0)
				_ldapResult.LastLogon = (long)result.Properties[ADProperties.lastLogon.ToString()][0];

			if (result.Properties[ADProperties.objectCategory.ToString()].Count > 0)
				_ldapResult.ObjectCategory = result.Properties[ADProperties.objectCategory.ToString()][0].ToString();

			if (result.Properties[ADProperties.objectClass.ToString()].Count > 0) {
				string[] _values = new string[result.Properties[ADProperties.objectClass.ToString()].Count];
				result.Properties[ADProperties.objectClass.ToString()].CopyTo(_values, 0);
				_ldapResult.ObjectClass = _values;
			}

			if (result.Properties[ADProperties.memberOf.ToString()].Count > 0) {
				string[] _values = new string[result.Properties[ADProperties.memberOf.ToString()].Count];
				result.Properties[ADProperties.memberOf.ToString()].CopyTo(_values, 0);
				_ldapResult.MemberOf = _values;
			}

			if (result.Properties[ADProperties.mail.ToString()].Count > 0)
				_ldapResult.Mail = result.Properties[ADProperties.mail.ToString()][0].ToString();

			return _ldapResult;
		}
	}
}