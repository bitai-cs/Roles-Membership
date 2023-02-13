using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Basgosoft.Reflection;

namespace AzManWinUI
{
	public static class Program
	{
		public const string HASHPROVIDER = "Shaman";
		public const string PWDKEYNAME = "pwd";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			#region System.Windows.Forms.Application settings
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			#endregion

			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Exception exceOutput;

			#region Login request
#if !DEBUG
			LoginUI formLogin = new LoginUI();
			if (formLogin.ShowDialog() != DialogResult.OK) {
				MessageBox.Show("Debe de ingresar la contraseña para poder ingresar al módulo.", Basgosoft.Reflection.ClaimantAssembly.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
#endif
			#endregion

			#region Start main window
			SplashUI formSplash = new SplashUI();
			formSplash.Show();
			System.Windows.Forms.Application.DoEvents();
			//System.Threading.Thread.Sleep(2000);

			ManagerUI formManager = new ManagerUI(ref formSplash);
			if (formManager.InitializeUI(out exceOutput))
				Application.Run(formManager);
			else {
				if (typeof(System.IO.DirectoryNotFoundException).Equals(exceOutput.GetType()) || typeof(System.IO.FileNotFoundException).Equals(exceOutput.GetType())) {
					string _folderPath = string.Format("{0}\\{1}\\{2}", Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), ClaimantAssembly.AssemblyCompany, ClaimantAssembly.AssemblyProduct);
					string _filePath = string.Format("{0}\\environment.config", _folderPath);

					try {
						System.IO.Directory.CreateDirectory(_folderPath);
						System.IO.File.WriteAllBytes(_filePath, NetSqlAzMan.SnapIn.Properties.Resources.environment_config);

						MessageBox.Show("Se ha creado por primera vez el archivo de configuración. Vuelva a iniciar la aplicación.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception exTop) {
						MessageBox.Show(String.Format("{0}\n\r{1}", "No se puede iniciar la aplicación debido al siguiente error.", exceOutput.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						MessageBox.Show(string.Format("A continuación se le proporcionaráa unarchivo que deberá guardarlo en la siguiente carpeta y con el mismo nombre.{0}{1}{2}", Environment.NewLine, Environment.NewLine, _filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						using (SaveFileDialog _sf = new SaveFileDialog()) {
							_sf.Title = "Archivo de configuración de la aplicación";
							_sf.Filter = "Application Config (*.config)|*.config";

							if (_sf.ShowDialog() != DialogResult.OK)
								return;

							try {
								System.IO.File.WriteAllBytes(_sf.FileName, NetSqlAzMan.SnapIn.Properties.Resources.environment_config);
								MessageBox.Show("Se guardo correctamente el archivo de configuración", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							catch (Exception ex) {
								MessageBox.Show(string.Format("Error al guardar el archivo.\n\r\n\r{0}\n\r\n\r{1}", ex.Message, ex.StackTrace), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}
				else
					throw exceOutput;
			}
			#endregion
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			string striMessage = "Error en el dominio de la aplicación.\n\r\n\r{0}";
			striMessage = String.Format(striMessage, ((Exception)e.ExceptionObject).Message);

			MessageBox.Show(striMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			string striMessage = "Error inesperado en la aplicación.\n\r\n\r{0}";
			striMessage = String.Format(striMessage, e.Exception.Message);

			MessageBox.Show(striMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}