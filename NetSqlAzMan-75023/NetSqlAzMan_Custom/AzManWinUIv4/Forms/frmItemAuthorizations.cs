using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.DirectoryServices;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;

namespace NetSqlAzMan.SnapIn.Forms {
    public partial class frmItemAuthorizations : frmBase {
        internal IAzManItem item = null;

        internal ServiceBusinessObjects.AzManItem _item = null;
        private string _webApiUri;

        private DataTable dtAuthorizations;
        private bool modified;
        private string currentOwnerName = ((System.Threading.Thread.CurrentPrincipal.Identity as WindowsIdentity) ?? WindowsIdentity.GetCurrent()).Name;
        private IAzManSid currentOwnerSid = new SqlAzManSID(((System.Threading.Thread.CurrentPrincipal.Identity as WindowsIdentity) ?? WindowsIdentity.GetCurrent()).User.Value);
        private WhereDefined currentOwnerSidWhereDefined;

        public frmItemAuthorizations(string wau) {
            InitializeComponent();

            _webApiUri = wau;

            string memberName;
            bool isLocal;
            DirectoryServicesUtils.GetMemberInfo(this.currentOwnerSid.StringValue, out memberName, out isLocal);
            this.currentOwnerSidWhereDefined = isLocal ? WhereDefined.Local : WhereDefined.LDAP;
            this.dtAuthorizations = new DataTable();
            DataColumn dcAuthorizationId = new DataColumn("AuthorizationID", typeof(int));
            dcAuthorizationId.AutoIncrement = true;
            dcAuthorizationId.AutoIncrementSeed = -1;
            dcAuthorizationId.AutoIncrementStep = -1;
            dcAuthorizationId.AllowDBNull = false;
            dcAuthorizationId.Unique = true;
            DataColumn dcMemberTypeEnum = new DataColumn("MemberTypeEnum", typeof(MemberType));
            DataColumn dcMemberType = new DataColumn("MemberType", typeof(Bitmap));
            DataColumn dcOwner = new DataColumn("Owner", typeof(string));
            DataColumn dcOwnerSid = new DataColumn("OwnerSID", typeof(string));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            DataColumn dcObjectSid = new DataColumn("ObjectSID", typeof(string));
            DataColumn dcWhereDefined = new DataColumn("WhereDefined", typeof(string));
            DataColumn dcWhereDefinedEnum = new DataColumn("WhereDefinedEnum", typeof(WhereDefined));
            DataColumn dcAuthorizationType = new DataColumn("AuthorizationType", typeof(Bitmap));
            DataColumn dcAuthorizationTypeEnum = new DataColumn("AuthorizationTypeEnum", typeof(AuthorizationType));
            DataColumn dcValidFrom = new DataColumn("ValidFrom", typeof(DateTime));
            dcValidFrom.AllowDBNull = true;
            DataColumn dcValidTo = new DataColumn("ValidTo", typeof(DateTime));
            dcValidTo.AllowDBNull = true;
            #region ***PERSONALIZADO***
            DataColumn dcDomainProfile = new DataColumn("DomainProfile", typeof(string)) {
                AllowDBNull = true
            };
            DataColumn dcSamAccountName = new DataColumn("samAccountName", typeof(string)) {
                AllowDBNull = true,
            };
            DataColumn dcCn = new DataColumn("cn", typeof(string)) {
                AllowDBNull = true
            };
            DataColumn dcDisplayName = new DataColumn("displayName", typeof(string)) {
                AllowDBNull = true
            };
            DataColumn dcObjectSidString = new DataColumn("objectSidString", typeof(string)) {
                AllowDBNull = true
            };
            DataColumn dcDistinguishedName = new DataColumn("distinguishedName", typeof(string)) {
                AllowDBNull = true
            };
            DataColumn dcObjectClass = new DataColumn("objectClass", typeof(string)) {
                AllowDBNull = true
            };
            #endregion


            dcMemberType.Caption = Globalization.MultilanguageResource.GetString("DGColumn_10");
            dcOwner.Caption = Globalization.MultilanguageResource.GetString("DGColumn_20");
            dcOwnerSid.Caption = Globalization.MultilanguageResource.GetString("DGColumn_30");
            dcName.Caption = Globalization.MultilanguageResource.GetString("DGColumn_40");
            dcObjectSid.Caption = Globalization.MultilanguageResource.GetString("DGColumn_50");
            dcWhereDefined.Caption = Globalization.MultilanguageResource.GetString("DGColumn_60");
            dcAuthorizationType.Caption = Globalization.MultilanguageResource.GetString("DGColumn_70");
            dcValidFrom.Caption = Globalization.MultilanguageResource.GetString("DGColumn_80");
            dcValidTo.Caption = Globalization.MultilanguageResource.GetString("DGColumn_90");
            #region ***PERSONALIZADO***
            dcDomainProfile.Caption = "Perfil Dominio";
            dcSamAccountName.Caption = "samAccountName";
            dcCn.Caption = "cn";
            dcDisplayName.Caption = "Display Name";
            dcObjectSidString.Caption = "Object Sid";
            dcDistinguishedName.Caption = "Distinguished Name";
            dcObjectClass.Caption = "Class";
            #endregion

            this.dtAuthorizations.Columns.AddRange(new DataColumn[] {
                dcAuthorizationId,
                dcMemberType,
                dcName,
                dcAuthorizationType,
                dcWhereDefined,
                dcOwner,
                dcOwnerSid,
                dcValidFrom,
                dcValidTo,
                dcObjectSid,
                dcAuthorizationTypeEnum,
                dcWhereDefinedEnum,
                dcMemberTypeEnum,
                #region ***PERSONALIZADO***
                dcDomainProfile,
                dcSamAccountName,
                dcCn,
                dcDisplayName,
                dcObjectSidString,
                dcDistinguishedName,
                dcObjectClass
                #endregion
            });

            this.modified = false;
        }

