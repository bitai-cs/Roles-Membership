using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetSqlAzManSnapIn.AddOn.Membership.UI
{
	public partial class UsersUI : System.Windows.Forms.Form
	{
		#region Private fields

		private Exception pvexceHandled;
		private string pvstriConnectionString = null;
		private List<Objects.UserStruct> pvlistUsers;
		#endregion

		#region Constructor

		public UsersUI(String connectionString) {
			InitializeComponent();

			//Store connection string
			pvstriConnectionString = connectionString;
		}

		#endregion

		#region Public members

		public bool LoadData(out Exception hex) {
			hex = null;

			try {
				using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
					if (!busoLogic.GetUsers(out pvlistUsers, out pvexceHandled))
						throw pvexceHandled;

				userStructBindingSource.DataSource = pvlistUsers;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Private event handlers

		private void new_OnClick(object sender, EventArgs e) {
			AddOn.Membership.UI.UserUI formRecord = new UserUI(pvstriConnectionString, tsbtCloseOnSave.Checked);

			formRecord.RecordSaved += new EventHandler<EventArgs>(formRecord_RecordSaved);

			if (formRecord.NewRecord(out pvexceHandled)) {
				formRecord.ShowDialog(this);
			}
		}

		private void formRecord_RecordSaved(object sender, EventArgs e) {
			refreshData_OnClick(tsbtRefresh, new EventArgs());
		}

		private void properties_OnClick(object sender, EventArgs e) {
			if (dgvwUsers.SelectedRows.Count == 0) {
				MessageBox.Show("Primero seleccione un usuario.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			Objects.UserStruct struRecord = (Objects.UserStruct)dgvwUsers.SelectedRows[0].DataBoundItem;

			using (AddOn.Membership.UI.UserUI formRecord = new UserUI(pvstriConnectionString, tsbtCloseOnSave.Checked)) {
				formRecord.RecordSaved += new EventHandler<EventArgs>(formRecord_RecordSaved);

				formRecord.UserRecord = struRecord;

				if (formRecord.EditRecord(out pvexceHandled))
					formRecord.ShowDialog(this);
				else
					MessageBox.Show(this, pvexceHandled.Message + "\n\r" + pvexceHandled.StackTrace, pvexceHandled.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void refreshData_OnClick(object sender, EventArgs e) {
			if (!this.LoadData(out pvexceHandled)) {
				throw pvexceHandled;
			}
		}

		private void changeStatus_Click(object sender, EventArgs e) {
			if (dgvwUsers.SelectedRows.Count == 0) {
				MessageBox.Show("Primero seleccione un usuario.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			Objects.UserStruct struRecord = (Objects.UserStruct)dgvwUsers.SelectedRows[0].DataBoundItem;

			using (AddOn.Membership.UI.UserUI formRecord = new UserUI(pvstriConnectionString, true)) {
				formRecord.RecordSaved += new EventHandler<EventArgs>(formRecord_RecordSaved);

				formRecord.UserRecord = struRecord;

				if (formRecord.ChangeRecordStatus(!struRecord.Enabled.Value, out pvexceHandled))
					formRecord.ShowDialog(this);
			}
		}

		#endregion		
	}
}