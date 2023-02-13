using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class BranchOfficeBusinessFactory : BaseBusinessFactory {
		#region Private methods
		private NetSqlAzMan.CustomDataLayer.EFCF.identity_BranchOffice getEntityFromBSO(ServiceBusinessObjects.BranchOffice sbo) {
			return new CustomDataLayer.EFCF.identity_BranchOffice() {
				BranchOfficeId = sbo.BranchOfficeId,
				BranchOfficeName = sbo.BranchOfficeName,
				RelativeBranchOfficeId = sbo.RelativeBranchOfficeId,
				RowVersion = sbo.RowVersion
			};
		}

		private ServiceBusinessObjects.BranchOffice getBSOfromEntity(CustomDataLayer.EFCF.identity_BranchOffice entity) {
			if (entity == null)
				return null;

			return new ServiceBusinessObjects.BranchOffice() {
				BranchOfficeId = entity.BranchOfficeId,
				BranchOfficeName = entity.BranchOfficeName,
				RelativeBranchOfficeId = entity.RelativeBranchOfficeId,
				RowVersion = entity.RowVersion
			};
		}
		#endregion

		public async Task<List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>> GetAllAsync() {
			var _boffDal = new CustomDataLayer.EFCF.identity_BranchOffice();

			var _entityList = await _boffDal.SelectAllAsync();

			var _sboList = new List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>();
			await Task.Run(() => {
				foreach (var _e in _entityList) {
					_sboList.Add(this.getBSOfromEntity(_e));
				}
			});

			return _sboList;
		}

		public async Task<ServiceBusinessObjects.BranchOffice> GetByIdAsync(string id) {
			var _dal = new CustomDataLayer.EFCF.identity_BranchOffice();
			var _entity = await _dal.SelectByIdAsync(id);

			return this.getBSOfromEntity(_entity);
		}

		public async Task<List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>> GetAllByUserIdAsync(int userId) {
			List<CustomDataLayer.EFCF.identity_BranchOffice> _l;
			CustomDataLayer.EFCF.identity_BranchOffice _boffDal = new CustomDataLayer.EFCF.identity_BranchOffice();
			var _entityList = await _boffDal.SelectByUserId(userId);

			List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice> _sboList = new List<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>();
			await Task.Run(() => {
				foreach (var _e in _entityList) {
					_sboList.Add(this.getBSOfromEntity(_e));
				}
			});

			return _sboList;
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.BranchOffice> RegisterAsync(NetSqlAzMan.ServiceBusinessObjects.BranchOffice branchOffice) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(branchOffice);

				var _dal = new CustomDataLayer.EFCF.identity_BranchOffice();
				using (var _cm = _dal.GetConnectionManager(false)) {
					_entity = await _dal.InsertAsync(_entity, _cm);
				}

				return this.getBSOfromEntity(_entity);
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.BranchOffice> UpdateAsync(NetSqlAzMan.ServiceBusinessObjects.BranchOffice branchOffice) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(branchOffice);

				var _dal = new CustomDataLayer.EFCF.identity_BranchOffice();
				using (var _cm = _dal.GetConnectionManager(false)) {
					_entity = await _dal.UpdateAsync(_entity, _cm);
				}

				return this.getBSOfromEntity(_entity);
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.BranchOffice> DeleteAsync(NetSqlAzMan.ServiceBusinessObjects.BranchOffice branchOffice) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(branchOffice);

				var _dal = new CustomDataLayer.EFCF.identity_BranchOffice();
				using (var _cm = _dal.GetConnectionManager(false)) {
					var _count = await _dal.DeleteAsync(_entity, _cm);
				}

				return this.getBSOfromEntity(_entity);
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}
	}
}
