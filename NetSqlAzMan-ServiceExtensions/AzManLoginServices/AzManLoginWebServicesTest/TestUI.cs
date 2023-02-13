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
	public partial class TestUI : Form
	{
		private LoginInfo _login;

		public TestUI() {
			InitializeComponent();
		}

		private void butnAzManLoginGetLogin_Click(object sender, EventArgs e) {
			LoginServiceClient _svc = new LoginServiceClient();
			DBUser _user;
			AuthorizationType _aut;
			string _att;

			if (_svc.StartLogin(out _user, out _aut, out _login, out _att, txtbUsuario.Text, txtbContrasena.Text, "AzManLoginWebServicesTest", "SISE", "TRAMITES_ALUMNADO", "IniciarSesion")) {
				MessageBox.Show(string.Format("Login creado: {0}", _login.LoginId));
				return;
			}
			else {
				MessageBox.Show(this, _att, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			string _output;

			LoginServiceClient _svc = new LoginServiceClient();

			if (_svc.Test(out _output, "Mensaje de prueba del servicio"))
				MessageBox.Show(_output);
			else
				System.Diagnostics.Debugger.Break();
		}

		private void button2_Click(object sender, EventArgs e) {
			if (_login == null) {
				MessageBox.Show(this, "Primero debe de obtener un Login.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			PwdSvcTestUI _form = new PwdSvcTestUI(_login);
			_form.ShowDialog();
		}

		private void button3_Click(object sender, EventArgs e) {
			LoginStatusEnum _loginStatus;
			AuthorizationType _authorization;
			string _attString;

			LoginServiceClient _svc = new LoginServiceClient();
			if (_svc.CheckLoginAccess(out _loginStatus, out _authorization, out _attString, "SISE", "LOGIN", "InicioSesion", _login)) {
				MessageBox.Show(_loginStatus.ToString());
				MessageBox.Show(_authorization.ToString());
			}
			else {
				MessageBox.Show(_attString);
			}
		}
	}
}