using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSqlAzMan.CustomBussinessLogic.TestUI
{
	static class Program
	{
		internal const string Config_AppSettings_AzMan_CustomBusinessLogic_DbConnection = "AzMan_CustomBusinessLogic_DbConnection";

		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		[STAThread]
		static void Main() {
			NetSqlAzMan.CustomBussinessLogic.Global.AzManConnectionString = ConfigurationManager.AppSettings[Config_AppSettings_AzMan_CustomBusinessLogic_DbConnection];

			using (var _storage = new NetSqlAzMan.SqlAzManStorage(ConfigurationManager.AppSettings[Config_AppSettings_AzMan_CustomBusinessLogic_DbConnection])) {
				NetSqlAzMan.CustomBussinessLogic.Global.AzManStorage = _storage.GetAzManStorageDTO();
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += Application_ThreadException;
			Application.Run(new MainUI());
		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			ShowErrorMessage(null, Application.ProductName, e.Exception);
		}

		internal static void ShowErrorMessage(IWin32Window owner, string title, Exception exce) {
			var _typeWebApiExc = typeof(WebApiStandard.WebApiCommon.WebApiRequestException);

			if (exce.GetType().Equals(_typeWebApiExc)) {
				var _webApiExc = (WebApiStandard.WebApiCommon.WebApiRequestException)exce;

				var _message = string.Format("{0}\r\nStatus Code: {1}\r\nContent Type: {2}\r\nRequest URI: {3}\r\n\r\n{4}", _webApiExc.Message, _webApiExc.StatusCode.ToString(), _webApiExc.ContentType.ToString(), _webApiExc.RequestUri, _webApiExc.StackTrace);

				MessageBox.Show(owner, _message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

				if (_webApiExc.ContentType == WebApiStandard.WebApiCommon.ContenType.NoContent) {
					var _relatedExce = (WebApiStandard.WebApiCommon.EmptyContentErrorResponse)_webApiExc.ResponseContent;

					MessageBox.Show(owner, _relatedExce.ToStringReport(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (_webApiExc.ContentType == WebApiStandard.WebApiCommon.ContenType.TextHtml) {
					var _relatedExce = (WebApiStandard.WebApiCommon.TextHtmlContentErrorResponse)_webApiExc.ResponseContent;

					_message = _relatedExce.HtmlText;
					MessageBox.Show(owner, _message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (_webApiExc.ContentType == WebApiStandard.WebApiCommon.ContenType.AppJson) {
					var _relatedExce = (WebApiStandard.WebApiCommon.JsonContentErrorResponse)_webApiExc.ResponseContent;

					MessageBox.Show(owner, _relatedExce.ToStringReport(true, true), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else {
				Exception _current = exce;
				string _message = null;
				while (_current != null) {
					_message = string.Format("{0}\r\n{1}", _current.Message, _current.StackTrace);

					MessageBox.Show(owner, _message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					_current = _current.InnerException;
				}
			}
		}

		internal static void ShowInfoMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		internal static void ShowWarningMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
}
