using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManLoginWebServicesTest.LoginSvcRef;

namespace AzManLoginWebServicesTest
{
	/// <summary>
	/// User Interface
	/// </summary>
	public partial class PwdSvcTestUI : Form
	{
		LoginInfo _login;

		private PwdSvcTestUI() {
			InitializeComponent();
		}

		internal PwdSvcTestUI(LoginInfo login)
			: this() {

			_login = login;

			if (_login == null) {
				MessageBox.Show("Debe de especificar un Login.");
				button1.Enabled = false;
			}
			else {
				lablPrompt.Text = string.Format(lablPrompt.Text, _login.LoginId);
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			string _statusType;
			string _statusMessage;
			string _statusTrace;

			txtbStatus.Text = string.Empty;

			using (LoginServiceClient _svc = new LoginServiceClient()) {
				if (_svc.ChangePwdEx(out _statusType, out _statusMessage, out _statusTrace, _login, txtbCurrent.Text, txtbNew.Text, txtbConfirmation.Text)) {
					MessageBox.Show(this, "Se cambio la contraseña.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
					var _msg = string.Format("{0}\n\r{1}\n\r{2}", _statusType, _statusMessage, _statusTrace);

					txtbStatus.Text = _msg;

					MessageBox.Show(this, _msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
