using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManApplicationAttribute {
		/// <summary>
		/// Gets the authorization attribute id.
		/// </summary>
		/// <value>The authorization attribute id.</value>		
		[Key]
		[Required]
		public int AttributeId { get; set; }

		/// <summary>
		/// Gets the Owner.
		/// </summary>
		/// <value>The parent.</value>
		public AzManApplication Owner { get; set; }

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
		public event ENS.AttributeDeletedDelegate<AzManApplication> AttributeDeleted;

		///// <summary>
		///// Occurs after an Attribute object has been Updated.
		///// </summary>
		//public event ENS.AttributeUpdatedDelegate<AzManApplication> AttributeUpdated;

		public AzManApplicationAttribute Clone() {
			var _clone = new AzManApplicationAttribute {
				AttributeId = this.AttributeId,
				Key = this.Key,
				Value = this.Value,
				Owner = this.Owner.Clone()
			};

			return _clone;
		}
	}
}
