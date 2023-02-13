using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity
{
	public class Common
	{
		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri_Key = "AzManAspNetIdentity_WebApiClient_EndPointUri";
		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout_Key = "AzManAspNetIdentity_WebApiClient_RequestTimeout";
		private const string Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize_Key = "AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize";

		static Common()
		{
			AzManWebApiClientHelpers.Common.Config_AzManWebApiClient_RequestTimeout = Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout();

			AzManWebApiClientHelpers.Common.Config_AzManWebApiClient_MaxResponseContentBufferSize = Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize();
		}

		public static string Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri {
			get {
				return AzManWebApiClientHelpers.Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri;
			}
			set {
				AzManWebApiClientHelpers.Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri = value;
			}
		}

		private static int Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout()
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_RequestTimeout_Key]);
		}

		private static int Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize()
		{
			return Convert.ToInt32(ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_MaxResponseContentBufferSize_Key]);
		}

		public static string Get_Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri()
		{
			return ConfigurationManager.AppSettings[Common.Config_AppSettings_AzManAspNetIdentity_WebApiClient_EndPointUri_Key];
		}
	}
}
