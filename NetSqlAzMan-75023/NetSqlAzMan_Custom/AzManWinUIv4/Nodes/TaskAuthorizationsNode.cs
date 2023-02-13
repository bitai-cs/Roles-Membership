using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.Interfaces;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using System.Threading.Tasks;

namespace AzManWinUI.Nodes {
	public class TaskAuthorizationsNode : BaseNode {
		#region Private fields
		private IAzManApplication application;

		private string _webApiUri;
		private NetSqlAzMan.ServiceBusinessObjects.AzManApplication _application;

		private ToolStripButton pvtsbtTb_Refresh;
		private ToolStripButton pvtsbtCt_Refresh;
		#endregion

		#region Constructor
		//public TaskAuthorizationsNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
		//	this.application = application;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}

		public TaskAuthorizationsNode(string wau, NetSqlAzMan.ServiceBusinessObjects.AzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
			_webApiUri = wau;

			this._application = application;

			this.createNodeActionButtons();

			this.renderNode();
		}
		#endregion

		#region Protected Overriden members
		protected override void createNodeActionButtons() {
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			this.Text = MultilanguageResource.GetString("Folder_Msg110");
			this.ImageKey = ImageIndexes.TaskAuthorizationsImgIdx;
			this.SelectedImageKey = ImageIndexes.TaskAuthorizationsImgIdx;
			this.Tag = this._application;

			this.ListItemText = this.Text;
			this.FirstSubItemText = MultilanguageResource.GetString("Folder_Tit110");
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			///New Logic
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem> _itemDefinitions = null;
			#region Call WebApi			
			var _h = new AzManWebApiClientHelpers.AzManItemsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManItem>(_webApiUri);
			var _return = Task.Run(() => _h.GetItemsAsync(this._application.Store.Name, this._application.Name, NetSqlAzMan.ServiceBusinessObjects.ItemType.Task, false, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_itemDefinitions = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion
			foreach (NetSqlAzMan.ServiceBusinessObjects.AzManItem item in _itemDefinitions)
				listChildren.Add(new ItemAuthorizationNode(_webApiUri, item, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));

			///OLD Logic
			//IAzManItem[] items = this.application.GetItems(ItemType.Task);
			//foreach (IAzManItem item in items)
			//	listChildren.Add(new ItemAuthorizationNode(item, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, false, true));
		}
		#endregion

		#region Event handlers
		private void action_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}
		#endregion
	}
}