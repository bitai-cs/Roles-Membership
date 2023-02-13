namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class identity_Department {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public identity_Department() {
			identity_User = new HashSet<identity_User>();
		}

		[Key]
		public int DepartmentId { get; set; }

		[Required]
		[StringLength(150)]
		[Index(IsUnique = true)]
		public string DepartmentName { get; set; }

		[Column(TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<identity_User> identity_User { get; set; }
	}
}
