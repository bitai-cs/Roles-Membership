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
	public class BranchOfficeController :BaseApiController
	{
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>))]
		public async Task<IHttpActionResult> Get() {
			NetSqlAzMan.CustomBussinessLogic.BranchOfficeBusinessFactory _bf = new NetSqlAzMan.CustomBussinessLogic.BranchOfficeBusinessFactory();
			var _l = await _bf.GetAllAsync();

			return this.Ok(_l);
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.BranchOffice))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.BranchOffice bso) {
			HttpResponseMessage _respMsg;
			if (bso == null)
				return GetResponseMessageForInvalidModel(null);

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _bol = new NetSqlAzMan.CustomBussinessLogic.BranchOfficeBusinessFactory();
			var _created = await _bol.RegisterAsync(bso);

			Uri _locationUri = GetUriForRouteValues(new { id = _created.BranchOfficeId });
			_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _created);
			_respMsg.Headers.Location = _locationUri;

			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.BranchOffice))]
		public async Task<HttpResponseMessage> Put(string id, NetSqlAzMan.ServiceBusinessObjects.BranchOffice bso) {
			HttpResponseMessage _respMsg;
			try {
				if (bso == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(bso.BranchOfficeId))
					return GetResponseMessageForMismatchingIds();

				var _bol = new NetSqlAzMan.CustomBussinessLogic.BranchOfficeBusinessFactory();
				var _updated = await _bol.UpdateAsync(bso);

				_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _updated);
				_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del BranchOffice {0}: {1}", _updated.BranchOfficeId, _updated.BranchOfficeName));

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
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.BranchOffice))]
		public async Task<HttpResponseMessage> Delete(string id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.BranchOffice bso) {
			try {
				if (bso == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(bso.BranchOfficeId))
					return GetResponseMessageForMismatchingIds();

				var _bf = new NetSqlAzMan.CustomBussinessLogic.BranchOfficeBusinessFactory();
				var _deleted = await _bf.DeleteAsync(bso);

				var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _deleted);
				_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó los datos del BranchOffice {0}: {1}", _deleted.BranchOfficeId, _deleted.BranchOfficeName));

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
