using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AzManLoginWebServices
{
	[DataContract]
	public class LoginInfo
	{
		#region Private fields
		Guid _loginGuid;
		string _loginId;
		string _ldapDomain;
		string _userName;
		string _appName;
		int _timeOut;
		LoginStatusEnum _status;
		//AzManLoginWebServices.DirectSvcRef.DBUser _userInfo;
		#endregion

		#region Constructor
		internal LoginInfo(Guid loginGuid) {
			_loginGuid = loginGuid;
			_loginId = loginGuid.ToString();
			_status = LoginStatusEnum.Unknown;
		}
		#endregion

		#region Propeties

		[DataMember]
		public Guid LoginGuid {
			internal set {
				_loginGuid = value;
			}
			get {
				return _loginGuid;
			}
		}

		[DataMember]
		public string LoginId {
			internal set {
				_loginId = value;
			}
			get {
				return _loginGuid.ToString();
			}
		}

		[DataMember]
		public string AppName {
			internal set {
				_appName = value;
			}
			get {
				return _appName;
			}
		}

		[DataMember]
		public string LDAPDomain {
			internal set {
				_ldapDomain = value;
			}
			get {
				return _ldapDomain;
			}
		}

		[DataMember]
		public string UserName {
			internal set {
				_userName = value;
			}
			get {
				return _userName;
			}
		}

		[DataMember]
		public int TimeOut {
			internal set {
				_timeOut = value;
			}
			get {
				return _timeOut;
			}
		}

		[DataMember]
		public LoginStatusEnum Status {
			internal set {
				_status = value;
			}
			get {
				return _status;
			}
		}

		#endregion
	}
}
