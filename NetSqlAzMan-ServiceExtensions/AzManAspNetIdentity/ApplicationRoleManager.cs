using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity {
	public class ApplicationRoleManager : RoleManager<ApplicationRole, int> {
		public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
		: base(roleStore) {
			this.RoleValidator = new ApplicationRoleValidator(this);
		}

		//public ApplicationRoleManager(RoleStore store) : base(store) {
		//	this.RoleValidator = new ApplicationRoleValidator(this);
		//}

		//public override IQueryable<ApplicationRole> Roles {
		//	get {
		//		//return base.Roles;
		//		throw new NotImplementedException();
		//	}
		//}

		//public override Task<IdentityResult> CreateAsync(ApplicationRole role) {
		//	throw new NotImplementedException();
		//	//return base.CreateAsync(role);
		//}

		//public override Task<IdentityResult> DeleteAsync(ApplicationRole role) {
		//	throw new NotImplementedException();
		//	//return base.DeleteAsync(role);
		//}

		//public override bool Equals(object obj) {
		//	throw new NotImplementedException();
		//	//return base.Equals(obj);
		//}

		//public override Task<ApplicationRole> FindByIdAsync(int roleId) {
		//	throw new NotImplementedException();
		//	//return base.FindByIdAsync(roleId);
		//}

		//public override Task<ApplicationRole> FindByNameAsync(string roleName) {
		//	throw new NotImplementedException();
		//	//return base.FindByNameAsync(roleName);
		//}

		//public override int GetHashCode() {
		//	throw new NotImplementedException();
		//	//return base.GetHashCode();
		//}

		//public override Task<bool> RoleExistsAsync(string roleName) {
		//	throw new NotImplementedException();
		//	//return base.RoleExistsAsync(roleName);
		//}

		//public override string ToString() {
		//	throw new NotImplementedException();
		//	//return base.ToString();
		//}

		//public override Task<IdentityResult> UpdateAsync(ApplicationRole role) {
		//	throw new NotImplementedException();
		//	//return base.UpdateAsync(role);
		//}

		//protected override void Dispose(bool disposing) {
		//	//throw new NotImplementedException();
		//	base.Dispose(disposing);
		//}
	}
}