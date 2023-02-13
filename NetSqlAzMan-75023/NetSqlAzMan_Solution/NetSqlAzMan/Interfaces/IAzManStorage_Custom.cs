using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManStorage
	{
		IAzManDBUser GetAzManUser(IAzManSid customSid, bool dummy);
		
		bool GetAzManDBUser(string userName, out IAzManDBUser user, out Exception hex);
		
		IAzManDBUser[] GetAzManUsers(bool dummy);
		
		/// <summary>
		/// VBastidas: Finds the DB User and Password
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		IAzManDBUser GetDBUserWithPassword(string userName);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="azManUser"></param>
		/// <param name="statusType"></param>
		/// <param name="status"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		bool GetDBUserByValidatingPassword(string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace);
		
		/// <summary>
		/// Get DS user by aunthenticaring. 
		/// </summary>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="azManUser"></param>
		/// <param name="exceptionType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		bool GetLDAPUserByValidatingPassword(string ldapDomain, string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace);
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lDAPDomain"></param>
		/// <param name="userName"></param>
		/// <param name="azManUser"></param>
		/// <param name="statusType"></param>
		/// <param name="status"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		bool GetLDAPUser(string ldapDomain, string userName, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="StoreName"></param>
		/// <param name="ApplicationName"></param>
		/// <param name="ItemName"></param>
		/// <param name="LDAPDomain"></param>
		/// <param name="User"></param>
		/// <param name="Sid"></param>
		/// <param name="ValidFor"></param>
		/// <param name="OperationsOnly"></param>
		/// <param name="contextParameters"></param>
		/// <returns></returns>
		AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string ldapDomain, string userName, string sid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="StoreName"></param>
		/// <param name="ApplicationName"></param>
		/// <param name="ItemName"></param>
		/// <param name="LDAPDomain"></param>
		/// <param name="User"></param>
		/// <param name="Sid"></param>
		/// <param name="ValidFor"></param>
		/// <param name="OperationsOnly"></param>
		/// <param name="attributes"></param>
		/// <param name="contextParameters"></param>
		/// <returns></returns>
		AuthorizationType CheckAccessLDAP(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="currentPassword"></param>
		/// <param name="renewedPassword"></param>
		/// <param name="passwordConfirmation"></param>
		/// <param name="statusType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="statusTrace"></param>
		/// <returns></returns>
		bool ChangeDBUserPassword(NetSqlAzMan.Cache.DBUser user, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace);

		/// <summary>
		/// Change DB User password by its id
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="currentPassword"></param>
		/// <param name="renewedPassword"></param>
		/// <param name="passwordConfirmation"></param>
		/// <param name="statusType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="statusTrace"></param>
		/// <returns></returns>
		bool ChangeDBUserPasswordById(int userId, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace);
	}
}
