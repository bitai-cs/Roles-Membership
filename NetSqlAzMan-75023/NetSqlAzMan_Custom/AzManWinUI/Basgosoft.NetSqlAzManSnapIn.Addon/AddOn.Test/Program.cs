using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NetSqlAzManSnapIn.AddOn.Test
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Application.Run(new TestUI());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			MessageBox.Show(((Exception)e.ExceptionObject).Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
