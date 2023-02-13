using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSqlAzMan.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace NetSqlAzMan.SnapIn.Forms
{
	public partial class frmItemsHierarchyView :frmBase
	{
		private string _webApiUri = null;
		private IEnumerable<ServiceBusinessObjects.AzManApplication> _applications = null;

		private bool firstShow = true;
		private bool closeRequest = false;

		public frmItemsHierarchyView(string webApiUri, IEnumerable<ServiceBusinessObjects.AzManApplication> applications) {
			_webApiUri = webApiUri;
			_applications = applications;

			if (this._applications != null && this._applications.Count() > 0) {
				var _ih = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
				foreach (ServiceBusinessObjects.AzManApplication app in this._applications) {
					if (app.Items.Any())
						continue;

					IEnumerable<ServiceBusinessObjects.AzManItem> _items = null;
					#region Call Web Api
					var _ihResult = Task.Run(() => _ih.GetItemsAsync(app.Store.Name, app.Name, ServiceBusinessObjects.ItemType.All, false, true, true, false)).Result;
					if (_ih.IsResponseContentError(_ihResult))
						_ih.ThrowWebApiRequestException(_ihResult);
					else 
						_items = _ih.GetEnumerableSBOFromReturnedContent(_ihResult);
					#endregion
					app.Items = _items;
				}
			}

			InitializeComponent();
		}

		internal void frmNetSqlAzManBase_Load(object sender, EventArgs e) {
			this.DialogResult = DialogResult.None;
			ImageList clonedImageList = new ImageList();
			foreach (Image image in this.imageListItemHierarchyView.Images) {
				clonedImageList.Images.Add((Image)image.Clone());
			}

			this.itemsHierarchyTreeView.ImageList = clonedImageList;
			/*Application.DoEvents();*/
			NetSqlAzMan.SnapIn.Globalization.ResourcesManager.CollectResources(this);
		}

		protected void HourGlass(bool switchOn) {
			this.Cursor = switchOn ? Cursors.WaitCursor : Cursors.Arrow;
			/*Application.DoEvents();*/
		}

		protected void ShowError(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		protected void ShowInfo(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		protected void ShowWarning(string text, string caption) {
			MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void frmNetSqlAzManBase_FormClosing(object sender, FormClosingEventArgs e) {
			if (this.DialogResult == DialogResult.None)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnClose_Click(object sender, EventArgs e) {
			this.closeRequest = true;
			this.DialogResult = DialogResult.OK;
		}

		internal bool findNode(TreeNode startingNode, string text) {
			foreach (TreeNode childNode in startingNode.Nodes) {
				if (childNode.Text.Equals(text))
					return true;
				if (this.findNode(childNode, text))
					return true;
			}
			return false;
		}

		internal TreeNode findTreeNode(TreeNode startingNode, string text) {
			if (String.Compare(startingNode.Text, text, true) == 0)
				return startingNode;
			foreach (TreeNode childNode in startingNode.Nodes) {
				TreeNode result = this.findTreeNode(childNode, text);
				if (result != null)
					return result;
			}
			return null;
		}

		internal void BuildApplicationsTreeView() {
			if (this._applications != null && this._applications.Count() > 0) {
				var _arrApps = _applications.ToArray();
				this.itemsHierarchyTreeView.Nodes.Clear();
				TreeNode root = new TreeNode(_arrApps[0].Store.Storage.Description, 0, 0);
				root.ToolTipText = ".NET Sql Authorization Manager";
				this.itemsHierarchyTreeView.Nodes.Add(root);
				TreeNode storeNode = new TreeNode(_arrApps[0].Store.Name, 1, 1);
				root.Nodes.Add(storeNode);
				root.Expand();
				storeNode.Expand();
				/*Application.DoEvents();*/
				storeNode.ToolTipText = _arrApps[0].Store.Description;
				this.pb.Value = 0;
				this.pb.Minimum = 0;
				this.pb.Maximum = _arrApps.Length - 1;
				this.pb.Visible = true;
				this.lblStatus.Visible = true;
				for (int i = 0; i < _arrApps.Length; i++) {
					if (this.closeRequest) {
						return;
					}
					this.pb.Value = i;
					/*Application.DoEvents();*/
					this.addApplicationNode(storeNode, _arrApps[i]);
					storeNode.Expand();
					/*Application.DoEvents();*/
				}
				root.ExpandAll();
				this.pb.Visible = false;
				this.lblStatus.Visible = false;
			}
		}

		private void addApplicationNode(TreeNode parentNode, ServiceBusinessObjects.AzManApplication azManApp) {
			TreeNode appNode = new TreeNode(azManApp.Name, 2, 2);
			appNode.ToolTipText = azManApp.Description;
			appNode.Tag = azManApp;
			parentNode.Nodes.Add(appNode);
			appNode.Expand();

			var _itemsWithoutParent = azManApp.Items.Where(_i => !_i.ItemsWhereIAmAMember.Any());

			foreach (ServiceBusinessObjects.AzManItem item in _itemsWithoutParent.Where(_i => _i.ItemType == ServiceBusinessObjects.ItemType.Role)) {
				this.addRoleNode(appNode, item, azManApp);
				/*Application.DoEvents();*/
			}

			foreach (ServiceBusinessObjects.AzManItem item in _itemsWithoutParent.Where(_i => _i.ItemType == ServiceBusinessObjects.ItemType.Task)) {
				this.addTaskNode(appNode, item, azManApp);
				/*Application.DoEvents();*/
			}

			foreach (ServiceBusinessObjects.AzManItem item in _itemsWithoutParent.Where(_i => _i.ItemType == ServiceBusinessObjects.ItemType.Operation)) {
				this.addOperationNode(appNode, item, azManApp);
				/*Application.DoEvents();*/
			}

			appNode.Collapse();
		}

		private void addRoleNode(TreeNode parentNode, ServiceBusinessObjects.AzManItem roleItem, ServiceBusinessObjects.AzManApplication azManApp) {
			TreeNode roleNode = new TreeNode(roleItem.Name, 3, 3);
			roleNode.ToolTipText = roleItem.Description;
			roleNode.Tag = roleItem;

			parentNode.Nodes.Add(roleNode);

			var _q = from children in azManApp.Items
						where children.ItemsWhereIAmAMember.Any(f => f.ItemId == roleItem.ItemId)
						select children;

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Role)) {
				this.addRoleNode(roleNode, subItem, azManApp);
				roleNode.Expand();
				/*Application.DoEvents();*/
			}

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Task)) {
				this.addTaskNode(roleNode, subItem, azManApp);
				roleNode.Expand();
				/*Application.DoEvents();*/
			}

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Operation)) {
				this.addOperationNode(roleNode, subItem, azManApp);
				roleNode.Expand();
				/*Application.DoEvents();*/
			}
		}

		private void addTaskNode(TreeNode parentNode, ServiceBusinessObjects.AzManItem taskItem, ServiceBusinessObjects.AzManApplication azManApp) {
			TreeNode taskNode = new TreeNode(taskItem.Name, 4, 4);
			taskNode.ToolTipText = taskItem.Description;
			taskNode.Tag = taskItem;

			parentNode.Nodes.Add(taskNode);

			var _q = from children in azManApp.Items
						where children.ItemsWhereIAmAMember.Any(f => f.ItemId == taskItem.ItemId)
						select children;

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Task)) {
				this.addTaskNode(taskNode, subItem, azManApp);
				taskNode.Expand();
				/*Application.DoEvents();*/
			}

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Operation)) {
				this.addOperationNode(taskNode, subItem, azManApp);
				taskNode.Expand();
				/*Application.DoEvents();*/
			}
		}

		private void addOperationNode(TreeNode parentNode, ServiceBusinessObjects.AzManItem operationItem, ServiceBusinessObjects.AzManApplication azManApp) {
			TreeNode operationNode = new TreeNode(operationItem.Name, 5, 5);
			operationNode.ToolTipText = operationItem.Description;
			operationNode.Tag = operationItem;

			parentNode.Nodes.Add(operationNode);

			var _q = from children in azManApp.Items
						where children.ItemsWhereIAmAMember.Any(f => f.ItemId == operationItem.ItemId)
						select children;

			foreach (var subItem in _q.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Operation)) {
				this.addOperationNode(operationNode, subItem, azManApp);
				operationNode.Expand();
				/*Application.DoEvents();*/
			}
		}

		private void frmItemsHierarchyView_Activated(object sender, EventArgs e) {
			if (this.firstShow) {
				this.firstShow = false;
				try {
					this.BuildApplicationsTreeView();
				}
				catch (Exception ex) {
					this.ShowError(ex.Message, Globalization.MultilanguageResource.GetString("frmItemsHierarchyView_Msg10"));
					this.DialogResult = DialogResult.Cancel;
					this.Close();
				}
			}
		}
	}
}