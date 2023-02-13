using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class ldapwac_ServerBaseDN
	{
		[Key]
		public short BaseDNId { get; set; }

		public byte DomainProfileId { get; set; }

		public byte BaseDNOrder { get; set; }

		[Required]
		[StringLength(255)]
		public string BaseDN { get; set; }

		[Column(TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual ldapwac_DomainProfile ldapwac_DomainProfile { get; set; }
	}
}
