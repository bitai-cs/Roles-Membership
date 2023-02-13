using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.WebApiClientHelpers {
	internal static class Global {
		internal const string Config_Appsettings_Crypto64Key_Key = "Crypto64Key";
		//internal const string Config_AppSettings_WebApp_ShowExceptionDetails_Key = "WebApp_ShowExceptionDetails";
		internal const string Config_AppSettings_Notifications_ShowExceptionStackTrace_Key = "Notifications_ShowExceptionStackTrace";
		internal const string Config_AppSettings_WebApiClient_ConnectionTimeOut_Key = "WebApiClient_RequestTimeout";
		internal const string Config_AppSettings_WebApiClient_MaxResponseContentBufferSize_Key = "WebApiClient_MaxResponseContentBufferSize";


		internal const string MimeType_AppJson = "application/json";
		internal const string MimeType_TextHtml = "text/html";
		internal const string MimeType_NoContent = "";

		internal enum AcceptHeaderType {
			ApplicationJson
		}

		internal static string Get_Config_Appsettings_Crypto64Key() {
			return ConfigurationManager.AppSettings[Global.Config_Appsettings_Crypto64Key_Key];
		}

		//internal static bool Get_Config_AppSettings_WebApp_ShowExceptionDetails() {
		//	return Convert.ToBoolean(ConfigurationManager.AppSettings[Config_AppSettings_WebApp_ShowExceptionDetails_Key]);
		//}

		internal static bool Get_Config_AppSettings_Notifications_ShowExceptionStackTrace() {
			return Convert.ToBoolean(ConfigurationManager.AppSettings[Config_AppSettings_Notifications_ShowExceptionStackTrace_Key]);
		}
		
		internal static int Get_Config_AppSettings_WebApiClient_RequestTimeout() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Global.Config_AppSettings_WebApiClient_ConnectionTimeOut_Key]);
		}

		internal static int Get_Config_AppSettings_WebApiClient_MaxResponseContentBufferSize() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Global.Config_AppSettings_WebApiClient_MaxResponseContentBufferSize_Key]);
		}


		internal static HttpClient GetHttpClient(string webApiUri) {
			return GetHttpClient(webApiUri, true, AcceptHeaderType.ApplicationJson);
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders, AcceptHeaderType acceptHeaderType) {
			var _client = new HttpClient() {
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Get_Config_AppSettings_WebApiClient_RequestTimeout()),
				MaxResponseContentBufferSize = Get_Config_AppSettings_WebApiClient_MaxResponseContentBufferSize()
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			switch (acceptHeaderType) {
				case AcceptHeaderType.ApplicationJson:
					_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					break;
				default:
					throw new Exception("No se ha especificado el tipo de Header Accept.");
			}

			return _client;
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders /*, AcceptHeaderType acceptHeaderType*/) {
			var _client = new HttpClient() {
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Get_Config_AppSettings_WebApiClient_RequestTimeout()),
				MaxResponseContentBufferSize = Get_Config_AppSettings_WebApiClient_MaxResponseContentBufferSize()
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			//switch (acceptHeaderType) {
			//	case AcceptHeaderType.ApplicationJson:
			//		_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MimeType_AppJson));
			//		break;
			//	default:
			//		throw new Exception("No se admite el tipo MIME para el Header Accept.");
			//}

			return _client;
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
