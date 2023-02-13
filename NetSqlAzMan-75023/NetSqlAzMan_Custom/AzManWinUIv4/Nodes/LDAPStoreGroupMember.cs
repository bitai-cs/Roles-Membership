using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace AzManWinUI.Nodes
{
	public class LdapStoreGroupMemberNode :BaseNode
	{
		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup _storeGroup;

		public LdapStoreGroupMemberNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup storeGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			this._webApiUri = webApiUri;

			this._storeGroup = storeGroup;

			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons() {
			//Este nodo no tiene acciones
		}

		protected override void renderNode() {
			this.Text = this._storeGroup.Name;
			this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
			this.SelectedImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
			this.Tag = this._storeGroup;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this._storeGroup.Description;
			this.SecondSubItemText = this._storeGroup.LDAPQuery;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//Este nodo no lista nodos hijos
		}
	}
}
