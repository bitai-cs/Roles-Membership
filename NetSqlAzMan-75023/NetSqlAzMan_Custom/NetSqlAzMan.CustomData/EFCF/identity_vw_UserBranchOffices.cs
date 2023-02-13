namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	//[NotMapped()]
	public partial class identity_vw_UserBranchOffices {
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserID { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(2)]
		public string BranchOfficeId { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(150)]
		public string BranchOfficeName { get; set; }

		[Key]
		[Column(Order = 3)]
		[StringLength(2)]
		public string RelativeBranchOfficeId { get; set; }
	}
}
