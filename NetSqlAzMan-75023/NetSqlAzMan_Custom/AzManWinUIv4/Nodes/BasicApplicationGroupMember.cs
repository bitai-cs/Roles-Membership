using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Globalization;
using System.Threading.Tasks;

namespace AzManWinUI.Nodes {
	public class BasicApplicationGroupMember : Basgosoft.ManagementConsoleLib.BaseNode {
		IAzManApplicationGroupMember member;
		NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember _member;

		private string _webApiUri;

		public BasicApplicationGroupMember(IAzManApplicationGroupMember applicationGroupMember, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			this.member = applicationGroupMember;

			this.createNodeActionButtons();

			this.renderNode();
		}

		public BasicApplicationGroupMember(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember applicationGroupMember, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = wau;

			this._member = applicationGroupMember;

			this.createNodeActionButtons();

			this.renderNode();
		}

		protected override void createNodeActionButtons() {
			//Este nodo no tiene acciones
		}

		protected override void renderNode() {
			string sMemberNonMember = _member.IsMember ? MultilanguageResource.GetString("Domain_Member") : MultilanguageResource.GetString("Domain_NonMember");

			NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo _memberInfo = null;
			string displayName;
			NetSqlAzMan.ServiceBusinessObjects.MemberType memberType;
            if (_member.WhereDefined != NetSqlAzMan.ServiceBusinessObjects.WhereDefined.LDAP) {
                #region Call WebApi
                var _h = new AzManWebApiClientHelpers.AzManApplicationGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo>(_webApiUri);
                var _return = Task.Run(() => _h.GetMemberIfoAsync(_member.ApplicationGroup.Application.Store.Name, _member.ApplicationGroup.Application.Name, _member.ApplicationGroup.Name, _member.ApplicationGroupMemberId)).Result;
                if (_h.IsResponseContentError(_return))
                    _h.ThrowWebApiRequestException(_return);
                else
                    _memberInfo = _h.GetSBOFromReturnedContent(_return);
                #endregion
                displayName = _memberInfo.DisplayName;
                memberType = _memberInfo.MemberType;
            }
            else {
                displayName = _member.LdapDescription;
                memberType = _member.objectClass.Equals("group", StringComparison.OrdinalIgnoreCase) ? NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTGroup : NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTUser;
            }

			this.Text = displayName;

			switch (memberType) {
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.AnonymousSID:
					this.ImageKey = ImageIndexes.SidNotFoundImgIdx;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.ApplicationGroup:
					NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup _nestedAppGroup = null;
					#region Get NESTED ApplicationGroup by name
					var _agh = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
					var _agh_return = Task.Run(() => _agh.GetByApplicationGroupIdAsync(_member.ApplicationGroup.Application.Store.Name, _member.ApplicationGroup.Application.Name, _member.SID.BinaryValueToBase64String(), _member.SID.StringValue, false)).Result;
					if (_agh.IsResponseContentError(_agh_return))
						_agh.ThrowWebApiRequestException(_agh_return);
					else
						_nestedAppGroup = _agh.GetSBOFromReturnedContent(_agh_return);
					#endregion
					//this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
					switch (_nestedAppGroup.GroupType) {
						//switch (member.ApplicationGroup.Application.GetApplicationGroup(member.SID).GroupType) {
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
							this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
							break;
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
							this.ImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
							break;
					}
					break;
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.StoreGroup:
					NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup _nestedStoreGroup = null;
					#region Get NESTED StoreGroup by name
					var _sgh = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
					var _sgh_return = Task.Run(() => _sgh.GetBySidAsync(_member.ApplicationGroup.Application.Store.Name, _member.SID.BinaryValueToBase64String(), _member.SID.StringValue, false)).Result;
					if (_sgh.IsResponseContentError(_sgh_return))
						_sgh.ThrowWebApiRequestException(_sgh_return);
					else
						_nestedStoreGroup = _sgh.GetSBOFromReturnedContent(_sgh_return);
					#endregion
					//this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;					
					switch (_nestedStoreGroup.GroupType) {
						//switch (member.ApplicationGroup.Application.Store.GetStoreGroup(member.SID).GroupType) {
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
							this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
							break;
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
							this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
							break;
					}
					break;
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTGroup:
					this.ImageKey = ImageIndexes.WindowsGroupImgIdx;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTUser:
					this.ImageKey = ImageIndexes.WindowsUserImgIdx;
					break;
				case NetSqlAzMan.ServiceBusinessObjects.MemberType.DatabaseUser:
					this.ImageKey = ImageIndexes.DatabaseUserImgIdx;
					break;
			}

			this.SelectedImageKey = this.ImageKey;

			this.ListItemText = this.Text;
			this.FirstSubItemText = _member.WhereDefined.ToString();
			this.SecondSubItemText = sMemberNonMember;
			this.ThirdSubItemText = _member.SID.StringValue;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			//Este nodo no devuelve un listado de sus nodos hijos
		}
	}
}