using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
		{

		}

		public override bool SupportsUserPhoneNumber => false;

		public override async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
		{
			var _values = user.Id.Split('|');
			if (!user.IsLdapUser) //User is DB User
			{
				//Validar el hash de la contraseña almacenada con la contraseña ingresada
				var _ret = await Task.Run(() => this.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password));
				switch (_ret)
				{
					case PasswordVerificationResult.Failed:
						return false;
					case PasswordVerificationResult.Success:
						return true;
					case PasswordVerificationResult.SuccessRehashNeeded:
						throw new Exception("El password es correcto, pero el hash necesita volver a ser generado debido a que tiene un formató obsoleto.");
					default:
						throw new NotImplementedException();
				}
			}
			else //User is Ldap Entry
			{
				var _h = new AzManWebApiClientHelpers.AzManCredentialsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>();
				var _return = await _h.ValidateCredentialAsync(new NetSqlAzMan.ServiceBusinessObjects.AzManCredential { DomainProfile = user.DomainProfile, UserName = user.UserName, Password = password });
				if (_h.IsResponseContentError(_return))
				{
					var _ex = _h.GetWebApiRequestException(_return);
					throw _ex;
				}
				else
					return true;
			}
		}
	}
}