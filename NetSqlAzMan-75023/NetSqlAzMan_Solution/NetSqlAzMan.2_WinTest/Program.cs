using System;
using System.Configuration;
using System.Windows.Forms;

namespace NetSqlAzMan_WinTest {
	static class Program {
		internal static string CONFIG_ConnectionString;
		internal static NetSqlAzMan.SqlAzManStorage Storage;

		/// <summary>
		/// The main entry point for the store.
		/// </summary>
		[STAThread]
		static void Main() {
			CONFIG_ConnectionString = ConfigurationManager.ConnectionStrings["AzManDB"].ConnectionString;
						
			NetSqlAzMan.CustomBussinessLogic.Global.AzManConnectionString = CONFIG_ConnectionString;

			Storage = new NetSqlAzMan.SqlAzManStorage(CONFIG_ConnectionString);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}