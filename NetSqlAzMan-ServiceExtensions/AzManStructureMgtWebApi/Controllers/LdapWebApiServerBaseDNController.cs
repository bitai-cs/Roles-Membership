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
	public class LdapWebApiServerBaseDNController :BaseApiController
	{
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>))]
		public async Task<IHttpActionResult> GetByDomainProfileAndWideScopeBaseDNStatusAsync(string domainProfile, bool wideScopeBaseDN) {
			var _bol = new NetSqlAzMan.CustomBussinessLogic.LdapWacServerBaseDNBusinessFactory();
			var _return = await Task.Run(() => _bol.GetLdapServerBaseDNByDomainProfileAndWideScopeStatusAsync(domainProfile, wideScopeBaseDN));

			return this.Ok(_return);
		}
	}
}
