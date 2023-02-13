using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
	public class HttpLdapWebApiRequestError
	{
		public enum ResponseModeltype
		{
			NoContent,
			TextHtml,
			Json
		}

		public class NoContentResponseModel
		{
			public string WebServer;
			public DateTime DateTime;
		}

		public class TextHtmlResponseModel
		{
			public string HtmlText;
		}

		public class JsonResponseModel
		{
			public string ErrorMessage;
		}

		public string RequestUri { get; set; }

		public HttpStatusCode StatusCode { get; set; }

		public string ReasonPhrase { get; set; }

		public ResponseModeltype ResponseModelType { get; set; }

		public NoContentResponseModel NoContentResponseModelObject { get; set; }

		public TextHtmlResponseModel TextHtmlResponseModelObject { get; set; }

		public JsonResponseModel JsonResponseModelObject { get; set; }
	}
}
