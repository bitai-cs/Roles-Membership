using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzManLoginHelper
{
	public class HelperCreateLoginCompletedEventArgs : EventArgs
	{
		public HelperCreateLoginCompletedEventArgs(Exception error, bool cancelled, object userState) {
			Error = error;
			Cancelled = cancelled;
			UserState = userState;
		}

		public bool Cancelled {
			get;
			internal set;
		}

		public Exception Error {
			get;
			internal set;
		}

		public object UserState {
			get;
			internal set;
		}

		public bool Response_CreateLoginResult;

		public AzManLoginHelper.AzManLoginSvcRef.DBUser Response_dbUser;

		public AzManLoginHelper.AzManLoginSvcRef.LoginInfo Response_login;

		public AzManLoginHelper.AzManLoginSvcRef.AuthorizationType Response_authorization;

		public string Response_outputString;
	}
}
