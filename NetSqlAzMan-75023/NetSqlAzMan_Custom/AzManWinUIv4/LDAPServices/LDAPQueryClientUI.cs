using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;

namespace AzManWinUI.LDAPServices {
	public partial class LDAPQueryClientUI : Form {
		private int _elapsed;

		private string _webApiUri;

		private NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] _selectedADObjects;
		public NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] SelectedADObjects {
			get {
				return _selectedADObjects;
			}
		}

		public LDAPQueryClientUI(string webApiUri) {
			InitializeComponent();

			_webApiUri = webApiUri;

			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig> _resultList;
			#region Call WebApi
			var _h = new AzManWinUI.WebApiHelpers.LDAPConfigHelper<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>(_webApiUri);
			var _return = Task.Run(() => _h.GetListAsync()).Result;
			if (_return.ContainsKey("error")) {
				var _values = _return["error"].ToArray();
				var _requestUri = _values[0].ToString();
				var _respMsg = (HttpResponseMessage)_values[1];
				//Disparar error
				throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			}
			else {
				var _values = _return["list"].ToArray();
				_resultList = (IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPConfig>)_values[0];
			}
			#endregion

			combLDAP.DisplayMember = "ldap_domain";
			combLDAP.ValueMember = "ldap_domain";
			combLDAP.DataSource = _resultList;
			combLDAP.SelectedIndex = 0;
		}

		private void waitAni(bool flag) {
			pictWait.Visible = pictWait.Enabled = flag;

			_elapsed = -1;
			lablWait.Text = string.Empty;
			lablWait.Visible = timrWait.Enabled = flag;

			if (flag)
				timrWait_Tick(null, null);
		}

		private async void butnSearch_Click(object sender, EventArgs e) {
			int _count = 0;

			try {
				waitAni(true);

				combLDAP.Enabled = butnSearch.Enabled = txtbUsuario.Enabled = butnSelect.Enabled = butnConfirm.Enabled = false;

				_count = await filterLDAPObjects(txtbUsuario.Text);

				if (_count.Equals(0))
					MessageBox.Show(this, "No se puede encontrar un objeto que coincida parcial o totalmente con el nombre ingresado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch {
				throw;
			}
			finally {
				combLDAP.Enabled = butnSearch.Enabled = txtbUsuario.Enabled = butnSelect.Enabled = butnConfirm.Enabled = true;

				waitAni(false);

				if (_count > 0)
					Program.ShowInfoMessage(this, this.Text, string.Format("{0} objetos encontrados", _count));
			}
		}

		private async Task<int> filterLDAPObjects(string filter) {
			lvieSelected.Items.Clear();

			var _ldap = (NetSqlAzMan.ServiceBusinessObjects.LDAPConfig)combLDAP.SelectedItem;

			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LDAPResult> _resultList = null;
			#region Call WebApi
			var _h = new AzManWinUI.WebApiHelpers.LdapWebApiClientHelper<NetSqlAzMan.ServiceBusinessObjects.LDAPResult>(_webApiUri);
			var _return = await Task.Run(() => _h.SearchUsersAndGroupsAsync(_ldap.ldap_domain, filter));
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_resultList = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion

			if (_resultList.Count() > 0) {
				foreach (var _object in _resultList) {
					if (string.IsNullOrEmpty(_object.samAccountName))
						continue;
					var _li = lvieSelected.Items.Add(_object.ObjectSid, _object.samAccountName, string.Empty);
					_li.Tag = _object;
					_li.SubItems.Add(_object.DisplayName);
					_li.SubItems.Add(_object.DomainProfile);
					_li.SubItems.Add(_object.UserPrincipalName);
					_li.SubItems.Add(_object.ObjectSid);
				}
			}

			return _resultList.Count();
		}

		private void butnSelect_Click(object sender, EventArgs e) {
			if (lvieSelected.SelectedItems.Count.Equals(0)) {
				MessageBox.Show(this, "Primero debe de seleccionar los usuarios que desea agregar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			foreach (ListViewItem _lvi in lvieSelected.SelectedItems) {
				var _object = (NetSqlAzMan.ServiceBusinessObjects.LDAPResult)_lvi.Tag;
				if (!lvieConfirm.Items.ContainsKey(_object.ObjectSid)) {
					var _li = lvieConfirm.Items.Add(_object.ObjectSid,
_object.samAccountName, string.Empty);
					_li.Tag = _object;
					_li.SubItems.Add(_object.DisplayName);
					_li.SubItems.Add(_object.DomainProfile);
					_li.SubItems.Add(_object.UserPrincipalName);
					_li.SubItems.Add(_object.ObjectSid);
				}
			}
		}

		private void butnConfirm_Click(object sender, EventArgs e) {
			if (lvieSelected.Items.Count.Equals(0)) {
				MessageBox.Show(this, "No ha seleccionado los usuarios que desea agregar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			_selectedADObjects = new NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[lvieConfirm.Items.Count];
			int _i = 0;
			foreach (ListViewItem _li in lvieConfirm.Items) {
				var _ldapResult = (NetSqlAzMan.ServiceBusinessObjects.LDAPResult)_li.Tag;
				var _object = new NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject() {
					DomainProfile = _ldapResult.DomainProfile,
					LDAPSid = _ldapResult.ObjectSid,
					ADSPath = _ldapResult.DistinguishedName,
					ClassName = _ldapResult.ObjectCategory.ToLower().StartsWith("cn=group") ? "group" : "user",
					Name = _ldapResult.samAccountName,
					UPN = _ldapResult.UserPrincipalName
				};
				_selectedADObjects.SetValue(_object, _i);
				_i++;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void timrWait_Tick(object sender, EventArgs e) {
			++_elapsed;
			lablWait.Text = string.Format("Esperando {0} segundos", _elapsed);
		}
	}
}
