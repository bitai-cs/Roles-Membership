using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity {
	public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) {
		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user) {
			//return base.CreateUserIdentityAsync(user);
			return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
		}
	}
}