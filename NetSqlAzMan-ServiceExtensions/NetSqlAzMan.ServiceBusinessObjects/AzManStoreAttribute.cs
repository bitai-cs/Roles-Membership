using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	/// <summary>
	/// Interfaces interface for all Attributes
	/// </summary>
	public class AzManStoreAttribute {
		/// <summary>
		/// Gets the authorization attribute id.
		/// </summary>
		/// <value>The authorization attribute id.</value>
		[Required]
		public int AttributeId { get; set; }

		/// <summary>
		/// Gets the Owner.
		/// </summary>
		/// <value>The parent.</value>
		public AzManStore Owner { get; set; }

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		[Required]
		public string Key { get; set; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		[Required]
		public string Value { get; set; }

		/// <summary>
		/// Occurs after an Attribute object has been Deleted.
		/// </summary>
		public event ENS.AttributeDeletedDelegate<AzManStore> AttributeDeleted;

		///// <summary>
		///// Occurs after an Attribute object has been Updated.
		///// </summary>
		//public event ENS.AttributeUpdatedDelegate<AzManStore> AttributeUpdated;

		public AzManStoreAttribute Clone() {
			return new AzManStoreAttribute {
				AttributeId = this.AttributeId,
				Key = this.Key,
				Owner = this.Owner.Clone(),
				Value = this.Value
			};
		}
	}
}