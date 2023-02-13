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
	public class LDAPClientController :ApiController
	{
		/// <summary>
		/// Search only user and group class objects
		/// </summary>
		/// <param name="domainProfile">Profile to use in search</param>
		/// <param name="searchCriteria">Part o full Canocnical Name or samAccountName</param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>))]
		public async Task<IHttpActionResult> SearchUsersAndGroupsAsync(string domainProfile, string searchCriteria) {
			var _bol = new NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory();
			var _result = await _bol.SearchUsersAndGroupsWithWSvcAsync(domainProfile, searchCriteria);

			//Devolver OK asi no haya encontrado datos y la lista este con 0 elementos
			return Ok(_result);
		}
	}
}
