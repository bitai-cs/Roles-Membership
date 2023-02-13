using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan;

namespace NetSqlAzMan.SnapIn.Forms {
	public partial class LdapWacDomainProfilesUI : Form {
		private string _webApiUri = null;
		private bool _onSave = false;
		private Nullable<byte> _selectedId = null;

		public LdapWacDomainProfilesUI(string webApiUri) {
			InitializeComponent();

			_webApiUri = webApiUri;
			butnNew.Tag = false;
			_selectedId = null;
		}

		private async Task<IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>> LoadData() {
			var _h = new AzManWebApiClientHelpers.LdapWebApiDomainProfilesHelper<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>(_webApiUri);
			var _hr = await _h.GetAllAsync();
			if (_h.IsResponseContentError(_hr))
				throw _h.GetWebApiRequestException(_hr);

			return _h.GetEnumerableSBOFromReturnedContent(_hr);
		}

		private async Task<ServiceBusinessObjects.LdapDomainProfile> UpdateData() {
			var _modified = new ServiceBusinessObjects.LdapDomainProfile() {
				DomainProfileId = _selectedId.Value,
				DomainProfile = txtbDomainProfile.Text,
				LdapProxyWebApiUri = txtbWebApiUrl.Text,
				LdapServerProfile = txtbWebApiServerProfile.Text,
				DomainName = txtbDomainName.Text,
				Enabled = chkbEnabled.Checked
			};

			var _h = new AzManWebApiClientHelpers.LdapWebApiDomainProfilesHelper<ServiceBusinessObjects.LdapDomainProfile>(_webApiUri);
			var _result = await _h.UpdateAsync(_selectedId.Value, _modified);
			if (_h.IsResponseContentError(_result))
				throw _h.GetWebApiRequestException(_result);
			else
				return _h.GetSBOFromReturnedContent(_result);
		}

		private async Task<ServiceBusinessObjects.LdapDomainProfile> RegisterData() {
			var _new = new ServiceBusinessObjects.LdapDomainProfile() {
				DomainProfile = txtbDomainProfile.Text,
				LdapProxyWebApiUri = txtbWebApiUrl.Text,
				LdapServerProfile = txtbWebApiServerProfile.Text,
				DomainName = txtbDomainName.Text,
				Enabled = chkbEnabled.Checked
			};

			var _h = new AzManWebApiClientHelpers.LdapWebApiDomainProfilesHelper<ServiceBusinessObjects.LdapDomainProfile>(_webApiUri);
			var _result = await _h.RegisterAsync(_new);
			if (_h.IsResponseContentError(_result))
				throw _h.GetWebApiRequestException(_result);
			else
				return _h.GetSBOFromReturnedContent(_result);
		}

		private async Task<ServiceBusinessObjects.LdapDomainProfile> DeleteData(ServiceBusinessObjects.LdapDomainProfile domainProfile) {
			var _h = new AzManWebApiClientHelpers.LdapWebApiDomainProfilesHelper<ServiceBusinessObjects.LdapDomainProfile>(_webApiUri);
			var _return = await _h.DeleteAsync(domainProfile.DomainProfileId, domainProfile);
			if (_h.IsResponseContentError(_return))
				throw _h.GetWebApiRequestException(_return);
			else
				return _h.GetSBOFromReturnedContent(_return);
		}

		private async void LdapWacDomainProfilesUI_Load(object sender, EventArgs e) {
			try {
				panlEdit.Enabled = dgvwDomainProfiles.Enabled = false;

				ldapDomainProfileBindingSource.DataSource = null;

				var _data = await this.LoadData();

				ldapDomainProfileBindingSource.DataSource = _data;

				butnSave.Visible = (_data.Count() > 0);
			}
			catch {
				throw;
			}
			finally {
				panlEdit.Enabled = dgvwDomainProfiles.Enabled = true;
			}
		}

		private void butnNewDomainProfile_Click(object sender, EventArgs e) {
			try {
				if ((bool)butnNew.Tag == false) {
					dgvwDomainProfiles.Enabled = false;

					_selectedId = null;
					txtbDomainProfile.Clear();
					txtbWebApiUrl.Clear();
					txtbWebApiServerProfile.Clear();
					txtbDomainName.Clear();
					chkbEnabled.Checked = true;
					butnNew.Text = "&Cancelar";
					butnNew.Tag = true;
					butnSave.Visible = true;

					txtbDomainProfile.Focus();
				}
				else {
					butnNew.Text = "&Nuevo";
					butnNew.Tag = false;
					butnSave.Visible = (dgvwDomainProfiles.Rows.Count > 0);

					dgvwDomainProfiles.Enabled = true;
					dgvwDomainProfiles_SelectionChanged(dgvwDomainProfiles, null);
				}
			}
			catch {
				throw;
			}
			finally {
			}
		}

		private void dgvwDomainProfiles_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

		}

