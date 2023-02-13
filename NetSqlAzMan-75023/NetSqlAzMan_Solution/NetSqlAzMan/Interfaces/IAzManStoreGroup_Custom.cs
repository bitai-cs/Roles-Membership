using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManStoreGroup 
	{
		[OperationContract]
		IAzManStoreGroupMember CreateStoreGroupMemberCustom(IAzManSid sid, WhereDefined whereDefined, bool isMember, string ldapDomain);
	}
}
