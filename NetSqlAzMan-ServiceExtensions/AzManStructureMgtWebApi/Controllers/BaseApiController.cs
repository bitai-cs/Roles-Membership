using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Net.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Filters;

namespace AzManStructureMgtWebApi.Controllers
{
	public class BaseApiController :ApiController
	{
		protected NetSqlAzMan.SqlAzManStorage _storage = null;

		public BaseApiController() : base() {
			_storage = new NetSqlAzMan.SqlAzManStorage(NetSqlAzMan.CustomBussinessLogic.Global.AzManConnectionString);
		}

		protected Uri GetUriForRouteValues(object route) {
			//Get location of new resource
			Uri _locationUri = new Uri(Url.Link("DefaultApi", route));
			return _locationUri;
		}

		internal static Exception GetExceptionFromInvalidModelState(System.Web.Http.ModelBinding.ModelStateDictionary invalidModelState) {
			if (invalidModelState == null)
				return new Exception("El modelo de datos no puede ser nulo.");

			if (invalidModelState.IsValid)
				throw new InvalidOperationException("El modelo es válido, no se puede crear una respuesta  error.");

			string _exceptions = null;
			foreach (var _value in invalidModelState.Values) {
				if (_value.Errors.Count > 0) {
					foreach (var _error in _value.Errors) {
						if (!string.IsNullOrEmpty(_error.ErrorMessage))
							_exceptions += _error.ErrorMessage + " | ";
						else {
							if (_error.Exception != null)
								_exceptions += _error.Exception.Message + " | ";
						}
					}
				}
			}

			return new Exception("Error(es) en el modelo de datos: " + _exceptions.TrimEnd().Substring(0, _exceptions.TrimEnd().Length - 1));
		}

		protected HttpResponseMessage GetResponseMessageForForbidden(object data, string headerMessage) {
			var _resp = this.Request.CreateResponse(HttpStatusCode.Forbidden, data);
			_resp.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, headerMessage);

			return _resp;
		}

		protected HttpResponseMessage GetResponseMessageForOK(object data, string headerMessage) {
			var _resp = this.Request.CreateResponse(HttpStatusCode.OK, data);
			_resp.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, headerMessage);

			return _resp;
		}

		protected HttpResponseMessage GetResponseMessageForOK(object data, string headerMessage, Uri location) {
			var _resp = GetResponseMessageForOK(data, headerMessage);
			_resp.Headers.Location = location;

			return _resp;
		}

		protected HttpResponseMessage GetResponseMessageForNotFoundId(string id) {
			return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception(string.Format("No se encuentra el recurso con el id: {0}", id)));
		}

		protected HttpResponseMessage GetResponseMessageForInvalidModel(System.Web.Http.ModelBinding.ModelStateDictionary invalidModelState) {
			var _exc = BaseApiController.GetExceptionFromInvalidModelState(invalidModelState);

			var _respMsg = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, _exc);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, "El modelo de datos no es válido.");

			return _respMsg;
		}

		protected HttpResponseMessage GetResponseMessageForMismatchingIds() {
			return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new InvalidOperationException("El id no corresponde con el id del modelo proporcionado."));
		}

		protected HttpResponseMessage GetResponseMessageForDbConcurrencyException(DbUpdateConcurrencyException ex) {
			return this.Request.CreateErrorResponse(HttpStatusCode.Conflict, ex);
		}

		//[Obsolete("Ahora los errores generales y no controlados lo maneja un filtro global.")]
		//protected HttpResponseMessage GetResponseMessageForGeneralException(Exception ex) {
		//	//Crea una respuesta con el error serializado
		//	return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
		//}
	}
}