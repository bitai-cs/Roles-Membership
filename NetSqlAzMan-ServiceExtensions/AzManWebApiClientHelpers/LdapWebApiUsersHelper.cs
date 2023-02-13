using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWebApiClientHelpers
{
	public class LdapWebApiUsersHelper<BSO> : BaseHelper<BSO>
	{
		public LdapWebApiUsersHelper() : base()
		{
		}

		public LdapWebApiUsersHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> SearchUsersAndGroupsAsyncModeAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, string searchCriteria, LdapHelperDTO.RequiredEntryAttributes requiredEntryAttributes)
		{
			string _requestUri = string.Format("api/LdapWebApiUsers?domainProfile={0}&useGC={1}&baseDNOrder={2}&searchCriteria={3}&requiredEntryAttributes={4}", domainProfile, useGC, baseDNOrder, searchCriteria, requiredEntryAttributes);
			using (var _client = Common.GetHttpClient(this.WebApiUri))
			{
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