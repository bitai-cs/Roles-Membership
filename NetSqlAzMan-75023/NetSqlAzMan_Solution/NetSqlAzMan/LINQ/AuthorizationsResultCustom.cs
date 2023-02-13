using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.LINQ
{
	public partial class AuthorizationsResult
	{
		private string _LDAPDomain;

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LDAPDomain", DbType = "VarChar(50)", CanBeNull = true)]
		public string LDAPDomain {
			get {
				return this._LDAPDomain;
			}
			set {
				if ((this._LDAPDomain != value)) {
					this._LDAPDomain = value;
				}
			}
		}
	}
}
