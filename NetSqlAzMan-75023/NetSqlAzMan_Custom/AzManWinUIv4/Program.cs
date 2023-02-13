using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Basgosoft.Reflection;
using NetSqlAzMan.SnapIn.Globalization;
using AzManWinUI.Properties;

namespace AzManWinUI
{
	public static class Program
	{
		internal static Settings Settings = new Settings();

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
			SplashUI _splash = new SplashUI();
			_splash.Show();
			Application.DoEvents();

			//ManagerUI formManager = new ManagerUI(ref formSplash);
			//if (formManager.InitializeUI(out exceOutput))
			//	Application.Run(formManager);
			AppUI _ui = new AzManWinUI.AppUI(ref _splash);
			if (_ui.InitializeUI(out exceOutput)) {
				Application.Run(_ui);
			}
			else {
				if (typeof(System.IO.DirectoryNotFoundException).Equals(exceOutput.GetType())) {
					string _folderPath = string.Format("{0}\\{1}\\{2}", Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), ClaimantAssembly.AssemblyCompany, ClaimantAssembly.AssemblyProduct);
					string _filePath = string.Format("{0}\\environment.config", _folderPath);

					try {
						System.IO.Directory.CreateDirectory(_folderPath);
						System.IO.File.WriteAllBytes(_filePath, NetSqlAzMan.SnapIn.Properties.Resources.environment_config);

						MessageBox.Show("Se ha creado por primera vez el archivo de configuración. Vuelva a iniciar la aplicación.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception exTop) {
						MessageBox.Show(String.Format("{0}\n\r{1}", "No se puede iniciar la aplicación debido al siguiente error.", exTop.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						MessageBox.Show(string.Format("A continuación se le proporcionaráa un archivo que deberá guardarlo en la siguiente carpeta y con el mismo nombre.{0}{1}{2}", Environment.NewLine, Environment.NewLine, _filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		internal static void ShowErrorMessage(IWin32Window owner, string title, Exception exce) {
			Exception _current = exce;
			string _message = null;
			string _newLine = Environment.NewLine;
			while (_current != null) {
				_message = string.Format("{1}{0}{0}{2}", _newLine, _current.Message, _current.StackTrace);

				MessageBox.Show(owner, _message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				_current = _current.InnerException;
			}
		}

		internal static void ShowErrorMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		internal static void ShowInfoMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		internal static void ShowWarningMessage(IWin32Window owner, string title, string message) {
			MessageBox.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		internal static DialogResult ShowQuestionMessage(IWin32Window owner, string title, string message) {
			return MessageBox.Show(owner, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			ShowErrorMessage(null, Application.ProductName, (Exception)e.ExceptionObject);
		}

		internal static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
			var _hex = e.Exception;
			var _excTypeName = _hex.GetType().Name;
			switch (_excTypeName) {
				case "HttpWebApiRequestException":
					var _requestExc = (AzManWinUI.HttpWebApiRequestException)_hex;

					Type _modelTypeName1 = typeof(Models.AppJsonResponseHttpRequestExceptionModel);
					Type _modelTypeName2 = typeof(Models.TextHtmlResponseHttpRequestExceptionModel);
					Type _modelTypeName3 = typeof(Models.EmptyContentResponseHttpRequestExceptionModel);

					if (_requestExc.RelatedException.GetType().Equals(_modelTypeName1)) {
						var _jsonReqExcModel = (AzManWinUI.Models.AppJsonResponseHttpRequestExceptionModel)_requestExc.RelatedException;
						do {
							var _report = "Mensaje: {0}" + Environment.NewLine + "URI: {1}" + Environment.NewLine + "Status Code: {2}" + Environment.NewLine + "Reason Phrase: {3}" + Environment.NewLine + Environment.NewLine + "{4}";

							_report = string.Format(_report, _requestExc.Message, _requestExc.RequestUri, ((int)_requestExc.StatusCode), _requestExc.ReasonPhrase, _jsonReqExcModel.ToStringReport(false, Global.Get_Config_AppSettings_Notifications_ShowExceptionStackTrace()));

							Program.ShowErrorMessage(null, Application.ProductName, _report);

							_jsonReqExcModel = _jsonReqExcModel.InnerException;
						} while (_jsonReqExcModel != null);
					}
					else if (_requestExc.RelatedException.GetType().Equals(_modelTypeName2)) {
						var _htmlReqExcModel = (AzManWinUI.Models.TextHtmlResponseHttpRequestExceptionModel)_requestExc.RelatedException;
						new HtmlRequestExceptionModelViewerUI(_requestExc, _htmlReqExcModel).ShowDialog();
					}
					else if (_requestExc.RelatedException.GetType().Equals(_modelTypeName3)) {
						var _emptyReqExcModel = (AzManWinUI.Models.EmptyContentResponseHttpRequestExceptionModel)_requestExc.RelatedException;

						var _report = "Mensaje: {0}" + Environment.NewLine + "URI: {1}" + Environment.NewLine + "Status Code: {2}" + Environment.NewLine + "Reason Phrase: {3}" + Environment.NewLine + Environment.NewLine + "{4}";

						_report = string.Format(_report, _requestExc.Message, _requestExc.RequestUri, ((int)_requestExc.StatusCode), _requestExc.ReasonPhrase, _emptyReqExcModel.ToStringReport());

						Program.ShowErrorMessage(null, Application.ProductName, _report);
					}
					else {
						throw new NotImplementedException("No se ha implementado el manejo del error para " + _excTypeName + " modelo tipo " + _requestExc.RelatedException.GetType().FullName);
					}

					break;

				default:
					Program.ShowErrorMessage(null, Application.ProductName, _hex);

					break;
			}
		}
	}
}