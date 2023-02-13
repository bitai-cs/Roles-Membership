using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class LdapWebApiDomainProfileController :BaseApiController
	{
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>))]
		public async Task<IHttpActionResult> GetAllLdapDomainProfileAsync() {
			var _bol = new NetSqlAzMan.CustomBussinessLogic.LdapWacDomainProfileBusinessFactory();
			var _t = Task.Run(() => _bol.GetAllLdapDomainProfilesAsync());

			return this.Ok(await _t);
		}


		[HttpPost]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>))]
		public async Task<HttpResponseMessage> RegisterAsync([FromBody] NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile newSbo) {
			try {
				if (newSbo == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				var _bl = new NetSqlAzMan.CustomBussinessLogic.LdapWacDomainProfileBusinessFactory();
				var _created = await _bl.RegisterAsync(newSbo);

				Uri _locationUri = GetUriForRouteValues(new { id = _created.DomainProfileId });
				var _respMsg = this.GetResponseMessageForOK(_created, string.Format("Se actualizó los datos del Perfil de Dominio {0}", _created.DomainProfile), _locationUri);

				return _respMsg;
			}
			catch (DbUpdateConcurrencyException ex) {
				return GetResponseMessageForDbConcurrencyException(ex);
			}
			catch {
				throw;
			}
		}


		[HttpPut]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>))]
		public async Task<HttpResponseMessage> UpdateAsync(byte id, [FromBody] NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile modified) {
			try {
				if (modified == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(modified.DomainProfileId))
					return GetResponseMessageForMismatchingIds();

				var _bl = new NetSqlAzMan.CustomBussinessLogic.LdapWacDomainProfileBusinessFactory();
				var _updated = await _bl.UpdateAsync(modified);

				var _respMsg = this.GetResponseMessageForOK(_updated, string.Format("Se actualizó los datos del Perfil de Dominio {0}", _updated.DomainProfile));

				return _respMsg;
			}
			catch (DbUpdateConcurrencyException ex) {
				return GetResponseMessageForDbConcurrencyException(ex);
			}
			catch {
				throw;
			}
		}

		[HttpDelete]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>))]
		public async Task<HttpResponseMessage> DeleteAsync(byte id, [FromBody] NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile dataToDelete) {
			try {
				if (dataToDelete == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(dataToDelete.DomainProfileId))
					return GetResponseMessageForMismatchingIds();

				var _bl = new NetSqlAzMan.CustomBussinessLogic.LdapWacDomainProfileBusinessFactory();
				var _deleted = await _bl.DeleteAsync(dataToDelete);

				var _respMsg = this.GetResponseMessageForOK(_deleted, string.Format("Se eliminó los datos del Perfil de Dominio {0}", _deleted.DomainProfile));

				return _respMsg;
			}
			catch (DbUpdateConcurrencyException ex) {
				return GetResponseMessageForDbConcurrencyException(ex);
			}
			catch {
				throw;
			}
		}
	}
}
