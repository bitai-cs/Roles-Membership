using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManAuthorizationMemberDisplayInfo {
		public MemberType MemberType { get; set; }
		public string DisplayName { get; set; }
	}
}