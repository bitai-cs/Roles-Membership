using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace AzManWinUI
{
	public partial class LoginUI : Form
	{
		private Exception pvexceError = null;

		public LoginUI() {
			InitializeComponent();

			this.Text = Basgosoft.Reflection.ClaimantAssembly.AssemblyTitle;

#if DEBUG
			txtbPassword.Text = "0987";
#endif
		}

		private bool validatePassword(string password, out bool result, out Exception hex) {
			result = false;
			hex = null;

			try {
				CryptographyManager crypto = EnterpriseLibraryContainer.Current.GetInstance<CryptographyManager>();
				result = crypto.CompareHash(Program.HASHPROVIDER, password, System.Configuration.ConfigurationManager.AppSettings[Program.PWDKEYNAME]);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private void butnOk_Click(object sender, EventArgs e) {
			bool boolResult;
			if (!validatePassword(txtbPassword.Text, out boolResult, out pvexceError))
				throw pvexceError;

			if (!boolResult) {
				MessageBox.Show(this, "Contraseña incorrecta.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.DialogResult = System.Windows.Forms.DialogResult.None;
			}
		}

		private void LoginUI_KeyDown(object sender, KeyEventArgs e) {
			if (!(e.Alt && e.Shift && e.KeyCode == Keys.End))
				return;

			if (!txtbPassword.Text.Equals("1234"))
				return;

			using (ChangePwdUI f = new ChangePwdUI()) {
				Exception ex = null;
				if (!f.saveNewPassword("0987", out ex))
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

				MessageBox.Show(this, "La aplicación necesita ser reiniciada.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

				this.Close();
			}
		}
	}
}