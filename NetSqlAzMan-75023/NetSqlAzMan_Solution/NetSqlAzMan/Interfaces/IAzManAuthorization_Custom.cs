using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManAuthorization
	{
		string LDAPDomain {
			get;
		}
	}
}
