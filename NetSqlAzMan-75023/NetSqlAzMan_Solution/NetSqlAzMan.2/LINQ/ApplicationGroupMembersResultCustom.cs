using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.LINQ
{
    public partial class ApplicationGroupMembersResult
    {
        private string _domainProfile;

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "DomainProfile", Storage = "_domainProfile", DbType = "VarChar(50)", CanBeNull = true)]
        public string DomainProfile {
            get {
                return this._domainProfile;
            }
            set {
                if ((this._domainProfile != value)) {
                    this._domainProfile = value;
                }
            }
        }

        private System.String _samAccountName;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "samAccountName", Storage = "_samAccountName", DbType = "VarChar(255)", CanBeNull = true)]
        public System.String samAccountName {
            get {
                return this._samAccountName;
            }
            set {
                if ((this._samAccountName != value)) {
                    this._samAccountName = value;
                }
            }
        }

        private System.String _cn;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "cn", Storage = "_cn", DbType = "VarChar(255)", CanBeNull = true)]
        public System.String cn {
            get {
                return this._cn;
            }
            set {
                if ((this._cn != value)) {
                    this._cn = value;
                }
            }
        }

        private System.String _displayName;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "displayName", Storage = "_displayName", DbType = "VarChar(255)", CanBeNull = true)]
        public System.String displayName {
            get {
                return this._displayName;
            }
            set {
                if ((this._displayName != value)) {
                    this._displayName = value;
                }
            }
        }

        private System.String _objectSidString;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "objectSidString", Storage = "_objectSidString", DbType = "VarChar(100)", CanBeNull = true)]
        public System.String objectSidString {
            get {
                return this._objectSidString;
            }
            set {
                if ((this._objectSidString != value)) {
                    this._objectSidString = value;
                }
            }
        }

        private System.String _distinguishedName;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "distinguishedName", Storage = "_distinguishedName", DbType = "VarChar(2000)", CanBeNull = true)]
        public System.String distinguishedName {
            get {
                return this._distinguishedName;
            }
            set {
                if ((this._distinguishedName != value)) {
                    this._distinguishedName = value;
                }
            }
        }

        private System.String _objectClass;
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "objectClass", Storage = "_objectClass", DbType = "VarChar(10)", CanBeNull = true)]
        public System.String objectClass {
            get {
                return this._objectClass;
            }
            set {
                if ((this._objectClass != value)) {
                    this._objectClass = value;
                }
            }
        }
    }
}
