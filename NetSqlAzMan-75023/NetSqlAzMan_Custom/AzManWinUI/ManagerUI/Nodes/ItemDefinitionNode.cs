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
	public class ItemDefinitionNode : BaseNode
	{
		#region Private fields

		private IAzManItem item;

		private ToolStripButton pvtsbtCt_Properties;
		private ToolStripButton pvtsbtTb_Properties;

		private ToolStripButton pvtsbtCt_Delete;
		private ToolStripButton pvtsbtTb_Delete;

		private ToolStripButton pvtsbtCt_Refresh;
		private ToolStripButton pvtsbtTb_Refresh;

		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_Delete = "delete";

		#endregion

		#region Constructor

		public ItemDefinitionNode(IAzManItem item, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.item = item;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Internal Properties

		internal IAzManItem Item
		{
			get
			{
				return this.item;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons()
		{
			string striButton1;
			string striButton2;
			ActionButton ab;

			switch (this.item.ItemType)
			{
				case ItemType.Role:
					striButton1 = MultilanguageResource.GetString("ListView_Msg60");
					striButton2 = MultilanguageResource.GetString("ListView_Tit60");
					break;
				case ItemType.Task:
					striButton1 = MultilanguageResource.GetString("ListView_Msg70");
					striButton2 = MultilanguageResource.GetString("ListView_Tit70");
					break;
				case ItemType.Operation:
					striButton1 = MultilanguageResource.GetString("ListView_Msg80");
					striButton2 = MultilanguageResource.GetString("ListView_Tit80");
					break;
				default:
					striButton1 = null;
					striButton2 = null;
					break;
			}
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg40");
			striButton2 = MultilanguageResource.GetString("Menu_Msg40");
			ab = new ActionButton(ActionButtonKey_Delete, striButton1, striButton2, new EventHandler(action_Delete_Click), out  pvtsbtCt_Delete, out pvtsbtTb_Delete, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out  pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode()
		{
			this.Text = item.Name;
			switch (this.item.ItemType)
			{
				case ItemType.Role:
					this.ImageKey = ImageIndexes.RoleImgIdx;
					this.SelectedImageKey = ImageIndexes.RoleImgIdx;
					break;

				case ItemType.Task:
					this.ImageKey = ImageIndexes.TaskImgIdx;
					this.SelectedImageKey = ImageIndexes.TaskImgIdx;
					break;

				case ItemType.Operation:
					this.ImageKey = ImageIndexes.OperationImgIdx;
					this.SelectedImageKey = ImageIndexes.OperationImgIdx;
					break;
			}
			this.Tag = this.item;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this.item.Description;
			this.SecondSubItemText = this.item.ItemId.ToString();

			if (this.item.Application.IAmManager)
			{
				this.getActionButton(ActionButtonKey_Delete).Enable = true;
			}
			else
			{
				this.getActionButton(ActionButtonKey_Delete).Enable = false;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			IAzManItem[] allMembers = this.item.GetMembers();
			foreach (IAzManItem member in allMembers)
			{
				if (member.ItemType != ItemType.Operation || member.ItemType == ItemType.Operation && member.Application.Store.Storage.Mode == NetSqlAzManMode.Developer)
				{
					listChildren.Add(new ItemDefinitionMemberNode(member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
				}
			}
		}

		#endregion

		#region Event handlers

		private void action_Properties_Click(object sender, EventArgs e)
		{
			frmItemProperties frm = new frmItemProperties();
			frm.Text += " - " + this.item.Name;
			frm.application = this.item.Application;
			frm.item = this.item;
			frm.itemType = this.item.ItemType;
			string oldItemName = this.item.Name;
			DialogResult dr = frm.ShowDialog();

			if (dr == DialogResult.OK)
			{
				this.renderNode();

				//Update relative child in Item Authorizations
				ItemDefinitionsNode itemDefinitionsScopeNode = (ItemDefinitionsNode)this.Parent.Parent;
				BaseNode itemAuthorizationsScopeNode = (ItemAuthorizationsNode)itemDefinitionsScopeNode.Parent.Nodes[2];

				switch (this.item.ItemType)
				{
					case ItemType.Role:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[0].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[0]; //Roles Auth
						else
							return;
						break;
					case ItemType.Task:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[1].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[1]; //Task Auth
						else
							return;
						break;
					case ItemType.Operation:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[2].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[2]; //Operation Auth
						else
							return;
						break;
				}

				foreach (ItemAuthorizationNode itemAuthorizationScopeNode in itemAuthorizationsScopeNode.Nodes)
				{
					if (oldItemName == itemAuthorizationScopeNode.Item.Name)
					{
						itemAuthorizationScopeNode.Item = this.item;
						break;
					}
				}
			}
		}

		private void action_Delete_Click(object sender, EventArgs e)
		{
			string striCaption = null;
			string striText = null;

			switch (this.item.ItemType)
			{
				case ItemType.Role:
					striCaption = MultilanguageResource.GetString("ListView_Msg90");
					striText = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg100"), this.item.Name);
					break;
				case ItemType.Task:
					striCaption = MultilanguageResource.GetString("ListView_Msg110");
					striText = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg120"), this.item.Name);
					break;
				case ItemType.Operation:
					striCaption = MultilanguageResource.GetString("ListView_Msg130");
					striText = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg140"), this.item.Name);
					break;
			}

			string oldItemName = this.item.Name;

			DialogResult dr = MessageBox.Show(striText, striCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr == DialogResult.Yes)
			{
				this.item.Delete();

				//Remove relative child and all its children in Item Authorizations
				ItemDefinitionsNode itemDefinitionsScopeNode = (ItemDefinitionsNode)this.Parent.Parent;
				BaseNode itemAuthorizationsScopeNode = (ItemAuthorizationsNode)itemDefinitionsScopeNode.Parent.Nodes[2];

				switch (this.item.ItemType)
				{
					case ItemType.Role:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[0].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[0];
						else
						{
							this.Remove();
							return;
						}
						break;
					case ItemType.Task:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[1].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[1];
						else
						{
							this.Remove();
							return;
						}
						break;
					case ItemType.Operation:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[2].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[2];
						else
						{
							this.Remove();
							return;
						}
						break;
				}

				foreach (ItemAuthorizationNode itemAuthorizationScopeNode in itemAuthorizationsScopeNode.Nodes)
				{
					if (oldItemName == itemAuthorizationScopeNode.Item.Name)
					{
						itemAuthorizationScopeNode.Remove();
						break;
					}
				}

				this.Remove();
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		#endregion
	}
}