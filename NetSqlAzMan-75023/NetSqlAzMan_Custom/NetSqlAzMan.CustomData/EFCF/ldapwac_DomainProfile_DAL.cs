using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class ldapwac_DomainProfile :ldapwac_DataLayerBase<ldapwac_DomainProfile>
	{
		#region Private methods
		private async Task<bool> loadRelatedEntityDataAsync(ldapwac_DomainProfile entity) {
			return await Task.Run(() => loadRelatedEntityData(entity));
		}
		#endregion

		protected override bool loadRelatedEntityData(ldapwac_DomainProfile entity) {
			if (entity != null) {
				entity.ldapwac_ServerBaseDN.Any();
			}

			return true;
		}

		public async Task<IEnumerable<ldapwac_DomainProfile>> SelectAllAsync() {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _entities = await (from _p in _ct.ldapwac_DomainProfile
											  orderby _p.DomainProfile
											  select _p).ToListAsync();

				foreach (var _e in _entities) {
					await loadRelatedEntityDataAsync(_e);
				}

				return _entities;
			}
		}

		public async Task<ldapwac_DomainProfile> SelectByDomainProfileIdAsync(byte domainProfileId) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _p in _ct.ldapwac_DomainProfile
							where _p.DomainProfileId == DomainProfileId
							select _p;

				var _entity = await _q.FirstOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return await _q.FirstOrDefaultAsync();
			}
		}

		public async Task<ldapwac_DomainProfile> SelectByDomainProfileAsync(string domainProfile) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _p in _ct.ldapwac_DomainProfile
							where _p.DomainProfile == domainProfile
							select _p;

				var _entity = await _q.FirstOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return await _q.FirstOrDefaultAsync();
			}
		}

		public async Task<ldapwac_DomainProfile> InsertAsync(ldapwac_DomainProfile newEntity) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				_ct.ldapwac_DomainProfile.Add(newEntity);
				var _count = await _ct.SaveChangesAsync();

				return newEntity;
			}
		}

		public async Task<ldapwac_DomainProfile> UpdateAsync(ldapwac_DomainProfile modifiedEntity) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				_ct.Entry(modifiedEntity).State = EntityState.Modified;
				var _count = await _ct.SaveChangesAsync();

				return modifiedEntity;
			}
		}

		public async Task<ldapwac_DomainProfile> DeleteAsync(ldapwac_DomainProfile deleteEntity, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
				if (connectionManager.GetTransaction() != null)
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

				_ct.Entry(deleteEntity).State = EntityState.Deleted;
				var _count = await _ct.SaveChangesAsync();

				return deleteEntity;
			}
		}
	}
}