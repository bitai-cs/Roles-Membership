using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using AzManAspNetIdentity.TestMVC.Models;

namespace AzManAspNetIdentity.TestMVC {
	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public class ApplicationUserManagerHelper {
		public static AzManAspNetIdentity.ApplicationUserManager Create(IdentityFactoryOptions<AzManAspNetIdentity.ApplicationUserManager> options, IOwinContext context) {
			var manager = new AzManAspNetIdentity.ApplicationUserManager(new AzManAspNetIdentity.UserStore(/*context.Get<ApplicationDbContext>()*/));

            // Configure validation logic for usernames
            //NOTE: Se puede personalizar el UserValidator
            manager.UserValidator = new UserValidator<AzManAspNetIdentity.ApplicationUser>(manager) {
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = false
			};

			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator {
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			//// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			//// You can write your own provider and plug it in here.
			//manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser> {
			//	MessageFormat = "Your security code is {0}"
			//});
			//manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser> {
			//	Subject = "Security Code",
			//	BodyFormat = "Your security code is {0}"
			//});

			manager.EmailService = new AzManAspNetIdentity.EmailService();
			manager.SmsService = new AzManAspNetIdentity.SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null) {
				manager.UserTokenProvider =
					 new DataProtectorTokenProvider<AzManAspNetIdentity.ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}

			return manager;
		}
	}

	// Configure the application sign-in manager which is used in this application.
	public class ApplicationSignInManagerHelper {
		public static AzManAspNetIdentity.ApplicationSignInManager Create(IdentityFactoryOptions<AzManAspNetIdentity.ApplicationSignInManager> options, IOwinContext context) {
			var _sm = new AzManAspNetIdentity.ApplicationSignInManager(context.GetUserManager<AzManAspNetIdentity.ApplicationUserManager>(), context.Authentication);

			return _sm;
		}
	}

	public class ApplicationRoleManagerHelper {
		public static AzManAspNetIdentity.ApplicationRoleManager Create(IdentityFactoryOptions<AzManAspNetIdentity.ApplicationRoleManager> options, IOwinContext context) {
			var _rm = new AzManAspNetIdentity.ApplicationRoleManager(new AzManAspNetIdentity.RoleStore());

			return _rm;
		}
	}
}
