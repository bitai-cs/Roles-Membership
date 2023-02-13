using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NetSqlAzManSnapIn.AddOn.Membership.Data
{
	public class UserBranchOffice : NetSqlAzManSnapIn.AddOn.Data.Base
	{
		#region Constructors

		public UserBranchOffice(string connectionString)
			: base(connectionString) {
			if (this.ptexceHandled != null)
				throw new Exception("Error al inicializar el proxy de datos.");
		}

		#endregion

		#region Query members

		#endregion

		#region Insert members

		public bool InsertUserBranchOffice(int userId, string branchOfficeId, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			hex = null;

			try {
				striCommandText = "dbo.identity_sp_InsertUserBranchOffice";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[2];

				sqlpParameters[0] = new SqlParameter("@userId", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Input;
				sqlpParameters[0].Value = userId;

				sqlpParameters[1] = new SqlParameter("@branchOfficeId", SqlDbType.VarChar);
				sqlpParameters[1].Size = 2;
				sqlpParameters[1].Direction = ParameterDirection.Input;
				sqlpParameters[1].Value = branchOfficeId;

				base.pthlprSql.ExecuteNonQuery(base.ptstriConnectionString, cmdtCommandType, striCommandText, sqlpParameters);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Delete members

		public bool DeleteUserBranchOffice(Int32 userId, out Boolean Deleted, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			Deleted = false;
			hex = null;

			try {
				striCommandText = "dbo.identity_sp_DeleteUserBranchOffice_1";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[2];

				sqlpParameters[0] = new SqlParameter("@userId", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Input;
				sqlpParameters[0].Value = userId;

				sqlpParameters[1] = new SqlParameter("@deleted", SqlDbType.Bit);
				sqlpParameters[1].Direction = ParameterDirection.Output;

				base.pthlprSql.ExecuteNonQuery(base.ptstriConnectionString, cmdtCommandType, striCommandText, sqlpParameters);

				Deleted = (Boolean)sqlpParameters[1].Value;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion
	}
}