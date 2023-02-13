using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetSqlAzMan.Extensions
{
	public static class LdapEntryExtensions
	{
		public static bool GetIsGroupByObjectClass(this LdapHelperDTO.LdapEntry e) {
			return e.objectClass.Where(i => i.Equals("group", StringComparison.OrdinalIgnoreCase)).Any();
		}
	}
}