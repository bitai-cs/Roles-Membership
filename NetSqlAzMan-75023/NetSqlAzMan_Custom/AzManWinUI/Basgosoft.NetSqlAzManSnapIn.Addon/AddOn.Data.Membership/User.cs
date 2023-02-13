using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NetSqlAzManSnapIn.AddOn.Membership.Data
{
	public class User : NetSqlAzManSnapIn.AddOn.Data.Base
	{
		#region Constructors

		public User(string connectionString)
			: base(connectionString) {
			if (this.ptexceHandled != null)
				throw new Exception("Error al inicializar el proxy de datos.");
		}

		#endregion

		#region Query members

		public bool GetUsers(ref DataSet dataSet, string tableName, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;

			hex = null;

			try {
				striCommandText = "dbo.identity_sp_GetUser_1";
				cmdtCommandType = CommandType.StoredProcedure;

				base.pthlprSql.FillDataset(base.ptstriConnectionString, cmdtCommandType, striCommandText, dataSet, new string[] { tableName });

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool GetUser(int userId, ref DataSet dataSet, string tableName, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			hex = null;

			try {
				striCommandText = "dbo.identity_sp_GetUser_2";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[1];

				sqlpParameters[0] = new SqlParameter("@userId", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Input;
				sqlpParameters[0].Value = userId;

				base.pthlprSql.FillDataset(base.ptstriConnectionString, cmdtCommandType, striCommandText, dataSet, new string[] { tableName }, sqlpParameters);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Insert & update members

		public bool InsertUser(out Nullable<Int32> userId, string userName, string password, string passwordHash, string firstName, string lastName, string mail, Int32 departmentId, bool enabled, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			userId = null;
			hex = null;

			try {
				striCommandText = "dbo.identity_sp_InsertUser_1";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[9];

				sqlpParameters[0] = new SqlParameter("@userId", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Output;

				sqlpParameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
				sqlpParameters[1].Direction = ParameterDirection.Input;
				sqlpParameters[1].Size = 255;
				sqlpParameters[1].Value = userName;

				sqlpParameters[2] = new SqlParameter("@password", SqlDbType.NVarChar);
				sqlpParameters[2].Direction = ParameterDirection.Input;
				sqlpParameters[2].Size = 50;
				sqlpParameters[2].Value = password;

				sqlpParameters[3] = new SqlParameter("@firstName", SqlDbType.NVarChar);
				sqlpParameters[3].Direction = ParameterDirection.Input;
				sqlpParameters[3].Size = 150;
				sqlpParameters[3].Value = firstName;

				sqlpParameters[4] = new SqlParameter("@lastName", SqlDbType.NVarChar);
				sqlpParameters[4].Direction = ParameterDirection.Input;
				sqlpParameters[4].Size = 150;
				sqlpParameters[4].Value = lastName;

				sqlpParameters[5] = new SqlParameter("@mail", SqlDbType.NVarChar);
				sqlpParameters[5].Direction = ParameterDirection.Input;
				sqlpParameters[5].Size = 255;
				sqlpParameters[5].Value = mail;

				sqlpParameters[6] = new SqlParameter("@departmentId", SqlDbType.Int);
				sqlpParameters[6].Direction = ParameterDirection.Input;
				sqlpParameters[6].Value = departmentId;

				sqlpParameters[7] = new SqlParameter("@enabled", SqlDbType.Bit);
				sqlpParameters[7].Direction = ParameterDirection.Input;
				sqlpParameters[7].Value = enabled;

				sqlpParameters[8] = new SqlParameter("@passwordHash", SqlDbType.NVarChar);
				sqlpParameters[8].Direction = ParameterDirection.Input;
				sqlpParameters[8].Size = 2048;
				sqlpParameters[8].Value = passwordHash;

				base.pthlprSql.ExecuteNonQuery(base.ptstriConnectionString, cmdtCommandType, striCommandText, sqlpParameters);

				userId = (Int32)sqlpParameters[0].Value;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool UpdateUser(int userId, string userName, string password, string passwordHash, string firstName, string lastName, string mail, Int32 departmentId, bool enabled, byte[] recordVersion, out bool updated, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			updated = false;
			hex = null;

			try {
				striCommandText = "dbo.identity_sp_UpdateUser_1";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[11];

				sqlpParameters[0] = new SqlParameter("@userID", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Input;
				sqlpParameters[0].Value = userId;

				sqlpParameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
				sqlpParameters[1].Direction = ParameterDirection.Input;
				sqlpParameters[1].Size = 255;
				sqlpParameters[1].Value = userName;

				sqlpParameters[2] = new SqlParameter("@password", SqlDbType.NVarChar);
				sqlpParameters[2].Direction = ParameterDirection.Input;
				sqlpParameters[2].Size = 50;
				sqlpParameters[2].Value = password;

				sqlpParameters[3] = new SqlParameter("@firstName", SqlDbType.NVarChar);
				sqlpParameters[3].Direction = ParameterDirection.Input;
				sqlpParameters[3].Size = 150;
				sqlpParameters[3].Value = firstName;

				sqlpParameters[4] = new SqlParameter("@lastName", SqlDbType.NVarChar);
				sqlpParameters[4].Direction = ParameterDirection.Input;
				sqlpParameters[4].Size = 150;
				sqlpParameters[4].Value = lastName;

				sqlpParameters[5] = new SqlParameter("@mail", SqlDbType.NVarChar);
				sqlpParameters[5].Direction = ParameterDirection.Input;
				sqlpParameters[5].Size = 255;
				sqlpParameters[5].Value = mail;

				sqlpParameters[6] = new SqlParameter("@departmentId", SqlDbType.Int);
				sqlpParameters[6].Direction = ParameterDirection.Input;
				sqlpParameters[6].Value = departmentId;

				sqlpParameters[7] = new SqlParameter("@enabled", SqlDbType.Bit);
				sqlpParameters[7].Direction = ParameterDirection.Input;
				sqlpParameters[7].Value = enabled;

				sqlpParameters[8] = new SqlParameter("@recordVersion", SqlDbType.Timestamp);
				sqlpParameters[8].Direction = ParameterDirection.Input;
				sqlpParameters[8].Value = recordVersion;

				sqlpParameters[9] = new SqlParameter("@Updated", SqlDbType.Bit);
				sqlpParameters[9].Direction = ParameterDirection.Output;

				sqlpParameters[10] = new SqlParameter("@passwordHash", SqlDbType.NVarChar);
				sqlpParameters[10].Direction = ParameterDirection.Input;
				sqlpParameters[10].Size = 2048;
				sqlpParameters[10].Value = passwordHash;

				base.pthlprSql.ExecuteNonQuery(base.ptstriConnectionString, cmdtCommandType, striCommandText, sqlpParameters);

				updated = (bool)sqlpParameters[9].Value;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Delete members

		public bool DeleteUser(Int32 UserId, out Boolean Deleted, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;
			SqlParameter[] sqlpParameters;

			Deleted = false;
			hex = null;

			try {
				striCommandText = "dbo.identity_sp_DeleteUser$1";
				cmdtCommandType = CommandType.StoredProcedure;
				sqlpParameters = new SqlParameter[2];

				sqlpParameters[0] = new SqlParameter("@UserId", SqlDbType.Int);
				sqlpParameters[0].Direction = ParameterDirection.Input;
				sqlpParameters[0].Value = UserId;

				sqlpParameters[1] = new SqlParameter("@Deleted", SqlDbType.Bit);
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