        private void frmAuthorizations_Load(object sender, EventArgs e) {
            this.DialogResult = DialogResult.None;
            //this.btnAddStoreGroups.Enabled = this.btnAddStoreGroups.Enabled = this.item.Application.Store.HasStoreGroups();
            this.btnAddStoreGroups.Enabled = true;
            //this.btnAddApplicationGroups.Enabled = this.item.Application.HasApplicationGroups();
            this.btnAddApplicationGroups.Enabled = true;
            //Prepare DataGridView
            this.dgAuthorizations.DataSource = this.dtAuthorizations;
            this.dgAuthorizations.Columns["MemberTypeEnum"].Visible = false;
            this.dgAuthorizations.Columns["AuthorizationID"].Visible = false;
            this.dgAuthorizations.Columns["OwnerSID"].Visible = false;
            this.dgAuthorizations.Columns["ObjectSID"].Visible = false;
            this.dgAuthorizations.Columns["WhereDefinedEnum"].Visible = false;
            this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Visible = false;

            this.dgAuthorizations.Columns["samAccountName"].Visible = false;
            this.dgAuthorizations.Columns["cn"].Visible = false;
            this.dgAuthorizations.Columns["displayName"].Visible = false;
            this.dgAuthorizations.Columns["objectSidString"].Visible = false;
            this.dgAuthorizations.Columns["distinguishedName"].Visible = false;
            this.dgAuthorizations.Columns["objectClass"].Visible = false;

            foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                dgvc.Resizable = DataGridViewTriState.True;
                dgvc.ReadOnly = true;
                dgvc.HeaderText = this.dtAuthorizations.Columns[dgvc.Name].Caption;
            }
            this.dgAuthorizations.Columns["WhereDefined"].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.dgAuthorizations.Columns["MemberType"].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.dgAuthorizations.Columns["AuthorizationType"].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.dgAuthorizations.Columns["ValidFrom"].ReadOnly = false;
            this.dgAuthorizations.Columns["ValidTo"].ReadOnly = false;
            this.RenderItemAuthorizations();
            this.Text += " - " + this._item.Name;
            this.allowWithDelegationToolStripMenuItem.Text = Globalization.MultilanguageResource.GetString("Domain_AllowWithDelegation");
            this.allowToolStripMenuItem.Text = Globalization.MultilanguageResource.GetString("Domain_Allow");
            this.denyToolStripMenuItem.Text = Globalization.MultilanguageResource.GetString("Domain_Deny");
            this.neutralToolStripMenuItem.Text = Globalization.MultilanguageResource.GetString("Domain_Neutral");
            NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
        }

        private void AddAuthorizationDataRow(ServiceBusinessObjects.AzManAuthorization authorization) {
            DataRow dr = this.dtAuthorizations.NewRow();
            dr["AuthorizationID"] = authorization.AuthorizationId;

            string displayName = string.Empty;
            ServiceBusinessObjects.MemberType memberType;
            AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo> _h = null;
            Dictionary<string, IEnumerable<object>> _return = null;
            ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo _info = null;
            if (authorization.SidWhereDefined != ServiceBusinessObjects.WhereDefined.LDAP) {
                #region Call WebApi
                _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo>(_webApiUri);
                _return = Task.Run(() => _h.GetMemberOrOwnerInfoAsync(authorization.GetStoreName(), authorization.GetApplicationName(), authorization.Item.Name, authorization.AuthorizationId, true)).Result;
                if (_h.IsResponseContentError(_return))
                    _h.ThrowWebApiRequestException(_return);
                else
                    _info = _h.GetSBOFromReturnedContent(_return);
                #endregion            
                displayName = _info.DisplayName;
                memberType = _info.MemberType;
            }
            else {
                displayName = authorization.LdapDescription;
                memberType = authorization.objectClass.Equals("group", StringComparison.OrdinalIgnoreCase) ? ServiceBusinessObjects.MemberType.WindowsNTGroup : ServiceBusinessObjects.MemberType.WindowsNTUser;
            }

            ////========================================================
            ////VBASTIDAS: Optimizar como se asigna el Owner para que no se use el Namespace
            ////System.SEcurity.Principal y pueda ser removido del proyecto.
            ////========================================================
            //#region Call WebApi
            //_h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo>(_webApiUri);
            //_return = Task.Run(() => _h.GetMemberOrOwnerInfoAsync(authorization.GetStoreName(), authorization.GetApplicationName(), authorization.Item.Name, authorization.AuthorizationId, false)).Result;
            //if (_h.IsResponseContentError(_return))
            //    _h.ThrowWebApiRequestException(_return);
            //else
            //    _info = _h.GetSBOFromReturnedContent(_return);
            //#endregion
            //string ownerDisplayName = _info.DisplayName;
            //ServiceBusinessObjects.MemberType ownerMemberType = _info.MemberType;

            dr["MemberType"] = this.RenderMemberType(memberType, authorization.SID);
            dr["MemberTypeEnum"] = memberType;
            //dr["Owner"] = ownerDisplayName;
            dr["Owner"] = authorization.Owner.StringValue;
            dr["Name"] = displayName;
            dr["OwnerSID"] = authorization.Owner;
            if (authorization.SidWhereDefined == ServiceBusinessObjects.WhereDefined.Database) {
                dr["ObjectSID"] = new SqlAzManSID(authorization.SID.BinaryValue, true);
            }
            else {
                dr["ObjectSID"] = authorization.SID;
            }

            switch (authorization.SidWhereDefined) {
                case ServiceBusinessObjects.WhereDefined.LDAP:
                    dr["WhereDefined"] = Globalization.MultilanguageResource.GetString("WhereDefined_LDAP");
                    break;
                case ServiceBusinessObjects.WhereDefined.Local:
                    dr["WhereDefined"] = Globalization.MultilanguageResource.GetString("WhereDefined_Local");
                    break;
                case ServiceBusinessObjects.WhereDefined.Database:
                    dr["WhereDefined"] = Globalization.MultilanguageResource.GetString("WhereDefined_DB");
                    break;
                case ServiceBusinessObjects.WhereDefined.Store:
                    dr["WhereDefined"] = Globalization.MultilanguageResource.GetString("WhereDefined_Store");
                    break;
                case ServiceBusinessObjects.WhereDefined.Application:
                    dr["WhereDefined"] = Globalization.MultilanguageResource.GetString("WhereDefined_Application");
                    break;
            }

            dr["WhereDefinedEnum"] = authorization.SidWhereDefined;
            dr["AuthorizationType"] = this.RenderAuthorizationType(authorization.AuthorizationType);
            dr["AuthorizationTypeEnum"] = authorization.AuthorizationType;
            dr["ValidFrom"] = authorization.ValidFrom.HasValue ? (object)authorization.ValidFrom.Value : DBNull.Value;
            dr["ValidTo"] = authorization.ValidTo.HasValue ? (object)authorization.ValidTo.Value : DBNull.Value;
            #region ***PERSONALIZADO***
            dr["DomainProfile"] = authorization.DomainProfile;
            dr["samAccountName"] = authorization.samAccountName;
            dr["cn"] = authorization.cn;
            dr["displayName"] = authorization.displayName;
            dr["objectSidString"] = authorization.objectSidString;
            dr["distinguishedName"] = authorization.distinguishedName;
            dr["objectClass"] = authorization.objectClass;
            #endregion

            this.dtAuthorizations.Rows.Add(dr);
        }

