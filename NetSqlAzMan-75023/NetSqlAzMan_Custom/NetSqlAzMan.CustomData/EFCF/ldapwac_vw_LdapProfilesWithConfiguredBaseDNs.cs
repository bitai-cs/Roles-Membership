using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EFCF {
	public partial class ldapwac_vw_DomainProfilesWithConfiguredBaseDNs {
		[Key]
		[Column(Order = 0)]
		[StringLength(50)]
		public string DomainProfile { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(1000)]
		public string LdapProxyWebApiUri { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(50)]
		public string LdapServerProfile { get; set; }

		[Key]
		[Column(Order = 3)]
		public bool Enabled { get; set; }

		public short? BaseDNId { get; set; }

		public byte? BaseDNOrder { get; set; }

		[StringLength(255)]
		public string BaseDN { get; set; }

		[Column(TypeName = "timestamp")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[MaxLength(8)]
		public byte[] RowVersion { get; set; }
	}
}
