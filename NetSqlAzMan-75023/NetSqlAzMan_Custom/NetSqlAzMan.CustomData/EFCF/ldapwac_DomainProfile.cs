using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class ldapwac_DomainProfile
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ldapwac_DomainProfile() {
			ldapwac_ServerBaseDN = new HashSet<ldapwac_ServerBaseDN>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte DomainProfileId { get; set; }

		[Required]
		[StringLength(50)]
		public string DomainProfile { get; set; }

		[Required]
		[StringLength(1000)]
		public string LdapProxyWebApiUri { get; set; }

		[Required]
		[StringLength(50)]
		public string LdapServerProfile { get; set; }

		[Required]
		[StringLength(50)]
		public string DomainName { get; set; }

		[Required]
		public bool Enabled { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<ldapwac_ServerBaseDN> ldapwac_ServerBaseDN { get; set; }
	}
}
