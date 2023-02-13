using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity.AzManWebApiClientHelpers {
	internal static class Common {
		internal const string MimeType_AppJson = "application/json";
		internal const string MimeType_TextHtml = "text/html";
		internal const string MimeType_NoContent = "";

		internal enum AcceptHeaderType {
			ApplicationJson
		}

		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri_Key = "AzManAspNetIdentity_WebApiClient_EndPointUri";
		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout_Key = "AzManAspNetIdentity_WebApiClient_RequestTimeout";
		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize_Key = "AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize";

		//internal static readonly string Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri;
		internal static readonly int Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout;
		internal static readonly int Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize;

		static Common() {
			//Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri = Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri();

			Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout = Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout();

			Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize = Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize();
		}

		internal static string Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri() {
			return ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri_Key];
		}

		private static int Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout_Key]);
		}

		private static int Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize() {
			return Convert.ToInt32(ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize_Key]);
		}

		internal static HttpClient GetHttpClient(string webApiUri) {
			return GetHttpClient(webApiUri, true, AcceptHeaderType.ApplicationJson);
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders, AcceptHeaderType acceptHeaderType) {
#if DEBUG
			System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
			{
				return true;
			};
#endif
			var _client = new HttpClient() {
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout),
				MaxResponseContentBufferSize = Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize
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
				Timeout = TimeSpan.FromSeconds(Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout),
				MaxResponseContentBufferSize = Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			return _client;
		}
	}
}
