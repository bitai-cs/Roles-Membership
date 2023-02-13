using Newtonsoft.Json;
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
	public class AzManStorageAuthorizationsController :BaseApiController
	{
		/// <summary>
		/// Obtener el tipo de autorización que tiene un Usuario sobre un Item (Role, Task, Operation)
		/// </summary>
		/// <param name="store">Nombre del Store</param>
		/// <param name="application">Nombre del Application</param>
		/// <param name="item">Nombre del Item</param>
		/// <param name="domainProfile">Opcional; Perfil de Dominio relacionado al userName</param>
		/// <param name="userName">Usuario; si domainProfile está asignado el usuario se buscará en el servicio LDAP que le corresponde, de lo contrario el usuario se buscará en los usuarios registrados en la  base de datos </param>
		/// <param name="validFor">Opcional; Fecha de vigencia de la autorización; comunmente la fecha en la que se está realizando la validación.</param>
		/// <param name="operationsOnly">Solo buscar el item (item name) en Operaciones</param>
		/// <param name="contextParameters">List(KeyValuePair(String, Object)) serializado a JSON y codificado como URL</param>
		/// <returns>HttpResponseMessage</returns>
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationInfo))]
		[ActionName("CheckAccess")]
		public async Task<HttpResponseMessage> CheckAccessAsync(string store, string application, string item, string domainProfile, string userName, Nullable<DateTime> validFor = null, Nullable<bool> operationsOnly = false, string contextParameters = null) {
			if (validFor == null)
				validFor = DateTime.Now;

			List<KeyValuePair<string, object>> _dict = null;
			if (!string.IsNullOrEmpty(contextParameters))
				_dict = JsonConvert.DeserializeObject<List<KeyValuePair<string, object>>>(contextParameters);

			Exception _exce = null;
			NetSqlAzMan.Interfaces.IAzManDBUser _azUser = null;
			var _auth = NetSqlAzMan.Interfaces.AuthorizationType.Neutral;
			var _sboAuth = NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Neutral;
			List<KeyValuePair<string, string>> _attributes = null;

			//Si no se ha pasado el "domainProfile", entonces se evalua un usuario de B.D.
			if (string.IsNullOrEmpty(domainProfile)) {
				_azUser = await Task.Run(() => _storage.GetDBUser(userName));

				_auth = await Task.Run(() => _storage.CheckAccess(store, application, item, _azUser, validFor.Value, operationsOnly.Value, out _attributes, _dict?.ToArray()));
			} //"domainProfile" asignado, se evaluará un usuario en el servicio LDAP relacionado.
			else {
				var _status = await Task.Run(() => _storage.GetLDAPUser(domainProfile, userName, out _azUser, out _exce));
				if (!_status)
					throw _exce;

				_auth = _storage.CheckAccessLDAP(store, application, item, domainProfile, _azUser.CustomSid.StringValue, _azUser.CustomSid.BinaryValue, validFor.Value, operationsOnly.Value, out _attributes, _dict?.ToArray());
			}

			switch (_auth) {
				case NetSqlAzMan.Interfaces.AuthorizationType.Neutral:
					_sboAuth = NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Neutral;
					break;
				case NetSqlAzMan.Interfaces.AuthorizationType.Deny:
					_sboAuth = NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Deny;
					break;
				case NetSqlAzMan.Interfaces.AuthorizationType.Allow:
					_sboAuth = NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Allow;
					break;
				case NetSqlAzMan.Interfaces.AuthorizationType.AllowWithDelegation:
					_sboAuth = NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.AllowWithDelegation;
					break;
				default:
					throw new InvalidOperationException("No se puede identificar el tipo de autorización.");
			}

			var _return = new NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationInfo() {
				AuthorizationType = _sboAuth,
				AuthorizationAttributes = _attributes
			};

			var _headerMsg = string.Format("Usuario {0} tiene el acceso {1} al item {2}->{3}->{4}", string.IsNullOrEmpty(domainProfile) ? userName : domainProfile + "\\" + userName, _sboAuth.ToString(), store, application, item);

			return GetResponseMessageForOK(_return, _headerMsg);
		}
	}
}