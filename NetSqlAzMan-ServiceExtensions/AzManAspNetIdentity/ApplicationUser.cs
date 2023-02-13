using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity
{
	public class ApplicationUser : IUser<string>
	{
		/// <summary>
		/// Para un usuario de base de datos, es su ID como cadena.
		/// Para un usuario del Servicio de Directorio, es su ObjectSID como cadena
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Opcional. Se usa cuando el usuario proviene de un Servicio de Directorio
		/// </summary>
		public string DomainProfile { get; set; }

		/// <summary>
		/// Para un usuario LDAP, es el samAccountName
		/// </summary>
		public string UserName { get; set; }

		public bool IsLdapUser { get; set; }

		public string Email { get; set; }

		/// <summary>
		/// Para un usuario de BD, es el hash de su contraseña.
		/// Para un usuario LDAP, queda en nulo.
		/// </summary>
		public string PasswordHash { get; set; }

		public bool Enabled { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}
