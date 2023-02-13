using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.ENS;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LINQ;

namespace NetSqlAzMan
{
	public partial class SqlAzManApplicationGroupMember
	{
		private string ldapDomain;	
		public string LDAPDomain {
			get {
				return ldapDomain;
			}
		}

		internal SqlAzManApplicationGroupMember(NetSqlAzManStorageDataContext db, IAzManApplicationGroup applicationGroup, int applicationGroupMemberId, IAzManSid sid, WhereDefined whereDefined, bool isMember, string ldapDomain, SqlAzManENS ens)
			: this(db, applicationGroup, applicationGroupMemberId, sid, whereDefined, isMember, ens) {
			this.ldapDomain = ldapDomain;
		}
	}
}
