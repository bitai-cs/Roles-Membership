using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NetSqlAzManSnapIn.AddOn.Membership.Data
{
	public class Department : NetSqlAzManSnapIn.AddOn.Data.Base
	{
		#region Constructors

		public Department(string connectionString)
			: base(connectionString) {
			if (this.ptexceHandled != null)
				throw new Exception("Error al inicializar el proxy de datos.");
		}

		#endregion

		#region Query members

		public bool GetDepartments(ref DataSet dataSet, string tableName, out Exception hex) {
			string striCommandText;
			CommandType cmdtCommandType;

			hex = null;

			try {
				striCommandText = "dbo.identity_sp_GetDepartment_1";
				cmdtCommandType = CommandType.StoredProcedure;

				base.pthlprSql.FillDataset(base.ptstriConnectionString, cmdtCommandType, striCommandText, dataSet, new string[] { tableName });

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