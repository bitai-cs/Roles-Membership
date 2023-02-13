namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	//[NotMapped()]
	public partial class identity_vw_WrongItemsHierarchyView {
		[Key]
		[Column(Order = 0)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ChildId { get; set; }

		[Key]
		[Column(Order = 1)]
		public byte ChildItemType { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(255)]
		public string ChildName { get; set; }

		[Key]
		[Column(Order = 3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ParentId { get; set; }

		[Key]
		[Column(Order = 4)]
		public byte ParentItemType { get; set; }

		[Key]
		[Column(Order = 5)]
		[StringLength(255)]
		public string ParentName { get; set; }
	}
}