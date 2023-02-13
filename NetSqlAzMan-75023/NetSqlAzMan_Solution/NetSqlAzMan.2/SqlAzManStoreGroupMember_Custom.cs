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
        private string _domainProfile;
        public string DomainProfile {
            get {
                return _domainProfile;
            }
        }

        private string _samAccountName;
        public string samAccountName {
            get {
                return this._samAccountName;
            }
            set {
                if ((this._samAccountName != value)) {
                    this._samAccountName = value;
                }
            }
        }

        private string _cn;
        public string cn {
            get {
                return this._cn;
            }
            set {
                if ((this._cn != value)) {
                    this._cn = value;
                }
            }
        }

        private string _displayName;
        public string displayName {
            get {
                return this._displayName;
            }
            set {
                if ((this._displayName != value)) {
                    this._displayName = value;
                }
            }
        }

        private string _objectSidString;
        public string objectSidString {
            get {
                return this._objectSidString;
            }
            set {
                if ((this._objectSidString != value)) {
                    this._objectSidString = value;
                }
            }
        }

        private string _distinguishedName;
        public string distinguishedName {
            get {
                return this._distinguishedName;
            }
            set {
                if ((this._distinguishedName != value)) {
                    this._distinguishedName = value;
                }
            }
        }

        private string _objectClass;
        public string objectClass {
            get {
                return this._objectClass;
            }
            set {
                if ((this._objectClass != value)) {
                    this._objectClass = value;
                }
            }
        }

        internal SqlAzManStoreGroupMember(NetSqlAzManStorageDataContext db, IAzManStoreGroup storeGroup, int storeGroupMemberId, IAzManSid sid, WhereDefined whereDefined, bool isMember, SqlAzManENS ens, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass)
            : this(db, storeGroup, storeGroupMemberId, sid, whereDefined, isMember, ens) {
            this._domainProfile = domainProfile;
            this._samAccountName = samAccountName;
            this._cn = cn;
            this._displayName = displayName;
            this._objectSidString = objectSidString;
            this._distinguishedName = distinguishedName;
            this._objectClass = objectClass;
        }
    }
}
