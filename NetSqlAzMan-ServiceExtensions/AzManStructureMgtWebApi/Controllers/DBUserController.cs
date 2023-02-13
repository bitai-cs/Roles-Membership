using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	/// <summary>
	/// Controlador de la Vista de los datos de un DBUser
	/// </summary>	
	public class DBUserController :BaseApiController
	{
		// GET: api/DBUser
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.DBUser>))]
		public async Task<IHttpActionResult> Get(string sortOrderField, bool ascendingOrder, string userFilter, string nameFilter, Nullable<bool> enabledFilter) {
			NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory _bol = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();
			var _t = Task.Run(() => _bol.GetAllAsync(sortOrderField, ascendingOrder, userFilter, nameFilter, enabledFilter));

			return this.Ok(await _t);
		}

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.DBUser))]
		public async Task<IHttpActionResult> Get(int id) {
			NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory _bf = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();

			NetSqlAzMan.ServiceBusinessObjects.DBUser _user = await Task.Run(() => _bf.GetByIdAsync(id));

			if (_user == null)
				return this.NotFound();
			else
				return this.Ok(_user);
		}

		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.DBUser))]
		public async Task<HttpResponseMessage> Post(NetSqlAzMan.ServiceBusinessObjects.DBUser businessServiceObject) {
			HttpResponseMessage _respMsg;

			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _f = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();
			var _created = await _f.RegisterAsync(businessServiceObject);

			//Get location of new resource
			Uri _locationUri = GetUriForRouteValues(new { id = _created.UserID });
			_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _created);
			_respMsg.Headers.Location = _locationUri;

			return _respMsg;
		}

		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.DBUser))]
		public async Task<HttpResponseMessage> Put(int id, [FromBody]NetSqlAzMan.ServiceBusinessObjects.DBUser businessServiceObject) {
			try {
				if (!this.ModelState.IsValid)
					return GetResponseMessageForInvalidModel(this.ModelState);

				if (!id.Equals(businessServiceObject.UserID))
					return GetResponseMessageForMismatchingIds();

				var _bol = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();

				HttpResponseMessage _respMsg;
				NetSqlAzMan.ServiceBusinessObjects.DBUser _updated;
				if (string.IsNullOrEmpty(businessServiceObject.PasswordConfirmation)) {
					_updated = await _bol.UpdateAsync(businessServiceObject);

					_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _updated);
					_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del Usuario {0}: {1}", _updated.UserID, _updated.UserName));
				}
				else {
					_updated = await _bol.ChangePasswordAsync(businessServiceObject);

					_respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _updated);
					_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se cambió la contraseña del Usuario {0}: {1}", _updated.UserID, _updated.UserName));
				}

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
