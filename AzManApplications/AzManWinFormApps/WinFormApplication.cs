using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManWinFormApps
{
	public static class WinFormApplication
	{
		public const string CFG_AzMan_Store = "CFG_AzMan_Store";
		public const string CFG_AzMan_Application = "CFG_AzMan_App";
		public const string CFG_LoginSvc_EndPointName = "CFG_LoginSvc_EndPointName";
		public const string CFG_LoginSvc_FullWebPath = "CFG_LoginSvc_FullWebPath";

		public static string ConfigValue_AzManStore {
			get {
				return ConfigurationManager.AppSettings[CFG_AzMan_Store];
			}
		}

		public static string ConfigValue_AzManApplication {
			get {
				return ConfigurationManager.AppSettings[CFG_AzMan_Application];
			}
		}

		public static LoginInfo GetLoginInfoFromSession(WinFormSession session) {
			return session.LoginInfo;
		}

		public static DBUser GetUserInfoFromSession(WinFormSession session) {
			return session.DBUser;
		}

		public static string GetCurrentLocalFromSession(WinFormSession session) {
			return session.BranchId;
		}

		public static string GetCurrentLocalNameFromSession(WinFormSession session) {
			return session.BranchName;
		}

		public static void SaveLoginInfoIntoSession(LoginInfo logininfo, DBUser user, WinFormSession session) {
			session.LoginInfo = logininfo;
			session.DBUser = user;
		}

		public static void SaveLocalNameIntoSession(string branchId, string branchName, WinFormSession session) {
			session.BranchId = branchId;
			session.BranchName = branchName;
		}

		public static bool IsNewSessionOrEmptySession(WinFormSession session) {
			return session.LoginInfo == null || session.DBUser == null;
		}
	}
}
