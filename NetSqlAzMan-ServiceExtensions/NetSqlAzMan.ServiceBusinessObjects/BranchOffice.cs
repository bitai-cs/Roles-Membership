using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public partial class BranchOffice {
		public BranchOffice() : base() {
			BranchOfficeId = "??";
			BranchOfficeName = Guid.NewGuid().ToString();
			RowVersion = Encoding.ASCII.GetBytes("0");
			RelativeBranchOfficeId = "??";
		}

		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((2), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "BranchOfficeId", GroupName = "mainFrame", Name = "BranchOfficeId", Order = 1, Prompt = "Ingrese BranchOfficeId", ShortName = "BranchOfficeId")]
		public string BranchOfficeId { get; set; } // varchar(2), not null

		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(150, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((150), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "BranchOfficeName", GroupName = "mainFrame", Name = "BranchOfficeName", Order = 2, Prompt = "Ingrese BranchOfficeName", ShortName = "BranchOfficeName")]
		public string BranchOfficeName { get; set; } // nvarchar(150), not null

		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(2, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((2), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "RelativeBranchOfficeId", GroupName = "mainFrame", Name = "RelativeBranchOfficeId", Order = 3, Prompt = "Ingrese RelativeBranchOfficeId", ShortName = "RelativeBranchOfficeId")]
		public string RelativeBranchOfficeId { get; set; } // varchar(2), not null

		[Timestamp]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "RowVersion", GroupName = "mainFrame", Name = "RowVersion", Order = 4, Prompt = "Ingrese RowVersion", ShortName = "RowVersion")]
		public byte[] RowVersion { get; set; } // timestamp, not null

		public BranchOffice Clone() {
			return new BranchOffice() {
				BranchOfficeId = this.BranchOfficeId,
				BranchOfficeName = this.BranchOfficeName,
				RelativeBranchOfficeId = this.RelativeBranchOfficeId,
				RowVersion = (byte[])this.RowVersion.Clone()
			};
		}

		#region Overriden methods
		public override string ToString() {
			if (string.IsNullOrEmpty(this.BranchOfficeName))
				return null;
			else
				return string.Format("{0} ({1})", this.BranchOfficeName, this.BranchOfficeId.ToString());
		}
		#endregion
	}
}
