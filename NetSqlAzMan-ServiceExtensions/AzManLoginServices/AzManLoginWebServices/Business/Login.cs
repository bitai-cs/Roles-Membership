using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AzManLoginWebServices.BusinessLayer
{
	public class Login : BusinessObjectBase
	{
		#region InnerClass
		public enum LoginFields
		{
			LoginId,
			AppName,
			LdapDomain,
			UserName,
			RemoteIpV4,
			LogIn,
			Expires,
			Timeout,
			Expiration,
			Expired,
			LogOff,
			LoggedOut,
			Renovated,
			Renewal
		}
		#endregion

		#region Data Members

		string _LoginId;
		string _AppName;
		string _LDAPDomain;
		string _UserName;
		string _RemoteIpV4;
		DateTime _LogIn;
		DateTime _Expires;
		int _Timeout;
		Nullable<DateTime> _Expiration;
		bool _Expired;
		Nullable<DateTime> _LogOff;
		bool _LoggedOut;
		bool _Renovated;
		Nullable<DateTime> _Renewal;

		#endregion

		#region Constructor

		public Login() {
		}

		public Login(string ldapDomain, string userName, string appName, string remoteAddress) {
			this.LoginId = Guid.NewGuid().ToString();
			this.LDAPDomain = ldapDomain;
			this.UserName = userName;
			this.AppName = appName;
			this.LogIn = DateTime.Now;
			this.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["Login_TimeOut"]);
			this.Expires = this.LogIn.AddSeconds(this.Timeout);
			this.Expiration = null;
			this.Expired = false;
			this.RemoteIpV4 = remoteAddress;
			this.LogOff = null;
			this.LoggedOut = false;
			this.Renovated = false;
			this.Renewal = null;
		}

		#endregion

		#region Properties

		public string LoginId {
			get {
				return _LoginId;
			}
			internal set {
				if (_LoginId != value) {
					_LoginId = value;
					PropertyHasChanged("LoginId");
				}
			}
		}

		public string AppName {
			get {
				return _AppName;
			}
			set {
				if (_AppName != value) {
					_AppName = value;
					PropertyHasChanged("AppName");
				}
			}
		}

		public string UserName {
			get {
				return _UserName;
			}
			set {
				if (_UserName != value) {
					_UserName = value;
					PropertyHasChanged("UserName");
				}
			}
		}

		public string LDAPDomain {
			get {
				return _LDAPDomain;
			}
			set {
				if (_LDAPDomain != value) {
					_LDAPDomain = value;
					PropertyHasChanged("LDAPDomain");
				}
			}
		}

		public string RemoteIpV4 {
			get {
				return _RemoteIpV4;
			}
			set {
				if (_RemoteIpV4 != value) {
					_RemoteIpV4 = value;
					PropertyHasChanged("RemoteIpV4");
				}
			}
		}

		public DateTime LogIn {
			get {
				return _LogIn;
			}
			set {
				if (_LogIn != value) {
					_LogIn = value;
					PropertyHasChanged("LogIn");
				}
			}
		}

		public DateTime Expires {
			get {
				return _Expires;
			}
			set {
				if (_Expires != value) {
					_Expires = value;
					PropertyHasChanged("Expires");
				}
			}
		}

		public int Timeout {
			get {
				return _Timeout;
			}
			set {
				if (_Timeout != value) {
					_Timeout = value;
					PropertyHasChanged("Timeout");
				}
			}
		}

		public Nullable<DateTime> Expiration {
			get {
				return _Expiration;
			}
			set {
				if (_Expiration != value) {
					_Expiration = value;
					PropertyHasChanged("Expiration");
				}
			}
		}

		public bool Expired {
			get {
				return _Expired;
			}
			set {
				if (_Expired != value) {
					_Expired = value;
					PropertyHasChanged("Expired");
				}
			}
		}

		public Nullable<DateTime> LogOff {
			get {
				return _LogOff;
			}
			set {
				if (_LogOff != value) {
					_LogOff = value;
					PropertyHasChanged("LogOff");
				}
			}
		}

		public bool LoggedOut {
			get {
				return _LoggedOut;
			}
			set {
				if (_LoggedOut != value) {
					_LoggedOut = value;
					PropertyHasChanged("LoggedOut");
				}
			}
		}

		public bool Renovated {
			get {
				return _Renovated;
			}
			set {
				if (_Renovated != value) {
					_Renovated = value;
					PropertyHasChanged("Renovated");
				}
			}
		}

		public Nullable<DateTime> Renewal {
			get {
				return _Renewal;
			}
			set {
				if (_Renewal != value) {
					_Renewal = value;
					PropertyHasChanged("Renewal");
				}
			}
		}

		#endregion

		#region Validation

		internal override void AddValidationRules() {
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("LoginId", "LoginId"));
			ValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength("LoginId", "LoginId", 255));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("AppName", "AppName"));
			ValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength("AppName", "AppName", 255));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("UserName", "UserName"));
			ValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength("UserName", "UserName", 255));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("RemoteIpV4", "RemoteIpV4"));
			ValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength("RemoteIpV4", "RemoteIpV4", 15));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("LogIn", "LogIn"));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("Expires", "Expires"));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("Timeout", "Timeout"));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("Expired", "Expired"));
			ValidationRules.AddRules(new Validation.ValidateRuleNotNull("LoggedOut", "LoggedOut"));
		}

		#endregion
	}
}