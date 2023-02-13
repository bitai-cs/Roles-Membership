using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStandard.WebApiCommon
{
	public class TextHtmlContentErrorResponse :IErrorResponseContent
	{
		private string _html;

		public TextHtmlContentErrorResponse(string html) {
			_html = html;
		}

		public string HtmlText { get { return _html; } }

		public string ToHtmlReport() {
			return HtmlText;
		}

		public string ToStringReport() {
			return HtmlText;
		}
	}
}