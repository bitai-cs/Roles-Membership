namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	//[NotMapped()]
	public partial class identity_vw_Users {
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserID { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(255)]
		public string UserName { get; set; }

		[StringLength(50)]
		public string Password { get; set; }

		[StringLength(2048)]
		public string PasswordHash { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(150)]
		public string FirstName { get; set; }

		[Key]
		[Column(Order = 3)]
		[StringLength(150)]
		public string LastName { get; set; }

		[Key]
		[Column(Order = 4)]
		[StringLength(301)]
		public string FullName { get; set; }

		[StringLength(255)]
		public string Mail { get; set; }

		public int? DepartmentId { get; set; }

		[StringLength(150)]
		public string DepartmentName { get; set; }

		public string BranchOfficeIds { get; set; }

		public string RelativeBranchOfficeIds { get; set; }

		public string BranchOfficeNames { get; set; }

		[Key]
		[Column(Order = 5)]
		public bool Enabled { get; set; }

		[Key]
		[Column(Order = 6, TypeName = "timestamp")]
		[MaxLength(8)]
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
