using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWebApiClientHelpers {
	public class LDAPConfigHelper<BSO> : BaseHelper<BSO> {
		public LDAPConfigHelper() : base()
		{
		}

		public LDAPConfigHelper(string webApiUri) : base(webApiUri) {
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetListAsync() {
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/LDAPConfig";
			using (var _client = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson)) {
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
