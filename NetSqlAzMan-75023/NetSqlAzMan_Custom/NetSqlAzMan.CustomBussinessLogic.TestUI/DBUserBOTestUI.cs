using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	public partial class DBUserBOTestUI : Form {
		ServiceBusinessObjects.DBUser _createdUser = null;
		ServiceBusinessObjects.DBUser _editedUser = null;
		ServiceBusinessObjects.DBUser _updatedUser = null;

		public DBUserBOTestUI() {
			InitializeComponent();
		}

		private List<ServiceBusinessObjects.DBUser> getUserList() {
			return new List<ServiceBusinessObjects.DBUser>() { _createdUser, _editedUser, _updatedUser };
		}

		private async void button1_Click(object sender, EventArgs e) {
			try {
				butnGetAll.Enabled = false;

				dBUserBindingSource.DataSource = null;

				NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory _bo = new DBUserBusinessFactory();

				List<NetSqlAzMan.ServiceBusinessObjects.DBUser> _l =
					await Task.Run(() => {
						return _bo.GetAllAsync();
					});

				dBUserBindingSource.DataSource = _l;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnGetAll.Enabled = true;
			}
		}

		private async void butnRegister_Click(object sender, EventArgs e) {
			try {
				ServiceBusinessObjects.Department _dep = new ServiceBusinessObjects.Department() {
					DepartmentId = 5,
					DepartmentName = "Dummy"
				};

				var _offices = new ServiceBusinessObjects.ListOfSBO<ServiceBusinessObjects.BranchOffice>() {
					new ServiceBusinessObjects.BranchOffice { BranchOfficeId = "1" },
					new ServiceBusinessObjects.BranchOffice { BranchOfficeId = "2" },
					new ServiceBusinessObjects.BranchOffice { BranchOfficeId = "3" },
					new ServiceBusinessObjects.BranchOffice { BranchOfficeId = "4" }
				};

				var _guid = Guid.NewGuid().ToString();
				ServiceBusinessObjects.DBUser _newUser = new ServiceBusinessObjects.DBUser() {
					UserName = _guid,
					FirstName = _guid.Substring(0, 10),
					LastName = _guid.Substring(10, _guid.Length - 10),
					FullName = "dumny value...",
					Mail = null,
					Password = "123456",
					PasswordConfirmation = "123456",
					Enabled = true,
					Department = _dep,
					UserBranchOffices = _offices
				};

				CustomBussinessLogic.DBUserBusinessFactory _f = new DBUserBusinessFactory();
				_createdUser = await _f.RegisterAsync(_newUser);

				bindingSource1.DataSource = getUserList();
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private async void btunUpdate_Click(object sender, EventArgs e) {
			try {
				if (_createdUser == null)
					throw new InvalidOperationException("Primero debe de registrar un usuario.");

				_editedUser = _createdUser.Clone();
				_editedUser.Department = new ServiceBusinessObjects.Department() { DepartmentId = 10 };
				_editedUser.UserBranchOffices.Remove(_editedUser.UserBranchOffices.First());
				_editedUser.UserBranchOffices.Remove(_editedUser.UserBranchOffices.First());

				CustomBussinessLogic.DBUserBusinessFactory _f = new DBUserBusinessFactory();
				_updatedUser = await _f.UpdateAsync(_editedUser);

				bindingSource1.DataSource = getUserList();
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e) {
			if (dataGridView1.SelectedRows.Count == 0)
				return;

			var _dbUser = (ServiceBusinessObjects.DBUser)dataGridView1.SelectedRows[0].DataBoundItem;
			var _frm = new DBUserPropertiesUI(_dbUser);
			_frm.Show(this);
		}
	}
}
