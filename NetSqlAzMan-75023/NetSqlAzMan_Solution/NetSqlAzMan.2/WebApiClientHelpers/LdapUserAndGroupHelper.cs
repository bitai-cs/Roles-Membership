using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.WebApiClientHelpers {
	internal class LdapUserAndGroupHelper<BSO> : BaseHelper<BSO> {
		internal LdapUserAndGroupHelper(string webApiUri) : base(webApiUri) {
		}

		/// <summary>
		/// Obtiene los Entries filtrandolos por un atributo.
		/// </summary>
		/// <param name="profile">Perfil del servidor que se va usar</param>
		/// <param name="useGC">True: Para acceder al global Catalog, False: Para acceder al directorio predeterminado del servidor</param>
		/// <param name="firstAttribute">Atributo por el que se buscará.</param>
		/// <param name="firstAttributeFilter">Valor del atributo que se buscará</param>
		/// <returns></returns>
		internal async Task<Dictionary<string, IEnumerable<object>>> GetUserAndGroupsByAttributes(string profile, bool useGC, LDAPHelperDTO.EntryAttribute firstAttribute, string firstAttributeFilter, Nullable<bool> conjunction, Nullable<LDAPHelperDTO.EntryAttribute> secondAttribute, string secondAttributeFilter) {
			string _requestUri;
			if (conjunction == null)
				_requestUri = string.Format("api/LdapUserAndGroup?profile={0}&useGC={1}&firstAttribute={2}&firstAttributeFilter={3}", profile, useGC.ToString(), firstAttribute.ToString(), firstAttributeFilter);
			else
				_requestUri = string.Format("api/LdapUserAndGroup?profile={0}&useGC={1}&firstAttribute={2}&firstAttributeFilter={3}&conjunction={4}&secondAttribute={5}&secondAttributeFilter={6}", profile, useGC.ToString(), firstAttribute.ToString(), firstAttributeFilter, conjunction.Value, secondAttribute.Value.ToString(), secondAttributeFilter);

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