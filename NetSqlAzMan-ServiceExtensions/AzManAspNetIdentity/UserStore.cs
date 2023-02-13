using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity
{
	public class UserStore :IUserStore<ApplicationUser>, IUserLockoutStore<ApplicationUser, string>, IUserPasswordStore<ApplicationUser>, IUserTwoFactorStore<ApplicationUser, string>, IUserRoleStore<ApplicationUser> /*, IUserLoginStore<ApplicationUser, string>*/
	{
		private ApplicationUser _loadedAppUser;


		#region Private methods
		private ApplicationUser createAppUserFromDBUser(NetSqlAzMan.ServiceBusinessObjects.AzManDBUser dbUser) {
			return new ApplicationUser() {
				Id = "DBU|" + dbUser.CustomSid.StringValue,
				DomainProfile = null,
				UserName = dbUser.UserName,
				IsLdapUser = false,
				PasswordHash = dbUser.CustomColumns["Password"].ToString(),
				Email = dbUser.CustomColumns["Mail"].ToString(),
				Enabled = Convert.ToBoolean(dbUser.CustomColumns["Enabled"])
			};
		}

		private ApplicationUser createAppUserFromLdapEntry(LdapHelperDTO.LdapEntry entry) {
			return new ApplicationUser() {
				Id = "LDU|" + entry.objectSid + "|" + entry.DomainProfile + "|" + entry.samAccountName,
				DomainProfile = entry.DomainProfile,
				UserName = entry.samAccountName,
				IsLdapUser = true,
				PasswordHash = null,
				Email = entry.mail,
				Enabled = true
			};
		}
		#endregion


		#region IUserStore Implementation
		public Task CreateAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}

		public async Task<ApplicationUser> FindByIdAsync(string userId) {
			if (_loadedAppUser != null) {
				if (_loadedAppUser.Id.Equals(userId))
					return _loadedAppUser;
				else
					throw new InvalidOperationException("El id del usuario cargado no coincide con el Id que desea buscar.");
			}
			else {
				var _values = userId.Split('|');
				if (_values[0].Equals("DBU")) {
					NetSqlAzMan.ServiceBusinessObjects.AzManDBUser _dbUser = null;
					var _h = new AzManWebApiClientHelpers.AzManDBUsersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>();
					var _return = await _h.GetByUserNameOrIdAsync(_values[1], true);
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_dbUser = _h.GetSBOFromReturnedContent(_return);

					_loadedAppUser = createAppUserFromDBUser(_dbUser);

					return _loadedAppUser;
				}
				else {
					NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN _baseDN = null;

					var _objectSid = _values[1];
					var _domainProfile = _values[2];
					var _samAccountName = _values[3];
					var _h1 = new AzManWebApiClientHelpers.LdapWebApiServerBaseDNsHelper<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>();
					var _return = await _h1.GetByDomainProfileAndWideScopeStatusAsync(_domainProfile, true);
					if (_h1.IsResponseContentError(_return))
						_h1.ThrowWebApiRequestException(_return);
					else
						_baseDN = (_h1.GetEnumerableSBOFromReturnedContent(_return)).FirstOrDefault();

					LdapHelperDTO.LdapEntry _entry = null;
					var _h2 = new AzManWebApiClientHelpers.LdapWebApiUsersHelper<LdapHelperDTO.AsyncResult>();
					//Buscar el usuario
					var _return2 = await _h2.SearchUsersAndGroupsAsyncModeAsync(_baseDN.DomainProfile.DomainProfile, true, _baseDN.BaseDNOrder, _samAccountName, LdapHelperDTO.RequiredEntryAttributes.Minimun);
					//Validar si es que se obtuvo un error
					if (_h2.IsResponseContentError(_return2))
						_h2.ThrowWebApiRequestException(_return2);
					else { //Obtener el SBO obtenido
						var _ar = _h2.GetSBOFromReturnedContent(_return2);
						if (_ar.HasErrorInfo())
							throw new Exception(string.Format("{0}. Error tipo {1}", _ar.ErrorMessage, _ar.ErrorType));

						if (_ar.Entries.Count().Equals(0))
							throw new Exception(string.Format("No se ubica el usuario {0} en el repositorio indicado.", _samAccountName));

						_entry = _ar.Entries.First();
						//Validar que el entry obtenido tenga el mismo objectSid del login
						if (!_entry.objectSid.Equals(_objectSid, StringComparison.OrdinalIgnoreCase))
							throw new Exception(string.Format("El objectSid almacenado no coincide con el objectSid obtenido para el usuario {0}", _samAccountName));
					}

					_loadedAppUser = createAppUserFromLdapEntry(_entry);

					return _loadedAppUser;
				}
			}
		}

		public async Task<ApplicationUser> FindByNameAsync(string userName) {
			//throw new NotImplementedException();
			bool _isDbUser = false;
			string _domainProfile = null;
			string _userName = null;

			if (userName.Contains('\\')) {
				var _values = userName.Split('\\');
				_domainProfile = _values[0];
				_userName = _values[1];

				_isDbUser = false;
			}
			else {
				_domainProfile = string.Empty;
				_userName = userName;

				_isDbUser = true;
			}

			ApplicationUser _appUser = null;

			if (_isDbUser) {
				NetSqlAzMan.ServiceBusinessObjects.AzManDBUser _dbUser = null;
				var _h = new AzManWebApiClientHelpers.AzManDBUsersHelper<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>();
				var _return = await _h.GetByUserNameOrIdAsync(_userName, false);
				if (_h.IsResponseContentError(_return))
					_h.ThrowWebApiRequestException(_return);
				else
					_dbUser = _h.GetSBOFromReturnedContent(_return);

				_appUser = createAppUserFromDBUser(_dbUser);

				_loadedAppUser = _appUser;
			}
			else {
				NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN _baseDN = null;
				var _h1 = new AzManWebApiClientHelpers.LdapWebApiServerBaseDNsHelper<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>();
				var _return = await _h1.GetByDomainProfileAndWideScopeStatusAsync(_domainProfile, true);
				if (_h1.IsResponseContentError(_return))
					_h1.ThrowWebApiRequestException(_return);
				else
					_baseDN = (_h1.GetEnumerableSBOFromReturnedContent(_return)).FirstOrDefault();

				if (_baseDN == null)
					throw new Exception(string.Format("No se encontró la configuración BaseDN para el perfil de dominio {0}", _domainProfile));

				LdapHelperDTO.LdapEntry _entry = null;
				var _h2 = new AzManWebApiClientHelpers.LdapWebApiUsersHelper<LdapHelperDTO.AsyncResult>();
				var _return2 = await _h2.SearchUsersAndGroupsAsyncModeAsync(_baseDN.DomainProfile.DomainProfile, true, _baseDN.BaseDNOrder, _userName, LdapHelperDTO.RequiredEntryAttributes.Minimun);
				if (_h2.IsResponseContentError(_return2))
					_h2.ThrowWebApiRequestException(_return2);
				else {
					var _ar = _h2.GetSBOFromReturnedContent(_return2);
					if (_ar.HasErrorInfo())
						throw new Exception(string.Format("{0}. Error tipo {1}", _ar.ErrorMessage, _ar.ErrorType));

					if (_ar.Entries.Count().Equals(0))
						throw new Exception(string.Format("No se ubica el usuario {0} en el repositorio indicado.", userName));

					_entry = _ar.Entries.First();
				}

				_appUser = createAppUserFromLdapEntry(_entry);

				_loadedAppUser = _appUser;
			}

			return _appUser;
		}

		public Task UpdateAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}
		#endregion

		#region IUserLockoutStore Implementation
		public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}

		public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd) {
			throw new NotImplementedException();
		}

		public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}

		public Task ResetAccessFailedCountAsync(ApplicationUser user) {
			throw new NotImplementedException();
		}

		public Task<int> GetAccessFailedCountAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			return Task.FromResult(0);
		}

		public Task<bool> GetLockoutEnabledAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			return Task.Run(() => {
				return false;
			});
		}

		public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled) {
			throw new NotImplementedException();
		}
		#endregion

		#region IUserPasswordStore implementation
		public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash) {
			//throw new NotImplementedException();
			user.PasswordHash = passwordHash;

			return Task.FromResult(0);
		}

		public Task<string> GetPasswordHashAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			return Task.FromResult(true);
		}
		#endregion

		#region IUserTwoFactorStore implementation
		public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled) {
			//throw new NotImplementedException();
			return Task.FromResult(0);
		}

		public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			return Task.FromResult(false);
		}
		#endregion

		#region IUserRoleStore implementation
		public Task AddToRoleAsync(ApplicationUser user, string roleName) {
			throw new NotImplementedException();
		}

		public Task RemoveFromRoleAsync(ApplicationUser user, string roleName) {
			throw new NotImplementedException();
		}

		public Task<IList<string>> GetRolesAsync(ApplicationUser user) {
			//throw new NotImplementedException();
			var _array = new string[] { "Administrador", "Dummy", "Temp" };

			return Task.FromResult((IList<string>)_array);
		}

		public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName) {
			throw new NotImplementedException();
		}
		#endregion

		#region IUserLoginStore implementation
		//public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<ApplicationUser> FindAsync(UserLoginInfo login)
		//{
		//	throw new NotImplementedException();
		//}
		#endregion

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~UserStore() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose() {
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
