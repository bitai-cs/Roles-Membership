using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AzManLoginWebServicesSqlClrClientTest
{
	public partial class TestUI : Form
	{
		public TestUI() {
			InitializeComponent();
		}

		private void butnAzManLogin_Test_Click(object sender, EventArgs e) {
			SqlString _input = "Texto de prueba del servicio.";
			SqlString _output = null;
			SqlBoolean _result = false;
			SqlString _message = null;

			if (!chkbTestSqlClrAssembly.Checked) {
				try {
					AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures.AzManLogin_Test(_input, out _output, out _result, out _message);

					MessageBox.Show(string.Format("output: {0}\n\rresult: {1}\n\rmessage: {2}", _output.IsNull ? "NULL" : _output.Value, _result.IsNull ? "NULL" : _result.Value.ToString(), _message.IsNull ? "NULL" : _message.Value));
				}
				catch (Exception ex) {
					MessageBox.Show(string.Format("{0}\n\r{1}", ex.Message, ex.StackTrace));
				}
			}
			else {
				try {
					Database _db = DatabaseFactory.CreateDatabase();
					DbConnection _con = null;
					DbCommand _cmd = null;

					_con = _db.CreateConnection();
					_con.Open();

					_cmd = _db.GetStoredProcCommand("dbo.AzManLogin_Test");

					DbParameter _param = null;
					_param = _cmd.CreateParameter();
					_param.ParameterName = "@input";
					_param.DbType = System.Data.DbType.String;
					_param.Size = 255;
					_param.Direction = System.Data.ParameterDirection.Input;
					_param.Value = "Mensaje de prueba!";
					_cmd.Parameters.Add(_param);

					_param = _cmd.CreateParameter();
					_param.ParameterName = "@output";
					_param.DbType = System.Data.DbType.String;
					_param.Size = int.MaxValue;
					_param.Direction = System.Data.ParameterDirection.Output;
					_cmd.Parameters.Add(_param);

					_param = _cmd.CreateParameter();
					_param.ParameterName = "@result";
					_param.DbType = System.Data.DbType.Boolean;
					_param.Direction = System.Data.ParameterDirection.Output;
					_cmd.Parameters.Add(_param);

					_param = _cmd.CreateParameter();
					_param.ParameterName = "@message";
					_param.DbType = System.Data.DbType.String;
					_param.Size = int.MaxValue;
					_param.Direction = System.Data.ParameterDirection.Output;
					_cmd.Parameters.Add(_param);

					_db.ExecuteNonQuery(_cmd);

					_con.Close();

					_output = DBNull.Value.Equals(_cmd.Parameters["@output"].Value) ? null : _cmd.Parameters["@output"].Value.ToString();
					_result = Convert.ToBoolean(_cmd.Parameters["@result"].Value);
					_message = DBNull.Value.Equals(_cmd.Parameters["@message"].Value) ? null : _cmd.Parameters["@message"].Value.ToString();

					MessageBox.Show(string.Format("output: {0}\n\rresult: {1}\n\rmessage: {2}", _output.IsNull ? "NULL" : _output.Value, _result.IsNull ? "NULL" : _result.Value.ToString(), _message.IsNull ? "NULL" : _message.Value));
				}
				catch (Exception ex) {
					MessageBox.Show(string.Format("{0}\n\r{1}", ex.Message, ex.StackTrace));
				}
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			SqlInt32 _loginStatus;
			SqlInt32 _aut;
			SqlBoolean _result;
			SqlString _message;
		AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures.AzManLogin_CheckLoginAccess("SISE", "CAMPUS_ADMINISTRATIVO", "Acta_RegistrarNotas", "45d2d5f4-19a0-411b-9486-db9b9c04f9ea",
				"admcampus", out _loginStatus, out _aut, out _result, out _message);

			System.Diagnostics.Debugger.Break();
		}
	}
}
