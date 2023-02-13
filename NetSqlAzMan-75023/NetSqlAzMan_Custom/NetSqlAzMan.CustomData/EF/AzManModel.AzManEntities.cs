using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.CustomDataLayer.EF {
	public partial class AzManEntities {
		//Constructor to set custom ConnectiionString
		public AzManEntities(string connectionString)
			: base(connectionString) {
#if DEBUG
			this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
		}

		public AzManEntities(DbConnection connection) : base(connection, false) {
#if DEBUG
			this.Database.Log = s => System.Diagnostics.Debug.WriteLine(connection.ConnectionString);
#endif
		}
	}
}
