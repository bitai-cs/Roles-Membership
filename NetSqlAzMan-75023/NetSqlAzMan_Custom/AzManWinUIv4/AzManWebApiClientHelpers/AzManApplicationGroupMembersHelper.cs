using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWinUI.AzManWebApiClientHelpers {
	public class AzManApplicationGroupMembersHelper<BSO> : BaseHelper<BSO> {
		internal AzManApplicationGroupMembersHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetAsync(string store, string application, string applicationGroup) {
			var _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}", store, application, applicationGroup);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetMemberIfoAsync(string store, string application, string applicationGroup, int applicationGroupMemberId) {
			string _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}&applicationGroupMemberId={3}", store, application, applicationGroup, applicationGroupMemberId.ToString());

			using (var _c = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				HttpResponseMessage _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetMembersOrNonMembersAsync(string store, string application, string applicationGroup, bool isMember) {
			string _requestUri = string.Format("api/AzManApplicationGroupMembers?store={0}&application={1}&applicationGroup={2}&isMember={3}", store, application, applicationGroup, isMember.ToString());
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}
	}
}
