using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI
{
	internal static class Global
	{
		internal const string Config_AppSettings_AzManStructureMgtWebApi_BaseUri_Key = "AzManStructureMgtWebApi_BaseUri";
		internal const string Config_Appsettings_Crypto64Key_Key = "Crypto64Key";
		//internal const string Config_AppSettings_WebApp_ShowExceptionDetails_Key = "WebApp_ShowExceptionDetails";
		internal const string Config_AppSettings_Notifications_ShowExceptionStackTrace_Key = "Notifications_ShowExceptionStackTrace";		

		internal static string Get_Config_Appsettings_Crypto64Key() {
			return ConfigurationManager.AppSettings[Global.Config_Appsettings_Crypto64Key_Key];
		}

		//internal static bool Get_Config_AppSettings_WebApp_ShowExceptionDetails() {
		//	return Convert.ToBoolean(ConfigurationManager.AppSettings[Config_AppSettings_WebApp_ShowExceptionDetails_Key]);
		//}

		internal static bool Get_Config_AppSettings_Notifications_ShowExceptionStackTrace() {
			return Convert.ToBoolean(ConfigurationManager.AppSettings[Config_AppSettings_Notifications_ShowExceptionStackTrace_Key]);
		}
		internal static string Get_Config_AppSettings_AzManStructureMgtWebApi_BaseUri() {
			return ConfigurationManager.AppSettings[Global.Config_AppSettings_AzManStructureMgtWebApi_BaseUri_Key];
		}

//internal static Models.UncaugthApplicationException CreateUncaugthApplicationException(Exception ex) {
		//	var _exception = new Models.UncaugthApplicationException() {
		//		Message = ex.Message,
		//		ExceptionType = ex.GetType().FullName,
		//		StackTrace = ex.StackTrace,
		//		Source = ex.Source
		//	};

		//	var _flag = ex.InnerException;
		//	while (_flag != null) {
		//		_exception.InnerException = new Models.UncaugthApplicationException() {
		//			Message = _flag.Message,
		//			ExceptionType = _flag.GetType().FullName,
		//			StackTrace = _flag.StackTrace,
		//			Source = _flag.Source
		//		};
		//		_flag = _flag.InnerException;
		//	}

		//	return _exception;
		//}
	}
}
