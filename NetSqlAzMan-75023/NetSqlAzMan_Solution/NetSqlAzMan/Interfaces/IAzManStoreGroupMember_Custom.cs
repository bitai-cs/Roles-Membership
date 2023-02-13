using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManStoreGroupMember
	{
		string LDAPDomain {
			get;
		}
	}
}
