using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic.LdapWebApiClientHelpers
{
	internal class LdapGroupMembershipHelper<BSO> : BaseHelper<BSO>
	{
		internal LdapGroupMembershipHelper(string webApiUri) : base(webApiUri) {
		}

		/// <summary>
		/// Obtiene los Entries filtrandolos por un atributo.
		/// </summary>
		/// <param name="serverProfile">Perfil del servidor que se va usar</param>
		/// <param name="useGC">True: Para acceder al global Catalog, False: Para acceder al directorio predeterminado del servidor</param>
		/// <param name="attribute">Atributo por el que se buscará.</param>
		/// <param name="attributeFilter">Valor del atributo que se buscará</param>
		/// <returns></returns>
		internal async Task<Dictionary<string, IEnumerable<object>>> GetGroupMembershipAsync(string serverProfile, Nullable<bool> useGC, string domainProfile, string baseDN, Nullable<LdapHelperDTO.EntryAttribute> attribute, string attributeFilter, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			var _requestUri = string.Format("api/LdapGroupMembership/?serverProfile={0}&useGC={1}&domainProfile={2}&baseDN={3}&attribute={4}&attributeFilter={5}&requiredEntryAttributes={6}", serverProfile, useGC.ToString(), domainProfile, baseDN, attribute.ToString(), attributeFilter, requiredEntryAttributes);

			using (var _client = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseEnumerableContentAsync(_respMsg);
			}
		}
	}
}