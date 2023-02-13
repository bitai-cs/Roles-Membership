using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer
{
	public abstract class DataLayerBase<T>
	{
		protected abstract bool loadRelatedEntityData(T entity);

		public ConnectionManager GetConnectionManager(bool beginTransaction) {
			return Global.CreateConnectionManager(beginTransaction);
		}

		internal DbEntityValidationException GetSummarizedValidationErrors(DbEntityValidationException ex) {
			// Retrieve the error messages as a list of strings.
			var errorMessages = ex.EntityValidationErrors
					  .SelectMany(x => x.ValidationErrors)
					  .Select(x => x.ErrorMessage);

			// Join the list to a single string.
			var fullErrorMessage = string.Join("; ", errorMessages);

			// Combine the original exception message with the new one.
			var exceptionMessage = string.Concat("Errores de validación: ", fullErrorMessage);

			// return a new DbEntityValidationException with the improved exception message.
			return new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
		}
	}
}
