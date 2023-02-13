using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Forms;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes
{
	public class StoreGroupsNode :Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields
		private string _webApiUri;

		private NetSqlAzMan.ServiceBusinessObjects.AzManStore _store;
		private IAzManStore store;

		private ToolStripButton pvtsbtTb_NewGroup;
		private ToolStripButton pvtsbtTb_Import;
		private ToolStripButton pvtsbtTb_Refresh;

		private ToolStripButton pvtsitCt_NewGroup;
		private ToolStripButton pvtsitCt_Import;
		private ToolStripButton pvtsitCt_Refresh;

		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_NewGroup = "newGroup";
		public const string ActionButtonKey_Import = "import";

		#endregion

		#region Constructor
		public StoreGroupsNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManStore store, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = webApiUri;

			this._store = store;

			this.createNodeActionButtons();

			this.renderNode();
		}

		public StoreGroupsNode(IAzManStore store, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			this.store = store;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Internal Properties

		internal IAzManStore Store {
			get {
				return this.store;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("Folder_Msg90");
			this.ImageKey = ImageIndexes.StoreGroupsImgIdx;
			this.SelectedImageKey = ImageIndexes.StoreGroupsImgIdx;
			this.Tag = store;

			this.ListItemText = MultilanguageResource.GetString("Folder_Msg90");
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit90");

			if (!this._store.IAmManager) {
				this.getActionButton(ActionButtonKey_Import).Enable = false;
				this.getActionButton(ActionButtonKey_NewGroup).Enable = false;
			}
			else {
				this.getActionButton(ActionButtonKey_Import).Enable = true;
				this.getActionButton(ActionButtonKey_NewGroup).Enable = true;
			}
		}

		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg440");
			striButton2 = null;
			ab = new ActionButton(ActionButtonKey_NewGroup, striButton1, striButton2, new EventHandler(action_NewGroup_Click), out pvtsitCt_NewGroup, out pvtsbtTb_NewGroup, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg450");
			striButton2 = null;
			ab = new ActionButton(ActionButtonKey_Import, striButton1, striButton2, new EventHandler(action_Import_Click), out pvtsitCt_Import, out pvtsbtTb_Import, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsitCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup> _storeGroups = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
			var _return = Task.Run(() => _h.GetAllAsync(_store.Name, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_storeGroups = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion

			foreach (var _sg in _storeGroups) {
				_sg.Store = _store;
				listChildren.Add(new StoreGroupNode(_webApiUri, _sg, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
			}
		}

		#endregion

		#region Event handlers

		private void action_NewGroup_Click(object sender, EventArgs e) {
			frmNewStoreGroup frm = new frmNewStoreGroup(_webApiUri);
			frm._store = this._store;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK) {
				this.Nodes.Add(new StoreGroupNode(_webApiUri, frm._storeGroup, base.pttlstToolBar, base.ContextMenuStrip, base.pttvieTreeView, true, false, true));
			}
		}

		private void action_Import_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = "xml";
			openFileDialog.FileName = "NetSqlAzMan.xml";
			openFileDialog.Filter = "Xml files|*.xml|All files|*.*";
			openFileDialog.SupportMultiDottedExtensions = true;
			openFileDialog.Title = MultilanguageResource.GetString("ApplicationGroupsScopeNode_Msg10");
			DialogResult dr = openFileDialog.ShowDialog();
			if (dr == DialogResult.OK) {
				frmImportOptions frm = new frmImportOptions();
				frm.importIntoObject = this; //Revisar los procedimientos internos de esta propiedad
				frm.fileName = openFileDialog.FileName;
				frm.ShowDialog();

				this.Refresh();
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		#endregion
	}
}
