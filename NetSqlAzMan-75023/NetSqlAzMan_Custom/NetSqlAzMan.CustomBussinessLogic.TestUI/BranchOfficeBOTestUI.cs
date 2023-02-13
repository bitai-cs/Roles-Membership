using Microsoft.VisualBasic;
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
	public partial class BranchOfficeBOTestUI : Form {
		public BranchOfficeBOTestUI() {
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e) {
			BranchOfficeBusinessFactory _bo = new BranchOfficeBusinessFactory();
			var _l = await Task.Run(() => _bo.GetAllAsync());
			branchOfficeBindingSource.DataSource = _l;
		}

		private void button2_Click(object sender, EventArgs e) {
			BranchOfficeBusinessFactory _bo = new BranchOfficeBusinessFactory();
			var _l = Task.Run(() => _bo.GetAllByUserIdAsync(Convert.ToInt32(textBox1.Text)));
			bindingSource1.DataSource = _l.Result;
		}

		private ServiceBusinessObjects.BranchOffice _created = null;
		private async void button3_Click(object sender, EventArgs e) {
			try {
				var _newId = Interaction.InputBox("Ingrese el Id del nuevo BranchOffice", this.Text);
				if (_newId.Trim().Equals(string.Empty))
					throw new InvalidEnumArgumentException("Debe de ingresar el nuevo Id del BranchOffice.");

				var _bso = new ServiceBusinessObjects.BranchOffice() {
					BranchOfficeId = _newId,
					BranchOfficeName = "BranchOffice" + _newId,
					RelativeBranchOfficeId = _newId
				};

				var _bo = new BranchOfficeBusinessFactory();
				_created = await _bo.RegisterAsync(_bso);

				MessageBox.Show("BronchOffice registrado con rowversion: " + Convert.ToBase64String(_created.RowVersion));
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private ServiceBusinessObjects.BranchOffice _edited = null;
		private async void button4_Click(object sender, EventArgs e) {
			try {
				if (dgvwAll.SelectedRows.Count.Equals(0))
					throw new InvalidOperationException("Debe de seleccionar el BranchOffice a modificar");

				var _row = dgvwAll.SelectedRows[0];
				_edited = ((ServiceBusinessObjects.BranchOffice)_row.DataBoundItem).Clone();

				var _newName = Interaction.InputBox("Ingrese el nuevo nombre para el BranchOffice.", this.Text, "Nuevo nombre para " + _edited.BranchOfficeName);

				if (_newName.Trim().Equals(string.Empty))
					throw new ArgumentException("Debe de ingresar el nuevo nombre para el BranchOffice");

				//Edit data				
				_edited.BranchOfficeName = _newName;

				var _bl = new BranchOfficeBusinessFactory();
				var _updated = await _bl.UpdateAsync(_edited);

				MessageBox.Show(string.Format("BronchOffice actualizado. Old RowVersion: {0} / New RowVersion: {1}", Convert.ToBase64String(_edited.RowVersion), Convert.ToBase64String(_updated.RowVersion)));
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}

		private ServiceBusinessObjects.BranchOffice _deleted = null;
		private async void button5_Click(object sender, EventArgs e) {
			try {
				if (dgvwAll.SelectedRows.Count.Equals(0))
					throw new InvalidOperationException("Debe de seleccionar el BranchOffice a modificar");

				var _row = dgvwAll.SelectedRows[0];
				_deleted = ((ServiceBusinessObjects.BranchOffice)_row.DataBoundItem).Clone();

				if (MessageBox.Show(this, string.Format("¿Desea eliminar el BranchOffice \"{0}\"?", _deleted.BranchOfficeName), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
					return;

				var _bl = new BranchOfficeBusinessFactory();
				_deleted = await _bl.DeleteAsync(_deleted);

				MessageBox.Show(string.Format("BronchOffice eliminado: Id:{0} | Name:{1} | RowVersion: {2}", _deleted.BranchOfficeId, _deleted.BranchOfficeName, Convert.ToBase64String(_deleted.RowVersion)));
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
		}
	}
}
