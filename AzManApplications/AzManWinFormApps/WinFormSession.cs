using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManWinFormApps
{
	public sealed class WinFormSession
	{
		private LoginInfo _login = null;
		private DBUser _user = null;
		private string _local = null;
		private string _localName = null;

		public LoginInfo LoginInfo {
			get {
				return _login;
			}
			set {
				_login = value;
			}
		}

		public DBUser DBUser {
			get {
				return _user;
			}
			set {
				_user = value;
			}
		}

		public string BranchId {
			get {
				return _local;
			}
			set {
				_local = value;
			}
		}

		public string BranchName {
			get {
				return _localName;
			}
			set {
				_localName = value;
			}
		}
	}
}