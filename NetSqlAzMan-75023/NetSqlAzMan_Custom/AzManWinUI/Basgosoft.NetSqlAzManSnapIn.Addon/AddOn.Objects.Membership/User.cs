using System;
using System.Data;
using NetSqlAzManSnapIn.AddOn.Common.Membership;
using NetSqlAzManSnapIn.AddOn.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace NetSqlAzManSnapIn.AddOn.Membership.Objects
{
	public class UserStruct
	{
		#region Constructor

		public UserStruct()
			: this(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) {
		}

		internal UserStruct(Nullable<Int32> userId, String userName, String password, string passwordHash, String firstName, String lastName, String fullName, String mail, Nullable<Int32> departmentId, String departmentName, string branchOfficeIds, string relativeBranchOfficeIds, string branchOfficeNames, Nullable<Boolean> enabled, Byte[] _rowVersion) {
			pvin32UserId = userId;
			pvstriUserName = userName;
			pvstriPassword = password;
			pvstriPasswordHash = passwordHash;
			pvstriFirstName = firstName;
			pvstriLastName = lastName;
			pvstriFullName = fullName;
			pvstriMail = mail;
			pvin32DepartmentId = departmentId;
			pvstriDepartmentName = departmentName;
			pvstriBranchOfficeIds = branchOfficeIds;
			pvstriRelativeBranchOfficeIds = relativeBranchOfficeIds;
			pvstriBranchOfficeNames = branchOfficeNames;
			pvboolEnabled = enabled;
			pvtimsRowVersion = _rowVersion;
		}

		#endregion

		#region Properties

		private Nullable<Int32> pvin32UserId;
		public Nullable<Int32> UserId {
			get {
				return pvin32UserId;
			}
			internal set {
				pvin32UserId = value;
			}
		}

		private String pvstriUserName;
		public String UserName {
			get {
				return pvstriUserName;
			}
			set {
				pvstriUserName = value;
			}
		}

		private String pvstriFirstName;
		public String FirstName {
			get {
				return pvstriFirstName;
			}
			set {
				pvstriFirstName = value;
			}
		}

		private String pvstriLastName;
		public String LastName {
			get {
				return pvstriLastName;
			}
			set {
				pvstriLastName = value;
			}
		}

		private String pvstriFullName;
		public String FullName {
			get {
				return pvstriFullName;
			}
			internal set {
				pvstriFullName = value;
			}
		}

		private String pvstriMail;
		public String Mail {
			get {
				return pvstriMail;
			}
			set {
				pvstriMail = value;
			}
		}

		private String pvstriPassword;
		public String Password {
			get {
				return pvstriPassword;
			}
			set {
				pvstriPassword = value;
			}
		}

		private String pvstriPasswordHash;
		public String PasswordHash {
			get {
				return pvstriPasswordHash;
			}
			set {
				pvstriPasswordHash = value;
			}
		}

		private Nullable<Int32> pvin32DepartmentId;
		public Nullable<Int32> DepartmentId {
			get {
				return pvin32DepartmentId;
			}
			set {
				pvin32DepartmentId = value;
			}
		}

		private String pvstriDepartmentName;
		public String DepartmentName {
			get {
				return pvstriDepartmentName;
			}
			internal set {
				pvstriDepartmentName = value;
			}
		}

		private string pvstriBranchOfficeIds;
		public string BranchOfficeIds {
			get {
				return pvstriBranchOfficeIds;
			}
			set {
				pvstriBranchOfficeIds = value;
			}
		}

		private string pvstriRelativeBranchOfficeIds;
		public string RelativeBranchOfficeIds {
			get {
				return pvstriRelativeBranchOfficeIds;
			}
			set {
				pvstriRelativeBranchOfficeIds = value;
			}
		}

		private String pvstriBranchOfficeNames;
		public String BranchOfficeNames {
			get {
				return pvstriBranchOfficeNames;
			}
			internal set {
				pvstriBranchOfficeNames = value;
			}
		}

		private Nullable<Boolean> pvboolEnabled;
		public Nullable<Boolean> Enabled {
			get {
				return pvboolEnabled;
			}
			set {
				pvboolEnabled = value;
			}
		}

		private Byte[] pvtimsRowVersion;
		public Byte[] _RowVersion {
			get {
				return pvtimsRowVersion;
			}
			set {
				pvtimsRowVersion = value;
			}
		}

		#endregion

		#region Public methods

		public void CopyTo(ref UserStruct target) {
			target.UserId = this.UserId;
			target.UserName = this.UserName;
			target.Password = this.Password;
			target.FirstName = this.FirstName;
			target.LastName = this.LastName;
			target.FullName = this.FullName;
			target.Mail = this.Mail;
			target.DepartmentId = this.DepartmentId;
			target.DepartmentName = this.DepartmentName;
			target.BranchOfficeIds = this.BranchOfficeIds;
			target.BranchOfficeNames = this.BranchOfficeNames;
			target.Enabled = this.Enabled;
			target._RowVersion = this._RowVersion;
		}

		#endregion
	}

	public class User : NetSqlAzManSnapIn.AddOn.Objects.Base
	{
		#region Private members

		private NetSqlAzManSnapIn.AddOn.Membership.Data.User pvdaccProxyUser;
		private NetSqlAzManSnapIn.AddOn.Membership.Data.UserBranchOffice pvdaccProxyUserBranchOffice;

		#endregion

		#region Constructors

		public User(string connectionString) {
			pvdaccProxyUser = new NetSqlAzManSnapIn.AddOn.Membership.Data.User(connectionString);
			pvdaccProxyUserBranchOffice = new NetSqlAzManSnapIn.AddOn.Membership.Data.UserBranchOffice(connectionString);
		}

		#endregion

		#region Implementations

		public override void Dispose() {
			pvdaccProxyUser.Dispose();
		}

		#endregion

		#region Query members

		public bool GetUsers(out List<Objects.UserStruct> list, out Exception hex) {
			Common.Membership.Models.UserDataSet dastMaster;
			DataSet dastTemp;

			list = new List<UserStruct>();
			hex = null;

			try {
				dastMaster = new Common.Membership.Models.UserDataSet();
				dastTemp = (DataSet)dastMaster;

				if (!pvdaccProxyUser.GetUsers(ref dastTemp, dastMaster.identity_User.TableName, out ptexceHandled))
					throw new Exception("Error al obtener los datos de usuarios.", ptexceHandled);

				foreach (Common.Membership.Models.UserDataSet.identity_UserRow r in dastMaster.identity_User)
					list.Add(new UserStruct(r.UserID, r.UserName, r.Password, r.PasswordHash, r.FirstName, r.LastName, r.FullName, r.Mail, r.DepartmentId, r.DepartmentName, r.BranchOfficeIds, r.RelativeBranchOfficeIds, r.BranchOfficeNames, r.Enabled, r.RecordVersion));

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool GetUser(int userId, out UserStruct user, out Exception hex) {
			Common.Membership.Models.UserDataSet dastMaster;
			DataSet dastTemp;

			user = null;
			hex = null;

			try {
				dastMaster = new Common.Membership.Models.UserDataSet();
				dastTemp = (DataSet)dastMaster;

				if (!pvdaccProxyUser.GetUser(userId, ref dastTemp, dastMaster.identity_User.TableName, out ptexceHandled))
					throw new Exception("Error al obtener los datos del usuario.", ptexceHandled);

				Common.Membership.Models.UserDataSet.identity_UserRow daroUser = dastMaster.identity_User.FindByUserID(userId);

				user = new UserStruct(daroUser.UserID, daroUser.UserName, daroUser.Password, daroUser.PasswordHash, daroUser.FirstName, daroUser.LastName, daroUser.FullName, daroUser.Mail, daroUser.DepartmentId, daroUser.DepartmentName, daroUser.BranchOfficeIds, daroUser.RelativeBranchOfficeIds, daroUser.BranchOfficeNames, daroUser.Enabled, daroUser.RecordVersion);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Insert & Update members

		public bool InsertUser(UserStruct ms, string passwordConfirmation, out Exception hex) {
			hex = null;

			try {
				if (ms.UserId.HasValue)
					throw new Common.Exceptions.InvalidDataException("El id. de usuario ha sido asignado, no se puede registrar los datos.");

				if (String.IsNullOrWhiteSpace(ms.UserName))
					throw new Common.Exceptions.InvalidDataException("Debe de ingresar el usuario.");

				if (String.IsNullOrEmpty(ms.Password))
					throw new Common.Exceptions.InvalidDataException("Debe de ingresar la contraseña del usuario");

				if (String.IsNullOrEmpty(ms.PasswordHash))
					throw new Common.Exceptions.InvalidDataException("No se ha generado el hash del password.");

				if (String.IsNullOrEmpty(passwordConfirmation))
					throw new Common.Exceptions.InvalidDataException("Debe de ingresar la confirmación de contraseña del usuario");

				if (!ms.Password.Equals(passwordConfirmation))
					throw new Common.Exceptions.InvalidDataException("La contraseña y la confirmación de contraseña no coinciden.");

				if (String.IsNullOrWhiteSpace(ms.FirstName))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el nombre del usuario.");

				if (String.IsNullOrWhiteSpace(ms.LastName))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el apellido del usuario.");

				if (String.IsNullOrWhiteSpace(ms.Mail))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el correo del usuario.");

				if (!ms.DepartmentId.HasValue)
					throw new Common.Exceptions.InvalidDataException("Debe de especificar el area o departamento del usuario.");

				if (ms.DepartmentId.Equals(-1))
					throw new Common.Exceptions.InvalidDataException("El departamento (Unknown) no es permitido.");

				if (string.IsNullOrWhiteSpace(ms.BranchOfficeIds))
					throw new Common.Exceptions.InvalidDataException("Debe de especificar al menos una sucursal para el usuario.");

				if (ms.BranchOfficeIds.Contains("-1"))
					throw new Common.Exceptions.InvalidDataException("La sucursal (Unknown) no es permitida. Debe de obviarla.");

				if (!ms.Enabled.HasValue)
					throw new Common.Exceptions.InvalidDataException("Debe de especificar si el usuario estará habilitado o no.");

				Int32? in32UserId;
				if (!pvdaccProxyUser.InsertUser(out in32UserId, ms.UserName, ms.Password, ms.PasswordHash, ms.FirstName, ms.LastName, ms.Mail, ms.DepartmentId.Value, ms.Enabled.Value, out ptexceHandled))
					throw ptexceHandled;

				string[] ids = ms.BranchOfficeIds.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in ids) {
					if (!pvdaccProxyUserBranchOffice.InsertUserBranchOffice(in32UserId.Value, s, out ptexceHandled))
						throw ptexceHandled;
				}

				ms.UserId = in32UserId;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool UpdateUser(UserStruct ms, UserStruct old, string passwordConfirmation, out Exception hex) {
			hex = null;

			try {
				if (!ms.UserId.HasValue)
					throw new Common.Exceptions.InvalidDataException("No se ha asignado el id. del usuario. No se puede realizar esta acción.");

				if (String.IsNullOrWhiteSpace(ms.UserName))
					throw new Common.Exceptions.InvalidDataException("El nombre de usuario esta en blanco. No se puede realizar la transacción.");

				if (String.IsNullOrEmpty(ms.Password))
					throw new Common.Exceptions.InvalidDataException("Debe de ingresar la contraseña del usuario");

				if (String.IsNullOrEmpty(ms.PasswordHash))
					throw new Common.Exceptions.InvalidDataException("No se ha generado el hash del password");

				if (String.IsNullOrEmpty(passwordConfirmation))
					throw new Common.Exceptions.InvalidDataException("Debe de ingresar la confirmación de contraseña del usuario");

				if (!ms.Password.Equals(passwordConfirmation))
					throw new Common.Exceptions.InvalidDataException("La contraseña y la confirmación de contraseña no coinciden.");

				if (String.IsNullOrWhiteSpace(ms.FirstName))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el nombre del usuario.");

				if (String.IsNullOrWhiteSpace(ms.LastName))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el apellido del usuario.");

				if (String.IsNullOrWhiteSpace(ms.Mail))
					throw new Common.Exceptions.InvalidDataException("Debe ingresar el correo del usuario.");

				if (!ms.DepartmentId.HasValue)
					throw new Common.Exceptions.InvalidDataException("Debe de especificar el area o departamento del usuario.");

				if (ms.DepartmentId.Equals(-1))
					throw new Common.Exceptions.InvalidDataException("El departamento (Unknown) no es permitido.");

				if (string.IsNullOrWhiteSpace(ms.BranchOfficeIds))
					throw new Common.Exceptions.InvalidDataException("Debe de especificar al menos una sucursal para el usuario.");

				if (ms.BranchOfficeIds.Contains("-1"))
					throw new Common.Exceptions.InvalidDataException("La sucursal (Unknown) no es permitida. Debe de obviarla.");

				if (!ms.Enabled.HasValue)
					throw new Common.Exceptions.InvalidDataException("Debe de especificar si el usuario estará habilitado o no.");

				bool boolUpdated = false;
				if (pvdaccProxyUser.UpdateUser(ms.UserId.Value, ms.UserName, ms.Password, ms.PasswordHash, ms.FirstName, ms.LastName, ms.Mail, ms.DepartmentId.Value, ms.Enabled.Value, ms._RowVersion, out boolUpdated, out ptexceHandled)) {
					if (!boolUpdated)
						throw new Common.Exceptions.NoRecordAffectedException("No se puede actualizar los datos el usuario ha sido modificado o eliminado por otro proceso.");
				}
				else
					throw ptexceHandled;

				if (!old.BranchOfficeIds.Equals(ms.BranchOfficeIds)) {
					if (!pvdaccProxyUserBranchOffice.DeleteUserBranchOffice(ms.UserId.Value, out boolUpdated, out ptexceHandled))
						throw ptexceHandled;

					string[] ids = ms.BranchOfficeIds.Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string s in ids) {
						if (!pvdaccProxyUserBranchOffice.InsertUserBranchOffice(ms.UserId.Value, s, out ptexceHandled))
							throw ptexceHandled;
					}
				}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		//public bool DisableUser(UserStruct ms, string passwordConfirmation, out Exception hex) {
		//   hex = null;

		//   try {
		//      if (!ms.UserId.HasValue)
		//         throw new Common.Exceptions.InvalidDataException("No se ha asignado el id. del usuario. No se puede realizar esta acción.");

		//      if (String.IsNullOrWhiteSpace(ms.UserName))
		//         throw new Common.Exceptions.InvalidDataException("El nombre de usuario esta en blanco. No se puede realizar la transacción.");

		//      if (String.IsNullOrEmpty(ms.Password))
		//         throw new Common.Exceptions.InvalidDataException("Debe de ingresar la contraseña del usuario");

		//      if (String.IsNullOrEmpty(passwordConfirmation))
		//         throw new Common.Exceptions.InvalidDataException("Debe de ingresar la confirmación de contraseña del usuario");

		//      if (!ms.Password.Equals(passwordConfirmation))
		//         throw new Common.Exceptions.InvalidDataException("La contraseña y la confirmación de contraseña no coinciden.");

		//      if (String.IsNullOrWhiteSpace(ms.FirstName))
		//         throw new Common.Exceptions.InvalidDataException("Debe ingresar el nombre del usuario.");

		//      if (String.IsNullOrWhiteSpace(ms.LastName))
		//         throw new Common.Exceptions.InvalidDataException("Debe ingresar el apellido del usuario.");

		//      bool boolUpdated = false;
		//      if (pvdaccProxy.UpdateUser(ms.UserId.Value, ms.UserName, ms.Password, ms.FirstName, ms.LastName, ms.DepartmentId.Value, ms.BranchOfficeId.Value, ms.Enabled.Value, ms._RowVersion, out boolUpdated, out ptexceHandled)) {
		//         if (!boolUpdated)
		//            throw new Common.Exceptions.NoRecordAffectedException("El usuario no existe en la base de datos. Ha sido modificado o eliminado por otro proceso.");
		//      }
		//      else
		//         throw ptexceHandled;

		//      return true;
		//   }
		//   catch (Exception ex) {
		//      hex = ex;
		//      return false;
		//   }
		//}

		#endregion

		#region Delete members

		public bool DeleteUser(Int32 UserId, out Exception hex) {
			hex = null;

			try {
				bool boolDeleted;
				if (pvdaccProxyUser.DeleteUser(UserId, out boolDeleted, out ptexceHandled)) {
					if (!boolDeleted)
						throw new Common.Exceptions.NoRecordAffectedException("El usuario ya no existe en la base de datos, ha sido eliminado por otro proceso.");
				}
				else
					throw new Exception("Error al eliminar los datos de usuario.", ptexceHandled);

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion
	}
}