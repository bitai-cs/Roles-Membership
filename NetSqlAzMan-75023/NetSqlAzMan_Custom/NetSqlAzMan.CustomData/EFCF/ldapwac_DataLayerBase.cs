using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public abstract class ldapwac_DataLayerBase<T> :DataLayerBase<T>
	{
		public bool? ldapwac_fn_DomainProfileIsInUse(string domainProfile, ConnectionManager connectionManager) {
			using (var _ct = Global.GetAzManEntitiesCF(connectionManager.GetConnection())) {
				if (connectionManager.GetTransaction() != null)
					_ct.Database.UseTransaction(connectionManager.GetTransaction());

				var _return = _ct.ldapwac_fn_DomainProfileIsInUse(domainProfile);

				return _return;
			}
		}
	}
}
