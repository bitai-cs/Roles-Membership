using System;
using System.Web.Security;

namespace NetSqlAzMan_RoleProviderWebTest {
	public partial class _Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			bool isInRole = Roles.IsUserInRole("Arianna");
		}
		
		protected void Button1_Click(object sender, EventArgs e) {

		}
	}
}
