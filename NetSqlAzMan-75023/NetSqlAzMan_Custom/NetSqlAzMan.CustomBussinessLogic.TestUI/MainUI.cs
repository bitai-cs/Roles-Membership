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
	public partial class MainUI : Form {
		public MainUI() {
			InitializeComponent();
		}

		private void departamentoToolStripMenuItem_Click(object sender, EventArgs e) {
			DepartmentBOTestUI _f = new DepartmentBOTestUI();
			_f.MdiParent = this;
			_f.Show();
		}

		private void branchOfficeUIToolStripMenuItem_Click(object sender, EventArgs e) {
			BranchOfficeBOTestUI _f = new BranchOfficeBOTestUI();
			_f.MdiParent = this;
			_f.Show();
		}

		private void dBUserUIToolStripMenuItem_Click(object sender, EventArgs e) {
			DBUserBOTestUI _f = new DBUserBOTestUI();
			_f.MdiParent = this;
			_f.Show();
		}

		private void lDAPClientUIToolStripMenuItem_Click(object sender, EventArgs e) {
			LDAPClientBOTestUI _f = new LDAPClientBOTestUI();
			_f.MdiParent = this;
			_f.Show();
		}
	}
}
