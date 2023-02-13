using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManItem
	{
		/// <summary>
		/// Creates the authorization.
		/// </summary>
		/// <param name="owner">The owner.</param>
		/// <param name="ownerSidWhereDefined">The owner sid where defined.</param>
		/// <param name="sid">The object owner.</param>
		/// <param name="sidWhereDefined">The object owner where defined.</param>
		/// <param name="authorizationType">Type of the authorization.</param>
		/// <param name="validFrom">The valid from.</param>
		/// <param name="validTo">The valid to.</param>
		/// <param name="domainProfile">ID de la configuraci{on ldap que se usopara generar al auth..</param>
		/// <returns></returns>
		[OperationContract]
		IAzManAuthorization CreateAuthorizationCustom(IAzManSid owner, WhereDefined ownerSidWhereDefined, IAzManSid sid, WhereDefined sidWhereDefined, AuthorizationType authorizationType, Nullable<DateTime> validFrom, Nullable<DateTime> validTo, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass);
	}
}
