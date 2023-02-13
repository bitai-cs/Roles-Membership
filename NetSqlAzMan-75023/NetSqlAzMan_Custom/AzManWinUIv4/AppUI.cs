using AzManWinUI.Properties;
using NetSqlAzMan.SnapIn.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzManWinUI {
	public partial class AppUI :Form {
		#region Private fields
		private Exception _exce = null;
		private SplashUI _splash = null;
		private Settings _settings = Program.Settings;
		#endregion

		#region Constructor
		public AppUI(ref SplashUI splash) {
			_splash = splash;

			InitializeComponent();

			this.Text = Basgosoft.Reflection.ClaimantAssembly.AssemblyTitle;

			_settings.SettingsSaving += new System.Configuration.SettingsSavingEventHandler(pvsettSettings_SettingsSaving);

			if (!this.loadSettings(out _exce))
				throw _exce;
		}
		#endregion

		#region Public methods
		public bool InitializeUI(out Exception hex) {
			hex = null;

			try {
				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool ShowPreferences(out Exception hex) {
			hex = null;

			try {
				SettingsUI formSettings = new SettingsUI();
				if (formSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool ShowChangePassword(out Exception hex) {
			hex = null;

			try {
				ChangePwdUI formChange = new ChangePwdUI();
				if (formChange.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}
		#endregion

		#region Private members

		private bool loadSettings(out Exception hex) {
			hex = null;

			try {
				//Language
				MultilanguageResource.SetCulture(MultilanguageResource.cultureName(_settings.ManagerUI_Culture));

				//UI
				this.tsmiConsole.Text = MultilanguageResource.GetString("ManagerUI.Console.Text");
				this.tsmiConsoleExit.Text = MultilanguageResource.GetString("ManagerUI.Console.Exit.Text");
				this.tsmiTools.Text = MultilanguageResource.GetString("ManagerUI.Tools.Text");
				this.tsmiToolsPreferences.Text = MultilanguageResource.GetString("SettingsUI.Text");

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Event handlers
		private void tsmiToolsPreferences_Click(object sender, EventArgs e) {
			if (!this.ShowPreferences(out _exce))
				throw _exce;
		}

		private void tsmiToolsChangePassword_Click(object sender, EventArgs e) {
			if (!this.ShowChangePassword(out _exce))
				throw _exce;
		}

		private void tsmiToolsOpenConfigFile_Click(object sender, EventArgs e) {
			string _msg;
			Configuration _config = ConfigurationManager.OpenExeConfiguration
			(ConfigurationUserLevel.None);
			Process _pr = new Process();
			try {
				_msg = string.Format("Se abrirá el archivo de configuración {0}\n\r\n\r¿Desea continuar?", _config.FilePath);
				if (MessageBox.Show(this, _msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
					return;
				_pr.StartInfo.FileName = _config.FilePath;
				_pr.Start();
			}
			catch (Exception ex) {
				_msg = string.Format("{0}\n\r\n\r{1}", "Error al abrir el archivo de configuración.", ex.Message);
				MessageBox.Show(this, _msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AppUI_Shown(object sender, EventArgs e) {
			if (_splash != null)
				_splash.Hide();
		}

		private void AppUI_Load(object sender, EventArgs e) {

		}

		private void pvsettSettings_SettingsSaving(object sender, CancelEventArgs e) {
			if (!this.loadSettings(out _exce))
				throw _exce;
		}
		#endregion

		private void tsmiConsoleExit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void dummyToolStripMenuItem_Click(object sender, EventArgs e) {
			ManagerUI _manUI = new AzManWinUI.ManagerUI("http://localhost/AzManStructureMgtWebApi/");
			//ManagerUI _manUI = new AzManWinUI.ManagerUI("http://www.lp.com.pe/amwan/");

			if (!_manUI.InitializeUI(out _exce))
				throw _exce;
			else {
				_manUI.MdiParent = this;
				_manUI.Show();
			}
		}
	}
}