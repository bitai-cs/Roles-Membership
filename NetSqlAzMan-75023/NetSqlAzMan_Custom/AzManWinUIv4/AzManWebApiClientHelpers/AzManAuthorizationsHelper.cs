using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers
{
	public class AzManAuthorizationsHelper<BSO> :BaseHelper<BSO>
	{
		internal AzManAuthorizationsHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetAllByItemAsync(string store, string application, string item, bool loadAttributes) {
			string _requestUri = string.Format("api/AzManAuthorizations?store={0}&application={1}&item={2}&loadAttributes={3}", store, application, item, loadAttributes.ToString());
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetMemberOrOwnerInfoAsync(string store, string application, string item, int authorizationId, bool getMemberInfo) {
			string _requestUri = string.Format("api/AzManAuthorizations?store={0}&application={1}&item={2}&authorizationId={3}&getMemberInfo={4}", store, application, item, authorizationId, getMemberInfo.ToString());
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(int id, string store, string application, string item) {
			string _requestUri = string.Format("api/AzManAuthorizations/{0}?store={1}&application={2}&item={3}", id, store, application, item);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.DeleteAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> PostAsync(BSO bso) {
			string _requestUri = "api/AzManAuthorizations";
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.PostAsJsonAsync<BSO>(_requestUri, bso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> PutAsync(int id, BSO bso) {
			string _requestUri = string.Format("api/AzManAuthorizations/{0}", id);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.PutAsJsonAsync<BSO>(_requestUri, bso);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}