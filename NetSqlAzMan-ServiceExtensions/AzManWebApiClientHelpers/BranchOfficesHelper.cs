using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AzManWebApiClientHelpers
{
	public class BranchOfficesHelper<BSO> : BaseHelper<BSO>
	{
		public BranchOfficesHelper() : base()
		{
		}

		public BranchOfficesHelper(string webApiUri) : base(webApiUri)
		{
		}

		public async Task<Dictionary<string, IEnumerable<object>>> GetListAsync()
		{
			var _return = new Dictionary<string, IEnumerable<object>>();

			string _requestUri = "api/BranchOffice";
			using (var _client = Common.GetHttpClient(this.WebApiUri, true, Common.AcceptHeaderType.ApplicationJson))
			{
				HttpResponseMessage _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
				{
					_return = this.GetStoredResponseError(_requestUri, _respMsg);
				}
				else
				{
					_return = await GetStoredResponseEnumerableContentAsync(_respMsg);
				}
				//if (!_respMsg.IsSuccessStatusCode) {
				//	_return.Add("error", new object[] { _requestUri, _respMsg });
				//}
				//else {
				//	string _jsonContent = await _respMsg.Content.ReadAsStringAsync();
				//	var _list = await Task.Run(() => JsonConvert.DeserializeObject<List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>>(_jsonContent));

				//	_return.Add("list", new object[] { _list });
				//}
			}

			return _return;
		}

		//public async Task<Dictionary<string, IEnumerable<object>>> GetSelectListAsync(IEnumerable<string> selectedIds) {
		//	var _return = new Dictionary<string, IEnumerable<object>>();

		//	Helpers.BranchOfficesHelper _boh = new Helpers.BranchOfficesHelper();
		//	var _bohReturn = await _boh.GetListAsync();
		//	if (_bohReturn.ContainsKey("error")) {
		//		var _values = _bohReturn["error"].ToArray();
		//		_return.Add("error", new object[] { _values[0].ToString(), (HttpResponseMessage)_values[1] });
		//		return _return;
		//	}
		//	else {
		//		var _bohValue = _bohReturn["list"].ToArray();
		//		var _listBOff = (List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>)_bohValue[0];
		//		var branchOfficeList = from value in _listBOff
		//									  select new SelectListItem {
		//										  Value = value.BranchOfficeId.ToString(),
		//										  Text = value.BranchOfficeName,
		//										  Selected = selectedIds.Contains(value.BranchOfficeId)
		//									  };
		//		_return.Add("list", new object[] { branchOfficeList });

		//		return _return;
		//	}
		//}

		//public async Task<Dictionary<string, IEnumerable<object>>> GetSelectListExcludingByIdsAsync(IEnumerable<string> excludeIds) {
		//	var _return = new Dictionary<string, IEnumerable<object>>();

		//	Helpers.BranchOfficesHelper _boh = new Helpers.BranchOfficesHelper();
		//	var _bohReturn = await _boh.GetListAsync();
		//	if (_bohReturn.ContainsKey("error")) {
		//		var _values = _bohReturn["error"].ToArray();
		//		_return.Add("error", new object[] { _values[0].ToString(), (HttpResponseMessage)_values[1] });
		//		return _return;
		//	}
		//	else {
		//		var _bohValue = _bohReturn["list"].ToArray();
		//		var _listBOff = (List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>)_bohValue[0];
		//		var branchOfficeList = from item in _listBOff
		//									  where !excludeIds.Contains(item.BranchOfficeId)
		//									  select new SelectListItem {
		//										  Value = item.BranchOfficeId.ToString(),
		//										  Text = item.BranchOfficeName
		//									  };
		//		_return.Add("list", new object[] { branchOfficeList });

		//		return _return;
		//	}
		//}
	}
}