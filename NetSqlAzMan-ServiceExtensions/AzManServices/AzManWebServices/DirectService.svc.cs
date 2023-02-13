using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.ServiceModel;
using NetSqlAzMan;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;

namespace AzManWebServices
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
	public class DirectService : IDirectService
	{
		/// <summary>
		/// Acumula en una cadena cada par del diccionario para mejorar la serializacion del objeto DBUser 
		/// </summary>
		/// <param name="customColumns"></param>
		/// <returns>Cadena con los nombre y los valores de los demas atributos del usuario</returns>
		private string getAttributeString(Dictionary<string, object> customColumns) {
			string result = null;
			string val = null;
			foreach (KeyValuePair<string, object> entry in customColumns) {
				val = DBNull.Value.Equals(entry.Value) ? string.Empty : entry.Value.ToString();
				result = result + entry.Key + Convert.ToChar(61).ToString() + val + Convert.ToChar(124).ToString();
			}

			if (!string.IsNullOrWhiteSpace(result))
				result = result.Substring(0, result.Length - 1);

			return result;
		}

		/// <summary>
		/// Método de prueba del servicio
		/// </summary>
		/// <param name="input">Un valor cualquiera</param>
		/// <param name="output">La respuesta del servicio</param>
		/// <returns></returns>
		public bool Test(string input, out string output) {
			output = string.Format("You sent to AzManWebServices->DirectService->Test this input \"{0}\"", input);
			return true;
		}

		/// <summary>
		/// Se obtiene la informacion del usuario (sin su password)
		/// </summary>
		/// <param name="userName"></param>
		/// <returns>Un objeto DBUser con la informacion del usuario</returns>
		[Obsolete("Ahora usar el método GetUser.")]
		public DBUser DirectGetDBUser(string userName) {
			IAzManDBUser user = (new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString)).GetDBUser(userName);

			DBUser result = new DBUser();
			result.UserID = Int32.Parse(user.CustomColumns["UserID"].ToString()); //Convert.ToInt32(user.CustomSid.StringValue);
			result.UserName = user.UserName;
			result.AttributeString = getAttributeString(user.CustomColumns);

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="azManUser"></param>
		/// <param name="statusType"></param>
		/// <param name="status"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public bool GetUser(string ldapDomain, string userName, out SqlAzManDBUser azManUser, out string statusType, out string status, out string stackTrace) {
			azManUser = null;
			statusType = null;
			status = null;
			stackTrace = null;
			try {
				SqlAzManStorage _storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);

				if (string.IsNullOrEmpty(ldapDomain)) { //DB User
					azManUser = (SqlAzManDBUser)_storage.GetDBUser(userName);
				}
				else { //LDAP User
					if (!_storage.GetLDAPUser(ldapDomain, userName, out azManUser, out statusType, out status, out stackTrace))
						return false;
				}

				//DBUser result = new DBUser();
				//result.UserID = Int32.Parse(user.CustomColumns["UserID"].ToString()); //Convert.ToInt32(user.CustomSid.StringValue);
				//result.UserName = user.UserName;
				//result.AttributeString = getAttributeString(user.CustomColumns);

				return true;
			}
			catch (Exception ex) {
				statusType = ex.GetType().Name;
				status = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
		}

		/// <summary>
		/// Se valida el password de un usuario
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <returns>True si el usuario y el password son correctos. False si el usuario y/o el password son incorrectos</returns>
		[Obsolete("Ahora usar método ValidatePassword")]
		public bool DirectValidatePassword(string userName, string password) {
			IAzManStorage storage = null;

			try {
				storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);
				storage.OpenConnection();

				IAzManDBUser user = storage.GetDBUserWithPassword(userName);

				return user.CustomColumns["PasswordString"].ToString().Equals(password);
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				storage.CloseConnection();
			}
		}

		/// <summary>
		/// VBastidas: Verificar directamente en el Storage la autorizacion
		/// que tiene un AzManDBUser a determinado Item, sin atributos de 
		/// retorno. 
		/// </summary>
		/// <param name="store">Store name</param>
		/// <param name="app">Application Name</param>
		/// <param name="item">Item name</param>
		/// <param name="DBuserSSid">AzManDbUser Id or name</param>
		/// <param name="validFor">Fecha de vigencia, si la tuviese</param>
		/// <param name="operationsOnly">Solo items Operation</param>
		/// <param name="contextParameters">Valores adicionales ???</param>
		/// <returns></returns>
		public AuthorizationType DirectCheckAccess(string store, string app, string item, string DBuserSSid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters) {
			IAzManStorage storage = null;

			try {
				storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);
				storage.OpenConnection();
				IAzManDBUser user = storage.GetDBUser(DBuserSSid);
				AuthorizationType aut = storage.CheckAccess(store, app, item, user, validFor, operationsOnly, contextParameters);

				return aut;
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				storage.CloseConnection();
			}
		}

		/// <summary>
		/// /
		/// </summary>
		/// <param name="StoreName"></param>
		/// <param name="ApplicationName"></param>
		/// <param name="ItemName"></param>
		/// <param name="LDAPDomain"></param>
		/// <param name="User"></param>
		/// <param name="Sid"></param>
		/// <param name="ValidFor"></param>
		/// <param name="OperationsOnly"></param>
		/// <param name="attributes"></param>
		/// <param name="contextParameters"></param>
		/// <returns></returns>
		public bool CheckAccessLDAPEx(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out List<KeyValuePair<string, string>> attributes, out AuthorizationType authorization, out string statusType, out string status, out string stackTrace, params KeyValuePair<string, object>[] contextParameters) {
			SqlAzManStorage storage = null;

			authorization = AuthorizationType.Neutral;
			attributes = new List<KeyValuePair<string, string>>();
			statusType = null;
			status = null;
			stackTrace = null;
			try {
				storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);
				storage.OpenConnection();

				SqlAzManDBUser _azManUser;
				bool _result = storage.GetLDAPUser(LDAPDomain, User, out _azManUser, out statusType, out status, out stackTrace);
				if (!_result)
					return false;

				authorization = storage.CheckAccessLDAP(StoreName, ApplicationName, ItemName, LDAPDomain, User, _azManUser.CustomSid.StringValue, ValidFor, OperationsOnly, out attributes, contextParameters);

				return true;
			}
			catch (Exception ex) {
				statusType = ex.GetType().Name;
				status = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
			finally {
				storage.CloseConnection();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="StoreName"></param>
		/// <param name="ApplicationName"></param>
		/// <param name="ItemName"></param>
		/// <param name="LDAPDomain"></param>
		/// <param name="User"></param>
		/// <param name="Sid"></param>
		/// <param name="ValidFor"></param>
		/// <param name="OperationsOnly"></param>
		/// <param name="contextParameters"></param>
		/// <returns></returns>
		public bool CheckAccessLDAP(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out AuthorizationType authorization, out string statusType, out string status, out string stackTrace, params KeyValuePair<string, object>[] contextParameters) {
			List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
			return CheckAccessLDAPEx(StoreName, ApplicationName, ItemName, LDAPDomain, User, null, ValidFor, OperationsOnly, out attributes, out authorization, out statusType, out status, out stackTrace, contextParameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="azManUser"></param>
		/// <param name="statusType"></param>
		/// <param name="status"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public bool ValidatePassword(string ldapDomain, string userName, string password, out SqlAzManDBUser azManUser, out string statusType, out string status, out string stackTrace) {
			SqlAzManStorage _storage = null;

			azManUser = null;
			statusType = null;
			status = null;
			stackTrace = null;
			try {
				_storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);
				bool _result;
				if (!string.IsNullOrEmpty(ldapDomain)) {
					_result = _storage.GetLDAPUserByValidatingPassword(ldapDomain, userName, password, out azManUser, out statusType, out status, out stackTrace);

				}
				else {
					_result = _storage.GetDBUserByValidatingPassword(userName, password, out azManUser, out statusType, out status, out stackTrace);
				}

				if (!_result)
					return false;

				return true;
			}
			catch (Exception ex) {
				statusType = ex.GetType().Name;
				status = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
		}

		/// <summary>
		/// Implementacion del metodo
		/// </summary>
		/// <param name="user">=</param>
		/// <param name="current">=</param>
		/// <param name="renewed">=</param>
		/// <param name="confirmation">=</param>
		/// <param name="statusMessage">=</param>
		/// <returns></returns>
		public bool ChangePassword(DBUser user, string current, string renewed, string confirmation, out string statusMessage) {
			string _t;
			string _m;
			string _st;

			var _result = ChangePasswordEx(user, current, renewed, confirmation, out _t, out _m, out _st);

			statusMessage = string.Format("{0} Type:{1}\n\r{2}", _m, _t, _st);

			return _result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="current"></param>
		/// <param name="renewed"></param>
		/// <param name="confirmation"></param>
		/// <param name="statusType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="statusTrace"></param>
		/// <returns></returns>
		public bool ChangeDBUserPassword(DBUser user, string current, string renewed, string confirmation, out string statusType, out string statusMessage, out string statusTrace) {
			IAzManStorage _storage = null;
			statusType = null;
			statusMessage = null;
			statusTrace = null;
			try {
				_storage = new SqlAzManStorage(ConfigurationManager.ConnectionStrings["NetSqlAzManStorageCacheConnectionString"].ConnectionString);

				var _result = _storage.ChangeDBUserPassword(user, current, renewed, confirmation, out statusType, out statusMessage, out statusTrace);

				return _result;
			}
			catch (Exception ex) {
				statusType = ex.GetType().FullName;
				statusMessage = ex.Message;
				statusTrace = ex.StackTrace;

				return false;
			}
		}
	}
}
