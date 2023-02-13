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

namespace NetSqlAzMan.CustomDataLayer.TestUI
{
	public partial class EFCFTestUI :Form
	{
		public EFCFTestUI() {
			InitializeComponent();
		}

		private async void butnListUsers_Click(object sender, EventArgs e) {
			try {
				NetSqlAzMan.CustomDataLayer.EFCF.identity_User _userDal = new EFCF.identity_User();

				var _list = await _userDal.SelectAllAsync();

				identityUserBindingSource.DataSource = _list;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private async void butnInsert_Click(object sender, EventArgs e) {
			try {
				EFCF.identity_User _user = new EFCF.identity_User() {
					UserID = -1,
					UserName = "XXXXXX",
					FirstName = "XXX",
					LastName = "XXXx",
					FullName = "DUMMY TEXT",
					Mail = null,
					Password = GetStringSha256Hash("123456"),
					Enabled = true,
					DepartmentId = 16
				};

				_user.identity_UserBranchOffice.Add(new EFCF.identity_UserBranchOffice() { UserID = -1, BranchOfficeId = "1" });
				_user.identity_UserBranchOffice.Add(new EFCF.identity_UserBranchOffice() { UserID = -1, BranchOfficeId = "2" });

				EFCF.identity_User _userDal = new EFCF.identity_User();
				using (var _cm = _userDal.GetConnectionManager(false)) {
					_cm.BeginTransaction();
					_user = await _userDal.InsertAsync(_user, _cm);
					await _cm.CommitTransaction();
				}
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private async void button1_Click(object sender, EventArgs e) {
			CustomDataLayer.EF.Identity_UserBranchOffice _dal = new Identity_UserBranchOffice();
			var _cm = _dal.GetConnectionManager(true);
			var _created = await _dal.InsertAsync(2342, 55.ToString(), _cm);
			System.Diagnostics.Debugger.Break();
		}

		private void EFTestUI_Load(object sender, EventArgs e) {

		}

		private string GetStringSha256Hash(string text) {
			if (String.IsNullOrEmpty(text))
				return String.Empty;

			using (var sha = new System.Security.Cryptography.SHA256Managed()) {
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}

		private async void button2_Click(object sender, EventArgs e) {
			var _dal = new EFCF.ldapwac_DomainProfile();
			var _all = await _dal.SelectAllAsync();

			ldapwacDomainProfileBindingSource.DataSource = _all;
		}

		private void button3_Click(object sender, EventArgs e) {
			var _dp = (NetSqlAzMan.CustomDataLayer.EFCF.ldapwac_DomainProfile)ldapwacDomainProfileBindingSource.Current;

			var _dal = new EFCF.ldapwac_DomainProfile();
			using (var _cm = _dal.GetConnectionManager(false)) {
				var _return = _dal.ldapwac_fn_DomainProfileIsInUse(_dp.DomainProfile, _cm);
				MessageBox.Show("Resultado: " + _return.Value.ToString());
			}
		}
	}
}
