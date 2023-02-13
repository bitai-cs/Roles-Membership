using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class DepartmentsHelper<BSO> : BaseHelper<BSO>
	{
		public DepartmentsHelper() : base()
		{
		}

		public DepartmentsHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetListAsync(string sortOrderField = "DepartmentName", bool ascendingOrder = true, string departmentNameFilter = null)
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/Department?sortOrderField={0}&ascendingOrder={1}&departmentNameFilter={2}", sortOrderField, ascendingOrder.ToString(), departmentNameFilter ?? string.Empty);

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
				//	var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.Department>>(_jsonContent));

				//	_return.Add("list", new object[] { _list });
				//}
			}

			return _return;
		}

		//public async Task<Dictionary<string, IEnumerable<object>>> GetSelectListFromDepartmentsAsync(string selectedId) {
		//	var _return = new Dictionary<string, IEnumerable<object>>();

		//	Helpers.DepartmentsHelper _dh = new Helpers.DepartmentsHelper();
		//	var _dhReturn = await _dh.GetListAsync();
		//	if (_dhReturn.ContainsKey("error")) {
		//		var _values = _dhReturn["error"].ToArray();
		//		var _list = new object[] { _values[0].ToString(), (HttpResponseMessage)_values[1] };
		//		_return.Add("error", _list);

		//		return _return;
		//	}
		//	else {
		//		var _dhValue = _dhReturn["list"].ToArray();
		//		var _listDep = (List<NetSqlAzMan.ServiceBusinessObjects.Department>)_dhValue[0];
		//		var departmentList = from value in _listDep
		//									select new SelectListItem {
		//										Value = value.DepartmentId.ToString(),
		//										Text = value.DepartmentName,
		//										Selected = value.DepartmentId.ToString().Equals(selectedId)
		//									};

		//		_return.Add("list", new object[] { departmentList });

		//		return _return;
		//	}
		//}
	}
}