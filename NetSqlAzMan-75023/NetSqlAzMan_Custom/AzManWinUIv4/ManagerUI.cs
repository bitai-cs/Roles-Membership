using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using AzManWinUI.Properties;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Globalization;
using System.Net.Http;
using static AzManWinUI.Global;

namespace AzManWinUI
{
	public partial class ManagerUI :Form
	{
		#region Private fields
		private Exception _exce = null;
		private Settings _settings = Program.Settings;
		private List<KeyValuePair<string, string>> pvlistStoreAttributes = null;

		private string _webApiUri;
		#endregion

		#region Constructors
		public ManagerUI(string webApiUri) {
			InitializeComponent();

			_webApiUri = webApiUri;
				
			_settings.SettingsSaving += new System.Configuration.SettingsSavingEventHandler(_settings_SettingsSaving);

			if (!this.loadSettings(out _exce))
				throw _exce;

			this.tvieTree.ExceptionOnOperation += new BaseTreeView.SchemeTreeViewOperationExceptionEventHandler(tvieTree_ExceptionOnOperation);
		}
		#endregion

		#region Private methods
		private bool loadSettings(out Exception hex) {
			hex = null;

			try {
				//Language
				MultilanguageResource.SetCulture(MultilanguageResource.cultureName(_settings.ManagerUI_Culture));

				//General Settings
				this.tvieTree.ImageList = null;
				switch (_settings.ManagerUI_TreeNodeSize) {
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

				switch (_settings.ManagerUI_ListViewType) {
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
				pvlistStoreAttributes.Add(new KeyValuePair<string, string>(typeof(StructureViewEnum).Name, _settings.ManagerUI_StructureView.ToString()));
				pvlistStoreAttributes.Add(new KeyValuePair<string, string>(typeof(AuthorizationViewEnum).Name, _settings.ManagerUI_AuthorizationView.ToString()));

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}
		#endregion

		#region Public methods
		public bool InitializeUI(out Exception hex) {
			hex = null;

			try {
				this.Text = _webApiUri;

				var _rootNode = new Nodes.StorageNode(_webApiUri, this.CompanyName, this.ProductName, toolsMain, ctxmMenu, tvieTree, true, true, true, pvlistStoreAttributes);

				tvieTree.Nodes.Add(_rootNode);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}
		#endregion

		#region Event handlers
		private void tvieTree_ExceptionOnOperation(object sender, BaseTreeView.TreeViewOperationExceptionEventArgs e) {
			Program.ShowWarningMessage(this, this.Text, string.Format("Error en el nodo: {0}, acción: {1}", e.SelectedNode.Text, e.SourceEvent.ToString()));
			Program.ShowErrorMessage(this, this.Text, e.Error);
		}

		private void _settings_SettingsSaving(object sender, CancelEventArgs e) {
			if (!this.loadSettings(out _exce))
				throw _exce;
		}
		#endregion
	}
}