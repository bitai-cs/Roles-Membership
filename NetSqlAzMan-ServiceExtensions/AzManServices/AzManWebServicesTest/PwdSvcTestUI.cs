using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManWebServicesTest.DirectSvcRef;

namespace AzManWebServicesTest
{
	/// <summary>
	/// User Interface
	/// </summary>
	public partial class PwdSvcTestUI : Form
	{
		DBUser _user;

		private PwdSvcTestUI() {
			InitializeComponent();
		}

		internal PwdSvcTestUI(DBUser user)
			: this() {

			_user = user;

			if (_user == null) {
				MessageBox.Show("Debe de especificar un usuario.");
				button1.Enabled = false;
			}
			else {
				lablPrompt.Text = string.Format(lablPrompt.Text, _user.UserName);
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			string _statusType;
			string _statusMessage;
			string _statusTrace;

			txtbStatus.Text = string.Empty;

			using (DirectServiceClient _svc = new DirectServiceClient()) {
				_svc.Open();

				if (_svc.ChangePwdEx(out _statusType, out _statusMessage, out _statusTrace, _user, txtbCurrent.Text, txtbNew.Text, txtbConfirmation.Text)) {
					MessageBox.Show(this, "Se cambio la contraseña.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
					var _msg = string.Format("{0}\n\r{1}\n\r{2}", _statusType, _statusMessage, _statusTrace);

					MessageBox.Show(this, _msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

					txtbStatus.Text = _msg;
				}

				_svc.Close();
			}
		}
	}
}
