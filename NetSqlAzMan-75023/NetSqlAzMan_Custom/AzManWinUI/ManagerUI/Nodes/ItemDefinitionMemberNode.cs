using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;

namespace AzManWinUI.Nodes
{
	public class ItemDefinitionMemberNode : BaseNode
	{
		protected IAzManItem member;

		public ItemDefinitionMemberNode(IAzManItem member, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.member = member;
			
			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons()
		{

		}

		protected override void renderNode()
		{
			this.Text = member.Name;
			switch (member.ItemType)
			{
				case ItemType.Role:
					this.ImageKey = ImageIndexes.RoleImgIdx;
					break;
				case ItemType.Task:
					this.ImageKey = ImageIndexes.TaskImgIdx;
					break;
				case ItemType.Operation:
					this.ImageKey = ImageIndexes.OperationImgIdx;
					break;
			}
			this.SelectedImageKey = this.ImageKey;
			this.Tag = this.member;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this.member.ItemType.ToString();
			this.SecondSubItemText = this.member.Description;
			this.ThirdSubItemText = this.member.ItemId.ToString();
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Este nodo no devuelve nodos hijos
		}
	}
}