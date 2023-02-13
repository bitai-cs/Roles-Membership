using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class DBUsersHelper<BSO> : BaseHelper<BSO>
	{
		public DBUsersHelper() : base()
		{
		}

		public DBUsersHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetListAsync(string sortOrderField = "UserName", bool ascOrder = true, string userFilter = null, string nameFilter = null, Nullable<bool> enabledFilter = null)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/DBUser?sortOrderField={0}&ascendingOrder={1}&userFilter={2}&nameFilter={3}&enabledFilter={4}", sortOrderField, ascOrder.ToString(), userFilter ?? string.Empty, nameFilter ?? string.Empty, enabledFilter.HasValue ? enabledFilter.Value.ToString() : string.Empty);

			using (var _client = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await this.GetStoredResponseEnumerableContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode)
				//{
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else
				//{
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();

				//	//Deserializar contenido de la respuesta
				//	var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.DBUser>>(_jsonContent));

				//	_return.Add("list", new object[] { _list });
				//}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetByIdAsync(int id)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/DBUser/{0}", id.ToString());

			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _c.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return.Add("error", new object[] { _requestUri, _respMsg });
				}
				else
				{
					string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
					var _data = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.DBUser>(_jsonContent);

					_return.Add("data", new object[] { _data });
				}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PostAsync(NetSqlAzMan.ServiceBusinessObjects.DBUser data)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/DBUser";
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _c.PostAsJsonAsync(_requestUri, data);
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
				//	var _data = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.DBUser>(_jsonContent);

				//	_return.Add("data", new object[] { _data });
				//}
			}

			return _return;
		}

		public async Task<Dictionary<string, IEnumerable<object>>> PutAsync(int id, NetSqlAzMan.ServiceBusinessObjects.DBUser data)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/DBUser/{0}";
			_requestUri = string.Format(_requestUri, id.ToString());
			using (var _c = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _c.PutAsJsonAsync(_requestUri, data);
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
				//	var _data = JsonConvert.DeserializeObject<NetSqlAzMan.ServiceBusinessObjects.DBUser>(_jsonContent);

				//	_return.Add("data", new object[] { _data });
				//}
			}

			return _return;
		}
	}
}
