using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.DirectoryServices;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;

namespace NetSqlAzMan.SnapIn.Forms
{
    public partial class frmApplicationGroupsProperties :frmBase
    {
        //internal IAzManApplicationGroup applicationGroup;

        //private GenericMemberCollection MembersToAdd;
        //private GenericMemberCollection MembersToRemove;
        //private GenericMemberCollection NonMembersToAdd;
        //private GenericMemberCollection NonMembersToRemove;

        private ServiceBusinessObjects.AzManGenericMemberCollection MembersToAdd;
        private ServiceBusinessObjects.AzManGenericMemberCollection MembersToRemove;
        private ServiceBusinessObjects.AzManGenericMemberCollection NonMembersToAdd;
        private ServiceBusinessObjects.AzManGenericMemberCollection NonMembersToRemove;

        private string _webApiUri;
        internal ServiceBusinessObjects.AzManApplicationGroup _applicationGroup;

        private bool modified;
        private bool firstShow;

        public frmApplicationGroupsProperties(string wau) {
            InitializeComponent();

            this.firstShow = true;

            //this.MembersToAdd = new GenericMemberCollection();
            //this.MembersToRemove = new GenericMemberCollection();
            //this.NonMembersToAdd = new GenericMemberCollection();
            //this.NonMembersToRemove = new GenericMemberCollection();

            _webApiUri = wau;

            this.MembersToAdd = new ServiceBusinessObjects.AzManGenericMemberCollection();
            this.MembersToRemove = new ServiceBusinessObjects.AzManGenericMemberCollection();
            this.NonMembersToAdd = new ServiceBusinessObjects.AzManGenericMemberCollection();
            this.NonMembersToRemove = new ServiceBusinessObjects.AzManGenericMemberCollection();
        }

        private void PreSortListView(ListView lv) {
            lv.Sorting = SortOrder.Ascending;
        }
        private void SortListView(ListView lv) {
            if (lv.Sorting == SortOrder.Ascending)
                lv.Sorting = SortOrder.Descending;
            else
                lv.Sorting = SortOrder.Ascending;
            lv.Sort();
        }

        private void frmGroupsProperties_Load(object sender, EventArgs e) {
            this.DialogResult = DialogResult.None;
            this.txtName.Text = this._applicationGroup.Name;
            this.txtDescription.Text = this._applicationGroup.Description;
            this.txtGroupType.Text = (this._applicationGroup.GroupType == ServiceBusinessObjects.GroupType.Basic ? Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg10") : Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg20"));
            if (this._applicationGroup.GroupType == ServiceBusinessObjects.GroupType.Basic) {
                //this.btnMembersAddStoreGroups.Enabled = this.btnNonMembersAddStoreGroups.Enabled = this._applicationGroup.Application.Store.HasStoreGroups();
                this.btnMembersAddStoreGroups.Enabled = this.btnNonMembersAddStoreGroups.Enabled = true;
                //this.btnMembersAddApplicationGroups.Enabled = this.btnNonMembersAddApplicationGroup.Enabled = this._applicationGroup.Application.HasApplicationGroups();
                this.btnMembersAddApplicationGroups.Enabled = this.btnNonMembersAddApplicationGroup.Enabled = true;
                if (this.tabControl1.TabPages.Contains(this.tabLdapQuery))
                    this.tabControl1.TabPages.Remove(this.tabLdapQuery);
                this.lsvMembers.Items.Clear();
                this.lsvNonMembers.Items.Clear();
                this.picImage.Image = this.picBasic.Image;
                this.PreSortListView(this.lsvMembers);
                this.PreSortListView(this.lsvNonMembers);
            }
            else {
                if (this.tabControl1.Contains(this.tabMembers))
                    this.tabControl1.TabPages.Remove(this.tabMembers);
                if (this.tabControl1.Contains(this.tabNonMembers))
                    this.tabControl1.TabPages.Remove(this.tabNonMembers);
                this.picImage.Image = this.picLDap.Image;
            }
            this.RefreshApplicationGroupProperties();
            this.modified = false;
            this.btnApply.Enabled = false;
            //PorkAround: http://lab.msdn.microsoft.com/ProductFeedback/viewFeedback.aspx?feedbackId=FDBK49664
            ImageList clonedImageList = new ImageList();
            foreach (Image image in this.imageList1.Images) {
                clonedImageList.Images.Add((Image)image.Clone());
            }
            this.lsvMembers.SmallImageList = clonedImageList;
            this.lsvNonMembers.SmallImageList = clonedImageList;
            //PorkAround End
            /*Application.DoEvents();*/
            NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
        }

        private ListViewItem CreateStoreListViewItem(ServiceBusinessObjects.AzManStoreGroup member) {
            ListViewItem lvi = new ListViewItem();
            lvi.Tag = member;
            lvi.Text = member.Name;
            lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Store"));
            return lvi;
        }

        private ListViewItem CreateApplicationListViewItem(ServiceBusinessObjects.AzManApplicationGroup member) {
            ListViewItem lvi = new ListViewItem();
            lvi.Tag = member;
            lvi.Text = member.Name;
            lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Application"));
            return lvi;
        }

