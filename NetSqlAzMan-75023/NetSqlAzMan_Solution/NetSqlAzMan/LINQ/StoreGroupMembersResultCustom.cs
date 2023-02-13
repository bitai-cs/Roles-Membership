using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.LINQ
{
	public partial class StoreGroupMembersResult
	{
		private System.String _LDAPDomain;

		[global::System.Data.Linq.Mapping.ColumnAttribute(Name = "LDAPDomain", Storage = "_LDAPDomain", DbType = "VarChar(50)", CanBeNull = true)]
		public System.String LDAPDomain {
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
