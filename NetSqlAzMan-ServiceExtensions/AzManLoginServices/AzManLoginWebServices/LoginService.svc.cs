using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.InteropServices;

namespace AzManLoginWebServices
{
	/// <summary>
	/// Servicio deLogin para el usuario
	/// </summary>
	public class LoginService : ILoginService
	{
		internal const string CONNECTIONSTRING_Name = "AzManLoginDatabase";

		#region Private methods

		private bool azMan_GetDBUser(string userName, out AzManLoginWebServices.DirectSvcRef.DBUser userInfo, out Exception hex) {
			userInfo = null;
			hex = null;

			try {
				AzManLoginWebServices.DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

				userInfo = _client.DirectGetDBUser(userName);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private bool azMan_ValidateDBUser(string userName, string password, out AzManLoginWebServices.DirectSvcRef.DBUser userInfo, out bool pwdIsValid, out Exception hex) {
			userInfo = null;
			pwdIsValid = false;
			hex = null;

			try {
				AzManLoginWebServices.DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

				userInfo = _client.DirectGetDBUser(userName);
				pwdIsValid = _client.DirectValidatePassword(userName, password);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private bool azMan_ValidateDBUser(string encodeUserName, out AzManLoginWebServices.DirectSvcRef.DBUser userInfo, out Exception hex) {
			userInfo = null;
			hex = null;

			try {
				AzManLoginWebServices.DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

				userInfo = _client.DirectGetDBUser(encodeUserName);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private bool azMan_DirectCheckAccess(string store, string app, string item, string userName, out DirectSvcRef.AuthorizationType aut, out Exception hex) {
			aut = DirectSvcRef.AuthorizationType.Neutral;
			hex = null;

			try {
				AzManLoginWebServices.DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

				aut = _client.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve(store, app, item, userName, DateTime.Now, false, null);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private void azMan_Util_GetPropertyValueFromAttString(string attString, string propName, out string propValue) {
			propValue = null;
			string[] propArray = attString.Split(Convert.ToChar(124));
			foreach (string pair in propArray) {
				string[] pairArray = pair.Split(Convert.ToChar(61));
				if (pairArray[0].ToLower().Equals(propName.ToLower())) {
					propValue = pairArray[1];
					return;
				}
			}
		}

		private bool getPropertiesFromIncomingMessage(OperationContext ctxt, out string address, out int port, out Exception hex) {
			address = null;
			port = -1;
			hex = null;

			try {
				System.ServiceModel.Channels.RemoteEndpointMessageProperty _msgProperty = ctxt.IncomingMessageProperties[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name] as System.ServiceModel.Channels.RemoteEndpointMessageProperty;
				address = _msgProperty.Address;
				port = _msgProperty.Port;

				return true;
			}
			catch (Exception ex) {
				hex = null;
				return false;
			}
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Metodo de prueba del servicio
		/// </summary>
		/// <param name="input">Valor recibido</param>
		/// <param name="output">Valor enviado</param>
		/// <returns></returns>
		public bool Test(string input, out string output) {
			output = string.Format("Ud. nos envió lo siguiente: {0}", input);
			return true;
		}

		[Obsolete("Ahora se debe de usar el método CreateLogin.")]
		public bool StartLogin(string userName, string pwd, string appName, out DirectSvcRef.DBUser user, string store, string app, string requiredItem, out DirectSvcRef.AuthorizationType aut, out LoginInfo login, out string attributeString) {
			Exception exceHandled = null;

			aut = DirectSvcRef.AuthorizationType.Neutral;
			login = null;
			user = null;
			attributeString = null;

			try {
				bool _pwdIsValid = false;
				//Validar el usuario y su contraseña
				if (!this.azMan_ValidateDBUser(userName, pwd, out user, out _pwdIsValid, out exceHandled))
					throw exceHandled;

				//La contraseña no es valida
				if (!_pwdIsValid)
					throw new Exception(string.Format("Usuario y/o contraseña incorrectos.", pwd.ToString()));

				//No se pudo verificar el acceso
				if (!this.azMan_DirectCheckAccess(store, app, requiredItem, userName, out aut, out exceHandled)) {
					throw exceHandled;
				}

				//Verificar la autorizacion
				if (aut == DirectSvcRef.AuthorizationType.Allow || aut == DirectSvcRef.AuthorizationType.AllowWithDelegation) {
					string _remoteAddress;
					int _remotePort;

					//Obtener el ip y puerto del cliente que realiza la llamada
					if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out exceHandled))
						throw exceHandled;

					//Registrar el login del usuario
					BusinessLayer.Login _loginBO = new BusinessLayer.Login(null, userName, appName, _remoteAddress);
					BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
					fact.Insert(_loginBO);

					//Devolver la informacion de login del usuario
					login = new LoginInfo(new Guid(_loginBO.LoginId));
					login.UserName = _loginBO.UserName;
					login.AppName = _loginBO.AppName;
					login.TimeOut = _loginBO.Timeout;
					login.Status = LoginStatusEnum.LoggedIn;
				}

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		public bool StartLoginEnc(string encodedUserName, string appName, out DirectSvcRef.DBUser user, string store, string app, string requiredItem, out DirectSvcRef.AuthorizationType aut, out LoginInfo login, out string attributeString) {
			Exception exceHandled = null;

			aut = DirectSvcRef.AuthorizationType.Neutral;
			login = null;
			user = null;
			attributeString = null;

			try {
				//Validar el usuario y su contraseña
				if (!this.azMan_ValidateDBUser(encodedUserName, out user, out exceHandled))
					throw exceHandled;

				//No se pudo verificar el acceso
				if (!this.azMan_DirectCheckAccess(store, app, requiredItem, user.UserName, out aut, out exceHandled)) {
					throw exceHandled;
				}

				//Verificar la autorizacion
				if (aut == DirectSvcRef.AuthorizationType.Allow || aut == DirectSvcRef.AuthorizationType.AllowWithDelegation) {
					string _remoteAddress;
					int _remotePort;

					//Obtener el ip y puerto del cliente que realiza la llamada
					if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out exceHandled))
						throw exceHandled;

					//Registrar el login del usuario
					BusinessLayer.Login _loginBO = new BusinessLayer.Login(null, encodedUserName, appName, _remoteAddress);
					BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
					fact.Insert(_loginBO);

					//Devolver la informacion de login del usuario
					login = new LoginInfo(new Guid(_loginBO.LoginId));
					login.UserName = _loginBO.UserName;
					login.AppName = _loginBO.AppName;
					login.TimeOut = _loginBO.Timeout;
					login.Status = LoginStatusEnum.LoggedIn;
				}

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		public bool CreateLogin(string userName, string password, string clientApplication, string azManStore, string azManApplication, string azManItem, out DirectSvcRef.DBUser dbUser, out LoginInfo loginInfo, out DirectSvcRef.AuthorizationType authorizationType, out string outputString) {
			Exception _exce = null;

			authorizationType = DirectSvcRef.AuthorizationType.Neutral;
			loginInfo = null;
			dbUser = null;
			outputString = null;

			try {
				bool _pwdIsValid = false;
				//Validar el usuario y su contraseña
				if (!this.azMan_ValidateDBUser(userName, password, out dbUser, out _pwdIsValid, out _exce))
					throw _exce;

				//La contraseña no es valida
				if (!_pwdIsValid)
					throw new Exception(string.Format("Usuario y/o contraseña incorrectos.", password.ToString()));

				//No se pudo verificar el acceso
				if (!this.azMan_DirectCheckAccess(azManStore, azManApplication, azManItem, userName, out authorizationType, out _exce)) {
					throw _exce;
				}

				//Verificar la autorizacion
				if (authorizationType == DirectSvcRef.AuthorizationType.Allow || authorizationType == DirectSvcRef.AuthorizationType.AllowWithDelegation) {
					string _remoteAddress;
					int _remotePort;

					//Obtener el ip y puerto del cliente que realiza la llamada
					if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out _exce))
						throw _exce;

					//Registrar el login del usuario
					BusinessLayer.Login _loginBO = new BusinessLayer.Login(null, userName, clientApplication, _remoteAddress);
					BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
					fact.Insert(_loginBO);

					//Devolver la informacion de login del usuario
					loginInfo = new LoginInfo(new Guid(_loginBO.LoginId)) {
						UserName = _loginBO.UserName,
						AppName = _loginBO.AppName,
						TimeOut = _loginBO.Timeout,
						Status = LoginStatusEnum.LoggedIn
					};
				}

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				outputString = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// Para crear un login mediante LDAP; esta en implementación.
		/// </summary>
		/// <param name="domain"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="clientApplication"></param>
		/// <param name="azManStore"></param>
		/// <param name="azManApplication"></param>
		/// <param name="azManItem"></param>
		/// <param name="azManUser"></param>
		/// <param name="loginInfo"></param>
		/// <param name="authorizationType"></param>
		/// <param name="statusType"></param>
		/// <param name="status"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		//public bool CreateLoginEx(string domain, string userName, string password, string clientApplication, string azManStore, string azManApplication, string azManItem, out DirectSvcRef.SqlAzManDBUser azManUser, out LoginInfo loginInfo, out DirectSvcRef.AuthorizationType authorizationType, out string statusType, out string status, out string stackTrace) {
		//	Exception _exce = null;

		//	azManUser = null;
		//	loginInfo = null;
		//	authorizationType = DirectSvcRef.AuthorizationType.Neutral;
		//	statusType = null;
		//	status = null;
		//	stackTrace = null;
		//	try {
		//		if (!string.IsNullOrEmpty(domain)) { //Es un usuario de algún dominio
		//			//Validar usuario y password. Obtiene usuario si es correcto.
		//			DirectSvcRef.DirectServiceClient _c = new DirectSvcRef.DirectServiceClient();
		//			bool _result = _c.ValidateUserPassword(out azManUser, out statusType, out status, out stackTrace, domain, userName, password);
		//			//El usuario y/o contraseña son incorrectos.
		//			if (!_result) {
		//				status = string.Format("{0}\n\r{1}", "Usuario y/o contraseña incorrectos.", status);
		//				return false;
		//			}

		//			//Validar el acceso a la objeto asegurable.
		//			_result = _c.CheckAccessLDAP(out authorizationType, out statusType, out status, out stackTrace, azManStore, azManApplication, azManItem, domain, userName, null, DateTime.MinValue, false, null);
		//			//Error al verificar acceso
		//			if (!_result) {
		//				status = string.Format("Error al verificar el acceso al item {0}.\n\r{1}", azManItem, status);
		//				return false;
		//			}
		//		}
		//		else { //Es un usuario almacenado en bd.
		//			bool _pwdIsValid = false;
		//			//Validar el usuario y su contraseña
		//			if (!this.azMan_ValidateDBUser(userName, password, out dbUser, out _pwdIsValid, out _exce))
		//				throw _exce;

		//			//La contraseña no es valida
		//			if (!_pwdIsValid)
		//				throw new Exception("Usuario y/o contraseña incorrectos.");

		//			//No se pudo verificar el acceso
		//			if (!this.azMan_DirectCheckAccess(azManStore, azManApplication, azManItem, userName, out authorizationType, out _exce)) {
		//				throw _exce;
		//			}
		//		}

		//		//Verificar la autorizacion
		//		if (authorizationType == DirectSvcRef.AuthorizationType.Allow || authorizationType == DirectSvcRef.AuthorizationType.AllowWithDelegation) {
		//			string _remoteAddress;
		//			int _remotePort;

		//			//Obtener el ip y puerto del cliente que realiza la llamada
		//			if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out _exce))
		//				throw _exce;

		//			//Registrar el login del usuario
		//			BusinessLayer.Login _loginBO = new BusinessLayer.Login(domain, userName, clientApplication, _remoteAddress);
		//			BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
		//			fact.Insert(_loginBO);

		//			//Devolver la informacion de login del usuario
		//			loginInfo = new LoginInfo(new Guid(_loginBO.LoginId)) {
		//				LDAPDomain = _loginBO.LDAPDomain,
		//				UserName = _loginBO.UserName,
		//				AppName = _loginBO.AppName,
		//				TimeOut = _loginBO.Timeout,
		//				Status = LoginStatusEnum.LoggedIn
		//			};
		//		}

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		statusType = ex.GetType().Name;
		//		status = ex.Message;
		//		stackTrace = ex.StackTrace;

		//		return false;
		//	}
		//}

		[Obsolete("Ya no se debe usar.El parametro appName se eliminará en la implementación de un nuevo método.")]
		public bool GetLogin(string loginId, string userName, string appName, out DirectSvcRef.DBUser user, out LoginInfo login, out string attributeString) {
			Exception exceHandled = null;

			login = null;
			user = null;
			attributeString = null;

			try {
				string _remoteAddress;
				int _remotePort;

				//Obtener el ip y puerto del cliente que realiza la llamada
				if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out exceHandled))
					throw exceHandled;

				//Obtener el login del usuario
				BusinessLayer.Login _loginBO = null;
				BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
				_loginBO = fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(loginId));

				if (_loginBO == null)
					throw new Exception("No es posible ubicar el Login del usuario.");

				if (!_loginBO.UserName.Equals(userName))
					throw new Exception(string.Format("El login no corresponde al usuario {0}.", userName));

				//Devolver la informacion de login del usuario
				login = new LoginInfo(new Guid(_loginBO.LoginId));
				login.UserName = _loginBO.UserName;
				login.AppName = _loginBO.AppName;
				login.TimeOut = _loginBO.Timeout;
				if (_loginBO.Expired)
					login.Status = LoginStatusEnum.Expired;
				else if (_loginBO.LoggedOut)
					login.Status = LoginStatusEnum.LoggedOut;
				else
					login.Status = LoginStatusEnum.LoggedIn;

				//Obtener el usuario
				if (!this.azMan_GetDBUser(userName, out user, out exceHandled))
					throw exceHandled;

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		public bool GetLoginByIdAndUser(string loginId, string userName, out DirectSvcRef.DBUser dbUser, out LoginInfo loginInfo, out string outputString) {
			Exception exceHandled = null;

			loginInfo = null;
			dbUser = null;
			outputString = null;

			try {
				string _remoteAddress;
				int _remotePort;

				//Obtener el ip y puerto del cliente que realiza la llamada
				if (!this.getPropertiesFromIncomingMessage(OperationContext.Current, out _remoteAddress, out _remotePort, out exceHandled))
					throw exceHandled;

				//Obtener el login del usuario
				BusinessLayer.Login _loginBO = null;
				BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
				_loginBO = fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(loginId));

				if (_loginBO == null)
					throw new Exception("No es posible ubicar el Login del usuario.");

				if (!_loginBO.UserName.Equals(userName))
					throw new Exception(string.Format("El login no corresponde al usuario {0}.", userName));

				//Devolver la informacion de login del usuario
				loginInfo = new LoginInfo(new Guid(_loginBO.LoginId));
				loginInfo.UserName = _loginBO.UserName;
				loginInfo.AppName = _loginBO.AppName;
				loginInfo.TimeOut = _loginBO.Timeout;
				if (_loginBO.Expired)
					loginInfo.Status = LoginStatusEnum.Expired;
				else if (_loginBO.LoggedOut)
					loginInfo.Status = LoginStatusEnum.LoggedOut;
				else
					loginInfo.Status = LoginStatusEnum.LoggedIn;

				//Obtener el usuario
				if (!this.azMan_GetDBUser(userName, out dbUser, out exceHandled))
					throw exceHandled;

				return true;
			}
			catch (Exception ex) {
				outputString = ex.Message;
				return false;
			}
		}

		public bool RevalidateLogin(string loginId, string userName, string pwd, string appName, out DirectSvcRef.DBUser user, out LoginInfo login, out string attributeString) {
			Exception exceHandled = null;

			login = null;
			user = null;
			attributeString = null;

			try {
				//Validar el login del usuario
				BusinessLayer.Login _loginBO = null;
				BusinessLayer.LoginFactory fact = new BusinessLayer.LoginFactory();
				_loginBO = fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(loginId));

				if (_loginBO == null)
					throw new Exception("No es posible ubicar el Login del usuario.");

				if (!_loginBO.UserName.Equals(userName))
					throw new Exception(string.Format("El login no corresponde al usuario {0}.", userName));

				//Validar el usuario y su contraseña
				bool _pwdIsValid = false;
				if (!this.azMan_ValidateDBUser(userName, pwd, out user, out _pwdIsValid, out exceHandled))
					throw exceHandled;

				if (!_pwdIsValid)
					throw new Exception(string.Format("Usuario y/o contraseña incorrectos.", pwd.ToString()));

				//Renovar tiempos del Login
				DateTime _now = DateTime.Now;
				_loginBO.Renovated = true;
				_loginBO.Renewal = _now;
				_loginBO.Expired = false;
				_loginBO.Expires = _now.AddSeconds(_loginBO.Timeout);
				fact.Update(_loginBO);

				//Devolver la informacion de login del usuario
				login = new LoginInfo(new Guid(_loginBO.LoginId));
				login.UserName = _loginBO.UserName;
				login.AppName = _loginBO.AppName;
				login.TimeOut = _loginBO.Timeout;
				login.Status = LoginStatusEnum.LoggedIn;

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		public bool StartLogOut(LoginInfo login, out string attributeString) {
			attributeString = null;

			try {
				BusinessLayer.LoginFactory _fact = new BusinessLayer.LoginFactory();
				BusinessLayer.Login _loginBO = _fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(login.LoginId));

				//No se encontró el login
				if (_loginBO == null)
					throw new Exception("No se pudo obtener información del Login debido a que no existe o fue modificado por otro proceso.");

				////El login ya habia expirado
				//if (_loginBO.Expired)
				//   throw new Exception("El login ya ha expirado. No se puede realizar la operación.");

				//El login fue cerrado
				if (_loginBO.LoggedOut)
					throw new Exception("Su login ya había terminado. No se puede realizar la operación.");

				//Cerrar el login
				_loginBO.LogOff = DateTime.Now;
				_loginBO.LoggedOut = true;
				_fact.Update(_loginBO);

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		[Obsolete("Ya no se debe usar este método. en su lugar use CheckLoginStatusAndAuthorization")]
		public bool CheckLoginAccess(string store, string app, string item, LoginInfo loginInfo, out LoginStatusEnum loginStatus, out DirectSvcRef.AuthorizationType aut, out string attributeString) {
			loginStatus = LoginStatusEnum.Unknown;
			aut = DirectSvcRef.AuthorizationType.Neutral;
			attributeString = null;

			try {
				BusinessLayer.LoginFactory _fact = new BusinessLayer.LoginFactory();
				BusinessLayer.Login _loginBO = _fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(loginInfo.LoginId));

				//No se encontró el login
				if (_loginBO == null)
					throw new Exception("No se pudo obtener información del Login debido a que no existe o fue modificado por otro proceso.");

				//El login esta en linea
				if (!_loginBO.LoggedOut) {
					//El login no ha expirado
					if (!_loginBO.Expired) {
						//El login DEBE expirar
						if (DateTime.Now > _loginBO.Expires) {
							_loginBO.Expiration = DateTime.Now;
							_loginBO.Expired = true;
							_fact.Update(_loginBO);
							loginStatus = LoginStatusEnum.Expired;
						}
						else { //El login NO DEBE expirar
							_loginBO.Expires = DateTime.Now.AddSeconds(_loginBO.Timeout);
							_loginBO.Expired = false;
							_fact.Update(_loginBO);
							loginStatus = LoginStatusEnum.LoggedIn;

							DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

							aut = _client.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve(store, app, item, loginInfo.UserName, DateTime.Now, false, null);
						}
					}
					else { //El login EXPIRÓ
						loginStatus = LoginStatusEnum.Expired;
					}
				}
				else { //El login NO esta en linea
					loginStatus = LoginStatusEnum.LoggedOut;
				}

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				attributeString = ex.Message;
				return false;
			}
		}

		public bool CheckLoginStatusAndAuthorization(string azManStore, string azManApplication, string azManItem, LoginInfo loginInfo, out LoginStatusEnum loginStatus, out DirectSvcRef.AuthorizationType authorizationType, out string outputString) {
			loginStatus = LoginStatusEnum.Unknown;
			authorizationType = DirectSvcRef.AuthorizationType.Neutral;
			outputString = null;

			try {
				BusinessLayer.LoginFactory _fact = new BusinessLayer.LoginFactory();
				BusinessLayer.Login _loginBO = _fact.GetByPrimaryKey(new BusinessLayer.LoginKeys(loginInfo.LoginId));

				//No se encontró el login
				if (_loginBO == null)
					throw new Exception("No se pudo encontrar información del Login debido a que no existe o fue modificado por otro proceso.");

				//El login esta en linea
				if (!_loginBO.LoggedOut) {
					//El login no ha expirado
					if (!_loginBO.Expired) {
						//El login DEBE expirar
						if (DateTime.Now > _loginBO.Expires) {
							_loginBO.Expiration = DateTime.Now;
							_loginBO.Expired = true;
							_fact.Update(_loginBO);
							loginStatus = LoginStatusEnum.Expired;
						}
						else { //El login NO DEBE expirar
							_loginBO.Expires = DateTime.Now.AddSeconds(_loginBO.Timeout);
							_loginBO.Expired = false;
							_fact.Update(_loginBO);
							loginStatus = LoginStatusEnum.LoggedIn;

							DirectSvcRef.DirectServiceClient _client = new DirectSvcRef.DirectServiceClient();

							authorizationType = _client.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve(azManStore, azManApplication, azManItem, loginInfo.UserName, DateTime.Now, false, null);
						}
					}
					else { //El login EXPIRÓ
						loginStatus = LoginStatusEnum.Expired;
					}
				}
				else { //El login NO esta en linea
					loginStatus = LoginStatusEnum.LoggedOut;
				}

				return true;
			}
			catch (Exception ex) {
				//attributeString = string.Concat(ex.Message, Environment.NewLine, ex.StackTrace);
				outputString = ex.Message;
				return false;
			}
		}

		public bool ChangePwd(LoginInfo login, string current, string renewed, string confirmation, out string statusMessages) {
			string _t;
			string _m;
			string _st;

			var _result = ChangePwdEx(login, current, renewed, confirmation, out _t, out _m, out _st);

			statusMessages = string.Format("{0} Type: {1}\n\r{2}", _m, _t, _st);

			return _result;
		}

		public bool ChangePwdEx(LoginInfo login, string current, string renewed, string confirmation, out string statusType, out string statusMessage, out string statusTrace) {
			statusType = null;
			statusMessage = null;
			statusTrace = null;

			try {
				DirectSvcRef.DBUser _user;
				Exception _exce;
				if (!azMan_GetDBUser(login.UserName, out _user, out _exce))
					throw new Exception(string.Format("{0}\n\r{1}\n\r{2}", "Error al intentar obtener el usuario.", _exce.Message, _exce.StackTrace));

				DirectSvcRef.DirectServiceClient _svc = new DirectSvcRef.DirectServiceClient();
				var _result = _svc.ChangePwdEx(out statusType, out statusMessage, out statusTrace, _user, current, renewed, confirmation);

				return _result;
			}
			catch (Exception ex) {
				statusType = ex.GetType().FullName;
				statusMessage = ex.Message;
				statusTrace = ex.StackTrace;

				return false;
			}
		}

		#endregion
	}
}