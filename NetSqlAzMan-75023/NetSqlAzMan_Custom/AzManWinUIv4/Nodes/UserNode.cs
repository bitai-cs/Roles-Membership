using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;

namespace AzManWinUI.Nodes {
	public class UserNode : Basgosoft.ManagementConsoleLib.BaseNode {
		#region Private fields
		private string _webApiUri;
		private MembershipNode pvnodeMembershipNode;

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtTb_ChangePassword;
		private ToolStripButton pvtsbtTb_Disable;

		private ToolStripButton pvtsbtCt_Properties;
		private ToolStripButton pvtsbtCt_ChangePassword;
		private ToolStripButton pvtsbtCt_Disable;

		NetSqlAzMan.ServiceBusinessObjects.DBUser pvstruUser;
		#endregion

		public const string ActionButtonKey_Properties = "properties";
		public const string ActionButtonKey_ChangePassword = "changePassword";
		public const string ActionButtonKey_ChangeStatus = "changeStatus";

		#region Constructor

		public UserNode(string webApiUri, MembershipNode parentNode, NetSqlAzMan.ServiceBusinessObjects.DBUser user, ToolStrip toolBar,
ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			_webApiUri = webApiUri;

			this.pvnodeMembershipNode = parentNode;

			pvstruUser = user;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		protected override void createNodeActionButtons() {
			string striButton1;
			ActionButton ab;

			striButton1 = "Propiedades";
			ab = new ActionButton(ActionButtonKey_Properties, striButton1, null, new EventHandler(pvtsbtTb_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = "Reestablecer contraseña";
			ab = new ActionButton(ActionButtonKey_ChangePassword, striButton1, null, new EventHandler(pvtsbtTb_ChangePassword_Click), out pvtsbtCt_ChangePassword, out pvtsbtTb_ChangePassword, false);
			this.registerActionButton(ref ab);

			striButton1 = "Cambiar estado";
			ab = new ActionButton(ActionButtonKey_ChangeStatus, striButton1, null, new EventHandler(pvtsbtTb_ChangeStatus_Click), out pvtsbtCt_Disable, out pvtsbtTb_Disable, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = string.Format("{0} ({1})", this.pvstruUser.UserName, this.pvstruUser.UserID.ToString());
			if (this.pvstruUser.Enabled == true)
				this.ImageKey = ImageIndexes.UserImgIdx;
			else
				this.ImageKey = ImageIndexes.UserForbiddenImgIdx;
			this.Tag = pvstruUser;
			this.ListItemText = pvstruUser.UserID.ToString();
			this.FirstSubItemText = this.pvstruUser.UserName;
			this.SecondSubItemText = this.pvstruUser.FullName;
			this.ThirdSubItemText = "DepartmentName"; //this.pvstruUser.Department.DepartmentName;
			this.FourthSubItemText = "BranchOfficeNames";
			this.FifthSubItemText = this.pvstruUser.Enabled.ToString();
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//No lista hijos
		}

		#region Event handlers

		private void pvtsbtTb_Properties_Click(object sender, EventArgs e) {
			var _ui = new NetSqlAzMan.SnapIn.Forms.UserUI(_webApiUri, true) {
				UserRecord = pvstruUser
			};

			Exception _ex;
			if (_ui.EditRecord(out _ex)) {
				if (_ui.ShowDialog() == DialogResult.OK)
					this.Refresh();
			}
			else
				throw _ex;
		}

		private void pvtsbtTb_ChangePassword_Click(object sender, EventArgs e) {
			var _ui = new NetSqlAzMan.SnapIn.Forms.UserUI(_webApiUri, true) {
				UserRecord = pvstruUser
			};

			Exception _ex;
			if (_ui.ChangePassword(out _ex)) {
				if (_ui.ShowDialog() == DialogResult.OK)
					this.Refresh();
			}
			else
				throw _ex;
		}

		private void pvtsbtTb_ChangeStatus_Click(object sender, EventArgs e) {
			var _ui = new NetSqlAzMan.SnapIn.Forms.UserUI(_webApiUri, true) {
				UserRecord = pvstruUser
			};

			Exception _ex;
			if (_ui.ChangeStatus(!pvstruUser.Enabled, out _ex)) {
				if (_ui.ShowDialog() == DialogResult.OK)
					this.Refresh();
			}
			else
				throw _ex;
		}

		#endregion
	}
}
