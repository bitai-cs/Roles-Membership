using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.SnapIn.Forms {
	public partial class frmApplicationGroupsList : frmBase {
		//internal IAzManApplication application;
		//internal IAzManApplicationGroup applicationGroup;
		//internal IAzManApplicationGroup[] selectedApplicationGroups;

		internal ServiceBusinessObjects.AzManApplication _application;
		internal ServiceBusinessObjects.AzManApplicationGroup _applicationGroup;
		internal ServiceBusinessObjects.AzManApplicationGroup[] _selectedApplicationGroups;

		private string _webApiUri;

		public frmApplicationGroupsList(string wau) {
			InitializeComponent();

			_webApiUri = wau;

			this._selectedApplicationGroups = null;
		}

		private void frmApplicationGroupsList_Load(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			this.RefreshApplicationList();
			this.PreSortListView(this.lsvApplicationGroups);
			//PorkAround: http://lab.msdn.microsoft.com/ProductFeedback/viewFeedback.aspx?feedbackId=FDBK49664
			ImageList clonedImageList = new ImageList();
			foreach (Image image in this.imageList1.Images) {
				clonedImageList.Images.Add((Image)image.Clone());
			}
			this.lsvApplicationGroups.SmallImageList = clonedImageList;
			//PorkAround End
			/*Application.DoEvents();*/
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
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

		private void RefreshApplicationList() {
			this.HourGlass(true);
			this.lsvApplicationGroups.Items.Clear();

			IEnumerable<ServiceBusinessObjects.AzManApplicationGroup> _applicationGroups = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
			var _return = Task.Run(() => _h.GetAllAsync(_application.GetStoreName(), _application.Name, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_applicationGroups = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			foreach (ServiceBusinessObjects.AzManApplicationGroup _i in _applicationGroups) {
				//Show all store Groups rather than owner, if owner is a store Group
				if ((this._applicationGroup == null) || (this._applicationGroup != null && _i.SID.StringValue != this._applicationGroup.SID.StringValue)) {
					ListViewItem lvi = new ListViewItem();
					lvi.Tag = _i;
					lvi.ImageIndex = 0;
					lvi.Text = _i.Name;
					lvi.SubItems.Add(_i.Description);
					lvi.SubItems.Add(_i.GroupType == ServiceBusinessObjects.GroupType.Basic ? Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg10") : Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg20"));
					this.lsvApplicationGroups.Items.Add(lvi);
				}
			}
			this.HourGlass(false);

			//this.HourGlass(true);
			//this.lsvApplicationGroups.Items.Clear();
			//IAzManApplicationGroup[] applicationGroups = this.application.GetApplicationGroups();
			//foreach (IAzManApplicationGroup applicationGroup in applicationGroups) {
			//	//Show all store Groups rather than owner, if owner is a store Group
			//	if ((this.applicationGroup == null) || (this.applicationGroup != null && applicationGroup.SID.StringValue != this.applicationGroup.SID.StringValue)) {
			//		ListViewItem lvi = new ListViewItem();
			//		lvi.Tag = applicationGroup;
			//		lvi.ImageIndex = 0;
			//		lvi.Text = applicationGroup.Name;
			//		lvi.SubItems.Add(applicationGroup.Description);
			//		lvi.SubItems.Add(applicationGroup.GroupType == GroupType.Basic ? Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg10") : Globalization.MultilanguageResource.GetString("frmApplicationGroupsList_Msg20"));
			//		this.lsvApplicationGroups.Items.Add(lvi);
			//	}
			//}
			//this.HourGlass(false);
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

		private void frmApplicationGroupsList_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnOk_Click(object sender, EventArgs e) {
			this.HourGlass(true);
			this._selectedApplicationGroups = new ServiceBusinessObjects.AzManApplicationGroup[this.lsvApplicationGroups.CheckedItems.Count];
			int index = 0;
			foreach (ListViewItem lvi in this.lsvApplicationGroups.CheckedItems) {
				this._selectedApplicationGroups[index++] = (ServiceBusinessObjects.AzManApplicationGroup)lvi.Tag;
			}
			this.DialogResult = DialogResult.OK;
			this.HourGlass(false);

			//this.HourGlass(true);
			//this.selectedApplicationGroups = new IAzManApplicationGroup[this.lsvApplicationGroups.CheckedItems.Count];
			//int index = 0;
			//foreach (ListViewItem lvi in this.lsvApplicationGroups.CheckedItems) {
			//	this.selectedApplicationGroups[index++] = (IAzManApplicationGroup)lvi.Tag;
			//}
			//this.DialogResult = DialogResult.OK;
			//this.HourGlass(false);
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this._selectedApplicationGroups = new ServiceBusinessObjects.AzManApplicationGroup[0];
		}

		private void lsvApplicationGroups_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.SortListView(this.lsvApplicationGroups);
		}
	}
}