using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.WebApiClientHelpers {
	internal class LdapAuthenticationHelper<BSO> : BaseHelper<BSO> {
		internal LdapAuthenticationHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetAuthenticationAsync(string profile, bool useGC, string userName, string password) {
			var _requestUri = string.Format("api/LdapAuthentication?profile={0}&useGC={1}&userName={2}&password={3}", profile, useGC.ToString(), userName, password);

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
