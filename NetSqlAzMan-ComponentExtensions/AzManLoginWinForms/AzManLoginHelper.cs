using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using AzManLoginComponentUI.AzManLoginSvcRef;

namespace AzManLoginComponentUI
{
	internal class AzManLoginHelper
	{
		#region Enumerados de Propiedades de Usuario
		public enum UserProperty
		{
			UserID,
			FirstName,
			LastName,
			FullName,
			Mail,
			BranchOfficeIds,
			RelativeBranchOfficeIds,
			BranchOfficeNames,
			DepartmentId,
			DepartmentName,
			Enabled
		}
		#endregion

		private static LoginServiceClient getLoginServiceClient() {
			//Inicializar el cliente del Web Service de Logins
			//return new LoginServiceClient(ConfigurationManager.AppSettings[AppHelper.CFG_LoginSvc_EndPointName], ConfigurationManager.AppSettings[AppHelper.CFG_LoginSvc_FullWebPath]);
			return new LoginServiceClient();
		}

		//public static bool CheckLoginStatusAndAccess(Enum item, out LoginStatusEnum status, out AuthorizationType authorization, out string attributeString) {
		////Obtener el Login de la sesion
		//LoginInfo _login = AppHelper.GetLoginInfoFromProgram();

		////Obtener el cliente del WS de Login
		//LoginServiceClient _client = AzManLoginHelper.GetLoginServiceClient();

		////Verificar acceso del Login
		//return _client.CheckLoginAccess(out status, out authorization, out attributeString, ConfigurationManager.AppSettings[AppHelper.CFG_AzMan_Store], ConfigurationManager.AppSettings[AppHelper.CFG_AzMan_App], item.ToString(), _login);
		//}

		public static bool GetLoginByToken(string loginId, string userName, out DBUser user, out LoginInfo login, out string attributeString) {
			//Obtener el cliente del WS de Login
			LoginServiceClient _client = AzManLoginHelper.getLoginServiceClient();

			//Verificar acceso del Login
			return _client.GetLogin(out user, out login, out attributeString, loginId, userName, ConfigurationManager.AppSettings[AppHelper.CFG_AzMan_App]);
		}

		public static bool GetUserPropertyValueFromAttString(DBUser user, UserProperty userProperty, out string propertyValue, out Exception hex) {
			propertyValue = null;
			hex = null;
			try {
				propertyValue = null;
				string[] propArray = user.AttributeString.Split(Convert.ToChar(124));
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
	}
}
