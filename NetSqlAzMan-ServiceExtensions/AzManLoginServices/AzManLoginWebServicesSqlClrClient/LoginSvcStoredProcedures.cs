using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Configuration;

namespace AzManLoginWebServicesSqlClrClient
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

	/// <summary>
	/// Conjunto de Stored Procedures para consulta al servicio LOGIN
	/// </summary>
	public partial class LoginSvcStoredProcedures
	{
		internal const string APPSETTINGKEY_WsUrl = "AzManLoginWebServices.LoginService.Url";

		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzManLogin_Test(SqlString input, out SqlString output, out SqlBoolean result, out SqlString message) {
			const string striMemberName = "AzManLogin_Test";

			output = null;
			message = null;

			try {
				string _url = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl);

				AzManLoginWebServicesClient.LoginSvcRef.LoginService _svc = new AzManLoginWebServicesClient.LoginSvcRef.LoginService();
				_svc.Url = _url;

				string _output;
				bool _result;
				bool _dummy1;

				_svc.Test(input.ToString(), out _result, out _dummy1, out _output);

				output = _output;

				result = true;
			}
			catch (Exception ex) {
				message = string.Format("{0}:\n\r{1}\n\r{2}", striMemberName, ex.Message, ex.StackTrace);

				result = false;
			}
		}

		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzManLogin_GetLogin(SqlString token, SqlString user, SqlString appName, out SqlInt32 userId, out SqlString attributeString, out SqlInt32 loginStatus, out SqlBoolean result, out SqlString message) {
			const string striMemberName = "AzManLogin_GetLogin";
			string striSvcUrl = string.Empty;

			userId = -1;
			attributeString = null;
			loginStatus = -1;
			result = false;
			message = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl);

				AzManLoginWebServicesClient.LoginSvcRef.DBUser _user = null;
				AzManLoginWebServicesClient.LoginSvcRef.LoginInfo _login = null;
				bool _result;
				bool _dummy1;
				string _att;

				AzManLoginWebServicesClient.LoginSvcRef.LoginService svc = new AzManLoginWebServicesClient.LoginSvcRef.LoginService();
				svc.Url = striSvcUrl;

				//Llamar al WS
				svc.GetLogin(token.ToString(), user.ToString(), appName.ToString(), out _result, out _dummy1, out _user, out  _login, out _att);

				if (!_result)
					throw new Exception(_att);

				if (_login == null || _user == null)
					throw new Exception("No se pudo obtener la información de login y/o usuario.");

				//Obtener datos del usuario
				userId = _user.UserID;
				attributeString = _user.AttributeString;

				//Obtener datos del login
				loginStatus = (Int32)_login.Status;

				result = true;
			}
			catch (Exception ex) {
				message = string.Format("{0}:\n\r{1}\n\r{2}", striMemberName, ex.Message, ex.StackTrace);
				result = false;
			}
		}

		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzManLogin_GetUserPropertyValueFromAttString(SqlString attributeString, SqlInt32 userProperty, out SqlString propertyValue, out SqlBoolean result, out SqlString message) {
			const string striMemberName = "AzManLogin_GetUserPropertyValueFromAttString";

			propertyValue = null;
			message = null;
			try {
				string _enum = ((UserProperty)Enum.Parse(typeof(UserProperty), userProperty.ToString())).ToString();

				propertyValue = null;
				string[] propArray = attributeString.ToString().Split(Convert.ToChar(124));
				foreach (string pair in propArray) {
					string[] pairArray = pair.Split(Convert.ToChar(61));
					if (pairArray[0].ToLower().Equals(_enum.ToLower())) {
						propertyValue = pairArray[1];
						break;
					}
				}

				if (string.IsNullOrEmpty(propertyValue.ToString()))
					throw new Exception(string.Format("No se ubicó la propiedad {0}.", userProperty.ToString()));

				result = true;
			}
			catch (Exception ex) {
				message = string.Format("{0}:\n\r{1}\n\r{2}", striMemberName, ex.Message, ex.StackTrace);

				result = false;
			}
		}

		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzManLogin_CheckLoginAccess(SqlString store, SqlString app, SqlString item, SqlString loginId, SqlString userName, out SqlInt32 loginStatus, out SqlInt32 aut, out SqlBoolean result, out SqlString message) {
			const string striMemberName = "AzManLogin_CheckLoginAccess";
			string striSvcUrl = null;

			loginStatus = -1;
			aut = -1;
			message = null;
			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl);

				AzManLoginWebServicesClient.LoginSvcRef.LoginService _svc = new AzManLoginWebServicesClient.LoginSvcRef.LoginService();
				_svc.Url = striSvcUrl;

				bool _result;
				bool _dummy1;
				AzManLoginWebServicesClient.LoginSvcRef.DBUser _dbUser = null;
				AzManLoginWebServicesClient.LoginSvcRef.LoginInfo _login = null;
				string _att = null;
				_svc.GetLogin(loginId.Value, userName.Value, "???", out _result, out _dummy1, out _dbUser, out _login, out _att);

				if (!_result)
					throw new Exception(_att);

				AzManLoginWebServicesClient.LoginSvcRef.LoginStatusEnum _loginStatus;
				bool _dummy2;
				AzManLoginWebServicesClient.LoginSvcRef.AuthorizationType _aut;
				bool _dummy3;
				_svc.CheckLoginAccess(store.Value, app.Value, item.Value, _login, out _result, out  _dummy1, out _loginStatus, out _dummy2, out _aut, out _dummy3, out _att);

				if (_result) {
					loginStatus = (int)_loginStatus;
					aut = (int)_aut;
				}
				else
					throw new Exception(_att);

				result = true;
			}
			catch (Exception ex) {
				message = string.Format("{0}:\n\r{1}\n\r{2}", striMemberName, ex.Message, ex.StackTrace);

				result = false;
			}
		}

		internal static class ConfigurationManager
		{
			internal static string AppSettings(string key) {
				int _firstcall = System.Configuration.ConfigurationManager.ConnectionStrings.Count;

				return System.Configuration.ConfigurationManager.AppSettings[key];
			}
		}
	}
}