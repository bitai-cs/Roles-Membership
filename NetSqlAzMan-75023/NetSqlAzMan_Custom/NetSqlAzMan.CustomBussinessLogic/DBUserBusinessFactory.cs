using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class DBUserBusinessFactory : BaseBusinessFactory {
		#region Private methods		
		private NetSqlAzMan.ServiceBusinessObjects.Department getSboDepartmentFromEntity(CustomDataLayer.EFCF.identity_User entity) {
			if (entity.identity_Department == null)
				return null;

			return new ServiceBusinessObjects.Department() { DepartmentId = entity.identity_Department.DepartmentId, DepartmentName = entity.identity_Department.DepartmentName, RowVersion = entity.identity_Department.RowVersion };
		}

		private ServiceBusinessObjects.ListOfSBO<NetSqlAzMan.ServiceBusinessObjects.BranchOffice> getSboUserBranchOfficeListOfSBOFromEntity(CustomDataLayer.EFCF.identity_User entity) {
			var _ret = new ServiceBusinessObjects.ListOfSBO<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>();

			foreach (var _r in entity.identity_UserBranchOffice) {
				_ret.Add(new NetSqlAzMan.ServiceBusinessObjects.BranchOffice() { BranchOfficeId = _r.BranchOfficeId, BranchOfficeName = _r.identity_BranchOffice.BranchOfficeName, RelativeBranchOfficeId = _r.identity_BranchOffice.BranchOfficeId, RowVersion = _r.RowVersion });
			}

			return _ret;
		}

		private ServiceBusinessObjects.DBUser getSBOFromEntity(CustomDataLayer.EFCF.identity_User entity, ServiceBusinessObjects.AzManStorage storage) {
			if (entity == null)
				return null;

			var _dep = this.getSboDepartmentFromEntity(entity);

			var _offices = this.getSboUserBranchOfficeListOfSBOFromEntity(entity);

			return new ServiceBusinessObjects.DBUser() {
				Department = _dep,
				FullName = entity.FullName,
				Enabled = entity.Enabled,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				Mail = entity.Mail,
				Password = entity.Password,
				RowVersion = entity.RowVersion,
				Storage = storage,
				UserID = entity.UserID,
				UserName = entity.UserName,
				UserBranchOffices = _offices
			};
		}

		private CustomDataLayer.EFCF.identity_User getNewEntityFromSBO(ServiceBusinessObjects.DBUser dbUser) {
			var _iuser = new CustomDataLayer.EFCF.identity_User() {
				UserID = dbUser.UserID,
				UserName = dbUser.UserName,
				Password = dbUser.Password,
				FirstName = dbUser.FirstName,
				LastName = dbUser.LastName,
				FullName = dbUser.FullName,
				Mail = dbUser.Mail,
				/*No se debe de asignar una entidad Department, por que EF al detectar una 
				 * operación INSERT creará una nueva*/
				//identity_Department = new CustomDataLayer.EF.identity_Department() { DepartmentId = dbUser.Department.DepartmentId, DepartmentName = dbUser.Department.DepartmentName },
				Enabled = dbUser.Enabled,
				RowVersion = dbUser.RowVersion
			};

			if (dbUser.Department != null) {
				_iuser.DepartmentId = dbUser.Department.DepartmentId;
			}

			foreach (var _o in dbUser.UserBranchOffices) {
				_iuser.identity_UserBranchOffice.Add(new CustomDataLayer.EFCF.identity_UserBranchOffice() {
					UserID = dbUser.UserID,
					BranchOfficeId = _o.BranchOfficeId,
					RowVersion = _o.RowVersion
				});
			}

			return _iuser;
		}

		private async Task<CustomDataLayer.EFCF.identity_User> getExistingEntityFromSBO(ServiceBusinessObjects.DBUser dbUser) {
			CustomDataLayer.EFCF.identity_User _iuser;
			var _userDal = new CustomDataLayer.EFCF.identity_User();
			using (var _cm = _userDal.GetConnectionManager(false)) {
				_iuser = await _userDal.SelectByIdAsync(dbUser.UserID, _cm);
			}

			return _iuser;
		}
		#endregion

		public async Task<List<NetSqlAzMan.ServiceBusinessObjects.DBUser>> GetAllAsync(string sortOrderField = "UserName", bool ascendingOrder = true, string userFilter = null, string nameFilter = null, Nullable<bool> enabledFilter = null) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			if (string.IsNullOrEmpty(sortOrderField))
				throw new ArgumentException("sortOrderField");

			var _storage = Global.AzManStorage;

			NetSqlAzMan.CustomDataLayer.EFCF.identity_User _userDal = new CustomDataLayer.EFCF.identity_User();
			var _entityList = await _userDal.SelectAllAsync(sortOrderField, ascendingOrder, userFilter, nameFilter, null);

			return await Task.Run(() => {
				var _ret = new ServiceBusinessObjects.ListOfSBO<NetSqlAzMan.ServiceBusinessObjects.DBUser>();
				foreach (var _e in _entityList) {
					_ret.Add(this.getSBOFromEntity(_e, _storage));
				};

				return _ret;
			});
		}

		public async Task<ServiceBusinessObjects.DBUser> GetByIdAsync(int id) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			var _storage = Global.AzManStorage;

			NetSqlAzMan.CustomDataLayer.EFCF.identity_User _userDal = new CustomDataLayer.EFCF.identity_User();
			NetSqlAzMan.CustomDataLayer.EFCF.identity_User _iuser = null;
			using (var _cm = _userDal.GetConnectionManager(false)) {
				_iuser = await _userDal.SelectByIdAsync(id, _cm);
			}

			var _user = this.getSBOFromEntity(_iuser, _storage);

			return _user;
		}

		public async Task<ServiceBusinessObjects.DBUser> GetByUsernameAsync(string username) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			var _storage = Global.AzManStorage;

			var _userDal = new CustomDataLayer.EFCF.identity_User();
			NetSqlAzMan.CustomDataLayer.EFCF.identity_User _iuser = null;
			using (var _cm = _userDal.GetConnectionManager(false)) {
				_iuser = await _userDal.SelectByUsernameAsync(username, _cm);
			}

			var _user = this.getSBOFromEntity(_iuser, _storage);

			return _user;
		}

		public async Task<ServiceBusinessObjects.DBUser> RegisterAsync(ServiceBusinessObjects.DBUser dbUser) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			if (string.IsNullOrEmpty(dbUser.Password) || string.IsNullOrEmpty(dbUser.PasswordConfirmation))
				throw new Exception("Debe de ingresar la contraseña y la confirmación de la contraseña.");

			if (!dbUser.Password.Equals(dbUser.PasswordConfirmation))
				throw new System.Data.DataException("La contraseña no coincide con la confirmación.");

			var _storage = Global.AzManStorage;

			var _iuser = getNewEntityFromSBO(dbUser);
			_iuser.Password = new PasswordHasher().HashPassword(_iuser.Password);

			var _userDal = new CustomDataLayer.EFCF.identity_User();
			using (var _cm = _userDal.GetConnectionManager(false)) {
				_cm.BeginTransaction();
				_iuser = await _userDal.InsertAsync(_iuser, _cm);
				await _cm.CommitTransaction();
			}

			return this.getSBOFromEntity(_iuser, _storage);
		}

		public async Task<ServiceBusinessObjects.DBUser> UpdateAsync(ServiceBusinessObjects.DBUser dbUser) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			CustomDataLayer.EFCF.identity_User _userDal = new CustomDataLayer.EFCF.identity_User();
			CustomDataLayer.ConnectionManager _cm = null;

			try {
				ServiceBusinessObjects.DBUser _sbo;

				var _iuser = this.getNewEntityFromSBO(dbUser);

				var _storage = Global.AzManStorage;

				using (_cm = _userDal.GetConnectionManager(false)) {
					_cm.BeginTransaction();

					var _updated = await _userDal.UpdateAsync(_iuser, _cm);

					await _cm.CommitTransaction();

					_sbo = this.getSBOFromEntity(_updated, _storage);
				}

				return _sbo;
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		public async Task<ServiceBusinessObjects.DBUser> ChangePasswordAsync(ServiceBusinessObjects.DBUser dbUser) {
#if DEBUG
			System.Threading.Thread.Sleep(new Random().Next(1000, 2500));
#endif
			try {
				//Verificar si se tiene la nueva contraseña y la confirmación de la nueva contraseña
				if (string.IsNullOrEmpty(dbUser.NewPassword) || string.IsNullOrEmpty(dbUser.PasswordConfirmation))
					throw new Exception("Debe de ingresar la contraseña y la confirmación de la contraseña.");

				//Verificar la igualdad entre la nueva contraseña y su confirmación.
				if (!dbUser.NewPassword.Equals(dbUser.PasswordConfirmation))
					throw new Exception("La nueva contraseña y la confirmación no coinciden.");

				var _ph = new CustomBussinessLogic.PasswordHasher();
				//Verificar si la nueva contraseña es igual a la contraseña actual.
				var _v = _ph.VerifyHashedPassword(dbUser.Password, dbUser.NewPassword);
				if (_v)
					throw new InvalidOperationException("La nueva contraseña no puede ser igual a la contraseña vigente.");

				//Verificar que la contraseña vigente ingresada sea igual a la 
				//contraseña vigente almacenada
				_v = _ph.VerifyHashedPassword(dbUser.Password, dbUser.CurrentPassword);
				if (!_v)
					throw new InvalidOperationException("La contraseña vigente no es la correcta.");

				//Hash la nueva contraseña 
				var _h = _ph.HashPassword(dbUser.NewPassword);

				ServiceBusinessObjects.DBUser _sbo;

				var _storage = Global.AzManStorage;

				var _userDal = new CustomDataLayer.EFCF.identity_User();

				var _iuser = this.getNewEntityFromSBO(dbUser);
				_iuser.Password = _h;

				using (var _cm = _userDal.GetConnectionManager(true)) {
					var _updated = await _userDal.UpdatePasswordAsync(_iuser, _cm);

					await _cm.CommitTransaction();

					_sbo = getSBOFromEntity(_updated, _storage);
				}

				return _sbo;
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		//public async Task<bool> ChangeDBUserPassword(string userName, string currentPassword, string renewedPassword, string passwordConfirmation, out Exception hex) {
		//	hex = null;
		//	try {
		//		IAzManDBUser _dbUser;
		//		Exception _hex;
		//		if (!this.GetAzManDBUser(userName, out _dbUser, out _hex)) {
		//			hex = _hex;
		//			return false;
		//		}

		//		var _password = _dbUser.CustomColumns["Password"];
		//		if (!_password.Equals(currentPassword))
		//			throw new InvalidOperationException("La contraseña actual proporcionada no es válida.");
		//		if (!renewedPassword.Equals(passwordConfirmation))
		//			throw new InvalidOperationException("La nueva contraseña y la confirmación de la contraseña no coinciden.");

		//		var _r = (from u in this.db.DBUserTable
		//					 where u.UserName.ToLower() == userName.ToLower()
		//					 select u).First();
		//		_r.Password = renewedPassword;
		//		this.db.SubmitChanges();

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		hex = ex;
		//		return false;
		//	}
		//}

		//[Obsolete("Yan o se debe de usar este método. Será removido en futuras actualizaciones.")]
		//public bool ChangeDBUserPassword(NetSqlAzMan.Cache.DBUser user, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace) {
		//	SqlConnection conn = new SqlConnection(this.db.Connection.ConnectionString);

		//	exceptionType = null;
		//	statusMessage = null;
		//	stackTrace = null;

		//	try {
		//		conn.Open();

		//		SqlCommand cmd = new SqlCommand("dbo.[identity_sp_ChangeDBUserPassword]", conn) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		cmd.Parameters.AddWithValue("@userId", user.UserID);
		//		cmd.Parameters.AddWithValue("@currentPassword", currentPassword);
		//		cmd.Parameters.AddWithValue("@renewedPassword", renewedPassword);
		//		cmd.Parameters.AddWithValue("@passwordConfirmation", passwordConfirmation);

		//		cmd.ExecuteNonQuery();

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		exceptionType = ex.GetType().FullName;
		//		statusMessage = ex.Message;
		//		stackTrace = ex.StackTrace;

		//		return false;
		//	}
		//	finally {
		//		if (conn.State == ConnectionState.Open)
		//			conn.Close();
		//	}
		//}

		//[Obsolete("Yan o se debe de usar este método. Será removido en futuras actualizaciones.")]
		//public bool ChangeDBUserPasswordById(int userId, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace) {
		//	SqlConnection conn = new SqlConnection(this.db.Connection.ConnectionString);

		//	exceptionType = null;
		//	statusMessage = null;
		//	stackTrace = null;

		//	try {
		//		conn.Open();

		//		SqlCommand cmd = new SqlCommand("dbo.[identity_sp_ChangeDBUserPassword]", conn) {
		//			CommandType = CommandType.StoredProcedure
		//		};
		//		cmd.Parameters.AddWithValue("@userId", userId);
		//		cmd.Parameters.AddWithValue("@currentPassword", currentPassword);
		//		cmd.Parameters.AddWithValue("@renewedPassword", renewedPassword);
		//		cmd.Parameters.AddWithValue("@passwordConfirmation", passwordConfirmation);

		//		cmd.ExecuteNonQuery();

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		exceptionType = ex.GetType().FullName;
		//		statusMessage = ex.Message;
		//		stackTrace = ex.StackTrace;

		//		return false;
		//	}
		//	finally {
		//		if (conn.State == ConnectionState.Open)
		//			conn.Close();
		//	}
		//}
	}
}