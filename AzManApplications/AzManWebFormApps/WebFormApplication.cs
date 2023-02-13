using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace AzManWebFormApps
{
	public static class WebFormApplication
	{
		#region Constantes
		public const string CFG_Title = "CFG_Title";
		public const string CFG_FullWebPath = "CFG_FullWebPath";
		public const string CFG_TimeOutPageRelativePath = "CFG_TimeOutPageRelativePath";
		public const string CFG_TicketVersion = "CFG_TicketVersion";
		public const string CFG_AllowedOfficeIds = "CFG_AllowedOfficeIds";
		/*********************************************************************/
		public const string CFG_AzMan_Store = "CFG_AzMan_Store";
		public const string CFG_AzMan_App = "CFG_AzMan_App";
		/*********************************************************************/
		public const string CFG_LoginSvc_EndPointName = "CFG_LoginSvc_EndPointName";
		public const string CFG_LoginSvc_FullWebPath = "CFG_LoginSvc_FullWebPath";
		/**********************************************************************/
		public const string CFG_Login_ServerUrl = "CFG_Login_ServerUrl";
		public const string CFG_Login_SubDomain = "CFG_Login_SubDomain";
		public const string CFG_Login_AppFolder = "CFG_Login_AppFolder";
		public const string CFG_Login_AccountPageFullWebPath = "CFG_Login_AccountPageFullWebPath";
		/***********************************************************************/
		public const string CFG_Intranet_ServerUrl = "CFG_Intranet_ServerUrl";
		public const string CFG_Intranet_SubDomain = "CFG_Intranet_SubDomain";
		public const string CFG_Intranet_AppFolder = "CFG_Intranet_AppFolder";
		/************************************************************************/
		public const string CFG_Campus_ServerUrl = "CFG_Campus_ServerUrl";
		public const string CFG_Campus_SubDomain = "CFG_Campus_SubDomain";
		public const string CFG_Campus_AppFolder = "CFG_Campus_AppFolder";
		public const string CFG_Campus_ValidatorPageRelativePath = "CFG_Campus_ValidatorPageRelativePath";
		/*************************************************************************/
		public const string CFG_Tramites_ServerUrl = "CFG_Tramites_ServerUrl";
		public const string CFG_Tramites_SubDomain = "CFG_Tramites_SubDomain";
		public const string CFG_Tramites_AppFolder = "CFG_Tramites_AppFolder";
		public const string CFG_Tramites_ValidatorPageRelativePath = "CFG_Tramites_ValidatorPageRelativePath";
		/*************************************************************************/
		public const string SESVAR_Login = "Sess_Login";
		public const string SESVAR_User = "Sess_User";
		public const string SESVAR_OfficeId = "Sess_OfficeId";
		public const string SESVAR_OfficeName = "Sess_OfficeName";
		public const string SESVAR_FullName = "Sess_FullName";
		public const string SESVAR_DepartmentId = "Sess_DepartmentId";
		#endregion

		#region Enumerados
		public enum WebAppId
		{
			TRAM,
			BOLSA,
			INCIDENCIAS,
			CAMPUSADM
		}
		#endregion

		#region Properties
		public static string AzManStore {
			get {
				return ConfigurationManager.AppSettings[WebFormApplication.CFG_AzMan_Store];
			}
		}

		public static string AzManApplication {
			get {
				return ConfigurationManager.AppSettings[WebFormApplication.CFG_AzMan_App];
			}
		}
		#endregion

		public static bool IsNewSessionOrEmptySession(Page webPage) {
			if (webPage.Session.IsNewSession)
				return true;

			try {
				if (webPage.Session[WebFormApplication.SESVAR_Login] == null)
					return true;
				else
					return false;
			}
			catch {
				return true;
			}
		}

		public static void SaveVarIntoSession(Page webPage, string varName, object varValue) {
			webPage.Session.Remove(varName);
			webPage.Session.Add(varName, varValue);
		}

		public static void SaveLoginInfoIntoSession(Page webPage, AzManLoginHelper.AzManLoginSvcRef.LoginInfo login, bool remove) {
			if (remove)
				webPage.Session.Remove(WebFormApplication.SESVAR_Login);

			webPage.Session.Add(WebFormApplication.SESVAR_Login, login);
		}

		public static void SaveLoginInfoIntoSession(Page webPage, AzManLoginHelper.AzManLoginSvcRef.DBUser user, AzManLoginHelper.AzManLoginSvcRef.LoginInfo login, bool remove) {
			if (remove) {
				webPage.Session.Remove(WebFormApplication.SESVAR_Login);
				webPage.Session.Remove(WebFormApplication.SESVAR_User);
			}

			webPage.Session.Add(WebFormApplication.SESVAR_Login, login);
			webPage.Session.Add(WebFormApplication.SESVAR_User, user);
		}

		public static void SignOutAndGoToLocalLogin(Page webPage, bool signOut, bool abandonSession, bool goToLoginPage, string extraQueryString) {
			if (signOut)
				//Eliminar cookie de autenticacion
				System.Web.Security.FormsAuthentication.SignOut();

			if (abandonSession)
				//Abandonar sesion ASP .NET
				webPage.Session.Abandon();

			if (goToLoginPage)
				//Redireccionar a la pagina de login configurada
				System.Web.Security.FormsAuthentication.RedirectToLoginPage(extraQueryString);
		}

		public static void SignOutAndGoToGlobalLogin(Page webPage, bool signOut, bool abandonSession, bool goToLoginPage, string extraQueryString) {
			//Eliminar cookie de autenticacion
			if (signOut)
				System.Web.Security.FormsAuthentication.SignOut();

			//Abandonar sesion ASP .NET
			if (abandonSession)
				webPage.Session.Abandon();

			//Redireccionar a la pagina de login configurada
			if (goToLoginPage) {
				if (string.IsNullOrEmpty(extraQueryString))
					webPage.Response.Redirect(ConfigurationManager.AppSettings[WebFormApplication.CFG_Login_AccountPageFullWebPath]);
				else
					webPage.Response.Redirect(string.Format("{0}?{1}", ConfigurationManager.AppSettings[WebFormApplication.CFG_Login_AccountPageFullWebPath], extraQueryString));
			}
		}

		public static AzManLoginHelper.AzManLoginSvcRef.LoginInfo GetLoginInfoFromSession(Page webPage) {
			//Obtener el Login de la sesion
			AzManLoginHelper.AzManLoginSvcRef.LoginInfo _login = (AzManLoginHelper.AzManLoginSvcRef.LoginInfo)webPage.Session[WebFormApplication.SESVAR_Login];

			return _login;
		}

		public static AzManLoginHelper.AzManLoginSvcRef.DBUser GetUserFromSession(Page webPage) {
			//Obtener el Login de la sesion
			AzManLoginHelper.AzManLoginSvcRef.DBUser _login = (AzManLoginHelper.AzManLoginSvcRef.DBUser)webPage.Session[WebFormApplication.SESVAR_User];

			return _login;
		}

		public static string GetTokenStringFromLoginInfo(AzManLoginHelper.AzManLoginSvcRef.LoginInfo login) {
			return string.Concat(login.UserName, Convert.ToChar(179).ToString(), login.LoginId);
		}

		public static FormsAuthenticationTicket CreateFormsAuthenticationTicket(string token, string userData, bool persistent) {
			return new FormsAuthenticationTicket(Convert.ToInt32(ConfigurationManager.AppSettings[WebFormApplication.CFG_TicketVersion]), token, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), persistent, userData, FormsAuthentication.FormsCookiePath);
		}

		public static void RedirectToTimeOutPage(Page webPage, bool endResponse) {
			webPage.Response.Redirect(string.Concat(ConfigurationManager.AppSettings[WebFormApplication.CFG_FullWebPath], ConfigurationManager.AppSettings[WebFormApplication.CFG_TimeOutPageRelativePath]), endResponse);
		}

		public static void WriteTimeOutAlertScript(Page webPage, bool closeWindow) {
			webPage.ClientScript.RegisterStartupScript(webPage.GetType(), "timeOutAlert", "alert('La sesión ha caducado. Vuelva a ingresar para continuar.');", true);
			if (closeWindow)
				webPage.ClientScript.RegisterStartupScript(webPage.GetType(), "closeWindow", "window.close();", true);
		}

		public static void GetTokenFromAuthenticationTicket(Page webPage, out string token) {
			token = null;

			HttpCookie _cookie = webPage.Request.Cookies.Get(System.Web.Security.FormsAuthentication.FormsCookieName);

			System.Web.Security.FormsAuthenticationTicket _ticket = System.Web.Security.FormsAuthentication.Decrypt(_cookie.Value);

			token = _ticket.Name;
		}

		public static string GetQueryString(Page webPage, string[] excludedKeys) {
			string _query = null;

			foreach (string _k in webPage.Request.QueryString.AllKeys) {
				if (!excludedKeys.Contains(_k))
					_query += string.Concat("&", _k, "=", webPage.Request.QueryString.Get(_k));
			}

			if (!string.IsNullOrEmpty(_query)) {
				_query = _query.Substring(1);
			}

			return _query;
		}

		public static string GetUrlAndQueryString(string url, string qstring) {
			if (string.IsNullOrEmpty(qstring))
				return url;
			else {
				if (url.Contains('?'))
					return string.Format("{0}&{1}", url, qstring);
				else
					return string.Format("{0}?{1}", url, qstring);
			}
		}
	}
}
