using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using NetSqlAzManSnapIn.AddOn.Membership.Objects;

namespace NetSqlAzManSnapIn.AddOn.Test
{
	public partial class TestUI : Form
	{
		private Exception pvexceHandled = null;

		public TestUI() {
			InitializeComponent();
		}

		private void ShowMembershipUI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			NetSqlAzManSnapIn.AddOn.Membership.UI.UsersUI formMembership = new NetSqlAzManSnapIn.AddOn.Membership.UI.UsersUI(@"Data source=.\SQL2K14; Initial Catalog=AzMan_MS_CI_AI_CF; User Id=secman; Password=secman;");

			if (!formMembership.LoadData(out pvexceHandled))
				MessageBox.Show(this, pvexceHandled.Message + "\n\r" + pvexceHandled.StackTrace, pvexceHandled.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);

			formMembership.ShowDialog(this);
		}

		private void linkNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			NetSqlAzManSnapIn.AddOn.Membership.UI.UserUI formUser = new Membership.UI.UserUI(@"Data source=.\SQL2K14; Initial Catalog=AzMan_MS_CI_AI_CF; User Id=secman; Password=secman;", true);

			formUser.UserRecord = new UserStruct();

			if (!formUser.NewRecord(out pvexceHandled)) {
				MessageBox.Show(this, pvexceHandled.Message, pvexceHandled.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			formUser.ShowDialog();
		}
	}
}
