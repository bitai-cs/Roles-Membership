using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetSqlAzMan.LINQ
{
    public partial class GetDBUsersResult
    {
        private System.Data.Linq.Binary _DBUserSid;

        private string _DBUserName;

        private string _Password;

        private int _UserID;

        private string _FirstName;

        private string _LastName;

        private string _FullName;

        private string _Mail;

        private string _BranchOfficeIds;

        private string _RelativeBranchOfficeIds;

        private string _BranchOfficeNames;

        private int _DepartmentId;

        private string _DepartmentName;

        private bool _Enabled;

        public GetDBUsersResult() {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DBUserSid", DbType = "VarBinary(85)", UpdateCheck = UpdateCheck.Never)]
        public System.Data.Linq.Binary DBUserSid {
            get {
                return this._DBUserSid;
            }
            set {
                if ((this._DBUserSid != value)) {
                    this._DBUserSid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DBUserName", DbType = "NVarChar(255) NOT NULL", CanBeNull = false)]
        public string DBUserName {
            get {
                return this._DBUserName;
            }
            set {
                if ((this._DBUserName != value)) {
                    this._DBUserName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "NVarChar(2048)")]
        public string Password {
            get {
                return this._Password;
            }
            set {
                if ((this._Password != value)) {
                    this._Password = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserID", DbType = "Int NOT NULL")]
        public int UserID {
            get {
                return this._UserID;
            }
            set {
                if ((this._UserID != value)) {
                    this._UserID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstName", DbType = "NVarChar(150) NOT NULL", CanBeNull = false)]
        public string FirstName {
            get {
                return this._FirstName;
            }
            set {
                if ((this._FirstName != value)) {
                    this._FirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastName", DbType = "NVarChar(150) NOT NULL", CanBeNull = false)]
        public string LastName {
            get {
                return this._LastName;
            }
            set {
                if ((this._LastName != value)) {
                    this._LastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FullName", DbType = "NVarChar(301) NOT NULL", CanBeNull = false)]
        public string FullName {
            get {
                return this._FullName;
            }
            set {
                if ((this._FullName != value)) {
                    this._FullName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Mail", DbType = "NVarChar(255)")]
        public string Mail {
            get {
                return this._Mail;
            }
            set {
                if ((this._Mail != value)) {
                    this._Mail = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BranchOfficeIds", DbType = "NVarChar(MAX)")]
        public string BranchOfficeIds {
            get {
                return this._BranchOfficeIds;
            }
            set {
                if ((this._BranchOfficeIds != value)) {
                    this._BranchOfficeIds = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RelativeBranchOfficeIds", DbType = "NVarChar(MAX)")]
        public string RelativeBranchOfficeIds {
            get {
                return this._RelativeBranchOfficeIds;
            }
            set {
                if ((this._RelativeBranchOfficeIds != value)) {
                    this._RelativeBranchOfficeIds = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BranchOfficeNames", DbType = "NVarChar(MAX)")]
        public string BranchOfficeNames {
            get {
                return this._BranchOfficeNames;
            }
            set {
                if ((this._BranchOfficeNames != value)) {
                    this._BranchOfficeNames = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DepartmentId", DbType = "Int NOT NULL")]
        public int DepartmentId {
            get {
                return this._DepartmentId;
            }
            set {
                if ((this._DepartmentId != value)) {
                    this._DepartmentId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DepartmentName", DbType = "NVarChar(150) NOT NULL", CanBeNull = false)]
        public string DepartmentName {
            get {
                return this._DepartmentName;
            }
            set {
                if ((this._DepartmentName != value)) {
                    this._DepartmentName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Enabled", DbType = "Bit NOT NULL")]
        public bool Enabled {
            get {
                return this._Enabled;
            }
            set {
                if ((this._Enabled != value)) {
                    this._Enabled = value;
                }
            }
        }
    }

    public partial class NetSqlAzManStorageDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_AuthorizationInsertCustom")]
        [return: global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")]
        public int AuthorizationInsertCustom([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ItemId", DbType = "Int")] System.Nullable<int> itemId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary ownerSid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "TinyInt")] System.Nullable<byte> ownerSidWhereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary objectSid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "TinyInt")] System.Nullable<byte> objectSidWhereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AuthorizationType", DbType = "TinyInt")] System.Nullable<byte> authorizationType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ValidFrom", DbType = "DateTime")] System.Nullable<System.DateTime> validFrom, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ValidTo", DbType = "DateTime")] System.Nullable<System.DateTime> validTo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationId", DbType = "Int")] System.Nullable<int> applicationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DomainProfile", DbType = "VarChar(50)")] string DomainProfile, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "samAccountName", DbType = "VarChar(255)")] string samAccountName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "cn", DbType = "VarChar(255)")] string cn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "displayName", DbType = "VarChar(255)")] string displayName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectSidString", DbType = "VarChar(100)")] string objectSidString, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "distinguishedName", DbType = "VarChar(2000)")] string distinguishedName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectClass", DbType = "VarChar(10)")] string objectClass) {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), itemId, ownerSid, ownerSidWhereDefined, objectSid, objectSidWhereDefined, authorizationType, validFrom, validTo, applicationId, DomainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            return ((int)(result.ReturnValue));
        }



        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_ApplicationGroupMemberInsertCustom")]
        [return: global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")]
        public int ApplicationGroupMemberInsertCustom([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationGroupId", DbType = "Int")] System.Nullable<int> applicationGroupId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary objectSid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WhereDefined", DbType = "TinyInt")] System.Nullable<byte> whereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsMember", DbType = "Bit")] System.Nullable<bool> isMember, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationId", DbType = "Int")] System.Nullable<int> applicationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DomainProfile", DbType = "VarChar(50)")] string DomainProfile, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "samAccountName", DbType = "VarChar(255)")] string samAccountName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "cn", DbType = "VarChar(255)")] string cn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "displayName", DbType = "VarChar(255)")] string displayName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectSidString", DbType = "VarChar(100)")] string objectSidString, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "distinguishedName", DbType = "VarChar(2000)")] string distinguishedName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectClass", DbType = "VarChar(10)")] string objectClass) {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), applicationGroupId, objectSid, whereDefined, isMember, applicationId, DomainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            return ((int)(result.ReturnValue));
        }



        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_StoreGroupMemberInsertCustom")]
        [return: global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")]
        public int StoreGroupMemberInsertCustom([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StoreId", DbType = "Int")] System.Nullable<int> storeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StoreGroupId", DbType = "Int")] System.Nullable<int> storeGroupId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary objectSid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WhereDefined", DbType = "TinyInt")] System.Nullable<byte> whereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsMember", DbType = "Bit")] System.Nullable<bool> isMember, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DomainProfile", DbType = "VarChar(50)")] string DomainProfile, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "samAccountName", DbType = "VarChar(255)")] string samAccountName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "cn", DbType = "VarChar(255)")] string cn, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "displayName", DbType = "VarChar(255)")] string displayName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectSidString", DbType = "VarChar(100)")] string objectSidString, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "distinguishedName", DbType = "VarChar(2000)")] string distinguishedName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "objectClass", DbType = "VarChar(10)")] string objectClass) {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), storeId, storeGroupId, objectSid, whereDefined, isMember, DomainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            return ((int)(result.ReturnValue));
        }



        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_GetDBUsers", IsComposable = true)]
        public IQueryable<GetDBUsersResult> GetDBUsers([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StoreName", DbType = "NVarChar(255)")] string storeName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationName", DbType = "NVarChar(255)")] string applicationName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DBUserSid", DbType = "VarBinary(85)")] System.Data.Linq.Binary dBUserSid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DBUserName", DbType = "NVarChar(255)")] string dBUserName) {
            return this.CreateMethodCallQuery<GetDBUsersResult>(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), storeName, applicationName, dBUserSid, dBUserName);
        }
    }
}