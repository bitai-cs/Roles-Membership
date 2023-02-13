using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManAspNetIdentity.AzManWebApiClientHelpers
{
	public class AzManCredentialsHelper<BSO> : BaseHelper<BSO>
	{
		internal AzManCredentialsHelper(string webApiUri) : base(webApiUri) {
		}

		//internal NetSqlAzMan.ServiceBusinessObjects.AzManCredential createCredential(string domainProfile, string userName, string password) {
		//	return new NetSqlAzMan.ServiceBusinessObjects.AzManCredential() {
		//		DomainProfile = domainProfile,
		//		UserName = userName,
		//		Password = password
		//	};
		//}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetCredentialAsync(BSO bso) {
			string _requestUri = "api/AzManCredentials";
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _request = new HttpRequestMessage(HttpMethod.Get, _requestUri);
				_request.Content = new ObjectContent<BSO>(bso, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
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