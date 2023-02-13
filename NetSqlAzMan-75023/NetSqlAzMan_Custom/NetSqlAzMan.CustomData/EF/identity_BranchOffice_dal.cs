using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NetSqlAzMan.CustomDataLayer.EF {
	public partial class identity_BranchOffice {
		public async Task<IEnumerable<identity_BranchOffice>> SelectAllAsync() {
			try {
				using (var _ct = Global.GetAzManEntities()) {
					return await _ct.identity_BranchOffice.Select(_r => _r).OrderBy(k => k.BranchOfficeName).ToListAsync();
				}
			}
			catch (Exception ex) {
				throw;
			}
		}

		public async Task<IEnumerable<identity_BranchOffice>> SelectByUserId(int userId) {
			using (var _ct = Global.GetAzManEntities()) {
				var _q = from _bo in _ct.identity_BranchOffice
							join _ubo in _ct.identity_UserBranchOffice on _bo.BranchOfficeId equals _ubo.BranchOfficeId
							where _ubo.UserID.Equals(userId)
							select _bo;

				return await _q.ToListAsync();
			}
		}
	}
}
