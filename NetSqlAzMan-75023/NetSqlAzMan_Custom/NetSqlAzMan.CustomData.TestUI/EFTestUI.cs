using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSqlAzMan.CustomDataLayer.EF;
using System.Configuration;

namespace NetSqlAzMan.CustomDataLayer.TestUI {
	public partial class EFTestUI : Form {
		public EFTestUI() {
			InitializeComponent();
		}

		private async void butnListUsers_Click(object sender, EventArgs e) {
			try {
				NetSqlAzMan.CustomDataLayer.EF.identity_User _userDal = new identity_User();

				var _list = await _userDal.SelectAllAsync();

				identityvwUsersBindingSource.DataSource = _list;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private async void butnInsert_Click(object sender, EventArgs e) {
			try {
				NetSqlAzMan.CustomDataLayer.EF.identity_User _user = new identity_User() {
					UserID = -1,
					UserName = "XXXXXX",
					FirstName = "XXX",
					LastName = "XXXx",
					Mail = null,
					Password = Encoding.Unicode.GetBytes("123456"),
					Enabled = true,
					DepartmentId = 16
				};

				_user.identity_UserBranchOffice.Add(new Identity_UserBranchOffice() { UserID = -1, BranchOfficeId = "1" });
				_user.identity_UserBranchOffice.Add(new Identity_UserBranchOffice() { UserID = -1, BranchOfficeId = "2" });

				NetSqlAzMan.CustomDataLayer.EF.identity_User _userDal = new identity_User();

				_user = await _userDal.InsertAsync(_user);
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private async void button1_Click(object sender, EventArgs e) {
			CustomDataLayer.EF.Identity_UserBranchOffice _dal = new Identity_UserBranchOffice();
			var _cm = _dal.GetConnectionManager(false);
			var _created = await _dal.InsertAsync(2342, 55.ToString(), _cm);
			System.Diagnostics.Debugger.Break();
		}

		private void EFTestUI_Load(object sender, EventArgs e) {

		}
	}
}
