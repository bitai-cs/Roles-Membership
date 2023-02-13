using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWinUI.AzManWebApiClientHelpers
{
	public class AzManStorageAuthorizationsHelper<BSO> :BaseHelper<BSO>
	{
		internal AzManStorageAuthorizationsHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> CheckAccessAsync(string store, string application, string item, string domainProfile, string userName, Nullable<DateTime> validFor = null, Nullable<bool> operationsOnly = false, IEnumerable<KeyValuePair<string, object>> contextParameters = null) {
			var _jsonContextParams = JsonConvert.SerializeObject(contextParameters);

			string _requestUri = string.Format("api/AzManStorageAuthorizations/CheckAccess?store={0}&application={1}&item={2}&domainProfile={3}&userName={4}&validFor={5}&operationsOnly={6}&contextParameters={7}", store, application, item, domainProfile, userName, validFor, operationsOnly, _jsonContextParams);
			using (var _c = Global.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}