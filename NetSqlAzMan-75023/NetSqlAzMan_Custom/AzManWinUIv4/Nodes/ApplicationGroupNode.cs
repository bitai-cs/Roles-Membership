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
	public class ApplicationGroupNode : BaseNode {
		#region Private members
		protected IAzManApplicationGroup applicationGroup;

		protected NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup _applicationGroup;

		private string _webApiUri;

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

		#region Contructor

		//public ApplicationGroupNode(IAzManApplicationGroup applicationGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable)
		//{
		//	this.applicationGroup = applicationGroup;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}

		public ApplicationGroupNode(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup applicationGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			this._webApiUri = wau;

			this._applicationGroup = applicationGroup;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Public Properties

		public NetSqlAzMan.ServiceBusinessObjects.GroupType GroupType {
			get {
				return this._applicationGroup.GroupType;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg160");
			striButton2 = MultilanguageResource.GetString("Menu_Tit160");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg170");
			striButton2 = MultilanguageResource.GetString("Menu_Tit170");
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
			this.Text = this._applicationGroup.Name;
			this.ImageKey = this._applicationGroup.GroupType == NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic ? ImageIndexes.ApplicationGroupBasicImgIdx : ImageIndexes.ApplicationGroupLDAPImgIdx;
			this.SelectedImageKey = this.ImageKey;
			this.Tag = this._applicationGroup;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this._applicationGroup.Description;
			this.SecondSubItemText = this._applicationGroup.GroupType.ToString();
			this.ThirdSubItemText = this._applicationGroup.SID.StringValue;

			if (this._applicationGroup.Application.IAmManager) {
				this.getActionButton(ActionButtonKey_Delete).Enable = true;
			}
			else {
				this.getActionButton(ActionButtonKey_Delete).Enable = false;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			switch (this.GroupType) {
				case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
					IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember> _allMembers = null;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManApplicationGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember>(_webApiUri);
					var _return = Task.Run(() => _h.GetAsync(_applicationGroup.Application.Store.Name, _applicationGroup.Application.Name, _applicationGroup.Name)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_allMembers = _h.GetEnumerableSBOFromReturnedContent(_return);
					#endregion
					foreach (NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember member in _allMembers)
						listChildren.Add(new BasicApplicationGroupMember(_webApiUri, member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));

					break;

				case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
					listChildren.Add(new LDAPApplicationGroupMember(this._applicationGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));

					break;
			}
		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		private void action_Delete_Click(object sender, EventArgs e) {
			DialogResult dr = MessageBox.Show(String.Format(MultilanguageResource.GetString("Menu_Msg190") + "\r\n'{0}'", this._applicationGroup.Name), MultilanguageResource.GetString("Menu_Msg180"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes) {
				NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup _deleted = null;
				#region Call WebApi
				var _h = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
				var _return = Task.Run(() => _h.DeleteAsync(this._applicationGroup.Name, this._applicationGroup)).Result;
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
		}

		private void action_Export_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring");

			//frmExportOptions frm = new frmExportOptions();
			//DialogResult dr = frm.ShowDialog();
			//if (dr == DialogResult.OK) {
			//	frmExport frmwait = new frmExport();
			//	frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.applicationGroup }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.applicationGroup.Application.Store.Storage);
			//}
		}

		private void action_Properties_Click(object sender, EventArgs e) {
			frmApplicationGroupsProperties frm = new frmApplicationGroupsProperties(_webApiUri);
			frm.Text += " - " + this._applicationGroup.Name;
			frm._applicationGroup = this._applicationGroup;
			DialogResult dr = frm.ShowDialog();

			if (dr == DialogResult.OK)
				this.Refresh();
		}

		#endregion
	}
}