        private ListViewItem CreateLDapListViewItem(ServiceBusinessObjects.AzManApplicationGroupMember member) {
            ////VBASTIDAS: Ahora el meber almacena su información Ldap para no volver 
            ////a solicitar los datos al Web Api
            //var _return = this.getApplicationGroupMemberInfo(member.ApplicationGroupMemberId);

            ListViewItem lvi = new ListViewItem();
            lvi.Tag = member;
            lvi.Text = member.LdapDescription;
            lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_LDAP"));
            return lvi;
        }

        private ListViewItem CreateDBListViewItem(ServiceBusinessObjects.AzManApplicationGroupMember member) {
            ListViewItem lvi = new ListViewItem();
            if (member != null) {
                var _return = this.getApplicationGroupMemberInfo(member.ApplicationGroupMemberId);

                lvi.Tag = member;
                lvi.Text = _return.DisplayName;
                lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_DB"));
            }
            return lvi;
        }

        private ListViewItem CreateListViewItem(ServiceBusinessObjects.AzManGenericMember member) {
            ListViewItem lvi = new ListViewItem();
            if (member != null) {
                lvi.Tag = member;
                if (!string.IsNullOrEmpty(member.DomainProfile))
                    lvi.Text = string.Format("{0}\\{1}", member.DomainProfile, member.Name);
                else
                    lvi.Text = string.Format("{0}", member.Name);
                switch (member.WhereDefined.ToString()) {
                    case "LDAP":
                        lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_LDAP"));
                        break;
                    case "Local":
                        lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Local"));
                        break;
                    case "Database":
                        lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_DB"));
                        break;
                    case "Store":
                        lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Store"));
                        break;
                    case "Application":
                        lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Application"));
                        break;
                }
            }
            return lvi;
        }