        private DataRow AddDBUserDataRow(ServiceBusinessObjects.AzManDBUser member) {
            DataRow dr = this.dtAuthorizations.NewRow();
            dr["Owner"] = this.currentOwnerName;
            dr["OwnerSID"] = this.currentOwnerSid;
            dr["Name"] = member.UserName;
            dr["MemberType"] = this.RenderMemberType(ServiceBusinessObjects.MemberType.DatabaseUser, member.CustomSid);
            dr["MemberTypeEnum"] = ServiceBusinessObjects.MemberType.DatabaseUser;
            dr["ObjectSID"] = member.CustomSid.StringValue;
            dr["WhereDefined"] = ServiceBusinessObjects.WhereDefined.Database.ToString();
            dr["WhereDefinedEnum"] = ServiceBusinessObjects.WhereDefined.Database;
            dr["AuthorizationType"] = this.RenderAuthorizationType(ServiceBusinessObjects.AuthorizationType.Neutral);
            dr["AuthorizationTypeEnum"] = ServiceBusinessObjects.AuthorizationType.Neutral;
            dr["ValidFrom"] = DBNull.Value;
            dr["ValidTo"] = DBNull.Value;
            this.dtAuthorizations.Rows.Add(dr);
            return dr;
        }

        private DataRow AddStoreDataRow(ServiceBusinessObjects.AzManStoreGroup member) {
            DataRow dr = this.dtAuthorizations.NewRow();
            dr["Owner"] = this.currentOwnerName;
            dr["OwnerSID"] = this.currentOwnerSid;
            dr["Name"] = member.Name;
            dr["MemberType"] = this.RenderMemberType(ServiceBusinessObjects.MemberType.StoreGroup, member.SID);
            dr["MemberTypeEnum"] = ServiceBusinessObjects.MemberType.StoreGroup;
            dr["ObjectSID"] = member.SID;
            dr["WhereDefined"] = ServiceBusinessObjects.WhereDefined.Store.ToString();
            dr["WhereDefinedEnum"] = ServiceBusinessObjects.WhereDefined.Store;
            dr["AuthorizationType"] = this.RenderAuthorizationType(ServiceBusinessObjects.AuthorizationType.Neutral);
            dr["AuthorizationTypeEnum"] = ServiceBusinessObjects.AuthorizationType.Neutral;
            dr["ValidFrom"] = DBNull.Value;
            dr["ValidTo"] = DBNull.Value;
            this.dtAuthorizations.Rows.Add(dr);
            return dr;
        }

        private DataRow AddApplicationDataRow(ServiceBusinessObjects.AzManApplicationGroup member) {
            DataRow dr = this.dtAuthorizations.NewRow();
            dr["Owner"] = this.currentOwnerName;
            dr["OwnerSID"] = this.currentOwnerSid;
            dr["Name"] = member.Name;
            dr["MemberType"] = this.RenderMemberType(ServiceBusinessObjects.MemberType.ApplicationGroup, member.SID);
            dr["MemberTypeEnum"] = ServiceBusinessObjects.MemberType.ApplicationGroup;
            dr["ObjectSID"] = member.SID;
            dr["WhereDefined"] = ServiceBusinessObjects.WhereDefined.Application.ToString();
            dr["WhereDefinedEnum"] = ServiceBusinessObjects.WhereDefined.Application;
            dr["AuthorizationType"] = this.RenderAuthorizationType(ServiceBusinessObjects.AuthorizationType.Neutral);
            dr["AuthorizationTypeEnum"] = ServiceBusinessObjects.AuthorizationType.Neutral;
            dr["ValidFrom"] = DBNull.Value;
            dr["ValidTo"] = DBNull.Value;
            this.dtAuthorizations.Rows.Add(dr);
            return dr;
        }

