using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManApplicationsHelper<BSO> :BaseHelper<BSO>
	{
		public AzManApplicationsHelper() : base()
		{
		}

		public AzManApplicationsHelper(string webApiUri) : base(webApiUri) {
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAsync(string store, bool loadAttributes, bool loadItems, bool loadApplicationGroups) {
			string _requestUri = string.Format("api/AzManApplications?store={0}&loadAttributes={1}&loadItems={2}&loadApplicationGroups={3}", store, loadAttributes.ToString(), loadItems, loadApplicationGroups);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson)) {
				HttpResponseMessage _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return this.GetStoredResponseError(_requestUri, _respMsg);
				else
					return await this.GetStoredResponseEnumerableContentAsync(_respMsg);
				//if (!_respMsg.IsSuccessStatusCode) {
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	var _list = JsonConvert.DeserializeObject<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>>(_jsonContent);

				//	_return.Add("list", new object[] { _list });
				//}
			}
		}

		//public async Task<Dictionary<string, IEnumerable<object>>> GetListAsync(string sortOrderField, bool ascOrder, string userFilter, string nameFilter, Nullable<bool> enabledFilter) {
		//	var _return = new Dictionary<string, IEnumerable<object>>();

		//	string _requestUri = string.Format("api/DBUser?sortOrderField={0}&ascendingOrder={1}&userFilter={2}&nameFilter={3}&enabledFilter={4}", sortOrderField, ascOrder.ToString(), userFilter ?? string.Empty, nameFilter ?? string.Empty, enabledFilter.HasValue ? enabledFilter.Value.ToString() : string.Empty);

		//	using (var _client = Common.GetHttpClient(true, Common.AcceptHeaderType.ApplicationJson)) {
		//		HttpResponseMessage _respMsg = await _client.GetAsync(_requestUri);
		//			if (!_respMsg.IsSuccessStatusCode) {
		//				_return.Add("error", new object[] { _requestUri, _respMsg });
		//			}
		//			else {
		//				string _jsonContent = await _respMsg.Content.ReadAsStringAsync();

		//				//Deserializar contenido de la respuesta
		//				var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.DBUser>>(_jsonContent));

		//				_return.Add("data", new object[] { _list });
		//			}
		//	}

		//	return _return;
		//}
		public async Task<Dictionary<string, IEnumerable<object>>> PostAsync(NetSqlAzMan.ServiceBusinessObjects.AzManApplication newApplication, string store) {
			string _requestUri = string.Format("api/AzManApplications/?store={0}", store);
			using (var _c = Common.GetHttpClient(this.WebApiUri)) {
				var _respMsg = await _c.PostAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_requestUri, newApplication);
				//if (!_respMsg.IsSuccessStatusCode) {
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	NetSqlAzMan.ServiceBusinessObjects.AzManApplication _list = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_jsonContent);

				//	_return.Add("list", new object[] { _list });
				//}
				if (!_respMsg.IsSuccessStatusCode) {
					return this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else {
					return await this.GetStoredResponseContentAsync(_respMsg);
				}
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PutAsync(string application, string store, NetSqlAzMan.ServiceBusinessObjects.AzManApplication modifiedApplication) {
			string _requestUri = string.Format("api/AzManApplications/{0}?store={1}", application, store);

			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _c.PutAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_requestUri, modifiedApplication);
				if (!_respMsg.IsSuccessStatusCode)
					return this.GetStoredResponseError(_requestUri, _respMsg);
				else
					return await this.GetStoredResponseContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(string application, string store) {
			string _requestUri = string.Format("api/AzManApplications/{0}?store={1}", application, store);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _c.DeleteAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}
