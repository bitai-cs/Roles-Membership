using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public partial class Department {
		public Department() {
			DepartmentName = Guid.NewGuid().ToString();
			RowVersion = Encoding.ASCII.GetBytes("0");
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "Id Departamento", GroupName = "mainFrame", Name = "Id Departamento", Order = 1, Prompt = "Ingrese Id Departamento", ShortName = "Id Departamento")]
		public int DepartmentId { get; set; } // int, not null

		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(150, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((150), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "Departamento", GroupName = "mainFrame", Name = "Departamento", Order = 2, Prompt = "Ingrese Nombre Departamento", ShortName = "Departamento")]
		public string DepartmentName { get; set; } // nvarchar(150), not null

		[Timestamp]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "RowVersion", GroupName = "mainFrame", Name = "RowVersion", Order = 3, Prompt = "Ingrese RowVersion", ShortName = "RowVersion")]
		public byte[] RowVersion { get; set; } // timestamp, not null

		#region Overriden methods
		public override string ToString() {
			if (string.IsNullOrEmpty(this.DepartmentName))
				return null;
			else
				return string.Format("{0} ({1})", this.DepartmentName, this.DepartmentId.ToString());
		}
		#endregion

		public Department Clone() {
			return new Department() {
				DepartmentId = this.DepartmentId,
				DepartmentName = this.DepartmentName,
				RowVersion = (byte[])this.RowVersion.Clone()
			};
		}
	}
}
