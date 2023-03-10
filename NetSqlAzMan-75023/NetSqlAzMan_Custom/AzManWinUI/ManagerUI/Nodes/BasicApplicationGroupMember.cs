using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI.Nodes
{
	public class BasicApplicationGroupMember : Basgosoft.ManagementConsoleLib.BaseNode
	{
		IAzManApplicationGroupMember member;

		public BasicApplicationGroupMember(IAzManApplicationGroupMember applicationGroupMember, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.member = applicationGroupMember;
			
			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons()
		{
			//Este nodo no tiene accioes
		}

		protected override void renderNode()
		{
			string sMemberNonMember = member.IsMember ? MultilanguageResource.GetString("Domain_Member") : MultilanguageResource.GetString("Domain_NonMember");

			string displayName;
			MemberType memberType = member.GetMemberInfo(out displayName);

			this.Text = displayName;

			switch (memberType)
			{
				case MemberType.AnonymousSID:
					this.ImageKey = ImageIndexes.SidNotFoundImgIdx;
					break;
				case MemberType.ApplicationGroup:
					//this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
					switch (member.ApplicationGroup.Application.GetApplicationGroup(member.SID).GroupType)
					{
						case GroupType.Basic:
							this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
							break;
						case GroupType.LDapQuery:
							this.ImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
							break;
					}
					break;
				case MemberType.StoreGroup:
					//this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					switch (member.ApplicationGroup.Application.Store.GetStoreGroup(member.SID).GroupType)
					{
						case GroupType.Basic:
							this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
							break;
						case GroupType.LDapQuery:
							this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
							break;
					}
					break;
				case MemberType.WindowsNTGroup:
					this.ImageKey = ImageIndexes.WindowsGroupImgIdx;
					break;
				case MemberType.WindowsNTUser:
					this.ImageKey = ImageIndexes.WindowsUserImgIdx;
					break;
				case MemberType.DatabaseUser:
					this.ImageKey = ImageIndexes.DatabaseUserImgIdx;
					break;
			}

			this.SelectedImageKey = this.ImageKey;

			this.ListItemText = this.Text;
			this.FirstSubItemText = member.WhereDefined.ToString();
			this.SecondSubItemText = sMemberNonMember;
			this.ThirdSubItemText = member.SID.StringValue;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Este nodo no devuelve un listado de sus nodos hijos
		}
	}
}
