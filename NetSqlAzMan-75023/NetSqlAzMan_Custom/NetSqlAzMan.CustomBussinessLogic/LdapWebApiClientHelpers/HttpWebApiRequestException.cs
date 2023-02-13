using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic.LdapWebApiClientHelpers
{
	public class HttpWebApiRequestException :Exception
	{
		public string RequestUri { get; }

		public HttpStatusCode StatusCode { get; }

		public ContenTypeEnum ContentType { get; }

		public string ReasonPhrase { get; }

		public object ResponseContent { get; }

		public HttpWebApiRequestException(string message, string requestUri, HttpStatusCode statusCode, ContenTypeEnum contentType, string reasonPhrase, object relatedException) : base(message) {
			RequestUri = requestUri;
			StatusCode = statusCode;
			ContentType = contentType;
			ReasonPhrase = reasonPhrase;
			ResponseContent = relatedException;
		}
	}
}
