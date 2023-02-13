using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace AzManWinUI.Nodes
{
	public class LDAPApplicationGroupMember : BaseNode
	{
		private IAzManApplicationGroup applicationGroup;

		public LDAPApplicationGroupMember(IAzManApplicationGroup applicationGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.applicationGroup = applicationGroup;
			
			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons()
		{
			//Este nodo no tiene acciones
		}

		protected override void renderNode()
		{
			this.Text = this.applicationGroup.Name;
			this.ImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
			this.SelectedImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
			this.Tag = this.applicationGroup;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this.applicationGroup.Description;
			this.SecondSubItemText = this.applicationGroup.LDAPQuery;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Este nodo no lista nodos hijos
		}
	}
}
