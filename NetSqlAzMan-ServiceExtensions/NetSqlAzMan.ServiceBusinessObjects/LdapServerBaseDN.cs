using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class LdapServerBaseDN {
		[Key]
		[Required]
		public short BaseDNId { get; set; }

		[Required]
		public LdapDomainProfile DomainProfile { get; set; }

		[Required]
		public byte BaseDNOrder { get; set; }

		[Required]
		public string BaseDN { get; set; }

		[Timestamp]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "RowVersion", GroupName = "mainFrame", Name = "RowVersion", Order = 4, Prompt = "Ingrese RowVersion", ShortName = "RowVersion")]
		public byte[] RowVersion { get; set; } // timestamp, not null
	}
}
