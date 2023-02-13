using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI.AzManWebApiClientHelpers {
	public class LDAPConfigHelper<BSO> : BaseHelper<BSO> {
		internal LDAPConfigHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetListAsync() {
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/LDAPConfig";
			using (var _client = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				HttpResponseMessage _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode) {
					_return.Add("error", new object[] { _requestUri, _respMsg });
				}
				else {
					string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
					var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>>(_jsonContent));

					_return.Add("list", new object[] { _list });
				}
			}

			return _return;
		}
	}
}
