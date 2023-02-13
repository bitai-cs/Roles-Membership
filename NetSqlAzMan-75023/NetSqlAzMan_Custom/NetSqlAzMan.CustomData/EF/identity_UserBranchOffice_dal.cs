using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EF {
	public partial class Identity_UserBranchOffice : DataLayerBase<Identity_UserBranchOffice> {
		protected override bool loadRelatedEntityData(Identity_UserBranchOffice entity) {
			return true;
		}

		public async Task<Identity_UserBranchOffice> InsertAsync(int userId, string branchOfficeId, ConnectionManager connectionManager) {
			var _new = new Identity_UserBranchOffice() {
				UserID = userId,
				BranchOfficeId = branchOfficeId
			};

			using (var _ct = Global.GetAzManEntities(connectionManager.GetConnection())) {
				_ct.Database.UseTransaction(connectionManager.GetTransaction());

				//var _exists = await (_ct.identity_UserBranchOffice.Where(f => (f.UserID == _new.UserID && f.BranchOfficeId == _new.BranchOfficeId))).FirstOrDefaultAsync();

				////Eliminación del registro
				//await _ct.Database.ExecuteSqlCommandAsync(@"delete from identity_UserBranchOffice where UserID=@p1 and BranchOfficeId=@p2;", new SqlParameter("@p1", _new.UserID), new SqlParameter("@p2", _new.BranchOfficeId));

				//if (_exists == null)
				_ct.Entry(_new).State = System.Data.Entity.EntityState.Added;

				await _ct.SaveChangesAsync();
			}

			return _new;
		}		
	}
}
