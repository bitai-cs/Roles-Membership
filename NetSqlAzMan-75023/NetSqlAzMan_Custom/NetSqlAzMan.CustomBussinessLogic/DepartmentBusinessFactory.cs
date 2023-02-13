using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class DepartmentBusinessFactory : BaseBusinessFactory {
		#region Private methods
		private CustomDataLayer.EFCF.identity_Department getEntityFromBSO(NetSqlAzMan.ServiceBusinessObjects.Department serviceObject) {
			return new CustomDataLayer.EFCF.identity_Department() {
				DepartmentId = serviceObject.DepartmentId,
				DepartmentName = serviceObject.DepartmentName,
				RowVersion = serviceObject.RowVersion
			};
		}

		private ServiceBusinessObjects.Department getBSOFromEntity(CustomDataLayer.EFCF.identity_Department dataEntity) {
			if (dataEntity == null)
				return null;

			return new ServiceBusinessObjects.Department {
				DepartmentId = dataEntity.DepartmentId,
				DepartmentName = dataEntity.DepartmentName,
				RowVersion = dataEntity.RowVersion
			};
		}
		#endregion

		public async Task<NetSqlAzMan.ServiceBusinessObjects.Department> GetNew() {
			return await Task.Run(() => new NetSqlAzMan.ServiceBusinessObjects.Department());
		}

		public async Task<List<NetSqlAzMan.ServiceBusinessObjects.Department>> GetAllAsync(string sortOrderField = "DepartmentName", bool ascendingOrder = true, string departmentNameFilter = null) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				if (string.IsNullOrEmpty(sortOrderField))
					throw new ArgumentException("Argumento no válido.", "sortOrderField");

				CustomDataLayer.EFCF.identity_Department _deptDal = new CustomDataLayer.EFCF.identity_Department();
				var _entityList = await _deptDal.SelectAllAsync(sortOrderField, ascendingOrder, departmentNameFilter);

				var _sboList = new List<ServiceBusinessObjects.Department>();
				await Task.Run(() => {
					foreach (var _e in _entityList) {
						_sboList.Add(this.getBSOFromEntity(_e));
					}
				});

				return _sboList;
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.Department>> GetByUserIdAsync(int userId) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _dal = new CustomDataLayer.EFCF.identity_Department();
				var _listIDepto = await _dal.SelectByUserIdAsync(userId);

				var _listRet = new List<NetSqlAzMan.ServiceBusinessObjects.Department>();
				await Task.Run(() => {
					foreach (var _e in _listIDepto) {
						_listRet.Add(this.getBSOFromEntity(_e));
					}
				});

				return _listRet;
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.Department> GetByIdAsync(int departmentId) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _dal = new CustomDataLayer.EFCF.identity_Department();
				var _entity = await _dal.SelectByIdAsync(departmentId);

				return this.getBSOFromEntity(_entity);
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.Department> InsertAsync(NetSqlAzMan.ServiceBusinessObjects.Department department) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(department);

				var _dal = new CustomDataLayer.EFCF.identity_Department();
				_entity = await _dal.InsertAsync(_entity);

				return this.getBSOFromEntity(_entity);
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.Department> UpdateAsync(NetSqlAzMan.ServiceBusinessObjects.Department department) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(department);

				var _dal = new CustomDataLayer.EFCF.identity_Department();
				_entity = await _dal.UpdateAsync(_entity);

				return this.getBSOFromEntity(_entity);
			}
			catch (DbUpdateConcurrencyException ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
			catch (Exception ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
			}
		}

		public async Task<ServiceBusinessObjects.Department> DeleteAsync(NetSqlAzMan.ServiceBusinessObjects.Department department) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = getEntityFromBSO(department);

				var _dal = new CustomDataLayer.EFCF.identity_Department();
				var _count = await _dal.DeleteAsync(_entity);

				return department;
			}
			catch (DbUpdateConcurrencyException ex) {
#if DEBUG
				Global.Loger.Error(ex);
#endif
				throw;
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
