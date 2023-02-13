using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebApiStandard.WebApiCommon
{
	public class WebApiRequestException :Exception
	{
		public string RequestUri { get; }

		public HttpStatusCode StatusCode { get; }

		public ContenType ContentType { get; }

		public string ReasonPhrase { get; }

		public IErrorResponseContent ResponseContent { get; }

		public WebApiRequestException(string message, string requestUri, HttpStatusCode statusCode, ContenType contentType, string reasonPhrase, IErrorResponseContent relatedException) : base(message) {
			RequestUri = requestUri;
			StatusCode = statusCode;
			ContentType = contentType;
			ReasonPhrase = reasonPhrase;
			ResponseContent = relatedException;
		}
	}
}
