using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan.SnapIn.Forms;
using NetSqlAzMan.SnapIn.Printing;
using Basgosoft.ManagementConsoleLib;
using MMC = Microsoft.ManagementConsole;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes
{
	public class StoreNode : Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields
		private string _webApiUri;

		private NetSqlAzMan.ServiceBusinessObjects.AzManStore _store;

		private ToolStripButton pvtsbtTb_Properties;
		private ToolStripButton pvtsbtTb_NewApplication;
		private ToolStripButton pvtsbtTb_ItemsHierarchyView;
		private ToolStripButton pvtsbtTb_ItemsHierarchyReport;
		private ToolStripButton pvtsbtTb_AuthorizationsReport;
		private ToolStripButton pvtsbtTb_EffectivePermissionsReport;
		private ToolStripButton pvtsbtTb_Import;
		private ToolStripButton pvtsbtTb_Export;
		private ToolStripButton pvtsbtTb_Delete;
		private ToolStripButton pvtsbtTb_Refresh;

		private ToolStripButton pvtsbtCt_Properties;
		private ToolStripButton pvtsbtCt_NewApplication;
		private ToolStripButton pvtsbtCt_ItemsHierarchyView;
		private ToolStripButton pvtsbtCt_ItemsHierarchyReport;
		private ToolStripButton pvtsbtCt_AuthorizationsReport;
		private ToolStripButton pvtsbtCt_EffectivePermissionsReport;
		private ToolStripButton pvtsbtCt_Import;
		private ToolStripButton pvtsbtCt_Export;
		private ToolStripButton pvtsbtCt_Delete;
		private ToolStripButton pvtsbtCt_Refresh;

		#endregion

		#region Public Constants Field

		public const string ActionButtonKey_NewApplication = "newApplication";
		public const string ActionButtonKey_Import = "import";

		#endregion

		#region Constructors
		public StoreNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManStore store, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable)
		{
			_webApiUri = webApiUri;

			this._store = store;

			this.createNodeActionButtons();

			this.renderNode();
		}

		//public StoreNode(IAzManStore store, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable)
		//	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
		//	this.store = store;

		//	this.createNodeActionButtons();

		//	this.renderNode();
		//}
		#endregion

		#region Internal Properties

		//internal IAzManStore Store {
		//	get {
		//		return store;
		//	}
		//}

		#endregion

		#region Protected Overriden members

		protected override void renderNode()
		{
			string fixedserverrole;
			if (this._store.IAmAdmin)
				fixedserverrole = "Admin";
			else if (this._store.IAmManager)
				fixedserverrole = "Manager";
			else if (this._store.IAmUser)
				fixedserverrole = "User";
			else
				fixedserverrole = "Reader";

			this.Text = String.Format("{0} ({1})", this._store.Name, fixedserverrole);
			this.ImageKey = ImageIndexes.StoreImgIdx;
			this.SelectedImageKey = ImageIndexes.StoreImgIdx;
			this.Tag = this._store;

			this.ListItemText = this._store.Name;
			this.FirstSubItemText = this._store.Description;
			this.SecondSubItemText = this._store.StoreId.ToString();

			if (!this._store.IAmManager)
			{
				this.getActionButton(ActionButtonKey_NewApplication).Enable = false;
				this.getActionButton(ActionButtonKey_Import).Enable = false;
			}
			else
			{
				this.getActionButton(ActionButtonKey_NewApplication).Enable = true;
				this.getActionButton(ActionButtonKey_Import).Enable = true;
			}
		}

		protected override void createNodeActionButtons()
		{
			string striButton1;
			string striButton2;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg470");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg480");
			striButton2 = null;
			ab = new ActionButton(ActionButtonKey_NewApplication, striButton1, striButton2, new EventHandler(action_NewApplication_Click), out pvtsbtCt_NewApplication, out pvtsbtTb_NewApplication, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_ItemsHierarchicalView");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_ItemsHierarchyView_Click), out pvtsbtTb_ItemsHierarchyView, out pvtsbtCt_ItemsHierarchyView, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("rptMsg10");
			striButton2 = MultilanguageResource.GetString("rptTit10");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_ItemsHierarchyReport_Click), out pvtsbtCt_ItemsHierarchyReport, out pvtsbtTb_ItemsHierarchyReport, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("rptMsg20");
			striButton2 = MultilanguageResource.GetString("rptTit20");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_AuthorizationsReport_Click), out pvtsbtTb_AuthorizationsReport, out pvtsbtCt_AuthorizationsReport, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("rptMsg30");
			striButton2 = MultilanguageResource.GetString("rptTit30");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_EffectivePermissionsReport_Click), out pvtsbtCt_EffectivePermissionsReport, out pvtsbtTb_EffectivePermissionsReport, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg490");
			striButton2 = MultilanguageResource.GetString("Menu_Tit490");
			ab = new ActionButton(ActionButtonKey_Import, striButton1, striButton2, new EventHandler(action_Import_Click), out pvtsbtCt_Import, out pvtsbtTb_Import, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg500");
			striButton2 = MultilanguageResource.GetString("Menu_Tit500");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Export_Click), out pvtsbtCt_Export, out pvtsbtTb_Export, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg40");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Delete_Click), out pvtsbtCt_Delete, out pvtsbtTb_Delete, true);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren)
		{
			//Crear nodo StoreGroupsNode
			StoreGroupsNode nodeStoreGroups = new StoreGroupsNode(_webApiUri, this._store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true);
			listChildren.Add(nodeStoreGroups);

			//Listar las aplicaciones del Store
			var _h = new AzManWebApiClientHelpers.AzManApplicationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_webApiUri);
			var _return = Task.Run(() => _h.GetAsync(_store.Name, false, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
			{
				var _list = _h.GetEnumerableSBOFromReturnedContent(_return);
				foreach (var _app in _list)
				{
					_app.Store = _store;
					listChildren.Add(new ApplicationNode(_webApiUri, _app, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
				}
			}
		}
		#endregion

		#region Private methods
		private IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplication> getAzManApplications()
		{
			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManApplication> _apps = null;
			var _h = new AzManWebApiClientHelpers.AzManApplicationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_webApiUri);
			var _result = Task.Run(() => _h.GetAsync(this._store.Name, false, false, false)).Result;
			if (_h.IsResponseContentError(_result))
				_h.ThrowWebApiRequestException(_result);
			else
				_apps = _h.GetEnumerableSBOFromReturnedContent(_result);

			return _apps;
		}
		#endregion


		#region Event handlers
		private void action_Properties_Click(object sender, EventArgs e)
		{
			frmStoreProperties frm = new frmStoreProperties(_webApiUri);
			frm.Text += " - " + this._store.Name;
			frm._store = this._store;
			frm._storage = this._store.Storage;

			//DialogResult dr = this.SnapIn.Console.ShowDialog(frm);
			if (frm.ShowDialog() == DialogResult.OK)
				this.Refresh();
		}

		private void action_NewApplication_Click(object sender, EventArgs e)
		{
			frmApplicationProperties frm = new frmApplicationProperties(_webApiUri);
			//frm.store = this.store;
			frm._store = this._store;

			if (frm.ShowDialog() == DialogResult.OK)
			{
				//this.Nodes.Add(new ApplicationNode(frm.application, this.pttlstToolBar, this.ContextMenuStrip, (BaseTreeView)this.TreeView, true, true, true));
				this.Nodes.Add(new ApplicationNode(_webApiUri, frm._application, this.pttlstToolBar, this.ContextMenuStrip, (BaseTreeView)this.TreeView, true, true, true));
			}
		}

		private void action_Delete_Click(object sender, EventArgs e)
		{
			string striMessage = String.Format(MultilanguageResource.GetString("Menu_Msg520") + "\r\n'{0}'", this._store.Name);
			DialogResult dr = MessageBox.Show(striMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dr == DialogResult.Yes)
			{
				#region Call WebApi
				var _h = new AzManWebApiClientHelpers.AzManStoresHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_webApiUri);
				var _return = Task.Run(() => _h.DeleteAsync(this._store.Name)).Result;
				if (_return.ContainsKey("error"))
				{
					throw _h.GetWebApiRequestException(_return);
				}
				else
				{
					var _deleted = _h.GetSBOFromReturnedContent(_return);
				}
				//if (_return.ContainsKey("error")) {
				//	var _values = _return["error"].ToArray();
				//	var _requestUri = _values[0].ToString();
				//	var _respMsg = (HttpResponseMessage)_values[1];

				//	throw _h.getHttpWebApiRequestException(_requestUri, _respMsg);
				//}
				//else {
				//	var _values = _return["list"].ToArray();
				//	var _deleted = (NetSqlAzMan.ServiceBusinessObjects.AzManStore)_values[0];
				//}

				//Eliminar el Store de la Base de datos
				//this.store.Delete();
				#endregion

				this.Remove();
			}
		}

		private void action_EffectivePermissionsReport_Click(object sender, EventArgs e)
		{
			frmPrint frm = new frmPrint();
			var rep = new ptEffectivePermissions(_webApiUri, _store, this.getAzManApplications());
			frm.document = rep;
			frm.ShowDialog();
			//this.SnapIn.Console.ShowDialog(frm);
		}

		private void action_AuthorizationsReport_Click(object sender, EventArgs e)
		{
			frmPrint frm = new frmPrint();
			var rep = new ptItemAuthorizations(_webApiUri, this._store, this.getAzManApplications());
			frm.document = rep;
			frm.ShowDialog();
			//this.SnapIn.Console.ShowDialog(frm);
		}

		private void action_ItemsHierarchyReport_Click(object sender, EventArgs e)
		{
			frmPrint frm = new frmPrint();
			ptItemsHierarchy rep = new ptItemsHierarchy(_webApiUri, this.getAzManApplications());
			frm.document = rep;
			frm.ShowDialog();
		}

		private void action_ItemsHierarchyView_Click(object sender, EventArgs e)
		{
			frmItemsHierarchyView frm = new frmItemsHierarchyView(_webApiUri, this.getAzManApplications());
			frm.ShowDialog();
		}

		private void action_Export_Click(object sender, EventArgs e)
		{
			//frmExportOptions frm = new frmExportOptions();
			//DialogResult dr = frm.ShowDialog();
			//if (dr == DialogResult.OK) {
			//	frmExport frmwait = new frmExport();
			//	frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.store }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.store.Storage);
			//}
		}

		private void action_Import_Click(object sender, EventArgs e)
		{
			//OpenFileDialog openFileDialog = new OpenFileDialog();
			//openFileDialog.DefaultExt = "xml";
			//openFileDialog.FileName = "NetSqlAzMan.xml";
			//openFileDialog.Filter = "Xml files|*.xml|All files|*.*";
			//openFileDialog.SupportMultiDottedExtensions = true;
			//openFileDialog.Title = MultilanguageResource.GetString("ApplicationGroupsScopeNode_Msg10");
			//DialogResult dr = openFileDialog.ShowDialog();

			//if (dr == DialogResult.OK) {
			//	frmImportOptions frm = new frmImportOptions();
			//	frm.importIntoObject = this.store;
			//	frm.fileName = openFileDialog.FileName;
			//	frm.ShowDialog();

			//	this.Refresh();
			//}
		}

		private void action_Refresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}
		#endregion
	}
}