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
	public class RoleDefinitionsNode : BaseNode {
		#region Private fields
		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManApplication _application;

		private ToolStripButton pvtsbtCt_New;
		private ToolStripButton pvtsbtTb_New;

		private ToolStripButton pvtsbtCt_Refresh;
		private ToolStripButton pvtsbtTb_Refresh;
		#endregion

		#region Public Constants Field
		public const string ActionButtonKey_NewRole = "newRole";
		#endregion

		#region Constructor
		public RoleDefinitionsNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			this._webApiUri = webApiUri;

			this._application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Protected Overriden members
		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg330");
			striButton2 = MultilanguageResource.GetString("Menu_Tit330");
			ab = new ActionButton(ActionButtonKey_NewRole, striButton1, striButton2, new EventHandler(action_New_Click), out pvtsbtCt_New, out pvtsbtTb_New, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("Folder_Msg70");
			this.ImageKey = ImageIndexes.RoleItemsImgIdx;
			this.SelectedImageKey = ImageIndexes.RoleItemsImgIdx;
			this.Tag = this._application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit70");

			if (!this._application.IAmManager) {
				this.getActionButton(ActionButtonKey_NewRole).Enable = false;
			}
			else {
				this.getActionButton(ActionButtonKey_NewRole).Enable = true;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//New Logic
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem> _itemDefinitions = null;
			#region Call WebApi			
			var _h = new AzManWebApiClientHelpers.AzManItemsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManItem>(_webApiUri);
			var _return = Task.Run(() => _h.GetItemsAsync(this._application.Store.Name, this._application.Name, NetSqlAzMan.ServiceBusinessObjects.ItemType.Role, false, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_itemDefinitions = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			foreach (NetSqlAzMan.ServiceBusinessObjects.AzManItem definition in _itemDefinitions)
				listChildren.Add(new ItemDefinitionNode(_webApiUri, definition, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
		}
		#endregion

		#region Event handlers

		private void action_New_Click(object sender, EventArgs e) {
			NetSqlAzMan.ServiceBusinessObjects.AzManItem _created = null;
			DialogResult dr = DialogResult.Cancel;
			using (var _frm = new frmItemProperties(_webApiUri) {
				_application = this._application,
				_item = null /*Nulo por que se va a crear uno nuevo*/,
				_itemType = NetSqlAzMan.ServiceBusinessObjects.ItemType.Role
			}) {
				dr = _frm.ShowDialog();
				if (dr == DialogResult.OK)
					_created = _frm._item;
				else
					return;
			}

			//Create item node
			this.Nodes.Add(new ItemDefinitionNode(_webApiUri, _created, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));

			//Add relative child in Item Authorizations if Opened				
			if (this.Parent != null //ItemDefinitions
					&& this.Parent.Parent != null //ApplicationNode
					&& this.Parent.Parent.Nodes.Count >= 3 //ApplicationNode tiene al menos las tres carpetas
					&& ((ItemAuthorizationsNode)this.Parent.Parent.Nodes[2]).AreChildrenNodesAdded
					&& ((RoleAuthorizationsNode)this.Parent.Parent.Nodes[2].Nodes[0]).AreChildrenNodesAdded
				) {
				ItemDefinitionsNode itemDefinitionsScopeNode = (ItemDefinitionsNode)this.Parent;
				RoleAuthorizationsNode itemAuthorizationsScopeNode = (itemDefinitionsScopeNode.Parent.Nodes[2].Nodes[0]) as RoleAuthorizationsNode;
				if (itemAuthorizationsScopeNode != null)
					itemAuthorizationsScopeNode.Nodes.Add(new ItemAuthorizationNode(_webApiUri, _created, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		#endregion
	}
}