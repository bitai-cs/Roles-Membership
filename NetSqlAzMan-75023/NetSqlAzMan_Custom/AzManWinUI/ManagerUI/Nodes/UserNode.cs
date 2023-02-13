using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;

namespace AzManWinUI.Nodes
{
	public class UserNode : Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields

		private MembershipNode pvnodeMembershipNode;

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtTb_Disable;

		private ToolStripButton pvtsbtCt_Properties;
		private ToolStripButton pvtsbtCt_Disable;

		private NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct pvstruUser;

		#endregion

		public const string ActionButtonKey_Properties = "properties";
		public const string ActionButtonKey_ChangeStatus = "changeStatus";

		#region Constructor

		public UserNode(MembershipNode parentNode, NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct user, ToolStrip toolBar,
ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
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

			striButton1 = "Cambiar estado";
			ab = new ActionButton(ActionButtonKey_ChangeStatus, striButton1, null, new EventHandler(pvtsbtTb_ChangeStatus_Click), out pvtsbtCt_Disable, out pvtsbtTb_Disable, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = string.Format("{0} ({1})", this.pvstruUser.UserName, this.pvstruUser.UserId.ToString());
			if (this.pvstruUser.Enabled == true)
				this.ImageKey = ImageIndexes.UserImgIdx;
			else
				this.ImageKey = ImageIndexes.UserForbiddenImgIdx;
			this.Tag = pvstruUser;
			this.ListItemText = pvstruUser.UserId.ToString();
			this.FirstSubItemText = this.pvstruUser.UserName;
			this.SecondSubItemText = this.pvstruUser.FullName;
			this.ThirdSubItemText = this.pvstruUser.DepartmentName;
			this.FourthSubItemText = this.pvstruUser.BranchOfficeNames;
			this.FifthSubItemText = this.pvstruUser.Enabled.Value.ToString();
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//No lista hijos
		}

		#region Event handlers

		private void pvtsbtTb_Properties_Click(object sender, EventArgs e) {
			NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI formUser = new NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI(pvnodeMembershipNode.ConnectionString, true);

			formUser.UserRecord = pvstruUser;
			if (!formUser.EditRecord(out ptexceError))
				throw this.ptexceError;

			if (formUser.ShowDialog() == DialogResult.OK)
				this.Refresh();
		}

		private void pvtsbtTb_ChangeStatus_Click(object sender, EventArgs e) {
			NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI formUser = new NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI(pvnodeMembershipNode.ConnectionString, true);

			formUser.UserRecord = pvstruUser;
			if (!formUser.ChangeRecordStatus(!pvstruUser.Enabled.Value, out ptexceError))
				throw this.ptexceError;

			if (formUser.ShowDialog() == DialogResult.OK)
				this.Refresh();
		}

		#endregion
	}
}
