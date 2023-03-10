using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.Forms;
using System.Data.SqlClient;
using System.Security.Principal;
using NetSqlAzMan.SnapIn.Globalization;
using NetSqlAzMan;
using Basgosoft.ManagementConsoleLib;
using Basgosoft.Environment.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using static AzManWinUI.Global;

namespace AzManWinUI.Nodes
{
	public class StorageNode :Basgosoft.ManagementConsoleLib.BaseNode
	{
		#region Private fields
		private string _webApiUri;

		private string companyName;
		private string productName;

		private string dataSource;
		private string initialCatalog;
		private string security;
		private string userId;
		private string password;
		private string otherSettings;

		private IAzManStorage storage;
		private NetSqlAzMan.ServiceBusinessObjects.AzManStorage _storage;

		private List<KeyValuePair<string, string>> pvlistStoreAttributes;

		private ToolStripButton pvtsbtTb_Config;
		private ToolStripButton pvtsbtTb_LDAPConfig;
		private ToolStripButton pvtsbtTb_ModeAndLogging;
		private ToolStripButton pvtsbtTb_Auditing;
		private ToolStripButton pvtsbtTb_InvalidateWCFCaheService;
		private ToolStripButton pvtsbtTb_ConfigLdapWacDomainProfile;
		private ToolStripButton pvtsbtTb_NewStore;
		private ToolStripButton pvtsbtTb_ImportStores;
		private ToolStripButton pvtsbtTb_ExportStores;
		private ToolStripButton pvtsbtTb_ImportFromMsAzMan;
		private ToolStripButton pvtsbtTb_Refresh;

		private ToolStripButton pvtsbtCt_Config;
		private ToolStripButton pvtsbtCt_LDAPConfig;
		private ToolStripButton pvtsbtCt_ModeAndLogging;
		private ToolStripButton pvtsbtCt_Auditing;
		private ToolStripButton pvtsbtCt_InvalidateWCFCaheService;
		private ToolStripButton pvtsbtCt_ConfigLdapWacDomainProfile;
		private ToolStripButton pvtsbtCt_NewStore;
		private ToolStripButton pvtsbtCt_ImportStores;
		private ToolStripButton pvtsbtCt_ExportStores;
		private ToolStripButton pvtsbtCt_ImportFromMsAzMan;
		private ToolStripButton pvtsbtCt_Refresh;

		#endregion

		#region Public const fields
		public const string ActionButtonKey_ConfigLdapWacDomainProfiles = "ConfigLdapWacDomainProfiles";
		public const string ActionButtonKey_NewStore = "NewStore";
		public const string ActionButtonKey_ImportStores = "ImportStores";
		public const string ActionButtonKey_ImportAzMan = "ImportAzMan";
		public const string ActionButtonKey_Properties = "properties";
		#endregion

		#region Constructors

		public StorageNode(string webApiUri, string companyName, string productName, ToolStrip toolBar, ContextMenuStrip contextMenu, BaseTreeView treeView, bool isListable, bool isExpandible, bool isActivable, List<KeyValuePair<string, string>> storeAttributeList)
			: base(toolBar, contextMenu, treeView, isListable, isExpandible, isActivable) {
			_webApiUri = webApiUri;

			//Informacion del proveedor de la aplicación
			this.companyName = companyName;
			this.productName = productName;

			pvlistStoreAttributes = storeAttributeList.ToList();

			//Crear comandos del nodo
			this.createNodeActionButtons();

			//Inicializar las propiedades iniciales de node
			this.renderNode();

			base.KeyDown += new KeyEventHandler(StorageNode_KeyDown);
		}

		#endregion

