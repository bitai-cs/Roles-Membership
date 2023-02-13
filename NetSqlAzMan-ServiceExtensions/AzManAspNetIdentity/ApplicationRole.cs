using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity {
	public class ApplicationRole : IRole<int> {
		public ApplicationRole() {
		}

		public int Id { get; set; }

		public string Name { get; set; }
	}
}
