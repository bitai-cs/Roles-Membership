using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI.Nodes
{
	public class ItemAuthorizationMemberNode : BaseNode
	{
		private IAzManAuthorization authorization;

		public ItemAuthorizationMemberNode(IAzManAuthorization authorization, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			this.authorization = authorization;
			
			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons()
		{
		}

		protected override void renderNode()
		{
			string sAuthType;
			switch (this.authorization.AuthorizationType)
			{
				case AuthorizationType.Neutral:
					sAuthType = MultilanguageResource.GetString("Domain_Neutral");
					break;
				case AuthorizationType.Allow:
					sAuthType = MultilanguageResource.GetString("Domain_Allow");
					break;
				case AuthorizationType.AllowWithDelegation:
					sAuthType = MultilanguageResource.GetString("Domain_AllowWithDelegation");
					break;
				case AuthorizationType.Deny:
					sAuthType = MultilanguageResource.GetString("Domain_Deny");
					break;
				default:
					sAuthType = "???";
					break;
			}

			string displayName;
			MemberType memberType = this.authorization.GetMemberInfo(out displayName);
			string ownerName;
			MemberType ownerType = this.authorization.GetOwnerInfo(out ownerName);

			this.Text = displayName;

			switch (memberType)
			{
				case MemberType.AnonymousSID:
					this.ImageKey = ImageIndexes.SidNotFoundImgIdx;
					break;
				case MemberType.ApplicationGroup:

					if (this.authorization.Item.Application.GetApplicationGroup(this.authorization.SID).GroupType == GroupType.Basic)
					{
						this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
					}
					else
					{
						this.ImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
					}
					break;
				case MemberType.StoreGroup:
					if (this.authorization.Item.Application.Store.GetStoreGroup(authorization.SID).GroupType == GroupType.Basic)
					{
						this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
					}
					else
					{
						this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
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
			this.Tag = this.authorization;

			string sidWDS;
			switch (authorization.SidWhereDefined.ToString())
			{
				case "LDAP":
					sidWDS = MultilanguageResource.GetString("WhereDefined_LDAP");
					break;
				case "Local":
					sidWDS = MultilanguageResource.GetString("WhereDefined_Local");
					break;
				case "Database":
					sidWDS = MultilanguageResource.GetString("WhereDefined_DB");
					break;
				case "Store":
					sidWDS = MultilanguageResource.GetString("WhereDefined_Store");
					break;
				case "Application":
					sidWDS = MultilanguageResource.GetString("WhereDefined_Application");
					break;
				default:
					sidWDS = "???";
					break;
			}

			this.ListItemText = this.Text;
			this.FirstSubItemText = sAuthType;
			this.SecondSubItemText = sidWDS;
			this.ThirdSubItemText = ownerName;
			this.FourthSubItemText = (this.authorization.ValidFrom.HasValue ? this.authorization.ValidFrom.Value.ToString() : String.Empty);
			this.FifthSubItemText = (this.authorization.ValidTo.HasValue ? this.authorization.ValidTo.Value.ToString() : String.Empty);
			this.SixthSubItemText = this.authorization.SID.StringValue;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Este nodo no devuelve nodos hijos
		}
	}
}