        private void RemoveListViewItem(ref ListView lsv, ServiceBusinessObjects.AzManGenericMember member) {
            foreach (ListViewItem lvi in lsv.Items) {
                string objectSid = null;
                if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null)
                    objectSid = ((ServiceBusinessObjects.AzManGenericMember)lvi.Tag).sid.StringValue;
                else if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null)
                    objectSid = ((ServiceBusinessObjects.AzManStoreGroup)lvi.Tag).SID.StringValue;
                else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroup) != null)
                    objectSid = ((ServiceBusinessObjects.AzManApplicationGroup)lvi.Tag).SID.StringValue;
                else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroupMember) != null)
                    objectSid = ((ServiceBusinessObjects.AzManApplicationGroupMember)lvi.Tag).SID.StringValue;
                if (objectSid != null) {
                    if (member.sid.StringValue == objectSid) {
                        lvi.Remove();
                        return;
                    }
                }
            }

            //foreach (ListViewItem lvi in lsv.Items) {
            //	string objectSid = null;
            //	if ((lvi.Tag as GenericMember) != null)
            //		objectSid = ((GenericMember)lvi.Tag).sid.StringValue;
            //	else if ((lvi.Tag as IAzManStoreGroup) != null)
            //		objectSid = ((IAzManStoreGroup)lvi.Tag).SID.StringValue;
            //	else if ((lvi.Tag as IAzManApplicationGroup) != null)
            //		objectSid = ((IAzManApplicationGroup)lvi.Tag).SID.StringValue;
            //	else if ((lvi.Tag as IAzManApplicationGroupMember) != null)
            //		objectSid = ((IAzManApplicationGroupMember)lvi.Tag).SID.StringValue;
            //	if (objectSid != null) {
            //		if (member.sid.StringValue == objectSid) {
            //			lvi.Remove();
            //			return;
            //		}
            //	}
            //}
        }

        private ServiceBusinessObjects.AzManStoreGroup getStoreGroupBySid(ServiceBusinessObjects.AzManSid sid) {
            ServiceBusinessObjects.AzManStoreGroup _bso = null;
            #region Web Api Call
            var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
            var _return = Task.Run(() => _h.GetBySidAsync(this._applicationGroup.Application.Store.Name, sid.BinaryValueToBase64String(), sid.StringValue, false)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _bso = _h.GetSBOFromReturnedContent(_return);
            #endregion
            return _bso;
        }

        private IEnumerable<ServiceBusinessObjects.AzManApplicationGroupMember> getApplicationGroupMembersOrNonMembers(bool isMember) {
            IEnumerable<ServiceBusinessObjects.AzManApplicationGroupMember> _items = null;
            #region Web Api Call
            var _h = new AzManWebApiClientHelpers.AzManApplicationGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMember>(_webApiUri);
            var _return = Task.Run(() => _h.GetMembersOrNonMembersAsync(this._applicationGroup.Application.Store.Name, this._applicationGroup.Application.Name, this._applicationGroup.Name, isMember)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _items = _h.GetEnumerableSBOFromReturnedContent(_return);
            #endregion

            return _items;
        }

        private ServiceBusinessObjects.AzManApplicationGroup getApplicationGroupBySid(ServiceBusinessObjects.AzManSid sid) {
            ServiceBusinessObjects.AzManApplicationGroup _bso = null;
            #region Web Api Call
            var _h = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
            var _return = Task.Run(() => _h.GetByApplicationGroupIdAsync(this._applicationGroup.Application.Store.Name, this._applicationGroup.Application.Name, sid.BinaryValueToBase64String(), sid.StringValue, false)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _bso = _h.GetSBOFromReturnedContent(_return);
            #endregion

            return _bso;
        }

        private ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo getApplicationGroupMemberInfo(int applicationGroupMemberId) {
            ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo _sbo = null;
            #region Web Api Call
            var _h = new AzManWebApiClientHelpers.AzManApplicationGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplicationGroupMemberDisplayInfo>(_webApiUri);
            var _return = Task.Run(() => _h.GetMemberIfoAsync(this._applicationGroup.Application.Store.Name, this._applicationGroup.Application.Name, this._applicationGroup.Name, applicationGroupMemberId)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _sbo = _h.GetSBOFromReturnedContent(_return);
            #endregion
            return _sbo;
        }

        private void RefreshApplicationGroupProperties() {
            this.HourGlass(true);
            if (this._applicationGroup.GroupType == ServiceBusinessObjects.GroupType.Basic) {
                //Members
                //Add committed sids 
                this.lsvMembers.Items.Clear();
                IEnumerable<ServiceBusinessObjects.AzManApplicationGroupMember> applicationGroupMembers = this.getApplicationGroupMembersOrNonMembers(true);
                foreach (ServiceBusinessObjects.AzManApplicationGroupMember applicationGroupMember in applicationGroupMembers) {
                    //Store Groups
                    if (applicationGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Store)
                        this.lsvMembers.Items.Add(this.CreateStoreListViewItem(this.getStoreGroupBySid(applicationGroupMember.SID)));

                    //Application Groups
                    if (applicationGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Application)
                        this.lsvMembers.Items.Add(this.CreateApplicationListViewItem(this.getApplicationGroupBySid(applicationGroupMember.SID)));

                    //Ldap Object
                    if (applicationGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.LDAP || applicationGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Local)
                        this.lsvMembers.Items.Add(this.CreateLDapListViewItem(applicationGroupMember));

                    //DB Users
                    if (applicationGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Database)
                        this.lsvMembers.Items.Add(this.CreateDBListViewItem(applicationGroupMember));
                }
                //Add uncommitted sids 
                foreach (ServiceBusinessObjects.AzManGenericMember member in this.MembersToAdd) {
                    this.lsvMembers.Items.Add(this.CreateListViewItem(member));
                }
                //Remove uncommitted sids
                foreach (ServiceBusinessObjects.AzManGenericMember member in this.MembersToRemove) {
                    this.RemoveListViewItem(ref this.lsvMembers, member);
                }

                //Non-Members
                //Add committed non-sids 
                this.lsvNonMembers.Items.Clear();
                IEnumerable<ServiceBusinessObjects.AzManApplicationGroupMember> applicationGroupNonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                foreach (ServiceBusinessObjects.AzManApplicationGroupMember applicationGroupNonMember in applicationGroupNonMembers) {
                    //Store Groups
                    if (applicationGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Store)
                        this.lsvNonMembers.Items.Add(this.CreateStoreListViewItem(this.getStoreGroupBySid(applicationGroupNonMember.SID)));
                    //Application Groups
                    if (applicationGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Application)
                        this.lsvNonMembers.Items.Add(this.CreateApplicationListViewItem(this.getApplicationGroupBySid(applicationGroupNonMember.SID)));
                    //Ldap Object
                    if (applicationGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.LDAP || applicationGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Local)
                        this.lsvNonMembers.Items.Add(this.CreateLDapListViewItem(applicationGroupNonMember));
                    //DB Users
                    if (applicationGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Database)
                        this.lsvNonMembers.Items.Add(this.CreateDBListViewItem(applicationGroupNonMember));
                }
                //Add uncommitted non-sids 
                foreach (ServiceBusinessObjects.AzManGenericMember nonmember in this.NonMembersToAdd) {
                    this.lsvNonMembers.Items.Add(this.CreateListViewItem(nonmember));
                }
                //Remove uncommitted non-sids
                foreach (ServiceBusinessObjects.AzManGenericMember nonmember in this.NonMembersToRemove) {
                    this.RemoveListViewItem(ref this.lsvNonMembers, nonmember);
                }
                this.btnApply.Enabled = this.modified;
                if (!this._applicationGroup.Application.IAmManager)
                    this.txtName.Enabled = this.txtDescription.Enabled = this.btnOK.Enabled = this.btnApply.Enabled =
                         this.btnMembersAddApplicationGroups.Enabled =
                         this.btnMembersAddStoreGroups.Enabled = this.btnMembersAddDBUsers.Enabled = this.btnMembersAddWindowsUsersAndGroups.Enabled =
                         this.btnMembersRemove.Enabled =
                         this.btnNonMembersAddApplicationGroup.Enabled =
                         this.btnNonMembersAddStoreGroups.Enabled = this.btnNonMembersAddDBUsers.Enabled = this.btnNonMembersAddWindowsUsersAndGroup.Enabled =
                         this.btnNonMembersRemove.Enabled = false;
            }
            else //Ldap Query
                {
                this.txtGroupType.Text = Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Msg10");
                this.txtLDapQuery.Text = this._applicationGroup.LDAPQuery;
                this.btnApply.Enabled = this.modified;
                if (!this._applicationGroup.Application.IAmManager)
                    this.txtName.Enabled = this.txtDescription.Enabled = this.txtLDapQuery.Enabled = this.btnOK.Enabled = this.btnApply.Enabled =
                         this.lsvMembers.Enabled = this.lsvNonMembers.Enabled = false;
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

        private void frmGroupsProperties_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Cancel;
        }

        private void txtName_TextChanged(object sender, EventArgs e) {
            if (this.txtName.Text.Trim().Length > 0) {
                this.btnOK.Enabled = true;
                this.errorProvider1.SetError(this.txtName, String.Empty);
            }
            else {
                this.btnOK.Enabled = false;
                this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Tit20"));
            }
            this.modified = true;
            this.btnApply.Enabled = true;
        }

        //private bool FindMember(IAzManApplicationGroupMember[] members, string objectSid) {
        //	foreach (IAzManApplicationGroupMember m in members) {
        //		if (m.SID.StringValue == objectSid)
        //			return true;
        //	}
        //	return false;
        //}
        private bool FindMember(IEnumerable<ServiceBusinessObjects.AzManApplicationGroupMember> members, string objectSid) {
            foreach (ServiceBusinessObjects.AzManApplicationGroupMember m in members) {
                if (m.SID.StringValue == objectSid)
                    return true;
            }
            return false;
        }

        //private bool FindMember(GenericMemberCollection members, string objectSid) {
        //	foreach (GenericMember m in members) {
        //		if (m.sid.StringValue == objectSid)
        //			return true;
        //	}
        //	return false;
        //}
        private bool FindMember(ServiceBusinessObjects.AzManGenericMemberCollection members, string objectSid) {
            foreach (ServiceBusinessObjects.AzManGenericMember m in members) {
                if (m.sid.StringValue == objectSid)
                    return true;
            }
            return false;
        }

        private void btnMembersAddApplicationGroups_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                using (var frm = new frmApplicationGroupsList(_webApiUri)) {
                    frm._application = this._applicationGroup.Application;
                    frm._applicationGroup = this._applicationGroup;
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK) {
                        foreach (ServiceBusinessObjects.AzManApplicationGroup sg in frm._selectedApplicationGroups) {
                            if (!this.MembersToRemove.Remove(sg.SID.StringValue)) {
                                var _members = this.getApplicationGroupMembersOrNonMembers(true);
                                if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_members.ToArray(), sg.SID.StringValue)) {
                                    this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Application));
                                    this.modified = true;
                                }
                            }
                        }
                        this.RefreshApplicationGroupProperties();
                    }
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //frmApplicationGroupsList frm = new frmApplicationGroupsList();
            //frm.application = this.applicationGroup.Application;
            //frm.applicationGroup = this.applicationGroup;
            //DialogResult dr = frm.ShowDialog(this);
            ///*Application.DoEvents();*/
            //frm.Dispose();
            ///*Application.DoEvents();*/
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManApplicationGroup sg in frm.selectedApplicationGroups) {
            //		if (!this.MembersToRemove.Remove(sg.SID.StringValue)) {
            //			if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupMembers(), sg.SID.StringValue)) {
            //				this.MembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Application));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void btnMembersAddWindowsUsersAndGroups_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this._applicationGroup.Application.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Developer);
                /*Application.DoEvents();*/
                if (res != null) {
                    var _members = this.getApplicationGroupMembersOrNonMembers(true);
                    foreach (ADObject o in res) {
                        if (!this.MembersToRemove.Remove(o.Sid) && !this.FindMember(_members.ToArray(), o.Sid) && !this.FindMember(this.MembersToAdd, o.Sid)) {
                            var wd = ServiceBusinessObjects.WhereDefined.LDAP;
                            if (!o.ADSPath.StartsWith("LDAP"))
                                wd = ServiceBusinessObjects.WhereDefined.Local;
                            this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), wd));
                            this.modified = true;
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
                this.HourGlass(false);
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }
        }

        private void btnMembersRemove_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                foreach (ListViewItem lvi in this.lsvMembers.SelectedItems) {
                    if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManStoreGroup)(lvi.Tag);
                        this.MembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Store));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroup) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManApplicationGroup)(lvi.Tag);
                        this.MembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Application));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroupMember) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManApplicationGroupMember)(lvi.Tag);
                        this.MembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.SID.StringValue, lviTag.SID, lviTag.WhereDefined));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManGenericMember)(lvi.Tag);
                        if (this.MembersToAdd.Remove(lviTag.sid.StringValue))
                            this.modified = true;
                    }
                }
                this.RefreshApplicationGroupProperties();
                if (this.lsvMembers.Items.Count == 0)
                    this.btnMembersRemove.Enabled = false;
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //foreach (ListViewItem lvi in this.lsvMembers.SelectedItems) {
            //	if ((lvi.Tag as IAzManStoreGroup) != null) {
            //		IAzManStoreGroup lviTag = (IAzManStoreGroup)(lvi.Tag);
            //		this.MembersToRemove.Add(new GenericMember(lviTag.Name, lviTag.SID, WhereDefined.Store));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as IAzManApplicationGroup) != null) {
            //		IAzManApplicationGroup lviTag = (IAzManApplicationGroup)(lvi.Tag);
            //		this.MembersToRemove.Add(new GenericMember(lviTag.Name, lviTag.SID, WhereDefined.Application));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as IAzManApplicationGroupMember) != null) {
            //		IAzManApplicationGroupMember lviTag = (IAzManApplicationGroupMember)(lvi.Tag);
            //		this.MembersToRemove.Add(new GenericMember(lviTag.SID.StringValue, lviTag.SID, WhereDefined.LDAP));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as GenericMember) != null) {
            //		GenericMember lviTag = (GenericMember)(lvi.Tag);
            //		if (this.MembersToAdd.Remove(lviTag.sid.StringValue))
            //			this.modified = true;
            //	}
            //}
            //this.RefreshApplicationGroupProperties();
            //if (this.lsvMembers.Items.Count == 0)
            //	this.btnMembersRemove.Enabled = false;
            //this.HourGlass(false);
        }

        private void btnApply_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                this.CommitChanges();
                this.btnApply.Enabled = false;
            }
            catch {
                throw;
                //this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
            }
            finally {
                this.HourGlass(false);
            }
        }

        private void CommitChanges() {
            if (!this.modified)
                return;

            var _modifiedBso = _applicationGroup.Clone();

            //Store Group Properties
            _modifiedBso.Name = txtName.Text.Trim();
            _modifiedBso.Description = this.txtDescription.Text.Trim();
            _modifiedBso.GroupType = this._applicationGroup.GroupType;

            if (this._applicationGroup.GroupType == ServiceBusinessObjects.GroupType.Basic) {
                _modifiedBso.LDAPQuery = string.Empty;

                //Members
                var _genericMembers = new ServiceBusinessObjects.AzManGenericMember[this.MembersToAdd.Count];
                this.MembersToAdd.CopyTo(_genericMembers);
                _modifiedBso.MembersToAdd = _genericMembers;

                _genericMembers = new ServiceBusinessObjects.AzManGenericMember[this.MembersToRemove.Count];
                this.MembersToRemove.CopyTo(_genericMembers);
                _modifiedBso.MembersToRemove = _genericMembers;

                _genericMembers = new ServiceBusinessObjects.AzManGenericMember[this.NonMembersToAdd.Count];
                this.NonMembersToAdd.CopyTo(_genericMembers);
                _modifiedBso.NonMembersToAdd = _genericMembers;

                _genericMembers = new ServiceBusinessObjects.AzManGenericMember[this.NonMembersToRemove.Count];
                this.NonMembersToRemove.CopyTo(_genericMembers);
                _modifiedBso.NonMembersToRemove = _genericMembers;
            }
            else {
                _modifiedBso.LDAPQuery = this.txtLDapQuery.Text.Trim();
            }

            ServiceBusinessObjects.AzManApplicationGroup _updated = null;
            #region Call WebApi				
            var _h = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
            var _return = Task.Run(() => _h.PutAsync(_applicationGroup.Name, _modifiedBso)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _updated = _h.GetSBOFromReturnedContent(_return);
            #endregion

            this._applicationGroup = _updated;

            this.MembersToAdd.Clear();
            this.MembersToRemove.Clear();
            this.NonMembersToAdd.Clear();
            this.NonMembersToRemove.Clear();
            this.modified = false;

            //try {
            //	if (!this.modified)
            //		return;
            //	//Application Group Properties
            //	this.applicationGroup.Application.Store.Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
            //	this.applicationGroup.Rename(this.txtName.Text.Trim());
            //	this.applicationGroup.Update(this.txtDescription.Text.Trim(), this.applicationGroup.GroupType);
            //	if (this.applicationGroup.GroupType == GroupType.Basic) {
            //		//Members
            //		//Members To Add
            //		#region ***PERSONALIZADO***
            //		foreach (GenericMember member in this.MembersToAdd) {
            //			this.applicationGroup.CreateApplicationGroupMemberCustom(member.sid, member.WhereDefined, true, member.LDAPDomain);
            //		}
            //		#endregion
            //		//Members To Remove
            //		foreach (GenericMember member in this.MembersToRemove) {
            //			this.applicationGroup.GetApplicationGroupMember(member.sid).Delete();
            //		}
            //		//Non Members
            //		//Non Members To Add
            //		foreach (GenericMember nonMember in this.NonMembersToAdd) {
            //			this.applicationGroup.CreateApplicationGroupMember(nonMember.sid, nonMember.WhereDefined, false);
            //		}
            //		//Non Members To Remove
            //		foreach (GenericMember nonMember in this.NonMembersToRemove) {
            //			this.applicationGroup.GetApplicationGroupMember(nonMember.sid).Delete();
            //		}
            //		this.MembersToAdd.Clear();
            //		this.MembersToRemove.Clear();
            //		this.NonMembersToAdd.Clear();
            //		this.NonMembersToRemove.Clear();
            //		this.modified = false;
            //	}
            //	else {
            //		this.applicationGroup.UpdateLDapQuery(this.txtLDapQuery.Text.Trim());
            //	}
            //	this.applicationGroup.Application.Store.Storage.CommitTransaction();
            //}
            //catch {
            //	this.applicationGroup.Application.Store.Storage.RollBackTransaction();
            //	throw;
            //}
        }

        private void btnOK_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                this.CommitChanges();
                this.btnApply.Enabled = false;
                this.DialogResult = DialogResult.OK;
            }
            catch {
                throw;
                //this.DialogResult = DialogResult.None;
                //this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
            }
            finally {
                this.HourGlass(false);
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e) {
            this.modified = true;
            this.btnApply.Enabled = true;
        }

        private void btnNonMembersAddApplicationGroup_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                var frm = new frmApplicationGroupsList(_webApiUri);
                frm._application = this._applicationGroup.Application;
                frm._applicationGroup = this._applicationGroup;
                DialogResult dr = frm.ShowDialog(this);
                this.HourGlass(true);
                if (dr == DialogResult.OK) {
                    foreach (ServiceBusinessObjects.AzManApplicationGroup sg in frm._selectedApplicationGroups) {
                        if (!this.NonMembersToRemove.Remove(sg.SID.StringValue)) {
                            var _nonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                            if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_nonMembers, sg.SID.StringValue)) {
                                this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Application));
                                this.modified = true;
                            }
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //frmApplicationGroupsList frm = new frmApplicationGroupsList();
            //frm.application = this.applicationGroup.Application;
            //frm.applicationGroup = this.applicationGroup;
            //DialogResult dr = frm.ShowDialog(this);
            //this.HourGlass(true);
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManApplicationGroup sg in frm.selectedApplicationGroups) {
            //		if (!this.NonMembersToRemove.Remove(sg.SID.StringValue)) {
            //			if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupNonMembers(), sg.SID.StringValue)) {
            //				this.NonMembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Application));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void btnNonMembersAddWindowsUsersAndGroup_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this._applicationGroup.Application.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Developer);
                /*Application.DoEvents();*/
                if (res != null) {
                    var _nonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                    foreach (ADObject o in res) {
                        if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(_nonMembers, o.Sid) && !this.FindMember(this.NonMembersToAdd, o.Sid)) {
                            var wd = ServiceBusinessObjects.WhereDefined.LDAP;
                            if (!o.ADSPath.StartsWith("LDAP"))
                                wd = ServiceBusinessObjects.WhereDefined.Local;
                            this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), wd));
                            this.modified = true;
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
                this.HourGlass(false);
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this.applicationGroup.Application.Store.Storage.Mode == NetSqlAzManMode.Developer);
            //if (res != null) {
            //	foreach (ADObject o in res) {
            //		if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(this.applicationGroup.GetApplicationGroupNonMembers(), o.Sid) && !this.FindMember(this.NonMembersToAdd, o.Sid)) {
            //			WhereDefined wd = WhereDefined.LDAP;
            //			if (!o.ADSPath.StartsWith("LDAP"))
            //				wd = WhereDefined.Local;
            //			this.NonMembersToAdd.Add(new GenericMember(o.Name, new SqlAzManSID(o.Sid), wd));
            //			this.modified = true;
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void btnNonMembersRemove_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                foreach (ListViewItem lvi in this.lsvNonMembers.SelectedItems) {
                    if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManStoreGroup)lvi.Tag;
                        this.NonMembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Store));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroup) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManApplicationGroup)lvi.Tag;
                        this.NonMembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Application));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManApplicationGroupMember) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManApplicationGroupMember)lvi.Tag;
                        this.NonMembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.SID.StringValue, lviTag.SID, lviTag.WhereDefined));
                        this.modified = true;
                    }
                    else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null) {
                        var lviTag = (ServiceBusinessObjects.AzManGenericMember)lvi.Tag;
                        if (this.NonMembersToAdd.Remove(lviTag.sid.StringValue))
                            this.modified = true;
                    }
                }
                this.RefreshApplicationGroupProperties();
                if (this.lsvNonMembers.Items.Count == 0)
                    this.btnNonMembersRemove.Enabled = false;
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //foreach (ListViewItem lvi in this.lsvNonMembers.SelectedItems) {
            //	if ((lvi.Tag as IAzManStoreGroup) != null) {
            //		IAzManStoreGroup lviTag = (IAzManStoreGroup)lvi.Tag;
            //		this.NonMembersToRemove.Add(new GenericMember(lviTag.Name, lviTag.SID, WhereDefined.Store));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as IAzManApplicationGroup) != null) {
            //		IAzManApplicationGroup lviTag = (IAzManApplicationGroup)lvi.Tag;
            //		this.NonMembersToRemove.Add(new GenericMember(lviTag.Name, lviTag.SID, WhereDefined.Application));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as IAzManApplicationGroupMember) != null) {
            //		IAzManApplicationGroupMember lviTag = (IAzManApplicationGroupMember)lvi.Tag;
            //		this.NonMembersToRemove.Add(new GenericMember(lviTag.SID.StringValue, lviTag.SID, WhereDefined.LDAP));
            //		this.modified = true;
            //	}
            //	else if ((lvi.Tag as GenericMember) != null) {
            //		GenericMember lviTag = (GenericMember)lvi.Tag;
            //		if (this.NonMembersToAdd.Remove(lviTag.sid.StringValue))
            //			this.modified = true;
            //	}
            //}
            //this.RefreshApplicationGroupProperties();
            //if (this.lsvNonMembers.Items.Count == 0)
            //	this.btnNonMembersRemove.Enabled = false;
            //this.HourGlass(false);
        }

        private void lsvMembers_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (this.lsvMembers.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
                this.btnMembersRemove.Enabled = false;
            else
                this.btnMembersRemove.Enabled = true && this._applicationGroup.Application.IAmManager;
        }

        private void lsvNonMembers_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (this.lsvNonMembers.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
                this.btnNonMembersRemove.Enabled = false;
            else
                this.btnNonMembersRemove.Enabled = true && this._applicationGroup.Application.IAmManager;

        }

        private void btnMembersAddStoreGroups_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                var frm = new frmStoreGroupsList(_webApiUri);
                frm._store = this._applicationGroup.Application.Store;
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    foreach (ServiceBusinessObjects.AzManStoreGroup sg in frm._selectedStoreGroups) {
                        if (!this.MembersToRemove.Remove(sg.SID.StringValue)) {
                            var _members = this.getApplicationGroupMembersOrNonMembers(true);
                            if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_members.ToArray(), sg.SID.StringValue)) {
                                this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Store));
                                this.modified = true;
                            }
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //frmStoreGroupsList frm = new frmStoreGroupsList();
            //frm.store = this.applicationGroup.Application.Store;
            //DialogResult dr = frm.ShowDialog(this);
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManStoreGroup sg in frm.selectedStoreGroups) {
            //		if (!this.MembersToRemove.Remove(sg.SID.StringValue)) {
            //			if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupMembers(), sg.SID.StringValue)) {
            //				this.MembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Store));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void btnNonMembersAddStoreGroups_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                var frm = new frmStoreGroupsList(_webApiUri);
                frm._store = this._applicationGroup.Application.Store;
                DialogResult dr = frm.ShowDialog(this);
                this.HourGlass(true);
                if (dr == DialogResult.OK) {
                    foreach (var sg in frm._selectedStoreGroups) {
                        if (!this.NonMembersToRemove.Remove(sg.SID.StringValue)) {
                            var _nonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                            if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_nonMembers, sg.SID.StringValue)) {
                                this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Store));
                                this.modified = true;
                            }
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //frmStoreGroupsList frm = new frmStoreGroupsList();
            //frm.store = this.applicationGroup.Application.Store;
            //DialogResult dr = frm.ShowDialog(this);
            //this.HourGlass(true);
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManStoreGroup sg in frm.selectedStoreGroups) {
            //		if (!this.NonMembersToRemove.Remove(sg.SID.StringValue)) {
            //			if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupNonMembers(), sg.SID.StringValue)) {
            //				this.NonMembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Store));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void txtLDapQuery_TextChanged(object sender, EventArgs e) {
            this.btnApply.Enabled = true;
            this.modified = true;
        }

        private void lsvMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            this.btnMembersRemove.Enabled = this.lsvMembers.SelectedItems.Count > 0 && this._applicationGroup.Application.IAmManager;
        }

        private void lsvNonMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            this.btnNonMembersRemove.Enabled = this.lsvNonMembers.SelectedItems.Count > 0 && this._applicationGroup.Application.IAmManager;
        }

        private void btnTestLDapQuery_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                frmActiveDirectoryObjectsList frm = new frmActiveDirectoryObjectsList();
                frm.Text = Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Msg20") + this._applicationGroup.Name;

                IEnumerable<ServiceBusinessObjects.LDAPResult> _resultList = null;
                #region Call WebApi
                var _h = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>(_webApiUri);
                var _return = Task.Run(() => _h.GetLdapQueryResultAsync(_applicationGroup.Application.Store.Name, _applicationGroup.Application.Name, _applicationGroup.Name, this.txtLDapQuery.Text)).Result;
                if (_h.IsResponseContentError(_return))
                    _h.ThrowWebApiRequestException(_return);
                else
                    _resultList = _h.GetEnumerableSBOFromReturnedContent(_return);
                #endregion
                frm.ldapResultList = _resultList;
                frm.ShowDialog(this);

                this.HourGlass(false);
            }
            catch {
                this.HourGlass(false);

                //this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Tit30"));
                throw;
            }
        }

        private void frmApplicationGroupsProperties_Activated(object sender, EventArgs e) {
            if (this.firstShow) {
                this.txtName.Focus();
                this.txtName.SelectAll();
                this.firstShow = false;
            }
        }

        private void lsvMembers_ColumnClick(object sender, ColumnClickEventArgs e) {
            this.SortListView(this.lsvMembers);
        }

        private void lsvNonMembers_ColumnClick(object sender, ColumnClickEventArgs e) {
            this.SortListView(this.lsvNonMembers);
        }

        private void btnMembersAddDBUsers_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);
                var frm = new frmDBUsersList(_webApiUri);
                frm._application = this._applicationGroup.Application;
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    foreach (ServiceBusinessObjects.AzManDBUser dbUser in frm._selectedDBUsers) {
                        if (!this.MembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
                            var _members = this.getApplicationGroupMembersOrNonMembers(true);
                            if (!this.MembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(_members, dbUser.CustomSid.StringValue)) {
                                this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(dbUser.UserName, dbUser.CustomSid, ServiceBusinessObjects.WhereDefined.Database));
                                this.modified = true;
                            }
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //frmDBUsersList frm = new frmDBUsersList();
            //frm.application = this.applicationGroup.Application;
            //DialogResult dr = frm.ShowDialog(this);
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManDBUser dbUser in frm.selectedDBUsers) {
            //		if (!this.MembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
            //			if (!this.MembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupMembers(), dbUser.CustomSid.StringValue)) {
            //				this.MembersToAdd.Add(new GenericMember(dbUser.UserName, dbUser.CustomSid, WhereDefined.Database));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        private void btnNonMembersAddDBUsers_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                frmDBUsersList frm = new frmDBUsersList(_webApiUri);
                frm._application = this._applicationGroup.Application;
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    foreach (var dbUser in frm._selectedDBUsers) {
                        if (!this.NonMembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
                            var _nonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                            if (!this.NonMembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(_nonMembers, dbUser.CustomSid.StringValue)) {
                                this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(dbUser.UserName, dbUser.CustomSid, ServiceBusinessObjects.WhereDefined.Database));
                                this.modified = true;
                            }
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

            //this.HourGlass(true);
            //frmDBUsersList frm = new frmDBUsersList();
            //frm.application = this.applicationGroup.Application;
            //DialogResult dr = frm.ShowDialog(this);
            //if (dr == DialogResult.OK) {
            //	foreach (IAzManDBUser dbUser in frm.selectedDBUsers) {
            //		if (!this.NonMembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
            //			if (!this.NonMembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(this.applicationGroup.GetApplicationGroupNonMembers(), dbUser.CustomSid.StringValue)) {
            //				this.NonMembersToAdd.Add(new GenericMember(dbUser.UserName, dbUser.CustomSid, WhereDefined.Database));
            //				this.modified = true;
            //			}
            //		}
            //	}
            //	this.RefreshApplicationGroupProperties();
            //}
            //this.HourGlass(false);
        }

        #region ***PERSONALIZADO***
        private void butnAddLDAPObjects_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                //var _f = new AzManWinUI.LDAPServices.LDAPQueryClientUI(_webApiUri);
                //if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                //	return;
                var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
                if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                ADObject[] res = _f.SelectedADObjects;
                if (res != null) {
                    var _members = this.getApplicationGroupMembersOrNonMembers(true);
                    foreach (ADObject o in res) {
                        if (!this.MembersToRemove.Remove(o.Sid) && !this.FindMember(_members.ToArray(), o.Sid) && !this.FindMember(this.MembersToAdd, o.Sid)) {
                            this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), ServiceBusinessObjects.WhereDefined.LDAP, o.DomainProfile, o.samAccountName, o.cn, o.displayName, o.LdapSid, o.distinguishedName, o.ClassName));
                            this.modified = true;
                        }
                    }
                    this.RefreshApplicationGroupProperties();
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

            //	AzManWinUI.LDAPServices.LDAPQueryClientUI _f = new AzManWinUI.LDAPServices.LDAPQueryClientUI(this.applicationGroup.Application.Store.Storage);
            //	if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //		return;

            //	ADObject[] res = _f.SelectedADObjects;
            //	if (res != null) {
            //		foreach (ADObject o in res) {
            //			if (!this.MembersToRemove.Remove(o.Sid) && !this.FindMember(this.applicationGroup.GetApplicationGroupMembers(), o.Sid) && !this.FindMember(this.MembersToAdd, o.Sid)) {
            //				this.MembersToAdd.Add(new GenericMember(o.Name, new SqlAzManSID(o.Sid), WhereDefined.LDAP, o.DomainProfile));
            //				this.modified = true;
            //			}
            //		}
            //		this.RefreshApplicationGroupProperties();
            //	}
            //	this.HourGlass(false);
            //}
            //catch (Exception ex) {
            //	this.HourGlass(false);
            //	MessageBox.Show(ex.Message);
            //}
        }
        #endregion

        private void butnNonMembersAddLDAPObjects_Click(object sender, EventArgs e) {
            try {
                this.HourGlass(true);

                //var _f = new AzManWinUI.LDAPServices.LDAPQueryClientUI(_webApiUri);
                //if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                //	return;
                var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
                if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                ADObject[] res = _f.SelectedADObjects;
                if (res != null) {
                    var _nonMembers = this.getApplicationGroupMembersOrNonMembers(false);
                    foreach (ADObject o in res) {
                        if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(_nonMembers, o.Sid) && !this.FindMember(this.NonMembersToAdd, o.Sid)) {
                            this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), ServiceBusinessObjects.WhereDefined.LDAP, o.DomainProfile, o.samAccountName, o.cn, o.displayName, o.LdapSid, o.distinguishedName, o.ClassName));
                            this.modified = true;
                        }
                    }
                    this.RefreshApplicationGroupProperties();
                }
            }
            catch {
                throw;
            }
            finally {
                this.HourGlass(false);
            }

        }
    }
}