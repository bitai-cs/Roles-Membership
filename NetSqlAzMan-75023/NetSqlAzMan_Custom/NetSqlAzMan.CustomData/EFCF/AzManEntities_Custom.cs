using EntityFramework.Functions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.CustomDataLayer.EFCF
{
	public partial class AzManEntities
	{
		public const string dbo = nameof(dbo);

		public AzManEntities(string connectionString)
			: base(connectionString) {
#if DEBUG
			this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
		}

		public AzManEntities(DbConnection connection) : base(connection, false) {
#if DEBUG
			this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
		}

		// Defines scalar-valued function (non-composable), 
		// which cannot be used in LINQ to Entities queries;
		// and can be called directly.
		[Function(FunctionType.NonComposableScalarValuedFunction, nameof(ldapwac_fn_DomainProfileIsInUse), Schema = dbo)]
		[return: Parameter(DbType = "bit")]
		public bool? ldapwac_fn_DomainProfileIsInUse(
			 [Parameter(DbType = "varchar", ClrType = typeof(string))]string domainProfile) {
			ObjectParameter domainProfileParameter = new ObjectParameter(nameof(domainProfile), domainProfile);
			return this.ObjectContext().ExecuteFunction<bool?>(
				 nameof(this.ldapwac_fn_DomainProfileIsInUse), domainProfileParameter).SingleOrDefault();
		}
	}
}
