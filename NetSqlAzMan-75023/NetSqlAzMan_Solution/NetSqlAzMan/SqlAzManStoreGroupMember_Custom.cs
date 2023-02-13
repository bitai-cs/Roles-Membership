using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.ENS;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LINQ;

namespace NetSqlAzMan
{
	public sealed partial class SqlAzManStoreGroupMember
	{
		private string ldapDomain;
		public string LDAPDomain {
			get {
				return ldapDomain;
			}
		}

		internal SqlAzManStoreGroupMember(NetSqlAzManStorageDataContext db, IAzManStoreGroup storeGroup, int storeGroupMemberId, IAzManSid sid, WhereDefined whereDefined, bool isMember, SqlAzManENS ens, string ldapDomain)
			: this(db, storeGroup, storeGroupMemberId, sid, whereDefined, isMember, ens) {
			this.ldapDomain = ldapDomain;
		}
	}
}
