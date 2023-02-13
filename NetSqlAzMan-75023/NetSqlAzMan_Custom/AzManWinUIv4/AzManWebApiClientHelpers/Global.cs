using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers
{
	internal static class Global
	{
		internal const string Config_AppSettings_AzMan_WebApiClient_RequestTimeout_Key = "AzMan_WebApiClient_RequestTimeout";
		internal const string Config_AppSettings_AzMan_WebApiClient_MaxResponseContentBufferSize_Key = "AzMan_WebApiClient_MaxResponseContentBufferSize";

		internal const string MimeType_AppJson = "application/json";
		internal const string MimeType_TextHtml = "text/html";
		internal const string MimeType_NoContent = "";

		internal enum AcceptHeaderType
		{
			ApplicationJson
		}

		internal static int Get_Config_AppSettings_AzMan_WebApiClient_RequestTimeout() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Global.Config_AppSettings_AzMan_WebApiClient_RequestTimeout_Key]);
		}

		internal static int Get_Config_AppSettings_AzMan_WebApiClient_MaxResponseContentBufferSize() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Global.Config_AppSettings_AzMan_WebApiClient_MaxResponseContentBufferSize_Key]);
		}

		internal static HttpClient GetHttpClient(string webApiUri) {
			return GetHttpClient(webApiUri, true, AcceptHeaderType.ApplicationJson);
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders, AcceptHeaderType acceptHeaderType) {
#if DEBUG
			System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
				return true;
			};
#endif
			var _client = new HttpClient() {
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Global.Get_Config_AppSettings_AzMan_WebApiClient_RequestTimeout()),
				MaxResponseContentBufferSize = Global.Get_Config_AppSettings_AzMan_WebApiClient_MaxResponseContentBufferSize()
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

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders) {
			var _client = new HttpClient() {
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Global.Get_Config_AppSettings_AzMan_WebApiClient_RequestTimeout()),
				MaxResponseContentBufferSize = Global.Get_Config_AppSettings_AzMan_WebApiClient_MaxResponseContentBufferSize()
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			return _client;
		}
	}
}
