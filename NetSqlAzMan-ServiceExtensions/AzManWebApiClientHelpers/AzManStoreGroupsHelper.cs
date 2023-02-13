using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManStoreGroupsHelper<BSO> : BaseHelper<BSO>
	{
		public AzManStoreGroupsHelper() : base()
		{
		}

		public AzManStoreGroupsHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAllAsync(string store, bool loadStoreGroupMembers)
		{
			var _requestUri = string.Format("api/AzManStoreGroups?store={0}&loadStoreGroupMembers={1}", store, loadStoreGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByNameAsync(string id, string store, bool loadStoreGroupMembers)
		{
			string _requestUri = string.Format("api/AzManStoreGroups/{0}?store={1}&loadStoreGroupMembers={2}", id, store, loadStoreGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetBySidAsync(string store, string storeGroupBinaryId, string storeGroupStringId, bool loadStoreGroupMembers)
		{
			var _requestUri = string.Format("api/AzManStoreGroups?store={0}&storeGroupBinaryId={1}&storeGroupStringId={2}&loadStoreGroupMembers={3}", store, System.Net.WebUtility.UrlEncode(storeGroupBinaryId), storeGroupStringId, loadStoreGroupMembers.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetLDAPQueryResultAsync(string store, string storeGroupName, string ldapQuery)
		{
			string _requestUri = string.Format("api/AzManStoreGroups?store={0}&storeGroupName={1}&ldapQuery={2}", store, storeGroupName, ldapQuery);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetIsStoreGroupMemberAsync(string store, string storeGroup, string userNameToCheck, string domainProfile = null)
		{
			string _requestUri = string.Format("api/AzManStoreGroups/GetIsStoreGroupMember?store={0}&storeGroup={1}&userNameToCheck={2}&domainProfile={3}", store, storeGroup, userNameToCheck, domainProfile);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PostAsync(NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup newBso)
		{
			string _requestUri = "api/AzManStoreGroups";
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.PostAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_requestUri, newBso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PutAsync(string id, NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup storeGroup)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/AzManStoreGroups/{0}", id);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.PutAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_requestUri, storeGroup);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await this.GetStoredResponseContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode) {
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	var contentData_ = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_jsonContent);

				//	_return.Add("contentData", new object[] { contentData_ });
				//}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(string id, BSO storeGroup)
		{
			string _requestUri = string.Format("api/AzManStoreGroups/{0}", id);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true))
			{
				var _request = new HttpRequestMessage(HttpMethod.Delete, _requestUri);
				_request.Content = new ObjectContent<BSO>(storeGroup, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
				//_request.Content = new StringContent(JsonConvert.SerializeObject(storeGroup), System.Text.Encoding.UTF8, Common.MimeType_AppJson);
				var _respMsg = await _c.SendAsync(_request);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}
