using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;

namespace AzManWinUI.Nodes
{
	public class MembershipNode : Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtTb_NewUser;

		private ToolStripButton pvtsbtCt_Refresh;
		private ToolStripButton pvtsbtCt_NewUser;

		#endregion

		#region Pubic Constants

		public const string ActionButtonKey_NewUser = "new";
		public const string ActionButtonKey_Refresh = "refresh";

		#endregion

		#region Constructor

		public MembershipNode(string connectionString, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			this.pvstriConnectionString = connectionString;

			this.createNodeActionButtons();

			this.renderNode();

			base.KeyDown += new KeyEventHandler(MembershipNode_KeyDown);
		}

		#endregion

		#region Properties

		private string pvstriConnectionString;
		internal string ConnectionString {
			get {
				return pvstriConnectionString;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons() {
			string striButton = null;
			string striButton2 = null;
			ActionButton ab;

			striButton = "Nuevo";
			striButton2 = "Nuevo usuario";
			ab = new ActionButton(ActionButtonKey_NewUser, striButton, striButton2, new EventHandler(pvtsbtCt_NewUser_Click), out pvtsbtCt_NewUser, out pvtsbtTb_NewUser, false);
			this.registerActionButton(ref ab);

			striButton = "Refrescar";
			striButton2 = null;
			ab = new ActionButton(ActionButtonKey_Refresh, striButton, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = "Usuarios";
			this.ImageKey = ImageIndexes.MembershipsImgIdx;
			this.SelectedImageKey = ImageIndexes.MembershipsImgIdx;
			this.Tag = String.Empty;

			this.ListItemText = this.Text;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			List<NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct> listUsers;

			using (NetSqlAzManSnapIn.AddOn.Membership.Objects.User busoObject = new NetSqlAzManSnapIn.AddOn.Membership.Objects.User(this.pvstriConnectionString)) {
				if (!busoObject.GetUsers(out listUsers, out this.ptexceError))
					throw this.ptexceError;
			}

			foreach (NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct user in listUsers) {
				listChildren.Add(new UserNode(this, user, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			}
		}

		#endregion

		#region Event handlers

		private void pvtsbtCt_NewUser_Click(object sender, EventArgs e) {
			NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI formUser = new NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI(this.ConnectionString, true);

			formUser.UserRecord = new NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct();
			if (!formUser.NewRecord(out ptexceError))
				throw ptexceError;

			if (formUser.ShowDialog() == DialogResult.OK) {
				if (!this.ChildListView.ListParentNodeDetails(out ptexceError))
					throw ptexceError;
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		private void MembershipNode_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.F5:
					this.getActionButton(ActionButtonKey_Refresh).Execute();
					break;
			}
		}

		#endregion
	}
}