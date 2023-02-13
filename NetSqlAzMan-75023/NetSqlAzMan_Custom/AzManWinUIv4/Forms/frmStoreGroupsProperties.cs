using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.DirectoryServices;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NetSqlAzMan.SnapIn.Forms
{
	public partial class frmStoreGroupsProperties : frmBase
	{
		internal ServiceBusinessObjects.AzManStoreGroup _storeGroup;
		private string _webApiUri;

		private ServiceBusinessObjects.AzManGenericMemberCollection MembersToAdd;
		private ServiceBusinessObjects.AzManGenericMemberCollection MembersToRemove;
		private ServiceBusinessObjects.AzManGenericMemberCollection NonMembersToAdd;
		private ServiceBusinessObjects.AzManGenericMemberCollection NonMembersToRemove;
		private bool modified;
		private bool firstShow;

		public frmStoreGroupsProperties(string webApiUri)
		{
			InitializeComponent();

			_webApiUri = webApiUri;

			this.firstShow = true;
			this.MembersToAdd = new ServiceBusinessObjects.AzManGenericMemberCollection();
			this.MembersToRemove = new ServiceBusinessObjects.AzManGenericMemberCollection();
			this.NonMembersToAdd = new ServiceBusinessObjects.AzManGenericMemberCollection();
			this.NonMembersToRemove = new ServiceBusinessObjects.AzManGenericMemberCollection();
		}

		private void PreSortListView(ListView lv)
		{
			lv.Sorting = SortOrder.Ascending;
		}
		private void SortListView(ListView lv)
		{
			if (lv.Sorting == SortOrder.Ascending)
				lv.Sorting = SortOrder.Descending;
			else
				lv.Sorting = SortOrder.Ascending;
			lv.Sort();
		}

		private void frmGroupsProperties_Load(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			//PorkAround: http://lab.msdn.microsoft.com/ProductFeedback/viewFeedback.aspx?feedbackId=FDBK49664
			ImageList clonedImageList = new ImageList();
			foreach (Image image in this.imageList1.Images)
			{
				clonedImageList.Images.Add((Image)image.Clone());
			}
			this.lsvMembers.SmallImageList = clonedImageList;
			this.lsvNonMembers.SmallImageList = clonedImageList;
			//PorkAround End
			this.txtName.Text = this._storeGroup.Name;
			this.txtDescription.Text = this._storeGroup.Description;
			this.txtGroupType.Text = (this._storeGroup.GroupType == ServiceBusinessObjects.GroupType.Basic ? Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg10") : Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg20"));

			if (this._storeGroup.GroupType == ServiceBusinessObjects.GroupType.Basic)
			{
				//this.btnMembersAddStoreGroup.Enabled = this.btnNonMembersAddStoreGroup.Enabled = this.storeGroup.Store.HasStoreGroups();
				this.btnMembersAddStoreGroup.Enabled = this.btnNonMembersAddStoreGroup.Enabled = true;

				if (this.tabControl1.TabPages.Contains(this.tabLdapQuery))
					this.tabControl1.TabPages.Remove(this.tabLdapQuery);
				this.lsvMembers.Items.Clear();
				this.lsvNonMembers.Items.Clear();
				this.picImage.Image = this.picBasic.Image;
				this.PreSortListView(this.lsvMembers);
				this.PreSortListView(this.lsvNonMembers);
			}
			else
			{
				if (this.tabControl1.Contains(this.tabMembers))
					this.tabControl1.TabPages.Remove(this.tabMembers);
				if (this.tabControl1.Contains(this.tabNonMembers))
					this.tabControl1.TabPages.Remove(this.tabNonMembers);
				this.picImage.Image = this.picLDap.Image;
			}
			this.RefreshStoreGroupProperties();
			this.modified = false;
			this.btnApply.Enabled = false;
			this.txtName.Focus();
			this.txtName.SelectAll();
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
		}

		private ServiceBusinessObjects.AzManStoreGroup getStoreGroupBySid(ServiceBusinessObjects.AzManSid sid)
		{
			ServiceBusinessObjects.AzManStoreGroup _sbo = null;
			#region Web Api Call
			var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
			var _return = Task.Run(() => _h.GetBySidAsync(this._storeGroup.Store.Name, sid.BinaryValueToBase64String(), sid.StringValue, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_sbo = _h.GetSBOFromReturnedContent(_return);
			#endregion
			return _sbo;
		}

		private ServiceBusinessObjects.AzManStoreGroupMemberDisplayInfo getStoreGroupMemberInfo(int storeGroupMemberId)
		{
			NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMemberDisplayInfo _sbo = null;
			#region Web Api Call
			var _h = new AzManWebApiClientHelpers.AzManStoreGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMemberDisplayInfo>(_webApiUri);
			var _return = Task.Run(() => _h.GetMemberIfoAsync(this._storeGroup.Store.Name, this._storeGroup.Name, storeGroupMemberId)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_sbo = _h.GetSBOFromReturnedContent(_return);
			#endregion
			return _sbo;
		}

		private IEnumerable<ServiceBusinessObjects.AzManStoreGroupMember> getStoreGroupMembersOrNonMembers(bool isMember)
		{
			#region Web Api Call
			var _h = new AzManWebApiClientHelpers.AzManStoreGroupMembersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroupMember>(_webApiUri);
			var _return = Task.Run(() => _h.GetMembersOrNonMembersAsync(this._storeGroup.Store.Name, this._storeGroup.Name, isMember)).Result;
			if (_h.IsResponseContentError(_return))
				throw _h.GetWebApiRequestException(_return);
			else
				return _h.GetEnumerableSBOFromReturnedContent(_return);

			//if (_return.ContainsKey("error")) {
			//    var _values = _return["error"].ToArray();
			//    var _requestUri = _values[0].ToString();
			//    var _respMsg = (HttpResponseMessage)_values[1];
			//    //Disparar error según el tipo de respuesta Http
			//    throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			//}
			//else {
			//    var _values = _return["contentData"].ToArray();
			//    return (IEnumerable<ServiceBusinessObjects.AzManStoreGroupMember>)_values[0];
			//}
			#endregion
		}

		private ListViewItem CreateStoreListViewItem(ServiceBusinessObjects.AzManStoreGroup member)
		{
			ListViewItem lvi = new ListViewItem();
			lvi.Tag = member;
			lvi.Text = member.Name;
			lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_Store"));
			return lvi;
		}

		private ListViewItem CreateLDapListViewItem(ServiceBusinessObjects.AzManStoreGroupMember member)
		{
			////VBASTIDAS: Ahora el member ya almacena los datos Ldap, no es 
			////necesario volver a solicitarlos al Web Api
			//var _return = this.getStoreGroupMemberInfo(member.StoreGroupMemberId);

			ListViewItem lvi = new ListViewItem();
			lvi.Tag = member;
			lvi.Text = member.LdapDescription;
			lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_LDAP"));
			return lvi;
		}

		private ListViewItem CreateDBListViewItem(ServiceBusinessObjects.AzManStoreGroupMember member)
		{
			ListViewItem lvi = new ListViewItem();
			if (member != null)
			{
				var _return = this.getStoreGroupMemberInfo(member.StoreGroupMemberId);

				lvi.Tag = member;
				lvi.Text = _return.DisplayName;
				lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("WhereDefined_DB"));
			}
			return lvi;
		}

		private ListViewItem CreateListViewItem(GenericMember member)
		{
			ListViewItem lvi = new ListViewItem();
			if (member != null)
			{
				lvi.Tag = member;
				if (!string.IsNullOrEmpty(member.LDAPDomain))
					lvi.Text = string.Format("{0}\\{1}", member.LDAPDomain, member.Name);
				else
					lvi.Text = string.Format("{0}", member.Name);
				switch (member.WhereDefined.ToString())
				{
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

		private ListViewItem CreateListViewItem(ServiceBusinessObjects.AzManGenericMember member)
		{
			ListViewItem lvi = new ListViewItem();
			if (member != null)
			{
				lvi.Tag = member;
				if (!string.IsNullOrEmpty(member.DomainProfile))
					lvi.Text = string.Format("{0}\\{1}", member.DomainProfile, member.Name);
				else
					lvi.Text = string.Format("{0}", member.Name);
				switch (member.WhereDefined.ToString())
				{
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

		private void RemoveListViewItem(ref ListView lsv, ServiceBusinessObjects.AzManGenericMember member)
		{
			foreach (ListViewItem lvi in lsv.Items)
			{
				string objectSid = null;
				if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null)
					objectSid = ((ServiceBusinessObjects.AzManGenericMember)lvi.Tag).sid.StringValue;
				else if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null)
					objectSid = ((ServiceBusinessObjects.AzManStoreGroup)lvi.Tag).SID.StringValue;
				else if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroupMember) != null)
					objectSid = ((ServiceBusinessObjects.AzManStoreGroupMember)lvi.Tag).SID.StringValue;
				if (objectSid != null)
				{
					if (member.sid.StringValue == objectSid)
					{
						lvi.Remove();
						return;
					}
				}
			}
		}

		private void RefreshStoreGroupProperties()
		{
			if (this._storeGroup.GroupType == ServiceBusinessObjects.GroupType.Basic)
			{
				//Members
				//Add committed sids 
				this.lsvMembers.Items.Clear();

				IEnumerable<ServiceBusinessObjects.AzManStoreGroupMember> storeGroupMembers = this.getStoreGroupMembersOrNonMembers(true);
				foreach (ServiceBusinessObjects.AzManStoreGroupMember storeGroupMember in storeGroupMembers)
				{
					//Store Groups
					if (storeGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Store)
						this.lsvMembers.Items.Add(this.CreateStoreListViewItem(this.getStoreGroupBySid(storeGroupMember.SID)));

					//Ldap Object
					if (storeGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.LDAP || storeGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Local)
						this.lsvMembers.Items.Add(this.CreateLDapListViewItem(storeGroupMember));

					//DB Users
					if (storeGroupMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Database)
						this.lsvMembers.Items.Add(this.CreateDBListViewItem(storeGroupMember));
				}

				//Add uncommitted sids 
				foreach (ServiceBusinessObjects.AzManGenericMember member in this.MembersToAdd)
				{
					this.lsvMembers.Items.Add(this.CreateListViewItem(member));
				}

				//Remove uncommitted sids
				foreach (ServiceBusinessObjects.AzManGenericMember member in this.MembersToRemove)
				{
					this.RemoveListViewItem(ref this.lsvMembers, member);
				}

				//Non-Members
				//Add committed non-sids 
				this.lsvNonMembers.Items.Clear();
				IEnumerable<ServiceBusinessObjects.AzManStoreGroupMember> storeGroupNonMembers = this.getStoreGroupMembersOrNonMembers(false);
				foreach (ServiceBusinessObjects.AzManStoreGroupMember storeGroupNonMember in storeGroupNonMembers)
				{
					//Store Groups
					if (storeGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Store)
						this.lsvNonMembers.Items.Add(this.CreateStoreListViewItem(this.getStoreGroupBySid(storeGroupNonMember.SID)));
					//Ldap Object
					if (storeGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.LDAP || storeGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Local)
						this.lsvNonMembers.Items.Add(this.CreateLDapListViewItem(storeGroupNonMember));
					//DB Users
					if (storeGroupNonMember.WhereDefined == ServiceBusinessObjects.WhereDefined.Database)
						this.lsvNonMembers.Items.Add(this.CreateDBListViewItem(storeGroupNonMember));
				}
				//Add uncommitted non-sids 
				foreach (ServiceBusinessObjects.AzManGenericMember nonmember in this.NonMembersToAdd)
				{
					this.lsvNonMembers.Items.Add(this.CreateListViewItem(nonmember));
				}
				//Remove uncommitted non-sids
				foreach (ServiceBusinessObjects.AzManGenericMember nonmember in this.NonMembersToRemove)
				{
					this.RemoveListViewItem(ref this.lsvNonMembers, nonmember);
				}
				this.btnApply.Enabled = this.modified;
				if (!this._storeGroup.Store.IAmManager)
					this.txtName.Enabled = this.txtDescription.Enabled = this.btnOK.Enabled = this.btnApply.Enabled =
						  this.btnMembersAddStoreGroup.Enabled = this.btnMembersAddDBUsers.Enabled = this.btnMembersAddWindowsUsersAndGroups.Enabled =
						  this.btnMembersRemove.Enabled =
						  this.lsvMembers.Enabled = this.lsvNonMembers.Enabled =
						  this.btnNonMembersAddStoreGroup.Enabled = this.btnNonMembersAddDBUsers.Enabled = this.btnNonMembersAddWindowsUsersAndGroup.Enabled =
						  this.btnNonMembersRemove.Enabled = false;
			}
			else //Ldap Query
			{
				this.txtGroupType.Text = Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Msg10");
				this.txtLDapQuery.Text = this._storeGroup.LDAPQuery;
				this.btnApply.Enabled = this.modified;
				if (!this._storeGroup.Store.IAmManager)
					this.txtName.Enabled = this.txtDescription.Enabled = this.txtLDapQuery.Enabled = this.btnOK.Enabled = this.btnApply.Enabled = false;
			}
		}

		protected void HourGlass(bool switchOn)
		{
			this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
			/*Application.DoEvents();*/
		}

		protected void ShowError(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected void ShowInfo(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected void ShowWarning(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void frmGroupsProperties_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			if (this.txtName.Text.Trim().Length > 0)
			{
				this.btnOK.Enabled = true;
				this.errorProvider1.SetError(this.txtName, String.Empty);
			}
			else
			{
				this.btnOK.Enabled = false;
				this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmStoreGroupsProperties_Msg10"));
			}
			this.modified = true;
			this.btnApply.Enabled = true;
		}

		private bool FindMember(ServiceBusinessObjects.AzManStoreGroupMember[] members, ServiceBusinessObjects.AzManSid sid)
		{
			foreach (ServiceBusinessObjects.AzManStoreGroupMember m in members)
			{
				if (m.SID.StringValue == sid.StringValue)
					return true;
			}
			return false;
		}

		private bool FindMember(ServiceBusinessObjects.AzManGenericMemberCollection members, ServiceBusinessObjects.AzManSid sid)
		{
			foreach (var m in members)
			{
				if (m.sid.StringValue == sid.StringValue)
					return true;
			}
			return false;
		}

		private void btnMembersAddStoreGroups_Click(object sender, EventArgs e)
		{
			try
			{
				var frm = new frmStoreGroupsList(_webApiUri);
				frm._store = this._storeGroup.Store;
				frm._storeGroup = this._storeGroup;
				DialogResult dr = frm.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					foreach (ServiceBusinessObjects.AzManStoreGroup sg in frm._selectedStoreGroups)
					{
						if (!this.MembersToRemove.Remove(sg.SID.StringValue))
						{
							var _members = this.getStoreGroupMembersOrNonMembers(true);
							if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_members.ToArray(), sg.SID))
							{
								this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Store));
								this.modified = true;
							}
						}
					}

					this.RefreshStoreGroupProperties();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}

			//frmStoreGroupsList frm = new frmStoreGroupsList();
			//frm.store = this.storeGroup.Store;
			//frm.storeGroup = this.storeGroup;
			//DialogResult dr = frm.ShowDialog(this);
			//if (dr == DialogResult.OK) {
			//	foreach (IAzManStoreGroup sg in frm.selectedStoreGroups) {
			//		if (!this.MembersToRemove.Remove(sg.SID.StringValue)) {
			//			if (!this.MembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.storeGroup.GetStoreGroupMembers(), sg.SID)) {
			//				this.MembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Store));
			//				this.modified = true;
			//			}
			//		}
			//	}

			//	this.RefreshStoreGroupProperties();
			//}
			//this.HourGlass(false);
		}

		private void btnMembersAddWindowsUsersAndGroups_Click(object sender, EventArgs e)
		{
			try
			{
				MessageBox.Show(this, "Solo se mostrará los usuarios de la red local. Esta función carece de utilidad cuando accede fuera de las instalaciones o red de la oficina.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				this.HourGlass(true);
				ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this._storeGroup.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Developer);
				//*Application.DoEvents();*/
				if (res != null)
				{
					var _members = this.getStoreGroupMembersOrNonMembers(true);
					foreach (ADObject o in res)
					{
						if (!this.MembersToRemove.Remove(o.Sid) && !this.FindMember(_members.ToArray(), new ServiceBusinessObjects.AzManSid(o.Sid)) && !this.FindMember(this.MembersToAdd, new ServiceBusinessObjects.AzManSid(o.Sid)))
						{
							var wd = ServiceBusinessObjects.WhereDefined.LDAP;
							if (!o.ADSPath.StartsWith("LDAP"))
								wd = ServiceBusinessObjects.WhereDefined.Local;
							this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), wd));
							this.modified = true;
						}
					}
					this.RefreshStoreGroupProperties();
				}
				this.HourGlass(false);
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}

			//try {
			//	MessageBox.Show(this, "Solo se mostrará los usuarios de la red local. Esta función carece de utilidad cuando accede fuera de las instalaciones o red de la oficina.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			//	this.HourGlass(true);
			//	ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this.storeGroup.Store.Storage.Mode == NetSqlAzManMode.Developer);
			//	/*Application.DoEvents();*/
			//	if (res != null) {
			//		foreach (ADObject o in res) {
			//			if (!this.MembersToRemove.Remove(o.Sid) && !this.FindMember(this.storeGroup.GetStoreGroupMembers(), new SqlAzManSID(o.Sid)) && !this.FindMember(this.MembersToAdd, new SqlAzManSID(o.Sid))) {
			//				WhereDefined wd = WhereDefined.LDAP;
			//				if (!o.ADSPath.StartsWith("LDAP"))
			//					wd = WhereDefined.Local;
			//				this.MembersToAdd.Add(new GenericMember(o.Name, new SqlAzManSID(o.Sid), wd));
			//				this.modified = true;
			//			}
			//		}
			//		this.RefreshStoreGroupProperties();
			//	}
			//	this.HourGlass(false);
			//}
			//catch (Exception ex) {
			//	this.HourGlass(false);
			//	MessageBox.Show(ex.Message);
			//}
		}

		private void btnMembersRemove_Click(object sender, EventArgs e)
		{
			this.HourGlass(true);
			foreach (ListViewItem lvi in this.lsvMembers.SelectedItems)
			{
				if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null)
				{
					ServiceBusinessObjects.AzManStoreGroup lviTag = (ServiceBusinessObjects.AzManStoreGroup)(lvi.Tag);
					this.MembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Store));
					this.modified = true;
				}
				else if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroupMember) != null)
				{
					ServiceBusinessObjects.AzManStoreGroupMember lviTag = (ServiceBusinessObjects.AzManStoreGroupMember)(lvi.Tag);
					this.MembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.SID.StringValue, lviTag.SID, lviTag.WhereDefined));
					this.modified = true;
				}
				else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null)
				{
					ServiceBusinessObjects.AzManGenericMember lviTag = (ServiceBusinessObjects.AzManGenericMember)(lvi.Tag);
					if (this.MembersToAdd.Remove(lviTag.sid.StringValue))
						this.modified = true;
				}
			}
			this.RefreshStoreGroupProperties();
			if (this.lsvMembers.Items.Count == 0)
				this.btnMembersRemove.Enabled = false;
			this.HourGlass(false);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);
				this.CommitChanges();
				this.btnApply.Enabled = false;
			}
			catch
			{
				throw;
				//this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
			}
			finally
			{
				this.HourGlass(false);
			}
		}

		private void CommitChanges()
		{
			if (!this.modified)
				return;

			var _modifiedBso = _storeGroup.Clone();

			//Store Group Properties
			_modifiedBso.Name = txtName.Text.Trim();
			_modifiedBso.Description = this.txtDescription.Text.Trim();
			_modifiedBso.GroupType = this._storeGroup.GroupType;

			if (this._storeGroup.GroupType == ServiceBusinessObjects.GroupType.Basic)
			{
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
			else
			{
				_modifiedBso.LDAPQuery = this.txtLDapQuery.Text.Trim();
			}

			ServiceBusinessObjects.AzManStoreGroup _updated = null;
			#region Call WebApi				
			var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
			var _return = Task.Run(() => _h.PutAsync(_storeGroup.Name, _modifiedBso)).Result;
			if (_h.IsResponseContentError(_return))
			{
				throw _h.GetWebApiRequestException(_return);
			}
			else
			{
				_updated = _h.GetSBOFromReturnedContent(_return);
			}
			//if (_return.ContainsKey("error")) {
			//    var _values = _return["error"].ToArray();
			//    var _requestUri = _values[0].ToString();
			//    var _respMsg = (HttpResponseMessage)_values[1];

			//    throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			//}
			//else {
			//    var _values = _return["contentData"].ToArray();
			//    _updated = (ServiceBusinessObjects.AzManStoreGroup)_values[0];
			//}
			#endregion

			_storeGroup = _updated;

			this.MembersToAdd.Clear();
			this.MembersToRemove.Clear();
			this.NonMembersToAdd.Clear();
			this.NonMembersToRemove.Clear();
			this.modified = false;

			//try {
			//	if (!this.modified)
			//		return;
			//	//Store Group Properties
			//	this.storeGroup.Store.Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
			//	this.storeGroup.Rename(this.txtName.Text.Trim());
			//	this.storeGroup.Update(this.txtDescription.Text.Trim(), this.storeGroup.GroupType);
			//	if (this.storeGroup.GroupType == GroupType.Basic) {
			//		//Members
			//		//Members To Add
			//		#region ***PERSONALIZADO***
			//		foreach (GenericMember member in this.MembersToAdd) {
			//			this.storeGroup.CreateStoreGroupMemberCustom(member.sid, member.WhereDefined, true, member.LDAPDomain);
			//		}
			//		#endregion
			//		//Members To Remove
			//		foreach (GenericMember member in this.MembersToRemove) {
			//			this.storeGroup.GetStoreGroupMember(member.sid).Delete();
			//		}
			//		//Non Members
			//		//Non Members To Add
			//		foreach (GenericMember nonMember in this.NonMembersToAdd) {
			//			this.storeGroup.CreateStoreGroupMember(nonMember.sid, nonMember.WhereDefined, false);
			//		}
			//		//Non Members To Remove
			//		foreach (GenericMember nonMember in this.NonMembersToRemove) {
			//			this.storeGroup.GetStoreGroupMember(nonMember.sid).Delete();
			//		}
			//		this.MembersToAdd.Clear();
			//		this.MembersToRemove.Clear();
			//		this.NonMembersToAdd.Clear();
			//		this.NonMembersToRemove.Clear();
			//		this.modified = false;
			//	}
			//	else {
			//		this.storeGroup.UpdateLDapQuery(this.txtLDapQuery.Text.Trim());
			//	}
			//	this.storeGroup.Store.Storage.CommitTransaction();
			//}
			//catch {
			//	this.storeGroup.Store.Storage.RollBackTransaction();
			//	throw;
			//}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);
				this.CommitChanges();
				this.btnApply.Enabled = false;
				this.DialogResult = DialogResult.OK;
			}
			catch
			{
				throw;
				//this.DialogResult = DialogResult.None;
				//this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("UpdateError_Msg10"));
			}
			finally
			{
				this.HourGlass(false);
			}
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			this.modified = true;
			this.btnApply.Enabled = true;
		}

		private void btnNonMembersAddStoreGroup_Click(object sender, EventArgs e)
		{
			this.HourGlass(true);
			frmStoreGroupsList frm = new frmStoreGroupsList(_webApiUri);
			frm._store = this._storeGroup.Store;
			frm._storeGroup = this._storeGroup;
			DialogResult dr = frm.ShowDialog(this);
			this.HourGlass(true);
			if (dr == DialogResult.OK)
			{
				foreach (var sg in frm._selectedStoreGroups)
				{
					if (!this.NonMembersToRemove.Remove(sg.SID.StringValue))
					{
						var _nonMembers = this.getStoreGroupMembersOrNonMembers(false);
						if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(_nonMembers.ToArray(), sg.SID))
						{
							this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(sg.Name, sg.SID, ServiceBusinessObjects.WhereDefined.Store));
							this.modified = true;
						}
					}
				}
				this.RefreshStoreGroupProperties();
			}
			this.HourGlass(false);

			//this.HourGlass(true);
			//frmStoreGroupsList frm = new frmStoreGroupsList();
			//frm.store = this.storeGroup.Store;
			//frm.storeGroup = this.storeGroup;
			//DialogResult dr = frm.ShowDialog(this);
			//this.HourGlass(true);
			//if (dr == DialogResult.OK) {
			//	foreach (IAzManStoreGroup sg in frm.selectedStoreGroups) {
			//		if (!this.NonMembersToRemove.Remove(sg.SID.StringValue)) {
			//			if (!this.NonMembersToAdd.ContainsByObjectSid(sg.SID.StringValue) && !this.FindMember(this.storeGroup.GetStoreGroupNonMembers(), sg.SID)) {
			//				this.NonMembersToAdd.Add(new GenericMember(sg.Name, sg.SID, WhereDefined.Store));
			//				this.modified = true;
			//			}
			//		}
			//	}
			//	this.RefreshStoreGroupProperties();
			//}
			//this.HourGlass(false);			
		}

		private void btnNonMembersAddWindowsUsersAndGroup_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);
				ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this._storeGroup.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Developer);
				if (res != null)
				{
					foreach (ADObject o in res)
					{
						if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(this._storeGroup.Members.Where(i => i.IsMember.Equals(false)).ToArray(), new ServiceBusinessObjects.AzManSid(o.Sid)) && !this.FindMember(this.NonMembersToAdd, new ServiceBusinessObjects.AzManSid(o.Sid)))
						{
							var wd = ServiceBusinessObjects.WhereDefined.LDAP;
							if (!o.ADSPath.StartsWith("LDAP"))
								wd = ServiceBusinessObjects.WhereDefined.Local;
							this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), wd));
							this.modified = true;
						}
					}
					this.RefreshStoreGroupProperties();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}

			//this.HourGlass(true);
			//ADObject[] res = DirectoryServicesUtils.ADObjectPickerShowDialog(this, this.storeGroup.Store.Storage.Mode == NetSqlAzManMode.Developer);
			//if (res != null) {
			//	foreach (ADObject o in res) {
			//		if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(this.storeGroup.GetStoreGroupNonMembers(), new SqlAzManSID(o.Sid)) && !this.FindMember(this.NonMembersToAdd, new SqlAzManSID(o.Sid))) {
			//			WhereDefined wd = WhereDefined.LDAP;
			//			if (!o.ADSPath.StartsWith("LDAP"))
			//				wd = WhereDefined.Local;
			//			this.NonMembersToAdd.Add(new GenericMember(o.Name, new SqlAzManSID(o.Sid), wd));
			//			this.modified = true;
			//		}
			//	}
			//	this.RefreshStoreGroupProperties();
			//}
			//this.HourGlass(false);
		}

		private void btnNonMembersRemove_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in this.lsvNonMembers.SelectedItems)
			{
				if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroup) != null)
				{
					ServiceBusinessObjects.AzManStoreGroup lviTag = (ServiceBusinessObjects.AzManStoreGroup)lvi.Tag;
					this.NonMembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.Name, lviTag.SID, ServiceBusinessObjects.WhereDefined.Store));
					this.modified = true;
				}
				else if ((lvi.Tag as ServiceBusinessObjects.AzManStoreGroupMember) != null)
				{
					ServiceBusinessObjects.AzManStoreGroupMember lviTag = (ServiceBusinessObjects.AzManStoreGroupMember)lvi.Tag;
					this.NonMembersToRemove.Add(new ServiceBusinessObjects.AzManGenericMember(lviTag.SID.StringValue, lviTag.SID, lviTag.WhereDefined));
					this.modified = true;
				}
				else if ((lvi.Tag as GenericMember) != null)
				{
					GenericMember lviTag = (GenericMember)lvi.Tag;
					if (this.NonMembersToAdd.Remove(lviTag.sid.StringValue))
						this.modified = true;
				}
			}
			this.RefreshStoreGroupProperties();
			if (this.lsvNonMembers.Items.Count == 0)
				this.btnNonMembersRemove.Enabled = false;
			this.HourGlass(false);
		}

		private void lsvMembers_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this.lsvMembers.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
				this.btnMembersRemove.Enabled = false;
			else
				this.btnMembersRemove.Enabled = true && this._storeGroup.Store.IAmManager;
		}

		private void lsvNonMembers_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this.lsvNonMembers.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
				this.btnNonMembersRemove.Enabled = false;
			else
				this.btnNonMembersRemove.Enabled = true && this._storeGroup.Store.IAmManager;

		}

		private void txtLDapQuery_TextChanged(object sender, EventArgs e)
		{
			this.btnApply.Enabled = true;
			this.modified = true;
		}

		private void lsvMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.btnMembersRemove.Enabled = this.lsvMembers.SelectedItems.Count > 0 && this._storeGroup.Store.IAmManager;
		}

		private void lsvNonMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			this.btnNonMembersRemove.Enabled = this.lsvNonMembers.SelectedItems.Count > 0 && this._storeGroup.Store.IAmManager;
		}

		private void btnTestLDapQuery_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);
				frmActiveDirectoryObjectsList frm = new frmActiveDirectoryObjectsList();
				frm.Text = Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Msg20") + this._storeGroup.Name;

				IEnumerable<ServiceBusinessObjects.LDAPResult> _resultList = null;
				#region Call WebApi
				var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>(_webApiUri);
				var _return = Task.Run(() => _h.GetLDAPQueryResultAsync(_storeGroup.Store.Name, _storeGroup.Name, this.txtLDapQuery.Text)).Result;
				if (_h.IsResponseContentError(_return))
					_h.ThrowWebApiRequestException(_return);
				else
					_resultList = _h.GetEnumerableSBOFromReturnedContent(_return);
				#endregion
				frm.ldapResultList = _resultList;
				frm.ShowDialog(this);

				this.HourGlass(false);
			}
			catch
			{
				this.HourGlass(false);
				//this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmApplicationGroupsProperties_Tit30"));
				throw;
			}
		}

		private void frmStoreGroupsProperties_Activated(object sender, EventArgs e)
		{
			if (this.firstShow)
			{
				this.txtName.Focus();
				this.txtName.SelectAll();
				this.firstShow = false;
			}
		}

		private void lsvMembers_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			this.SortListView(this.lsvMembers);
		}

		private void lsvNonMembers_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			this.SortListView(this.lsvNonMembers);
		}

		private void btnMembersAddDBUsers_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);

				frmDBUsersList frm = new frmDBUsersList(_webApiUri);
				frm._store = this._storeGroup.Store;
				DialogResult dr = frm.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					foreach (var dbUser in frm._selectedDBUsers)
					{
						if (!this.MembersToRemove.Remove(dbUser.CustomSid.StringValue))
						{
							var _members = this.getStoreGroupMembersOrNonMembers(true);
							if (!this.MembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(_members.ToArray(), dbUser.CustomSid))
							{
								this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(dbUser.UserName, dbUser.CustomSid, ServiceBusinessObjects.WhereDefined.Database));
								this.modified = true;
							}
						}
					}
					this.RefreshStoreGroupProperties();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}

			//this.HourGlass(true);
			//frmDBUsersList frm = new frmDBUsersList();
			//frm.store = this.storeGroup.Store;
			//DialogResult dr = frm.ShowDialog(this);
			//if (dr == DialogResult.OK) {
			//	foreach (IAzManDBUser dbUser in frm.selectedDBUsers) {
			//		if (!this.MembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
			//			if (!this.MembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(this.storeGroup.GetStoreGroupMembers(), dbUser.CustomSid)) {
			//				this.MembersToAdd.Add(new GenericMember(dbUser.UserName, dbUser.CustomSid, WhereDefined.Database));
			//				this.modified = true;
			//			}
			//		}
			//	}
			//	this.RefreshStoreGroupProperties();
			//}
			//this.HourGlass(false);
		}

		private void btnNonMembersAddDBUsers_Click(object sender, EventArgs e)
		{
			this.HourGlass(true);
			frmDBUsersList frm = new frmDBUsersList(_webApiUri);
			frm._store = this._storeGroup.Store;
			DialogResult dr = frm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				foreach (var dbUser in frm._selectedDBUsers)
				{
					if (!this.NonMembersToRemove.Remove(dbUser.CustomSid.StringValue))
					{
						var _nonMembers = this.getStoreGroupMembersOrNonMembers(false);
						if (!this.NonMembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(_nonMembers.ToArray(), dbUser.CustomSid))
						{
							this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(dbUser.UserName, dbUser.CustomSid, ServiceBusinessObjects.WhereDefined.Database));
							this.modified = true;
						}
					}
				}
				this.RefreshStoreGroupProperties();
			}
			this.HourGlass(false);

			//this.HourGlass(true);
			//frmDBUsersList frm = new frmDBUsersList();
			//frm.store = this.storeGroup.Store;
			//DialogResult dr = frm.ShowDialog(this);
			//if (dr == DialogResult.OK) {
			//	foreach (IAzManDBUser dbUser in frm.selectedDBUsers) {
			//		if (!this.NonMembersToRemove.Remove(dbUser.CustomSid.StringValue)) {
			//			if (!this.NonMembersToAdd.ContainsByObjectSid(dbUser.CustomSid.StringValue) && !this.FindMember(this.storeGroup.GetStoreGroupNonMembers(), dbUser.CustomSid)) {
			//				this.NonMembersToAdd.Add(new GenericMember(dbUser.UserName, dbUser.CustomSid, WhereDefined.Database));
			//				this.modified = true;
			//			}
			//		}
			//	}
			//	this.RefreshStoreGroupProperties();
			//}
			//this.HourGlass(false);			
		}

		#region ***PERSONALIZADO***
		private void butnAddLDAPObjects_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);

				var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
				if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;

				ADObject[] _sel = _f.SelectedADObjects;
				if (_sel != null)
				{
					var _members = this.getStoreGroupMembersOrNonMembers(true);
					foreach (ADObject _o in _sel)
					{
						if (!this.MembersToRemove.Remove(_o.Sid) && !this.FindMember(_members.ToArray(), new ServiceBusinessObjects.AzManSid(_o.LdapSid)) && !this.FindMember(this.MembersToAdd, new ServiceBusinessObjects.AzManSid(_o.Sid)))
						{
							this.MembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(_o.Name, new ServiceBusinessObjects.AzManSid(_o.Sid), ServiceBusinessObjects.WhereDefined.LDAP, _o.DomainProfile, _o.samAccountName, _o.cn, _o.displayName, _o.LdapSid, _o.distinguishedName, _o.ClassName));
							this.modified = true;
						}
					}
					this.RefreshStoreGroupProperties();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}
		}

		private void butnNonMembersAddLDAPObjects_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);

				var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
				if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;

				ADObject[] res = _f.SelectedADObjects;
				if (res != null)
				{
					var _nonMembers = this.getStoreGroupMembersOrNonMembers(false);
					foreach (ADObject o in res)
					{
						if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember(_nonMembers.ToArray(), new ServiceBusinessObjects.AzManSid(o.LdapSid)) && !this.FindMember(this.NonMembersToAdd, new ServiceBusinessObjects.AzManSid(o.Sid)))
						{
							this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), ServiceBusinessObjects.WhereDefined.LDAP, o.DomainProfile, o.samAccountName, o.cn, o.displayName, o.LdapSid, o.distinguishedName, o.ClassName));
							this.modified = true;
						}
					}
					this.RefreshStoreGroupProperties();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				this.HourGlass(false);
			}

			//try {
			//	this.HourGlass(true);

			//	var _f = new AzManWinUI.LDAPServices.LDAPQueryClientUI(_webApiUri);
			//	if (_f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			//		return;

			//	ADObject[] res = _f.SelectedADObjects;
			//	if (res != null) {
			//		foreach (ADObject o in res) {
			//			if (!this.NonMembersToRemove.Remove(o.Sid) && !this.FindMember2(this._storeGroup.Members.Where(f => f.IsMember == false).ToArray(), new ServiceBusinessObjects.AzManSid(o.LDAPSid)) && !this.FindMember2(this.NonMembersToAdd, new ServiceBusinessObjects.AzManSid(o.Sid))) {
			//				this.NonMembersToAdd.Add(new ServiceBusinessObjects.AzManGenericMember(o.Name, new ServiceBusinessObjects.AzManSid(o.Sid), ServiceBusinessObjects.WhereDefined.LDAP, o.DomainProfile));
			//				this.modified = true;
			//			}
			//		}
			//		this.RefreshStoreGroupProperties();
			//	}
			//}
			//catch {
			//	throw;
			//}
			//finally {
			//	this.HourGlass(false);
			//}
		}
		#endregion
	}
}