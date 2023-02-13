using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzManWinUI {
	public class HttpWebApiRequestException : Exception {
		public string RequestUri { get; }

		public HttpStatusCode StatusCode { get; }

		public string ReasonPhrase { get; }

		public object RelatedException { get; }

		public HttpWebApiRequestException(string message, string requestUri, HttpStatusCode statusCode, string reasonPhrase, object relatedException) : base(message) {
			RequestUri = requestUri;
			StatusCode = statusCode;
			ReasonPhrase = reasonPhrase;
			RelatedException = relatedException;
		}
	}
}
