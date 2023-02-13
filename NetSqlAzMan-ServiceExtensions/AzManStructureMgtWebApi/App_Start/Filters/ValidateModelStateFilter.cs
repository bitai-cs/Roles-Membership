using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace AzManStructureMgtWebApi.Filters
{
	public class ValidateModelStateFilter :ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext) {
			if (!actionContext.ModelState.IsValid) {
				var _exc = Controllers.BaseApiController.GetExceptionFromInvalidModelState(actionContext.ModelState);

				var _exceptionModel = (new NetSqlAzMan.ServiceBusinessObjects.Utilities.ObjectGenerator()).GetJsonResponseModelFromException("Se interceptó el error en  AzManStructureMgtWebApi.Handlers.GlobalExceptionHandler.Handle", _exc);

				var _jsonExceptionModel = JsonConvert.SerializeObject(_exceptionModel);

				//var _respMsg = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, _exc);
				//_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, "El modelo de datos no es válido.");
				var _respMsg = new HttpResponseMessage(HttpStatusCode.BadRequest) {
					Content = new StringContent(_jsonExceptionModel),
					ReasonPhrase = "El modelo de datos no es válido (" + this.GetType().FullName + ")."
				};

				//actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
				actionContext.Response = _respMsg;
			}
		}
	}
}