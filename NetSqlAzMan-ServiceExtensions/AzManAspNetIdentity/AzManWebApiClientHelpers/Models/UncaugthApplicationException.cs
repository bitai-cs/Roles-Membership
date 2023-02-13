using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AzManWinUI.Models {
	public class UncaugthApplicationException {
		[Display(Name = "Mensaje de Error")]
		public string Message { get; set; }
		[Display(Name = "Tipo de Error")]
		public string ExceptionType { get; set; }
		[Display(Name = "Rastro de Pila de llamadas")]
		public string StackTrace { get; set; }
		[Display(Name = "Origen")]
		public string Source { get; set; }
		[Display(Name = "Error Interno")]
		public UncaugthApplicationException InnerException { get; set; }

		public string ToStringReport() {
			var _template = "Message: {0}\n\rException Type: {1}\n\rStack Trace: {2}\n\rSource: {3}\n\r";

			var _report = string.Format(_template, this.Message, this.ExceptionType, this.StackTrace, this.Source);

			var _innerException = this.InnerException;
			while (_innerException != null) {
				_report += "Inner Exception:\n\r\n\r" + string.Format(_template, _innerException.Message, _innerException.ExceptionType, _innerException.StackTrace, _innerException.Source);

				_innerException = _innerException.InnerException;
			}

			return _report;
		}
	}
}