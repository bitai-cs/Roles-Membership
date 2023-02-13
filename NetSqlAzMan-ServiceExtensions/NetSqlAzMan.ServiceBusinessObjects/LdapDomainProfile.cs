using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
	public partial class LdapDomainProfile
	{
		public LdapDomainProfile() {
			DomainProfileId = 0;
			DomainProfile = "New";
		}

		[Key]
		[Required]
		[Display(Name = "Domain Profile Id")]
		public virtual byte DomainProfileId { get; set; }

		[MaxLength(50)]
		[StringLength(50)]
		[Required]
		[Display(Name = "Domain Profile")]
		public virtual string DomainProfile { get; set; } // nvarchar(50), not null

		[MaxLength(1000)]
		[StringLength(1000)]
		[Required]
		[Display(Name = "Web API Uri")]
		public virtual string LdapProxyWebApiUri { get; set; } // nvarchar(50), not null

		[MaxLength(50)]
		[StringLength(50)]
		[Required]
		[Display(Name = "Ldap Server Profile")]
		public virtual string LdapServerProfile { get; set; } // nvarchar(50), null

		[MaxLength(50)]
		[StringLength(50)]
		[Required]
		[Display(Name = "Domain Name")]
		public virtual string DomainName { get; set; } // nvarchar(50), not null

		[Required]
		[Display(Name = "Habilitado")]
		public virtual bool Enabled { get; set; } // bit, not null		
	}
}
