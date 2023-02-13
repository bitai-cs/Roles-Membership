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
	public partial class DBUserPropertiesUI : Form {
		private ServiceBusinessObjects.DBUser _dbUser;

		public DBUserPropertiesUI(ServiceBusinessObjects.DBUser dbUser) {
			InitializeComponent();

			_dbUser = dbUser;

			propertyGrid1.SelectedObject = _dbUser;
		}

		private async void butnChange_Click(object sender, EventArgs e) {
			_dbUser.Password = txtbNew.Text;
			_dbUser.PasswordConfirmation = txtbConfirmation.Text;

			var _f = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();
			var _return = await _f.ChangePasswordAsync(_dbUser);

			Program.ShowInfoMessage(this, this.Text, "Se cambió la contraseña.");
		}
	}
}
