using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzManWinUI {
	public partial class HtmlRequestExceptionModelViewerUI : Form {
		public HtmlRequestExceptionModelViewerUI() {
			InitializeComponent();
		}

		public HtmlRequestExceptionModelViewerUI(HttpWebApiRequestException requestException, Models.TextHtmlResponseHttpRequestExceptionModel relatedModel) : this() {
			txtbMessage.Text = requestException.Message;
			txtbURI.Text = requestException.RequestUri;
			txtbStatusCode.Text = ((int)requestException.StatusCode).ToString();
			txtbReasonPhrase.Text = requestException.ReasonPhrase;
			HTMLViewer.DocumentText = relatedModel.HTMLText;
		}

		private void HtmlRequestExceptionModelViewerUI_Load(object sender, EventArgs e) {

		}
	}
}
