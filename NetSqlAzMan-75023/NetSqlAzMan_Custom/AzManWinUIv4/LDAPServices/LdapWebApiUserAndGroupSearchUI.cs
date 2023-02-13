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

namespace AzManWinUI.LDAPServices
{
    public partial class LdapWebApiUserAndGroupSearchUI :Form
    {
        private int _elapsed;
        private string _webApiUri;

        #region Properties
        private NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] _selectedADObjects;
        public NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject[] SelectedADObjects {
            get {
                return _selectedADObjects;
            }
        }
        #endregion

        #region Constructors
        public LdapWebApiUserAndGroupSearchUI(string webApiUri) {
            InitializeComponent();

            _webApiUri = webApiUri;

            combWilcardCharType.SelectedIndex = 0;

            IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile> _sbos = null;
            #region Call WebApi
            var _h = new AzManWebApiClientHelpers.LdapWebApiDomainProfilesHelper<NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile>(_webApiUri);
            var _return = Task.Run(() => _h.GetAllAsync()).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _sbos = _h.GetEnumerableSBOFromReturnedContent(_return);
            #endregion

            combDomainProfile.DisplayMember = "DomainProfile";
            combDomainProfile.ValueMember = "DomainProfile";
            combDomainProfile.DataSource = _sbos;
            combDomainProfile.SelectedIndex = 0;
        }
        #endregion

        #region Private methods
        private void waitAni(bool flag) {
            pictWait.Visible = pictWait.Enabled = flag;

            _elapsed = -1;
            lablWait.Text = string.Empty;
            lablWait.Visible = timrWait.Enabled = flag;

            if (flag)
                timrWait_Tick(null, null);
        }

        private async Task<int> filterLDAPObjects(string filter) {
            lvieSelected.Items.Clear();

            var _domainProfile = combDomainProfile.SelectedItem as NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile;
            var _serverBaseDN = combServerBaseDN.SelectedItem as NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN;

            LdapHelperDTO.AsyncResult _result = null;
            #region Call WebApi
            var _h = new AzManWebApiClientHelpers.LdapWebApiUsersHelper<LdapHelperDTO.AsyncResult>(_webApiUri);
            var _return = await Task.Run(() => _h.SearchUsersAndGroupsAsyncModeAsync(_domainProfile == null ? null : _domainProfile.DomainProfile, true, _serverBaseDN == null ? new Nullable<byte>() : _serverBaseDN.BaseDNOrder, filter, LdapHelperDTO.RequiredEntryAttributes.Minimun));
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _result = _h.GetSBOFromReturnedContent(_return);
            #endregion

            if (_result.Entries.Count() > 0) {
                foreach (var _entry in _result.Entries) {
                    if (string.IsNullOrEmpty(_entry.samAccountName))
                        continue;
                    var _li = lvieSelected.Items.Add(_entry.objectSid, _entry.samAccountName, string.Empty);
                    _li.Tag = _entry;
                    _li.SubItems.Add(_entry.displayName);
                    _li.SubItems.Add(_entry.DomainProfile);
                    _li.SubItems.Add(_entry.userPrincipalName);
                    _li.SubItems.Add(_entry.objectSid);
                }
            }

            if (_result.HasErrorInfo()) {
                MessageBox.Show(this, string.Format("Error al obtener los datos.\r\n{0}: {1}", _result.ErrorType, _result.ErrorMessage), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _result.Entries.Count();
        }
        #endregion

        #region Event handlers
        private async void butnSearch_Click(object sender, EventArgs e) {
            int _count = 0;

            try {
                waitAni(true);

                combDomainProfile.Enabled = combServerBaseDN.Enabled = combWilcardCharType.Enabled = butnSearch.Enabled = txtbUsuario.Enabled = butnSelect.Enabled = butnConfirm.Enabled = false;

                var _criteria = txtbUsuario.Text;
                switch (combWilcardCharType.SelectedIndex) {
                    case 0: //Igual
                        break;
                    case 1: //Empieza con
                        _criteria = _criteria + '*';
                        break;
                    case 2: //Termina con						
                        _criteria = "*" + _criteria;
                        break;
                    case 3: //Contiene
                        _criteria = "*" + _criteria + "*";
                        break;
                }

                _count = await filterLDAPObjects(_criteria);

                if (_count.Equals(0)) {
                    waitAni(false);

                    MessageBox.Show(this, "No se puede encontrar un objeto que coincida con el  nombre ingresado.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch {
                throw;
            }
            finally {
                combDomainProfile.Enabled = combServerBaseDN.Enabled = combWilcardCharType.Enabled = butnSearch.Enabled = txtbUsuario.Enabled = butnSelect.Enabled = butnConfirm.Enabled = true;

                waitAni(false);

                if (_count > 0)
                    Program.ShowInfoMessage(this, this.Text, string.Format("{0} objetos encontrados", _count));
            }
        }

        private void butnSelect_Click(object sender, EventArgs e) {
            if (lvieSelected.SelectedItems.Count.Equals(0)) {
                MessageBox.Show(this, "Primero debe de seleccionar los usuarios que desea agregar.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (ListViewItem _lvi in lvieSelected.SelectedItems) {
                var _entry = (LdapHelperDTO.LdapEntry)_lvi.Tag;
                if (!lvieConfirm.Items.ContainsKey(_entry.objectSid)) {
                    var _li = lvieConfirm.Items.Add(_entry.objectSid, _entry.samAccountName, string.Empty);
                    _li.Tag = _entry;
                    _li.SubItems.Add(_entry.displayName);
                    _li.SubItems.Add(_entry.DomainProfile);
                    _li.SubItems.Add(_entry.userPrincipalName);
                    _li.SubItems.Add(_entry.objectSid);
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
                var _entry = (LdapHelperDTO.LdapEntry)_li.Tag;
                var _object = new NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject() {
                    DomainProfile = _entry.DomainProfile,
                    LdapSid = _entry.objectSid,
                    LdapBinarySid = _entry.objectSidBytes,
                    ADSPath = _entry.distinguishedName,
                    ClassName = _entry.objectClass.Where(i => i.Equals("group", StringComparison.OrdinalIgnoreCase)).Any() ? "group" : "user",
                    Name = _entry.samAccountName,
                    UPN = _entry.userPrincipalName,
                    samAccountName = _entry.samAccountName,
                    cn = _entry.cn,
                    displayName = _entry.displayName,
                    distinguishedName = _entry.distinguishedName
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

        private void combLdapProfileId_SelectedIndexChanged(object sender, EventArgs e) {
            var _profile = (NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile)combDomainProfile.SelectedItem;

            IEnumerable<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN> _sbos = null;
            #region Call WebApi
            var _h = new AzManWebApiClientHelpers.LdapWebApiServerBaseDNsHelper<NetSqlAzMan.ServiceBusinessObjects.LdapServerBaseDN>(_webApiUri);
            var _return = Task.Run(() => _h.GetByDomainProfileAndWideScopeStatusAsync(_profile.DomainProfile, false)).Result;
            if (_h.IsResponseContentError(_return))
                _h.ThrowWebApiRequestException(_return);
            else
                _sbos = _h.GetEnumerableSBOFromReturnedContent(_return);
            #endregion

            combServerBaseDN.DisplayMember = "BaseDN";
            combServerBaseDN.ValueMember = "BaseDNOrder";
            combServerBaseDN.DataSource = _sbos;
            if (!_sbos.Count().Equals(0))
                combServerBaseDN.SelectedIndex = 0;
        }
        #endregion
    }
}
