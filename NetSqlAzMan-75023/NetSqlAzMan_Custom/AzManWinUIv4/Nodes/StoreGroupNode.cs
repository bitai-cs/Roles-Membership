using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes {
	public class StoreGroupNode : BaseNode {
		#region Private members
		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup _storeGroup;

		//private IAzManStoreGroup storeGroup;

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtCt_Properties;

		private ToolStripButton pvtsbtTb_Export;
		private ToolStripButton pvtsbtCt_Export;

		private ToolStripButton pvtsbtTb_Delete;
		private ToolStripButton pvtsbtCt_Delete;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;
		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_Delete = "delete";

		#endregion

		#region Constructors
		//public StoreGroupNode(IAzManStoreGroup storeGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
		//	this.storeGroup = storeGroup;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}

		public StoreGroupNode(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup storeGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = wau;

			this._storeGroup = storeGroup;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Public Properties

		public NetSqlAzMan.ServiceBusinessObjects.GroupType GroupType {
			get {
				return this._storeGroup.GroupType;
			}
		}

		#endregion

		#region Protected overriden members

		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg400");
			striButton2 = MultilanguageResource.GetString("Menu_Tit400");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg410");
			striButton2 = MultilanguageResource.GetString("Menu_Tit410");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Export_Click), out pvtsbtCt_Export, out pvtsbtTb_Export, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg40");
			striButton2 = MultilanguageResource.GetString("Menu_Msg40");
			ab = new ActionButton(ActionButtonKey_Delete, striButton1, striButton2, new EventHandler(action_Delete_Click), out pvtsbtCt_Delete, out pvtsbtTb_Delete, true);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, true);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = this._storeGroup.Name;

			switch (this._storeGroup.GroupType) {
				case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
					this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					this.SelectedImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					break;

				case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
					this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
					this.SelectedImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
					break;
			}

			this.Tag = this._storeGroup;

			this.ListItemText = this._storeGroup.Name;
			this.FirstSubItemText = this._storeGroup.Description;
			this.SecondSubItemText = this._storeGroup.GroupType.ToString();
			this.ThirdSubItemText = this._storeGroup.SID.StringValue;

			if (this._storeGroup.Store.IAmManager) {
				this.getActionButton(ActionButtonKey_Delete).Enable = true;
			}
			else {
				this.getActionButton(ActionButtonKey_Delete).Enable = false;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			switch (this._storeGroup.GroupType) {
				case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
					IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember> _storeGroupMembers = null;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManStoreGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>(_webApiUri);
					var _return = Task.Run(() => _h.GetAllAsync(_storeGroup.Store.Name, _storeGroup.Name)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_storeGroupMembers = _h.GetEnumerableSBOFromReturnedContent(_return);
					#endregion

					foreach (var _sgm in _storeGroupMembers) {
						_sgm.StoreGroup = _storeGroup;
						listChildren.Add(new BasicStoreGroupMemberNode(_webApiUri, _sgm, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
					}
					break;

				case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
					listChildren.Add(new LdapStoreGroupMemberNode(_webApiUri, this._storeGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
					break;
			}

			//switch (this.storeGroup.GroupType) {
			//	case NetSqlAzMan.Interfaces.GroupType.Basic:
			//		IAzManStoreGroupMember[] allMembers = this.storeGroup.GetStoreGroupAllMembers();
			//		foreach (IAzManStoreGroupMember member in allMembers)
			//			listChildren.Add(new BasicStoreGroupMember(member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			//		break;

			//	case NetSqlAzMan.Interfaces.GroupType.LDapQuery:
			//		listChildren.Add(new LDAPStoreGroupMember(this.storeGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			//		break;
			//}
		}

		#endregion

		#region Event handlers

		private void action_Export_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring!");

			//frmExportOptions frm = new frmExportOptions();
			//DialogResult dr = frm.ShowDialog();
			//if (dr == DialogResult.OK) {
			//	frmExport frmwait = new frmExport();
			//	frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.storeGroup }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.storeGroup.Store.Storage);
			//}
		}

		private void action_Properties_Click(object sender, EventArgs e) {
			using (frmStoreGroupsProperties frm = new frmStoreGroupsProperties(_webApiUri)) {
				frm.Text += " - " + this._storeGroup.Name;
				frm._storeGroup = this._storeGroup;

				DialogResult dr = frm.ShowDialog();
				if (dr == DialogResult.OK) {
					this._storeGroup = frm._storeGroup; //Devolvemos el Bso actualizado
					this.renderNode();
					this.Refresh();
				}
			}
		}

		private void action_Delete_Click(object sender, EventArgs e) {
			DialogResult dr = MessageBox.Show(String.Format(MultilanguageResource.GetString("Menu_Msg430") + "\r\n'{0}'", this._storeGroup.Name), MultilanguageResource.GetString("Menu_Msg420"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (dr == DialogResult.Yes) {
				try {
					NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup _deleted = null;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
					var _return = Task.Run(() => _h.DeleteAsync(this._storeGroup.Name, this._storeGroup)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_deleted = _h.GetSBOFromReturnedContent(_return);
					#endregion

					//Remover el nodo del Arbol
					this.Remove();

					//Remover el ListViewItem del listado (No siempre se lista dependiendo 
					//de como se hizo la seleccion del nodo, por eso se valida si hay una 
					//referencia al ParentListViewItem)
					if (this.RelatedListViewItem != null)
						this.RelatedListViewItem.Remove();
				}
				catch {
					throw;
				}
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		#endregion
	}
}