using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
	/// <summary>
	/// Interfaces interface for all Attributes
	/// </summary>
	public class AzManAttribute<OWNER>
	{
		/// <summary>
		/// Gets the authorization attribute id.
		/// </summary>
		/// <value>The authorization attribute id.</value>
		public int AttributeId { get; set; }

		/// <summary>
		/// Gets the Owner.
		/// </summary>
		/// <value>The parent.</value>
		public OWNER Owner { get; set; }

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public string Key { get; set; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }

		/// <summary>
		/// Occurs after an Attribute object has been Deleted.
		/// </summary>
		public event ENS.AttributeDeletedDelegate<OWNER> AttributeDeleted;

		/// <summary>
		/// Occurs after an Attribute object has been Updated.
		/// </summary>
		public event ENS.AttributeUpdatedDelegate<OWNER> AttributeUpdated;
	}
}
