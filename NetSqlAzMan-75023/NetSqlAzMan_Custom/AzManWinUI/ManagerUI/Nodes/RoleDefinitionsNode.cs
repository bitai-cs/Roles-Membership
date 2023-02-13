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
	public class RoleDefinitionsNode : BaseNode
	{
		#region Private fields

		private IAzManApplication application;

		private ToolStripButton pvtsbtCt_New;
		private ToolStripButton pvtsbtTb_New;

		private ToolStripButton pvtsbtCt_Refresh;
		private ToolStripButton pvtsbtTb_Refresh;

		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_NewRole = "newRole";

		#endregion

		#region Constructor

		public RoleDefinitionsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
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

			striButton1 = MultilanguageResource.GetString("Menu_Msg330");
			striButton2 = MultilanguageResource.GetString("Menu_Tit330");
			ab = new ActionButton(ActionButtonKey_NewRole, striButton1, striButton2, new EventHandler(action_New_Click), out pvtsbtCt_New, out pvtsbtTb_New, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out  pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode()
		{
			this.Text = MultilanguageResource.GetString("Folder_Msg70");
			this.ImageKey = ImageIndexes.RoleItemsImgIdx;
			this.SelectedImageKey = ImageIndexes.RoleItemsImgIdx;
			this.Tag = this.application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit70");

			if (!this.application.IAmManager)
			{
				this.getActionButton(ActionButtonKey_NewRole).Enable = false;
			}
			else
			{
				this.getActionButton(ActionButtonKey_NewRole).Enable = true;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			IAzManItem[] itemDefinitions = this.application.GetItems(ItemType.Role);
			foreach (IAzManItem definition in itemDefinitions)
				listChildren.Add(new ItemDefinitionNode(definition, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
		}

		#endregion

		#region Event handlers

		private void action_New_Click(object sender, EventArgs e)
		{
			frmItemProperties frm = new frmItemProperties();
			frm.application = this.application;
			frm.item = null;
			frm.itemType = ItemType.Role;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				this.Nodes.Add(new ItemDefinitionNode(frm.item, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));

				//Add relative child in Item Authorizations if Opened				
				//if (this.Parent != null //ItemDefinitions
				//      && this.Parent.Parent != null //ApplicationNode
				//      && this.Parent.Parent.Nodes.Count >= 3 //ApplicationNode tiene al menos las tres carpetas
				//      && ((ItemAuthorizationsNode)this.Parent.Parent.Nodes[2]).AreChildrenNodesAdded
				//   )
				//{
				//   ((RoleAuthorizationsNode)this.Parent.Parent.Nodes[2].Nodes[0]).Refresh();
				//}
				if (this.Parent != null //ItemDefinitions
						&& this.Parent.Parent != null //ApplicationNode
						&& this.Parent.Parent.Nodes.Count >= 3 //ApplicationNode tiene al menos las tres carpetas
						&& ((ItemAuthorizationsNode)this.Parent.Parent.Nodes[2]).AreChildrenNodesAdded
						&& ((RoleAuthorizationsNode)this.Parent.Parent.Nodes[2].Nodes[0]).AreChildrenNodesAdded
					)
				{
					ItemDefinitionsNode itemDefinitionsScopeNode = (ItemDefinitionsNode)this.Parent;
					RoleAuthorizationsNode itemAuthorizationsScopeNode = (itemDefinitionsScopeNode.Parent.Nodes[2].Nodes[0]) as RoleAuthorizationsNode;
					if (itemAuthorizationsScopeNode != null)
						itemAuthorizationsScopeNode.Nodes.Add(new ItemAuthorizationNode(frm.item, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
				}
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		#endregion
	}
}