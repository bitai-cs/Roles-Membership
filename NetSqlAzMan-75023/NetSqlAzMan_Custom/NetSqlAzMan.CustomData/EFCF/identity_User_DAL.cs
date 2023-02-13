using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class identity_User :DataLayerBase<identity_User>
	{
		#region Private methods
		private async Task<bool> loadRelatedEntityDataAsync(EFCF.identity_User entity) {
			return await Task.Run(() => loadRelatedEntityData(entity));
		}
		#endregion

		protected override bool loadRelatedEntityData(identity_User entity) {
			if (entity != null) {
				if (entity.DepartmentId.HasValue)
					entity.identity_Department.ToString();
				entity.identity_UserBranchOffice.Count.ToString();
				foreach (var _e2 in entity.identity_UserBranchOffice) {
					_e2.identity_BranchOffice.ToString();
				}
			}

			return true;
		}

		public async Task<IEnumerable<EFCF.identity_User>> SelectAllAsync(string sortOrderField = "UserName", bool ascendingOrder = true, string userFilter = null, string nameFilter = null, Nullable<bool> enabledFilter = null) {
			IEnumerable<EFCF.identity_User> _list = null;

			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _oq = from u in _ct.identity_User
							 select u;

				//Aplicamos el filtro de usuario y nombre segun sea el caso
				if (!string.IsNullOrEmpty(userFilter) && !string.IsNullOrEmpty(nameFilter))
					_oq = _oq.Where(f => (f.UserName.Contains(userFilter) | f.FullName.Contains(nameFilter)));
				else if (!(string.IsNullOrEmpty(userFilter) && string.IsNullOrEmpty(nameFilter))) {
					if (!string.IsNullOrEmpty(userFilter))
						_oq = _oq.Where(f => f.UserName.Contains(userFilter));

					if (!string.IsNullOrEmpty(nameFilter))
						_oq = _oq.Where(f => f.FullName.Contains(nameFilter));
				}

				if (enabledFilter.HasValue)
					_oq = _oq.Where(f => f.Enabled.Equals(enabledFilter.Value));

				//Aplicamos el orden de los registros
				switch (sortOrderField) {
					case "UserID":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.UserID);
						else
							_oq = _oq.OrderByDescending(f => f.UserID);
						break;
					case "UserName":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.UserName);
						else
							_oq = _oq.OrderByDescending(f => f.UserName);
						break;
					case "FirstName":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.FirstName);
						else
							_oq = _oq.OrderByDescending(f => f.FirstName);
						break;
					case "LastName":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.LastName);
						else
							_oq = _oq.OrderByDescending(f => f.LastName);
						break;
					case "FullName":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.FullName);
						else
							_oq = _oq.OrderByDescending(f => f.FullName);
						break;
					case "Mail":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.Mail);
						else
							_oq = _oq.OrderByDescending(f => f.Mail);
						break;
					case "Enabled":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.Enabled);
						else
							_oq = _oq.OrderByDescending(f => f.Enabled);
						break;
					case "RowVersion":
						if (ascendingOrder)
							_oq = _oq.OrderBy(f => f.RowVersion);
						else
							_oq = _oq.OrderByDescending(f => f.RowVersion);
						break;
					default:
						throw new Exception(string.Format("No se pudo identificar el campo {0} para ordenar los regitros.", sortOrderField));
				}

				_list = await _oq.ToListAsync();

				//Load entity realted data using LAZY loading
				foreach (var _e in _list) {
					await loadRelatedEntityDataAsync(_e);
				}
			} //End of DbContext

			return _list;
		}

		public async Task<EFCF.identity_User> SelectByIdAsync(int id, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
				if (connectionManager.GetTransaction() != null)
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

				var _entity = await (from _r in _ct.identity_User
											where _r.UserID.Equals(id)
											select _r).SingleOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return _entity;
			}
		}

		public async Task<EFCF.identity_User> SelectByUsernameAsync(string userName, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
				if (connectionManager.GetTransaction() != null)
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

				var _entity = await (from _r in _ct.identity_User
											where _r.UserName.Equals(userName)
											select _r).SingleOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return _entity;
			}
		}

		public async Task<EFCF.identity_User> InsertAsync(EFCF.identity_User iuser, ConnectionManager connectionManager) {
			EFCF.identity_User _created = null;
			try {
				//Contexto de inserción de datos
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_ct.identity_User.Add(iuser);
					var _count = await _ct.SaveChangesAsync();
				}

				//Contexto de consulta (con los datos ya confirmados en 
				//el conexto anterior)
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

					//Load entity related data using LAZY loading
					_created = await (from u in _ct.identity_User
											where u.UserID == iuser.UserID
											select u).FirstAsync();

					await loadRelatedEntityDataAsync(_created);
				}

				return _created;
			}
			catch (DbEntityValidationException ex) {
				throw GetSummarizedValidationErrors(ex);
			}
			catch {
				throw;
			}
		}

		public async Task<EFCF.identity_User> UpdateAsync(EFCF.identity_User iuser, ConnectionManager connectionManager) {
			int _rc = -1;
			try {
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					var _q = from d in _ct.identity_UserBranchOffice
								where d.UserID == iuser.UserID
								select d;

					_ct.identity_UserBranchOffice.RemoveRange(_q);

					_rc = await _ct.SaveChangesAsync();
				}

				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_ct.Entry(iuser).State = EntityState.Modified;
					_ct.identity_UserBranchOffice.AddRange(iuser.identity_UserBranchOffice);

					_rc = await _ct.SaveChangesAsync();
				}

				iuser = await SelectByIdAsync(iuser.UserID, connectionManager);

				return iuser;
			}
			catch (DbEntityValidationException ex) {
				throw GetSummarizedValidationErrors(ex);
			}
			catch {
				throw;
			}
		}

		public async Task<EFCF.identity_User> UpdatePasswordAsync(EFCF.identity_User iuser, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
				if (connectionManager.GetTransaction() != null)
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

				SqlParameter[] _params = new SqlParameter[3];
				_params[0] = new SqlParameter("@p1", iuser.UserID);
				_params[1] = new SqlParameter("@p2", iuser.RowVersion);
				_params[2] = new SqlParameter("@p3", iuser.Password);
				var _return = await _ct.Database.ExecuteSqlCommandAsync("update [dbo].[identity_User] set [Password]=@p3 where [UserID]=@p1 and [RowVersion]=@p2;", _params);

				if (_return < 1)
					throw new DbUpdateConcurrencyException("No se ubica la versión del registro que desea actualizar. Ha sido modificado o eliminado por otro usuario o proceso. Se recomienda volver a cargar los datos.");
			}

			iuser = await SelectByIdAsync(iuser.UserID, connectionManager);

			return iuser;
		}
	}
}