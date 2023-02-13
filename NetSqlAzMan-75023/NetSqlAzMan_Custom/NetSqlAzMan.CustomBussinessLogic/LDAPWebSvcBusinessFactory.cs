using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class LDAPWebSvcBusinessFactory : BaseBusinessFactory {
		public async Task<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig> GetLDAPConfigByDomainProfileAsync(string domainProfile) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentException("Debe de especificar el Domain Profile.");

			CustomDataLayer.EFCF.identity_LDAPConfig _boffDal = new CustomDataLayer.EFCF.identity_LDAPConfig();
			var _entity = await _boffDal.SelectByDomainProfileAsync(domainProfile);

			if (_entity == null) return null;

			var _sbo = new NetSqlAzMan.ServiceBusinessObjects.LDAPConfig() {
				ldap_domain = _entity.ldap_domain,
				ldap_description = _entity.ldap_description,
				ldap_client_endpoint = _entity.ldap_client_endpoint,
				ldap_enabled = _entity.ldap_enabled,
				RowVersion = _entity.RowVersion
			};

			return _sbo;
		}

		public async Task<List<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>> GetAllLDAPConfigAsync() {
			CustomDataLayer.EFCF.identity_LDAPConfig _boffDal = new CustomDataLayer.EFCF.identity_LDAPConfig();
			var _entityList = await _boffDal.SelectAllAsync();

			List<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig> _sboList = new List<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>();
			await Task.Run(() => {
				foreach (var _e in _entityList) {
					_sboList.Add(new NetSqlAzMan.ServiceBusinessObjects.LDAPConfig() {
						ldap_domain = _e.ldap_domain,
						ldap_description = _e.ldap_description,
						ldap_client_endpoint = _e.ldap_client_endpoint,
						ldap_enabled = _e.ldap_enabled,
						RowVersion = _e.RowVersion
					});
				}
			});

			return _sboList;
		}

		//TODO ESTE CÓDIGO DEBERÍA IMPLEMENTARSE EN EL SQLAZMANSTORAGE
		//Y SOLO SER LLAMADO DESDE AQUÍ y ELIMINAR LA REFERENCIA AL WEB SERVICE
		//LDAPHELPERSVCREF
		public async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>> SearchUsersAndGroupsWithWSvcAsync(string domainProfile, string searchCriteria) {
			var _cfg = await GetLDAPConfigByDomainProfileAsync(domainProfile);
			if (_cfg == null)
				throw new Exception(string.Format("No existe un perfil definido con el nombre {0}.", domainProfile));

			if (searchCriteria.Contains("**"))
				throw new ArgumentException("El criterio de búsqueda no puede contener doble caracter comodín.");

			if (searchCriteria.Replace("*", "").Length < 3)
				throw new ArgumentException("El criterio de búsqueda debe tener al menos 3 caracteres.");

			if (!searchCriteria.Contains('*'))
				searchCriteria = string.Format("*{0}*", searchCriteria);

			LDAPHelperSvcRef.LDAPHelperClient _c = new LDAPHelperSvcRef.LDAPHelperClient(_cfg.ldap_client_endpoint);
			var _req = new LDAPHelperSvcRef.SearchUsersAndGroupsByTwoPropertiesRequest(_cfg.ldap_domain, LDAPHelperSvcRef.LDAPHelperADProperties.cn, searchCriteria, LDAPHelperSvcRef.LDAPHelperADProperties.samAccountName, searchCriteria, true);
			var _resp = await _c.SearchUsersAndGroupsByTwoPropertiesAsync(_req);
			if (!_resp.SearchUsersAndGroupsByTwoPropertiesResult) {
				if (_resp.status.IsException) {
					throw new Exception(string.Format("{0}. ExceptionType: {1}", _resp.status.StatusMessage, _resp.status.ExceptionType));
				}
				else
					throw new Exception(_resp.status.StatusMessage);
			}

			var _resolvedList = new List<ServiceBusinessObjects.LDAPResult>();
			foreach (var _r in _resp.result) {
				_resolvedList.Add(new ServiceBusinessObjects.LDAPResult() {
					C = _r.c,
					CN = _r.cn,
					Company = _r.company,
					CreateTimeStamp = _r.createTimeStamp,
					defaultClassStore = _r.defaultClassStore,
					Department = _r.department,
					Description = _r.description,
					DirectoryEntryPath = _r.DirectoryEntryPath,
					DisplayName = _r.displayName,
					DistinguishedName = _r.distinguishedName,
					DomainProfile = _r.DomainProfile,
					GivenName = _r.givenName,
					IsDeleted = _r.isDeleted,
					LastLogon = _r.lastLogon,
					Mail = _r.mail,
					Manager = _r.manager?.distinguishedName,
					Member = _r.member,
					MemberOf = _r.memberOf,
					ObjectCategory = _r.objectCategory,
					ObjectClass = _r.objectClass,
					ObjectGuid = _r.objectGuid,
					ObjectGuidBytes = _r.objectGuidBytes,
					ObjectSidBytes = _r.objectSidBytes,
					ObjectSid = _r.objectSid,
					ResultFromGC = _r.ResultFromGC,
					samAccountName = _r.samAccountName,
					SN = _r.sn,
					TelephoneNumber = _r.telephoneNumber,
					Title = _r.Title,
					UserPrincipalName = _r.userPrincipalName
				});
			}

			return _resolvedList;
		}
	}
}