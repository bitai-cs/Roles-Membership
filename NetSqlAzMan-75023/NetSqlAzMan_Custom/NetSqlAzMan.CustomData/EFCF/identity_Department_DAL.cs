using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NetSqlAzMan.CustomDataLayer.EFCF {
	public partial class identity_Department {
		public async Task<IEnumerable<identity_Department>> SelectAllAsync(string sortOrderField = "DepartmentName", bool ascendingOrder = true, string departmentNameFilter = null) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _r in _ct.identity_Department
							select _r;

				if (!string.IsNullOrEmpty(departmentNameFilter))
					_q = _q.Where(_r => _r.DepartmentName.Contains(departmentNameFilter));

				switch (sortOrderField) {
					case "DepartmentId":
						if (ascendingOrder)
							_q = _q.OrderBy(_r => _r.DepartmentId);
						else
							_q = _q.OrderByDescending(_r => _r.DepartmentId);
						break;
					case "DepartmentName":
						if (ascendingOrder)
							_q = _q.OrderBy(_r => _r.DepartmentName);
						else
							_q = _q.OrderByDescending(_r => _r.DepartmentName);
						break;
					case "RowVersion":
						if (ascendingOrder)
							_q = _q.OrderBy(_r => _r.RowVersion);
						else
							_q = _q.OrderByDescending(_r => _r.RowVersion);
						break;
					default:
						throw new ArgumentException("No se pudo identificar el campo de ordenamiento.");
				}

				return await _q.ToListAsync();
			}
		}

		public async Task<IEnumerable<identity_Department>> SelectByUserIdAsync(int userId) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _d in _ct.identity_Department
							join _u in _ct.identity_User on _d.DepartmentId equals _u.DepartmentId
							where _u.UserID.Equals(userId)
							select _d;
				return await _q.ToListAsync();
			}
		}

		public async Task<identity_Department> SelectByIdAsync(int departmentId) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _d in _ct.identity_Department
							where _d.DepartmentId.Equals(departmentId)
							select _d;
				var _l = await _q.ToListAsync();
				if (_l.Count.Equals(0))
					return null;
				else
					return _l[0];
			}
		}

		public async Task<identity_Department> InsertAsync(identity_Department idepartment) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				_ct.identity_Department.Add(idepartment);
				var _count = await _ct.SaveChangesAsync();

				return idepartment;
			}
		}

		public async Task<identity_Department> UpdateAsync(identity_Department department) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				_ct.Entry(department).State = EntityState.Modified;
				await _ct.SaveChangesAsync();

				return department;
			}
		}

		public async Task<int> DeleteAsync(identity_Department department) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				_ct.Entry(department).State = EntityState.Deleted;
				var _count = await _ct.SaveChangesAsync();

				return _count;
			}
		}
	}
}
