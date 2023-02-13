using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.ENS;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LINQ;

namespace NetSqlAzMan
{
	public partial class SqlAzManAuthorization
	{
		private string ldapDomain;
		public string LDAPDomain {
			get {
				return this.ldapDomain;
			}
		}

		internal SqlAzManAuthorization(NetSqlAzManStorageDataContext db, IAzManItem item, int authorizationId, IAzManSid owner, WhereDefined ownerSidWhereDefined, IAzManSid sid, WhereDefined objectSidWhereDefined, string ldapDomain, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo, SqlAzManENS ens)
			: this(db, item, authorizationId, owner, ownerSidWhereDefined, sid, objectSidWhereDefined, authorizationType, validFrom, validTo, ens) {
			this.ldapDomain = ldapDomain;
		}
	}
}
