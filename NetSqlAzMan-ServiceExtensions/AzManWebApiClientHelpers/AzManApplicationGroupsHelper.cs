using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManApplicationGroupsHelper<BSO> : BaseHelper<BSO>
	{
		#region Constructor
		public AzManApplicationGroupsHelper() : base()
		{
		}

		public AzManApplicationGroupsHelper(string webApiUri) : base(webApiUri)
		{
		}
		#endregion

		public async Task<Dictionary<string, IEnumerable<object>>> GetAllAsync(string store, string application, bool loadApplicationGroupMembers)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups?store={0}&application={1}&loadApplicationGroupMembers={2}", store, application, loadApplicationGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
				{
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
				}
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByNameAsync(string id, string store, string application, bool loadApplicationGroupMembers)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups/{0}?store={1}&application={2}&loadApplicationGroupMembers={2}", id, store, application, loadApplicationGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByApplicationGroupIdAsync(string store, string application, string applicationGroupBinaryId, string applicationGroupStringId, bool loadApplicationGroupMembers)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups?store={0}&application={1}&applicationGroupBinaryId={2}&applicationGroupStringId={3}&loadApplicationGroupMembers={4}", store, application, System.Net.WebUtility.UrlEncode(applicationGroupBinaryId), applicationGroupStringId, loadApplicationGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetLdapQueryResultAsync(string store, string application, string applicationGroupName, string ldapQuery)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups?store={0}&application={1}&applicationGroupName={2}&ldapQuery={3}", store, application, applicationGroupName, ldapQuery);

			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetIsApplicationGroupMemberAsync(string store, string application, string applicationGroup, string userNameToCheck, string domainProfile = null)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups/GetIsApplicationGroupMember?store={0}&application={1}&applicationGroup={2}&userNameToCheck={3}&domainProfile={4}", store, application, applicationGroup, userNameToCheck, domainProfile);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PostAsync(BSO newBso)
		{
			string _requestUri = "api/AzManApplicationGroups";
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.PostAsJsonAsync<BSO>(_requestUri, newBso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PutAsync(string id, BSO modifiedBso)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups/{0}", id);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.PutAsJsonAsync<BSO>(_requestUri, modifiedBso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(string id, BSO bso)
		{
			string _requestUri = string.Format("api/AzManApplicationGroups/{0}", id);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true))
			{
				var _request = new HttpRequestMessage(HttpMethod.Delete, _requestUri);
				_request.Content = new ObjectContent<BSO>(bso, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
				//_request.Content = new StringContent(JsonConvert.SerializeObject(applicationGroup), System.Text.Encoding.UTF8, Common.MimeType_AppJson);
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