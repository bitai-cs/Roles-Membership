using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LDAPHelperSvcRef;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;

namespace AzManWinUI.LDAPServices {
	public partial class LDAPQueryClientUI : Form {
		private NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] _selectedADObjects;
		public NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] SelectedADObjects {
			get {
				return _selectedADObjects;
			}
		}

		public LDAPQueryClientUI(IAzManStorage storage) {
			InitializeComponent();

			System.Data.SqlClient.SqlConnection _cn = new System.Data.SqlClient.SqlConnection(storage.ConnectionString);
			System.Data.SqlClient.SqlCommand _cmd = _cn.CreateCommand();
			_cmd.CommandText = "SELECT [ldap_domain],[ldap_description],[ldap_client_endpoint],[ldap_enabled] FROM [Basgosoft].[LDAPConfig] WHERE ldap_enabled = 1;";
			_cmd.CommandType = CommandType.Text;
			_cmd.CommandTimeout = 20;
			_cn.Open();
			System.Data.SqlClient.SqlDataReader _reader = _cmd.ExecuteReader();
			List<LDAPConfig> _list = new List<LDAPConfig>();
			while (_reader.Read()) {
				_list.Add(new LDAPConfig(_reader.GetString(0), _reader.GetString(1), _reader.GetString(2)));
			}
			_reader.Close();

			combLDAP.DisplayMember = "Description";
			combLDAP.ValueMember = "Domain";
			combLDAP.DataSource = _list;

			_cn.Close();

			combLDAP.SelectedIndex = 0;
		}

		private void butnSearch_Click(object sender, EventArgs e) {
			filterLDAPObjects(txtbUsuario.Text);
		}

		private bool filterLDAPObjects(string filter) {
			try {
				lvieSelected.Items.Clear();

				LDAPConfig _ldap = (LDAPConfig)combLDAP.SelectedItem;

				NetSqlAzMan.LDAPHelperSvcRef.LDAPHelperClient _client = new NetSqlAzMan.LDAPHelperSvcRef.LDAPHelperClient(_ldap.Client_EndPoint);
				NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult[] _result;
				StatusInfo _status;
				if (!_client.SearchUsersAndGroupsByTwoProperties(out _result, out _status, _ldap.Domain, NetSqlAzMan.LDAPHelperSvcRef.LDAPHelperADProperties.cn, filter, NetSqlAzMan.LDAPHelperSvcRef.LDAPHelperADProperties.samAccountName, filter, false)) {
					throw new Exception(string.Format("{0}:{1}\n\r{2}", _status.ExceptionType, _status.StatusMessage, _status.StackTrace));
				}

				if (_result.Length.Equals(0))
					MessageBox.Show(this, "No se puede encontrar un objeto que coincida parcial o totalmente con el nombre ingresado.");

				foreach (NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult _object in _result) {
					if (string.IsNullOrEmpty(_object.samAccountName))
						continue;
					var _li = lvieSelected.Items.Add(_object.objectSid, _object.samAccountName, string.Empty);
					_li.Tag = _object;
					_li.SubItems.Add(_object.displayName);
					_li.SubItems.Add(_object.DomainProfile);
					_li.SubItems.Add(_object.userPrincipalName);
					_li.SubItems.Add(_object.objectSid);
				}

				return true;
			}
			catch (Exception ex) {
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}
		}

		private void butnSelect_Click(object sender, EventArgs e) {
			if (lvieSelected.SelectedItems.Count.Equals(0)) {
				MessageBox.Show(this, "Primero debe de seleccionar los usuarios que desea agregar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			foreach (ListViewItem _lvi in lvieSelected.SelectedItems) {
				NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult _object = (NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult)_lvi.Tag;
				if (!lvieConfirm.Items.ContainsKey(_object.objectSid)) {
					var _li = lvieConfirm.Items.Add(_object.objectSid,
_object.samAccountName, string.Empty);
					_li.Tag = _object;
					_li.SubItems.Add(_object.displayName);
					_li.SubItems.Add(_object.DomainProfile);
					_li.SubItems.Add(_object.userPrincipalName);
					_li.SubItems.Add(_object.objectSid);
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
				NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult _ldapResult = (NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult)_li.Tag;
				NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject _object = new NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject() {

					ADSPath = _ldapResult.distinguishedName,
					ClassName = _ldapResult.objectCategory.ToLower().StartsWith("cn=group") ? "group" : "user",
					LDAPDomain = _ldapResult.DomainProfile,
					LDAPSid = _ldapResult.objectSid,
					Name = _ldapResult.samAccountName,
					UPN = _ldapResult.userPrincipalName
				};
				_selectedADObjects.SetValue(_object, _i);
				_i++;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}

	internal class LDAPConfig {
		public string Description {
			get;
			set;
		}
		public string Domain {
			get;
			set;
		}
		public string Client_EndPoint {
			get;
			set;
		}

		internal LDAPConfig(string domain, string description, string client_endpoint) {
			Domain = domain;
			Description = description;
			Client_EndPoint = client_endpoint;
		}
	}
}
