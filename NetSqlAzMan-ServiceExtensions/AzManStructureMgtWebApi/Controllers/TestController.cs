using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzManStructureMgtWebApi.Controllers
{
	public class TestController : ApiController
	{
		[HttpGet]
		public IHttpActionResult Get(byte id)
		{
			switch (id)
			{
				case 1:
					return Ok<string>(string.Format("TEST 1: {0}", System.DateTime.Today.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString()));

				case 2:
					try
					{
						NetSqlAzMan.SqlAzManStorage.VerifyStorageDB(Global.Get_CONFIG_APPSETTINGS_AzMan_CustomBusinessLogic_DbConnection());

						return Ok(string.Format("TEST 2: Estado base de datos {0}", "OK"));
					}
					catch (Exception ex)
					{
						return Ok(string.Format("TEST 2: {0}", ex.Message));
					}
					
				case 3:
					var _topExce = new Exception("Top Error!", new InvalidCastException("Inner Error 1", new EntryPointNotFoundException("Inner Error 2")));

					throw _topExce;

				default:
					return Ok(string.Format("No existe el test nro. {0}", id));
			}
		}
	}
}
