using System;
using System.Collections.Generic;
using System.ServiceModel;
using NetSqlAzMan;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;

namespace AzManWebServices
{
	[ServiceContract]
	public interface IDirectService
	{
		[OperationContract()]
		bool Test(string input, out string output);

		[OperationContract()]
		DBUser DirectGetDBUser(string userName);

		[OperationContract()]
		bool GetUser(string ldapDomain, string userName, out SqlAzManDBUser azManUser, out string statusType, out string status, out string stackTrace);

		[OperationContract()]
		bool DirectValidatePassword(string userName, string password);

		[OperationContract(Name = "DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve")]
		AuthorizationType DirectCheckAccess(string store, string app, string item, string DBuserSSid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);

		[OperationContract]
		bool ChangePassword(DBUser user, string current, string renewed, string confirmation, out string statusMessage);

		[OperationContract]
		bool ChangePasswordEx(DBUser user, string current, string renewed, string confirmation, out string statusType, out string statusMessage, out string statusTrace);

		[OperationContract]
		bool ValidatePassword(string domain, string userName, string password, out SqlAzManDBUser azManUser, out string statusType, out string status, out string stackTrace);

		[OperationContract]
		bool CheckAccessLDAP(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out AuthorizationType authorization, out string statusType, out string status, out string stackTrace, params KeyValuePair<string, object>[] contextParameters);

		[OperationContract]
		bool CheckAccessLDAPEx(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out List<KeyValuePair<string, string>> attributes, out AuthorizationType authorization, out string statusType, out string status, out string stackTrace, params KeyValuePair<string, object>[] contextParameters);
	}
}
