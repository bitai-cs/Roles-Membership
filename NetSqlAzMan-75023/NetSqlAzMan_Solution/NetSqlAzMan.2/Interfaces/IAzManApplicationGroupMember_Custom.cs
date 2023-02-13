using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
    public partial interface IAzManApplicationGroupMember
    {
        string DomainProfile { get; }

        string samAccountName { get; }

        string cn { get; }

        string displayName { get; }

        string objectSidString { get; }

        string distinguishedName { get; }

        string objectClass { get; }
    }
}
