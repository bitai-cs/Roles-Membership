using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;

namespace AzManWinUI.Nodes
{
	public class ApplicationGroupsNode : BaseNode
	{
		#region Private fields

		private IAzManApplication application;

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

		#region Constructors

		public ApplicationGroupsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			this.application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Internal Properties

		internal IAzManApplication Application {
			get {
				return this.application;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg210");
			striButton2 = MultilanguageResource.GetString("Menu_Tit210");
			ab = new ActionButton(ActionButtonKey_NewGroup, striButton1, striButton2, new EventHandler(action_NewGroup_Click), out pvtsitCt_NewGroup, out pvtsbtTb_NewGroup, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg220");
			striButton2 = MultilanguageResource.GetString("Menu_Tit220");
			ab = new ActionButton(ActionButtonKey_Import, striButton1, striButton2, new EventHandler(action_Import_Click), out pvtsitCt_Import, out pvtsbtTb_Import, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsitCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("ListView_Msg20");
			this.ImageKey = ImageIndexes.ApplicationGroupsImgIdx;
			this.SelectedImageKey = ImageIndexes.ApplicationGroupsImgIdx;
			this.Tag = this.application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Msg10");

			if (!this.application.IAmManager) {
				this.getActionButton(ActionButtonKey_NewGroup).Enable = false;
				this.getActionButton(ActionButtonKey_Import).Enable = false;
			}
			else {
				this.getActionButton(ActionButtonKey_NewGroup).Enable = true;
				this.getActionButton(ActionButtonKey_Import).Enable = true;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			IAzManApplicationGroup[] applicationGroups = this.application.GetApplicationGroups();
			foreach (IAzManApplicationGroup group in applicationGroups)
				listChildren.Add(new ApplicationGroupNode(group, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
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
				frm.importIntoObject = this;
				frm.fileName = openFileDialog.FileName;
				frm.ShowDialog();

				this.Refresh();
			}
		}

		private void action_NewGroup_Click(object sender, EventArgs e) {
			frmNewApplicationGroup frm = new frmNewApplicationGroup();
			frm.application = this.application;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK) {
				//this.Refresh();
				//this.Children.Add(new ApplicationGroupScopeNode(frm.applicationGroup));
				this.Nodes.Add(new ApplicationGroupNode(frm.applicationGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
			}
		}

		#endregion
	}
}
