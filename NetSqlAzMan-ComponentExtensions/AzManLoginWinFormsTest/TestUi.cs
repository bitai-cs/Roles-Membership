using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using AzManLoginHelper.AzManLoginSvcRef;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;


namespace AzManLoginWinFormsTest
{
	public partial class TestUi : Form
	{
		BindingList<DBUser> _list = new BindingList<DBUser>();

		public TestUi() {
			InitializeComponent();
			InitSkinGallery();

			this.combStore.Properties.Items.AddRange(new object[] { "SISE", "¿?¿?¿?" });

			this.combApplication.Properties.Items.AddRange(new object[] { "LOGIN", "CAMPUS_ADMINISTRATVO", "¿?¿?¿?" });

			this.combItem.Properties.Items.AddRange(new object[] { "IniciarSesion", "¿?¿?¿?" });
		}

		void InitSkinGallery() {
			SkinHelper.InitSkinGallery(rgbiSkins, true);
		}

		private void iExit_ItemClick(object sender, ItemClickEventArgs e) {
			this.Close();
		}

		private void simpleButton2_Click(object sender, EventArgs e) {
			lablBranch.Text = "¿?";

			var _rows = gridView1.GetSelectedRows();
			var _row = gridView1.GetRow(_rows[0]);
			DBUser _dbUser = (DBUser)_row;
			AzManLoginWinForms.BranchSelectorUI _f = new AzManLoginWinForms.BranchSelectorUI(_dbUser);

			if (_f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				lablBranch.Text = _f.SelectedBranchItem.ToString();
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e) {
			AzManLoginWinForms.LoginUI _f = new AzManLoginWinForms.LoginUI(Application.ProductName, combStore.SelectedItem.ToString(), combApplication.SelectedItem.ToString(), combItem.SelectedItem.ToString(), "ADMCAMPUS", "123456");
			if (_f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				_list.Add(_f.DBUser);
				XtraMessageBox.Show(this, _f.DBUser.UserName);
			}
			else {
				XtraMessageBox.Show(this, "NO se ha iniciado sesión.");
			}
		}

		private void TestUi_Load(object sender, EventArgs e) {
			combStore.SelectedIndex = 0;
			combApplication.SelectedIndex = 0;
			combItem.SelectedIndex = 0;

			gridControl.DataSource = _list;
		}

		private void iAbout_ItemClick(object sender, ItemClickEventArgs e) {
			GridControl.About();
		}
	}
}