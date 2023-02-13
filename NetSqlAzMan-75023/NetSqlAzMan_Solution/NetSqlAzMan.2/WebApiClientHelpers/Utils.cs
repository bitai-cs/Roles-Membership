using NetSqlAzMan.LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.WebApiClientHelpers {
	internal class Utils {
		//internal async Task<LdapProfile> GetLdapProfile(string profileId) 
		internal async Task<NetSqlAzMan.ServiceBusinessObjects.LdapProfile> GetLdapProfileAsync(string profileId) {
			if (string.IsNullOrEmpty(profileId))
				throw new ArgumentException("Debe de especificar el ProfileId.");

			var _boffDal = new CustomDataLayer.EFCF.identity_LdapProfile();
			var _entity = await _boffDal.SelectByProfileIdAsync(profileId);

			if (_entity == null) return null;

			var _sbo = new NetSqlAzMan.ServiceBusinessObjects.LdapProfile() {
				ProfileId = _entity.ProfileId,
				LdapProxyWebApiUri = _entity.LdapProxyWebApiUri,
				LdapServerProfile = _entity.LdapServerProfile,
				Enabled = _entity.Enabled
			};

			return _sbo;
		}
	}
}
