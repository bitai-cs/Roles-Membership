using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class identity_BranchOffice :DataLayerBase<identity_BranchOffice>
	{
		protected override bool loadRelatedEntityData(identity_BranchOffice entity) {
			return true;
		}

		public async Task<identity_BranchOffice> SelectByIdAsync(string id) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				return await (from bo in _ct.identity_BranchOffice
								  where bo.BranchOfficeId.Equals(id)
								  select bo).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<identity_BranchOffice>> SelectAllAsync() {
			try {
				using (var _ct = Global.GetAzManEntitiesCF()) {
					return await _ct.identity_BranchOffice.Select(_r => _r).OrderBy(k => k.BranchOfficeName).ToListAsync();
				}
			}
			catch {
				throw;
			}
		}

		public async Task<IEnumerable<identity_BranchOffice>> SelectByUserId(int userId) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _bo in _ct.identity_BranchOffice
							join _ubo in _ct.identity_UserBranchOffice on _bo.BranchOfficeId equals _ubo.BranchOfficeId
							where _ubo.UserID.Equals(userId)
							select _bo;

				return await _q.ToListAsync();
			}
		}

		public async Task<EFCF.identity_BranchOffice> InsertAsync(EFCF.identity_BranchOffice identity, ConnectionManager connectionManager) {
			EFCF.identity_BranchOffice _created = null;
			try {
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_ct.identity_BranchOffice.Add(identity);
					var _count = await _ct.SaveChangesAsync();
				}

				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_created = await (from e in _ct.identity_BranchOffice
											where e.BranchOfficeId == identity.BranchOfficeId
											select e).FirstAsync();
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

		public async Task<EFCF.identity_BranchOffice> UpdateAsync(EFCF.identity_BranchOffice identity, ConnectionManager connectionManager) {
			EFCF.identity_BranchOffice _updated = null;
			try {
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_ct.Entry(identity).State = EntityState.Modified;

					var _count = await _ct.SaveChangesAsync();
				}

				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_updated = await (from e in _ct.identity_BranchOffice
											where e.BranchOfficeId == identity.BranchOfficeId
											select e).FirstAsync();
				}

				return _updated;
			}
			catch (DbEntityValidationException ex) {
				throw GetSummarizedValidationErrors(ex);
			}
			catch {
				throw;
			}
		}

		public async Task<int> DeleteAsync(EFCF.identity_BranchOffice identity, ConnectionManager connectionManager) {
			try {
				using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
					if (connectionManager.GetTransaction() != null)
						_ct.Database.UseTransaction(connectionManager.GetTransaction());

					_ct.Entry(identity).State = EntityState.Deleted;

					return await _ct.SaveChangesAsync();
				}
			}
			catch (DbEntityValidationException ex) {
				throw GetSummarizedValidationErrors(ex);
			}
			catch {
				throw;
			}
		}
	}
}
