using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic.LdapWebApiClientHelpers {
	internal class LdapAuthenticationHelper<BSO> : BaseHelper<BSO> {
		internal LdapAuthenticationHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetAuthenticationAsync(string serverProfile, Nullable<bool> useGC, string domainProfile, string domainName, string userName, string password) {
			var _requestUri = string.Format("api/LdapAuthentication?serverProfile={0}&useGC={1}&domainProfile={2}&domainName={3}&userName={4}&password={5}", serverProfile, useGC.ToString(), domainProfile, domainName, userName, password);

			using (var _client = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}
