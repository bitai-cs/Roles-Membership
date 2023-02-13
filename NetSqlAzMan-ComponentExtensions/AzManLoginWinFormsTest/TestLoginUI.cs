using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManLoginWinFormsTest
{
	public partial class TestLoginUI : Form
	{
		BindingList<DBUser> _list = new BindingList<DBUser>();

		public TestLoginUI() {
			InitializeComponent();

			this.combStore.Items.AddRange(new object[] { "SISE", "¿?¿?¿?" });

			this.combApplication.Items.AddRange(new object[] { "LOGIN", "CAMPUS_ADMINISTRATVO", "¿?¿?¿?" });

			this.combItem.Items.AddRange(new object[] { "IniciarSesion", "¿?¿?¿?" });
		}

		private void butnLogin_Click(object sender, EventArgs e) {
			AzManLoginWinForms.LoginUI _f = new AzManLoginWinForms.LoginUI(Application.ProductName, combStore.SelectedItem.ToString(), combApplication.SelectedItem.ToString(), combItem.SelectedItem.ToString(), "ADMCAMPUS", "v1k0b6");
			if (_f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				_list.Add(_f.DBUser);
				MessageBox.Show(this, _f.DBUser.UserName);
			}
			else {
				MessageBox.Show(this, "NO se ha iniciado sesión.");
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			lablBranch.Text = "¿?";
			var _row = gridView1.SelectedRows[0];
			DBUser _dbUser = (DBUser)_row.DataBoundItem;
			AzManLoginWinForms.BranchSelectorUI _f = new AzManLoginWinForms.BranchSelectorUI(_dbUser);

			if (_f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
				lablBranch.Text = _f.SelectedBranchItem.ToString();
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void TestLoginUI_Load(object sender, EventArgs e) {
			combStore.SelectedIndex = 0;
			combApplication.SelectedIndex = 0;
			combItem.SelectedIndex = 0;

			gridView1.DataSource = _list;
		}
	}
}