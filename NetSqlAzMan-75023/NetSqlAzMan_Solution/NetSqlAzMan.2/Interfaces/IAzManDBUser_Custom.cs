using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NetSqlAzMan.Interfaces
{
	public partial interface IAzManDBUser
	{
		[DataMember]
		[Display(Name = "Servidor LDAP")]
		string DomainProfile {
			get;
		}
		[DataMember]
		[Display(Name = "Es Entrada Ldap")]
		bool IsLdapEntry {
			get;
		}
		[DataMember]
		[Display(Name = "Nombre del Usuario")]
		string DisplayName {
			get;
		}
	}
}
