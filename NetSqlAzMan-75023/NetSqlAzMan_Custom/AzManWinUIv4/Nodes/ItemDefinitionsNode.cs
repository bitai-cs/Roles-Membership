using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Forms;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI.Nodes {
	public class ItemDefinitionsNode : BaseNode {
		#region Private fields
		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManApplication _application;

		private ToolStripButton pvtsbtTb_Import;
		private ToolStripButton pvtsitCt_Import;

		private ToolStripButton pvtsbtTb_Export;
		private ToolStripButton pvtsitCt_Export;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsitCt_Refresh;
		#endregion

		#region Public Constants Field
		public const string ActionButtonKey_Import = "import";
		#endregion

		#region Constructor
		//public ItemDefinitionsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
		//	this.application = application;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}
		public ItemDefinitionsNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			_webApiUri = webApiUri;

			this._application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Internal properties
		internal NetSqlAzMan.ServiceBusinessObjects.AzManApplication Application {
			get {
				return this._application;
			}
		}
		#endregion

		#region Protected Overriden members
		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg300");
			striButton2 = MultilanguageResource.GetString("Menu_Tit300");
			ab = new ActionButton(ActionButtonKey_Import, striButton1, striButton2, new EventHandler(action_Import_Click), out pvtsitCt_Import, out pvtsbtTb_Import, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg310");
			striButton2 = MultilanguageResource.GetString("Menu_Tit310");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Export_Click), out pvtsitCt_Export, out pvtsbtTb_Export, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsitCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("Folder_Msg30");
			this.ImageKey = ImageIndexes.ItemsImgIdx;
			this.SelectedImageKey = ImageIndexes.ItemsImgIdx;
			this.Tag = this._application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit30");

			if (!this._application.IAmManager) {
				this.getActionButton(ActionButtonKey_Import).Enable = false;
			}
			else {
				this.getActionButton(ActionButtonKey_Import).Enable = true;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			var enumStructureView = (StructureViewEnum)Enum.Parse(typeof(StructureViewEnum), this._application.Store.Attributes.Where(s => s.Key.Equals(typeof(StructureViewEnum).Name)).First().Value, true);

			if (enumStructureView == StructureViewEnum.Role || enumStructureView == StructureViewEnum.RoleTask)
				listChildren.Add(new RoleDefinitionsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

			if (enumStructureView == StructureViewEnum.RoleTask)
				listChildren.Add(new TaskDefinitionsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

			//Operation Definitions visibile only in Developer Mode.
			if (this._application.Store.Storage.Mode != NetSqlAzMan.ServiceBusinessObjects.AzManMode.Administrator)
				listChildren.Add(new OperationDefinitionsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		private void action_Export_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring!");

			//frmExportOptions frm = new frmExportOptions();
			//DialogResult dr = frm.ShowDialog();
			//if (dr == DialogResult.OK) {
			//	frmExport frmwait = new frmExport();
			//	IAzManItem[] itemToExport = this.application.GetItems();
			//	IAzManExport[] toExport = new IAzManExport[itemToExport.Length];
			//	for (int i = 0; i < itemToExport.Length; i++) {
			//		toExport[i] = itemToExport[i];
			//	}
			//	frmwait.ShowDialog(null, frm.fileName, toExport, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.application.Store.Storage);
			//}
		}

		private void action_Import_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring!");

			//OpenFileDialog openFileDialog = new OpenFileDialog();
			//openFileDialog.DefaultExt = "xml";
			//openFileDialog.FileName = "NetSqlAzMan.xml";
			//openFileDialog.Filter = "Xml files|*.xml|All files|*.*";
			//openFileDialog.SupportMultiDottedExtensions = true;
			//openFileDialog.Title = MultilanguageResource.GetString("ApplicationGroupsScopeNode_Msg10");
			//DialogResult dr = openFileDialog.ShowDialog();
			//if (dr == DialogResult.OK) {
			//	frmImportOptions frm = new frmImportOptions();
			//	frm.importIntoObject = this;
			//	frm.fileName = openFileDialog.FileName;
			//	frm.ShowDialog();

			//	((ApplicationNode)this.Parent).Refresh(this.ChildListView);
			//	//((ApplicationScopeNode)this.Parent).internalRender();
			//}
		}
		#endregion
	}
}
