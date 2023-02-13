using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
	public sealed partial class SqlAzManDBUser
	{
		private void validateCustomColumns(Dictionary<string, object> customColumns) {
			if (customColumns == null)
				throw new ArgumentException("customColumns");			
		}

		#region Constructor
		internal SqlAzManDBUser(bool isLDAPObject, IAzManSid customSid, string LDAPDomain, string userName, string displayName, Dictionary<string, object> customColumns) {
			this.validateCustomColumns(customColumns);

			_customSid = customSid;
			_userName = userName;
			_displayName = displayName;
			_isLDAPObject = isLDAPObject;
			_LDAPDomain = LDAPDomain;
			_customColumns = customColumns;

			if (_isLDAPObject && string.IsNullOrEmpty(_LDAPDomain))
				throw new Exception("El usuario pertenece a un dominio que no ha sido especificado.");
		}
		#endregion

		#region Members
		private bool _isLDAPObject;
		[DataMember]
		public bool IsLDAPObject {
			internal set {
				_isLDAPObject = value;
			}
			get {
				return _isLDAPObject;
			}
		}

		private string _LDAPDomain;
		[DataMember]
		public string LDAPDomain {
			set {
				_LDAPDomain = value;
			}
			get {
				return _LDAPDomain;
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
