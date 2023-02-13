using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EF
{
	/// <summary>
	/// Additional declarations for identity_User
	/// </summary>
	public partial class identity_User :DataLayerBase<identity_User>
	{
		#region Private methods
		private async Task<bool> loadRelatedEntityDataAsync(identity_User entity) {
			return await Task.Run(() => loadRelatedEntityData(entity));
		}
		#endregion

		protected override bool loadRelatedEntityData(identity_User entity) {
			if (entity.DepartmentId.HasValue)
				entity.identity_Department.ToString();

			entity.identity_UserBranchOffice.Count.ToString();
			foreach (var _e2 in entity.identity_UserBranchOffice) {
				_e2.identity_BranchOffice.ToString();
			}

			return true;
		}

		public async Task<IEnumerable<identity_User>> SelectAllAsync(string sortOrderField = "UserName", bool ascendingOrder = true, string userFilter = null, string nameFilter = null, Nullable<bool> enabledFilter = null) {
			IEnumerable<identity_User> _list = null;

			using (var _ct = Global.GetAzManEntities()) {
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

		public async Task<identity_User> SelectByIdAsync(int id) {
			using (var _ct = Global.GetAzManEntities()) {
				var _entity = await (from _r in _ct.identity_User
											where _r.UserID.Equals(id)
											select _r).FirstOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return _entity;
			}
		}

		public async Task<identity_User> InsertAsync(identity_User iuser) {
			identity_User _created = null;
			try {
				//Contexto de inserción de datos
				using (var _ct = Global.GetAzManEntities()) {
					_ct.identity_User.Add(iuser);
					var _count = await _ct.SaveChangesAsync();
				}

				//Contexto de consulta (con los datos ya confirmados en 
				//el conexto anterior)
				using (var _ct = Global.GetAzManEntities()) {
					//Load entity related data using LAZY loading
					_created = await (from u in _ct.identity_User
											where u.UserID == iuser.UserID
											select u).FirstAsync();

					await loadRelatedEntityDataAsync(_created);
				}

				return _created;
			}
			catch (Exception ex) {
				throw;
			}
		}

		public async Task<identity_User> UpdateAsync(identity_User iuser, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntities(connectionManager.GetConnection())) {
				_ct.Database.UseTransaction(connectionManager.GetTransaction());

				_ct.Entry(iuser).State = EntityState.Modified;

				int _count;
				foreach (var _e in iuser.identity_UserBranchOffice) {
					_count = await _ct.identity_UserBranchOffice.Where(f => f.UserID == _e.UserID && f.BranchOfficeId == _e.BranchOfficeId).CountAsync();
					if (_count > 0)
						_ct.Entry(_e).State = EntityState.Modified;
					else
						_ct.Entry(_e).State = EntityState.Added;
				}

				//NetSqlAzMan.CustomDataLayer.EF.identity_UserBranchOffice _dal = new EF.identity_UserBranchOffice();
				//_dal.

				_count = await _ct.SaveChangesAsync();
			}

			return iuser;
		}
	}
}
