using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
	public class AzManCredential
	{
		public string DomainProfile;
		[Required(AllowEmptyStrings = false)]
		[MinLength(3)]
		public string UserName;
		[Required(AllowEmptyStrings = false)]
		[MinLength(3)]
		public string Password;
	}
}
