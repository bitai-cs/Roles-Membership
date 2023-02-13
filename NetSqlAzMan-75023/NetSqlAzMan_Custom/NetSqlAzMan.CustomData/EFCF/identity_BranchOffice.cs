namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class identity_BranchOffice {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public identity_BranchOffice() {
			identity_BranchOffice1 = new HashSet<identity_BranchOffice>();
			identity_UserBranchOffice = new HashSet<identity_UserBranchOffice>();
		}

		[Key]
		[StringLength(2)]
		public string BranchOfficeId { get; set; }

		[Required]
		[StringLength(150)]
		[Index(IsUnique = true)]
		public string BranchOfficeName { get; set; }

		[Required]
		[StringLength(2)]
		[Index(IsUnique = false)]
		public string RelativeBranchOfficeId { get; set; }

		[Column(TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<identity_BranchOffice> identity_BranchOffice1 { get; set; }

		public virtual identity_BranchOffice identity_BranchOffice2 { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<identity_UserBranchOffice> identity_UserBranchOffice { get; set; }
	}
}
