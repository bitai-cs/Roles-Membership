using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace AzManLoginWebServices.BusinessLayer.DataLayer
{
	/// <summary>
	/// Data access layer class for Login
	/// </summary>
	class LoginSql : DataLayerBase
	{
		#region Constructor

		/// <summary>
		/// Class constructor
		/// </summary>
		public LoginSql() {
			// Nothing for now.
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// insert new row in the table
		/// </summary>
		/// <param name="businessObject">business object</param>
		/// <returns>true of successfully insert</returns>
		public bool Insert(Login businessObject) {
			DbConnection con = null;
			DbCommand cmd = null;
			string striCmd = "[Basgosoft].[Login_pa_Insert]";

			try {
				con = Db.CreateConnection();

				cmd = Db.GetStoredProcCommand(striCmd);
				cmd.Connection = con;

				Db.DiscoverParameters(cmd);

				cmd.Parameters["@LoginId"].Value = businessObject.LoginId;
				cmd.Parameters["@AppName"].Value = businessObject.AppName;
				cmd.Parameters["@LDAPDomain"].Value = businessObject.LDAPDomain;
				cmd.Parameters["@UserName"].Value = businessObject.UserName;
				cmd.Parameters["@RemoteIpV4"].Value = businessObject.RemoteIpV4;
				cmd.Parameters["@LogIn"].Value = businessObject.LogIn;
				cmd.Parameters["@Expires"].Value = businessObject.Expires;
				cmd.Parameters["@Timeout"].Value = businessObject.Timeout;
				cmd.Parameters["@Expiration"].Value = businessObject.Expiration;
				cmd.Parameters["@Expired"].Value = businessObject.Expired;
				cmd.Parameters["@LogOff"].Value = businessObject.LogOff;
				cmd.Parameters["@LoggedOut"].Value = businessObject.LoggedOut;
				cmd.Parameters["@Renovated"].Value = businessObject.Renovated;
				cmd.Parameters["@Renewal"].Value = businessObject.Renewal;

				con.Open();

				cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				con.Close();
				cmd.Dispose();
			}
		}

		/// <summary>
		/// update row in the table
		/// </summary>
		/// <param name="businessObject">business object</param>
		/// <returns>true for successfully updated</returns>
		public bool Update(Login businessObject) {
			DbConnection con = null;
			DbCommand cmd = null;
			string striCmd = "[Basgosoft].[Login_pa_Update]";

			try {
				con = Db.CreateConnection();

				cmd = Db.GetStoredProcCommand(striCmd);
				cmd.Connection = con;

				Db.DiscoverParameters(cmd);

				cmd.Parameters["@LoginId"].Value = businessObject.LoginId;
				cmd.Parameters["@AppName"].Value = businessObject.AppName;
				cmd.Parameters["@UserName"].Value = businessObject.UserName;
				cmd.Parameters["@RemoteIpV4"].Value = businessObject.RemoteIpV4;
				cmd.Parameters["@LogIn"].Value = businessObject.LogIn;
				cmd.Parameters["@Expires"].Value = businessObject.Expires;
				cmd.Parameters["@Timeout"].Value = businessObject.Timeout;
				cmd.Parameters["@Expiration"].Value = businessObject.Expiration;
				cmd.Parameters["@Expired"].Value = businessObject.Expired;
				cmd.Parameters["@LogOff"].Value = businessObject.LogOff;
				cmd.Parameters["@LoggedOut"].Value = businessObject.LoggedOut;
				cmd.Parameters["@Renovated"].Value = businessObject.Renovated;
				cmd.Parameters["@Renewal"].Value = businessObject.Renewal;

				con.Open();

				cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				con.Close();
				cmd.Dispose();
			}
		}

		/// <summary>
		/// Select by primary key
		/// </summary>
		/// <param name="keys">primary keys</param>
		/// <returns>Login business object</returns>
		public Login SelectByPrimaryKey(LoginKeys keys) {
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "[Basgosoft].[Login_pa_SelectByPrimaryKey]";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			// Use connection object of base class
			sqlCommand.Connection = MainConnection;

			try {

				sqlCommand.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, keys.LoginId));


				MainConnection.Open();

				IDataReader dataReader = sqlCommand.ExecuteReader();

				if (dataReader.Read()) {
					Login businessObject = new Login();

					PopulateBusinessObjectFromReader(businessObject, dataReader);

					return businessObject;
				}
				else {
					return null;
				}
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				MainConnection.Close();
				sqlCommand.Dispose();
			}

		}

		/// <summary>
		/// Select records by field
		/// </summary>
		/// <param name="fieldName">name of field</param>
		/// <param name="value">value of field</param>
		/// <returns>list of Login</returns>
		public List<Login> SelectByField(string fieldName, object value) {

			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "[Basgosoft].[Login_pa_SelectByField]";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			// Use connection object of base class
			sqlCommand.Connection = MainConnection;

			try {

				sqlCommand.Parameters.Add(new SqlParameter("@FieldName", fieldName));
				sqlCommand.Parameters.Add(new SqlParameter("@Value", value));

				MainConnection.Open();

				IDataReader dataReader = sqlCommand.ExecuteReader();

				return PopulateObjectsFromReader(dataReader);

			}
			catch (Exception ex) {
				throw ex;
			}
			finally {

				MainConnection.Close();
				sqlCommand.Dispose();
			}

		}

		/// <summary>
		/// Delete by primary key
		/// </summary>
		/// <param name="keys">primary keys</param>
		/// <returns>true for successfully deleted</returns>
		public bool Delete(LoginKeys keys) {
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "[Basgosoft].[Login_pa_DeleteByPrimaryKey]";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			// Use connection object of base class
			sqlCommand.Connection = MainConnection;

			try {

				sqlCommand.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, keys.LoginId));


				MainConnection.Open();

				sqlCommand.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				MainConnection.Close();
				sqlCommand.Dispose();
			}

		}

		/// <summary>
		/// Delete records by field
		/// </summary>
		/// <param name="fieldName">name of field</param>
		/// <param name="value">value of field</param>
		/// <returns>true for successfully deleted</returns>
		public bool DeleteByField(string fieldName, object value) {
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText = "[Basgosoft].[Login_pa_DeleteByField]";
			sqlCommand.CommandType = CommandType.StoredProcedure;

			// Use connection object of base class
			sqlCommand.Connection = MainConnection;

			try {

				sqlCommand.Parameters.Add(new SqlParameter("@FieldName", fieldName));
				sqlCommand.Parameters.Add(new SqlParameter("@Value", value));

				MainConnection.Open();

				sqlCommand.ExecuteNonQuery();

				return true;

			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				MainConnection.Close();
				sqlCommand.Dispose();
			}

		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Populate business object from data reader
		/// </summary>
		/// <param name="businessObject">business object</param>
		/// <param name="dataReader">data reader</param>
		internal void PopulateBusinessObjectFromReader(Login businessObject, IDataReader dataReader) {
			object _val;

			businessObject.LoginId = dataReader.GetString(dataReader.GetOrdinal(Login.LoginFields.LoginId.ToString()));

			businessObject.AppName = dataReader.GetString(dataReader.GetOrdinal(Login.LoginFields.AppName.ToString()));

			businessObject.UserName = dataReader.GetString(dataReader.GetOrdinal(Login.LoginFields.UserName.ToString()));

			businessObject.RemoteIpV4 = dataReader.GetString(dataReader.GetOrdinal(Login.LoginFields.RemoteIpV4.ToString()));

			businessObject.LogIn = dataReader.GetDateTime(dataReader.GetOrdinal(Login.LoginFields.LogIn.ToString()));

			businessObject.Expires = dataReader.GetDateTime(dataReader.GetOrdinal(Login.LoginFields.Expires.ToString()));

			businessObject.Timeout = dataReader.GetInt32(dataReader.GetOrdinal(Login.LoginFields.Timeout.ToString()));

			_val = dataReader.GetValue(dataReader.GetOrdinal(Login.LoginFields.Expiration.ToString()));
			businessObject.Expiration = DBNull.Value.Equals(_val) ? new Nullable<DateTime>() : Convert.ToDateTime(_val);

			businessObject.Expired = dataReader.GetBoolean(dataReader.GetOrdinal(Login.LoginFields.Expired.ToString()));

			_val = dataReader.GetValue(dataReader.GetOrdinal(Login.LoginFields.LogOff.ToString()));
			businessObject.LogOff = DBNull.Value.Equals(_val) ? new Nullable<DateTime>() : Convert.ToDateTime(_val);

			businessObject.LoggedOut = dataReader.GetBoolean(dataReader.GetOrdinal(Login.LoginFields.LoggedOut.ToString()));

			businessObject.Renovated = dataReader.GetBoolean(dataReader.GetOrdinal(Login.LoginFields.Renovated.ToString()));

			_val = dataReader.GetValue(dataReader.GetOrdinal(Login.LoginFields.Renewal.ToString()));
			businessObject.Renewal = DBNull.Value.Equals(_val) ? new Nullable<DateTime>() : Convert.ToDateTime(_val);
		}

		/// <summary>
		/// Populate business objects from the data reader
		/// </summary>
		/// <param name="dataReader">data reader</param>
		/// <returns>list of Login</returns>
		internal List<Login> PopulateObjectsFromReader(IDataReader dataReader) {

			List<Login> list = new List<Login>();

			while (dataReader.Read()) {
				Login businessObject = new Login();
				PopulateBusinessObjectFromReader(businessObject, dataReader);
				list.Add(businessObject);
			}
			return list;

		}

		#endregion
	}
}