using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomBussinessLogic {
	public class LdapWacDomainProfileBusinessFactory : BaseBusinessFactory {
		#region Private methods
		private CustomDataLayer.EFCF.ldapwac_DomainProfile getEntityFromBSO(NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile sbo) {
			return new CustomDataLayer.EFCF.ldapwac_DomainProfile() {
				DomainProfileId = sbo.DomainProfileId,
				DomainProfile = sbo.DomainProfile,
				LdapProxyWebApiUri = sbo.LdapProxyWebApiUri,
				LdapServerProfile = sbo.LdapServerProfile,
				DomainName = sbo.DomainName,
				Enabled = sbo.Enabled
			};
		}
		#endregion

		#region Internal Static methods
		internal static ServiceBusinessObjects.LdapDomainProfile GetSBOfromEntity(CustomDataLayer.EFCF.ldapwac_DomainProfile entity) {
			if (entity == null)
				return null;

			var _sbo = new ServiceBusinessObjects.LdapDomainProfile() {
				DomainProfileId = entity.DomainProfileId,
				DomainProfile = entity.DomainProfile,
				LdapProxyWebApiUri = entity.LdapProxyWebApiUri,
				LdapServerProfile = entity.LdapServerProfile,
				DomainName = entity.DomainName,
				Enabled = entity.Enabled
			};

			return _sbo;
		}
		#endregion

		public async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>> GetAllLdapDomainProfilesAsync() {
			var _dal = new CustomDataLayer.EFCF.ldapwac_DomainProfile();
			var _entityList = await _dal.SelectAllAsync();
			var _sboList = new List<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>();
			await Task.Run(() => {
				foreach (var _e in _entityList) {
					_sboList.Add(LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(_e));
				}
			});

			return _sboList;
		}

		public async Task<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile> GetLdapDomainProfileByDomainProfileAsync(string domainProfile) {
			if (string.IsNullOrEmpty(domainProfile))
				throw new ArgumentNullException("domainProfile");

			var _boffDal = new CustomDataLayer.EFCF.ldapwac_DomainProfile();
			var _entity = await _boffDal.SelectByDomainProfileAsync(domainProfile);

			if (_entity == null) return null;

			var _sbo = LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(_entity);

			return _sbo;
		}

		public async Task<ServiceBusinessObjects.LdapDomainProfile> GetNew() {
			return await Task.Run(() => new ServiceBusinessObjects.LdapDomainProfile());
		}

		public async Task<ServiceBusinessObjects.LdapDomainProfile> RegisterAsync(ServiceBusinessObjects.LdapDomainProfile sbo) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				//Validar datos
				if (string.IsNullOrEmpty(sbo.DomainProfile))
					throw new Exception("El Perfil Dominio es requerido.");
				if (string.IsNullOrEmpty(sbo.LdapProxyWebApiUri))
					throw new Exception("El URL del Servicio Proxy Ldap es requerido.");
				if (string.IsNullOrEmpty(sbo.LdapServerProfile))
					throw new Exception("El Perfil de Servidor del Servicio Proxy Ldap es requerido.");
				if (string.IsNullOrEmpty(sbo.DomainName))
					throw new Exception("El Nombre del Dominio es requerido.");

				var _entity = this.getEntityFromBSO(sbo);

				var _dal = new CustomDataLayer.EFCF.ldapwac_DomainProfile();
				_entity = await _dal.InsertAsync(_entity);

				return LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(_entity);
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

		public async Task<ServiceBusinessObjects.LdapDomainProfile> UpdateAsync(ServiceBusinessObjects.LdapDomainProfile sbo) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				//Validar datos
				if (string.IsNullOrEmpty(sbo.DomainProfile))
					throw new Exception("El Perfil Dominio es requerido.");
				if (string.IsNullOrEmpty(sbo.LdapProxyWebApiUri))
					throw new Exception("El URL del Servicio Proxy Ldap es requerido.");
				if (string.IsNullOrEmpty(sbo.LdapServerProfile))
					throw new Exception("El Perfil de Servidor del Servicio Proxy Ldap es requerido.");
				if (string.IsNullOrEmpty(sbo.DomainName))
					throw new Exception("El Nombre del Dominio es requerido.");

				var _entity = this.getEntityFromBSO(sbo);

				var _dal = new CustomDataLayer.EFCF.ldapwac_DomainProfile();
				_entity = await _dal.UpdateAsync(_entity);

				return LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(_entity);
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

		public async Task<ServiceBusinessObjects.LdapDomainProfile> DeleteAsync(ServiceBusinessObjects.LdapDomainProfile sbo) {
			try {
#if DEBUG
				Thread.Sleep((new Random()).Next(500, 2000));
#endif
				var _entity = this.getEntityFromBSO(sbo);

				var _dal = new CustomDataLayer.EFCF.ldapwac_DomainProfile();

				using (var _cm = _dal.GetConnectionManager(true)) {
					var _flag = _dal.ldapwac_fn_DomainProfileIsInUse(sbo.DomainProfile, _cm);

					if (_flag.Value)
						throw new Exception("El Perfil de Dominio está asignado a otros elementos de seguridad. No se puede eliminar el Perfil de Dominio.");

					_entity = await _dal.DeleteAsync(_entity, _cm);

					await _cm.CommitTransaction();
				}

				return LdapWacDomainProfileBusinessFactory.GetSBOfromEntity(_entity);
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
