using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;

namespace AzManWinUI.Nodes
{
	public class ApplicationGroupNode : BaseNode
	{
		#region Private members

		protected IAzManApplicationGroup applicationGroup;

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

		public ApplicationGroupNode(IAzManApplicationGroup applicationGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable)
		{
			this.applicationGroup = applicationGroup;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Public Properties

		public GroupType GroupType
		{
			get
			{
				return this.applicationGroup.GroupType;
			}
		}

		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons()
		{
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
		
		protected override void renderNode()
		{
			this.Text = this.applicationGroup.Name;
			this.ImageKey = this.applicationGroup.GroupType == GroupType.Basic ? ImageIndexes.ApplicationGroupBasicImgIdx : ImageIndexes.ApplicationGroupLDAPImgIdx;
			this.SelectedImageKey = this.ImageKey;
			this.Tag = this.applicationGroup;

			this.ListItemText = this.Text;
			this.FirstSubItemText = this.applicationGroup.Description;
			this.SecondSubItemText = this.applicationGroup.GroupType.ToString();
			this.ThirdSubItemText = this.applicationGroup.SID.StringValue;

			if (this.applicationGroup.Application.IAmManager)
			{
				this.getActionButton(ActionButtonKey_Delete).Enable = true;
			}
			else
			{
				this.getActionButton(ActionButtonKey_Delete).Enable = false;
			}
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			switch (this.GroupType)
			{
				case NetSqlAzMan.Interfaces.GroupType.Basic:

					IAzManApplicationGroupMember[] allMembers = this.applicationGroup.GetApplicationGroupAllMembers();
					foreach (IAzManApplicationGroupMember member in allMembers)
						listChildren.Add(new BasicApplicationGroupMember(member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));

					break;

				case NetSqlAzMan.Interfaces.GroupType.LDapQuery:
					listChildren.Add(new LDAPApplicationGroupMember(this.applicationGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
					break;
			}

		}

		#endregion

		#region Event handlers

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		private void action_Delete_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(String.Format(MultilanguageResource.GetString("Menu_Msg190") + "\r\n'{0}'", this.applicationGroup.Name), MultilanguageResource.GetString("Menu_Msg180"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes)
			{
				this.applicationGroup.Delete();

				this.Remove();
			}
		}

		private void action_Export_Click(object sender, EventArgs e)
		{
			frmExportOptions frm = new frmExportOptions();
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				frmExport frmwait = new frmExport();
				frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.applicationGroup }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.applicationGroup.Application.Store.Storage);
			}
		}

		private void action_Properties_Click(object sender, EventArgs e)
		{
			frmApplicationGroupsProperties frm = new frmApplicationGroupsProperties();
			frm.Text += " - " + this.applicationGroup.Name;
			frm.applicationGroup = this.applicationGroup;
			DialogResult dr = frm.ShowDialog();

			if (dr == DialogResult.OK)
				this.Refresh();				
		}

		#endregion
	}
}