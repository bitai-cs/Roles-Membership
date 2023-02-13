using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI.Nodes
{
	public class ItemAuthorizationsNode : BaseNode
	{
		#region Private fields

		private IAzManApplication application;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;

		#endregion

		#region Constructor

		public ItemAuthorizationsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			this.application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("Folder_Msg20");
			this.ImageKey = ImageIndexes.AuthorizationsImgIdx;
			this.SelectedImageKey = ImageIndexes.AuthorizationsImgIdx;
			this.Tag = this.application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit20");
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			AuthorizationViewEnum enumAuthorizationView =  (AuthorizationViewEnum)Enum.Parse(typeof(AuthorizationViewEnum), this.application.Store.Attributes[typeof(AuthorizationViewEnum).Name].Value, true);

			if (enumAuthorizationView == AuthorizationViewEnum.Role || enumAuthorizationView == AuthorizationViewEnum.RoleTask)
				listChildren.Add(new RoleAuthorizationsNode(this.application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

			if (enumAuthorizationView == AuthorizationViewEnum.RoleTask)
				listChildren.Add(new TaskAuthorizationsNode(this.application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

			//Operation Definitions visibile only in Developer Mode.
			if (this.application.Store.Storage.Mode != NetSqlAzManMode.Administrator)
				listChildren.Add(new OperationAuthorizationsNode(this.application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		#endregion
	}
}