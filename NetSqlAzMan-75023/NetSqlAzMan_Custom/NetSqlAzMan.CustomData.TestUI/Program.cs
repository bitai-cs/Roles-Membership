using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace NetSqlAzMan.CustomDataLayer.TestUI {
	static class Program {
		internal const string Config_AppSettings_AzManConnection = "AzManConnection";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			//Asignar la cadena de conexión
			CustomDataLayer.Global.AzManConnectionString = ConfigurationManager.AppSettings[Program.Config_AppSettings_AzManConnection];

			if (MessageBox.Show("Desea continuar con el test para EF? Si:EF o NO:LinqToClasses", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Application.Run(new EFCFTestUI());
			else
				Application.Run(new LINQTestUI());
		}

		public static void ShowErrorMessage(IWin32Window owner, string title, Exception exce) {
			Exception _current = exce;
			string _message = null;
			while (_current != null) {
				_message = string.Format("{0}\n\r{1}", _current.Message, _current.StackTrace);

				MessageBox.Show(owner, _message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				_current = _current.InnerException;
			}
		}

		public static void ShowInfoMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void ShowWarningMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			ShowErrorMessage(null, Application.ProductName, (Exception)e.ExceptionObject);
		}

		internal static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			ShowErrorMessage(null, Application.ProductName, e.Exception);
		}
	}
}
