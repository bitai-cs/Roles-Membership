using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class AzManStoresHelper<BSO> : BaseHelper<BSO>
	{
		public AzManStoresHelper() : base()
		{
		}

		public AzManStoresHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetAsync(bool loadAttributes, bool loadStoreGroups, bool loadStoreApplications)
		{
			string _requestUri = string.Format("api/AzManStores?loadAttributes={0}&loadStoreGroups={1}&loadStoreApplications={2}", loadAttributes.ToString(), loadStoreGroups.ToString(), loadStoreApplications.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri))
			{
				var _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PostAsync(NetSqlAzMan.ServiceBusinessObjects.AzManStore store)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/AzManStores";

			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.PostAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_requestUri, store);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await this.GetStoredResponseContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode)
				//{
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else
				//{
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	var _contentData = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_jsonContent);

				//	_return.Add("contentData", new object[] { _contentData });
				//}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PutAsync(string storeName, NetSqlAzMan.ServiceBusinessObjects.AzManStore modifiedStore)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/AzManStores/{0}", storeName);

			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.PutAsJsonAsync<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_requestUri, modifiedStore);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await this.GetStoredResponseContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode) {
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	var _contentData = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_jsonContent);

				//	_return.Add("contentData", new object[] { _contentData });
				//}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> DeleteAsync(string storeName)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/AzManStores/{0}", storeName);
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				var _respMsg = await _c.DeleteAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await this.GetStoredResponseContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode)
				//{
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else
				//{
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	NetSqlAzMan.ServiceBusinessObjects.AzManStore _list = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_jsonContent);

				//	_return.Add("list", new object[] { _list });
				//}
			}

			return _return;
		}
	}
}
