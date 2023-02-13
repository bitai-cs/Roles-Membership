using System;
using System.Windows.Forms;
using System.Data.SqlTypes;
using AzManWebServicesSqlClrClient;
using AzManWebServicesClient.DirectSvcRef;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Basgosoft.NetSqlAzManCacheWSSqlClrClientTest
{
	/// <summary>
	/// Test UI
	/// </summary>
	public partial class TestUI : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestUI() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			try {
				if (!chkbTestSqlClrAssembly.Checked) {
					SqlInt32 si32UserId;
					SqlString sstrAttributeString;

					DirectSvcStoredProcedures.AzMan_DirectGetDBUser(txtbUserName.Text, out si32UserId, out sstrAttributeString);

					if (!si32UserId.Value.Equals(-1)) {
						txtbOutput.Text += "UserID: " + si32UserId.ToString() + Environment.NewLine;
						txtbOutput.Text += "AttributeString: " + sstrAttributeString.Value + Environment.NewLine;
						txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;

						SqlString sstrPropertyValue;
						DirectSvcStoredProcedures.AzMan_Util_GetPropertyValueFromAttString(sstrAttributeString, new SqlString("RelativeBranchOfficeIds"), out sstrPropertyValue);
						MessageBox.Show(sstrPropertyValue.Value);

						DirectSvcStoredProcedures.AzMan_Util_GetPropertyValueFromAttString(sstrAttributeString, new SqlString("BranchOfficeNames"), out sstrPropertyValue);
						MessageBox.Show(sstrPropertyValue.Value);
					}
					else {
						txtbOutput.Text += "AttributeString: " + sstrAttributeString.Value + Environment.NewLine;
						txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
					}
				}
				else {
					int si32UserId;
					string sstrAttributeString;

					Database _db = DatabaseFactory.CreateDatabase();
					DbConnection _con = null;
					DbCommand _cmd = null;

					_con = _db.CreateConnection();
					_con.Open();

					_cmd = _db.GetStoredProcCommand("dbo.AzMan_DirectGetDBUser");

					DbParameter _param = _cmd.CreateParameter();
					_param.ParameterName = "@userName";
					_param.DbType = System.Data.DbType.String;
					_param.Size = 255;
					_param.Direction = System.Data.ParameterDirection.Input;
					_param.Value = txtbUserName.Text;
					_cmd.Parameters.Add(_param);

					_param = _cmd.CreateParameter();
					_param.ParameterName = "@userID";
					_param.DbType = System.Data.DbType.Int32;
					_param.Direction = System.Data.ParameterDirection.Output;
					_cmd.Parameters.Add(_param);

					_param = _cmd.CreateParameter();
					_param.ParameterName = "@attributeString";
					_param.DbType = System.Data.DbType.String;
					_param.Size = int.MaxValue;
					_param.Direction = System.Data.ParameterDirection.Output;
					_cmd.Parameters.Add(_param);

					_db.ExecuteNonQuery(_cmd);

					_con.Close();

					si32UserId = (int)_cmd.Parameters["@userID"].Value;
					sstrAttributeString = (string)_cmd.Parameters["@attributeString"].Value;

					if (!si32UserId.Equals(-1)) {
						txtbOutput.Text += "UserID: " + si32UserId + Environment.NewLine;
						txtbOutput.Text += "AttributeString: " + sstrAttributeString + Environment.NewLine;
						txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;

						SqlString sstrPropertyValue;
						DirectSvcStoredProcedures.AzMan_Util_GetPropertyValueFromAttString(sstrAttributeString, new SqlString("RelativeBranchOfficeIds"), out sstrPropertyValue);
						MessageBox.Show(sstrPropertyValue.Value);

						DirectSvcStoredProcedures.AzMan_Util_GetPropertyValueFromAttString(sstrAttributeString, new SqlString("BranchOfficeNames"), out sstrPropertyValue);
						MessageBox.Show(sstrPropertyValue.Value);
					}
					else {
						txtbOutput.Text += "AttributeString: " + sstrAttributeString + Environment.NewLine;
						txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
					}
				}
			}
			catch (Exception ex) {
				txtbOutput.Text += "ERROR: " + Environment.NewLine;
				txtbOutput.Text += "Source: " + ex.Source + Environment.NewLine;
				txtbOutput.Text += "Message: " + ex.Message + Environment.NewLine;
				txtbOutput.Text += "StackTrace: " + ex.StackTrace + Environment.NewLine;
				txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			SqlBoolean sbooValid;
			SqlString sstrAttString;

			try {
				DirectSvcStoredProcedures.AzMan_DirectValidatePassword(txtbUserName.Text, txtbPassword.Text, out sbooValid, out sstrAttString);

				txtbOutput.Text += "isValid: " + sbooValid.Value.ToString() + Environment.NewLine;

				if (!sstrAttString.IsNull)
					txtbOutput.Text += "attributeString: " + sstrAttString.Value + Environment.NewLine;

				txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
			}
			catch (Exception ex) {
				txtbOutput.Text += "ERROR: " + Environment.NewLine;
				txtbOutput.Text += "Source: " + ex.Source + Environment.NewLine;
				txtbOutput.Text += "Message: " + ex.Message + Environment.NewLine;
				txtbOutput.Text += "StackTrace: " + ex.StackTrace + Environment.NewLine;
				txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			SqlInt32 si32Aut;
			SqlString sstrAttributeString;

			try {
				DirectSvcStoredProcedures.AzMan_DirectCheckAccess("SISE", "TRAMITES_ALUMNADO", txtbItemName.Text, txtbUserName.Text, DateTime.Now, false, out si32Aut, out sstrAttributeString);

				if (!si32Aut.Value.Equals(-1)) {
					txtbOutput.Text += "Authorization: " + ((AuthorizationType)si32Aut.Value).ToString() + Environment.NewLine;
					txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
				}
				else {
					txtbOutput.Text += "AttributeString: " + sstrAttributeString.Value + Environment.NewLine;
					txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
				}
			}
			catch (Exception ex) {
				txtbOutput.Text += "ERROR: " + Environment.NewLine;
				txtbOutput.Text += "Source: " + ex.Source + Environment.NewLine;
				txtbOutput.Text += "Message: " + ex.Message + Environment.NewLine;
				txtbOutput.Text += "StackTrace: " + ex.StackTrace + Environment.NewLine;
				txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
			}
		}

		private void button4_Click(object sender, EventArgs e) {
			if (chkbTestSqlClrAssembly.Checked) {
				Database _db = DatabaseFactory.CreateDatabase();
				DbConnection _con = null;
				DbCommand _cmd = null;

				_con = _db.CreateConnection();
				_con.Open();

				_cmd = _db.GetStoredProcCommand("dbo.AzMan_Test");

				DbParameter _param = null;
				_param = _cmd.CreateParameter();
				_param.ParameterName = "@input";
				_param.DbType = System.Data.DbType.String;
				_param.Size = 255;
				_param.Direction = System.Data.ParameterDirection.Input;
				_param.Value = "Mensaje de prueba!";
				_cmd.Parameters.Add(_param);

				_param = _cmd.CreateParameter();
				_param.ParameterName = "@result";
				_param.DbType = System.Data.DbType.Boolean;
				_param.Direction = System.Data.ParameterDirection.Output;
				_cmd.Parameters.Add(_param);

				_param = _cmd.CreateParameter();
				_param.ParameterName = "@output";
				_param.DbType = System.Data.DbType.String;
				_param.Size = int.MaxValue;
				_param.Direction = System.Data.ParameterDirection.Output;
				_cmd.Parameters.Add(_param);

				_db.ExecuteNonQuery(_cmd);

				_con.Close();

				string _output;
				bool _result;
				string _message;
				_result = (bool)_cmd.Parameters["@result"].Value;
				_output = (string)_cmd.Parameters["@output"].Value;

				if (_result) {
					txtbOutput.Text += "result: " + _result.ToString() + Environment.NewLine;
					txtbOutput.Text += "output: " + _output + Environment.NewLine;
					txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
				}
				else {
					txtbOutput.Text += "result: " + _result.ToString() + Environment.NewLine;
					txtbOutput.Text += "message: " + _output + Environment.NewLine;
					txtbOutput.Text += "________________________________________________________________________" + Environment.NewLine + Environment.NewLine;
				}
			}
			else {
				MessageBox.Show("No implementado.");
			}
		}
	}
}