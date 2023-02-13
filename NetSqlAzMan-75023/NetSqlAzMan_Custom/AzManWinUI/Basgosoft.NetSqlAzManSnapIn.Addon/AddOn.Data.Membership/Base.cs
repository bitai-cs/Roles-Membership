using System;
using System.Collections.Generic;
using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Data
{
	public abstract class Base : IDisposable
	{
		#region Protected fields

		protected Int32 ptin32ConnectionTimeOut = 20;
		protected Int32 ptin32CommandTimeOut = 60;

		protected string ptstriConnectionString;

		protected Exception ptexceHandled;

		#endregion

		#region Private fields

		protected Basgosoft.Data.Sql.AdvancedHelper pthlprSql;

		#endregion

		#region Constructors

		internal Base(string connectionString) {
			ptstriConnectionString = connectionString;

			pthlprSql = new Basgosoft.Data.Sql.AdvancedHelper(ptin32ConnectionTimeOut, ptin32CommandTimeOut);
		}

		#endregion

		#region IDisposable Members

		public void Dispose() {
			pthlprSql = null;
		}

		#endregion
	}
}