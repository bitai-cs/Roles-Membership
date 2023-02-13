using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	public partial class DepartmentBOTestUI : Form {
		public DepartmentBOTestUI() {
			InitializeComponent();
		}

		private void showErrors(Exception ex) {
			Exception _curr = ex;
			do {
				MessageBox.Show(this, string.Format("{0}\n\r{1}", _curr.Message, _curr.StackTrace), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				_curr = _curr.InnerException;
			} while (_curr != null);
		}

		private void showMessage(string message) {
			MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private DialogResult showConfirmation(string message) {
			return MessageBox.Show(this, message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		private async void button1_Click(object sender, EventArgs e) {
			try {
				butnGetAllAsync.Enabled = false;
				txtbDepNameFilter.Enabled = false;

				departmentBindingSource.DataSource = null;

				NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _f = new DepartmentBusinessFactory();
				var _l = Task.Run(() => _f.GetAllAsync("DepartmentName", true, txtbDepNameFilter.Text));

				departmentBindingSource.DataSource = await _l;
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				butnGetAllAsync.Enabled = true;
				txtbDepNameFilter.Enabled = true;
			}
		}

		private async void button2_Click(object sender, EventArgs e) {
			try {
				butnGetByUserId.Enabled = false;
				txtbUserID.Enabled = false;

				departmentBindingSource.DataSource = null;

				NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bo = new DepartmentBusinessFactory();
				var _l = Task.Run(() => _bo.GetByUserIdAsync(Convert.ToInt32(txtbUserID.Text)));

				departmentBindingSource.DataSource = await _l;
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				txtbUserID.Enabled = true;
				butnGetByUserId.Enabled = true;
			}
		}

		private void departmentBindingSource_CurrentItemChanged(object sender, EventArgs e) {

		}

		private async void button3_Click(object sender, EventArgs e) {
			try {
				butnUpdateAsync.Enabled = false;

				DataGridViewRow _row = dataGridView1.SelectedRows[0];
				NetSqlAzMan.ServiceBusinessObjects.Department _dep = (NetSqlAzMan.ServiceBusinessObjects.Department)_row.DataBoundItem;

				CustomBussinessLogic.DepartmentBusinessFactory _bol = new DepartmentBusinessFactory();
				await Task.Run(() => _bol.UpdateAsync(_dep));

				showMessage("Se actualizó el registro.");
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				butnUpdateAsync.Enabled = true;
			}
		}

		private async void butnInsertAsync_Click(object sender, EventArgs e) {
			try {
				butnInsertAsync.Enabled = false;
				txtbNewDepName.Enabled = false;

				NetSqlAzMan.ServiceBusinessObjects.Department _new = new NetSqlAzMan.ServiceBusinessObjects.Department() {
					DepartmentName = txtbNewDepName.Text
				};

				NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bol = new DepartmentBusinessFactory();
				var _t = Task.Run(() => _bol.InsertAsync(_new));

				await _t;

				showMessage("Se generó el nuevo departamento con id=" + _new.DepartmentId.ToString());
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				butnInsertAsync.Enabled = true;
				txtbNewDepName.Enabled = true;
			}
		}

		private async void button5_Click(object sender, EventArgs e) {
			try {
				butnDeleteAsync.Enabled = false;

				DataGridViewRow _row = dataGridView1.SelectedRows[0];
				NetSqlAzMan.ServiceBusinessObjects.Department _dep = (NetSqlAzMan.ServiceBusinessObjects.Department)_row.DataBoundItem;

				if (showConfirmation("¿Desea eliminar el departamento " + _dep.DepartmentName + " seleccionado?") == DialogResult.No)
					return;

				NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bol = new DepartmentBusinessFactory();
				await Task.Run(() => _bol.DeleteAsync(_dep));

				showMessage("Se eliminó el departamento: " + _dep.DepartmentName + " con id: " + _dep.DepartmentId);
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				butnDeleteAsync.Enabled = true;
			}
		}

		private async void butnGetById_Click(object sender, EventArgs e) {
			try {
				butnGetById.Enabled = false;
				txtbId.Enabled = false;

				departmentBindingSource.DataSource = null;

				NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory _bol = new NetSqlAzMan.CustomBussinessLogic.DepartmentBusinessFactory();
				var _t = Task.Run(() => _bol.GetByIdAsync(Convert.ToInt32(txtbId.Text)));

				NetSqlAzMan.ServiceBusinessObjects.Department _ret;
				_ret = await _t;

				departmentBindingSource.DataSource = _ret;

				if (_ret == null)
					showMessage(string.Format("No se encontró el ID: {0}", txtbId.Text));
			}
			catch (Exception ex) {
				showErrors(ex);
			}
			finally {
				butnGetById.Enabled = true;
				txtbId.Enabled = true;
			}
		}

		private void DepartmentBOTestUI_Load(object sender, EventArgs e) {

		}
	}
}