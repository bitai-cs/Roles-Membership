using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManLoginHelper;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManLoginWinForms
{
	public partial class BranchSelectorUI : Form
	{
		private Exception _exce = null;

		#region Constructor
		public BranchSelectorUI(DBUser dbUser)
			: base() {
			InitializeComponent();

			_user = dbUser;

			string _branchIds;
			string _branchNames;
			if (LoginHelper.GetUserPropertyValueFromAttString(_user, AzManLoginHelper.UserPropertyEnum.RelativeBranchOfficeIds, out _branchIds, out _exce) && LoginHelper.GetUserPropertyValueFromAttString(_user, AzManLoginHelper.UserPropertyEnum.BranchOfficeNames, out _branchNames, out _exce)) {
				var _ids = _branchIds.Split(new string[] { "!" }, StringSplitOptions.None);
				var _names = _branchNames.Split(new string[] { "!" }, StringSplitOptions.None);

				List<BranchItemStruct> _list = new List<BranchItemStruct>(_ids.GetLength(0));
				int _i = -1;
				foreach (string _s in _ids) {
					_i++;
					_list.Add(new BranchItemStruct() {
						Id = _s,
						Name = _names[_i]
					});
				}

				listBranchOffice.DataSource = _list;
			}
			else {
				throw _exce;
			}
		}
		#endregion

		#region Properties
		private DBUser _user = null;
		public DBUser DBUser {
			get {
				return _user;
			}
		}

		private BranchItemStruct _selectedBranchItem;
		public BranchItemStruct SelectedBranchItem {
			get {
				return _selectedBranchItem;
			}
			set {
				_selectedBranchItem = value;
				listBranchOffice.SelectedItem = value;
			}
		}
		#endregion

		#region Event handlers
		private void BranchSelectorUI_Load(object sender, EventArgs e) {
		}

		private void butnOk_Click(object sender, EventArgs e) {
			_selectedBranchItem = (BranchItemStruct)listBranchOffice.SelectedItem;

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void listBranchOffice_MouseDoubleClick(object sender, MouseEventArgs e) {
			var _index = listBranchOffice.IndexFromPoint(e.Location);
			if (_index == System.Windows.Forms.ListBox.NoMatches)
				return;
			listBranchOffice.SelectedIndex = _index;
			butnOk_Click(this, new EventArgs());
		}
		#endregion
	}
}
