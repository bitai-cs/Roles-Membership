using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManStorageController :BaseApiController
	{
		internal AzManStorageController() : base() {
		}

		#region Private methods
		internal NetSqlAzMan.ServiceBusinessObjects.AzManStorage getSBOFromStorage(NetSqlAzMan.Interfaces.IAzManStorage storage, bool loadStores) {
			var _sbo = new NetSqlAzMan.ServiceBusinessObjects.AzManStorage() {
				ConnectionString = storage.ConnectionString,
				DatabaseVesion = storage.DatabaseVesion,
				IAmAdmin = storage.IAmAdmin,
				Mode = (NetSqlAzMan.ServiceBusinessObjects.AzManMode)storage.Mode,
				StorageTimeOut = storage.StorageTimeOut,
				LogErrors = storage.LogErrors,
				LogInformations = storage.LogInformations,
				LogWarnings = storage.LogWarnings,
				LogOnDb = storage.LogOnDb,
				LogOnEventLog = storage.LogOnEventLog
				//Description = Se autogenera al llamar a la propiedad
			};

			if (loadStores) {
				var _sboStores = new AzManStoresController().getSBOFromListOfStores(storage.GetStores(), storage, false, false, false);

				_sbo.Stores = _sboStores;
			}

			return _sbo;
		}
		#endregion

		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManStorage))]
		public async Task<IHttpActionResult> Get(bool loadStores = false) {
			var _sbo = await Task.Run(() => this.getSBOFromStorage(_storage, loadStores));

			return Ok(_sbo);
		}
	}
}