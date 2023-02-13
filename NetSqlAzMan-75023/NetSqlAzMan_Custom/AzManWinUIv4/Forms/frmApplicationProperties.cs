using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.SnapIn.Forms {
	public partial class frmApplicationProperties : frmBase {
		internal IAzManStore store;
		internal IAzManApplication application;

		private string _webApiUri;
		internal ServiceBusinessObjects.AzManStore _store;
		internal ServiceBusinessObjects.AzManApplication _application;

		public frmApplicationProperties(string webApiUri) {
			_webApiUri = webApiUri;

			InitializeComponent();
		}

		private void frmApplicationProperties_Load(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			//if (this.application != null) {
			if (this._application != null) {
				this.btnAttributes.Enabled = true;
				this.btnPermissions.Enabled = true;
				this.Text = Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg10");
				this.txtName.Text = this._application.Name;
				this.txtDescription.Text = this._application.Description;
				this.txtName.SelectAll();
				if (!this._application.IAmManager)
					this.txtName.Enabled = this.txtDescription.Enabled = this.btnOk.Enabled = false;
			}
			else {
				this.btnAttributes.Enabled = false;
				this.btnPermissions.Enabled = false;
				this.Text = Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg20");
			}
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
		}

		protected void HourGlass(bool switchOn) {
			this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
			/*Application.DoEvents();*/
		}

		protected void ShowError(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected void ShowInfo(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected void ShowWarning(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void frmApplicationProperties_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnOk_Click(object sender, EventArgs e) {
			try {
				this.HourGlass(true);
				//Create
				//if (this.application == null) {
				if (this._application == null) {
					#region Call Webapi
					var _new = new ServiceBusinessObjects.AzManApplication() {
						Name = txtName.Text,
						Description = txtDescription.Text
					};

					var _h = new AzManWebApiClientHelpers.AzManApplicationsHelper<ServiceBusinessObjects.AzManApplication>(_webApiUri);
					ServiceBusinessObjects.AzManApplication _created = null;
					var _return = Task.Run(() => _h.PostAsync(_new, this._store.Name)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else {
						_created = _h.GetSBOFromReturnedContent(_return);
						_new.Store = this._store;
					}
					#endregion

					//this.application = this.store.CreateApplication(this.txtName.Text, this.txtDescription.Text);
					this._application = _created;

					this.DialogResult = DialogResult.OK;
				}
				else {
					#region Call Webapi
					var _modified = new ServiceBusinessObjects.AzManApplication() {
						Name = txtName.Text,
						Description = txtDescription.Text
					};

					var _h = new AzManWebApiClientHelpers.AzManApplicationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_webApiUri);
					ServiceBusinessObjects.AzManApplication _updated = null;
					var _return = Task.Run(() => _h.PutAsync(this._application.Name, this._store.Name, _modified)).Result;
					if (_h.IsResponseContentError(_return))
						_h.ThrowWebApiRequestException(_return);
					else
						_updated = _h.GetSBOFromReturnedContent(_return);

					this._application.Name = _updated.Name;
					this._application.Description = _updated.Description;
					#endregion

					//this.application.Store.Storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
					//this.application.Rename(this.txtName.Text.Trim());
					//this.application.Update(this.txtDescription.Text.Trim());
					//this.application.Store.Storage.CommitTransaction();
					this.DialogResult = DialogResult.OK;
				}
				this.HourGlass(false);
			}
			catch (Exception ex) {
				this.HourGlass(false);

				//if (this.application != null)
				//	this.application.Store.Storage.RollBackTransaction();

				this.DialogResult = DialogResult.None;
				this.ShowError(ex.Message, this._application == null ? Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg30") : Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg40"));
			}
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}

		private void txtName_TextChanged(object sender, EventArgs e) {
			if (this.txtName.Text.Trim().Length == 0) {
				this.btnOk.Enabled = false;
				this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg50"));
			}
			else {
				this.btnOk.Enabled = true;
				this.errorProvider1.SetError(this.txtName, String.Empty);
			}
		}

		private void btnAttributes_Click(object sender, EventArgs e) {
			try {
				frmApplicationAttributes frm = new frmApplicationAttributes();
				frm.Text += " - " + this.application.Name;
				frm.application = this.application;
				frm.ShowDialog(this);
				/*Application.DoEvents();*/
				frm.Dispose();
				/*Application.DoEvents();*/
			}
			catch (Exception ex) {
				this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg40"));
			}
		}

		private void btnPermissions_Click(object sender, EventArgs e) {
			try {
				frmApplicationPermissions frm = new frmApplicationPermissions();
				frm.Text += " - " + this.application.Name;
				frm.application = this.application;
				frm.ShowDialog(this);
				/*Application.DoEvents();*/
			}
			catch (Exception ex) {
				this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg40"));
			}
		}
	}
}