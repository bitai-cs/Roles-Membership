using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AzManWebApiClientHelpers.Models {
	public class AppJsonResponseHttpRequestExceptionModel {
		[Display(Name = "Mensaje")]
		public string Message { get; set; }

		[Display(Name = "Mensaje de la Excepción")]
		public string ExceptionMessage { get; set; }

		[Display(Name = "Tipo de Excepción")]
		public string ExceptionType { get; set; }

		[Display(Name = "Rastro de la Pila")]
		public string StackTrace { get; set; }

		[Display(Name = "Excepción Interna")]
		public AppJsonResponseHttpRequestExceptionModel InnerException { get; set; }

		public string ToStringReport(bool includeInnerExceptions, bool includeStackTrace) {
			var _template = "{3}Message: {0}\n\r{3}Exception Message: {1}\n\r{3}Exception Type: {2}\n\r" + (includeStackTrace ? "{3}Stack Trace: {4}" : string.Empty) + "\n\r";

			string _report;
			if (includeStackTrace)
				_report = string.Format(_template, this.Message, this.ExceptionMessage, this.ExceptionType, string.Empty, this.StackTrace);
			else
				_report = string.Format(_template, this.Message, this.ExceptionMessage, this.ExceptionType, string.Empty);

			if (includeInnerExceptions) {
				var _innerException = this.InnerException;
				var _tabCount = 0;
				while (_innerException != null) {
					_report += new string('\t', _tabCount) + "Inner Exception:\n\r\n\r";

					_tabCount++;
					if (includeStackTrace)
						_report += string.Format(_template, _innerException.Message, _innerException.ExceptionMessage, _innerException.ExceptionType, new string('\t', _tabCount), _innerException.StackTrace);
					else
						_report += string.Format(_template, _innerException.Message, _innerException.ExceptionMessage, _innerException.ExceptionType, new string('\t', _tabCount));

					_innerException = _innerException.InnerException;
				}
			}
			else {
				if (this.InnerException != null)
					_report += "Inner Exception: Si, existe error anidado.";
			}

			return _report;
		}
	}
}