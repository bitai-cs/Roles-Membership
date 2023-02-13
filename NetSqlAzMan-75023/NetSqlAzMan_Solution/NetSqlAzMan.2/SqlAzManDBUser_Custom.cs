using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan {
	public sealed partial class SqlAzManDBUser {
		private void validateCustomColumns(Dictionary<string, object> customColumns) {
			if (customColumns == null)
				throw new ArgumentException("customColumns");
		}

		#region Constructor
		internal SqlAzManDBUser(bool isLdapEntry, IAzManSid customSid, string domainProfile, string userName, string displayName, Dictionary<string, object> customColumns) {
			this.validateCustomColumns(customColumns);

			_customSid = customSid;
			_userName = userName;
			_displayName = displayName;
			_isLdapEntry = isLdapEntry;
			_domainProfile = domainProfile;
			_customColumns = customColumns;

			if (_isLdapEntry && string.IsNullOrEmpty(_domainProfile))
				throw new Exception("El usuario está marcado como un usuario LDAP y no se ha asignado el domainProfile relacionado.");
		}
		#endregion

		#region Members
		private bool _isLdapEntry;
		[DataMember]
		public bool IsLdapEntry {
			internal set {
				_isLdapEntry = value;
			}
			get {
				return _isLdapEntry;
			}
		}

		private string _domainProfile;
		[DataMember]
		public string DomainProfile {
			set {
				_domainProfile = value;
			}
			get {
				return _domainProfile;
			}
		}

		private string _displayName;
		[DataMember]
		public string DisplayName {
			set {
				_displayName = value;
			}
			get {
				return _displayName;
			}
		}
		#endregion		
	}
}
