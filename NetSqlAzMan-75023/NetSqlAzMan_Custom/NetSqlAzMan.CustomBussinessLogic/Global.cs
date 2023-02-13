using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace NetSqlAzMan.CustomBussinessLogic {
	public static class Global {
		/// <summary>
		/// AzManConnectionString debe de asignarse antes de realizar cualquier operación.
		/// </summary>
		public static string AzManConnectionString {
			get {
				return CustomDataLayer.Global.AzManConnectionString;
			}
			set {
				CustomDataLayer.Global.AzManConnectionString = value;
			}
		}

		/// <summary>
		/// AzManStorage que servirá de referencia para cualquier operación.
		/// </summary>
		public static ServiceBusinessObjects.AzManStorage AzManStorage;

		/// <summary>
		/// Logger local de laq librería
		/// </summary>
		internal static readonly log4net.ILog Loger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	}

	public class NetSqlAzManCustom_Exception :Exception {

	}
}