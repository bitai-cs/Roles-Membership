using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManStoreGroupMembersHelper<BSO> : BaseHelper<BSO>
	{
		public AzManStoreGroupMembersHelper() : base()
		{
		}

		public AzManStoreGroupMembersHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAllAsync(string store, string storeGroup)
		{
			var _requestUri = string.Format("api/AzManStoreGroupMembers?store={0}&storeGroup={1}", store, storeGroup);
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetMemberIfoAsync(string store, string storeGroup, int storeGroupMemberId)
		{
			string _requestUri = string.Format("api/AzManStoreGroupMembers?store={0}&storeGroup={1}&storeGroupMemberId={2}", store, storeGroup, storeGroupMemberId.ToString());

			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetMembersOrNonMembersAsync(string store, string storeGroup, bool isMember)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/AzManStoreGroupMembers?store={0}&storeGroup={1}&isMember={2}", store, storeGroup, isMember.ToString());

			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);

				//if (!_respMsg.IsSuccessStatusCode) {
				//    _return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//    string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//    var _list = JsonConvert.DeserializeObject<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>>(_jsonContent);

				//    _return.Add("contentData", new object[] { _list });
				//}
			}

			//return _return;
		}
	}
}