        private DataRow AddLDapDataRow(ServiceBusinessObjects.AzManGenericMember member, bool isAGroup) {
            DataRow dr = this.dtAuthorizations.NewRow();
            dr["Owner"] = this.currentOwnerName;
            dr["OwnerSID"] = this.currentOwnerSid;
            dr["Name"] = member.Name;
            ServiceBusinessObjects.MemberType memberType = isAGroup ? ServiceBusinessObjects.MemberType.WindowsNTGroup : ServiceBusinessObjects.MemberType.WindowsNTUser;
            dr["MemberType"] = this.RenderMemberType(memberType, member.sid);
            dr["MemberTypeEnum"] = memberType;
            dr["ObjectSID"] = member.sid;
            dr["WhereDefined"] = member.WhereDefined.ToString();
            dr["WhereDefinedEnum"] = member.WhereDefined;
            dr["AuthorizationType"] = this.RenderAuthorizationType(member.authorizationType);
            dr["AuthorizationTypeEnum"] = member.authorizationType;
            dr["ValidFrom"] = member.validFrom.HasValue ? (object)member.validFrom.Value : DBNull.Value;
            dr["ValidTo"] = member.validTo.HasValue ? (object)member.validTo.Value : DBNull.Value;
            #region ***PERSONALIZADO***
            dr["DomainProfile"] = member.DomainProfile;
            dr["samAccountName"] = member.samAccountName;
            dr["cn"] = member.cn;
            dr["displayName"] = member.displayName;
            dr["objectSidString"] = member.objectSidString;
            dr["distinguishedName"] = member.distinguishedName;
            dr["objectClass"] = member.objectClass;
            #endregion
            this.dtAuthorizations.Rows.Add(dr);
            //Adjust columns Width
            foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            }
            return dr;
        }

        private void RenderItemAuthorizations() {
            this.HourGlass(true);
            this.dtAuthorizations.Rows.Clear();

            IEnumerable<ServiceBusinessObjects.AzManAuthorization> _authorizations = null;
            #region Call WebApi
            var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
            var _return = Task.Run(() => _h.GetAllByItemAsync(_item.GetStoreName(), _item.GetApplicationName(), _item.Name, false)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _authorizations = _h.GetEnumerableSBOFromReturnedContent(_return);
            #endregion
            foreach (ServiceBusinessObjects.AzManAuthorization authorization in _authorizations) {
                AddAuthorizationDataRow(authorization);
            }
            ////OLD Logic
            //IAzManAuthorization[] authorizations = this.item.GetAuthorizations();
            //foreach (IAzManAuthorization authorization in authorizations) {
            //	AddAuthorizationDataRow(authorization);
            //}

            this.dtAuthorizations.AcceptChanges();
            this.btnAttributes.Enabled = this.dtAuthorizations.Rows.Count > 0;
            this.modified = false;
            this.btnApply.Enabled = false;
            //Adjust columns Width
            foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            }
            if (!this._item.Application.IAmManager) {
                this.contextMenuStrip1.Enabled =
                     this.btnAddStoreGroups.Enabled = this.btnAddApplicationGroups.Enabled = this.btnAddWindowsUsersAndGroups.Enabled = this.btnAddDBUsers.Enabled =
                     this.btnOK.Enabled = this.btnApply.Enabled = this.btnRemove.Enabled = false;
                this.dgAuthorizations.ReadOnly = true;
            }
            this.HourGlass(false);
        }

        protected void HourGlass(bool switchOn) {
            this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
            /*Application.DoEvents();*/
        }

        protected void ShowError(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowInfo(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowWarning(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void frmAuthorizations_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Cancel;
        }

        private void btnAddApplicationGroups_Click(object sender, EventArgs e) {
            try {
                var frm = new frmApplicationGroupsList(_webApiUri);
                frm._application = this._item.Application;
                frm._applicationGroup = null;
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    List<DataRow> rowsAdded = new List<DataRow>();
                    foreach (ServiceBusinessObjects.AzManApplicationGroup ag in frm._selectedApplicationGroups) {
                        rowsAdded.Add(this.AddApplicationDataRow(ag));
                        this.modified = true;
                    }
                    this.SelectDataGridViewRows(rowsAdded);
                }
                this.btnApply.Enabled = this.modified;
                //Adjust columns Width
                foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                    dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                }
                this.HourGlass(false);
            }
            finally {
                this.btnApply.Enabled = this.modified;
            }

            //try {
            //	frmApplicationGroupsList frm = new frmApplicationGroupsList("SSSS");
            //	frm.application = this.item.Application;
            //	frm.applicationGroup = null;
            //	DialogResult dr = frm.ShowDialog(this);
            //	if (dr == DialogResult.OK) {
            //		List<DataRow> rowsAdded = new List<DataRow>();
            //		foreach (IAzManApplicationGroup ag in frm.selectedApplicationGroups) {
            //			rowsAdded.Add(this.AddApplicationDataRow(ag));
            //			this.modified = true;
            //		}
            //		this.SelectDataGridViewRows(rowsAdded);
            //	}
            //	this.btnApply.Enabled = this.modified;
            //	//Adjust columns Width
            //	foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
            //		dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            //	}
            //	this.HourGlass(false);
            //}
            //finally {
            //	this.btnApply.Enabled = this.modified;
            //}
        }

        private void btnAddStoreGroups_Click(object sender, EventArgs e) {
            var frm = new frmStoreGroupsList(_webApiUri);
            frm._store = this._item.Application.Store;
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.OK) {
                List<DataRow> rowsAdded = new List<DataRow>();
                foreach (var sg in frm._selectedStoreGroups) {
                    rowsAdded.Add(this.AddStoreDataRow(sg));
                    this.modified = true;
                }
                this.SelectDataGridViewRows(rowsAdded);
            }
            this.btnApply.Enabled = this.modified;
            //Adjust columns Width
            foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            }

            this.HourGlass(false);

            //frmStoreGroupsList frm = new frmStoreGroupsList();
            //frm.store = this.item.Application.Store;
            //DialogResult dr = frm.ShowDialog(this);
            //if (dr == DialogResult.OK) {
            //	List<DataRow> rowsAdded = new List<DataRow>();
            //	foreach (IAzManStoreGroup sg in frm.selectedStoreGroups) {
            //		rowsAdded.Add(this.AddStoreDataRow(sg));
            //		this.modified = true;
            //	}
            //	this.SelectDataGridViewRows(rowsAdded);
            //}
            //this.btnApply.Enabled = this.modified;
            ////Adjust columns Width
            //foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
            //	dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            //}
            //this.HourGlass(false);
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                foreach (DataGridViewRow dgViewRow in this.dgAuthorizations.SelectedRows) {
                    ((DataRowView)dgViewRow.DataBoundItem).Row.Delete();
                    this.modified = true;
                }

                if (this.dtAuthorizations.Rows.Count == 0)
                    this.btnRemove.Enabled = false;

                this.btnApply.Enabled = this.modified;
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                this.CommitChanges();
                this.btnApply.Enabled = false;
                this.DialogResult = DialogResult.OK;
                this.HourGlass(false);
            }
            catch (Exception ex) {
                this.HourGlass(false);
                this.DialogResult = DialogResult.None;
                this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
            }
        }

        private void btnApply_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                this.CommitChanges();
                this.btnApply.Enabled = false;
                this.HourGlass(false);
            }
            catch (Exception ex) {
                this.HourGlass(false);
                this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
            }
        }

        private void dgAuthorizations_SelectionChanged(object sender, EventArgs e) {
            if (this.dgAuthorizations.SelectedRows.Count == 0) {
                this.btnRemove.Enabled = this.btnAttributes.Enabled = false;
                this.dgAuthorizations.ContextMenu = null;
            }
            else if (this.dgAuthorizations.SelectedRows.Count == 1) {
                this.btnRemove.Enabled = this.btnAttributes.Enabled = true && this._item.Application.IAmManager;
                this.dgAuthorizations.ContextMenuStrip = this.contextMenuStrip1;
            }
            else {
                this.btnRemove.Enabled = true && this._item.Application.IAmManager;
                this.btnAttributes.Enabled = false;
                this.dgAuthorizations.ContextMenuStrip = this.contextMenuStrip1;
            }
        }

        private void dgAuthorizations_CellClick(object sender, DataGridViewCellEventArgs e) {
            //Authorization Type
            if (e.ColumnIndex != -1 && this.dgAuthorizations.Columns[e.ColumnIndex].Name == "AuthorizationType" && e.RowIndex >= 0 && this._item.Application.IAmManager) {
                DataRow dr = ((DataRowView)this.dgAuthorizations.Rows[e.RowIndex].DataBoundItem).Row;
                ServiceBusinessObjects.AuthorizationType newAuthorizationType = this.nextAuthorizationType(dr);
                this.modified = true;
                this.btnApply.Enabled = true;
            }
            else if (e.RowIndex == -1) /*Sort request*/{
                //AuthorizationType column
                if (this.dgAuthorizations.Columns[e.ColumnIndex].Name == "AuthorizationType") {
                    ListSortDirection sortDirection = ListSortDirection.Descending;
                    if (this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Tag == null) {
                        this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Tag = sortDirection;
                    }
                    else {
                        sortDirection = (ListSortDirection)this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Tag;
                    }
                    if (sortDirection == ListSortDirection.Ascending)
                        sortDirection = ListSortDirection.Descending;
                    else
                        sortDirection = ListSortDirection.Ascending;
                    this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Tag = sortDirection;
                    this.dgAuthorizations.Sort(this.dgAuthorizations.Columns["AuthorizationTypeEnum"], sortDirection);
                }
                //WhereDefined column
                else if (this.dgAuthorizations.Columns[e.ColumnIndex].Name == "WhereDefined") {
                    ListSortDirection sortDirection = ListSortDirection.Descending;
                    if (this.dgAuthorizations.Columns["WhereDefinedEnum"].Tag == null) {
                        this.dgAuthorizations.Columns["WhereDefinedEnum"].Tag = sortDirection;
                    }
                    else {
                        sortDirection = (ListSortDirection)this.dgAuthorizations.Columns["WhereDefinedEnum"].Tag;
                    }
                    if (sortDirection == ListSortDirection.Ascending)
                        sortDirection = ListSortDirection.Descending;
                    else
                        sortDirection = ListSortDirection.Ascending;
                    this.dgAuthorizations.Columns["WhereDefinedEnum"].Tag = sortDirection;
                    this.dgAuthorizations.Sort(this.dgAuthorizations.Columns["WhereDefinedEnum"], sortDirection);
                }
                //MemberType column
                else if (this.dgAuthorizations.Columns[e.ColumnIndex].Name == "MemberType") {
                    ListSortDirection sortDirection = ListSortDirection.Descending;
                    if (this.dgAuthorizations.Columns["MemberTypeEnum"].Tag == null) {
                        this.dgAuthorizations.Columns["MemberTypeEnum"].Tag = sortDirection;
                    }
                    else {
                        sortDirection = (ListSortDirection)this.dgAuthorizations.Columns["MemberTypeEnum"].Tag;
                    }
                    if (sortDirection == ListSortDirection.Ascending)
                        sortDirection = ListSortDirection.Descending;
                    else
                        sortDirection = ListSortDirection.Ascending;
                    this.dgAuthorizations.Columns["MemberTypeEnum"].Tag = sortDirection;
                    this.dgAuthorizations.Sort(this.dgAuthorizations.Columns["MemberTypeEnum"], sortDirection);
                }
                else {
                    this.dgAuthorizations.Columns["AuthorizationTypeEnum"].Tag = null;
                    this.dgAuthorizations.Columns["WhereDefinedEnum"].Tag = null;
                    this.dgAuthorizations.Columns["MemberTypeEnum"].Tag = null;
                }
            }
        }

        private ServiceBusinessObjects.AuthorizationType nextAuthorizationType(DataRow dr) {
            ServiceBusinessObjects.AuthorizationType authorizationType = (ServiceBusinessObjects.AuthorizationType)dr["AuthorizationTypeEnum"];
            ServiceBusinessObjects.AuthorizationType newAuthorizationType = authorizationType;
            switch (authorizationType) {
                case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation:
                    newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Allow;
                    break;
                case ServiceBusinessObjects.AuthorizationType.Allow:
                    newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Deny;
                    break;
                case ServiceBusinessObjects.AuthorizationType.Deny:
                    newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Neutral;
                    break;
                case ServiceBusinessObjects.AuthorizationType.Neutral:
                    newAuthorizationType = ServiceBusinessObjects.AuthorizationType.AllowWithDelegation;
                    break;
            }
            dr.BeginEdit();
            dr["AuthorizationType"] = this.RenderAuthorizationType(newAuthorizationType);
            dr["AuthorizationTypeEnum"] = newAuthorizationType;
            dr.EndEdit();

            return newAuthorizationType;
        }

        private Bitmap RenderAuthorizationType(ServiceBusinessObjects.AuthorizationType authorizationType) {
            switch (authorizationType) {
                case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation:
                    return Properties.Resources.AllowForDelegation;
                case ServiceBusinessObjects.AuthorizationType.Allow:
                    return Properties.Resources.Allow;
                case ServiceBusinessObjects.AuthorizationType.Deny:
                    return Properties.Resources.Deny;
                default:
                case ServiceBusinessObjects.AuthorizationType.Neutral:
                    return Properties.Resources.Neutral;
            }
        }

        private Bitmap RenderMemberType(ServiceBusinessObjects.MemberType memberType, ServiceBusinessObjects.AzManSid sid) {
            switch (memberType) {
                case ServiceBusinessObjects.MemberType.StoreGroup:
                    ServiceBusinessObjects.AzManStoreGroup _storeGroup = null;
                    #region Call WebApi
                    var _hsg = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
                    var _return_sg = Task.Run(() => _hsg.GetBySidAsync(_item.Application.Store.Name, sid.BinaryValueToBase64String(), sid.StringValue, false)).Result;
                    if (_hsg.IsResponseContentError(_return_sg))
                        _hsg.ThrowWebApiRequestException(_return_sg);
                    else
                        _storeGroup = _hsg.GetSBOFromReturnedContent(_return_sg);
                    #endregion
                    //if (this.item.Application.Store.GetStoreGroup(sid).GroupType == GroupType.Basic)
                    if (_storeGroup.GroupType == ServiceBusinessObjects.GroupType.Basic)
                        return Properties.Resources.StoreApplicationGroup_16x16.ToBitmap();
                    else
                        return Properties.Resources.WindowsQueryLDAPGroup_16x16.ToBitmap();
                case ServiceBusinessObjects.MemberType.ApplicationGroup:
                    ServiceBusinessObjects.AzManApplicationGroup _appGroup = null;
                    #region Call WebApi
                    var _hag = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
                    var _return_ag = Task.Run(() => _hag.GetByApplicationGroupIdAsync(_item.Application.Store.Name, _item.Application.Name, sid.BinaryValueToBase64String(), sid.StringValue, false)).Result;
                    if (_hag.IsResponseContentError(_return_ag))
                        _hag.ThrowWebApiRequestException(_return_ag);
                    else
                        _appGroup = _hag.GetSBOFromReturnedContent(_return_ag);
                    #endregion
                    //if (this.item.Application.GetApplicationGroup(sid).GroupType == GroupType.Basic)
                    if (_appGroup.GroupType == ServiceBusinessObjects.GroupType.Basic)
                        return Properties.Resources.StoreApplicationGroup_16x16.ToBitmap();
                    else
                        return Properties.Resources.WindowsQueryLDAPGroup_16x16.ToBitmap();
                case ServiceBusinessObjects.MemberType.WindowsNTUser:
                    return Properties.Resources.WindowsUser_16x16.ToBitmap();
                case ServiceBusinessObjects.MemberType.WindowsNTGroup:
                    return Properties.Resources.WindowsBasicGroup_16x16.ToBitmap();
                case ServiceBusinessObjects.MemberType.DatabaseUser:
                    return Properties.Resources.DBUser_16x16.ToBitmap();
                default:
                case ServiceBusinessObjects.MemberType.AnonymousSID:
                    return Properties.Resources.SIDNotFound_16x16.ToBitmap();
            }
        }

        private void dgAuthorizations_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (!this._item.Application.IAmManager)
                return;
            DataRow currentRow = ((DataRowView)this.dgAuthorizations.Rows[e.RowIndex].DataBoundItem).Row;
            if (currentRow["ValidFrom"] != DBNull.Value && currentRow["ValidTo"] != DBNull.Value) {
                DateTime validFrom = (DateTime)currentRow["ValidFrom"];
                DateTime validTo = (DateTime)currentRow["ValidTo"];
                if (validTo < validFrom) {
                    string error = Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg20");
                    currentRow.SetColumnError(this.dgAuthorizations.Columns[e.ColumnIndex].Name, error);
                }
                else {
                    currentRow.ClearErrors();
                }
            }
            else {
                currentRow.ClearErrors();
            }
            this.dgAuthorizations.Columns[e.ColumnIndex].Width = this.dgAuthorizations.Columns[e.ColumnIndex].GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            this.modified = true;
            this.btnApply.Enabled = true;
        }

        private void CommitChanges() {
            if (this.dtAuthorizations.HasErrors)
                throw new Exception(Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg30"));
            try {
                if (!this.modified)
                    return;

                // To Delete
                DataTable toDelete = this.dtAuthorizations.GetChanges(DataRowState.Deleted);
                if (toDelete != null) {
                    toDelete.RejectChanges();
                    ServiceBusinessObjects.AzManAuthorization _deleted = null;
                    foreach (DataRow dr in toDelete.Rows) {
                        #region Call WebApi
                        var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
                        var _return = Task.Run(() => _h.DeleteAsync((int)dr["AuthorizationID"], this._item.GetStoreName(), this._item.GetApplicationName(), this._item.Name)).Result;
                        if (_h.IsResponseContentError(_return))
                            _h.ThrowWebApiRequestException(_return);
                        else
                            _deleted = _h.GetSBOFromReturnedContent(_return);
                        #endregion
                    }
                }
                // To Add
                DataTable toAdd = this.dtAuthorizations.GetChanges(DataRowState.Added);
                if (toAdd != null) {
                    ServiceBusinessObjects.AzManAuthorization _created = null;
                    foreach (DataRow dr in toAdd.Rows) {
                        var _newAuthorization = new ServiceBusinessObjects.AzManAuthorization {
                            Owner = new ServiceBusinessObjects.AzManSid(dr["OwnerSID"].ToString(), this.currentOwnerSidWhereDefined == WhereDefined.Database),
                            OwnerSidWhereDefined = (ServiceBusinessObjects.WhereDefined)this.currentOwnerSidWhereDefined,
                            SID = new ServiceBusinessObjects.AzManSid(dr["ObjectSID"].ToString(), ((WhereDefined)dr["WhereDefinedEnum"]) == WhereDefined.Database),
                            SidWhereDefined = (ServiceBusinessObjects.WhereDefined)dr["WhereDefinedEnum"],
                            ValidFrom = (dr["ValidFrom"] != DBNull.Value ? (DateTime?)dr["ValidFrom"] : null),
                            ValidTo = (dr["ValidTo"] != DBNull.Value ? (DateTime?)dr["ValidTo"] : null),
                            Item = this._item.Clone(),
                            AuthorizationType = (ServiceBusinessObjects.AuthorizationType)dr["AuthorizationTypeEnum"],
                            DomainProfile = DBNull.Value.Equals(dr["DomainProfile"]) ? null : dr["DomainProfile"].ToString(),
                            samAccountName = DBNull.Value.Equals(dr["samAccountName"]) ? null : dr["samAccountName"].ToString(),
                            cn = DBNull.Value.Equals(dr["cn"]) ? null : dr["cn"].ToString(),
                            displayName = DBNull.Value.Equals(dr["displayName"]) ? null : dr["displayName"].ToString(),
                            objectSidString = DBNull.Value.Equals(dr["objectSidString"]) ? null : dr["objectSidString"].ToString(),
                            distinguishedName = DBNull.Value.Equals(dr["distinguishedName"]) ? null : dr["distinguishedName"].ToString(),
                            objectClass = DBNull.Value.Equals(dr["objectClass"]) ? null : dr["objectClass"].ToString()
                        };
                        #region Call WebApi
                        var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
                        var _return = Task.Run(() => _h.PostAsync(_newAuthorization)).Result;
                        if (_h.IsResponseContentError(_return))
                            _h.ThrowWebApiRequestException(_return);
                        else
                            _created = _h.GetSBOFromReturnedContent(_return);
                        #endregion
                        //Set new ID
                        DataRow originalRow = this.dtAuthorizations.Select("AuthorizationID=" + dr["AuthorizationID"].ToString())[0];
                        originalRow["AuthorizationID"] = _created.AuthorizationId;
                    }
                }
                // To Update
                DataTable toUpdate = this.dtAuthorizations.GetChanges(DataRowState.Modified);
                if (toUpdate != null) {
                    ServiceBusinessObjects.AzManAuthorization _modified = null;
                    foreach (DataRow dr in toUpdate.Rows) {
                        var _modifiedAuthorization = new ServiceBusinessObjects.AzManAuthorization {
                            AuthorizationId = (int)dr["AuthorizationID"],
                            Owner = new ServiceBusinessObjects.AzManSid(dr["OwnerSID"].ToString(), this.currentOwnerSidWhereDefined == WhereDefined.Database),
                            //OwnerSidWhereDefined = (ServiceBusinessObjects.WhereDefined)this.currentOwnerSidWhereDefined,
                            SID = new ServiceBusinessObjects.AzManSid(dr["ObjectSID"].ToString(), ((WhereDefined)dr["WhereDefinedEnum"]) == WhereDefined.Database),
                            SidWhereDefined = (ServiceBusinessObjects.WhereDefined)dr["WhereDefinedEnum"],
                            AuthorizationType = (ServiceBusinessObjects.AuthorizationType)dr["AuthorizationTypeEnum"],
                            ValidFrom = (dr["ValidFrom"] != DBNull.Value ? (DateTime?)dr["ValidFrom"] : null),
                            ValidTo = (dr["ValidTo"] != DBNull.Value ? (DateTime?)dr["ValidTo"] : null),
                            Item = this._item.Clone()
                        };
                        //this.item.GetAuthorization((int)dr["AuthorizationID"]).Update( new SqlAzManSID((string)dr["OwnerSID"], this.currentOwnerSidWhereDefined == WhereDefined.Database), new SqlAzManSID((string)dr["ObjectSID"], ((WhereDefined)dr["WhereDefinedEnum"]) == WhereDefined.Database), (WhereDefined)dr["WhereDefinedEnum"], (AuthorizationType)dr["AuthorizationTypeEnum"], (dr["ValidFrom"] != DBNull.Value ? (DateTime?)dr["ValidFrom"] : null), (dr["ValidTo"] != DBNull.Value ? (DateTime?)dr["ValidTo"] : null));

                        #region Call WebApi
                        var _h = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
                        var _return = Task.Run(() => _h.PutAsync(_modifiedAuthorization.AuthorizationId, _modifiedAuthorization)).Result;
                        if (_h.IsResponseContentError(_return))
                            _h.ThrowWebApiRequestException(_return);
                        else
                            _modified = _h.GetSBOFromReturnedContent(_return);
                        #endregion
                    }
                }

                this.modified = false;
                this.dtAuthorizations.AcceptChanges();

                //this.item.Application.Store.Storage.CommitTransaction();
            }
            catch {
                //this.item.Application.Store.Storage.RollBackTransaction();
                throw;
            }
            finally {
                this.btnApply.Enabled = this.modified;
            }

            //if (this.dtAuthorizations.HasErrors)
            //	throw new Exception(Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg30"));
            //try {
            //	if (!this.modified)
            //		return;
            //	//Application Group Properties
            //	this.item.Application.Store.Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
            //	// To Delete
            //	DataTable toDelete = this.dtAuthorizations.GetChanges(DataRowState.Deleted);
            //	if (toDelete != null) {
            //		toDelete.RejectChanges();
            //		foreach (DataRow dr in toDelete.Rows) {
            //			this.item.GetAuthorization((int)dr["AuthorizationID"]).Delete();
            //		}
            //	}
            //	// To Add
            //	DataTable toAdd = this.dtAuthorizations.GetChanges(DataRowState.Added);
            //	if (toAdd != null) {
            //		foreach (DataRow dr in toAdd.Rows) {
            //			#region ***PERSONALIZADO***
            //			IAzManAuthorization authorization = this.item.CreateAuthorizationCustom(
            //				 new SqlAzManSID((string)dr["OwnerSID"], this.currentOwnerSidWhereDefined == WhereDefined.Database),
            //				 this.currentOwnerSidWhereDefined,
            //				 (((WhereDefined)dr["WhereDefinedEnum"]) == WhereDefined.Database ?
            //				 new SqlAzManSID((string)dr["ObjectSID"], true) : new SqlAzManSID((string)dr["ObjectSID"], false)),
            //				 (WhereDefined)dr["WhereDefinedEnum"],
            //				 (AuthorizationType)dr["AuthorizationTypeEnum"],
            //				 (dr["ValidFrom"] != DBNull.Value ? (DateTime?)dr["ValidFrom"] : null),
            //				 (dr["ValidTo"] != DBNull.Value ? (DateTime?)dr["ValidTo"] : null),
            //				 (dr["DomainProfile"] != DBNull.Value ? dr["DomainProfile"].ToString() : null));
            //			#endregion
            //			DataRow originalRow = this.dtAuthorizations.Select("AuthorizationID=" + dr["AuthorizationID"].ToString())[0];
            //			originalRow["AuthorizationID"] = authorization.AuthorizationId;
            //		}
            //	}
            //	// To Update
            //	DataTable toUpdate = this.dtAuthorizations.GetChanges(DataRowState.Modified);
            //	if (toUpdate != null) {
            //		foreach (DataRow dr in toUpdate.Rows) {
            //			this.item.GetAuthorization((int)dr["AuthorizationID"]).Update(
            //				 new SqlAzManSID((string)dr["OwnerSID"], this.currentOwnerSidWhereDefined == WhereDefined.Database),
            //				 new SqlAzManSID((string)dr["ObjectSID"], ((WhereDefined)dr["WhereDefinedEnum"]) == WhereDefined.Database),
            //				 (WhereDefined)dr["WhereDefinedEnum"],
            //				 (AuthorizationType)dr["AuthorizationTypeEnum"],
            //				 (dr["ValidFrom"] != DBNull.Value ? (DateTime?)dr["ValidFrom"] : null),
            //				 (dr["ValidTo"] != DBNull.Value ? (DateTime?)dr["ValidTo"] : null));
            //		}
            //	}

            //	this.modified = false;
            //	this.dtAuthorizations.AcceptChanges();
            //	this.item.Application.Store.Storage.CommitTransaction();
            //}
            //catch {
            //	this.item.Application.Store.Storage.RollBackTransaction();
            //	throw;
            //}
            //finally {
            //	this.btnApply.Enabled = this.modified;
            //}
        }

        private void allowWithDelegationToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow drv in this.dgAuthorizations.SelectedRows) {
                DataRow dr = ((DataRowView)drv.DataBoundItem).Row;
                var newAuthorizationType = ServiceBusinessObjects.AuthorizationType.AllowWithDelegation;
                dr.BeginEdit();
                dr["AuthorizationType"] = this.RenderAuthorizationType(newAuthorizationType);
                dr["AuthorizationTypeEnum"] = newAuthorizationType;
                dr.EndEdit();
                this.modified = true;
                this.btnApply.Enabled = true;
            }
        }

        private void allowToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow drv in this.dgAuthorizations.SelectedRows) {
                DataRow dr = ((DataRowView)drv.DataBoundItem).Row;
                var newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Allow;
                dr.BeginEdit();
                dr["AuthorizationType"] = this.RenderAuthorizationType(newAuthorizationType);
                dr["AuthorizationTypeEnum"] = newAuthorizationType;
                dr.EndEdit();
                this.modified = true;
                this.btnApply.Enabled = true;
            }
        }

        private void denyToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow drv in this.dgAuthorizations.SelectedRows) {
                DataRow dr = ((DataRowView)drv.DataBoundItem).Row;
                var newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Deny;
                dr.BeginEdit();
                dr["AuthorizationType"] = this.RenderAuthorizationType(newAuthorizationType);
                dr["AuthorizationTypeEnum"] = newAuthorizationType;
                dr.EndEdit();
                this.modified = true;
                this.btnApply.Enabled = true;
            }
        }

        private void neutralToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow drv in this.dgAuthorizations.SelectedRows) {
                DataRow dr = ((DataRowView)drv.DataBoundItem).Row;
                var newAuthorizationType = ServiceBusinessObjects.AuthorizationType.Neutral;
                dr.BeginEdit();
                dr["AuthorizationType"] = this.RenderAuthorizationType(newAuthorizationType);
                dr["AuthorizationTypeEnum"] = newAuthorizationType;
                dr.EndEdit();
                this.modified = true;
                this.btnApply.Enabled = true;
            }
        }

        private void SelectDataGridViewRows(List<DataRow> rows) {
            this.dgAuthorizations.SuspendLayout();
            foreach (DataGridViewRow dgvr in this.dgAuthorizations.Rows) {
                DataRow gridRow = ((DataRowView)dgvr.DataBoundItem).Row;
                dgvr.Selected = false;
                foreach (DataRow dr in rows) {
                    if (gridRow == dr) {
                        dgvr.Selected = true;
                        rows.Remove(dr);
                        break;
                    }
                }
            }
            this.dgAuthorizations.ResumeLayout();
        }

        private void btnAttributes_Click(object sender, EventArgs e) {
            DialogResult dr = DialogResult.Yes;
            if (this.modified) {
                dr = MessageBox.Show(Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg40"), Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Tit40"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            if (dr == DialogResult.Yes) {
                if (this.modified) {
                    try {
                        this.HourGlass(true);
                        this.CommitChanges();
                        this.btnApply.Enabled = false;
                        this.HourGlass(false);
                    }
                    catch (Exception ex) {
                        this.HourGlass(false);
                        this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
                        return;
                    }
                }
                try {
                    DataRow dataRow = ((DataRowView)this.dgAuthorizations.SelectedRows[0].DataBoundItem).Row;
                    frmAuthorizationAttributes frm = new frmAuthorizationAttributes();
                    frm.Text += " - " + this._item.Name;
                    frm.authorization = this.item.GetAuthorization((int)dataRow["AuthorizationID"]);
                    frm.ShowDialog(this);
                    /*Application.DoEvents();*/
                    frm.Dispose();
                    /*Application.DoEvents();*/
                }
                catch (Exception ex) {
                    this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg50"));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgAuthorizations_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            //DateTime syntax check
            string columnName = this.dgAuthorizations.Columns[e.ColumnIndex].Name;
            this.ShowError(String.Format(Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg60") + " {0}", columnName), columnName);
            e.ThrowException = false;
        }

        private void btnAddDBUsers_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                var frm = new frmDBUsersList(_webApiUri);
                frm._application = this._item.Application;
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    List<DataRow> rowsAdded = new List<DataRow>();
                    foreach (ServiceBusinessObjects.AzManDBUser dbUser in frm._selectedDBUsers) {
                        rowsAdded.Add(this.AddDBUserDataRow(dbUser));
                        this.modified = true;
                    }
                    this.SelectDataGridViewRows(rowsAdded);
                }
                this.btnApply.Enabled = this.modified;
                //Adjust columns Width
                foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                    dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //try {
            //	this.HourGlass(true);
            //	frmDBUsersList frm = new frmDBUsersList();
            //	frm.application = this.item.Application;
            //	DialogResult dr = frm.ShowDialog(this);
            //	if (dr == DialogResult.OK) {
            //		List<DataRow> rowsAdded = new List<DataRow>();
            //		foreach (IAzManDBUser dbUser in frm.selectedDBUsers) {
            //			rowsAdded.Add(this.AddDBUserDataRow(dbUser));
            //			this.modified = true;
            //		}
            //		this.SelectDataGridViewRows(rowsAdded);
            //	}
            //	this.btnApply.Enabled = this.modified;
            //	//Adjust columns Width
            //	foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
            //		dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            //	}

            //}
            //finally {
            //	this.HourGlass(false);
            //}
        }

        private void btnAddWindowsUsersAndGroups_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this._item.Application.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Developer);
                if (res != null) {
                    List<DataRow> rowsAdded = new List<DataRow>();
                    foreach (ADObject o in res) {
                        //MessageBox.Show(string.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6}", o.ADSPath, o.ClassName, o.LDAPDomain, o.LDAPSid, o.Name, o.Sid, o.UPN));
                        ServiceBusinessObjects.WhereDefined wd = ServiceBusinessObjects.WhereDefined.LDAP;
                        if (!o.ADSPath.StartsWith("LDAP"))
                            wd = ServiceBusinessObjects.WhereDefined.Local;
                        string displayName;
                        bool isAGroup;
                        bool isLocal;
                        DirectoryServicesUtils.GetMemberInfo(o.Sid, out displayName, out isAGroup, out isLocal);
                        var gm = new ServiceBusinessObjects.AzManGenericMember(new ServiceBusinessObjects.AzManSid(o.Sid), wd, ServiceBusinessObjects.AuthorizationType.Neutral, null, null);
                        gm.Name = displayName;
                        rowsAdded.Add(this.AddLDapDataRow(gm, isAGroup));
                        this.modified = true;
                    }
                    this.SelectDataGridViewRows(rowsAdded);
                }
                this.btnApply.Enabled = this.modified;
                //Adjust columns Width
                foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                    dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                }
            }
            catch {
                throw;
                //this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemAuthorizations_Msg10"));
            }
            finally {
                this.HourGlass(false);
                this.btnApply.Enabled = this.modified;
            }
        }

        private void btnAddUsersAndGroupsLDAP_Click(object sender, EventArgs e) {
            var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
            if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            ADObject[] res = _f.SelectedADObjects;
            List<DataRow> rowsAdded = new List<DataRow>();
            foreach (ADObject o in res) {
                var wd = ServiceBusinessObjects.WhereDefined.LDAP;
                var gm = new ServiceBusinessObjects.AzManGenericMember(new ServiceBusinessObjects.AzManSid(o.LdapSid), wd, o.DomainProfile, o.samAccountName, o.cn, o.displayName, o.LdapSid, o.distinguishedName, o.ClassName, ServiceBusinessObjects.AuthorizationType.Neutral, null, null) {
                    Name = string.Format("{0}\\{1}", o.DomainProfile, o.Name)
                };
                rowsAdded.Add(this.AddLDapDataRow(gm, o.IsGroup));
                this.modified = true;
            }
            this.SelectDataGridViewRows(rowsAdded);
            this.btnApply.Enabled = this.modified;
            //Adjust columns Width
            foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
                dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            }

            //var _f = new AzManWinUI.LDAPServices.LDAPQueryClientUI(this.item.Application.Store.Storage);
            //if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //	return;

            //ADObject[] res = _f.SelectedADObjects;
            //List<DataRow> rowsAdded = new List<DataRow>();
            //foreach (ADObject o in res) {
            //	WhereDefined wd = WhereDefined.LDAP;
            //	GenericMember gm = new GenericMember(new SqlAzManSID(o.Sid), wd, o.DomainProfile, AuthorizationType.Neutral, null, null) {
            //		Name = string.Format("{0}\\{1}", o.DomainProfile, o.Name)
            //	};
            //	rowsAdded.Add(this.AddLDapDataRow(gm, o.IsGroup));
            //	this.modified = true;
            //}
            //this.SelectDataGridViewRows(rowsAdded);
            //this.btnApply.Enabled = this.modified;
            ////Adjust columns Width
            //foreach (DataGridViewColumn dgvc in this.dgAuthorizations.Columns) {
            //	dgvc.Width = dgvc.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            //}
        }
    }
}
