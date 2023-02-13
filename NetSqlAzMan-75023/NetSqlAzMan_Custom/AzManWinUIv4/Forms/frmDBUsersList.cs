using System;
using System.Drawing;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace NetSqlAzMan.SnapIn.Forms
{
	public partial class frmDBUsersList :frmBase
	{		
		private bool refreshing = false;

		//private ServiceBusinessObjects.AzManDBUser[] dbUsers = null;

		internal ServiceBusinessObjects.AzManStore _store;
		internal ServiceBusinessObjects.AzManApplication _application;		
		internal ServiceBusinessObjects.AzManDBUser[] _selectedDBUsers;

		private string _webApiUri;

		public frmDBUsersList(string webApiUri) {
			InitializeComponent();

			_webApiUri = webApiUri;

			this._selectedDBUsers = null;
			this._store = null;
			this._application = null;
		}

		private IEnumerable<ServiceBusinessObjects.AzManDBUser> getStorageDBUsers() {
			IEnumerable<ServiceBusinessObjects.AzManDBUser> _values = null;
			#region Web Api Call
			var _h = new AzManWebApiClientHelpers.AzManDBUsersHelper<ServiceBusinessObjects.AzManDBUser>(_webApiUri);
			var _return = Task.Run(() => _h.GetAllAsync()).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_values = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion

			return _values;
		}

		private void frmDBUsersList_Load(object sender, EventArgs e) {
			this.HourGlass(true);
			this.DialogResult = DialogResult.None;

			//Filtering
			this.cmbFieldName.Items.Clear();
			this.cmbOperator.Items.Clear();
			this.cmbOperator.Items.AddRange(
				 new[] {
						  "Is",
						  "Is not",
						  "Starts with",
						  "Ends with",
						  "Does not start with",
						  "Does not end with",
						  "Contains",
						  "Does not contain"
				 });
			this.cmbOperator.SelectedIndex = 6;
			this.txtFieldValue.Text = String.Empty;
			this.RefreshDBUsersList();
			this.PreSortListView(this.lsvDBUsers);
			//PorkAround: http://lab.msdn.microsoft.com/ProductFeedback/viewFeedback.aspx?feedbackId=FDBK49664
			ImageList clonedImageList = new ImageList();
			foreach (Image image in this.imageList1.Images) {
				clonedImageList.Images.Add((Image)image.Clone());
			}
			this.lsvDBUsers.SmallImageList = clonedImageList;
			//PorkAround End
			/*Application.DoEvents();*/
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
			this.lblFilter.Text = "Filter";
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

		private void RefreshDBUsersList() {
			ServiceBusinessObjects.AzManDBUser[] _dbUsers = null;

			if (this.refreshing) return;
			this.refreshing = true;
			this.HourGlass(true);
			this.lsvDBUsers.SuspendLayout();
			try {
				this.lsvDBUsers.Items.Clear();

				if (_dbUsers == null) {
					this.cmbFieldName.Items.Add("Name");
					this.cmbFieldName.Items.Add("Custom Sid");
					if (this._store != null) {
						_dbUsers = this.getStorageDBUsers().ToArray();
					}
					else if (this._application != null) {
						_dbUsers = this.getStorageDBUsers().ToArray();
					}
					else
						throw new System.InvalidOperationException(Globalization.MultilanguageResource.GetString("frmDBUsersList_Msg10"));

					if (_dbUsers.Length > 0) {
						foreach (var customColumn in _dbUsers[0].CustomColumns) {
							this.lsvDBUsers.Columns.Add(customColumn.Key);
							this.cmbFieldName.Items.Add(customColumn.Key);
						}
					}
					this.cmbFieldName.SelectedIndex = 0;
				}

				foreach (ServiceBusinessObjects.AzManDBUser dbUser in _dbUsers) {
					if (this.isInFilter(dbUser, this.cmbFieldName.SelectedItem.ToString(), this.cmbOperator.SelectedItem.ToString(), this.txtFieldValue.Text)) {
						ListViewItem lvi = new ListViewItem();
						lvi.Tag = dbUser;
						lvi.ImageIndex = 0;
						lvi.Text = dbUser.UserName;
						lvi.SubItems.Add(dbUser.CustomSid.StringValue);
						lvi.SubItems.Add(Globalization.MultilanguageResource.GetString("frmDBUsersList_Msg20"));
						//Custom Columns
						foreach (var customColumns in dbUser.CustomColumns) {
							if (customColumns.Value == null || customColumns.Value == DBNull.Value) {
								lvi.SubItems.Add(String.Empty);
							}
							else {
								lvi.SubItems.Add(customColumns.Value.ToString());
							}
						}
						this.lsvDBUsers.Items.Add(lvi);
					}
				}
			}
			finally {
				this.HourGlass(false);
				this.lsvDBUsers.ResumeLayout(true);
				this.refreshing = false;
			}
		}

		private bool isInFilter(ServiceBusinessObjects.AzManDBUser dbuser, string fieldName, string filterOperator, string fieldValue) {
			if (fieldValue.Trim() == String.Empty)
				return true;
			//Left Value
			string leftValue = String.Empty;
			if (String.Compare(fieldName, "Name", true) == 0)
				leftValue = dbuser.UserName;
			else if (String.Compare(fieldName, "Custom Sid", true) == 0)
				leftValue = dbuser.CustomSid.StringValue;
			else {
				foreach (var customColumn in dbuser.CustomColumns) {
					if (String.Compare(fieldName, customColumn.Key, true) == 0) {
						leftValue = customColumn.Value.ToString();
						break;
					}
				}
			}
			//Right Value
			string rightValue = fieldValue.Trim();
			//Operator
			if (filterOperator == "Is" && String.Compare(leftValue, rightValue, true) == 0) return true;
			if (filterOperator == "Is not" && String.Compare(leftValue, rightValue, true) != 0) return true;
			if (filterOperator == "Starts with" && leftValue.StartsWith(rightValue, StringComparison.CurrentCultureIgnoreCase)) return true;
			if (filterOperator == "Ends with" && leftValue.EndsWith(rightValue, StringComparison.CurrentCultureIgnoreCase)) return true;
			if (filterOperator == "Does not start with" && !leftValue.StartsWith(rightValue, StringComparison.CurrentCultureIgnoreCase)) return true;
			if (filterOperator == "Does not end with" && !leftValue.EndsWith(rightValue, StringComparison.CurrentCultureIgnoreCase)) return true;
			if (filterOperator == "Contains" && leftValue.IndexOf(rightValue, StringComparison.CurrentCultureIgnoreCase) != -1) return true;
			if (filterOperator == "Does not contain" && leftValue.IndexOf(rightValue, StringComparison.CurrentCultureIgnoreCase) == -1) return true;
			//otherwise
			return false;
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

		private void frmDBUsersList_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnOk_Click(object sender, EventArgs e) {
			this.HourGlass(true);
			this._selectedDBUsers = new ServiceBusinessObjects.AzManDBUser[this.lsvDBUsers.CheckedItems.Count];
			int index = 0;
			foreach (ListViewItem lvi in this.lsvDBUsers.CheckedItems) {
				this._selectedDBUsers[index++] = (ServiceBusinessObjects.AzManDBUser)lvi.Tag;
			}
			this.DialogResult = DialogResult.OK;
			this.HourGlass(false);
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			//this.selectedDBUsers = new IAzManDBUser[0];
			this._selectedDBUsers = new ServiceBusinessObjects.AzManDBUser[0];
		}

		private void lsvDBUsers_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.SortListView(this.lsvDBUsers);
		}

		private void frmDBUsersList_Activated(object sender, EventArgs e) {
			this.HourGlass(false);
		}

		private void Filters_Changed(object sender, EventArgs e) {
			if (this.txtFieldValue.Text.Trim() != String.Empty)
				this.RefreshDBUsersList();
		}
	}
}