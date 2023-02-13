using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class LDAPConfigController :BaseApiController
	{
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>))]
		public async Task<IHttpActionResult> GetAllConfigAsync() {
			NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory _bol = new NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory();
			var _t = Task.Run(() => _bol.GetAllLDAPConfigAsync());

			return this.Ok(await _t);
		}
	}
}