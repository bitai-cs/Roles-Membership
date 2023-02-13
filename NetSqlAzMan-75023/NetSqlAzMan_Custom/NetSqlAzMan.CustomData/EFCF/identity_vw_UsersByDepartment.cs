namespace NetSqlAzMan.CustomDataLayer.EFCF {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	//[NotMapped()]
	public partial class identity_vw_UsersByDepartment {
		[Key]
		[Column(Order = 0)]
		[StringLength(150)]
		public string Departamento { get; set; }

		[Key]
		[Column(Order = 1)]
		[StringLength(301)]
		public string Nombre { get; set; }

		[Key]
		[Column(Order = 2)]
		[StringLength(255)]
		public string Usuario { get; set; }

		[StringLength(50)]
		public string Clave { get; set; }

		[Column("Sede(s)")]
		public string Sede_s_ { get; set; }
	}
}
