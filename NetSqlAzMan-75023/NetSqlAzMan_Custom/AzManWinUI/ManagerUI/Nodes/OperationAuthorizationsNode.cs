using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI.Nodes
{
	public class OperationAuthorizationsNode : BaseNode
	{
		#region Private fields

		private IAzManApplication application;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;

		#endregion

		#region Constructor

		public OperationAuthorizationsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable)
		{
			this.application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons()
		{
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode()
		{
			this.Text = MultilanguageResource.GetString("Folder_Msg40");
			this.ImageKey = ImageIndexes.OperationAuthorizationsImgIdx;
			this.SelectedImageKey = ImageIndexes.OperationAuthorizationsImgIdx;
			this.Tag = this.application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit40");
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			IAzManItem[] items = this.application.GetItems(ItemType.Operation);
			foreach (IAzManItem item in items)
				listChildren.Add(new ItemAuthorizationNode(item, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		#endregion
	}
}