		private async void butnSave_Click(object sender, EventArgs e) {
			int? _createdIndex = null;
			int? _updatedIndex = null;
			try {
				_onSave = true;

				butnNew.Enabled = butnSave.Enabled = txtbDomainProfile.Enabled = txtbWebApiUrl.Enabled = txtbWebApiServerProfile.Enabled = chkbEnabled.Enabled = dgvwDomainProfiles.Enabled = false;

				if (_selectedId == null) {
					if (AzManWinUI.Program.ShowQuestionMessage(this, this.Text, "¿Desea guardar los datos?") == DialogResult.No)
						return;

					var _created = await Task.Run(() => this.RegisterData());

					ldapDomainProfileBindingSource.Add(_created);
					ldapDomainProfileBindingSource.ResetBindings(false);

					_createdIndex = ldapDomainProfileBindingSource.IndexOf(_created);
				}
				else {
					if (AzManWinUI.Program.ShowQuestionMessage(this, this.Text, "¿Desea actualizar los datos?") == DialogResult.No)
						return;

					var _updated = await Task.Run(() => this.UpdateData());

					_updatedIndex = ldapDomainProfileBindingSource.Position;

					ldapDomainProfileBindingSource.RemoveAt(_updatedIndex.Value);
					ldapDomainProfileBindingSource.Insert(_updatedIndex.Value, _updated);
					ldapDomainProfileBindingSource.Position = _updatedIndex.Value;
					ldapDomainProfileBindingSource.ResetBindings(false);
				}
			}
			catch (Exception ex) {
				throw ex;
			}
			finally {
				if (_selectedId == null) {
					if (_createdIndex != null) {
						butnNew.Enabled = butnSave.Enabled = txtbDomainProfile.Enabled = txtbWebApiUrl.Enabled = txtbWebApiServerProfile.Enabled = chkbEnabled.Enabled = dgvwDomainProfiles.Enabled = true;

						butnNew.Tag = false;
						butnNew.Text = "&Nuevo";

						_onSave = false;
						dgvwDomainProfiles.Rows[_createdIndex.Value].Selected = true;
					}
					else {
						butnNew.Enabled = butnSave.Enabled = txtbDomainProfile.Enabled = txtbWebApiUrl.Enabled = txtbWebApiServerProfile.Enabled = chkbEnabled.Enabled = true;

						//No se registró el nuevo Perfil de Dominio por algún error.
						//Se debe continuar en modo de inserción.
						_onSave = false;
					}
				}
				else {
					butnNew.Enabled = butnSave.Enabled = txtbDomainProfile.Enabled = txtbWebApiUrl.Enabled = txtbWebApiServerProfile.Enabled = chkbEnabled.Enabled = dgvwDomainProfiles.Enabled = true;

					if (_updatedIndex != null) {
						_onSave = false;
						dgvwDomainProfiles.Rows[_updatedIndex.Value].Selected = true;
					}
					else {
						//No se actualizó el Perfil de Dominio por algún error.
						//Se debe continuar en modo de actualización.
						_onSave = false;
					}
				}
			}
		}

		private async void dgvwDomainProfiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
			if (!(e.ColumnIndex.Equals(0) && e.Button == MouseButtons.Left))
				return;

			try {
				panlEdit.Enabled = dgvwDomainProfiles.Enabled = false;

				var _selectedDomainProfile = (ServiceBusinessObjects.LdapDomainProfile)dgvwDomainProfiles.Rows[e.RowIndex].DataBoundItem;

				if (AzManWinUI.Program.ShowQuestionMessage(this, this.Text, string.Format("¿Desea eliminar el Perfil de Dominio: {0}?", _selectedDomainProfile.DomainProfile)) == DialogResult.No)
					return;

				_selectedDomainProfile = await DeleteData(_selectedDomainProfile);

				dgvwDomainProfiles.Rows.RemoveAt(e.RowIndex);

				if (e.RowIndex == dgvwDomainProfiles.Rows.Count && dgvwDomainProfiles.Rows.Count > 0)
					dgvwDomainProfiles.Rows[e.RowIndex - 1].Selected = true;

				AzManWinUI.Program.ShowInfoMessage(this, this.Text, string.Format("Se eliminó el Perfil de Dominio: {0}", _selectedDomainProfile.DomainProfile));
			}
			catch {
				throw;
			}
			finally {
				panlEdit.Enabled = dgvwDomainProfiles.Enabled = true;
			}
		}

		private void dgvwDomainProfiles_SelectionChanged(object sender, EventArgs e) {
			if (_onSave)
				return;

			if (dgvwDomainProfiles.SelectedRows.Count == 0)
				return;

			var _rowIndex = dgvwDomainProfiles.SelectedRows[0].Index;

			var _domainProfile = (ServiceBusinessObjects.LdapDomainProfile)dgvwDomainProfiles.Rows[_rowIndex].DataBoundItem;

			_selectedId = _domainProfile.DomainProfileId;

			txtbDomainProfile.Text = _domainProfile.DomainProfile;
			txtbWebApiUrl.Text = _domainProfile.LdapProxyWebApiUri;
			txtbWebApiServerProfile.Text = _domainProfile.LdapServerProfile;
			txtbDomainName.Text = _domainProfile.DomainName;
			chkbEnabled.Checked = _domainProfile.Enabled;
		}
	}
}
