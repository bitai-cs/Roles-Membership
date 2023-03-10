using System;
using System.Collections.Generic;
using System.ServiceModel;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.Cache.Service
{
	[ServiceContract]
	public interface ICacheService
	{
		[OperationContract(Name = "CheckAccessForWindowsUsersWithAttributesRetrieve")]
		AuthorizationType CheckAccess(string storeName, string applicationName, string itemName, string userSSid, string[] groupsSSid, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters);
		[OperationContract(Name = "CheckAccessForWindowsUsersWithoutAttributesRetrieve")]
		AuthorizationType CheckAccess(string storeName, string applicationName, string itemName, string userSSid, string[] groupsSSid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);
		[OperationContract(Name = "CheckAccessForDatabaseUsersWithAttributesRetrieve")]
		AuthorizationType CheckAccess(string storeName, string applicationName, string itemName, string DBuserSSid, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters);
		[OperationContract(Name = "CheckAccessForDatabaseUsersWithoutAttributesRetrieve")]
		AuthorizationType CheckAccess
			(string storeName, string applicationName, string itemName, string DBuserSSid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);
		[OperationContract(Name = "InvalidateCache")]
		void InvalidateCache();
		[OperationContract(Name = "InvalidateCacheOnServicePartners")]
		void InvalidateCache(bool invalidateCacheOnServicePartners);
		//[OperationContract(Name = "InvalidateStoreCache")]
		void InvalidateStoreCache(string storeName);
		//[OperationContract(Name = "InvalidateStoreApplicationCache")]
		void InvalidateStoreApplicationCache(string storeName, string applicationName);
		[OperationContract()]
		string[] GetItemNames(string storeName, string applicationName, ItemType type);
		[OperationContract()]
		KeyValuePair<string, ItemType>[] GetAllItems(string storeName, string applicationName);
		[OperationContract(Name = "GetAuthorizedItemsForDatabaseUsers")]
		AuthorizedItem[] GetAuthorizedItems(string storeName, string applicationName, string DBuserSSid, DateTime validFor, params KeyValuePair<string, object>[] contextParameters);
		[OperationContract(Name = "GetAuthorizedItemsForWindowsUsers")]
		AuthorizedItem[] GetAuthorizedItems(string storeName, string applicationName, string userSSid, string[] groupsSSid, DateTime validFor, params KeyValuePair<string, object>[] contextParameters);
	}
}