using System;
using System.Collections.Generic;
using System.Text;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManLoginHelper
{
	public static class LoginHelper
	{
		#region Properties
		private static string _webServiceUrl;
		public static string WebServiceUrl {
			set {
				_webServiceUrl = value;
			}
			get {
				return _webServiceUrl;
			}
		}
		#endregion

		#region Private methods
		private static LoginService getLoginServiceClient() {
			if (string.IsNullOrEmpty(WebServiceUrl))
				return new LoginService();
			else {
				return new LoginService {
					Url = WebServiceUrl
				};
			}
		}
		#endregion

		#region Public methods
		public static bool CreateLogin(string userName, string password, string clientApplication, string azManStore, string azManApplication, string azManItem, LoginInfo loginInfo, DBUser dbUser, AuthorizationType authorizationType, string outputString) {
			bool _result;
			bool _resultSpecified;
			bool _authorizationSpecified;
			LoginService _client = getLoginServiceClient();
			_client.CreateLogin(userName, password, clientApplication, azManStore, azManApplication, azManItem, out _result, out _resultSpecified, out dbUser, out loginInfo, out authorizationType, out _authorizationSpecified, out outputString);

			return _result;
		}

		public static bool CheckLoginStatusAndAuthorization(LoginInfo loginInfo, string azManStore, string azManApplication, string azManItem, out LoginStatusEnum loginStatus, out AuthorizationType authorizationType, out string outputString) {
			LoginService _client = getLoginServiceClient();
			bool _result;
			bool _resultSpecified;
			bool _loginStatusSpecified;
			bool _authorizationTypeSpecified;
			_client.CheckLoginStatusAndAuthorization(azManStore, azManApplication, azManItem, loginInfo, out _result, out _resultSpecified, out loginStatus, out _loginStatusSpecified, out authorizationType, out _authorizationTypeSpecified, out outputString);

			//Verificar acceso del Login
			return _result;
		}

		public static bool GetLoginByToken(string loginId, string userName, out DBUser dbUser, out LoginInfo loginInfo, out string outputString) {
			LoginService _client = getLoginServiceClient();
			bool _result;
			bool _resultSpecified;
			_client.GetLoginByIdAndUser(loginId, userName, out _result, out _resultSpecified, out dbUser, out loginInfo, out outputString);

			return _result;
		}

		public static bool GetUserPropertyValueFromAttString(DBUser dbUser, UserPropertyEnum userProperty, out string propertyValue, out Exception hex) {
			propertyValue = null;
			hex = null;
			try {
				propertyValue = null;
				string[] propArray = dbUser.AttributeString.Split(Convert.ToChar(124));
				foreach (string pair in propArray) {
					string[] pairArray = pair.Split(Convert.ToChar(61));
					if (pairArray[0].ToLower().Equals(userProperty.ToString().ToLower())) {
						propertyValue = pairArray[1];
						return true;
					}
				}
				return false;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}
		#endregion
	}
}