		#region Private methods
		private void getStorage(out NetSqlAzMan.ServiceBusinessObjects.AzManStorage storage) {
			storage = null;
			#region Call WebApi
			
			var _h = new AzManWebApiClientHelpers.AzManStorageHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStorage>(_webApiUri);
			var _return = Task.Run(() => _h.GetAsync(false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				storage = _h.GetSBOFromReturnedContent(_return);
			#endregion
		}
		#endregion

		#region Protected Overriden members
		protected override void createNodeActionButtons() {
			string striButton1 = null;
			string striButton2 = null;
			ActionButton ab;

			striButton1 = MultilanguageResource.GetString("Menu_Msg340");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Config_Click), out pvtsbtCt_Config, out pvtsbtTb_Config, false);
			this.registerActionButton(ref ab);

			striButton1 = "Configuración del cliente LDAP";
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_LDAPConfig_Click), out pvtsbtCt_LDAPConfig, out pvtsbtTb_LDAPConfig, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg550");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_ModeAndLogging_Click), out pvtsbtCt_ModeAndLogging, out pvtsbtTb_ModeAndLogging, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg560");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_Auditing_Click), out pvtsbtCt_Auditing, out pvtsbtTb_Auditing, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg570");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_InvalidateWCFCaheService_Click), out pvtsbtCt_InvalidateWCFCaheService, out pvtsbtTb_InvalidateWCFCaheService, false);
			this.registerActionButton(ref ab);

			//striButton1 = MultilanguageResource.GetString("Menu_Msg360");
			striButton1 = "Perfiles de Dominio del Proxy LDAP";
			ab = new ActionButton(ActionButtonKey_ConfigLdapWacDomainProfiles, striButton1, striButton2, new EventHandler(action_Click), out pvtsbtCt_ConfigLdapWacDomainProfile, out pvtsbtTb_ConfigLdapWacDomainProfile, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg360");
			ab = new ActionButton(ActionButtonKey_NewStore, striButton1, striButton2, new EventHandler(action_NewStore_Click), out pvtsbtCt_NewStore, out pvtsbtTb_NewStore, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg370");
			ab = new ActionButton(ActionButtonKey_ImportStores, striButton1, striButton2, new EventHandler(action_ImportStores_Click), out pvtsbtCt_ImportStores, out pvtsbtTb_ImportStores, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg380");
			ab = new ActionButton(striButton1, striButton2, new EventHandler(action_ExportStores_Click), out pvtsbtCt_ExportStores, out pvtsbtTb_ExportStores, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("Menu_Msg390");
			ab = new ActionButton(ActionButtonKey_ImportAzMan, striButton1, striButton2, new EventHandler(action_ImportFromMsAzMan_Click), out pvtsbtCt_ImportFromMsAzMan, out pvtsbtTb_ImportFromMsAzMan, false);
			this.registerActionButton(ref ab);

			striButton1 = MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text");
			striButton2 = null;
			ab = new ActionButton(striButton1, striButton2, new EventHandler(pvtsbtCt_Refresh_Click), out pvtsbtCt_Refresh, out pvtsbtTb_Refresh, false);
			this.registerActionButton(ref ab);
		}

		protected override void renderNode() {
			//Obtener AzManStorage
			this.getStorage(out _storage);

			//Prepare Node
			this.Text = this._webApiUri;
			this.ToolTipText = _storage.Description;
			this.ImageKey = ImageIndexes.NetSqlAzManImgIdx;
			this.SelectedImageKey = ImageIndexes.NetSqlAzManImgIdx;
			this.Tag = _storage;

			bool imadmin;
			try {
				imadmin = _storage.IAmAdmin;
			}
			catch {
				imadmin = false;
			}

			if (!imadmin) {
				this.getActionButton(ActionButtonKey_NewStore).Enable = false;
				this.getActionButton(ActionButtonKey_ImportStores).Enable = false;
				this.getActionButton(ActionButtonKey_ImportAzMan).Enable = false;
			}
			else {
				this.getActionButton(ActionButtonKey_NewStore).Enable = true;
				this.getActionButton(ActionButtonKey_ImportStores).Enable = true;
				this.getActionButton(ActionButtonKey_ImportAzMan).Enable = true;
			}

			/*
			//Pasar los atributos comunes que usaran los Stores del Storage
			try {
				foreach (string k in this.storage.Stores.Keys) {
					//Se obtiene un Store
					IAzManStore store = this.storage.Stores[k];
					//Se recorre cada attributo que debe poseer el Store obtenido
					foreach (KeyValuePair<string, string> pair in pvlistStoreAttributes) {
						//Iniciar transaction
						this.storage.BeginTransaction(AzManIsolationLevel.ReadUncommitted);
						//El atributo del Store existe, actualizarlo
						if (store.Attributes.ContainsKey(pair.Key))
							store.Attributes[pair.Key].Update(pair.Key, pair.Value);
						else //El atributo del Store NO existe, crearlo
							store.CreateAttribute(pair.Key, pair.Value);
						//Confirmar transaction
						this.storage.CommitTransaction();
					}
				}
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			*/
		}

		protected override void createNewChildrenNodesAndAddToList(ref List<BaseNode> listChildren) {
			MembershipNode nodeMembership = new MembershipNode(_webApiUri, _storage, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, false, false, false);
			listChildren.Add(nodeMembership);

			IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManStore> _stores = null;
			#region Call WebApi
			var _h = new AzManWebApiClientHelpers.AzManStoresHelper<NetSqlAzMan.ServiceBusinessObjects.AzManStore>(_webApiUri);
			var _return = Task.Run(() => _h.GetAsync(true, false, false)).Result;
			if (_h.IsResponseContentError(_return))
				_h.ThrowWebApiRequestException(_return);
			else
				_stores = _h.GetEnumerableSBOFromReturnedContent(_return);
			#endregion

			foreach (var _store in _stores) {
				_store.Storage = _storage;
				listChildren.Add(new StoreNode(_webApiUri, _store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
			}
		}

		#endregion

		#region Public members

		public bool Config(out Exception hex) {
			hex = null;

			try {
				Basgosoft.Environment.Configuration.UI.ConfigurationManagerUI formUI = new Basgosoft.Environment.Configuration.UI.ConfigurationManagerUI(this.companyName, this.productName, false, true, false);

				if (formUI.InitializeUI(null, "password", out ptexceError)) {
					if (formUI.ShowDialog(this.TreeView.Parent) == System.Windows.Forms.DialogResult.OK) {
						this.renderNode();
					}
					else {
					}
				}
				else
					throw ptexceError;

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool ConfigLdapWacDomiainProfiles(out Exception hex) {
			hex = null;

			try {
				var frm = new LdapWacDomainProfilesUI(_webApiUri);
				//frm._storage = this._storage;

				DialogResult dr = frm.ShowDialog();
				//if (dr == DialogResult.OK) {
				//	//this.Nodes.Add(new StoreNode(frm.store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
				//	this.Nodes.Add(new StoreNode(_webApiUri, frm._store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
				//}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public bool NewStore(out Exception hex) {
			hex = null;

			try {
				frmStoreProperties frm = new frmStoreProperties(_webApiUri);
				frm._storage = this._storage;

				DialogResult dr = frm.ShowDialog();
				if (dr == DialogResult.OK) {
					//this.Nodes.Add(new StoreNode(frm.store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
					this.Nodes.Add(new StoreNode(_webApiUri, frm._store, this.pttlstToolBar, this.ContextMenuStrip, this.pttvieTreeView, true, true, true));
				}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}
		#endregion

		#region Event handlers

		private void action_Config_Click(object sender, EventArgs e) {
			throw new Exception("Esta opción ya no es funcional en esta versión.");

			if (!this.Config(out ptexceError))
				throw ptexceError;

			this.Refresh();
		}

		private void action_LDAPConfig_Click(object sender, EventArgs e) {
			MessageBox.Show("En preceso de implementación.");

			this.Refresh();
		}

		//private void action_ManageMembership_Click(object sender, EventArgs e)
		//{
		//   NetSqlAzManSnapIn.AddOn.Membership.UI.MembershipsUI formMembership = new NetSqlAzManSnapIn.AddOn.Membership.UI.MembershipsUI(this.Storage.ConnectionString);

		//   formMembership.ShowDialog();
		//}

		private void action_ExportStores_Click(object sender, EventArgs e) {
			frmExportOptions frm = new frmExportOptions();
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK) {
				frmExport frmwait = new frmExport();
				frmwait.ShowDialog(null, frm.fileName, this.storage.GetStores(), frm.includeSecurityObjects, frm.includeDBUsers, frm.includeAuthorizations, this.storage);
			}
		}

		private void action_ImportStores_Click(object sender, EventArgs e) {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = "xml";
			openFileDialog.FileName = "NetSqlAzMan.xml";
			openFileDialog.Filter = "Xml files|*.xml|All files|*.*";
			openFileDialog.SupportMultiDottedExtensions = true;
			openFileDialog.Title = MultilanguageResource.GetString("ApplicationGroupsScopeNode_Msg10");

			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				frmImportOptions frm = new frmImportOptions();
				frm.importIntoObject = this.storage;
				frm.fileName = openFileDialog.FileName;
				frm.ShowDialog();

				this.Refresh();
			}
		}

		private void action_Click(object sender, EventArgs e) {
			var _key = ((ToolStripButton)sender).Tag.ToString();

			switch (_key) {
				case ActionButtonKey_ConfigLdapWacDomainProfiles:
					if (!this.ConfigLdapWacDomiainProfiles(out ptexceError))
						throw ptexceError;
					break;
				default:
					throw new NotImplementedException("No se ha implementado el funcionamiento del comando.");
			}
		}

		private void action_NewStore_Click(object sender, EventArgs e) {
			if (!this.NewStore(out ptexceError))
				throw ptexceError;
		}

		private void action_ImportFromMsAzMan_Click(object sender, EventArgs e) {
			throw new System.NotImplementedException("Ësta función no está disponible en esta versión.");
		}

		private void action_InvalidateWCFCaheService_Click(object sender, EventArgs e) {
			//frmInvalidateWCFCacheService frm = new frmInvalidateWCFCacheService();
			//frm.ShowDialog();

			throw new System.NotImplementedException("Ësta función no está disponible en esta versión.");
		}

		private void action_Auditing_Click(object sender, EventArgs e) {
			frmSQLAudit frm = new frmSQLAudit();
			frm.storage = this.storage;
			frm.ShowDialog();
		}

		private void action_ModeAndLogging_Click(object sender, EventArgs e) {
			frmOptions frm = new frmOptions();
			frm.storage = this.storage;
			frm.mode = this.storage.Mode;
			frm.logOnEventLog = this.storage.LogOnEventLog;
			frm.logOnDb = this.storage.LogOnDb;
			frm.logErrors = this.storage.LogErrors;
			frm.logWarnings = this.storage.LogWarnings;
			frm.logInformations = this.storage.LogInformations;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK) {
				this.storage.Mode = frm.mode;
				this.storage.LogOnEventLog = frm.logOnEventLog;
				this.storage.LogOnDb = frm.logOnDb;
				this.storage.LogErrors = frm.logErrors;
				this.storage.LogWarnings = frm.logWarnings;
				this.storage.LogInformations = frm.logInformations;
				this.storage = new SqlAzManStorage(frmStorageConnection.ConstructConnectionString(this.dataSource, this.initialCatalog, !(this.security == "Sql"), this.userId, this.password, this.otherSettings));

				this.Refresh();
			}
		}

		private void pvtsbtCt_Refresh_Click(object sender, EventArgs e) {
			this.Refresh();
		}

		private void StorageNode_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.F5:
					this.getActionButton(MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text")).Execute();
					break;
			}
		}

		#endregion
	}
}