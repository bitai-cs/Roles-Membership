using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AzManLoginWebServices.BusinessLayer.DataLayer
{
	/// <summary>
	/// Data layer base class for database interaction.
	/// </summary>
	abstract class DataLayerBase : IDisposable
	{
		#region Data Members

		Database _db;
		SqlConnection _mainConnection;
		bool _isDisposed;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor 
		/// </summary>
		public DataLayerBase() {
			// Initialize the class members of data layer.
			InitClass();
		}

		#endregion

		#region Properties

		/// <summary>
		/// get the sql connection object
		/// </summary>
		protected SqlConnection MainConnection {
			get {
				return _mainConnection;
			}

		}

		/// <summary>
		/// Get Enterprise Library database
		/// </summary>
		protected Database Db {
			get {
				return _db;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes class members.
		/// </summary>
		private void InitClass() {
			// create Connection Object
			_mainConnection = new SqlConnection();

			// Get connection string from Config File and set to the connection
			_mainConnection.ConnectionString = ConfigurationManager.ConnectionStrings[AzManLoginWebServices.LoginService.CONNECTIONSTRING_Name].ConnectionString;
			_db = DatabaseFactory.CreateDatabase();

			_isDisposed = false;
		}

		#endregion

		#region IDisposeable

		/// <summary>
		/// Implements the IDispose' method Dispose.
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Implements the Dispose functionality.
		/// </summary>
		protected virtual void Dispose(bool bIsDisposing) {
			// Check to see if Dispose has already been called.
			if (!_isDisposed) {
				if (bIsDisposing) {
					// Dispose managed resources.
					_mainConnection.Dispose();
					_mainConnection = null;
					_db = null;
				}
			}
			_isDisposed = true;
		}

		#endregion

	}
}
