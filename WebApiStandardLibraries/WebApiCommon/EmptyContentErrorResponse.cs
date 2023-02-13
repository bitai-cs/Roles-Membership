using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStandard.WebApiCommon
{
	public class EmptyContentErrorResponse :IErrorResponseContent
	{
		public string WebServer { get; set; }

		public string Date { get; set; }

		public string ToHtmlReport() {
			var _template = "<p><b>Server:</b>&nbsp{1}</p><p><b>Date:</b>&nbsp{2}</p>";
			var _report = string.Format(_template, System.Environment.NewLine, this.WebServer, this.Date);

			return _report;
		}

		public string ToStringReport() {
			var _template = "Server: {1}{0}Date: {2}{0}";
			var _report = string.Format(_template, System.Environment.NewLine, this.WebServer, this.Date);

			return _report;
		}
	}
}