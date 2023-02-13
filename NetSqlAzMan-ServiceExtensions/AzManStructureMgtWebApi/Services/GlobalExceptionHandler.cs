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

namespace AzManStructureMgtWebApi.Services
{
	public class GlobalExceptionHandler :ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context) {
			var _statusCode = HttpStatusCode.NotImplemented;
			var _exception = context.Exception;
			var _typeWebApiExc = typeof(WebApiStandard.WebApiCommon.WebApiRequestException);
			var _typeNotFoundExc = typeof(WebApiStandard.WebApiCommon.NotFoundException);

			if (_typeNotFoundExc.Equals(_exception.GetType())) {
				_statusCode = HttpStatusCode.NotFound;
			}
			else
				_statusCode = HttpStatusCode.InternalServerError;

			var _e = new WebApiStandard.WebApiCommon.JsonContentErrorResponse(_exception);

			var _resp = new HttpResponseMessage(_statusCode);
			_resp.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(_e));

			//if (_typeWebApiExc.Equals(_exception.GetType())) {
			//	var _webApiExc = (WebApiStandard.WebApiCommon.WebApiRequestException)_exception;
			//}
			//else {
			//}

			context.Result = new ArgumentNullResult(context.Request, _resp);
		}

		public class ArgumentNullResult :IHttpActionResult
		{
			private HttpRequestMessage _request;
			private HttpResponseMessage _httpResponseMessage;


			public ArgumentNullResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage) {
				_request = request;
				_httpResponseMessage = httpResponseMessage;
			}

			public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
				return Task.FromResult(_httpResponseMessage);
			}
		}
	}
}