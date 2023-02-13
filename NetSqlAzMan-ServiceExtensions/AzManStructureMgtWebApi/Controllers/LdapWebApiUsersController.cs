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
	public class LdapWebApiUsersController :BaseApiController
	{
		//[HttpGet]
		//[ResponseType(typeof(LDAPHelperDTO.AsyncResult))]
		//public async Task<IHttpActionResult> SearchUsersAndGroupsAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, string searchCriteria, Nullable<LDAPHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
		//	try {
		//		var _bol = new NetSqlAzMan.CustomBussinessLogic.LdapWebApiBusinessFactory();
		//		var _result = await _bol.SearchUsersAndGroupsAsyncModeAsync(domainProfile, useGC, baseDNOrder, searchCriteria, requiredEntryAttributes);

		//		//Devolver OK asi no haya encontrado datos y la lista este con 0 elementos
		//		return Ok(_result);
		//	}
		//	catch (Exception ex) {
		//		return InternalServerError(ex);
		//	}
		//}
		[HttpGet]
		[ResponseType(typeof(LdapHelperDTO.AsyncResult))]
		public async Task<HttpResponseMessage> SearchUsersAndGroupsAsync(string domainProfile, Nullable<bool> useGC, Nullable<byte> baseDNOrder, string searchCriteria, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			var _bol = new NetSqlAzMan.CustomBussinessLogic.LdapWebApiBusinessFactory();
			var _result = await _bol.SearchUsersAndGroupsAsyncModeAsync(domainProfile, useGC, baseDNOrder, searchCriteria, requiredEntryAttributes);

			var _resp = this.GetResponseMessageForOK(_result, string.Format("La búsqueda devolvió {0} " +
				"entradas Ldap.", _result.Entries.Count()));

			return _resp;
		}
	}
}
