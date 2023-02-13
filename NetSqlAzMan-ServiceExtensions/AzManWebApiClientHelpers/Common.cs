using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWebApiClientHelpers
{
	public static class Common
	{
		internal const string MimeType_AppJson = "application/json";
		internal const string MimeType_TextHtml = "text/html";
		internal const string MimeType_NoContent = "";

		internal enum AcceptHeaderType
		{
			ApplicationJson
		}

		public static string Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri;
		public static int Config_AzManWebApiClient_RequestTimeout;
		public static int Config_AzManWebApiClient_MaxResponseContentBufferSize;
		
		internal static HttpClient GetHttpClient(string webApiUri)
		{
			return GetHttpClient(webApiUri, true, AcceptHeaderType.ApplicationJson);
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders, AcceptHeaderType acceptHeaderType)
		{
#if DEBUG
			System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
			{
				return true;
			};
#endif
			var _client = new HttpClient()
			{
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Common.Config_AzManWebApiClient_RequestTimeout),
				MaxResponseContentBufferSize = Common.Config_AzManWebApiClient_MaxResponseContentBufferSize
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			switch (acceptHeaderType)
			{
				case AcceptHeaderType.ApplicationJson:
					_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					break;
				default:
					throw new Exception("No se ha especificado el tipo de Header Accept.");
			}

			return _client;
		}

		internal static HttpClient GetHttpClient(string webApiUri, bool clearDefaultRequestHeaders)
		{
			var _client = new HttpClient()
			{
				BaseAddress = new Uri(webApiUri),
				Timeout = TimeSpan.FromSeconds(Common.Config_AzManWebApiClient_RequestTimeout),
				MaxResponseContentBufferSize = Common.Config_AzManWebApiClient_MaxResponseContentBufferSize
			};

			if (clearDefaultRequestHeaders)
				_client.DefaultRequestHeaders.Clear();

			return _client;
		}
	}
}
