using System;
using System.Windows.Forms;

namespace AzManWebServicesTest
{
	public partial class DirectSvcTestUI : Form
	{
		DirectSvcRef.DBUser _user;
		DirectSvcRef.SqlAzManDBUser _azManUser;
		string _statusType;
		string _status;
		string _stackTrace;

		public DirectSvcTestUI() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			DirectSvcRef.DirectServiceClient c = new DirectSvcRef.DirectServiceClient();

			string attributeString;
			try {
				_user = c.DirectGetDBUser(txtbUserName.Text);
				attributeString = string.Concat("UserID: ", _user.UserID.ToString(), Environment.NewLine, "User: ", _user.UserName, Environment.NewLine, "AttributeString: ", _user.AttributeString);
			}
			catch (Exception ex) {
				attributeString = string.Concat("Error a obtener informacion del usuario.", ":\n\r", ex.Message, "\n\r", ex.StackTrace);
			}

			txtbOutput.Text += string.Concat(attributeString, Environment.NewLine, Environment.NewLine);
		}

		private void button3_Click(object sender, EventArgs e) {
			//string striSvcUrl = global::AzManWebServicesTest.Properties.Settings.Default.AzManWebServicesTest_DirectSvcRef_URI;

			DirectSvcRef.DirectServiceClient c = new DirectSvcRef.DirectServiceClient();
			//c.Url = striSvcUrl;

			bool boolIsValid, boolSpec1;
			string attributeString;
			try {
				//c.DirectValidatePassword(txtbUserName.Text, txtbPassword.Text, out boolIsValid, out boolSpec1);
				boolIsValid = c.DirectValidatePassword(txtbUserName.Text, txtbPassword.Text);
				attributeString = string.Concat("Es válido: ", boolIsValid.ToString());
			}
			catch (Exception ex) {
				attributeString = string.Concat("Error al validar la contraseña del usuario.", ":\n\r", ex.Message, "\n\r", ex.StackTrace);
			}

			txtbOutput.Text += string.Concat(attributeString, Environment.NewLine, Environment.NewLine);
		}

		private void button2_Click(object sender, EventArgs e) {
			//string striSvcUrl = global::AzManWebServicesTest.Properties.Settings.Default.AzManWebServicesTest_DirectSvcRef_URI;

			DirectSvcRef.DirectServiceClient c = new DirectSvcRef.DirectServiceClient();
			//c.Url = striSvcUrl;

			DirectSvcRef.AuthorizationType enumAut;
			string attributeString;
			try {
				//c.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve("SISE", "TRAMITES_ALUMNADO", txtbItemName.Text, txtbUserName.Text, DateTime.Now, true, false, true, null, out enumAut, out boolAutSpec);

				enumAut = c.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve("SISE", "TRAMITES_ALUMNADO", txtbItemName.Text, txtbUserName.Text, DateTime.Now, true, null);

				attributeString = string.Concat("Authorization: ", enumAut.ToString());
			}
			catch (Exception ex) {
				attributeString = string.Concat("Error al consultar la autorizacion al item.", ":\n\r", ex.Message, "\n\r", ex.StackTrace);
			}

			txtbOutput.Text += string.Concat(attributeString, Environment.NewLine, Environment.NewLine);
		}

		private void TestUI_Load(object sender, EventArgs e) {

		}

		private void button1_Click_1(object sender, EventArgs e) {
			if (_user == null) {
				MessageBox.Show(this, "Primero debe de obtener un DBUser.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			PwdSvcTestUI _form = new PwdSvcTestUI(_user);
			_form.ShowDialog();
		}

		private void button4_Click(object sender, EventArgs e) {
			DirectSvcRef.DirectServiceClient _wsc = new DirectSvcRef.DirectServiceClient();

			string attributeString;
			try {
				string[] _vals = txtbUseName2.Text.Split(new char[] { '\\' });
				if (!_wsc.GetUser(out _azManUser, out _statusType, out _status, out _stackTrace, _vals[0], _vals[1])) {
					txtbOutput.Text += string.Concat(_status, Environment.NewLine, _stackTrace);
				}
				else {
					System.Diagnostics.Debugger.Break();
				}
				//attributeString = string.Concat("UserID: ", _user.UserID.ToString(), Environment.NewLine, "User: ", _user.UserName, Environment.NewLine, "AttributeString: ", _azManUser.AttributeString);
			}
			catch (Exception ex) {
				txtbOutput.Text = string.Concat("Error a obtener informacion del usuario.", ":\n\r", ex.Message, "\n\r", ex.StackTrace);
			}

			//txtbOutput.Text += string.Concat(attributeString, Environment.NewLine, Environment.NewLine);
		}
	}
}