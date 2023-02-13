using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using System.Threading.Tasks;

namespace AzManWinUI.Nodes {
    public class ItemAuthorizationMemberNode : BaseNode {
        //private IAzManAuthorization authorization;

        private string _webApiUri;
        private NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization _authorization;

        //public ItemAuthorizationMemberNode(IAzManAuthorization authorization, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
        //	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
        //	this.authorization = authorization;

        //	this.createNodeActionButtons();

        //	this.renderNode();
        //}

        public ItemAuthorizationMemberNode(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization authorization, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
            : base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
            _webApiUri = wau;

            this._authorization = authorization;

            this.createNodeActionButtons();

            this.renderNode();
        }

        protected override void createNodeActionButtons() {
        }

        protected override void renderNode() {
            string sAuthType = null;
            switch (this._authorization.AuthorizationType) {
                case NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Neutral:
                    sAuthType = MultilanguageResource.GetString("Domain_Neutral");
                    break;
                case NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Allow:
                    sAuthType = MultilanguageResource.GetString("Domain_Allow");
                    break;
                case NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.AllowWithDelegation:
                    sAuthType = MultilanguageResource.GetString("Domain_AllowWithDelegation");
                    break;
                case NetSqlAzMan.ServiceBusinessObjects.AuthorizationType.Deny:
                    sAuthType = MultilanguageResource.GetString("Domain_Deny");
                    break;
                default:
                    sAuthType = "???";
                    break;
            }

            ///OLD Logic
            //string displayName;
            //MemberType memberType = this.authorization.GetMemberInfo(out displayName);
            //string ownerName;
            //MemberType ownerType = this.authorization.GetOwnerInfo(out ownerName);			

            Dictionary<string, IEnumerable<object>> _return;
            string displayName;
            NetSqlAzMan.ServiceBusinessObjects.MemberType memberType;
            NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo _infoMember = null;
            if (_authorization.SidWhereDefined != NetSqlAzMan.ServiceBusinessObjects.WhereDefined.LDAP) {
                var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo>(_webApiUri);
                #region Call WebApi                
                _return = Task.Run(() => _h.GetMemberOrOwnerInfoAsync(_authorization.Item.Application.Store.Name, _authorization.Item.Application.Name, _authorization.Item.Name, _authorization.AuthorizationId, true)).Result;
                if (_h.IsResponseContentError(_return))
                    _h.ThrowWebApiRequestException(_return);
                else
                    _infoMember = _h.GetSBOFromReturnedContent(_return);
                #endregion
                displayName = _infoMember.DisplayName;
                memberType = _infoMember.MemberType;
            }
            else {
                displayName = _authorization.LdapDescription;
                memberType = _authorization.objectClass.Equals("group", StringComparison.OrdinalIgnoreCase) ? NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTGroup : NetSqlAzMan.ServiceBusinessObjects.MemberType.WindowsNTUser;
            }

            string ownerName;
            ////VBASTIDAS: No es necesario para esta versión obtener la info del 
            ////objectSid que creo la autorización, ya que la aplicación puede correr en
            ////diferentes PCs
            //NetSqlAzMan.ServiceBusinessObjects.MemberType ownerType;
            //NetSqlAzMan.ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo _infoOwner = null;
            //#region Call WebApi
            //_return = Task.Run(() => _h.GetMemberOrOwnerInfoAsync(_authorization.Item.Application.Store.Name, _authorization.Item.Application.Name, _authorization.Item.Name, _authorization.AuthorizationId, false)).Result;
            //if (_h.IsResponseContentError(_return))
            //    _h.ThrowWebApiRequestException(_return);
            //else
            //    _infoOwner = _h.GetSBOFromReturnedContent(_return);
            //#endregion
            //ownerName = _infoOwner.DisplayName;
            //ownerType = _infoOwner.MemberType;
            ownerName = _authorization.Owner.StringValue;

            this.Text = displayName;

            switch (memberType) {
                case NetSqlAzMan.ServiceBusinessObjects.MemberType.AnonymousSID:
                    this.ImageKey = ImageIndexes.SidNotFoundImgIdx;
                    break;
                case NetSqlAzMan.ServiceBusinessObjects.MemberType.ApplicationGroup:
                    NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup _appGroup = null;
                    #region Call WebApi
                    var _agh = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
                    var _return_ag = Task.Run(() => _agh.GetByApplicationGroupIdAsync(_authorization.Item.Application.Store.Name, _authorization.Item.Application.Name, this._authorization.SID.BinaryValueToBase64String(), this._authorization.SID.StringValue, false)).Result;
                    if (_agh.IsResponseContentError(_return_ag))
                        _agh.ThrowWebApiRequestException(_return_ag);
                    else
                        _appGroup = _agh.GetSBOFromReturnedContent(_return_ag);
                    #endregion

                    //if (this.authorization.Item.Application.GetApplicationGroup(this.authorization.SID).GroupType == GroupType.Basic) {
                    if (_appGroup.GroupType == NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic) {
                        this.ImageKey = ImageIndexes.ApplicationGroupBasicImgIdx;
                    }
                    else {
                        this.ImageKey = ImageIndexes.ApplicationGroupLDAPImgIdx;
                    }
                    break;
                case NetSqlAzMan.ServiceBusinessObjects.MemberType.StoreGroup:
                    NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup _storeGroup = null;
                    #region Call WebApi
                    var _sgh = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
                    var _return_sgg = Task.Run(() => _sgh.GetBySidAsync(_authorization.Item.Application.Store.Name, this._authorization.SID.BinaryValueToBase64String(), this._authorization.SID.StringValue, false)).Result;
                    if (_sgh.IsResponseContentError(_return_sgg))
                        _sgh.ThrowWebApiRequestException(_return_sgg);
                    else
                        _storeGroup = _sgh.GetSBOFromReturnedContent(_return_sgg);
                    #endregion

                    //if (this.authorization.Item.Application.Store.GetStoreGroup(authorization.SID).GroupType == GroupType.Basic) {
                    if (_storeGroup.GroupType == NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic) {
                        this.ImageKey = ImageIndexes.StoreGroupBasicImgIdx;
                    }
                    else {
                        this.ImageKey = ImageIndexes.StoreGroupLDAPImgIdx;
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
            this.Tag = this._authorization;

            string sidWDS;
            switch (_authorization.SidWhereDefined.ToString()) {
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
            //this.FourthSubItemText = (this.authorization.ValidFrom.HasValue ? this.authorization.ValidFrom.Value.ToString() : String.Empty);
            this.FourthSubItemText = (this._authorization.ValidFrom.HasValue ? this._authorization.ValidFrom.Value.ToString() : String.Empty);
            //this.FifthSubItemText = (this.authorization.ValidTo.HasValue ? this.authorization.ValidTo.Value.ToString() : String.Empty);
            this.FifthSubItemText = (this._authorization.ValidTo.HasValue ? this._authorization.ValidTo.Value.ToString() : String.Empty);
            this.SixthSubItemText = this._authorization.SID.StringValue;
        }

        protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
            //Este nodo no devuelve nodos hijos
        }
    }
}
