using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers {
	public class AzManItemsHelper<BSO> : BaseHelper<BSO> {
		internal AzManItemsHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetItemByNameAsync(string item, string store, string application, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			string _requestUri = string.Format("api/AzManItems/{0}?store={1}&application={2}&loadAttributes={3}&loadMembers={4}&loadItemsWhereIAmAMember={5}&loadAuthorizations={6}", item, store, application, loadAttributes.ToString(), loadMembers.ToString(), loadItemsWhereIAmAMember, loadAuthorizations);

			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetItemsAsync(string store, string application, NetSqlAzMan.ServiceBusinessObjects.ItemType itemType, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			string _requestUri = string.Format("api/AzManItems?store={0}&application={1}&itemType={2}&loadAttributes={3}&loadMembers={4}&loadItemsWhereIAmAMember={5}&loadAuthorizations={6}", store, application, itemType.ToString(), loadAttributes.ToString(), loadMembers.ToString(), loadItemsWhereIAmAMember.ToString(), loadAuthorizations.ToString());

			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetItemMembersAsync(string store, string application, string item, NetSqlAzMan.ServiceBusinessObjects.ItemType itemMemberType, bool loadItemMemberAttributes, bool loadItemMemberMembers, bool loadItemsWhereItemMemberIsMember, bool loadItemMemberAuthorizations) {
			string _requestUri = string.Format("api/AzManItems?store={0}&application={1}&item={2}&itemMemberType={3}&loadItemMemberAttributes={4}&loadItemMemberMembers={5}&loadItemsWhereItemMemberIsMember={6}&loadItemMemberAuthorizations={7}", store, application, item, itemMemberType.ToString(), loadItemMemberAttributes.ToString(), loadItemMemberMembers.ToString(), loadItemsWhereItemMemberIsMember.ToString(), loadItemMemberAuthorizations.ToString());

			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> PostItemAsync(string store, string application, BSO bso) {
			string _requestUri = string.Format("api/AzManItems?store={0}&application={1}", store, application);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.PostAsJsonAsync<BSO>(_requestUri, bso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> PutItemAsync(string item, string store, string application, BSO modifiedItem) {
			string _requestUri = string.Format("api/AzManItems/{0}?store={1}&application={2}", item, store, application);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.PutAsJsonAsync<BSO>(_requestUri, modifiedItem);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> DeleteItemAsync(string id, string store, string application) {
			string _requestUri = string.Format("api/AzManItems/{0}?store={1}&application={2}", id, store, application);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.DeleteAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}