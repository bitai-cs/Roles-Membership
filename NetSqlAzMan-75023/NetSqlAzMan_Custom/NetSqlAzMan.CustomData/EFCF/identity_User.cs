namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class identity_User {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public identity_User() {
			Password = "*****";
			identity_UserValidationRequest = new HashSet<identity_UserValidationRequest>();
			identity_UserBranchOffice = new HashSet<identity_UserBranchOffice>();
		}

		[Key]
		public int UserID { get; set; }

		[Required]
		[StringLength(255)]
		[Index(IsUnique = true)]
		public string UserName { get; set; }

		//[Required]
		//[MaxLength(50)]
		//public byte[] Password { get; set; }

		[Required]
		[StringLength(2048)]
		public string Password { get; set; }

		[Required]
		[StringLength(150)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(150)]
		public string LastName { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[Required]
		[StringLength(301)]
		public string FullName { get; set; }

		[StringLength(255)]
		public string Mail { get; set; }

		public int? DepartmentId { get; set; }

		public bool Enabled { get; set; }

		[Column(TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }

		public virtual identity_Department identity_Department { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<identity_UserValidationRequest> identity_UserValidationRequest { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<identity_UserBranchOffice> identity_UserBranchOffice { get; set; }
	}
}
