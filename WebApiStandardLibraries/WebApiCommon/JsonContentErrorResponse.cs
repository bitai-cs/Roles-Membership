using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiStandard.WebApiCommon
{
	public class JsonContentErrorResponse :IErrorResponseContent
	{
		public string ErrorType;
		public string ErrorDescription;
		public string Source;
		public string StackTrace;
		public IEnumerable<string> ErrorDetail;
		public JsonContentErrorResponse InnerErrorSummary;

		/// <summary>
		/// Constructor por defecto y/o para el Deserializador JSON
		/// </summary>
		public JsonContentErrorResponse() {
			ErrorDetail = new List<string>();
		}

		public JsonContentErrorResponse(Exception ex) : this() {
			ErrorType = ex.GetType().FullName;
			ErrorDescription = ex.Message;
			Source = ex.Source;	
			StackTrace = ex.StackTrace;

			var _detail = new List<string>();
			foreach (var _key in ex.Data.Keys) {
				_detail.Add(string.Format("{0}: {1}", _key.ToString(), ex.Data[_key].ToString()));
			}
			ErrorDetail = _detail;

			if (ex.InnerException == null)
				return;

			if (ex.InnerException != null)
				InnerErrorSummary = new JsonContentErrorResponse(ex.InnerException);
		}

		public override string ToString() {
			return string.Format("{0}: {1}", this.ErrorType, this.ErrorDescription);
		}

		public string ToStringReport(bool includeStackTrace, bool includeInnerErrors) {
			var _template = "{0}Description: {1}: {2}\r\n{0}Source: {3}\r\n{0}Details: {4}\r\n" + (includeStackTrace ? "{0}Stack Trace: {5}\r\n" : string.Empty);

			var _details = string.Empty;
			foreach (var _i in this.ErrorDetail) {
				_details += _i + " | ";
			}
			if (this.ErrorDetail.Count() > 0)
				_details = _details.Substring(0, _details.Length - 3);

			string _report;
			if (includeStackTrace)
				_report = string.Format(_template, string.Empty, this.ErrorType, this.ErrorDescription, this.Source, _details, this.StackTrace);
			else
				_report = string.Format(_template, string.Empty, this.ErrorType, this.ErrorDescription, this.Source, _details);

			if (includeInnerErrors) {
				var _innerError = this.InnerErrorSummary;
				var _tabCount = 0;
				while (_innerError != null) {
					_report += new string('\t', _tabCount) + "Inner Exception:\n\r\n\r";

					_tabCount++;

					_details = string.Empty;
					foreach (var _i in _innerError.ErrorDetail) {
						_details += _i + " | ";
					}
					if (_innerError.ErrorDetail.Count() > 0)
						_details = _details.Substring(0, _details.Length - 3);

					if (includeStackTrace)
						_report += string.Format(_template, new string('\t', _tabCount), _innerError.ErrorType, _innerError.ErrorDescription, _innerError.Source, _details, _innerError.StackTrace);
					else
						_report += string.Format(_template, new string('\t', _tabCount), _innerError.ErrorType, _innerError.ErrorDescription, _innerError.Source, _details);

					_innerError = _innerError.InnerErrorSummary;
				}
			}
			else {
				if (this.InnerErrorSummary != null)
					_report += "Inner Exception: Si, existe error anidado.";
			}

			return _report;
		}

		public string ToHtmlReport() {
			throw new NotImplementedException();
		}

		public string ToStringReport() {
			return ToStringReport(true, true);
		}
	}
}
