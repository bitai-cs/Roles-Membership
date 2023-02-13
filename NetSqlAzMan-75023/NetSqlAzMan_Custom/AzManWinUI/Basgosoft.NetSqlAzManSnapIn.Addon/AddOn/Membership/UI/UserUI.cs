using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace NetSqlAzManSnapIn.AddOn.Membership.UI
{
	public partial class UserUI : KryptonForm
	{
		#region Private fields

		private Exception pvexceHandled = null;
		private String pvstriConnectionString = String.Empty;
		private Boolean pvboolCloseOnSave = true;
		private Common.DataActionsEnum pvenumDataAction = Common.DataActionsEnum.Unknown;
		private Objects.UserStruct pvstruUserTemp = new Objects.UserStruct();
		private Objects.UserStruct pvstruUserRef = null;
		private List<Objects.DepartmentStruct> pvlistDepartments = null;
		private List<Objects.BranchOfficeStruct> pvlistBranchOffices = null;

		#endregion

		#region Public event

		public event EventHandler<EventArgs> RecordSaved;

		#endregion

		#region Constructor

		public UserUI(String connection, Boolean closeOnSave) {
			InitializeComponent();

			pvstriConnectionString = connection;
			pvboolCloseOnSave = closeOnSave;

			this.loadBranchOffices();
			this.loadDepartments();
		}

		#endregion

		#region Properties

		public Objects.UserStruct UserRecord {
			get {
				return pvstruUserRef;
			}
			set {
				pvstruUserRef = value;
			}
		}

		#endregion

		#region Private methods

		private void OnRecordSaved() {
			if (this.RecordSaved != null)
				this.RecordSaved(this, new EventArgs());
		}

		private void loadBranchOffices() {
			using (Objects.BranchOffice busoLogic = new Objects.BranchOffice(pvstriConnectionString)) {
				if (!busoLogic.GetBranchOffices(out pvlistBranchOffices, out pvexceHandled))
					throw pvexceHandled;

				lstbBranchOffices.ValueMember = "BranchOfficeId";
				lstbBranchOffices.DisplayMember = "BranchOfficeName";
				lstbBranchOffices.DataSource = pvlistBranchOffices;
			}

			lstbBranchOffices.SelectedIndex = -1;
		}

		private void loadDepartments() {
			using (Objects.Department busoLogic = new Objects.Department(pvstriConnectionString)) {
				if (!busoLogic.GetDepartments(out pvlistDepartments, out pvexceHandled))
					throw pvexceHandled;

				cmbbDepartment.ValueMember = "DepartmentId";
				cmbbDepartment.DisplayMember = "DepartmentName";
				cmbbDepartment.DataSource = pvlistDepartments;
			}
		}

		#endregion

		#region Public methods

		public bool NewRecord(out Exception hex) {
			hex = null;

			try {
				if (pvboolCloseOnSave)
					if (pvstruUserRef == null)
						throw new Common.Exceptions.InvalidDataException("Debe de establecer primero la propiedad UserRecord del Form.");

				//Establecer la accion de datos
				pvenumDataAction = Common.DataActionsEnum.New; //New

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Inicializar la estructura de datos
				pvstruUserTemp = new Objects.UserStruct();

				//inicializar controles
				txtbUserName.Text = String.Empty;
				txtbPassword.Text = String.Empty;
				txtbConfirmation.Text = String.Empty;
				txtbFirstName.Text = String.Empty;
				txtbLastName.Text = String.Empty;
				txtbMail.Text = String.Empty;
				lstbBranchOffices.SelectedIndex = -1;
				cmbbDepartment.SelectedIndex = -1;
				chkbEnabled.Checked = true;

				//Habilitar controles
				txtbUserName.Enabled = true;
				txtbFirstName.Enabled = true;
				txtbLastName.Enabled = true;
				txtbMail.Enabled = true;
				lstbBranchOffices.Enabled = true;
				cmbbDepartment.Enabled = true;
				txtbPassword.Enabled = true;
				txtbConfirmation.Enabled = true;
				chkbEnabled.Enabled = false;

				txtbUserName.Focus();

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool EditRecord(out Exception hex) {
			hex = null;

			try {
				if (pvstruUserRef == null)
					throw new Common.Exceptions.InvalidDataException("Debe de establecer primero la propiedad UserRecord del Form.");

				pvenumDataAction = Common.DataActionsEnum.Edit; //Edit

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Copiar info del usuario en otra variable
				pvstruUserRef.CopyTo(ref pvstruUserTemp);

				//Mostrar datos
				txtbUserName.Text = pvstruUserTemp.UserName;
				txtbPassword.Text = pvstruUserTemp.Password;
				txtbConfirmation.Text = pvstruUserTemp.Password;
				txtbFirstName.Text = pvstruUserTemp.FirstName;
				txtbLastName.Text = pvstruUserTemp.LastName;
				txtbMail.Text = pvstruUserTemp.Mail;

				string[] striIds = pvstruUserTemp.BranchOfficeIds.Split(new char[] { Convert.ToChar(33) }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in striIds) {
					lstbBranchOffices.SelectedValue = s;
				}

				cmbbDepartment.SelectedValue = pvstruUserTemp.DepartmentId;
				chkbEnabled.Checked = pvstruUserTemp.Enabled.Value;

				//Estado de los controles
				txtbUserName.Enabled = false;
				txtbFirstName.Enabled = true;
				txtbLastName.Enabled = true;
				txtbMail.Enabled = true;
				lstbBranchOffices.Enabled = true;
				cmbbDepartment.Enabled = true;
				txtbPassword.Enabled = true;
				txtbConfirmation.Enabled = true;
				chkbEnabled.Enabled = false;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool ChangeRecordStatus(bool status, out Exception hex) {
			hex = null;

			try {
				if (pvstruUserRef == null)
					throw new Common.Exceptions.InvalidDataException("Debe de establecer primero la propiedad UserRecord del Form.");

				pvenumDataAction = Common.DataActionsEnum.Activation;

				//Copiar info del usuario en otra variable
				pvstruUserRef.CopyTo(ref pvstruUserTemp);

				//Marcamos el nuevo estado
				pvstruUserTemp.Enabled = status;

				//Mostrar datos
				txtbUserName.Text = pvstruUserTemp.UserName;
				txtbPassword.Text = pvstruUserTemp.Password;
				txtbConfirmation.Text = pvstruUserTemp.Password;
				txtbFirstName.Text = pvstruUserTemp.FirstName;
				txtbLastName.Text = pvstruUserTemp.LastName;
				txtbMail.Text = pvstruUserTemp.Mail;

				string[] striIds = pvstruUserTemp.BranchOfficeIds.Replace("'", string.Empty).Split(new char[] { Convert.ToChar(33) }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in striIds) {
					lstbBranchOffices.SelectedValue = Convert.ToInt32(s);
				}

				cmbbDepartment.SelectedValue = pvstruUserTemp.DepartmentId;
				chkbEnabled.Checked = pvstruUserTemp.Enabled.Value;

				//Estado de los controles
				txtbUserName.Enabled = false;
				txtbPassword.Enabled = false;
				txtbConfirmation.Enabled = false;
				txtbFirstName.Enabled = false;
				txtbLastName.Enabled = false;
				txtbMail.Enabled = false;
				cmbbDepartment.Enabled = false;
				lstbBranchOffices.Enabled = false;
				chkbEnabled.Enabled = false;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool Save(out Exception hex) {
			hex = null;

			try {
				pvstruUserTemp.UserName = txtbUserName.Text;
				pvstruUserTemp.Password = txtbPassword.Text;
				pvstruUserTemp.PasswordHash = txtbPassword.Text;
				pvstruUserTemp.FirstName = txtbFirstName.Text;
				pvstruUserTemp.LastName = txtbLastName.Text;
				pvstruUserTemp.Mail = txtbMail.Text;

				string striIds = null;
				foreach (Objects.BranchOfficeStruct o in lstbBranchOffices.SelectedItems) {
					striIds = striIds + o.BranchOfficeId + "!";
				}
				if (!string.IsNullOrWhiteSpace(striIds))
					striIds = striIds.Substring(0, striIds.Length - 1);

				pvstruUserTemp.BranchOfficeIds = striIds;
				if (cmbbDepartment.SelectedValue != null)
					pvstruUserTemp.DepartmentId = (int)cmbbDepartment.SelectedValue;
				pvstruUserTemp.Enabled = chkbEnabled.Checked;

				switch (pvenumDataAction) {
					case Common.DataActionsEnum.New: //New			
						using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
							if (!busoLogic.InsertUser(pvstruUserTemp, txtbConfirmation.Text, out pvexceHandled))
								throw pvexceHandled;

						if (pvboolCloseOnSave) {
							using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
								if (!busoLogic.GetUser(pvstruUserTemp.UserId.Value, out pvstruUserTemp, out pvexceHandled))
									throw pvexceHandled;
						}
						else
							if (!this.NewRecord(out pvexceHandled))
								throw pvexceHandled;

						break;

					case Common.DataActionsEnum.Edit: //Edit
						using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
							if (!busoLogic.UpdateUser(pvstruUserTemp, pvstruUserRef, txtbConfirmation.Text, out pvexceHandled))
								throw pvexceHandled;

						using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
							if (!busoLogic.GetUser(pvstruUserTemp.UserId.Value, out pvstruUserTemp, out pvexceHandled))
								throw pvexceHandled;

						break;

					case Common.DataActionsEnum.Activation: //Activation
						using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
							if (!busoLogic.UpdateUser(pvstruUserTemp, pvstruUserRef, txtbConfirmation.Text, out pvexceHandled))
								throw pvexceHandled;

						using (Objects.User busoLogic = new Objects.User(pvstriConnectionString))
							if (!busoLogic.GetUser(pvstruUserTemp.UserId.Value, out pvstruUserTemp, out pvexceHandled))
								throw pvexceHandled;

						break;

					default:
						throw new Common.Exceptions.InvalidDataException("La acción no está implmentada.");
				}

				//Copiar datos
				if (pvstruUserRef != null)
					pvstruUserTemp.CopyTo(ref pvstruUserRef);

				this.OnRecordSaved();

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Event handlers

		private void butnOk_Click(object sender, EventArgs e) {
			DialogResult enumResult = MessageBox.Show("Desea confirmar la acción?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (enumResult == System.Windows.Forms.DialogResult.No)
				return;

			if (this.Save(out pvexceHandled)) {
				if (!pvboolCloseOnSave)
					this.DialogResult = System.Windows.Forms.DialogResult.None;
				else
					MessageBox.Show("Proceso completado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else {
				MessageBox.Show(pvexceHandled.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

				this.DialogResult = System.Windows.Forms.DialogResult.None;
			}
		}

		private void butnCancel_Click(object sender, EventArgs e) {

		}

		#endregion
	}
}