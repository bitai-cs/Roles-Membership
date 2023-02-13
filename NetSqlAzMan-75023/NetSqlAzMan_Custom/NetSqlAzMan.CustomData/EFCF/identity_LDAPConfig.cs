using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NetSqlAzMan.CustomDataLayer.EFCF {

	public partial class identity_LDAPConfig {
		[Key]
		[StringLength(50)]
		public string ldap_domain { get; set; }

		[Required]
		[StringLength(50)]
		[Index(IsUnique = true)]
		public string ldap_description { get; set; }

		[StringLength(50)]
		public string ldap_client_endpoint { get; set; }

		public bool ldap_enabled { get; set; }

		[Column(TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
