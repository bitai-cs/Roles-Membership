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
        public string DomainProfile { get; private set; }

        public string samAccountName { get; private set; }

        public string cn { get; private set; }

        public string displayName { get; private set; }

        public string objectSidString { get; private set; }

        public string distinguishedName { get; private set; }

        public string objectClass { get; private set; }

        internal SqlAzManAuthorization(NetSqlAzManStorageDataContext db, IAzManItem item, int authorizationId, IAzManSid owner, WhereDefined ownerSidWhereDefined, IAzManSid sid, WhereDefined objectSidWhereDefined, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo, SqlAzManENS ens)
            : this(db, item, authorizationId, owner, ownerSidWhereDefined, sid, objectSidWhereDefined, authorizationType, validFrom, validTo, ens) {
            this.DomainProfile = domainProfile;
            this.samAccountName = samAccountName;
            this.cn = cn;
            this.displayName = displayName;
            this.objectSidString = objectSidString;
            this.distinguishedName = distinguishedName;
            this.objectClass = objectClass;
        }
    }
}
