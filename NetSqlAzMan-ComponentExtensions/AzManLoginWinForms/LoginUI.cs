using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManLoginHelper;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManLoginWinForms
{
	public partial class LoginUI : Form
	{
		#region Private fields
		private string _clientApplication;
		private string _azMan_Store;
		private string _azMan_Application;
		private string _azMan_ItemName;
		#endregion

		#region Properties
		private DBUser _dbUser;
		public DBUser DBUser {
			get {
				return _dbUser;
			}
		}

		private LoginInfo _loginInfo;
		public LoginInfo LoginInfo {
			get {
				return _loginInfo;
			}
		}
		#endregion

		#region Constructors
		public LoginUI(string clientApplication, string azMan_Store, string azMan_Application, string azMan_ItemName, string userName, string password)
			: base() {
			InitializeComponent();

			_clientApplication = clientApplication;
			_azMan_Store = azMan_Store;
			_azMan_Application = azMan_Application;
			_azMan_ItemName = azMan_ItemName;

			txtbUser.Text = userName;
			txtbPassword.Text = password;
		}
		#endregion

		#region Event handlers
		private void LoginUI_Load(object sender, EventArgs e) {
#if DEBUG
			txtbUser.Text = "ADMCAMPUS";
			txtbPassword.Text = "v1k0b6";
#endif
		}

		private void butnLogin_Click(object sender, EventArgs e) {
			try {
				butnLogin.Enabled = false;
				txtbUser.Enabled = false;
				txtbPassword.Enabled = false;
				this.UseWaitCursor = true;

				LoginHelper.HelperCreateLoginCompleted += loginHelper_HelperCreateLoginCompleted;
				LoginHelper.CreateLoginAsynchronously(txtbUser.Text.ToLower(), txtbPassword.Text, _clientApplication, _azMan_Store, _azMan_Application, _azMan_ItemName);
			}
			catch (Exception ex) {
				MessageBox.Show(string.Format("{0}\n\r{1}", ex.Message, ex.StackTrace), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

				txtbUser.Enabled = true;
				txtbPassword.Enabled = true;
				butnLogin.Enabled = true;
				this.UseWaitCursor = false;
			}
		}

		private void loginHelper_HelperCreateLoginCompleted(object sender, HelperCreateLoginCompletedEventArgs e) {
			try {
				LoginHelper.HelperCreateLoginCompleted -= loginHelper_HelperCreateLoginCompleted;

				if (e.Cancelled) {
					MessageBox.Show(string.Format("{0}", "Operación cancelada."), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (e.Error != null)
					throw e.Error;

				if (!e.Response_CreateLoginResult) {
					MessageBox.Show(string.Format("{0}", e.Response_outputString), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {
					if (e.Response_authorization == AuthorizationType.Allow || e.Response_authorization == AuthorizationType.AllowWithDelegation) {
						_dbUser = e.Response_dbUser;
						_loginInfo = e.Response_login;

						this.DialogResult = System.Windows.Forms.DialogResult.OK;
					}
					else {
						MessageBox.Show(string.Format("No tiene acceso a la operación {0}.\n\rSolicite el acceso al administrador del sistema.", _azMan_ItemName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
			catch (Exception ex) {
				MessageBox.Show(string.Format("Error al iniciar sesión.\n\r{0}\n\r{1}", ex.Message, ex.StackTrace), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally {
				txtbUser.Enabled = true;
				txtbPassword.Enabled = true;
				butnLogin.Enabled = true;
				this.UseWaitCursor = false;
			}
		}
		#endregion

		private void pictConfig_Click(object sender, EventArgs e) {
			Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			Process _pr = new Process();
			try {
				_pr.StartInfo.FileName = _config.FilePath;
				_pr.Start();
			}
			catch (Exception ex) {
				MessageBox.Show(this, string.Format("{0}\n\r\n\r{1}", "Error al abrir el archivo de configuración.", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
