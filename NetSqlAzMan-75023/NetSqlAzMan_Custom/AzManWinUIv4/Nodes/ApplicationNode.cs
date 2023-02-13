using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Globalization;
using Basgosoft.ManagementConsoleLib;
using NetSqlAzMan.SnapIn.Forms;
using NetSqlAzMan.SnapIn.Printing;
using System.Threading.Tasks;
using System.Net.Http;

namespace AzManWinUI.Nodes {
    internal class ApplicationNode : Basgosoft.ManagementConsoleLib.BaseNode {
        #region Private fields
        private string _webApiUri;

        private NetSqlAzMan.ServiceBusinessObjects.AzManApplication _application;

        private ToolStripButton pvtsbtTb_Properties;
        private ToolStripButton pvtsbtCt_Properties;

        private ToolStripButton pvtsbtTb_ItemsHierarchicalView;
        private ToolStripButton pvtsbtCt_ItemsHierarchicalView;

        private ToolStripButton pvtsbtTb_GenerateCheckAccessHelper;
        private ToolStripButton pvtsbtCt_GenerateCheckAccessHelper;

        private ToolStripButton pvtsbtTb_CheckAccessTest;
        private ToolStripButton pvtsbtCt_CheckAccessTest;

        private ToolStripButton pvtsbtTb_ItemsHierarchyReport;
        private ToolStripButton pvtsbtCt_ItemsHierarchyReport;

        private ToolStripButton pvtsbtTb_AuthorizationsReport;
        private ToolStripButton pvtsbtCt_AuthorizationsReport;

        private ToolStripButton pvtsbtTb_EffectivePermissionsReport;
        private ToolStripButton pvtsbtCt_EffectivePermissionsReport;

        private ToolStripButton pvtsbtTb_Import;
        private ToolStripButton pvtsbtCt_Import;

        private ToolStripButton pvtsbtTb_Export;
        private ToolStripButton pvtsbtCt_Export;

        private ToolStripButton pvtsbtTb_Delete;
        private ToolStripButton pvtsbtCt_Delete;

        private ToolStripButton pvtsbtTb_Refresh;
        private ToolStripButton pvtsbtCt_Refresh;

        #endregion

        #region Public Constants Field

        public const string ActionButtonKey_Properties = "properties";
        public const string ActionButtonKey_ItemsHierarchicalView = "ItemsHierarchicalView";
        public const string ActionButtonKey_GenerateCheckAccessHelper = "GenerateCheckAccessHelper";
        public const string ActionButtonKey_CheckAccessTest = "CheckAccessTest";
        public const string ActionButtonKey_ItemsHierarchyReport = "ItemsHierarchyReport";
        public const string ActionButtonKey_AuthorizationsReport = "AuthorizationsReport";
        public const string ActionButtonKey_EffectivePermissionsReport = "EffectivePermissionsReport";
        public const string ActionButtonKey_Import = "Import";
        public const string ActionButtonKey_Export = "Export";
        public const string ActionButtonKey_Delete = "Delete";
        public const string ActionButtonKey_Refresh = "Refresh";

        #endregion

        #region Constructor

        //public ApplicationNode(IAzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
        //	: base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
        //	this.application = application;

        //	this.createNodeActionButtons();

        //	this.renderNode();
        //}

        public ApplicationNode(string webApiUri, NetSqlAzMan.ServiceBusinessObjects.AzManApplication application, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isAtivable)
            : base(toolBar, contextMenu, treeView, isListable, isExpandible, isAtivable) {
            _webApiUri = webApiUri;

            this._application = application;

            this.createNodeActionButtons();

            this.renderNode();
        }

        #endregion

        #region Internal Properties

        //internal IAzManApplication Application {
        //	get {
        //		return this.application;
        //	}
        //}

        //internal IAzManApplication Interface {
        //	get {
        //		return this.application;
        //	}
        //}

        #endregion

        #region Protected Overriden members

        protected override void renderNode() {
            string fixedserverrole;
            if (this._application.IAmAdmin)
                fixedserverrole = "Admin";
            else if (this._application.IAmManager)
                fixedserverrole = "Manager";
            else if (this._application.IAmUser)
                fixedserverrole = "User";
            else
                fixedserverrole = "Reader";

            this.Text = String.Format("{0} ({1})", this._application.Name, fixedserverrole);
            this.ImageKey = ImageIndexes.ApplicationImgIdx;
            this.SelectedImageKey = ImageIndexes.ApplicationImgIdx;
            this.Tag = _application;

            this.ListItemText = this._application.Name;
            this.FirstSubItemText = this._application.Description;

            #region Habilitar acciones segun parametros

            if (this._application.Store.IAmManager) {
                this.getActionButton(ActionButtonKey_Import).Enable = true;
                this.getActionButton(ActionButtonKey_Delete).Enable = true;
            }
            else {
                this.getActionButton(ActionButtonKey_Import).Enable = false;
                this.getActionButton(ActionButtonKey_Delete).Enable = false;
            }

            if (this._application.Store.Storage.Mode == NetSqlAzMan.ServiceBusinessObjects.AzManMode.Developer) {
                this.getActionButton(ActionButtonKey_GenerateCheckAccessHelper).Enable = true;
                this.getActionButton(ActionButtonKey_CheckAccessTest).Enable = true;
            }
            else {
                this.getActionButton(ActionButtonKey_GenerateCheckAccessHelper).Enable = false;
                this.getActionButton(ActionButtonKey_CheckAccessTest).Enable = false;
            }

            #endregion
        }

