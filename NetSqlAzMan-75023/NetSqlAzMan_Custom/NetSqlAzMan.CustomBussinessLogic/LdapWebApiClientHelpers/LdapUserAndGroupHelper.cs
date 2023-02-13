using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic.LdapWebApiClientHelpers
{
	internal class LdapUserAndGroupHelper<BSO> : BaseHelper<BSO>
	{
		internal LdapUserAndGroupHelper(string webApiUri) : base(webApiUri) {
		}

		/// <summary>
		/// Obtiene los Entries filtrandolos por un atributo.
		/// </summary>
		/// <param name="serverProfile">Perfil del servidor que se va usar</param>
		/// <param name="useGC">True: Para acceder al global Catalog, False: Para acceder al directorio predeterminado del servidor</param>
		/// <param name="firstAttribute">Atributo por el que se buscará.</param>
		/// <param name="firstAttributeFilter">Valor del atributo que se buscará</param>
		/// <returns></returns>
		internal async Task<Dictionary<string, IEnumerable<object>>> GetUserAndGroupsByAttributesAsyncModeAsync(string serverProfile, Nullable<bool> useGC, string domainProfile, string baseDN, Nullable<LdapHelperDTO.EntryAttribute> firstAttribute, string firstAttributeFilter, Nullable<bool> conjunction, Nullable<LdapHelperDTO.EntryAttribute> secondAttribute, string secondAttributeFilter, Nullable<LdapHelperDTO.RequiredEntryAttributes> requiredEntryAttributes) {
			string _requestUri;
			if (conjunction == null)
				_requestUri = string.Format("api/LdapUserAndGroup/GetAsync?serverProfile={0}&useGC={1}&domainProfile={2}&baseDN={3}&firstAttribute={4}&firstAttributeFilter={5}&requiredEntryAttributes={6}", serverProfile, useGC.ToString(), domainProfile, baseDN, firstAttribute.ToString(), firstAttributeFilter, requiredEntryAttributes);
			else
				_requestUri = string.Format("api/LdapUserAndGroup/GetAsync?serverProfile={0}&useGC={1}&domainProfile={2}&baseDN={3}&firstAttribute={4}&firstAttributeFilter={5}&conjunction={6}&secondAttribute={7}&secondAttributeFilter={8}&requiredEntryAttributes={9}", serverProfile, useGC.ToString(), domainProfile, baseDN, firstAttribute.ToString(), firstAttributeFilter, conjunction.Value, secondAttribute.Value.ToString(), secondAttributeFilter, requiredEntryAttributes);

			using (var _client = Global.GetHttpClient(this.WebApiUri, true, Global.AcceptHeaderType.ApplicationJson)) {
				var _respMsg = await _client.GetAsync(_requestUri);
				if (!_respMsg.IsSuccessStatusCode)
					return GetStoredResponseError(_requestUri, _respMsg);
				else
					return await GetStoredResponseContentAsync(_respMsg);
			}
		}
	}
}