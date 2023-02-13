using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetSqlAzMan.LINQ
{
	public partial class NetSqlAzManStorageDataContext
	{
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_AuthorizationInsertCustom")]
		[return: global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")]
		public int AuthorizationInsertCustom([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ItemId", DbType = "Int")] System.Nullable<int> itemId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary ownerSid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "TinyInt")] System.Nullable<byte> ownerSidWhereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary objectSid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "TinyInt")] System.Nullable<byte> objectSidWhereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AuthorizationType", DbType = "TinyInt")] System.Nullable<byte> authorizationType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ValidFrom", DbType = "DateTime")] System.Nullable<System.DateTime> validFrom, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ValidTo", DbType = "DateTime")] System.Nullable<System.DateTime> validTo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationId", DbType = "Int")] System.Nullable<int> applicationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LDAPDomain", DbType = "VarChar(50)")] string ldapDomain) {
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), itemId, ownerSid, ownerSidWhereDefined, objectSid, objectSidWhereDefined, authorizationType, validFrom, validTo, applicationId, ldapDomain);
			return ((int)(result.ReturnValue));
		}

		[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.netsqlazman_ApplicationGroupMemberInsertCustom")]
		[return: global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")]
		public int ApplicationGroupMemberInsertCustom([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationGroupId", DbType = "Int")] System.Nullable<int> applicationGroupId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarBinary(85)")] System.Data.Linq.Binary objectSid, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WhereDefined", DbType = "TinyInt")] System.Nullable<byte> whereDefined, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsMember", DbType = "Bit")] System.Nullable<bool> isMember, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApplicationId", DbType = "Int")] System.Nullable<int> applicationId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LDAPDomain", DbType = "VarChar(50)")] string ldapDomain) {
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), applicationGroupId, objectSid, whereDefined, isMember, applicationId, ldapDomain);
			return ((int)(result.ReturnValue));
		}
	}
}