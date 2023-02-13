using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWebApiClientHelpers
{
	public class LdapWebApiServerBaseDNsHelper<BSO> : BaseHelper<BSO>
	{
		public LdapWebApiServerBaseDNsHelper() : base()
		{
		}

		public LdapWebApiServerBaseDNsHelper(string webApiUri) : base(webApiUri) {
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByDomainProfileAndWideScopeStatusAsync(string domainProfile, Nullable<bool> wideScopeBaseDN) {
			string _requestUri = string.Format("api/LdapWebApiServerBaseDN?domainProfile={0}&wideScopeBaseDN={1}", domainProfile, wideScopeBaseDN);
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}
	}
}
