using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.SnapIn.DirectoryServices;
using NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace NetSqlAzMan.SnapIn.Forms {
    public partial class frmCheckAccessTest : frmBase {
        private string _webApiUri;

        public IAzManStorage storage = null;
        public NetSqlAzMan.ServiceBusinessObjects.AzManStorage _storage = null;

        public IAzManApplication application = null;
        public NetSqlAzMan.ServiceBusinessObjects.AzManApplication _application = null;

        private IAzManDBUser dbuser = null;
        private NetSqlAzMan.ServiceBusinessObjects.AzManDBUser _dbuser = null;

        private Nullable<NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker.ADObject> _adObject = null;

        //private WindowsIdentity _wid = null;

        private Cache.UserPermissionCache cache = null;
        private Cache.StorageCache storageCache = null;


        #region Constructor
        public frmCheckAccessTest(string webApiUri) {
            _webApiUri = webApiUri;

            InitializeComponent();
        }
        #endregion


        #region Protected methods
        protected void hourGlass(bool switchOn) {
            this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
            /*Application.DoEvents();*/
        }

        protected void showError(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void showInfo(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void showWarning(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion


        #region Private methods
        private void refreshItemsHierarchy() {
            var _ihv = new frmItemsHierarchyView(_webApiUri, new ServiceBusinessObjects.AzManApplication[] { this._application });

            this.DialogResult = DialogResult.None;

            ImageList _clonedImageList = new ImageList();
            foreach (Image _image in _ihv.imageListItemHierarchyView.Images) {
                _clonedImageList.Images.Add((Image)_image.Clone());
            }
            this.checkAccessTestTreeView.ImageList = _clonedImageList;

            _ihv.BuildApplicationsTreeView();
            this.checkAccessTestTreeView.Nodes.Clear();
            this.checkAccessTestTreeView.Nodes.Add((TreeNode)_ihv.itemsHierarchyTreeView.Nodes[0].Clone());
            this.checkAccessTestTreeView.ExpandAll();
            _ihv.Dispose();
        }

        private void radioButtonCheckedChanged() {
            this.cache = null;

            this._dbuser = null;
            this._adObject = null;

            if (radbLdapEntry.Checked) {
                txtbDBUser.Enabled = !(txtbLdapEntry.Enabled = true);

                this.txtbDBUser.BackColor = SystemColors.Control;
                this.txtbLdapEntry.BackColor = SystemColors.Window;
            }
            else if (radbDBUser.Checked) {
                txtbLdapEntry.Enabled = !(txtbDBUser.Enabled = true);

                this.txtbDBUser.BackColor = SystemColors.Window;
                this.txtbLdapEntry.BackColor = SystemColors.Control;
            }
            else
                throw new InvalidOperationException("Tipo de validación no implementado.");

            this.txtbDBUser.Text = string.Empty;
            this.txtbLdapEntry.Text = string.Empty;
        }

        private void formValidate() {
            this.btnCheckAccessTest.Enabled = (_dbuser != null || _adObject != null);
        }

        private void writeIdentityDetails() {
            if (this._dbuser != null) {
                this.writeLineDetailMessage(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg60"));
                this.writeLineDetailMessage(String.Format("{0}: {1}\t{2}: {3}", Globalization.MultilanguageResource.GetString("ColumnHeader_Name"), this._dbuser.UserName, Globalization.MultilanguageResource.GetString("frmDBUsersList_lsvDBUsers_1.Text"), this._dbuser.CustomSid.StringValue));

                ServiceBusinessObjects.StructContent<bool> _flag = null;

                this.writeLineDetailMessage("Member of these Store Groups:");
                //Obtener todos los StoreGroups del Store
                IEnumerable<ServiceBusinessObjects.AzManStoreGroup> _storeGroups = null;
                #region Call Web Api
                var _sgh = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
                var _sghResult = Task.Run(() => _sgh.GetAllAsync(this._application.Store.Name, false)).Result;
                if (_sgh.IsResponseContentError(_sghResult))
                    _sgh.ThrowWebApiRequestException(_sghResult);
                else
                    _storeGroups = _sgh.GetEnumerableSBOFromReturnedContent(_sghResult);
                #endregion
                //Verificar, por cada StoreGroup, si el usuario es miembro				
                var _sgh2 = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.StructContent<bool>>(_webApiUri);
                Dictionary<string, IEnumerable<object>> _sgh2Result = null;
                foreach (var _sg in _storeGroups) {
                    #region Call Web Api
                    _sgh2Result = Task.Run(() => _sgh2.GetIsStoreGroupMemberAsync(this._application.Store.Name, _sg.Name, _dbuser.UserName, null)).Result;
                    if (_sgh2.IsResponseContentError(_sgh2Result))
                        _sgh2.ThrowWebApiRequestException(_sgh2Result);
                    else
                        _flag = _sgh2.GetSBOFromReturnedContent(_sgh2Result);
                    #endregion

                    if (_flag.Value)
                        this.writeLineDetailMessage(_sg.Name);

                    _sgh2Result.Clear();
                }


                this.writeLineDetailMessage("Member of these Application Groups:");
                //Obtener todos los ApplicationGroups del Store / Application
                IEnumerable<ServiceBusinessObjects.AzManApplicationGroup> _applicationGroups = null;
                #region Call Web Api
                var _agh = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
                var _aghResult = Task.Run(() => _agh.GetAllAsync(this._application.Store.Name, this._application.Name, true)).Result;
                if (_agh.IsResponseContentError(_aghResult))
                    _agh.ThrowWebApiRequestException(_aghResult);
                else
                    _applicationGroups = _agh.GetEnumerableSBOFromReturnedContent(_aghResult);
                #endregion
                //Verificar, por cada ApplicationGroup, si el usuario es miembro
                var _agh2 = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.StructContent<bool>>(_webApiUri);
                Dictionary<string, IEnumerable<object>> _agh2Result = null;
                foreach (var _ag in _applicationGroups) {
                    #region Call Web Api
                    _agh2Result = Task.Run(() => _agh2.GetIsApplicationGroupMemberAsync(this._application.Store.Name, this._application.Name, _ag.Name, _dbuser.UserName, null)).Result;
                    if (_agh2.IsResponseContentError(_agh2Result))
                        _agh2.ThrowWebApiRequestException(_agh2Result);
                    else
                        _flag = _agh2.GetSBOFromReturnedContent(_agh2Result);
                    #endregion

                    if (_flag.Value)
                        this.writeLineDetailMessage(_ag.Name);

                    _agh2Result.Clear();
                }
            }
            else if (this._adObject != null) {
                this.writeLineDetailMessage(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg60"));
                this.writeLineDetailMessage(String.Format("{0}: {1}\t{2}: {3}", Globalization.MultilanguageResource.GetString("ColumnHeader_Name"), this._adObject.Value.Name, Globalization.MultilanguageResource.GetString("frmDBUsersList_lsvDBUsers_1.Text"), this._adObject.Value.LdapSid));

                ServiceBusinessObjects.StructContent<bool> _flag = null;

                this.writeLineDetailMessage("Member of these Store Groups:");
                //Obtener todos los StoreGroups del Store
                IEnumerable<ServiceBusinessObjects.AzManStoreGroup> _storeGroups = null;
                #region Call Web Api
                var _sgh = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.AzManStoreGroup>(_webApiUri);
                var _sghResult = Task.Run(() => _sgh.GetAllAsync(this._application.Store.Name, false)).Result;
                if (_sgh.IsResponseContentError(_sghResult))
                    _sgh.ThrowWebApiRequestException(_sghResult);
                else
                    _storeGroups = _sgh.GetEnumerableSBOFromReturnedContent(_sghResult);
                #endregion
                //Verificar, por cada StoreGroup, si el usuario es miembro				
                var _sgh2 = new AzManWebApiClientHelpers.AzManStoreGroupsHelper<ServiceBusinessObjects.StructContent<bool>>(_webApiUri);
                Dictionary<string, IEnumerable<object>> _sgh2Result = null;
                foreach (var _sg in _storeGroups) {
                    #region Call Web Api
                    _sgh2Result = Task.Run(() => _sgh2.GetIsStoreGroupMemberAsync(this._application.Store.Name, _sg.Name, _adObject.Value.Name, _adObject.Value.DomainProfile)).Result;
                    if (_sgh2.IsResponseContentError(_sgh2Result))
                        _sgh2.ThrowWebApiRequestException(_sgh2Result);
                    else
                        _flag = _sgh2.GetSBOFromReturnedContent(_sgh2Result);
                    #endregion

                    if (_flag.Value)
                        this.writeLineDetailMessage(_sg.Name);

                    _sgh2Result.Clear();
                }


                this.writeLineDetailMessage("Member of these Application Groups:");
                //Obtener todos los ApplicationGroups del Store / Application
                IEnumerable<ServiceBusinessObjects.AzManApplicationGroup> _applicationGroups = null;
                #region Call Web Api
                var _agh = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.AzManApplicationGroup>(_webApiUri);
                var _aghResult = Task.Run(() => _agh.GetAllAsync(this._application.Store.Name, this._application.Name, true)).Result;
                if (_agh.IsResponseContentError(_aghResult))
                    _agh.ThrowWebApiRequestException(_aghResult);
                else
                    _applicationGroups = _agh.GetEnumerableSBOFromReturnedContent(_aghResult);
                #endregion
                //Verificar, por cada ApplicationGroup, si el usuario es miembro
                var _agh2 = new AzManWebApiClientHelpers.AzManApplicationGroupsHelper<ServiceBusinessObjects.StructContent<bool>>(_webApiUri);
                Dictionary<string, IEnumerable<object>> _agh2Result = null;
                foreach (var _ag in _applicationGroups) {
                    #region Call Web Api
                    _agh2Result = Task.Run(() => _agh2.GetIsApplicationGroupMemberAsync(this._application.Store.Name, this._application.Name, _ag.Name, _adObject.Value.Name, _adObject.Value.DomainProfile)).Result;
                    if (_agh2.IsResponseContentError(_agh2Result))
                        _agh2.ThrowWebApiRequestException(_agh2Result);
                    else
                        _flag = _agh2.GetSBOFromReturnedContent(_agh2Result);
                    #endregion

                    if (_flag.Value)
                        this.writeLineDetailMessage(_ag.Name);

                    _agh2Result.Clear();
                }
            }
        }

        private void checkAccessTest(TreeNode itemNode, Boolean turboMode, Hashtable resultCache, Hashtable attributesCache) {
            string sItemType = String.Empty;
            switch (itemNode.ImageIndex) {
                case 3: sItemType = Globalization.MultilanguageResource.GetString("Domain_Role"); break;
                case 4: sItemType = Globalization.MultilanguageResource.GetString("Domain_Task"); break;
                case 5: sItemType = Globalization.MultilanguageResource.GetString("Domain_Operation"); break;
            }

            var _auth = ServiceBusinessObjects.AuthorizationType.Neutral;
            //AuthorizationType auth = AuthorizationType.Neutral;
            string _authName = String.Empty;
            Stopwatch _elapsed = new Stopwatch();

            List<KeyValuePair<string, string>> _attributes = null;

            //Cache Build
            if (this.chkCache.Checked && this.cache == null) {
                this.writeDetailMessage("Building UserPermissionCache ...");

                throw new NotImplementedException("El uso de UserPermissionCache no está habilitado en esta versión del software.");

                if (this.dbuser != null) {
                    this.cache = new NetSqlAzMan.Cache.UserPermissionCache(this.application.Store.Storage, this.application.Store.Name, this.application.Name, this.dbuser, true, true);
                }
                _elapsed.Stop();
                this.writeLineDetailMessage(String.Format("[{0} mls.]\r\n", _elapsed.ElapsedMilliseconds));
            }
            else if (this.chkStorageCache.Checked && this.storageCache == null) {
                this.writeDetailMessage("Building StorageCache ...");

                throw new NotImplementedException("El uso de StorageCache no está habilitado en esta versión del software.");

                this.storageCache = new NetSqlAzMan.Cache.StorageCache(this.application.Store.Storage.ConnectionString);
                this.storageCache.BuildStorageCache(this.application.Store.Name, application.Name);
                _elapsed.Stop();
                this.writeLineDetailMessage(String.Format("[{0} mls.]\r\n", _elapsed.ElapsedMilliseconds));
            }

            _elapsed.Restart();
            this.writeDetailMessage(String.Format("{0} {1} '{2}' ... ", Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg70"), sItemType, itemNode.Text));
            try {
                var _itemName = itemNode.Text;
                if (!resultCache.ContainsKey(_itemName)) {
                    //if (this._wid != null) {
                    //	throw new InvalidOperationException("Las versión actual y las versiones posteriores de este software, no soportarán más la validación de usuarios Principal.WindowsIdentity.");

                    // //Código no soportado nunca más.

                    //	//if (this.chkCache.Checked) {
                    //	//	auth = this.cache.CheckAccess(_itemName, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, out _attributes);
                    //	//}
                    //	//else if (this.chkStorageCache.Checked) {
                    //	//	IAzManItem item = (IAzManItem)itemNode.Tag;
                    //	//	auth = this.storageCache.CheckAccess(item.Application.Store.Name, item.Application.Name, item.Name, this._wid.GetUserBinarySSid(), this._wid.GetGroupsBinarySSid(), this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, false, out _attributes);
                    //	//}
                    //	//else {
                    //	//	auth = this.application.Store.Storage.CheckAccess(this.application.Store.Name, this.application.Name, _itemName, this._wid, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, false, out _attributes);
                    //	//}
                    //}

                    var _sah = new AzManWebApiClientHelpers.AzManStorageAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationInfo>(_webApiUri);

                    if (this._dbuser != null) {
                        if (this.chkCache.Checked) {
                            throw new NotImplementedException("El uso de UserPermissionCache no está habilitado en esta versión del software.");
                            //auth = this.cache.CheckAccess(tn.Text, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, out attributes);
                        }
                        else if (this.chkStorageCache.Checked) {
                            throw new NotImplementedException("El uso de StorageCache no está habilitado en esta versión del software.");
                            //IAzManItem item = (IAzManItem)tn.Tag;
                            //auth = this.storageCache.CheckAccess(item.Application.Store.Name, item.Application.Name, item.Name, this.dbuser.CustomSid.StringValue, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, false);
                        }
                        else {
                            ////OLD VERSION CODE
                            //auth = this.application.Store.Storage.CheckAccess(this.application.Store.Name, this.application.Name, itemNode.Text, this.dbuser, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, false, out attributes);

                            var _result = Task.Run(() => _sah.CheckAccessAsync(this._application.Store.Name, this._application.Name, _itemName, null, _dbuser.UserName)).Result;
                            if (_sah.IsResponseContentError(_result))
                                _sah.ThrowWebApiRequestException(_result);
                            else
                                _auth = _sah.GetSBOFromReturnedContent(_result).AuthorizationType;
                        }
                    }
                    else if (_adObject != null) {
                        if (this.chkCache.Checked) {
                            throw new NotImplementedException("El uso de UserPermissionCache no está habilitado en esta versión del software.");
                            //auth = this.cache.CheckAccess(tn.Text, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, out attributes);
                        }
                        else if (this.chkStorageCache.Checked) {
                            throw new NotImplementedException("El uso de StorageCache no está habilitado en esta versión del software.");
                            //IAzManItem item = (IAzManItem)tn.Tag;
                            //auth = this.storageCache.CheckAccess(item.Application.Store.Name, item.Application.Name, item.Name, this.dbuser.CustomSid.StringValue, this.dtValidFor.Checked ? this.dtValidFor.Value : DateTime.Now, false);
                        }
                        else {
                            var _result = Task.Run(() => {
                                return _sah.CheckAccessAsync(this._application.Store.Name, this._application.Name, _itemName, _adObject.Value.DomainProfile, _adObject.Value.Name);
                            }).Result;
                            if (_sah.IsResponseContentError(_result))
                                _sah.ThrowWebApiRequestException(_result);
                            else
                                _auth = _sah.GetSBOFromReturnedContent(_result).AuthorizationType;
                        }
                    }
                    else
                        throw new InvalidOperationException("No se ha seleccionado algún usuario, no se puede realizar la operación.");

                    resultCache.Add(_itemName, _auth);
                    attributesCache.Add(_itemName, _attributes);
                }
                else {
                    _auth = (ServiceBusinessObjects.AuthorizationType)resultCache[_itemName];
                    _attributes = (List<KeyValuePair<String, String>>)attributesCache[_itemName];
                }
                _elapsed.Stop();
                _authName = Globalization.MultilanguageResource.GetString("Domain_Neutral");
                switch (_auth) {
                    case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation:
                        _authName = Globalization.MultilanguageResource.GetString("Domain_AllowWithDelegation");
                        itemNode.BackColor = Color.SkyBlue;
                        break;
                    case ServiceBusinessObjects.AuthorizationType.Allow:
                        _authName = Globalization.MultilanguageResource.GetString("Domain_Allow");
                        itemNode.BackColor = Color.LightGreen;
                        break;
                    case ServiceBusinessObjects.AuthorizationType.Deny:
                        _authName = Globalization.MultilanguageResource.GetString("Domain_Deny");
                        itemNode.BackColor = Color.LightSalmon;
                        break;
                    case ServiceBusinessObjects.AuthorizationType.Neutral:
                        _authName = Globalization.MultilanguageResource.GetString("Domain_Neutral");
                        itemNode.BackColor = Color.Gainsboro;
                        break;
                }
                this.writeLineDetailMessage(String.Format("{0} [{1} mls.]", _authName, _elapsed.ElapsedMilliseconds));
                if (_attributes != null && _attributes.Count > 0) {
                    this.writeLineDetailMessage(String.Format(" {0} attribute(s) found:", _attributes.Count));
                    int attributeIndex = 0;
                    foreach (KeyValuePair<string, string> attr in _attributes) {
                        this.writeLineDetailMessage(String.Format("  {0}) Key: {1} Value: {2}", ++attributeIndex, attr.Key, attr.Value));
                    }
                }
            }
            catch (Exception ex) {
                _authName = Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10");
                this.writeLineDetailMessage(String.Format("{0} [{1} mls.]", ex.Message, _elapsed.ElapsedMilliseconds));
            }
            itemNode.Text = String.Format("{0} - ({1})", itemNode.Text, _authName.ToUpper());
            if (!turboMode) {
                itemNode.EnsureVisible();
                Application.DoEvents();
            }
            foreach (TreeNode tnChild in itemNode.Nodes) {
                this.checkAccessTest(tnChild, turboMode, resultCache, attributesCache);
            }
        }

        private void writeDetailMessage(string message) {
            this.txtDetails.AppendText(message);
            //this.lblMessage.Text = message.Replace("\r\n", "");
            //this.txtDetails.SelectionStart = this.txtDetails.Text.Length;
            //this.txtDetails.ScrollToCaret();
            /*Application.DoEvents();*/
        }

        private void writeLineDetailMessage(string message) {
            this.writeDetailMessage(message + "\r\n");
        }
        #endregion


        #region Event handlers
        internal void frmCheckAccessTest_Load(object sender, EventArgs e) {
            this.DialogResult = DialogResult.None;

            ImageList clonedImageList = new ImageList();
            foreach (Image image in this.imageListItemHierarchyView.Images) {
                clonedImageList.Images.Add((Image)image.Clone());
            }
            this.checkAccessTestTreeView.ImageList = clonedImageList;

            /*Application.DoEvents();*/
            NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);

            //this._wid = ((System.Threading.Thread.CurrentPrincipal.Identity as WindowsIdentity) ?? WindowsIdentity.GetCurrent());
            //NTAccount nta = (NTAccount)this._wid.User.Translate(typeof(NTAccount));
            //string currentUpnName = nta.Value;
            //if (currentUpnName.IndexOf('\\') != -1) {
            //	currentUpnName = currentUpnName.Substring(currentUpnName.IndexOf('\\') + 1);
            //}

            this.lblUPNUser.Text = "(otheruser@domain.ext)";
            this.lblMessage.Text = "...";
            this.butnBrowseLDAPEntry.Text = "...";
            this.butnBrowseDBUser.Text = "...";
            this.chkCache.Text = "UserPermissionCache";
            this.chkStorageCache.Text = "StorageCache";
            this.groupBox1.Text = " " + Globalization.MultilanguageResource.GetString("frmCheckAccessTest.Text") + " ";

            this.radioButtonCheckedChanged();

            this.formValidate();
        }

        private void btnBrowseDBUser_Click(object sender, EventArgs e) {
            try {
                this.radbDBUser.Checked = true;

                var frm = new frmDBUsersList(_webApiUri);
                frm._application = this._application;
                var dr = frm.ShowDialog(this);
                if (dr == DialogResult.OK) {
                    if (frm._selectedDBUsers.Length > 1) {
                        this.showError(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg80"), Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
                    }
                    if (frm._selectedDBUsers.Length == 1) {
                        this._adObject = null;

                        this._dbuser = frm._selectedDBUsers[0];
                        this.txtbDBUser.Text = string.Format("{0} ({1})", this._dbuser.UserName, this._dbuser.DisplayName);
                    }
                }
            }
            catch (Exception ex) {
                this.showError(ex.Message, Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
            }
            finally {
                this.formValidate();
            }

            //REFACTORING 

            //try {
            //	this.rbDBUser.Checked = true;
            //	frmDBUsersList frm = new frmDBUsersList();
            //	frm.application = this.application;
            //	DialogResult dr = frm.ShowDialog(this);
            //	if (dr == DialogResult.OK) {
            //		if (frm.selectedDBUsers.Length > 1) {
            //			this.ShowError(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg80"), Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
            //		}
            //		if (frm.selectedDBUsers.Length == 1) {
            //			this.wid = null;
            //			this.dbuser = frm.selectedDBUsers[0];
            //			this.txtDBUser.Text = this.dbuser.UserName;
            //		}
            //	}
            //}
            //catch (Exception ex) {
            //	this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
            //}
            //finally {
            //	this.FormValidate();
            //}
        }

        private void txtWindowsUser_TextChanged(object sender, EventArgs e) {
            this.formValidate();
        }

        private void chkCache_CheckedChanged(object sender, EventArgs e) {
            if (this.chkCache.Checked && this.chkStorageCache.Checked)
                this.chkStorageCache.Checked = false;
            this.cache = null;
        }

        private void chkStorageCache_CheckedChanged(object sender, EventArgs e) {
            if (this.chkStorageCache.Checked && this.chkCache.Checked)
                this.chkCache.Checked = false;
            this.cache = null;
        }

        private void btnCheckAccessTest_Click(object sender, EventArgs e) {
            try {
                if (_dbuser == null && _adObject == null)
                    throw new InvalidOperationException("Debe de seleccionar el tipo de usuario a validar.");

                this.hourGlass(true);
                this.txtDetails.Text = String.Empty;
                this.btnCheckAccessTest.Enabled = this.btnClose.Enabled = this.butnBrowseDBUser.Enabled = this.radbDBUser.Enabled = this.butnBrowseLDAPEntry.Enabled = this.radbLdapEntry.Enabled = false;
                this.writeLineDetailMessage(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg30") + " " + DateTime.Now.ToString());
                this.writeLineDetailMessage(String.Empty);
                this.writeIdentityDetails();
                this.writeLineDetailMessage(String.Empty);
                this.writeDetailMessage(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg40"));
                this.checkAccessTestTreeView.BeginUpdate();
                this.refreshItemsHierarchy();
                this.checkAccessTestTreeView.EndUpdate();
                Application.DoEvents();
                this.writeLineDetailMessage(Globalization.MultilanguageResource.GetString("Done_Msg10"));
                this.writeLineDetailMessage(String.Empty);
                TreeNode applicationTreeNode = this.checkAccessTestTreeView.Nodes[0].Nodes[0].Nodes[0];
                Boolean turboMode = this._application.Items.Count() > 50;
                Hashtable resultCache = new Hashtable();
                Hashtable attributesCache = new Hashtable();

                if (turboMode)
                    this.checkAccessTestTreeView.BeginUpdate();

                foreach (TreeNode itemTreeNode in applicationTreeNode.Nodes) {
                    this.checkAccessTest(itemTreeNode, turboMode, resultCache, attributesCache);
                    //#region Evaluar solo el primer nodo
                    //MessageBox.Show("Por motivos de pruebas, solo se está verificando el primer nodo; el resto se obvia. Se puede remover esta limitación en el código.\r\nEsto solo aplica en modo DEBUG.");
                    //break;r
                    //#endregion
                }

                if (turboMode)
                    this.checkAccessTestTreeView.EndUpdate();

                this.writeLineDetailMessage(String.Empty);
                this.writeLineDetailMessage(Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg50") + " " + DateTime.Now.ToString());
            }
            catch (Exception ex) {
                this.showError(ex.Message, Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
            }
            finally {
                this.btnCheckAccessTest.Enabled = this.btnClose.Enabled =
                      this.butnBrowseDBUser.Enabled = this.radbDBUser.Enabled = this.butnBrowseLDAPEntry.Enabled = this.radbLdapEntry.Enabled = true;
                this.hourGlass(false);
            }
        }

        private void radbDBUser_CheckedChanged(object sender, EventArgs e) {
            this.radioButtonCheckedChanged();
        }

        private void radbLdapEntry_CheckedChanged(object sender, EventArgs e) {
            this.radioButtonCheckedChanged();
        }

        private void radbWindowsUser_CheckedChanged(object sender, EventArgs e) {
            this.radioButtonCheckedChanged();
        }

        private void frmCheckAccessTest_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult == DialogResult.None)
                this.DialogResult = DialogResult.Cancel;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

        private void butnBrowseLDAPEntry_Click(object sender, EventArgs e) {
            try {
                var _f = new AzManWinUI.LDAPServices.LdapWebApiUserAndGroupSearchUI(_webApiUri);
                if (_f.ShowDialog() != DialogResult.OK)
                    return;

                _dbuser = null;
                _adObject = _f.SelectedADObjects.First();

                txtbLdapEntry.Text = string.Format("{0}\\{1}", _adObject.Value.DomainProfile, _adObject.Value.Name);
            }
            catch (Exception ex) {
                this.showError(ex.Message, Globalization.MultilanguageResource.GetString("frmCheckAccessTest_Msg10"));
            }
            finally {
                this.formValidate();
            }
        }
        #endregion
    }
}