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

namespace AzManWinUI.Nodes {
	public class ItemAuthorizationNode : BaseNode {
		#region Private fields
		//private IAzManItem item;

		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManItem _item;

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtCt_Properties;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;

		#endregion

		#region Constructor
		//public ItemAuthorizationNode(IAzManItem item, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
		//	this.item = item;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}

		public ItemAuthorizationNode(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManItem item, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = wau;

			this._item = item;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Internal Properties

		internal NetSqlAzMan.ServiceBusinessObjects.AzManItem Item {
			set {
				this._item = value;

				this.renderNode();
			}
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

			striButton1 = MultilanguageResource.GetString("Menu_Msg540");
			striButton2 = MultilanguageResource.GetString("Menu_Tit540");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, true);
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
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization> _authorizations = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
			var _return = Task.Run(() => _h.GetAllByItemAsync(_item.Application.Store.Name, _item.Application.Name, _item.Name, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_authorizations = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			foreach (NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization auth in _authorizations)
				listChildren.Add(new ItemAuthorizationMemberNode(_webApiUri, auth, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));

			//IAzManAuthorization[] authorizations = this.item.GetAuthorizations();
			//foreach (IAzManAuthorization auth in authorizations)
			//	listChildren.Add(new ItemAuthorizationMemberNode(auth, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
		}

		#endregion

		#region Event handlers

		private void action_Properties_Click(object sender, EventArgs e) {
			var frm = new frmItemAuthorizations(_webApiUri);
			frm.Text += " - " + this._item.Name;
			frm._item = this._item;

			DialogResult dr = frm.ShowDialog();

			if (dr == DialogResult.OK) {
				this.renderNode();
				this.Refresh();
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		#endregion
	}
}