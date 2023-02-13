using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManWebApiClientHelpers {
	public class AzManDBUsersHelper<BSO> : BaseHelper<BSO> {
		public AzManDBUsersHelper() : base()
		{
		}

		public AzManDBUsersHelper(string webApiUri) : base(webApiUri) {
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAllAsync() {
			string _requestUri = "api/AzManDBUsers";
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByUserNameOrIdAsync(string userNameOrId, bool isId) {
			string _requestUri = string.Format("api/AzManDBUsers?userNameOrId={0}&isId={1}", userNameOrId, isId);
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}
