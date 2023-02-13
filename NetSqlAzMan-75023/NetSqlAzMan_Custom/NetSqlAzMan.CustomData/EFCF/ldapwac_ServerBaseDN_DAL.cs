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
	public partial class ldapwac_ServerBaseDN :ldapwac_DataLayerBase<ldapwac_ServerBaseDN>
	{
		#region Private methods
		private async Task<bool> loadRelatedEntityDataAsync(EFCF.ldapwac_ServerBaseDN entity) {
			return await Task.Run(() => loadRelatedEntityData(entity));
		}
		#endregion

		protected override bool loadRelatedEntityData(ldapwac_ServerBaseDN entity) {
			if (entity != null) {
				entity.ldapwac_DomainProfile.ToString();
			}

			return true;
		}

		public async Task<IEnumerable<ldapwac_ServerBaseDN>> SelectByDomainProfileIdAndWideScopeStatusAsync(byte domainProfileId, bool wideScopeBaseDN) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _b in _ct.ldapwac_ServerBaseDN
							where _b.DomainProfileId == domainProfileId
							select _b;

				if (wideScopeBaseDN)
					_q = _q.Where(f => f.BaseDNOrder.Equals(0));
				else
					_q = _q.Where(f => !f.BaseDNOrder.Equals(0));

				var _entities = await _q.ToArrayAsync();

				foreach (var _e in _entities) {
					await loadRelatedEntityDataAsync(_e);
				}

				return _entities;
			}
		}

		public async Task<ldapwac_ServerBaseDN> SelectByDomainProfileIdAndBaseDNOrderAsync(byte domainProfileId, byte baseDNOrder) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _b in _ct.ldapwac_ServerBaseDN
							where _b.DomainProfileId == domainProfileId
							where _b.BaseDNOrder.Equals(baseDNOrder)
							select _b;

				var _entity = await _q.FirstOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return _entity;
			}
		}

		public async Task<IEnumerable<ldapwac_ServerBaseDN>> SelectByDomainProfileAndWideScopeStatusAsync(string domainProfile, bool wideScopeBaseDN) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _b in _ct.ldapwac_ServerBaseDN
							where _b.ldapwac_DomainProfile.DomainProfile == domainProfile
							select _b;

				if (wideScopeBaseDN)
					_q = _q.Where(f => f.BaseDNOrder.Equals(0));
				else
					_q = _q.Where(f => !f.BaseDNOrder.Equals(0));

				var _entities = await _q.ToArrayAsync();

				foreach (var _e in _entities) {
					await loadRelatedEntityDataAsync(_e);
				}

				return _entities;
			}
		}

		public async Task<ldapwac_ServerBaseDN> SelectByDomainProfileAndBaseDNOrderAsync(string domainProfile, byte baseDNOrder) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _b in _ct.ldapwac_ServerBaseDN
							where _b.ldapwac_DomainProfile.DomainProfile == domainProfile
							where _b.BaseDNOrder.Equals(baseDNOrder)
							select _b;

				var _entity = await _q.FirstOrDefaultAsync();

				await loadRelatedEntityDataAsync(_entity);

				return _entity;
			}
		}
	}
}
