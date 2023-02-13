using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManStorageHelper<BSO> : BaseHelper<BSO>
	{
		public AzManStorageHelper() : base()
		{
		}

		public AzManStorageHelper(string webApiUri) : base(webApiUri) {
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAsync(bool loadStores) {
			string _requestUri = string.Format("api/AzManStorage?loadStores={0}", loadStores);
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