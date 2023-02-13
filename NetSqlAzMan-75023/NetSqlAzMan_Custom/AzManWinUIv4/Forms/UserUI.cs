using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace NetSqlAzMan.SnapIn.Forms
{
	public enum DataActionsEnum : short
	{
		Unknown = 0,
		New = 1,
		Edit = 2,
		Delete = 3,
		Read = 4,
		Activation = 5,
		ChangePassword = 6
	}

	public partial class UserUI : KryptonForm
	{
		#region Private fields
		private readonly string _webApiUri = null;
		private Exception _hex = null;
		private Boolean _closeoOnSave = true;
		private DataActionsEnum _dataAction = DataActionsEnum.Unknown;
		private NetSqlAzMan.ServiceBusinessObjects.DBUser _dbUserTemp = null;
		private NetSqlAzMan.ServiceBusinessObjects.DBUser _dbUserRef = null;
		#endregion

		#region Public event

		public event EventHandler<EventArgs> RecordSaved;

		#endregion

		#region Constructor

		public UserUI(string webApiUri, Boolean closeOnSave)
		{
			InitializeComponent();

			_webApiUri = webApiUri;
			_closeoOnSave = closeOnSave;

			this.loadBranchOffices();
			this.loadDepartments();
		}

		#endregion

		#region Properties
		public NetSqlAzMan.ServiceBusinessObjects.DBUser UserRecord {
			get {
				return _dbUserRef;
			}
			set {
				_dbUserRef = value;
			}
		}
		#endregion

		#region Private methods
		private void OnRecordSaved()
		{
			if (this.RecordSaved != null)
				this.RecordSaved(this, new EventArgs());
		}

		private void loadBranchOffices()
		{
			IEnumerable<ServiceBusinessObjects.BranchOffice> _list = null;
			var _h = new AzManWebApiClientHelpers.BranchOfficesHelper<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>(_webApiUri);
			var _return = Task.Run(() => _h.GetListAsync()).Result;
			if (_h.IsResponseContentError(_return))
			{
				_h.ThrowWebApiRequestException(_return);
			}
			else
			{
				_list = _h.GetEnumerableSBOFromReturnedContent(_return);
			}
			//if (_return.ContainsKey("error"))
			//{
			//	var _values = _return["error"].ToArray();
			//	var _requestUri = _values[0].ToString();
			//	var _respMsg = (HttpResponseMessage)_values[1];
			//	//Disparar error
			//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			//}
			//else
			//{
			//	var _values = _return["list"].ToArray();
			//	_list = (IEnumerable<NetSqlAzMan.ServiceBusinessObjects.BranchOffice>)_values[0];
			//}

			lstbBranchOffices.ValueMember = "BranchOfficeId";
			lstbBranchOffices.DisplayMember = "BranchOfficeName";
			lstbBranchOffices.DataSource = _list;
			lstbBranchOffices.SelectedIndex = -1;
		}

		private void loadDepartments()
		{
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.Department> _list = null;

			var _h = new AzManWebApiClientHelpers.DepartmentsHelper<NetSqlAzMan.ServiceBusinessObjects.Department>(_webApiUri);
			var _return = Task.Run(() => _h.GetListAsync()).Result;
			if (_h.IsResponseContentError(_return))
			{
				throw _h.GetWebApiRequestException(_return);
			}
			else
			{
				_list = _h.GetEnumerableSBOFromReturnedContent(_return);
			}
			//if (_return.ContainsKey("error"))
			//{
			//	var _values = _return["error"].ToArray();
			//	var _requestUri = _values[0].ToString();
			//	var _respMsg = (HttpResponseMessage)_values[1];
			//	//Disparar error
			//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			//}
			//else
			//{
			//	var _values = _return["list"].ToArray();
			//	_list = (IEnumerable<NetSqlAzMan.ServiceBusinessObjects.Department>)_values[0];	
			//}

			cmbbDepartment.ValueMember = "DepartmentId";
			cmbbDepartment.DisplayMember = "DepartmentName";
			cmbbDepartment.DataSource = _list;
			cmbbDepartment.SelectedIndex = -1;
		}

		#endregion

		#region Public methods

		public bool NewRecord(out Exception hex)
		{
			hex = null;

			try
			{
				//if (_closeoOnSave)
				//	if (_dbUserRef == null)
				//		throw new ArgumentException("Debe de establecer primero la propiedad UserRecord del Form.");

				//Establecer la accion de datos
				_dataAction = DataActionsEnum.New; //New

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Inicializar la estructura de datos
				_dbUserTemp = new ServiceBusinessObjects.DBUser();

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

				//Ocultar controles
				lablPassword.Visible = txtbPassword.Visible = true;
				lablConfirmation.Visible = txtbConfirmation.Visible = true;
				chkbEnabled.Visible = false;

				txtbUserName.Focus();

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		public bool EditRecord(out Exception hex)
		{
			hex = null;

			try
			{
				if (_dbUserRef == null)
					throw new ArgumentException("Debe de establecer primero la propiedad UserRecord del Form.");

				_dataAction = DataActionsEnum.Edit;

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Copiar info del usuario en otra variable
				_dbUserTemp = _dbUserRef.Clone();

				//Mostrar datos
				txtbUserName.Text = _dbUserTemp.UserName;
				txtbPassword.Text = _dbUserTemp.Password;
				txtbFirstName.Text = _dbUserTemp.FirstName;
				txtbLastName.Text = _dbUserTemp.LastName;
				txtbMail.Text = _dbUserTemp.Mail;

				foreach (var _bo in _dbUserTemp.UserBranchOffices)
				{
					lstbBranchOffices.SelectedValue = _bo.BranchOfficeId;
				}

				if (_dbUserTemp.Department != null)
					cmbbDepartment.SelectedValue = _dbUserTemp.Department.DepartmentId;

				chkbEnabled.Checked = _dbUserTemp.Enabled;

				//Estado de los controles
				txtbUserName.Enabled = false;
				txtbFirstName.Enabled = true;
				txtbLastName.Enabled = true;
				txtbMail.Enabled = true;
				lstbBranchOffices.Enabled = true;
				cmbbDepartment.Enabled = true;

				//Ocultar controles
				lablPassword.Visible = txtbPassword.Visible = false;
				lablConfirmation.Visible = txtbConfirmation.Visible = false;
				chkbEnabled.Enabled = !(chkbEnabled.Visible = true);

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		public bool ChangeStatus(bool status, out Exception hex)
		{
			hex = null;

			try
			{
				if (_dbUserRef == null)
					throw new ArgumentException("Debe de establecer primero la propiedad UserRecord del Form.");

				_dataAction = DataActionsEnum.Activation;

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Copiar info del usuario en otra variable
				_dbUserTemp = _dbUserRef.Clone();

				//Marcamos el nuevo estado
				_dbUserTemp.Enabled = status;

				//Mostrar datos
				txtbUserName.Text = _dbUserTemp.UserName;
				txtbPassword.Text = _dbUserTemp.Password;
				txtbFirstName.Text = _dbUserTemp.FirstName;
				txtbLastName.Text = _dbUserTemp.LastName;
				txtbMail.Text = _dbUserTemp.Mail;

				foreach (var _bo in _dbUserTemp.UserBranchOffices)
				{
					lstbBranchOffices.SelectedValue = _bo.BranchOfficeId;
				}

				if (_dbUserTemp.Department != null)
					cmbbDepartment.SelectedValue = _dbUserTemp.Department.DepartmentId;

				chkbEnabled.Checked = _dbUserTemp.Enabled;

				//Estado de los controles
				txtbUserName.Enabled = false;
				txtbFirstName.Enabled = false;
				txtbLastName.Enabled = false;
				txtbMail.Enabled = false;
				lstbBranchOffices.Enabled = false;
				cmbbDepartment.Enabled = false;

				//Ocultar controles
				lablPassword.Visible = txtbPassword.Visible = false;
				lablConfirmation.Visible = txtbConfirmation.Visible = false;
				chkbEnabled.Enabled = !(chkbEnabled.Visible = true);

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		public bool ChangePassword(out Exception hex)
		{
			hex = null;

			try
			{
				if (_dbUserRef == null)
					throw new ArgumentException("Debe de establecer primero la propiedad UserRecord del Form.");

				_dataAction = DataActionsEnum.ChangePassword;

				//Recargar Surcursales y Departamentos
				this.loadBranchOffices();
				this.loadDepartments();

				//Copiar info del usuario en otra variable
				_dbUserTemp = _dbUserRef.Clone();

				//Mostrar datos
				txtbUserName.Text = _dbUserTemp.UserName;
				txtbFirstName.Text = _dbUserTemp.FirstName;
				txtbLastName.Text = _dbUserTemp.LastName;
				txtbMail.Text = _dbUserTemp.Mail;
				txtbPassword.Text = string.Empty;
				txtbConfirmation.Text = string.Empty;

				foreach (var _bo in _dbUserTemp.UserBranchOffices)
				{
					lstbBranchOffices.SelectedValue = _bo.BranchOfficeId;
				}

				if (_dbUserTemp.Department != null)
					cmbbDepartment.SelectedValue = _dbUserTemp.Department.DepartmentId;

				chkbEnabled.Checked = _dbUserTemp.Enabled;

				//Estado de los controles
				txtbUserName.Enabled = false;
				txtbFirstName.Enabled = false;
				txtbLastName.Enabled = false;
				txtbMail.Enabled = false;
				lstbBranchOffices.Enabled = false;
				cmbbDepartment.Enabled = false;

				//Ocultar controles
				lablPassword.Visible = txtbPassword.Visible = true;
				lablConfirmation.Visible = txtbConfirmation.Visible = true;
				chkbEnabled.Visible = false;

				txtbConfirmation.Focus();

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		public bool Save(out Exception hex)
		{
			hex = null;

			try
			{
				_dbUserTemp.UserName = txtbUserName.Text;
				_dbUserTemp.Password = txtbPassword.Text;
				_dbUserTemp.FirstName = txtbFirstName.Text;
				_dbUserTemp.LastName = txtbLastName.Text;
				_dbUserTemp.Mail = string.IsNullOrEmpty(txtbMail.Text.Trim()) ? null : txtbMail.Text;

				_dbUserTemp.UserBranchOffices.Clear();
				foreach (ServiceBusinessObjects.BranchOffice _bo in lstbBranchOffices.SelectedItems)
				{
					_dbUserTemp.UserBranchOffices.Add(_bo);
				}

				if (cmbbDepartment.SelectedValue == null)
					_dbUserTemp.Department = null;
				else
					_dbUserTemp.Department = (ServiceBusinessObjects.Department)cmbbDepartment.SelectedItem;

				_dbUserTemp.Enabled = chkbEnabled.Checked;

				var _h = new AzManWebApiClientHelpers.DBUsersHelper<NetSqlAzMan.ServiceBusinessObjects.DBUser>(_webApiUri);
				Dictionary<string, IEnumerable<object>> _return = null;
				switch (_dataAction)
				{
					case DataActionsEnum.New:
						#region Call WebApi						
						_return = Task.Run(() => _h.PostAsync(_dbUserTemp)).Result;
						if (_h.IsResponseContentError(_return))
						{
							throw _h.GetWebApiRequestException(_return);
						}
						else
						{
							_dbUserTemp = _h.GetSBOFromReturnedContent(_return);
						}
						//if (_return.ContainsKey("error"))
						//{
						//	var _values = _return["error"].ToArray();
						//	var _requestUri = _values[0].ToString();
						//	var _respMsg = (HttpResponseMessage)_values[1];
						//	//Disparar error
						//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
						//}
						//else
						//{
						//	var _values = _return["data"].ToArray();
						//	_dbUserTemp = (NetSqlAzMan.ServiceBusinessObjects.DBUser)_values[0];
						//}
						#endregion

						if (!_closeoOnSave)
							if (!this.NewRecord(out hex))
								throw hex;

						break;

					case DataActionsEnum.Edit:
						#region Call WebApi
						_return = Task.Run(() => _h.PutAsync(_dbUserTemp.UserID, _dbUserTemp)).Result;
						if (_h.IsResponseContentError(_return))
						{
							throw _h.GetWebApiRequestException(_return);
						}
						else
						{
							_dbUserTemp = _h.GetSBOFromReturnedContent(_return);
						}
						//if (_return.ContainsKey("error"))
						//{
						//	var _values = _return["error"].ToArray();
						//	var _requestUri = _values[0].ToString();
						//	var _respMsg = (HttpResponseMessage)_values[1];
						//	//Disparar error
						//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
						//}
						//else
						//{
						//	var _values = _return["data"].ToArray();
						//	_dbUserTemp = (NetSqlAzMan.ServiceBusinessObjects.DBUser)_values[0];
						//}
						#endregion

						break;

					case DataActionsEnum.ChangePassword:
						_dbUserTemp.PasswordConfirmation = txtbConfirmation.Text;

						#region Call WebApi
						_return = Task.Run(() => _h.PutAsync(_dbUserTemp.UserID, _dbUserTemp)).Result;
						if (_h.IsResponseContentError(_return))
						{
							throw _h.GetWebApiRequestException(_return);
						}
						else
						{
							_dbUserTemp = _h.GetSBOFromReturnedContent(_return);
						}
						//if (_return.ContainsKey("error"))
						//{
						//	var _values = _return["error"].ToArray();
						//	var _requestUri = _values[0].ToString();
						//	var _respMsg = (HttpResponseMessage)_values[1];
						//	//Disparar error
						//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
						//}
						//else
						//{
						//	var _values = _return["data"].ToArray();
						//	_dbUserTemp = (NetSqlAzMan.ServiceBusinessObjects.DBUser)_values[0];
						//}
						#endregion

						break;

					case DataActionsEnum.Activation: //Activation
						#region Call WebApi
						_return = Task.Run(() => _h.PutAsync(_dbUserTemp.UserID, _dbUserTemp)).Result;
						if (_h.IsResponseContentError(_return))
						{
							throw _h.GetWebApiRequestException(_return);
						}
						else
						{
							_dbUserTemp = _h.GetSBOFromReturnedContent(_return);
						}
						//if (_return.ContainsKey("error"))
						//{
						//	var _values = _return["error"].ToArray();
						//	var _requestUri = _values[0].ToString();
						//	var _respMsg = (HttpResponseMessage)_values[1];
						//	//Disparar error
						//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
						//}
						//else
						//{
						//	var _values = _return["data"].ToArray();
						//	_dbUserTemp = (NetSqlAzMan.ServiceBusinessObjects.DBUser)_values[0];
						//}
						#endregion

						break;

					default:
						throw new InvalidOperationException("La acción no está implementada.");
				}

				//Copiar datos
				if (_dbUserRef != null)
					_dbUserTemp.Copy(ref _dbUserRef);

				this.OnRecordSaved();

				return true;
			}
			catch (Exception ex)
			{
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Event handlers

		private void butnOk_Click(object sender, EventArgs e)
		{
			DialogResult enumResult = MessageBox.Show("Desea confirmar la acción?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (enumResult == System.Windows.Forms.DialogResult.No)
				return;

			if (this.Save(out _hex))
			{
				if (!_closeoOnSave)
					this.DialogResult = System.Windows.Forms.DialogResult.None;
				else
					MessageBox.Show("Proceso completado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				this.DialogResult = System.Windows.Forms.DialogResult.None;

				throw _hex;
			}
		}

		private void butnCancel_Click(object sender, EventArgs e)
		{

		}

		private void cmbbDepartment_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				cmbbDepartment.SelectedIndex = -1;
		}
		#endregion
	}
}