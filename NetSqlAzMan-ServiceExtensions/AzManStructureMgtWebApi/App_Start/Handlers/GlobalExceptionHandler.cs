using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AzManStructureMgtWebApi.Handlers
{
	public class GlobalExceptionHandler :ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context) {
			var _exceptionModel = (new NetSqlAzMan.ServiceBusinessObjects.Utilities.ObjectGenerator()).GetJsonResponseModelFromException("Se interceptó el error en  AzManStructureMgtWebApi.Handlers.GlobalExceptionHandler.Handle", context.Exception);

			var _jsonExceptionModel = JsonConvert.SerializeObject(_exceptionModel);

			var _respMsg = new HttpResponseMessage(HttpStatusCode.InternalServerError) {
				Content = new StringContent(_jsonExceptionModel, System.Text.Encoding.UTF8, "application/json"),
				ReasonPhrase = "AzManStructureMgtWebApi.Handlers.GlobalExceptionHandler.Handle: Error interno en la aplicación Web Api."
			};

			context.Result = new CustomErrorMessageResult(context.Request, _respMsg);
		}

		public class CustomErrorMessageResult :IHttpActionResult
		{
			private HttpRequestMessage _request;
			private readonly HttpResponseMessage _httpResponseMessage;

			public CustomErrorMessageResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage) {
				_request = request;
				_httpResponseMessage = httpResponseMessage;
			}

			public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
				return Task.FromResult(_httpResponseMessage);
			}
		}
	}
}