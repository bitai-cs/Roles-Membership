using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManStorage
	{
		//IAzManDBUser GetAzManUser(IAzManSid customSid, bool dummy);

		//bool GetAzManDBUser(string userName, out IAzManDBUser user, out Exception hex);

		//IAzManDBUser[] GetAzManUsers(bool dummy);

		///// <summary>
		///// VBastidas: Finds the DB User and Password
		///// </summary>
		///// <param name="userName"></param>
		///// <returns></returns>
		//[Obsolete("You must use GetDBUser method!")]
		//IAzManDBUser GetDBUserWithPassword(string userName);

		///// <summary>
		///// 
		///// </summary>
		///// <param name="userName"></param>
		///// <param name="password"></param>
		///// <param name="azManUser"></param>
		///// <param name="statusType"></param>
		///// <param name="status"></param>
		///// <param name="stackTrace"></param>
		///// <returns></returns>
		//bool GetDBUserByValidatingPassword(string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace);

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
		IAzManDBUser GetLDAPUserByValidatingPassword(string domainProfile, bool useGC, string userName, string password);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="azManUser"></param>
		/// <param name="hex"></param>
		/// <returns></returns>
		//bool GetLDAPUser(string ldapDomain, string userName, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace);
		bool GetLDAPUser(string ldapDomain, string userName, out IAzManDBUser azManUser, out Exception hex);

		AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);

		AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters);

		AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string sid, byte[] binarySid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters);

		AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string sid, byte[] binarySid, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters);

		//bool ChangeDBUserPassword(string userName, string currentPassword, string renewedPassword, string passwordConfirmation, out Exception hex);

		///// <summary>
		///// 
		///// </summary>
		///// <param name="user"></param>
		///// <param name="currentPassword"></param>
		///// <param name="renewedPassword"></param>
		///// <param name="passwordConfirmation"></param>
		///// <param name="statusType"></param>
		///// <param name="statusMessage"></param>
		///// <param name="statusTrace"></param>
		///// <returns></returns>
		//bool ChangeDBUserPassword(NetSqlAzMan.Cache.DBUser user, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace);

		///// <summary>
		///// Change DB User password by its id
		///// </summary>
		///// <param name="userId"></param>
		///// <param name="currentPassword"></param>
		///// <param name="renewedPassword"></param>
		///// <param name="passwordConfirmation"></param>
		///// <param name="statusType"></param>
		///// <param name="statusMessage"></param>
		///// <param name="statusTrace"></param>
		///// <returns></returns>
		//bool ChangeDBUserPasswordById(int userId, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace);
	}
}
