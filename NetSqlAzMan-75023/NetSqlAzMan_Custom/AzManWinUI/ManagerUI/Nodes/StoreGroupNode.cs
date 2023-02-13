using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;

namespace AzManWinUI.Nodes
{
	public class StoreGroupNode : BaseNode
	{
		#region Private members

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtCt_Properties;

		private ToolStripButton pvtsbtTb_Export;
		private ToolStripButton pvtsbtCt_Export;

		private ToolStripButton pvtsbtTb_Delete;
		private ToolStripButton pvtsbtCt_Delete;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;

		private IAzManStoreGroup storeGroup;

		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_Delete = "delete";

		#endregion

		#region Constructors

		public StoreGroupNode(IAzManStoreGroup storeGroup, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.storeGroup = storeGroup;

			this.createNodeActionButtons();

			this.renderNode();
		}

		#endregion

		#region Public Properties

		public GroupType GroupType
		{
			get
			{
				return this.storeGroup.GroupType;
			}
		}

		#endregion

		#region Protected overriden members

		protected override void createNodeActionButtons()
		{
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

		protected override void renderNode()
		{
			this.Text = this.storeGroup.Name;

			switch (this.storeGroup.GroupType)
			{
				case GroupType.Basic:
					this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					this.SelectedImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					break;

				case GroupType.LDapQuery:
					this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
					this.SelectedImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
					break;
			}

			this.Tag = this.storeGroup;

			this.ListItemText = this.storeGroup.Name;
			this.FirstSubItemText = this.storeGroup.Description;
			this.SecondSubItemText = this.storeGroup.GroupType.ToString();
			this.ThirdSubItemText = this.storeGroup.SID.StringValue;

			if (this.storeGroup.Store.IAmManager)
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
			switch (this.storeGroup.GroupType)
			{
				case NetSqlAzMan.Interfaces.GroupType.Basic:

					IAzManStoreGroupMember[] allMembers = this.storeGroup.GetStoreGroupAllMembers();
					foreach (IAzManStoreGroupMember member in allMembers)
						listChildren.Add(new BasicStoreGroupMember(member, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
					break;

				case NetSqlAzMan.Interfaces.GroupType.LDapQuery:

					listChildren.Add(new LDAPStoreGroupMember(this.storeGroup, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
					break;
			}
		}

		#endregion

		#region Event handlers

		private void action_Export_Click(object sender, EventArgs e)
		{
			frmExportOptions frm = new frmExportOptions();
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				frmExport frmwait = new frmExport();
				frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.storeGroup }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.storeGroup.Store.Storage);
			}
		}

		private void action_Properties_Click(object sender, EventArgs e)
		{
			frmStoreGroupsProperties frm = new frmStoreGroupsProperties();
			frm.Text += " - " + this.storeGroup.Name;
			frm.storeGroup = this.storeGroup;
			DialogResult dr = frm.ShowDialog();
			/*Application.DoEvents();*/
			frm.Dispose();
			/*Application.DoEvents();*/
			if (dr == DialogResult.OK)
			{
				this.renderNode();
				this.Refresh();
			}
		}

		private void action_Delete_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(String.Format(MultilanguageResource.GetString("Menu_Msg430") + "\r\n'{0}'", this.storeGroup.Name), MultilanguageResource.GetString("Menu_Msg420"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (dr == DialogResult.Yes)
			{
				try
				{
					this.storeGroup.Delete();

					//Remover el nodo del Arbol
					this.Remove();

					//Remover el ListViewItem del listado (No siempre se lista dependiendo de como se hizo 
					//la seleccion del nodo, por eso se valida si hay una referencia al ParentListViewItem)
					if (this.RelatedListViewItem != null)
						this.RelatedListViewItem.Remove();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		#endregion
	}
}