using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes {
	public class ItemDefinitionNode : BaseNode {
		#region Private fields
		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManItem _item;

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
		public ItemDefinitionNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManItem item, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = webApiUri;

			this._item = item;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Internal Properties
		internal NetSqlAzMan.ServiceBusinessObjects.AzManItem Item {
			get {
				return this._item;
			}
		}
		#endregion

		#region Protected Overriden members
		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			switch (this._item.ItemType) {
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
					striButton1 = MultilanguageResource.GetString("ListView_Msg60");
					striButton2 = MultilanguageResource.GetString("ListView_Tit60");
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
					striButton1 = MultilanguageResource.GetString("ListView_Msg70");
					striButton2 = MultilanguageResource.GetString("ListView_Tit70");
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
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
			ab = new ActionButton(ActionButtonKey_Delete, striButton1, striButton2, new EventHandler(action_Delete_Click), out pvtsbtCt_Delete, out pvtsbtTb_Delete, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = _item.Name;
			switch (this._item.ItemType) {
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
					this.ImageKey = ImageIndexes.RoleImgIdx;
					this.SelectedImageKey = ImageIndexes.RoleImgIdx;
					break;

				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
					this.ImageKey = ImageIndexes.TaskImgIdx;
					this.SelectedImageKey = ImageIndexes.TaskImgIdx;
					break;

				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
					this.ImageKey = ImageIndexes.OperationImgIdx;
					this.SelectedImageKey = ImageIndexes.OperationImgIdx;
					break;
			}
			this.Tag = this._item;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this._item.Description;
			this.SecondSubItemText = this._item.ItemId.ToString();

			if (this._item.Application.IAmManager) {
				this.getActionButton(ActionButtonKey_Delete).Enable = true;
			}
			else {
				this.getActionButton(ActionButtonKey_Delete).Enable = false;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem> _allMembers = null;
			#region Call WebApi			
			var _h = new AzManWebApiClientHelpers.AzManItemsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManItem>(_webApiUri);
			var _return = Task.Run(() => _h.GetItemMembersAsync(this._item.Application.Store.Name, this._item.Application.Name, this._item.Name, NetSqlAzMan.ServiceBusinessObjects.ItemType.All, false, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_allMembers = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			foreach (NetSqlAzMan.ServiceBusinessObjects.AzManItem member in _allMembers) {
				if (member.ItemType != NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation || member.ItemType == NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation && member.Application.Store.Storage.Mode == NetSqlAzMan.ServiceBusinessObjects.AzManMode.Developer) {
					listChildren.Add(new ItemDefinitionMemberNode(_webApiUri, member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
				}
			}

			////Old Logic
			//IAzManItem[] allMembers = this.item.GetMembers();
			//foreach (IAzManItem member in allMembers) {
			//	if (member.ItemType != ItemType.Operation || member.ItemType == ItemType.Operation && member.Application.Store.Storage.Mode == NetSqlAzManMode.Developer) {
			//		listChildren.Add(new ItemDefinitionMemberNode(member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			//	}
			//}
		}
		#endregion

		#region Event handlers
		private void action_Properties_Click(object sender, EventArgs e) {
			var _oldItemName = this._item.Name;

			DialogResult dr = DialogResult.Cancel;
			using (var frm = new frmItemProperties(_webApiUri) {
				Text = Text + " - " + this._item.Name,
				_application = this._item.Application,
				_item = this._item,
				_itemType = this._item.ItemType
			}) {
				dr = frm.ShowDialog();

				if (dr == DialogResult.OK)
					this._item = frm._item;
				else
					return;
			}

			this.Refresh();

			//Update relative child in Item Authorizations
			ItemDefinitionsNode _itemDefinitionsNode = (ItemDefinitionsNode)this.Parent.Parent;
			BaseNode _itemAuthorizationsNode = (ItemAuthorizationsNode)_itemDefinitionsNode.Parent.Nodes[2];

			switch (this._item.ItemType) {
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
					if (_itemAuthorizationsNode.AreChildrenNodesAdded && _itemAuthorizationsNode.Nodes[0].AreChildrenNodesAdded)
						_itemAuthorizationsNode = _itemAuthorizationsNode.Nodes[0]; //Roles Auth
					else
						return;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
					if (_itemAuthorizationsNode.AreChildrenNodesAdded && _itemAuthorizationsNode.Nodes[1].AreChildrenNodesAdded)
						_itemAuthorizationsNode = _itemAuthorizationsNode.Nodes[1]; //Task Auth
					else
						return;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
					if (_itemAuthorizationsNode.AreChildrenNodesAdded && _itemAuthorizationsNode.Nodes[2].AreChildrenNodesAdded)
						_itemAuthorizationsNode = _itemAuthorizationsNode.Nodes[2]; //Operation Auth
					else
						return;
					break;
			}

			foreach (ItemAuthorizationNode itemAuthorizationNode in _itemAuthorizationsNode.Nodes) {
				if (_oldItemName == itemAuthorizationNode.Item.Name) {
					itemAuthorizationNode.Item = this._item;
					break;
				}
			}
		}

		private void action_Delete_Click(object sender, EventArgs e) {
			string _caption = null;
			string _text = null;

			switch (this._item.ItemType) {
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
					_caption = MultilanguageResource.GetString("ListView_Msg90");
					_text = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg100"), this._item.Name);
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
					_caption = MultilanguageResource.GetString("ListView_Msg110");
					_text = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg120"), this._item.Name);
					break;
				case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
					_caption = MultilanguageResource.GetString("ListView_Msg130");
					_text = String.Format("{0}\r\n{1}", MultilanguageResource.GetString("ListView_Msg140"), this._item.Name);
					break;
			}

			var _oldItemName = this._item.Name;

			var dr = MessageBox.Show(_text, _caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr == DialogResult.Yes) {
				NetSqlAzMan.ServiceBusinessObjects.AzManItem _deleted = null;
				#region Call WebApi			
				var _h = new AzManWebApiClientHelpers.AzManItemsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManItem>(_webApiUri);
				var _return = Task.Run(() => _h.DeleteItemAsync(this._item.Name, this._item.Application.Store.Name, this._item.Application.Name)).Result;
				if (_h.IsResponseContentError(_return))
					_h.ThrowWebApiRequestException(_return);
				else
					_deleted = _h.GetSBOFromReturnedContent(_return);
				#endregion

				//Remove relative child and all its children in Item Authorizations
				ItemDefinitionsNode itemDefinitionsScopeNode = (ItemDefinitionsNode)this.Parent.Parent;
				BaseNode itemAuthorizationsScopeNode = (ItemAuthorizationsNode)itemDefinitionsScopeNode.Parent.Nodes[2];

				switch (this._item.ItemType) {
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[0].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[0];
						else {
							this.Remove();
							return;
						}
						break;
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[1].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[1];
						else {
							this.Remove();
							return;
						}
						break;
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
						if (itemAuthorizationsScopeNode.AreChildrenNodesAdded && itemAuthorizationsScopeNode.Nodes[2].AreChildrenNodesAdded)
							itemAuthorizationsScopeNode = itemAuthorizationsScopeNode.Nodes[2];
						else {
							this.Remove();
							return;
						}
						break;
				}

				foreach (ItemAuthorizationNode itemAuthorizationScopeNode in itemAuthorizationsScopeNode.Nodes) {
					if (_oldItemName == itemAuthorizationScopeNode.Item.Name) {
						itemAuthorizationScopeNode.Remove();
						break;
					}
				}

				this.Remove();
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}
		#endregion
	}
}