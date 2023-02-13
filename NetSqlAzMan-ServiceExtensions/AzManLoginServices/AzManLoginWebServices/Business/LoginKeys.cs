using System;
using System.Collections.Generic;
using System.Text;
namespace AzManLoginWebServices.BusinessLayer
{
	public class LoginKeys
	{
		#region Data Members

		string _LoginId;

		#endregion

		#region Constructor

		public LoginKeys(string LoginId) {
			_LoginId = LoginId;
		}

		#endregion

		#region Properties

		public string LoginId {
			get {
				return _LoginId;
			}
		}

		#endregion
	}
}