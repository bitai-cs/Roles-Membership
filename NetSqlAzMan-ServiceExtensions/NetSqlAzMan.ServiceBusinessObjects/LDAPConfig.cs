using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public partial class LDAPConfig {
		public LDAPConfig() {
			ldap_domain = Guid.NewGuid().ToString();
		}

		[Key]
		[MaxLength(50)]
		[StringLength(50)]
		[Required]
		[Display(Name = "Domain Profile")]
		public virtual string ldap_domain { get; set; } // nvarchar(50), not null
		[MaxLength(50)]
		[StringLength(50)]
		[Required]
		[Display(Name = "Description")]
		public virtual string ldap_description { get; set; } // nvarchar(50), not null
		[MaxLength(50)]
		[StringLength(50)]
		[Display(Name = "Client Endpoint")]
		public virtual string ldap_client_endpoint { get; set; } // nvarchar(50), null
		[Required]
		[Display(Name = "Enabled")]
		public virtual bool ldap_enabled { get; set; } // bit, not null
		[Timestamp]
		[Required]
		[Display(Name = "Row Version")]
		public virtual byte[] RowVersion { get; set; } // timestamp, not null
	}
}
