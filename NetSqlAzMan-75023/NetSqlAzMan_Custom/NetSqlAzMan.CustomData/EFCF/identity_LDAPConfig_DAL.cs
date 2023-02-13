using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NetSqlAzMan.CustomDataLayer.EFCF {
	public partial class identity_LDAPConfig {
		public async Task<IEnumerable<identity_LDAPConfig>> SelectAllAsync() {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _d in _ct.identity_LDAPConfig
							orderby _d.ldap_domain
							select _d;
				return await _q.ToListAsync();
			}
		}

		public async Task<identity_LDAPConfig> SelectByDomainProfileAsync(string domainProfile) {
			using (var _ct = Global.GetAzManEntitiesCF()) {
				var _q = from _d in _ct.identity_LDAPConfig
							where _d.ldap_domain.ToLower().Equals(domainProfile.ToLower())
							orderby _d.ldap_domain
							select _d;
				return await _q.FirstOrDefaultAsync();
			}
		}
	}
}
