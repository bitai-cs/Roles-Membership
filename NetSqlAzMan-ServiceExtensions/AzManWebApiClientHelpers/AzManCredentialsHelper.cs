using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManCredentialsHelper<BSO> : BaseHelper<BSO>
	{
		public AzManCredentialsHelper() : base()
		{
		}

		public AzManCredentialsHelper(string webApiUri) : base(webApiUri) {
		}

		//public NetSqlAzMan.ServiceBusinessObjects.AzManCredential createCredential(string domainProfile, string userName, string password) {
		//	return new NetSqlAzMan.ServiceBusinessObjects.AzManCredential() {
		//		DomainProfile = domainProfile,
		//		UserName = userName,
		//		Password = password
		//	};
		//}

		public async Task<Dictionary<string, IEnumerable<object>>> ValidateCredentialAsync(NetSqlAzMan.ServiceBusinessObjects.AzManCredential credentials) {
			string _requestUri = "api/AzManCredentials";
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _request = new HttpRequestMessage(HttpMethod.Post, _requestUri);
				_request.Content = new ObjectContent<NetSqlAzMan.ServiceBusinessObjects.AzManCredential>(credentials, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
				//Send request...
				var _respMsg = await _c.SendAsync(_request);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}