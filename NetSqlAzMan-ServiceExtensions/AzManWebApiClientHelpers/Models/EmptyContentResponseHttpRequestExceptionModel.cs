using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AzManWebApiClientHelpers.Models {
	public class EmptyContentResponseHttpRequestExceptionModel {
		[Display(Name = "Servidor Web")]
		public string WebServer { get; set; }
		[Display(Name = "Fecha")]
		public string Date { get; set; }

		public string ToStringReport() {
			var _template = "Server: {1}{0}Date: {2}{0}";
			var _report = string.Format(_template, System.Environment.NewLine, this.WebServer, this.Date);

			return _report;
		}
	}
}