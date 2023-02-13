using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic
{
	public class LdapWacServerBaseDNBusinessFactory :BaseBusinessFactory
	{
		public LdapWacServerBaseDNBusinessFactory() : base() {
		}

		#region Private methods
		private ServiceBusinessObjects.LdapServerBaseDN getSboLdapServerBaseDNFromEntity(CustomDataLayer.EFCF.ldapwac_ServerBaseDN entity) {
			var _domainProfile = LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(entity.ldapwac_DomainProfile);

			return new ServiceBusinessObjects.LdapServerBaseDN() {
				BaseDNId = entity.BaseDNId,
				DomainProfile = _domainProfile,
				BaseDNOrder = entity.BaseDNOrder,
				BaseDN = entity.BaseDN,
				RowVersion = entity.RowVersion
			};
		}
		#endregion

		public async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>> GetLdapServerBaseDNByDomainProfileIdAndWideScopeStatusAsync(byte domainProfileId, Nullable<bool> wideScopeBaseDN) {
			if (wideScopeBaseDN == null)
				throw new ArgumentNullException("wideScopeBaseDN");

			var _dal = new CustomDataLayer.EFCF.ldapwac_ServerBaseDN();
			var _entities = await _dal.SelectByDomainProfileIdAndWideScopeStatusAsync(domainProfileId, wideScopeBaseDN.Value);

			return await Task.Run(() => {
				var _sboList = new List<ServiceBusinessObjects.LdapServerBaseDN>();
				foreach (var _entity in _entities) {
					_sboList.Add(this.getSboLdapServerBaseDNFromEntity(_entity));
				}
				return _sboList;
			});
		}

		public async Task<ServiceBusinessObjects.LdapServerBaseDN> GetLdapServerBaseDNByDomainProfileIdAndBaseDNOrderAsync(byte domainProfileId, Nullable<byte> baseDNOrder) {
			if (baseDNOrder == null)
				throw new ArgumentNullException("baseDNOrder");

			var _dal = new CustomDataLayer.EFCF.ldapwac_ServerBaseDN();
			var _entity = await _dal.SelectByDomainProfileIdAndBaseDNOrderAsync(domainProfileId, baseDNOrder.Value);

			return await Task.Run(() => this.getSboLdapServerBaseDNFromEntity(_entity));
		}

		public async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>> GetLdapServerBaseDNByDomainProfileAndWideScopeStatusAsync(string domainProfile, Nullable<bool> wideScopeBaseDN) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentException("domainProfile");

			if (wideScopeBaseDN == null)
				throw new ArgumentNullException("wideScopeBaseDN");

			var _dal = new CustomDataLayer.EFCF.ldapwac_ServerBaseDN();
			var _entities = await _dal.SelectByDomainProfileAndWideScopeStatusAsync(domainProfile, wideScopeBaseDN.Value);

			return await Task.Run(() => {
				var _sboList = new List<ServiceBusinessObjects.LdapServerBaseDN>();
				foreach (var _entity in _entities) {
					_sboList.Add(this.getSboLdapServerBaseDNFromEntity(_entity));
				}
				return _sboList;
			});
		}

		public async Task<ServiceBusinessObjects.LdapServerBaseDN> GetLdapServerBaseDNByDomainProfileAndBaseDNOrderAsync(string domainProfile, Nullable<byte> baseDNOrder) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentException("domainProfile");

			if (baseDNOrder == null)
				throw new ArgumentNullException("baseDNOrder");

			var _dal = new CustomDataLayer.EFCF.ldapwac_ServerBaseDN();
			var _entity = await _dal.SelectByDomainProfileAndBaseDNOrderAsync(domainProfile, baseDNOrder.Value);

			return await Task.Run(() => this.getSboLdapServerBaseDNFromEntity(_entity));
		}
	}
}
