using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AzManStructureMgtWebApi {
	public class Global {
		///Config references
		internal const string CONFIG_APPSETTINGS_WebApp_Name_Key = "WebApp_Name";
		internal const string CONFIG_APPSETTINGS_AzMan_CustomBusinessLogic_DbConnection_Key = "AzMan_CustomBusinessLogic_DbConnection";

		///Helper Constants
		internal const string RESPONSE_HEADER_WebApiMessage = "WebApiMsg_Info";
		internal const string RESPONSE_HEADER_RecordCount = "WebApiMsg_RecordCount";

		public static string Get_CONFIG_APPSETTINGS_WebApp_Name() {
			return ConfigurationManager.AppSettings[CONFIG_APPSETTINGS_WebApp_Name_Key];
		}

		internal static string Get_CONFIG_APPSETTINGS_AzMan_CustomBusinessLogic_DbConnection() {
			return ConfigurationManager.AppSettings[Global.CONFIG_APPSETTINGS_AzMan_CustomBusinessLogic_DbConnection_Key];
		}
	}
}