        protected override void createNodeActionButtons() {
            string striButton1;
            string striButton2;
            ActionButton ab;

            striButton1 = MultilanguageResource.GetString("Menu_Msg230");
            striButton2 = MultilanguageResource.GetString("Menu_Tit230");
            ab = new ActionButton(ActionButtonKey_Properties, striButton1, striButton2, new EventHandler(action_Properties_Click), out pvtsbtCt_Properties, out pvtsbtTb_Properties, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("Menu_ItemsHierarchicalView");
            striButton2 = MultilanguageResource.GetString("Menu_ItemsHierarchicalViewDescription");
            ab = new ActionButton(ActionButtonKey_ItemsHierarchicalView, striButton1, striButton2, new EventHandler(action_ItemsHierarchicalView_Click), out pvtsbtCt_ItemsHierarchicalView, out pvtsbtTb_ItemsHierarchicalView, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("Menu_Msg240");
            striButton2 = MultilanguageResource.GetString("Menu_Tit240");
            ab = new ActionButton(ActionButtonKey_GenerateCheckAccessHelper, striButton1, striButton2, new EventHandler(action_GenerateCheckAccesHelper_Click), out pvtsbtTb_GenerateCheckAccessHelper, out pvtsbtCt_GenerateCheckAccessHelper, false);
            this.registerActionButton(ref ab);

            striButton1 = "Pruebas de acceso";
            striButton2 = string.Empty;
            ab = new ActionButton(ActionButtonKey_CheckAccessTest, striButton1, striButton2, new EventHandler(action_CheckAccessTest_Click), out pvtsbtCt_CheckAccessTest, out pvtsbtTb_CheckAccessTest, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("rptMsg10");
            striButton2 = MultilanguageResource.GetString("rptTit10");
            ab = new ActionButton(ActionButtonKey_ItemsHierarchyReport, striButton1, striButton2, new EventHandler(action_ItemsHierarchyReport_Click), out pvtsbtCt_ItemsHierarchyReport, out pvtsbtTb_ItemsHierarchyReport, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("rptMsg20");
            striButton2 = MultilanguageResource.GetString("rptTit20");
            ab = new ActionButton(ActionButtonKey_AuthorizationsReport, striButton1, striButton2, new EventHandler(action_AuthorizationsReport_Click), out pvtsbtCt_AuthorizationsReport, out pvtsbtTb_AuthorizationsReport, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("rptMsg30");
            striButton2 = MultilanguageResource.GetString("rptTit30");
            ab = new ActionButton(ActionButtonKey_EffectivePermissionsReport, striButton1, striButton2, new EventHandler(action_EffectivePermissionsReport_Click), out pvtsbtCt_EffectivePermissionsReport, out pvtsbtTb_EffectivePermissionsReport, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("Menu_Msg250");
            striButton2 = MultilanguageResource.GetString("Menu_Tit250");
            ab = new ActionButton(ActionButtonKey_Import, striButton1, striButton2, new EventHandler(action_Import_Click), out pvtsbtCt_Import, out pvtsbtTb_Import, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("Menu_Msg260");
            striButton2 = MultilanguageResource.GetString("Menu_Tit260");
            ab = new ActionButton(ActionButtonKey_Export, striButton1, striButton2, new EventHandler(action_Export_Click), out pvtsbtCt_Export, out pvtsbtTb_Export, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("Menu_Msg40");
            striButton2 = null;
            ab = new ActionButton(ActionButtonKey_Delete, striButton1, striButton2, new EventHandler(action_Delete_Click), out pvtsbtCt_Delete, out pvtsbtTb_Delete, false);
            this.registerActionButton(ref ab);

            striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
            striButton2 = null;
            ab = new ActionButton(ActionButtonKey_Refresh, striButton1, striButton2, new EventHandler(action_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
            this.registerActionButton(ref ab);
        }

        protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
            listChildren.Add(new ApplicationGroupsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

            listChildren.Add(new ItemDefinitionsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));

            listChildren.Add(new ItemAuthorizationsNode(_webApiUri, this._application, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
        }

        #endregion

        #region Event handlers

        private void action_Properties_Click(object sender, EventArgs e) {
            frmApplicationProperties frm = new frmApplicationProperties(_webApiUri);
            //frm.Text += " - " + this.application.Name;
            //frm.application = this.application;
            //frm.store = this.application.Store;
            frm.Text += " - " + this._application.Name;
            frm._application = this._application;
            frm._store = this._application.Store;

            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK) {
                this.renderNode();
            }
        }

        private void action_Refresh_Click(object sender, EventArgs e) {
            this.Refresh();
        }

        private void action_Export_Click(object sender, EventArgs e) {
            //frmExportOptions frm = new frmExportOptions();
            //DialogResult dr = frm.ShowDialog();

            //if (dr == DialogResult.OK) {
            //	frmExport frmwait = new frmExport();
            //	frmwait.ShowDialog(null, frm.fileName, new IAzManExport[] { this.application }, frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.application.Store.Storage);
            //}
        }

        private void action_Import_Click(object sender, EventArgs e) {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.DefaultExt = "xml";
            //openFileDialog.FileName = "NetSqlAzMan.xml";
            //openFileDialog.Filter = "Xml files|*.xml|All files|*.*";
            //openFileDialog.SupportMultiDottedExtensions = true;
            //openFileDialog.Title = MultilanguageResource.GetString("ApplicationGroupsScopeNode_Msg10");

            //DialogResult dr = openFileDialog.ShowDialog();
            //if (dr == DialogResult.OK) {
            //	frmImportOptions frm = new frmImportOptions();
            //	frm.importIntoObject = this.application;
            //	frm.fileName = openFileDialog.FileName;
            //	frm.ShowDialog();

            //	this.Refresh();
            //}
        }

        private void action_EffectivePermissionsReport_Click(object sender, EventArgs e) {
            frmPrint frm = new frmPrint();
            var rep = new ptEffectivePermissions(_webApiUri, _application.Store, new NetSqlAzMan.ServiceBusinessObjects.AzManApplication[] { _application });
            frm.document = rep;
            frm.ShowDialog();
        }

        private void action_AuthorizationsReport_Click(object sender, EventArgs e) {
            frmPrint frm = new frmPrint();
            var rep = new ptItemAuthorizations(_webApiUri, this._application.Store, new NetSqlAzMan.ServiceBusinessObjects.AzManApplication[] { this._application });
            //rep.Applications = new IAzManApplication[] { this.application };
            frm.document = rep;
            frm.ShowDialog();
        }

        private void action_ItemsHierarchyReport_Click(object sender, EventArgs e) {
            frmPrint frm = new frmPrint();
            ptItemsHierarchy rep = new ptItemsHierarchy(_webApiUri, new NetSqlAzMan.ServiceBusinessObjects.AzManApplication[] { this._application });
            frm.document = rep;
            frm.ShowDialog();
        }

        private void action_CheckAccessTest_Click(object sender, EventArgs e) {
            ////OLD VERSION
            //frmCheckAccessTest frm = new frmCheckAccessTest();
            //frm.application = this.application;
            //frm.ShowDialog();

            var frm = new frmCheckAccessTest(this._webApiUri);
            //frm.application = this.application;
            frm._application = this._application;
            frm.ShowDialog();
        }

        private void action_GenerateCheckAccesHelper_Click(object sender, EventArgs e) {
            //frmGenerateCheckAccessHelper frm = new frmGenerateCheckAccessHelper();
            //frm.application = this.application;
            //frm.ShowDialog();
        }

        private void action_ItemsHierarchicalView_Click(object sender, EventArgs e) {
            var _frm = new frmItemsHierarchyView(_webApiUri, new NetSqlAzMan.ServiceBusinessObjects.AzManApplication[] { this._application });
            _frm.ShowDialog();
        }

        private void action_Delete_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show(String.Format(MultilanguageResource.GetString("Menu_Msg280") + "\r\n'{0}'", this._application.Name), MultilanguageResource.GetString("Menu_Msg270"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes) {
                NetSqlAzMan.ServiceBusinessObjects.AzManApplication _deleted;
                #region Call Webapi
                var _h = new AzManWebApiClientHelpers.AzManApplicationsHelper<NetSqlAzMan.ServiceBusinessObjects.AzManApplication>(_webApiUri);
                var _return = Task.Run(() => _h.DeleteAsync(this._application.Name, this._application.Store.Name)).Result;
                if (_h.IsResponseContentError(_return))
                    _h.ThrowWebApiRequestException(_return);
                else
                    _deleted = _h.GetSBOFromReturnedContent(_return);
                #endregion

                //this.application.Delete();
                this.Remove();
            }
        }

        #endregion
    }
}
