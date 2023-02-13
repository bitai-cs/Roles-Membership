using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManItemAttribute {
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
		public AzManItem Owner { get; set; }

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
		public event ENS.AttributeDeletedDelegate<AzManItem> AttributeDeleted;

		///// <summary>
		///// Occurs after an Attribute object has been Updated.
		///// </summary>
		//public event ENS.AttributeUpdatedDelegate<AzManItem> AttributeUpdated;

		public AzManItemAttribute Clone() {
			var _clone = new AzManItemAttribute {
				AttributeId = this.AttributeId,
				Key = this.Key,
				Value = this.Value,
				Owner = this.Owner.Clone()
			};

			return _clone;
		}
	}
}
