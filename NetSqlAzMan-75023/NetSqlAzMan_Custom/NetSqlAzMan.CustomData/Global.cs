using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace NetSqlAzMan.CustomDataLayer
{
	public static class Global
	{
		public static string AzManConnectionString = null;

		//internal static readonly log4net.ILog Loger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		internal const string Config_ConnStrings_AzManEntities = "AzManEntities";
		internal const string Config_ConnStrings_AzManLinqToClasses = "AzManLinqToClasses";

		internal static string GetAzManEntitiesConnectionString() {
			if (string.IsNullOrEmpty(Global.AzManConnectionString))
				throw new Exception("No se ha establecido la cadena de conexión global.");

			string _cns = ConfigurationManager.ConnectionStrings[Global.Config_ConnStrings_AzManEntities].ConnectionString;
			_cns = string.Format(_cns, Global.AzManConnectionString);

			return _cns;
		}

		internal static string GetAzManEntitiesConnectionStringCF() {
			if (string.IsNullOrEmpty(Global.AzManConnectionString))
				throw new Exception("No se ha establecido la cadena de conexión global.");

			return Global.AzManConnectionString;
		}

		internal static NetSqlAzMan.CustomDataLayer.EF.AzManEntities GetAzManEntities() {
			return new NetSqlAzMan.CustomDataLayer.EF.AzManEntities(GetAzManEntitiesConnectionString());
		}

		internal static NetSqlAzMan.CustomDataLayer.EF.AzManEntities GetAzManEntities(DbConnection connection) {
			return new NetSqlAzMan.CustomDataLayer.EF.AzManEntities(connection);
		}

		internal static NetSqlAzMan.CustomDataLayer.EFCF.AzManEntities GetAzManEntitiesCF() {
			return new NetSqlAzMan.CustomDataLayer.EFCF.AzManEntities(GetAzManEntitiesConnectionStringCF());
		}

		internal static NetSqlAzMan.CustomDataLayer.EFCF.AzManEntities GetAzManEntitiesCF(DbConnection connection) {
			return new NetSqlAzMan.CustomDataLayer.EFCF.AzManEntities(connection);
		}

		internal static NetSqlAzMan.CustomDataLayer.LINQ.DBUsersModelDataContext GetDBUserModel() {
			string _cns = ConfigurationManager.ConnectionStrings[Global.Config_ConnStrings_AzManLinqToClasses].ConnectionString;
			_cns = string.Format(_cns, Global.AzManConnectionString);

			return new NetSqlAzMan.CustomDataLayer.LINQ.DBUsersModelDataContext(_cns);
		}

		internal static string ByteArrayToString(byte[] ba) {
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return "0x" + hex.ToString();
		}

		internal static ConnectionManager CreateConnectionManager(bool beginTransaction) {
			var _cm = new ConnectionManager();
			if (beginTransaction)
				_cm.BeginTransaction();

			return _cm;
		}
	}
}