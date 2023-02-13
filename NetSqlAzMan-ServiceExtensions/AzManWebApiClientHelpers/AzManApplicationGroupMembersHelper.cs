using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManApplicationGroupMembersHelper<BSO> : BaseHelper<BSO>
	{
		public AzManApplicationGroupMembersHelper() : base()
		{
		}

		public AzManApplicationGroupMembersHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAsync(string store, string application, string applicationGroup)
		{
			var _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}", store, application, applicationGroup);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetMemberIfoAsync(string store, string application, string applicationGroup, int applicationGroupMemberId)
		{
			string _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}&applicationGroupMemberId={3}", store, application, applicationGroup, applicationGroupMemberId.ToString());

			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetMembersOrNonMembersAsync(string store, string application, string applicationGroup, bool isMember)
		{
			string _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}&isMember={3}", store, application, applicationGroup, isMember.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}
	}
}
