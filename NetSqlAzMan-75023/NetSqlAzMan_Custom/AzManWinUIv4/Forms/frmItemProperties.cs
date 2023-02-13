using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.SnapIn.Forms {
	public partial class frmItemProperties : frmBase {
		//internal IAzManApplication application;
		//internal IAzManItem item;
		//internal ItemType itemType = ItemType.Operation;

		internal ServiceBusinessObjects.AzManApplication _application;
		internal ServiceBusinessObjects.AzManItem _item;
		internal ServiceBusinessObjects.ItemType _itemType;

		private string _webApiUri;

		private StringCollection MembersToAdd;
		private StringCollection MembersToRemove;
		private bool modified;
		private bool firstFocus = false;

		public frmItemProperties(string webApiUri) {
			_webApiUri = webApiUri;

			InitializeComponent();

			this.modified = false;
			this.MembersToAdd = new StringCollection();
			this.MembersToRemove = new StringCollection();
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

		private void frmItemProperties_Load(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			//PorkAround: http://lab.msdn.microsoft.com/ProductFeedback/viewFeedback.aspx?feedbackId=FDBK49664
			ImageList clonedImageList1 = new ImageList();
			foreach (Image image in this.imageList1.Images) {
				clonedImageList1.Images.Add((Image)image.Clone());
			}
			this.lsvRoles.SmallImageList = clonedImageList1;
			ImageList clonedImageList2 = new ImageList();
			foreach (Image image in this.imageList2.Images) {
				clonedImageList2.Images.Add((Image)image.Clone());
			}
			this.lsvTasks.SmallImageList = clonedImageList2;
			ImageList clonedImageList3 = new ImageList();
			foreach (Image image in this.imageList3.Images) {
				clonedImageList3.Images.Add((Image)image.Clone());
			}
			this.lsvOperations.SmallImageList = clonedImageList3;
			//PorkAround End

			//Item Properties
			if (this._item != null) { //Modify item
				this.btnAttributes.Enabled = true;
				this.btnBizRule.Enabled = true;
				this.PreSortListView(this.lsvRoles);
				this.PreSortListView(this.lsvTasks);
				this.PreSortListView(this.lsvOperations);
				this.tabControl1.TabPages.Remove(this.TabTasks);
				this.tabControl1.TabPages.Remove(this.TabOperations);

				switch (this._item.ItemType) {
					case ServiceBusinessObjects.ItemType.Role:
						if (!this.tabControl1.TabPages.Contains(this.TabRoles)) this.tabControl1.TabPages.Add(this.TabRoles);
						if (!this.tabControl1.TabPages.Contains(this.TabTasks)) this.tabControl1.TabPages.Add(this.TabTasks);
						if (!this.tabControl1.TabPages.Contains(this.TabOperations)) this.tabControl1.TabPages.Add(this.TabOperations);
						this.Text = this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg10");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg20");
						this.picImage.Image = this.picRole.Image;
						break;
					case ServiceBusinessObjects.ItemType.Task:
						if (!this.tabControl1.TabPages.Contains(this.TabTasks)) this.tabControl1.TabPages.Add(this.TabTasks);
						if (!this.tabControl1.TabPages.Contains(this.TabOperations)) this.tabControl1.TabPages.Add(this.TabOperations);
						this.tabControl1.TabPages.Remove(this.TabRoles);
						this.Text = this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg30");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg40");
						this.picImage.Image = this.picTask.Image;
						break;
					case ServiceBusinessObjects.ItemType.Operation:
						if (!this.tabControl1.TabPages.Contains(this.TabOperations)) this.tabControl1.TabPages.Add(this.TabOperations);
						this.tabControl1.TabPages.Remove(this.TabRoles);
						this.tabControl1.TabPages.Remove(this.TabTasks);
						this.Text = this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg50");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg60");
						this.picImage.Image = this.picOperation.Image;
						break;
				}
				if (this._item.Application.Store.Storage.Mode == ServiceBusinessObjects.AzManMode.Administrator)
					this.tabControl1.TabPages.Remove(this.TabOperations);
				this.Text += " - " + this._item.Name;
				this.txtName.Text = this._item.Name;
				this.txtDescription.Text = this._item.Description;
			}
			else { //Create a new Item
				this.btnAttributes.Enabled = false;
				this.btnBizRule.Enabled = false;
				switch (this._itemType) {
					case ServiceBusinessObjects.ItemType.Role:
						this.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg70");
						this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg80");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg20");
						this.picImage.Image = this.picRole.Image;
						break;
					case ServiceBusinessObjects.ItemType.Task:
						this.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg90");
						this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg100");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg40");
						this.picImage.Image = this.picTask.Image;
						break;
					case ServiceBusinessObjects.ItemType.Operation:
						this.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg110");
						this.lblInfo.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg120");
						this.tabItemDefinition.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg60");
						this.picImage.Image = this.picOperation.Image;
						break;
				}
				this.tabControl1.TabPages.Remove(this.TabRoles);
				this.tabControl1.TabPages.Remove(this.TabTasks);
				this.tabControl1.TabPages.Remove(this.TabOperations);
			}
			this.RefreshItems();
			this.modified = false;
			this.btnApply.Enabled = false;
			this.txtName.Focus();
			this.txtName.SelectAll();
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
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

		private void frmItemProperties_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void txtName_TextChanged(object sender, EventArgs e) {
			this.modified = true;
			if (String.IsNullOrEmpty(this.txtName.Text.Trim())) {
				this.btnOk.Enabled = this.btnApply.Enabled = false;
				this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg130"));
			}
			else {
				this.btnOk.Enabled = this.btnApply.Enabled = true;
				this.errorProvider1.SetError(this.txtName, String.Empty);
			}
		}

		private void btnOk_Click(object sender, EventArgs e) {
			try {
				this.HourGlass(true);
				this.commitChanges();
				this.btnApply.Enabled = false;
				this.DialogResult = DialogResult.OK;
			}
			catch {
				this.DialogResult = DialogResult.None;

				throw;

				//if (this._item == null) {
				//	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg140"));
				//}
				//if (this._item != null) {
				//	this.item.Application.Store.Storage.RollBackTransaction();
				//	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg150"));
				//}
			}
			finally {
				this.HourGlass(false);
			}
		}

		private void commitChanges() {
			try {
				if (this._item == null) { //Create an item
					var _new = new ServiceBusinessObjects.AzManItem() {
						Name = txtName.Text,
						Description = txtDescription.Text,
						ItemType = this._itemType,
					};
					ServiceBusinessObjects.AzManItem _created = null;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
					var _return = Task.Run(() => _h.PostItemAsync(this._application.Store.Name, this._application.Name, _new)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_created = _h.GetSBOFromReturnedContent(_return);
					#endregion
					this._item = _created;

					this.frmItemProperties_Load(this, EventArgs.Empty);
				}
				else {
					var _oldItemName = _item.Name;

					var _modified = _item.Clone();
					//Set new Values
					_modified.Name = txtName.Text;
					_modified.Description = txtDescription.Text;
					//Members To Add
					_modified.MembersToAddOrRemove.MembersToAdd.AddRange(this.MembersToAdd.Cast<string>().ToArray());
					//Members To Remove
					_modified.MembersToAddOrRemove.MembersToRemove.AddRange(this.MembersToRemove.Cast<string>().ToArray());

					ServiceBusinessObjects.AzManItem _updatedItem = null;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
					var _return = Task.Run(() => _h.PutItemAsync(_oldItemName, this._application.Store.Name, this._application.Name, _modified)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_updatedItem = _h.GetSBOFromReturnedContent(_return);
					#endregion

					this._item = _updatedItem;

					this.MembersToAdd.Clear();
					this.MembersToRemove.Clear();
					this.modified = false;

					////OLD LOGIC
					//this.item.Application.Store.Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
					//this.item.Rename(this.txtName.Text.Trim());
					//this.item.Update(this.txtDescription.Text.Trim());
					////Members
					////Members To Add
					//foreach (string member in this.MembersToAdd) {
					//	IAzManItem item = this.item.Application.GetItem(member);
					//	this.item.AddMember(item);
					//}
					////Members To Remove
					//foreach (string member in this.MembersToRemove) {
					//	IAzManItem item = this.item.Application.GetItem(member);
					//	this.item.RemoveMember(item);
					//}
					//this.MembersToAdd.Clear();
					//this.MembersToRemove.Clear();
					//this.modified = false;
					//this.item.Application.Store.Storage.CommitTransaction();
				}
				this.HourGlass(false);
			}
			catch {
				this.HourGlass(false);
				////OLD LOGIC
				//if (this.item != null && this.item.Application.Store.Storage.TransactionInProgress)
				//	this.item.Application.Store.Storage.RollBackTransaction();
				throw;
			}
		}

		private bool FindMember(IEnumerable<ServiceBusinessObjects.AzManItem> members, string name) {
			return (members.Where(f => f.Name.Equals(name)).FirstOrDefault() != null);

			////OLD LOGIC
			//foreach (IAzManItem m in members) {
			//	if (m.Name == name)
			//		return true;
			//}
			//return false;
		}

		//private bool FindMember(GenericMemberCollection members, string name) {
		private bool FindMember(ServiceBusinessObjects.AzManGenericMemberCollection members, string name) {
			return (members.Where(f => f.Name.Equals(name)).FirstOrDefault() != null);

			////OLD LOGIC
			//foreach (GenericMember m in members) {
			//	if (m.Name == name)
			//		return true;
			//}
			//return false;
		}

		private void btnAddOperation_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			frmItemsList frm = new frmItemsList(_webApiUri);
			frm._application = this._item.Application;
			frm._item = this._item;
			frm._itemType = ServiceBusinessObjects.ItemType.Operation;
			DialogResult dr = frm.ShowDialog(this);
			if (dr == DialogResult.OK) {
				IEnumerable<ServiceBusinessObjects.AzManItem> _itemMembers = this.getItemMembers();
				foreach (ServiceBusinessObjects.AzManItem selectedItem in frm._selectedItems) {
					if (!this.MembersToRemove.Contains(selectedItem.Name)) {
						//if (!this.MembersToAdd.Contains(item.Name) && !this.FindMember(this.item.GetMembers(), item.Name)) {
						if (!this.MembersToAdd.Contains(selectedItem.Name) && !this.FindMember(_itemMembers, selectedItem.Name)) {
							this.MembersToAdd.Add(selectedItem.Name);
							this.modified = true;
						}
					}
					else {
						this.MembersToRemove.Remove(selectedItem.Name);
					}
				}
				this.RefreshItems();
			}
			this.HourGlass(false);
		}

		private void btnRemoveOperation_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			this.HourGlass(true);
			foreach (ListViewItem lvi in this.lsvOperations.CheckedItems) {
				if ((lvi.Tag as ServiceBusinessObjects.AzManItem) != null) {
					ServiceBusinessObjects.AzManItem lviTag = (ServiceBusinessObjects.AzManItem)(lvi.Tag);
					this.MembersToRemove.Add(lviTag.Name);
					this.modified = true;
				}
				//else if ((lvi.Tag as GenericMember) != null) {
				else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null) {
					//GenericMember lviTag = (GenericMember)(lvi.Tag);
					ServiceBusinessObjects.AzManGenericMember lviTag = (ServiceBusinessObjects.AzManGenericMember)(lvi.Tag);
					if (this.MembersToAdd.Contains(lviTag.Name)) {
						this.MembersToAdd.Remove(lviTag.Name);
						this.modified = true;
					}
				}
			}
			this.RefreshItems();
			if (this.lsvOperations.Items.Count == 0 || this.lsvOperations.CheckedItems.Count == 0)
				this.btnRemoveOperation.Enabled = false;
			this.HourGlass(false);
		}

		private void lsvOperations_Check(object sender, ItemCheckEventArgs e) {
			if (this.lsvOperations.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
				this.btnRemoveOperation.Enabled = false;
			else
				this.btnRemoveOperation.Enabled = true;
		}

		private ListViewItem CreateListViewItem(string member) {
			ServiceBusinessObjects.AzManItem _itemMember = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManItemsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManItem>(_webApiUri);
			var _return = Task.Run(() => _h.GetItemByNameAsync(member, this._application.Store.Name, this._application.Name, false, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_itemMember = _h.GetSBOFromReturnedContent(_return);
			#endregion

			ListViewItem lvi = new ListViewItem();
			lvi.Tag = new GenericMember(_itemMember.Name, _itemMember.Description);
			lvi.Text = member;
			lvi.ImageIndex = 0;
			lvi.SubItems.Add(_itemMember.Description);

			return lvi;
		}

		//private ListViewItem CreateListViewItem(IAzManItem member) {
		private ListViewItem CreateListViewItem(ServiceBusinessObjects.AzManItem member) {
			ListViewItem lvi = new ListViewItem();
			lvi.Tag = member;
			lvi.Text = member.Name;
			lvi.ImageIndex = 0;
			lvi.SubItems.Add(member.Description);
			return lvi;

			//ListViewItem lvi = new ListViewItem();
			//lvi.Tag = member;
			//lvi.Text = member.Name;
			//lvi.ImageIndex = 0;
			//lvi.SubItems.Add(member.Description);
			//return lvi;
		}

		private void RemoveListViewItem(ref ListView lsv, string member) {
			foreach (ListViewItem lvi in lsv.Items) {
				if (lvi.Text == member) {
					lvi.Remove();
					return;
				}
			}
		}

		private void RefreshItems() {
			this.HourGlass(true);
			if (this._item != null) /*Item Properties*/{
				//Members
				//Add committed sids 
				this.lsvRoles.Items.Clear();
				this.lsvTasks.Items.Clear();
				this.lsvOperations.Items.Clear();

				Dictionary<string, IEnumerable<object>> _return;
				ServiceBusinessObjects.AzManItem _member = null;
				IEnumerable<ServiceBusinessObjects.AzManItem> _members = null;
				#region Call WebApi
				var _h = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
				_return = Task.Run(() => _h.GetItemMembersAsync(this._item.Application.Store.Name, this._item.Application.Name, this._item.Name, ServiceBusinessObjects.ItemType.All, false, false, false, false)).Result;
				if (_h.IsResponseContentError(_return))
					_h.ThrowWebApiRequestException(_return);
				else
					_members = _h.GetEnumerableSBOFromReturnedContent(_return);
				#endregion
				foreach (ServiceBusinessObjects.AzManItem _m in _members) {
					switch (_m.ItemType) {
						case ServiceBusinessObjects.ItemType.Role:
							this.lsvRoles.Items.Add(this.CreateListViewItem(_m));
							break;
						case ServiceBusinessObjects.ItemType.Task:
							this.lsvTasks.Items.Add(this.CreateListViewItem(_m));
							break;
						case ServiceBusinessObjects.ItemType.Operation:
							this.lsvOperations.Items.Add(this.CreateListViewItem(_m));
							break;
					}
				}

				//Add uncommitted sids 
				foreach (string _m in this.MembersToAdd) {
					#region Call WebApi
					_return = Task.Run(() => _h.GetItemByNameAsync(_m, this._item.Application.Store.Name, this._item.Application.Name, false, false, false, false)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_member = _h.GetSBOFromReturnedContent(_return);
					#endregion
					switch (_member.ItemType) {
						case ServiceBusinessObjects.ItemType.Role:
							this.lsvRoles.Items.Add(this.CreateListViewItem(_m));
							break;
						case ServiceBusinessObjects.ItemType.Task:
							this.lsvTasks.Items.Add(this.CreateListViewItem(_m));
							break;
						case ServiceBusinessObjects.ItemType.Operation:
							this.lsvOperations.Items.Add(this.CreateListViewItem(_m));
							break;
					}
				}

				//Remove uncommitted sids
				foreach (string _m in this.MembersToRemove) {
					#region Call WebApi
					_return = Task.Run(() => _h.GetItemByNameAsync(_m, this._item.Application.Store.Name, this._item.Application.Name, false, false, false, false)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_member = _h.GetSBOFromReturnedContent(_return);
					#endregion
					switch (_member.ItemType) {
						case ServiceBusinessObjects.ItemType.Role:
							this.RemoveListViewItem(ref this.lsvRoles, _m);
							break;
						case ServiceBusinessObjects.ItemType.Task:
							this.RemoveListViewItem(ref this.lsvTasks, _m);
							break;
						case ServiceBusinessObjects.ItemType.Operation:
							this.RemoveListViewItem(ref this.lsvOperations, _m);
							break;
					}
				}
			}

			this.btnApply.Enabled = this.modified;

			if (!this._application.IAmManager)
				this.txtName.Enabled = this.txtDescription.Enabled = this.btnOk.Enabled = this.btnApply.Enabled =
					 this.lsvRoles.Enabled = this.lsvTasks.Enabled = this.lsvOperations.Enabled =
					 this.btnAddRole.Enabled = this.btnAddTask.Enabled = this.btnAddOperation.Enabled =
					 this.btnRemoveRole.Enabled = this.btnRemoveTask.Enabled = this.btnRemoveOperation.Enabled = false;

			this.HourGlass(false);
		}

		private void btnApply_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			try {
				this.HourGlass(true);
				this.commitChanges();
				this.btnApply.Enabled = false;
				this.btnAttributes.Enabled = true;
				this.HourGlass(false);
			}
			catch (Exception ex) {
				this.HourGlass(false);
				if (this._item == null) {
					this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg140"));
				}
				if (this._item != null) {
					//this._item.Application.Store.Storage.RollBackTransaction();
					this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg150"));
				}
			}
		}

		private void frmItemProperties_Activated(object sender, EventArgs e) {
			if (!this.firstFocus) {
				this.txtName.Focus();
				this.firstFocus = true;
			}
		}

		private void lsvTasks_ItemCheck(object sender, ItemCheckEventArgs e) {
			if (this.lsvTasks.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
				this.btnRemoveTask.Enabled = false;
			else
				this.btnRemoveTask.Enabled = true && this._item.Application.IAmManager;
		}

		private void lsvRoles_ItemCheck(object sender, ItemCheckEventArgs e) {
			if (this.lsvRoles.CheckedItems.Count == 1 && e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
				this.btnRemoveRole.Enabled = false;
			else
				this.btnRemoveRole.Enabled = true && this._item.Application.IAmManager;
		}

		private IEnumerable<ServiceBusinessObjects.AzManItem> getItemMembers() {
			IEnumerable<ServiceBusinessObjects.AzManItem> _itemMembers = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
			var _return = Task.Run(() => _h.GetItemMembersAsync(this._application.Store.Name, this._application.Name, this._item.Name, ServiceBusinessObjects.ItemType.All, false, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_itemMembers = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			return _itemMembers;
		}


		private void btnAddRole_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			var _frm = new frmItemsList(_webApiUri);
			_frm._application = this._item.Application;
			_frm._item = this._item;
			_frm._itemType = ServiceBusinessObjects.ItemType.Role;
			DialogResult dr = _frm.ShowDialog(this);
			if (dr == DialogResult.OK) {
				IEnumerable<ServiceBusinessObjects.AzManItem> _itemMembers = getItemMembers();
				foreach (ServiceBusinessObjects.AzManItem item in _frm._selectedItems) {
					if (!this.MembersToRemove.Contains(item.Name)) {
						if (!this.MembersToAdd.Contains(item.Name) && !this.FindMember(_itemMembers, item.Name)) {
							this.MembersToAdd.Add(item.Name);
							this.modified = true;
						}
					}
					else {
						this.MembersToRemove.Remove(item.Name);
					}
				}
				this.RefreshItems();
			}
			this.HourGlass(false);
		}

		private void btnAddTask_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			frmItemsList frm = new frmItemsList(_webApiUri);
			frm._application = this._item.Application;
			frm._item = this._item;
			frm._itemType = ServiceBusinessObjects.ItemType.Task;
			DialogResult dr = frm.ShowDialog(this);
			if (dr == DialogResult.OK) {
				IEnumerable<ServiceBusinessObjects.AzManItem> _itemMembers = this.getItemMembers();
				foreach (ServiceBusinessObjects.AzManItem selectedItem in frm._selectedItems) {
					if (!this.MembersToRemove.Contains(selectedItem.Name)) {
						if (!this.MembersToAdd.Contains(selectedItem.Name) && !this.FindMember(_itemMembers, selectedItem.Name)) {
							this.MembersToAdd.Add(selectedItem.Name);
							this.modified = true;
						}
					}
					else {
						this.MembersToRemove.Remove(selectedItem.Name);
					}
				}
				this.RefreshItems();
			}
			this.HourGlass(false);
		}

		private void btnRemoveRole_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			this.HourGlass(true);
			foreach (ListViewItem lvi in this.lsvRoles.CheckedItems) {
				if ((lvi.Tag as ServiceBusinessObjects.AzManItem) != null) {
					ServiceBusinessObjects.AzManItem lviTag = (ServiceBusinessObjects.AzManItem)(lvi.Tag);
					this.MembersToRemove.Add(lviTag.Name);
					this.modified = true;
				}
				//else if ((lvi.Tag as GenericMember) != null) {
				else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null) {
					//GenericMember lviTag = (GenericMember)(lvi.Tag);
					ServiceBusinessObjects.AzManGenericMember lviTag = (ServiceBusinessObjects.AzManGenericMember)(lvi.Tag);
					if (this.MembersToAdd.Contains(lviTag.Name)) {
						this.MembersToAdd.Remove(lviTag.Name);
						this.modified = true;
					}
				}
			}
			this.RefreshItems();
			if (this.lsvRoles.Items.Count == 0 || this.lsvRoles.CheckedItems.Count == 0)
				this.btnRemoveRole.Enabled = false;
			this.HourGlass(false);
		}

		private void btnRemoveTask_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			this.HourGlass(true);
			foreach (ListViewItem lvi in this.lsvTasks.CheckedItems) {
				//if ((lvi.Tag as IAzManItem) != null) {
				if ((lvi.Tag as ServiceBusinessObjects.AzManItem) != null) {
					ServiceBusinessObjects.AzManItem lviTag = (ServiceBusinessObjects.AzManItem)(lvi.Tag);
					this.MembersToRemove.Add(lviTag.Name);
					this.modified = true;
				}
				//else if ((lvi.Tag as GenericMember) != null) {
				else if ((lvi.Tag as ServiceBusinessObjects.AzManGenericMember) != null) {
					ServiceBusinessObjects.AzManGenericMember lviTag = (ServiceBusinessObjects.AzManGenericMember)(lvi.Tag);
					if (this.MembersToAdd.Contains(lviTag.Name)) {
						this.MembersToAdd.Remove(lviTag.Name);
						this.modified = true;
					}
				}
			}
			this.RefreshItems();
			if (this.lsvTasks.Items.Count == 0 || this.lsvTasks.CheckedItems.Count == 0)
				this.btnRemoveTask.Enabled = false;
			this.HourGlass(false);
		}

		private void lsvRoles_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.SortListView(this.lsvRoles);
		}

		private void lsvTasks_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.SortListView(this.lsvTasks);
		}

		private void lsvOperations_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.SortListView(this.lsvOperations);
		}

		private void btnAttributes_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring");
			//try {
			//	frmItemAttributes frm = new frmItemAttributes();
			//	frm.Text += " - " + this._item.Name;
			//	frm.item = this.item;
			//	frm.ShowDialog(this);
			//	/*Application.DoEvents();*/
			//	frm.Dispose();
			//	/*Application.DoEvents();*/
			//}
			//catch (Exception ex) {
			//	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg160"));
			//}
		}

		private void btnBizRule_Click(object sender, EventArgs e) {
			MessageBox.Show("Refactoring");
			//try {
			//	frmBizRule frm = new frmBizRule();
			//	frm.Text = Globalization.MultilanguageResource.GetString("frmItemProperties_Msg170") + this._item.Name;
			//	frm.item = this.item;
			//	frm.ShowDialog(this);
			//	/*Application.DoEvents();*/
			//	frm.Dispose();
			//	/*Application.DoEvents();*/
			//}
			//catch (Exception ex) {
			//	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemProperties_Msg180"));
			//}
		}
	}
}