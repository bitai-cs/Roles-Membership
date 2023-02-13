using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basgosoft.ManagementConsoleLib;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes
{
	public class MembershipNode : Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields
		private NetSqlAzMan.ServiceBusinessObjects.AzManStorage _storage;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtTb_NewUser;

		private ToolStripButton pvtsbtCt_Refresh;
		private ToolStripButton pvtsbtCt_NewUser;

		#endregion

		#region Pubic Constants

		public const string ActionButtonKey_NewUser = "new";
		public const string ActionButtonKey_Refresh = "refresh";

		#endregion

		#region Constructor

		public MembershipNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManStorage storage, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			_webApiUri = webApiUri;
			_storage = storage;

			this.createNodeActionButtons();

			this.renderNode();

			base.KeyDown += new KeyEventHandler(MembershipNode_KeyDown);
		}

		#endregion

		#region Properties
		private string _webApiUri;
		internal string WebApiUri {
			get {
				return _webApiUri;
			}
		}
		#endregion

		#region Protected Overriden members

		protected override void createNodeActionButtons()
		{
			string striButton = null;
			string striButton2 = null;
			ActionButton ab;

			striButton = "Nuevo";
			striButton2 = "Nuevo usuario";
			ab = new ActionButton(ActionButtonKey_NewUser, striButton, striButton2, new EventHandler(pvtsbtCt_NewUser_Click), out pvtsbtCt_NewUser, out pvtsbtTb_NewUser, false);
			this.registerActionButton(ref ab);

			striButton = "Refrescar";
			striButton2 = null;
			ab = new ActionButton(ActionButtonKey_Refresh, striButton, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode()
		{
			this.Text = "Usuarios";
			this.ImageKey = ImageIndexes.MembershipsImgIdx;
			this.SelectedImageKey = ImageIndexes.MembershipsImgIdx;
			this.Tag = String.Empty;

			this.ListItemText = this.Text;
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.DBUser> _list = null;

			var _h = new AzManWebApiClientHelpers.DBUsersHelper<NetSqlAzMan.ServiceBusinessObjects.DBUser>(_webApiUri);
			var _return = Task.Run(() => _h.GetListAsync()).Result;
			if (_h.IsResponseContentError(_return))
			{
				throw _h.GetWebApiRequestException(_return);
			}
			else
			{
				_list = _h.GetEnumerableSBOFromReturnedContent(_return);
			}

			foreach (var _u in _list)
			{
				_u.Storage = _storage;
				listChildren.Add(new UserNode(_webApiUri, this, _u, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			}
			//if (_return.ContainsKey("error")) {
			//	var _values = _return["error"].ToArray();
			//	var _requestUri = _values[0].ToString();
			//	var _respMsg = (HttpResponseMessage)_values[1];
			//	//Disparar error
			//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
			//}
			//else {
			//	var _values = _return["list"].ToArray();
			//	var _list = (IEnumerable<NetSqlAzMan.ServiceBusinessObjects.DBUser>)_values[0];
			//	foreach (var _u in _list) {
			//		_u.Storage = _storage;
			//		listChildren.Add(new UserNode(_webApiUri, this, _u, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			//	}
			//}

			//NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory _f = new NetSqlAzMan.CustomBussinessLogic.DBUserBusinessFactory();
			//var _task = Task.Run(() => {
			//	return _f.GetAllAsync("UserName", true, string.Empty, string.Empty, null);
			//});

			//var _list = _task.Result;

			//foreach (var _u in _list) {
			//	listChildren.Add(new UserNode(this, _u, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, false));
			//}
		}

		#endregion

		#region Event handlers

		private void pvtsbtCt_NewUser_Click(object sender, EventArgs e)
		{
			NetSqlAzMan.SnapIn.Forms.UserUI formUser = new NetSqlAzMan.SnapIn.Forms.UserUI(_webApiUri, true);

			if (!formUser.NewRecord(out ptexceError))
				throw ptexceError;

			if (formUser.ShowDialog() == DialogResult.OK)
			{
				if (!this.ChildListView.ListParentNodeDetails(out ptexceError))
					throw ptexceError;
			}
		}

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}

		private void MembershipNode_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					this.getActionButton(ActionButtonKey_Refresh).Execute();
					break;
			}
		}

		#endregion
	}
}