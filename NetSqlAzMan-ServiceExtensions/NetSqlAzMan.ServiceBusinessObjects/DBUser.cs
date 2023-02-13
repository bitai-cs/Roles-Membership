using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public partial class DBUser {
		public DBUser() {
			var _dummy = new Guid().ToString();

			Password = _dummy;
			FullName = _dummy;
			Enabled = true;
			RowVersion = Encoding.Unicode.GetBytes("0");
			this.UserBranchOffices = new ListOfSBO<BranchOffice>();

			//Change Password dummy values			
			CurrentPassword = _dummy;
			NewPassword = _dummy;
			PasswordConfirmation = _dummy;
		}


		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "ID del Usuario", GroupName = "mainFrame", Name = "ID Usuario", Order = 1, Prompt = "Ingrese ID Usuario", ShortName = "ID")]
		public int UserID { get; set; } // int, not null


		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(255, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((255), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Usuario del sistema", GroupName = "mainFrame", Name = "Usuario", Order = 2, Prompt = "Ingrese Usuario", ShortName = "Usuario")]
		public string UserName { get; set; } // nvarchar(255), not null


		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description =
			"Contraseña", GroupName = "mainFrame", Name = "Contraseña", Order = 4, Prompt = "Ingrese Contraseña", ShortName = "Contraseña")]
		[MinLength(4, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		public string Password { get; set; } // nvarchar(2048), not null


		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(150, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((150), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Nombre del usuario", GroupName = "mainFrame", Name = "Nombre", Order = 5, Prompt = "Ingrese nombre del usuario", ShortName = "Nombre")]
		public string FirstName { get; set; } // nvarchar(150), not null


		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(150, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((150), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Apellido del usuario", GroupName = "mainFrame", Name = "Apellido", Order = 6, Prompt = "Ingrese Apellido del usuario", ShortName = "Apellido")]
		public string LastName { get; set; } // nvarchar(150), not null


		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[StringLength(301, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 0)]
		[MinLength(1, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[MaxLength((301), ErrorMessage = "{0} debe tener como máximo una longitud de {1}")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "Nombre completo", GroupName = "mainFrame", Name = "Nombre completo", Order = 7, Prompt = "Ingrese Nombre completo", ShortName = "Nombre completo")]
		public string FullName { get; set; } // nvarchar(301), not null


		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Correo electrónico", GroupName = "mainFrame", Name = "e-m@il", Order = 8, Prompt = "Ingrese e-m@il", ShortName = "e-m@il")]
		[MinLength(6, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		[EmailAddress(ErrorMessage = "Debe de ingresar una dirección de correo válida.")]
		public string Mail { get; set; } // nvarchar(255), null


		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Habilitado", GroupName = "mainFrame", Name = "Habilitado", Order = 10, Prompt = "Ingrese estado", ShortName = "Habilitado")]
		public bool Enabled { get; set; } // bit, not null


		[Timestamp]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description = "RowVersion", GroupName = "mainFrame", Name = "RowVersion", Order = 11, Prompt = "Ingrese RowVersion", ShortName = "RowVersion")]
		public byte[] RowVersion { get; set; } // timestamp, not null


		// dbo.identity_User.DepartmentId -> dbo.identity_Department.DepartmentId (FK_Users_Department)
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Departamento", GroupName = "mainFrame", Name = "Departamento", Order = 10, Prompt = "Ingrese Departamento", ShortName = "Departamento")]
		public Department Department { get; set; }


		// dbo.BranchOffice.UserID -> dbo.identity_User.UserID (FK_User_BranchOffice_User)
		[Display(AutoGenerateField = false, AutoGenerateFilter = true, Description = "Sucursales Relacionadas", GroupName = "mainFrame", Name = "Sucursales Relacionadas", Order = 10, Prompt = "Ingrese Sucursales Relacionadas", ShortName = "Sucursales Relacionadas")]
		//public virtual ICollection<BranchOffice> UserBranchOffices { get; set; }
		public virtual ListOfSBO<BranchOffice> UserBranchOffices { get; set; }


		public AzManStorage Storage { get; set; }


		#region Change Password Properties
		//Solo para transportar la clave cuando se está registrando un usuario
		[Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese su contraseña actual.")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description =
			"Contraseña actual", GroupName = "mainFrame", Name = "Contraseña actual", Order = 4, Prompt = "Ingrese contraseña actual.", ShortName = "Contraseña Actual")]
		[MinLength(4, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		public string CurrentPassword { get; set; } // nvarchar(2048), not null


		//Solo para transportar la clave cuando se está registrando un usuario
		[Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese su contraseña actual.")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description =
			"Contraseña actual", GroupName = "mainFrame", Name = "Contraseña actual", Order = 4, Prompt = "Ingrese contraseña actual.", ShortName = "Contraseña Actual")]
		[MinLength(4, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		public string NewPassword { get; set; } // nvarchar(2048), not null


		//Solo para transportar la clave cuando se está registrando un usuario
		[Required(AllowEmptyStrings = false, ErrorMessage = "Confirmación de la contraseña no puede ser nulo.")]
		[Display(AutoGenerateField = true, AutoGenerateFilter = true, Description =
			"Confirmación Contraseña", GroupName = "mainFrame", Name = "Confirmación", Order = 4, Prompt = "Ingrese Confirmación", ShortName = "Conf. Contraseña")]
		[MinLength(4, ErrorMessage = "{0} debe tener como mínimo una longitud de {1}")]
		public string PasswordConfirmation { get; set; } // nvarchar(2048), not null
		#endregion

		#region Overriden methods
		public override string ToString() {
			if (string.IsNullOrEmpty(this.UserName))
				return null;
			else
				return string.Format("{0} ({1})", this.UserName, this.UserID.ToString());
		}
		#endregion

		#region Public methods
		public DBUser Clone() {
			DBUser _userTemp = new ServiceBusinessObjects.DBUser() {
				UserID = this.UserID,
				UserName = this.UserName,
				FirstName = this.FirstName,
				LastName = this.LastName,
				Enabled = this.Enabled,
				FullName = this.FullName,
				Mail = this.Mail,
				Password = this.Password,
				RowVersion = this.RowVersion,
				Storage = this.Storage == null ? null : Storage.Clone(),
				Department = this.Department == null ? null : this.Department.Clone()
			};

			foreach (var _e in this.UserBranchOffices) {
				_userTemp.UserBranchOffices.Add(new ServiceBusinessObjects.BranchOffice() {
					BranchOfficeId = _e.BranchOfficeId,
					BranchOfficeName = _e.BranchOfficeName,
					RelativeBranchOfficeId = _e.RelativeBranchOfficeId,
					RowVersion = _e.RowVersion
				});
			}

			return _userTemp;
		}

		public void Copy(ref DBUser target) {
			target.UserID = this.UserID;
			target.UserName = this.UserName;
			target.FirstName = this.FirstName;
			target.LastName = this.LastName;
			target.Enabled = this.Enabled;
			target.FullName = this.FullName;
			target.Mail = this.Mail;
			target.Password = this.Password;
			target.RowVersion = this.RowVersion;

			if (this.Department != null) {
				if (target.Department == null)
					target.Department = new ServiceBusinessObjects.Department() {
						DepartmentId = this.Department.DepartmentId,
						DepartmentName = this.Department.DepartmentName,
						RowVersion = this.RowVersion
					};
				else {
					target.Department.DepartmentId = this.Department.DepartmentId;
					target.Department.DepartmentName = this.Department.DepartmentName;
					target.Department.RowVersion = this.RowVersion;
				}
			}
			else
				target.Department = null;

			target.UserBranchOffices.Clear();
			foreach (var _e in this.UserBranchOffices) {
				target.UserBranchOffices.Add(new ServiceBusinessObjects.BranchOffice() {
					BranchOfficeId = _e.BranchOfficeId,
					BranchOfficeName = _e.BranchOfficeName,
					RelativeBranchOfficeId = _e.RelativeBranchOfficeId,
					RowVersion = _e.RowVersion
				});
			}
		}

		public string[] GetArrayOfBranchOfficeIds() {
			if (this.UserBranchOffices.Count.Equals(0))
				return new string[] { };
			else {
				var _ids = new string[this.UserBranchOffices.Count];
				var _i = 0;
				foreach (var _bo in this.UserBranchOffices) {
					_ids[_i] = _bo.BranchOfficeId;
					_i++;
				}
				return _ids;
			}
		}
		#endregion
	}
}