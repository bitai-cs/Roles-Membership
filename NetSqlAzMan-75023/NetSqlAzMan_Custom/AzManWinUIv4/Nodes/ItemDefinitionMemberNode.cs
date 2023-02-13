using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;

namespace AzManWinUI.Nodes {
	public class ItemDefinitionMemberNode : BaseNode {
		protected IAzManItem member;

		private string _webApiUri;
		protected NetSqlAzMan.ServiceBusinessObjects.AzManItem _member;

		public ItemDefinitionMemberNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManItem member, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = webApiUri;

			this._member = member;

			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons() {
			//No Actions
		}

		protected override void renderNode() {
			this.Text = _member.Name;
			switch (_member.ItemType) {
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
					this.ImageKey = ImageIndexes.RoleImgIdx;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
					this.ImageKey = ImageIndexes.TaskImgIdx;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
					this.ImageKey = ImageIndexes.OperationImgIdx;
					break;
			}
			this.SelectedImageKey = this.ImageKey;
			this.Tag = this._member;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this._member.ItemType.ToString();
			this.SecondSubItemText = this._member.Description;
			this.ThirdSubItemText = this._member.ItemId.ToString();
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//Este nodo no devuelve nodos hijos
		}
	}
}