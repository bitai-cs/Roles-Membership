using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace AzManWinUI
{
	public partial class ChangePwdUI : Form
	{
		private Exception pvexceError;

		public ChangePwdUI()
		{
			InitializeComponent();
		}

		private bool validatePassword(string password, out bool result, out Exception hex)
		{
			result = false;
			hex = null;

			try
			{
				CryptographyManager crypto = EnterpriseLibraryContainer.Current.GetInstance<CryptographyManager>();
				result = crypto.CompareHash(Program.HASHPROVIDER, password, System.Configuration.ConfigurationManager.AppSettings[Program.PWDKEYNAME]);

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		private bool validateConfirmation(string newPwd, string pwdConf, out bool result, out Exception hex)
		{
			result = false;
			hex = null;

			try
			{
				if (string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(pwdConf))
					hex = new InvalidExpressionException("Es requerido que ingrese el nuevo password y la confirmación.");

				if (!newPwd.Equals(pwdConf))
					hex = new InvalidExpressionException("La nueva contraseña y la confirmación no coinciden.");

				result = hex == null;

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		internal bool saveNewPassword(string newPwd, out Exception hex)
		{
			hex = null;

			try
			{
				CryptographyManager crypto = EnterpriseLibraryContainer.Current.GetInstance<CryptographyManager>();

				string striCurrent = System.Configuration.ConfigurationManager.AppSettings[Program.PWDKEYNAME];

				string striNewHashedPwd = crypto.CreateHash(Program.HASHPROVIDER, newPwd);

				System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

				System.Configuration.AppSettingsSection appSettings =
		  config.AppSettings as System.Configuration.AppSettingsSection;

				System.Configuration.KeyValueConfigurationCollection settings = appSettings.Settings;
				System.Configuration.KeyValueConfigurationElement element = settings[Program.PWDKEYNAME];
				element.Value = striNewHashedPwd;

				config.Save();

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		private void butnOk_Click(object sender, EventArgs e)
		{
			bool boolResult;
			if (!validatePassword(txtbPassword.Text, out boolResult, out pvexceError))
			{
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				throw pvexceError;
			}

			if (!boolResult)
			{
				MessageBox.Show(this, "La contraseña actual que ha ingresado no es válida.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				return;
			}

			if (!validateConfirmation(txtbNew.Text, txtbConfirmation.Text, out boolResult, out pvexceError))
			{
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				throw pvexceError;
			}

			if (!boolResult)
			{
				MessageBox.Show(this, pvexceError.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				return;
			}

			if (!saveNewPassword(txtbNew.Text, out pvexceError))
			{
				this.DialogResult = System.Windows.Forms.DialogResult.None;
				throw pvexceError;
			}

			MessageBox.Show(this, "Se cambió la contraseña.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
