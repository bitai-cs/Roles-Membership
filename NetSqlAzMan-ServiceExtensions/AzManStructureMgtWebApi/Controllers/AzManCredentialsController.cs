using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.ComponentModel.DataAnnotations;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManCredentialsController :BaseApiController
	{
		// POST: api/AzManCredentials
		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManDBUser))]
		public async Task<HttpResponseMessage> ValidateCredential([FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManCredential credential) {
			//if (!ModelState.IsValid)
			//	return GetResponseMessageForInvalidModel(ModelState);

			NetSqlAzMan.Interfaces.IAzManDBUser _user = null;
			//Validar las credenciales para un usuario registrado en base de datos
			if (string.IsNullOrEmpty(credential.DomainProfile)) {
				_user = await Task.Run(() => {
					try {
						//TODO: Verificar que devuelva el tipo de error correcto 
						//cuando no encuentre el usuario
						return _storage.GetDBUser(credential.UserName);
					}
					catch (NetSqlAzMan.SqlAzManException) {
						return null;
					}
					catch {
						throw;
					}
				});

				if (_user == null)
					return GetResponseMessageForForbidden(null, "No se pudo encontrar al usuario.");

				//Check Password
				var _password = _user.CustomColumns["Password"].ToString();
				if (credential.Password.Equals(_password))
					return GetResponseMessageForOK(_user, "La credencial es válida.");
				else
					return GetResponseMessageForForbidden(null, "Contraseña incorrecta.");
			}
			else { //Validar las credenciales para un usuario Ldap
				_user = await Task.Run(() => {
					try {
						return _storage.GetLDAPUserByValidatingPassword(credential.DomainProfile, false, credential.UserName, credential.Password);
					}
					catch (NetSqlAzMan.SqlAzManException) {
						return null;
					}
					catch {
						throw;
					}
				});

				if (_user == null)
					return GetResponseMessageForForbidden(null, "Usuario y/o contraseña incorrecto(s).");

				return GetResponseMessageForOK(_user, "La credencial es válida.");
			}
		}
	}
}