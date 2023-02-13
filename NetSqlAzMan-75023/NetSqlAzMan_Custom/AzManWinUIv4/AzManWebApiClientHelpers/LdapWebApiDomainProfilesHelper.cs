using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers
{
	public class LdapWebApiDomainProfilesHelper<BSO> :BaseHelper<BSO>
	{
		internal LdapWebApiDomainProfilesHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetAllAsync() {
			string _requestUri = "api/LdapWebApiDomainProfile";
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> UpdateAsync(byte id, BSO sbo) {
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/LdapWebApiDomainProfile/{0}";
			_requestUri = string.Format(_requestUri, id.ToString());
			using (var _h = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _h.PutAsJsonAsync(_requestUri, sbo);
				if (!_respMsg.IsSuccessStatusCode)
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				else
					_return = await this.GetStoredResponseContentAsync(_respMsg);
			}

			return _return;
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> RegisterAsync(BSO sbo) {
			var _return = new Dictionary<string, IEnumerable<object>>();
			string _requestUri = "api/LdapWebApiDomainProfile/";
			using (var _h = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _h.PostAsJsonAsync(_requestUri, sbo);
				if (!_respMsg.IsSuccessStatusCode)
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				else
					_return = await this.GetStoredResponseContentAsync(_respMsg);
			}

			return _return;
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(byte id, BSO sbo) {
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/LdapWebApiDomainProfile/{0}";
			_requestUri = string.Format(_requestUri, id.ToString());
			using (var _c = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _request = new HttpRequestMessage(HttpMethod.Delete, _requestUri) {
					Content = new ObjectContent<BSO>(sbo, new System.Net.Http.Formatting.JsonMediaTypeFormatter())
				};
				var _respMsg = await _c.SendAsync(_request);
				if (!_respMsg.IsSuccessStatusCode)
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				else
					_return = await this.GetStoredResponseContentAsync(_respMsg);
			}

			return _return;
		}
	}
}