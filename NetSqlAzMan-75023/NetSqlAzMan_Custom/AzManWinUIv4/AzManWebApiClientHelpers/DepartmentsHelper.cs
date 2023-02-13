using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWinUI.AzManWebApiClientHelpers {
	public class DepartmentsHelper<BSO> : BaseHelper<BSO> {
		internal DepartmentsHelper(string webApiUri) : base(webApiUri) {
		}

		internal async Task<Dictionary<string, IEnumerable<object>>> GetListAsync(string sortOrderField = "DepartmentName", bool ascendingOrder = true, string departmentNameFilter = null) {
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = string.Format("api/Department?sortOrderField={0}&ascendingOrder={1}&departmentNameFilter={2}", sortOrderField, ascendingOrder.ToString(), departmentNameFilter ?? string.Empty);

			using (var _client = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				HttpResponseMessage _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode) {
					_return.Add("error", new object[] { _requestUri, _respMsg });
				}
				else {
					string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
					var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.Department>>(_jsonContent));

					_return.Add("list", new object[] { _list });
				}
			}

			return _return;
		}

		//internal async Task<Dictionary<string, IEnumerable<object>>> GetSelectListFromDepartmentsAsync(string selectedId) {
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