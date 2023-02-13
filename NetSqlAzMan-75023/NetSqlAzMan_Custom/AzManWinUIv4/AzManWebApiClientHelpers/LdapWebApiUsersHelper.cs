using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers
{
	public class LdapWebApiUsersHelper<BSO> : BaseHelper<BSO>
	{
		internal LdapWebApiUsersHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> SearchUsersAndGroupsAsyncModeAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, string searchCriteria, LdapHelperDTO.RequiredEntryAttributes requiredEntryAttributes) {
			string _requestUri = string.Format("api/LdapWebApiUsers?domainProfile={0}&useGC={1}&baseDNOrder={2}&searchCriteria={3}&requiredEntryAttributes={4}", domainProfile, useGC, baseDNOrder, searchCriteria, requiredEntryAttributes);
			using (var _client = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _client.GetAsync(_requestUri,
					HttpCompletionOption.ResponseHeadersRead); //Not buffered response read!
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}