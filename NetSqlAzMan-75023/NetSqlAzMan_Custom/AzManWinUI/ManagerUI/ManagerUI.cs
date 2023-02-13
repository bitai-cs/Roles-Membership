using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using AzManWinUI.Properties;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI
{
	public partial class ManagerUI : Form
	{
		#region Private fields

		private Exception pvexceError = null;
		private SplashUI pvformSplash = null;
		private Properties.Settings pvsettSettings = null;

		private List<KeyValuePair<string, string>> pvlistStoreAttributes = null;

		#endregion

		#region Constructors

		public ManagerUI(ref SplashUI splash) {
			pvformSplash = splash;

			InitializeComponent();

			this.Text = Basgosoft.Reflection.ClaimantAssembly.AssemblyTitle;

			pvsettSettings = new Settings();
			pvsettSettings.SettingsSaving += new System.Configuration.SettingsSavingEventHandler(pvsettSettings_SettingsSaving);

			if (!this.loadSettings(out pvexceError))
				throw pvexceError;

			this.tvieTree.ExceptionOnOperation += new BaseTreeView.SchemeTreeViewOperationExceptionEventHandler(tvieTree_ExceptionOnOperation);
		}

		#endregion

		#region Private members

		private bool loadSettings(out Exception hex) {
			hex = null;

			try {
				//Language
				MultilanguageResource.SetCulture(MultilanguageResource.cultureName(pvsettSettings.ManagerUI_Culture));

				//UI
				this.tsmiConsole.Text = MultilanguageResource.GetString("ManagerUI.Console.Text");
				this.tsmiConsoleExit.Text = MultilanguageResource.GetString("ManagerUI.Console.Exit.Text");
				this.tsmiTools.Text = MultilanguageResource.GetString("ManagerUI.Tools.Text");
				this.tsmiToolsPreferences.Text = MultilanguageResource.GetString("SettingsUI.Text");

				//General Settings
				this.tvieTree.ImageList = null;
				switch (pvsettSettings.ManagerUI_TreeNodeSize) {
					case TreeNodeSizeEnum.Small:
						this.tvieTree.ImageList = imgl16x16n;
						this.tvieTree.ItemHeight = 20;
						break;
					case TreeNodeSizeEnum.Large:
						this.tvieTree.ImageList = imgl24x24n;
						this.tvieTree.ItemHeight = 28;
						break;
				}
				this.tvieTree.Refresh();

				switch (pvsettSettings.ManagerUI_ListViewType) {
					case ListViewTypeEnum.Details:
						this.lvieNodeDetail.View = View.Details;
						break;

					case ListViewTypeEnum.LargeIcons:
						this.lvieNodeDetail.View = View.LargeIcon;
						break;

					case ListViewTypeEnum.List:
						this.lvieNodeDetail.View = View.List;
						break;

					case ListViewTypeEnum.SmallIcons:
						this.lvieNodeDetail.View = View.SmallIcon;
						break;

					case ListViewTypeEnum.Tile:
						this.lvieNodeDetail.View = View.Tile;
						break;
				}

				//View settings
				pvlistStoreAttributes = new List<KeyValuePair<string, string>>();
				pvlistStoreAttributes.Add(new KeyValuePair<string, string>(typeof(StructureViewEnum).Name, pvsettSettings.ManagerUI_StructureView.ToString()));
				pvlistStoreAttributes.Add(new KeyValuePair<string, string>(typeof(AuthorizationViewEnum).Name, pvsettSettings.ManagerUI_AuthorizationView.ToString()));

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Public members

		public bool InitializeUI(out Exception hex) {
			hex = null;

			try {
				Nodes.StorageNode nodeRoot = new Nodes.StorageNode(this.CompanyName, this.ProductName, toolsMain, ctxmMenu, tvieTree, true, true, true, pvlistStoreAttributes);

				tvieTree.Nodes.Add(nodeRoot);

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
				SettingsUI formSettings = new SettingsUI(ref pvsettSettings);
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

		#region Event handlers

		private void ManagerUI_Shown(object sender, EventArgs e) {
			if (pvformSplash != null)
				pvformSplash.Hide();
		}

		private void tvieTree_ExceptionOnOperation(object sender, BaseTreeView.TreeViewOperationExceptionEventArgs e) {
			MessageBox.Show(e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void pvsettSettings_SettingsSaving(object sender, CancelEventArgs e) {
			if (!this.loadSettings(out pvexceError))
				throw pvexceError;
		}

		private void tsmiToolsPreferences_Click(object sender, EventArgs e) {
			if (!this.ShowPreferences(out pvexceError))
				throw pvexceError;
		}

		private void salirToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void tsmiToolsChangePassword_Click(object sender, EventArgs e) {
			if (!this.ShowChangePassword(out pvexceError))
				throw pvexceError;
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

		#endregion
	}
}