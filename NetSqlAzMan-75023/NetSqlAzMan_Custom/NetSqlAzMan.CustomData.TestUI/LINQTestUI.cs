using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using NetSqlAzMan.CustomDataLayer.LINQ;

namespace NetSqlAzMan.CustomDataLayer.TestUI
{
	public partial class LINQTestUI : Form
	{
		private const string ConnectionId = "AzManConnectionString";

		public LINQTestUI() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			//using (DBUsersModelDataContext _ct = Program.GetDBUserModel()) {
			//	var _q = from c in _ct.identity_Users
			//				select c;

			//	var _l = _q.ToList();

			//	identityUserBindingSource.DataSource = _l;
			//}
		}
	}
}
