using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NetSqlAzMan.LINQ
{
	/// <summary>
	/// 
	/// </summary>
	public partial class NetSqlAzManStorageDataContext
	{
		private static Dictionary<KeyValuePair<string, string>, int?> dbUsersCheckSum = new Dictionary<KeyValuePair<string, string>, int?>();

		///// <summary>
		///// Gets the DB users ex.
		///// </summary>
		///// <param name="storeName">Name of the store.</param>
		///// <param name="applicationName">Name of the application.</param>
		///// <param name="dBUserSid">The d B user sid.</param>
		///// <param name="dBUserName">Name of the d B user.</param>
		///// <returns></returns>
		//public DataTable GetDBUsersEx(string storeName, string applicationName, byte[] dBUserSid, string dBUserName) {
		//	DataTable result = new DataTable("DBUsers");
		//	using (var da = new SqlDataAdapter("select * from dbo.netsqlazman_GetDBUsers(@StoreName, @ApplicationName, @DBUserSID, @DBUserName)", (SqlConnection)this.Connection)) {
		//		SqlParameter pStoreName = new SqlParameter("@StoreName", SqlDbType.NVarChar, 255);
		//		SqlParameter pApplicationName = new SqlParameter("@ApplicationName", SqlDbType.NVarChar, 255);
		//		SqlParameter pDBUserSID = new SqlParameter("@DBUserSID", SqlDbType.VarBinary, 85);
		//		SqlParameter pDBUserName = new SqlParameter("@DBUserName", SqlDbType.NVarChar, 255);
		//		pStoreName.Value = !String.IsNullOrEmpty(storeName) ? (object)storeName : DBNull.Value;
		//		pApplicationName.Value = !String.IsNullOrEmpty(applicationName)
		//											  ? (object)applicationName
		//											  : DBNull.Value;
		//		pDBUserSID.Value = dBUserSid != null ? (object)dBUserSid : DBNull.Value;
		//		pDBUserName.Value = !String.IsNullOrEmpty(dBUserName) ? (object)dBUserName : DBNull.Value;
		//		da.SelectCommand.Parameters.Add(pStoreName);
		//		da.SelectCommand.Parameters.Add(pApplicationName);
		//		da.SelectCommand.Parameters.Add(pDBUserSID);
		//		da.SelectCommand.Parameters.Add(pDBUserName);
		//		da.SelectCommand.Transaction = this.Transaction as SqlTransaction;
		//		da.Fill(result);
		//	}
		//	return result;
		//}

		///// <summary>
		///// VBastidas: Finds the DB user.Get the DB Users with Passwords
		///// </summary>
		///// <param name="storeName">Name of the store.</param>
		///// <param name="applicationName">Name of the application.</param>
		///// <param name="dBUserSid">The d B user sid.</param>
		///// <param name="dBUserName">Name of the d B user.</param>
		///// <returns></returns>
		//[Obsolete("Ya no se usa en NetSqlAzMan.2")]
		//public DataTable GetDBUsersWithPasswordsEx(string storeName, string applicationName, byte[] dBUserSid, string dBUserName) {
		//	DataTable result = new DataTable("DBUsers");
		//	using (var da = new SqlDataAdapter("SELECT   f.*, CONVERT(NVARCHAR(50), u.Password) AS PasswordString from dbo.netsqlazman_GetDBUsers(@StoreName, @ApplicationName, @DBUserSID, @DBUserName) as f INNER JOIN dbo.[identity_User] AS u ON f.UserID = u.UserID", (SqlConnection)this.Connection)) {
		//		SqlParameter pStoreName = new SqlParameter("@StoreName", SqlDbType.NVarChar, 255);
		//		SqlParameter pApplicationName = new SqlParameter("@ApplicationName", SqlDbType.NVarChar, 255);
		//		SqlParameter pDBUserSID = new SqlParameter("@DBUserSID", SqlDbType.VarBinary, 85);
		//		SqlParameter pDBUserName = new SqlParameter("@DBUserName", SqlDbType.NVarChar, 255);
		//		pStoreName.Value = !String.IsNullOrEmpty(storeName) ? (object)storeName : DBNull.Value;
		//		pApplicationName.Value = !String.IsNullOrEmpty(applicationName)
		//											  ? (object)applicationName
		//											  : DBNull.Value;
		//		pDBUserSID.Value = dBUserSid != null ? (object)dBUserSid : DBNull.Value;
		//		pDBUserName.Value = !String.IsNullOrEmpty(dBUserName) ? (object)dBUserName : DBNull.Value;
		//		da.SelectCommand.Parameters.Add(pStoreName);
		//		da.SelectCommand.Parameters.Add(pApplicationName);
		//		da.SelectCommand.Parameters.Add(pDBUserSID);
		//		da.SelectCommand.Parameters.Add(pDBUserName);
		//		da.SelectCommand.Transaction = this.Transaction as SqlTransaction;
		//		da.Fill(result);
		//	}
		//	return result;
		//}

		partial void OnCreated() {
			this.ObjectTrackingEnabled = true;
		}
	}
}