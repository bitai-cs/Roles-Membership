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
	/// <summary>
	/// Controlador para el acceso al recurso Department
	/// </summary>
	public class DepartmentController :BaseApiController
	{
		/// <summary>
		/// Obtiene todos los Department existentes, ordenados y filtrados de acuerdo a los parametros porporcionados.
		/// </summary>
		/// <param name="sortOrderField">Nombre del campo por el que se ordenará la data.</param>
		/// <param name="ascendingOrder">True o False si se ordena la data de forma ascendente.</param>
		/// <param name="departmentNameFilter">Criterio de búsqueda que se aplica al nombre del Department</param>
		/// <returns></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.Department>))]
		public async Task<IHttpActionResult> Get(string sortOrderField = "DepartmentName", bool ascendingOrder = true, string departmentNameFilter = null) {
			NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bf = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();
			var _l = await _bf.GetAllAsync(sortOrderField, ascendingOrder, departmentNameFilter);

			return this.Ok(_l);
		}

		// GET: api/Department/5	
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.Department))]
		public async Task<IHttpActionResult> Get(int id) {
			NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bol = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();

			var _t = await Task.Run(() => _bol.GetByIdAsync(id));

			if (_t == null)
				return this.NotFound();
			else
				return this.Ok(_t);
		}

		// POST: api/Department
		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.Department))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.Department bso) {
			HttpResponseMessage _respMsg;

			if (bso == null)
				return GetResponseMessageForInvalidModel(null);

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bol = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();
			var _created = await _bol.InsertAsync(bso);

			_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _created);
			Uri _locationUri = GetUriForRouteValues(new { id = _created.DepartmentId });
			_respMsg.Headers.Location = _locationUri;

			return _respMsg;
		}

		// PUT: api/Department/5
		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.Department))]
		public async Task<HttpResponseMessage> Put(int id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.Department bso) {
			try {
				if (bso == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(bso.DepartmentId))
					return GetResponseMessageForMismatchingIds();

				var _bol = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();
				var _updated = await _bol.UpdateAsync(bso);

				var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, bso);
				_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del Departamento {0}: {1}", _updated.DepartmentId, _updated.DepartmentName));

				return _respMsg;
			}
			catch (DbUpdateConcurrencyException ex) {
				return GetResponseMessageForDbConcurrencyException(ex);
			}
			catch {
				throw;
			}
		}

		// DELETE: api/Department/5
		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.Department))]
		public async Task<HttpResponseMessage> Delete(int id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.Department bso) {
			try {
				if (bso == null)
					return GetResponseMessageForInvalidModel(null);

				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(bso.DepartmentId))
					return GetResponseMessageForMismatchingIds();

				var _bf = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();
				var _deleted = await _bf.DeleteAsync(bso);

				var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _deleted);
				_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó los datos del departamento {0}: {1}", _deleted.DepartmentId, _deleted.DepartmentName));

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