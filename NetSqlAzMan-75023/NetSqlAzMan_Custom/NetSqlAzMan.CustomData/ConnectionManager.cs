using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer {
	public class ConnectionManager : IDisposable {
		private DbConnection _connection = null;
		private DbTransaction _transaction = null;
		private bool _isInTransaction = false;
		private bool _isInCommit = false;

		internal ConnectionManager() {
			//_connection = new EntityConnection(Global.GetAzManEntitiesConnectionString());
			_connection = new SqlConnection(Global.AzManConnectionString);
		}


		public bool IsInTransaction {
			get {
				return _isInTransaction;
			}
		}


		private void openIfClosed() {
			if (_connection.State == System.Data.ConnectionState.Closed)
				_connection.Open();
		}

		private async Task<bool> closeIfOpen() {
			if (_connection == null)
				return true;

			await Task.Run(() => {
				while (_connection.State == System.Data.ConnectionState.Executing || _connection.State == System.Data.ConnectionState.Fetching) {
					Thread.Sleep(5000);
				}
			});

			if (_connection.State == System.Data.ConnectionState.Open) {
				_connection.Close();
			}

			return true;
		}


		internal DbConnection GetConnection() {
			return _connection;
		}

		//internal void SetConnection(DbConnection connection) {
		//	_connection = connection;
		//}

		internal DbTransaction GetTransaction() {
			//if (_transaction == null)
			//	throw new InvalidOperationException("No se ha iniciado la transacción.");

			//if (!_isInTransaction)
			//	throw new InvalidOperationException("La transacción ya fué terminada. Debe de iniciar una nueva transacción.");

			return _transaction;
		}

		internal void SetTransaction(DbTransaction transaction) {
			if (_isInTransaction)
				throw new InvalidOperationException("Ya se está en una transacción. No se puede realizar la operación.");

			if (transaction == null)
				throw new InvalidOperationException("Debe de inicializar la transacción.");

			_transaction = transaction;
		}


		public void BeginTransaction() {
			if (_isInTransaction)
				throw new InvalidOperationException("Ya se ha iniciado una transacción. No se puede realizar la operación.");

			openIfClosed();

			_transaction = _connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

			_isInTransaction = true;
		}

		public async Task<bool> CommitTransaction() {
			await Task.Run(() => {
				if (_isInCommit)
					throw new InvalidOperationException("Ya se está realizando la confirmación de la transacción. No se puede realizar la operación.");

				_isInCommit = true;
				_transaction.Commit();
				_transaction.Dispose();
				_transaction = null;
				_isInCommit = false;

				_isInTransaction = false;

				return true;
			});

			await closeIfOpen();

			return true;
		}

		public async Task<bool> RollbackTransaction() {
			await Task.Run(() => {
				if (!_isInTransaction)
					throw new InvalidOperationException("No se ha iniciado alguna transacción. No se puede realizar la operación.");

				if (_isInCommit)
					throw new InvalidOperationException("Ya se está realizando la confirmación de la transacción. No se puede realizar la operación.");

				_transaction.Rollback();
				_transaction.Dispose();
				_transaction = null;

				_isInTransaction = false;

				return true;
			});

			await closeIfOpen();

			return true;
		}

		public void Dispose() {
			if (_isInTransaction) {
				var _ret = Task.Run(() => this.RollbackTransaction()).Result;
			}

			_transaction = null;
			_connection = null;
		}

		public override string ToString() {
			return string.Format("Connection String: {0}\nConnection State: {1}\nIs in Transaction: {2}", _connection.ConnectionString, _connection.State.ToString(), _isInTransaction);
		}
	}
}