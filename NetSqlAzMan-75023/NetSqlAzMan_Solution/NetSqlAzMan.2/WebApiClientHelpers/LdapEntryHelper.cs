using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.WebApiClientHelpers {
	internal class LdapEntryHelper<BSO> : BaseHelper<BSO> {
		internal LdapEntryHelper(string webApiUri) : base(webApiUri) {
		}

		/// <summary>
		/// Obtiene los Entries filtrandolos por un atributo.
		/// </summary>
		/// <param name="profile">Perfil del servidor que se va usar</param>
		/// <param name="useGC">True: Para acceder al global Catalog, False: Para acceder al directorio predeterminado del servidor</param>
		/// <param name="attribute">Atributo por el que se buscará.</param>
		/// <param name="attributeFilter">Valor del atributo que se buscará</param>
		/// <returns></returns>
		internal async Task<Dictionary<string, IEnumerable<object>>> GetEntriesByAttribute(string profile, bool useGC, LDAPHelperDTO.EntryAttribute attribute, string attributeFilter) {
			var _requestUri = string.Format("api/LdapEntry?profile={0}&useGC={1}&attribute={2}&attributeFilter={3}", profile, useGC.ToString(), attribute.ToString(), attributeFilter);

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
