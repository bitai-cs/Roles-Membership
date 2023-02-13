using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity {
	public class ApplicationRoleValidator : RoleValidator<ApplicationRole, int> {
		RoleManager<ApplicationRole, int> _manager;

		public ApplicationRoleValidator(RoleManager<ApplicationRole, int> manager) : base(manager) {
			_manager = manager;
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public override string ToString() {
			return base.ToString();
		}

		public override Task<IdentityResult> ValidateAsync(ApplicationRole item) {
			throw new NotImplementedException();
			//return base.ValidateAsync(item);
		}
	}
}
