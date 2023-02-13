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

namespace NetSqlAzMan.CustomBussinessLogic.TestUI
{
	public partial class LDAPClientBOTestUI :Form
	{
		public LDAPClientBOTestUI() {
			InitializeComponent();

			combEntryAttributes.DataSource = Enum.GetValues(typeof(LdapHelperDTO.EntryAttribute));
			combEntryAttributes.SelectedIndex = 3;

			combEntryAttributes2.DataSource = Enum.GetValues(typeof(LdapHelperDTO.KeyEntryAttribute));
			combEntryAttributes2.SelectedIndex = 2;
		}

		private void LDAPClientBOTestUI_Load(object sender, EventArgs e) {
			combWideScope.SelectedIndex = 0;
		}

		private async void butnGetAllConfig_Click(object sender, EventArgs e) {
			try {
				butnGetAllConfig.Enabled = false;

				lDAPConfigBindingSource.DataSource = null;

				NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory _bo = new LDAPWebSvcBusinessFactory();
				var _result = await Task.Run(() => _bo.GetAllLDAPConfigAsync());

				lDAPConfigBindingSource.DataSource = _result;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnGetAllConfig.Enabled = true;
			}
		}

		private async void button1_Click(object sender, EventArgs e) {
			try {
				butnGetConfigByProfile.Enabled = false;

				NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory _bo = new LDAPWebSvcBusinessFactory();
				var _cfg = await Task.Run(() => _bo.GetLDAPConfigByDomainProfileAsync("BASGOSOFT"));

				MessageBox.Show(string.Format("{0}: {1}", _cfg.ldap_domain, _cfg.ldap_client_endpoint));
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnGetConfigByProfile.Enabled = true;
			}
		}

		private async void button1_Click_1(object sender, EventArgs e) {
			try {
				butnResolveName.Enabled = false;
				txtbResolveNameCriteria.Enabled = false;
				dgrdResolveName.Enabled = false;

				NetSqlAzMan.CustomBussinessLogic.LDAPWebSvcBusinessFactory _bo = new LDAPWebSvcBusinessFactory();
				var _resultSet = await Task.Run(() => _bo.SearchUsersAndGroupsWithWSvcAsync("LANPERU", txtbResolveNameCriteria.Text));

				MessageBox.Show(_resultSet.Count().ToString("Se encontraron {0} objetos."));

				lDAPResultBindingSource.DataSource = _resultSet;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnResolveName.Enabled = true;
				txtbResolveNameCriteria.Enabled = true;
				dgrdResolveName.Enabled = true;
			}
		}

		private async void butnGetAllLdapProfiles_Click(object sender, EventArgs e) {
			try {
				butnGetAllLdapProfiles.Enabled = false;

				ldapWacDomainProfileBindingSource.DataSource = null;
				ldapServerBaseDNBindingSource.DataSource = null;
				ldapEntryBindingSource.DataSource = null;

				var _bo = new LdapWacDomainProfileBusinessFactory();
				var _result = await Task.Run(() => _bo.GetAllLdapDomainProfilesAsync());

				ldapWacDomainProfileBindingSource.DataSource = _result;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnGetAllLdapProfiles.Enabled = true;
			}
		}

		private async void butnSearchUsersAndGroupsWithWebAPI_Click(object sender, EventArgs e) {
			try {
				butnSearchUsersAndGroupsWithWebAPI.Enabled = false;
				txtbCriteria.Enabled = false;
				dataGridView3.Enabled = false;

				if (dataGridView2.SelectedRows.Count.Equals(0))
					throw new Exception("Seleccione un ServerBaseDN del listado.");

				ldapEntryBindingSource.DataSource = null;

				var _p = (ServiceBusinessObjects.LdapServerBaseDN)dataGridView4.SelectedRows[0].DataBoundItem;

				var _bo = new LdapWebApiBusinessFactory();
				var _result = await Task.Run(() => _bo.SearchUsersAndGroupsAsyncModeAsync(_p.DomainProfile.DomainProfile, true, _p.BaseDNOrder, txtbCriteria.Text, LdapHelperDTO.RequiredEntryAttributes.FewWithMemberAndMemberOf));

				MessageBox.Show(string.Format("DomainProfile: {0} -> Se encontraron {1} objetos.", _p.DomainProfile, _result.Entries.Count()));

				if (_result.HasErrorInfo())
					MessageBox.Show(string.Format("{0}: {1}", _result.ErrorType, _result.ErrorMessage));

				ldapEntryBindingSource.DataSource = _result.Entries;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnSearchUsersAndGroupsWithWebAPI.Enabled = true;
				txtbCriteria.Enabled = true;
				dataGridView3.Enabled = true;
			}
		}

		private async void button1_Click_2(object sender, EventArgs e) {
			try {
				butnGetConfigByProfile.Enabled = false;

				var _p = Interaction.InputBox("Que Domain Profile desea buscar?", this.Text, "HOLDING");

				var _bo = new LdapWacDomainProfileBusinessFactory();
				var _cfg = await Task.Run(() => _bo.GetLdapDomainProfileByDomainProfileAsync(_p));

				MessageBox.Show(string.Format("{0} => {1} | {2}", _cfg.DomainProfile, _cfg.LdapProxyWebApiUri, _cfg.LdapServerProfile));
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnGetConfigByProfile.Enabled = true;
			}
		}

