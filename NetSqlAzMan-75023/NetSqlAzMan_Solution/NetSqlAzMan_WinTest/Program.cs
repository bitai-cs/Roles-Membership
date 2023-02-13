using System;
using System.Configuration;
using System.Windows.Forms;

namespace NetSqlAzMan_WinTest {
	static class Program {
		internal static string CONFIG_ConnectionString;

		/// <summary>
		/// The main entry point for the store.
		/// </summary>
		[STAThread]
		static void Main() {
			CONFIG_ConnectionString = ConfigurationManager.ConnectionStrings["AzManDB"].ConnectionString;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}