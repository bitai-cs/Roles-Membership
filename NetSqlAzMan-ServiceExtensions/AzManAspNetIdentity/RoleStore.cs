using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity {
	public class RoleStore : IRoleStore<ApplicationRole, int> {
		public RoleStore() {

		}

		public Task CreateAsync(ApplicationRole role) {
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ApplicationRole role) {
			throw new NotImplementedException();
		}

		public void Dispose() {			
			//throw new NotImplementedException();
		}

		public Task<ApplicationRole> FindByIdAsync(int roleId) {
			throw new NotImplementedException();
		}

		public Task<ApplicationRole> FindByNameAsync(string roleName) {
			throw new NotImplementedException();
		}

		public Task UpdateAsync(ApplicationRole role) {
			throw new NotImplementedException();
		}
	}
}