		private async void button2_Click(object sender, EventArgs e) {
			try {
				butnAuthenticateUserAsync.Enabled = false;
				txtbUser.Enabled = false;
				txtbPassword.Enabled = false;

				var _bo = new LdapWebApiBusinessFactory();
				var _result = await _bo.AuthenticateUserAsync("PERU", false, txtbUser.Text, txtbPassword.Text);

				var _format = string.Format("DomainProfile: {0}\r\nUseGC: {1}\r\nUser: {2}\r\nIsAuthenticated: {3}", _result.DomainProfile, _result.UseGC, _result.User, _result.IsAuthenticated);
				Program.ShowInfoMessage(this, this.Text, _format);
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnAuthenticateUserAsync.Enabled = true;
				txtbUser.Enabled = true;
				txtbPassword.Enabled = true;
			}
		}

		private async void button2_Click_1(object sender, EventArgs e) {
			try {
				if (dataGridView2.SelectedRows.Count.Equals(0))
					throw new Exception("Seleccione un DomainProfile del listado.");

				ldapServerBaseDNBindingSource.DataSource = null;
				ldapEntryBindingSource.DataSource = null;

				var _p = (ServiceBusinessObjects.LdapDomainProfile)dataGridView2.SelectedRows[0].DataBoundItem;

				var _wideScope = combWideScope.SelectedIndex.Equals(0);

				var _bo = new LdapWacServerBaseDNBusinessFactory();
				var _result = await Task.Run(() => _bo.GetLdapServerBaseDNByDomainProfileAndWideScopeStatusAsync(_p.DomainProfile, _wideScope));

				ldapServerBaseDNBindingSource.DataSource = _result;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {

			}
		}

		private async void button3_Click(object sender, EventArgs e) {
			try {
				butnSearchEntriesAsyncModeAsync.Enabled = combEntryAttributes.Enabled = txtbEntryAttributeFilter.Enabled = dataGridView5.Enabled = false;

				if (dataGridView2.SelectedRows.Count.Equals(0))
					throw new Exception("Seleccione un ServerBaseDN del listado.");

				ldapEntryBindingSource1.DataSource = null;

				var _p = (ServiceBusinessObjects.LdapServerBaseDN)dataGridView4.SelectedRows[0].DataBoundItem;

				LdapHelperDTO.EntryAttribute _attribute;
				Enum.TryParse<LdapHelperDTO.EntryAttribute>(combEntryAttributes.SelectedValue.ToString(), out _attribute);

				var _bo = new LdapWebApiBusinessFactory();
				var _result = await Task.Run(() => _bo.SearchEntriesAsyncModeAsync(_p.DomainProfile.DomainProfile, true, _p.BaseDNOrder, _attribute, txtbEntryAttributeFilter.Text, LdapHelperDTO.RequiredEntryAttributes.AllWithMemberAndMemberOf));

				MessageBox.Show(string.Format("DomainProfile: {0} -> Se encontraron {1} objetos.", _p.DomainProfile, _result.Entries.Count()));

				if (_result.HasErrorInfo())
					MessageBox.Show(string.Format("{0}: {1}", _result.ErrorType, _result.ErrorMessage));

				ldapEntryBindingSource1.DataSource = _result.Entries;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnSearchEntriesAsyncModeAsync.Enabled = combEntryAttributes.Enabled = txtbEntryAttributeFilter.Enabled = dataGridView5.Enabled = true;
			}
		}

		private async void butnSearchGroupMembershipAsync_Click(object sender, EventArgs e) {
			try {
				butnSearchEntriesAsyncModeAsync.Enabled = combEntryAttributes.Enabled = txtbEntryAttributeFilter.Enabled = dataGridView5.Enabled = false;

				if (dataGridView2.SelectedRows.Count.Equals(0))
					throw new Exception("Seleccione un ServerBaseDN del listado.");

				ldapEntryBindingSource2.DataSource = null;

				var _p = (ServiceBusinessObjects.LdapServerBaseDN)dataGridView4.SelectedRows[0].DataBoundItem;

				LdapHelperDTO.KeyEntryAttribute _attribute;
				Enum.TryParse<LdapHelperDTO.KeyEntryAttribute>(combEntryAttributes2.SelectedValue.ToString(), out _attribute);

				var _bo = new LdapWebApiBusinessFactory();
				var _result = await Task.Run(() => _bo.SearchGroupMembershipAsync(_p.DomainProfile.DomainProfile, true, _p.BaseDNOrder, _attribute, txtbEntryAttributeFilter2.Text, LdapHelperDTO.RequiredEntryAttributes.AllWithMemberAndMemberOf));

				MessageBox.Show(string.Format("DomainProfile: {0} -> Se encontraron {1} objetos.", _p.DomainProfile, _result.Count()));

				ldapEntryBindingSource2.DataSource = _result;
			}
			catch (Exception ex) {
				Program.ShowErrorMessage(this, this.Text, ex);
			}
			finally {
				butnSearchEntriesAsyncModeAsync.Enabled = combEntryAttributes.Enabled = txtbEntryAttributeFilter.Enabled = dataGridView5.Enabled = true;
			}
		}

		private async void button4_Click(object sender, EventArgs e) {
			var _sbo = (ServiceBusinessObjects.LdapDomainProfile)ldapWacDomainProfileBindingSource.Current;

			var _bl = new CustomBussinessLogic.LdapWacDomainProfileBusinessFactory();
			var _deleted = await _bl.DeleteAsync(_sbo);
		}
	}
}