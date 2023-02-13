using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AzManWebApiClientHelpers.Models {
	public class TextHtmlResponseHttpRequestExceptionModel {
		private string _html;

		internal TextHtmlResponseHttpRequestExceptionModel(string html) {
			_html = html;
		}

		[Display(Name = "Text/HTML")]
		public string HTMLText { get { return _html; } }
	}
}