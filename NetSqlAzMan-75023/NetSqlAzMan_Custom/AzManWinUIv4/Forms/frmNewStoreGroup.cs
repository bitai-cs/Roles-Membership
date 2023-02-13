using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.SnapIn.Forms {
	public partial class frmNewStoreGroup : frmBase {
		//internal IAzManStore store;
		//internal IAzManStoreGroup storeGroup = null;

		internal ServiceBusinessObjects.AzManStore _store;
		internal ServiceBusinessObjects.AzManStoreGroup _storeGroup = null;

		private string _webApiUri = null;

		public frmNewStoreGroup(string webApiUri) {
			InitializeComponent();

			_webApiUri = webApiUri;
		}

		private void frmNewStoreGroup_Load(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
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

		private void frmNewStoreGroup_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void txtName_TextChanged(object sender, EventArgs e) {
			if (String.IsNullOrEmpty(this.txtName.Text.Trim())) {
				this.btnOk.Enabled = false;
				this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmNewStoreGroup_Msg10"));
			}
			else {
				this.btnOk.Enabled = true;
				this.errorProvider1.SetError(this.txtName, String.Empty);
			}
		}

		private void btnOk_Click(object sender, EventArgs e) {
			this.HourGlass(true);
			try {
				var _new = new ServiceBusinessObjects.AzManStoreGroup() {
					Store = _store.Clone(),
					Name = txtName.Text,
					Description = txtDescription.Text,
					GroupType = rbtBasic.Checked ? ServiceBusinessObjects.GroupType.Basic : ServiceBusinessObjects.GroupType.LDapQuery
				};

				ServiceBusinessObjects.AzManStoreGroup _created = null;
				#region Call WebApi
				var _h = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
				var _return = Task.Run(() => _h.PostAsync(_new)).Result;
				if (_h.IsResponseContentError(_return))
					_h.ThrowWebApiRequestException(_return);
				else
					_created = _h.GetSBOFromReturnedContent(_return);
				#endregion

				this._storeGroup = _created;

				this.HourGlass(false);
				this.DialogResult = DialogResult.OK;
			}
			catch (Exception ex) {
				this.HourGlass(false);
				this.DialogResult = DialogResult.None;

				throw ex;
				//this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmNewStoreGroup_Msg20"));
			}
		}
	}
}