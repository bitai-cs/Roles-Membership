using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace AzManWinUI.Nodes
{
	public class LDAPStoreGroupMember : BaseNode
	{
		private IAzManStoreGroup storeGroup;

		public LDAPStoreGroupMember(IAzManStoreGroup storeGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.storeGroup = storeGroup;
			
			this.createNodeActionButtons();

			this.renderNode();			
		}

		protected override void createNodeActionButtons()
		{
			//Este nodo no tiene acciones
		}

		protected override void renderNode()
		{
			this.Text = this.storeGroup.Name;
			this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
			this.SelectedImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
			this.Tag = this.storeGroup;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this.storeGroup.Description;
			this.SecondSubItemText = this.storeGroup.LDAPQuery;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Este nodo no lista nodos hijos
		}
	}
}
