using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.ServiceBusinessObjects;

namespace NetSqlAzMan.SnapIn.Forms
{
	public partial class frmStoreProperties : frmBase
	{
		internal IAzManStorage storage = null;
		internal IAzManStore store = null;

		internal AzManStorage _storage = null;
		internal AzManStore _store = null;
		private string _webApiUri;

		public frmStoreProperties(string webApiUri)
		{
			_webApiUri = webApiUri;

			InitializeComponent();
		}

		private void frmCreateStore_Load(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.None;
			//if (this.store != null) {
			if (this._store != null)
			{
				this.btnPermissions.Enabled = true;
				this.btnAttributes.Enabled = true;

				this.txtName.Text = this._store.Name;
				this.txtDescription.Text = this._store.Description;
				this.txtName.SelectAll();
				if (!this._store.IAmManager)
					this.txtName.Enabled = this.txtDescription.Enabled = this.btnOk.Enabled = false;
			}
			else
			{
				this.btnPermissions.Enabled = false;
				this.btnAttributes.Enabled = false;
			}
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
			if (this._store != null)
			{
				this.Text = Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg10");
			}
			else
			{
				this.Text = Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg20");
			}
		}

		protected void HourGlass(bool switchOn)
		{
			this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
			/*Application.DoEvents();*/
		}

		protected void ShowError(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected void ShowInfo(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected void ShowWarning(string text, string caption)
		{
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void frmCreateStore_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{
			this.ValidateForm();
		}

		private void ValidateForm()
		{
			bool isValid = true;
			if (this.txtName.Text.Trim().Length == 0)
			{
				isValid = false;
				this.errorProvider1.SetError(this.txtName, Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg30"));
			}
			else
			{
				this.errorProvider1.SetError(this.txtName, String.Empty);
			}
			this.btnOk.Enabled = isValid;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				this.HourGlass(true);
				//Create
				if (this._store == null)
				{
					var _new = new AzManStore()
					{
						Name = txtName.Text,
						Description = txtDescription.Text
					};

					AzManStore _created;
					#region Call WebApi
					var _h = new AzManWebApiClientHelpers.AzManStoresHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_webApiUri);
					var _return = Task.Run(() => _h.PostAsync(_new)).Result;
					if (_h.IsResponseContentError(_return))
					{
						throw _h.GetWebApiRequestException(_return);
					}
					else
					{
						_created = _h.GetSBOFromReturnedContent(_return);
					}
					//if (_return.ContainsKey("error"))
					//{
					//	var _values = _return["error"].ToArray();
					//	var _requestUri = _values[0].ToString();
					//	var _respMsg = (HttpResponseMessage)_values[1];

					//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
					//}
					//else
					//{
					//	var _values = _return["contentData"].ToArray();
					//	_created = (AzManStore)_values[0];
					//}
					#endregion

					this._store = _created;

					this.DialogResult = DialogResult.OK;
				}
				else
				{ //Update
					var _modified = this._store.Clone();
					_modified.Name = txtName.Text;
					_modified.Description = txtDescription.Text;

					AzManStore _updated;
					#region Call WebApi				
					var _h = new AzManWebApiClientHelpers.AzManStoresHelper<ServiceBusinessObjects.AzManStore>(_webApiUri);
					var _return = Task.Run(() => _h.PutAsync(_store.Name, _modified)).Result;
					if (_h.IsResponseContentError(_return))
					{
						throw _h.GetWebApiRequestException(_return);
					}
					else
					{
						_updated = _h.GetSBOFromReturnedContent(_return);
					}
					//if (_return.ContainsKey("error")) {
					//	var _values = _return["error"].ToArray();
					//	var _requestUri = _values[0].ToString();
					//	var _respMsg = (HttpResponseMessage)_values[1];

					//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
					//}
					//else {
					//	var _values = _return["contentData"].ToArray();
					//	_updated = (AzManStore)_values[0];
					//}
					#endregion

					this._store = _updated;

					this.DialogResult = DialogResult.OK;
				}

				this.HourGlass(false);
			}
			catch (Exception ex)
			{
				this.HourGlass(false);
				this.DialogResult = DialogResult.None;
				this.ShowError(ex.Message, this._store == null ? Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg40") : Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg50"));
			}
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			this.ValidateForm();
		}

		private void txtLDapPath_TextChanged(object sender, EventArgs e)
		{
			this.ValidateForm();
		}

		private void btnAttributes_Click(object sender, EventArgs e)
		{
			try
			{
				using (var frm = new frmStoreAttributes(_webApiUri))
				{
					frm.Text += " - " + this._store.Name;
					frm._store = this._store;

					if (frm.ShowDialog(this) == DialogResult.OK)
					{
						this._store = frm._store;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmStoreProperties_Msg60"));
			}
		}

		private void btnPermissions_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Refactoring!");

			//try {
			//	frmStorePermissions frm = new frmStorePermissions();
			//	frm.Text += " - " + this.store.Name;
			//	frm.store = this.store;
			//	frm.ShowDialog(this);
			//	/*Application.DoEvents();*/
			//}
			//catch (Exception ex) {
			//	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmApplicationProperties_Msg40"));
			//}
		}
	}
}