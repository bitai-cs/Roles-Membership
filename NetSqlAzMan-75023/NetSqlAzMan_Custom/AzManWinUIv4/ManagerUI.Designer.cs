namespace AzManWinUI {
	partial class ManagerUI {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerUI));
            this.toolsMain = new System.Windows.Forms.ToolStrip();
            this.stasMain = new System.Windows.Forms.StatusStrip();
            this.splcMain = new System.Windows.Forms.SplitContainer();
            this.imgl16x16n = new System.Windows.Forms.ImageList(this.components);
            this.imgl48x48n = new System.Windows.Forms.ImageList(this.components);
            this.imgl24x24n = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imgl32x32n = new System.Windows.Forms.ImageList(this.components);
            this.ctxmMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tvieTree = new Basgosoft.ManagementConsoleLib.BaseTreeView();
            this.lvieNodeDetail = new AzManWinUI.ListView.ManagerListView();
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).BeginInit();
            this.splcMain.Panel1.SuspendLayout();
            this.splcMain.Panel2.SuspendLayout();
            this.splcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolsMain
            // 
            this.toolsMain.AutoSize = false;
            this.toolsMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolsMain.Location = new System.Drawing.Point(503, 0);
            this.toolsMain.Name = "toolsMain";
            this.toolsMain.Size = new System.Drawing.Size(176, 389);
            this.toolsMain.TabIndex = 30;
            this.toolsMain.Text = "Actions";
            // 
            // stasMain
            // 
            this.stasMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stasMain.Location = new System.Drawing.Point(0, 389);
            this.stasMain.Name = "stasMain";
            this.stasMain.Size = new System.Drawing.Size(884, 22);
            this.stasMain.TabIndex = 2;
            this.stasMain.Text = "statusStrip1";
            // 
            // splcMain
            // 
            this.splcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splcMain.Location = new System.Drawing.Point(0, 0);
            this.splcMain.Name = "splcMain";
            // 
            // splcMain.Panel1
            // 
            this.splcMain.Panel1.Controls.Add(this.tvieTree);
            this.splcMain.Panel1MinSize = 200;
            // 
            // splcMain.Panel2
            // 
            this.splcMain.Panel2.Controls.Add(this.lvieNodeDetail);
            this.splcMain.Panel2.Controls.Add(this.splitter1);
            this.splcMain.Panel2.Controls.Add(this.toolsMain);
            this.splcMain.Size = new System.Drawing.Size(884, 389);
            this.splcMain.SplitterDistance = 200;
            this.splcMain.SplitterWidth = 5;
            this.splcMain.TabIndex = 3;
            this.splcMain.TabStop = false;
            // 
            // imgl16x16n
            // 
            this.imgl16x16n.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl16x16n.ImageStream")));
            this.imgl16x16n.TransparentColor = System.Drawing.Color.Transparent;
            this.imgl16x16n.Images.SetKeyName(0, "warning");
            this.imgl16x16n.Images.SetKeyName(1, "root");
            this.imgl16x16n.Images.SetKeyName(2, "memberships");
            this.imgl16x16n.Images.SetKeyName(3, "user");
            this.imgl16x16n.Images.SetKeyName(4, "store");
            this.imgl16x16n.Images.SetKeyName(5, "storeGroups");
            this.imgl16x16n.Images.SetKeyName(6, "application");
            this.imgl16x16n.Images.SetKeyName(7, "applicationGroups");
            this.imgl16x16n.Images.SetKeyName(8, "storeGroupLDAP");
            this.imgl16x16n.Images.SetKeyName(9, "storeGroupBasic");
            this.imgl16x16n.Images.SetKeyName(10, "items");
            this.imgl16x16n.Images.SetKeyName(11, "roleItems");
            this.imgl16x16n.Images.SetKeyName(12, "taskItems");
            this.imgl16x16n.Images.SetKeyName(13, "operationItems");
            this.imgl16x16n.Images.SetKeyName(14, "authorizations");
            this.imgl16x16n.Images.SetKeyName(15, "taskAuthorizations");
            this.imgl16x16n.Images.SetKeyName(16, "roleAuthorizations");
            this.imgl16x16n.Images.SetKeyName(17, "operationAuthorizations");
            this.imgl16x16n.Images.SetKeyName(18, "task");
            this.imgl16x16n.Images.SetKeyName(19, "role");
            this.imgl16x16n.Images.SetKeyName(20, "operation");
            this.imgl16x16n.Images.SetKeyName(21, "applicationGroupLDAP");
            this.imgl16x16n.Images.SetKeyName(22, "applicationGroupBasic");
            this.imgl16x16n.Images.SetKeyName(23, "windowsUser");
            this.imgl16x16n.Images.SetKeyName(24, "windowsGroup");
            this.imgl16x16n.Images.SetKeyName(25, "databaseUser");
            this.imgl16x16n.Images.SetKeyName(26, "userForbidden");
            // 
            // imgl48x48n
            // 
            this.imgl48x48n.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl48x48n.ImageStream")));
            this.imgl48x48n.TransparentColor = System.Drawing.Color.Transparent;
            this.imgl48x48n.Images.SetKeyName(0, "warning");
            this.imgl48x48n.Images.SetKeyName(1, "root");
            this.imgl48x48n.Images.SetKeyName(2, "memberships");
            this.imgl48x48n.Images.SetKeyName(3, "user");
            this.imgl48x48n.Images.SetKeyName(4, "store");
            this.imgl48x48n.Images.SetKeyName(5, "storeGroups");
            this.imgl48x48n.Images.SetKeyName(6, "storeGroupLDAP");
            this.imgl48x48n.Images.SetKeyName(7, "storeGroupBasic");
            this.imgl48x48n.Images.SetKeyName(8, "application");
            this.imgl48x48n.Images.SetKeyName(9, "applicationGroups");
            this.imgl48x48n.Images.SetKeyName(10, "applicationGroupLDAP");
            this.imgl48x48n.Images.SetKeyName(11, "applicationGroupBasic");
            this.imgl48x48n.Images.SetKeyName(12, "items");
            this.imgl48x48n.Images.SetKeyName(13, "roleItems");
            this.imgl48x48n.Images.SetKeyName(14, "taskItems");
            this.imgl48x48n.Images.SetKeyName(15, "operationItems");
            this.imgl48x48n.Images.SetKeyName(16, "authorizations");
            this.imgl48x48n.Images.SetKeyName(17, "roleAuthorizations");
            this.imgl48x48n.Images.SetKeyName(18, "taskAuthorizations");
            this.imgl48x48n.Images.SetKeyName(19, "operationAuthorizations");
            this.imgl48x48n.Images.SetKeyName(20, "role");
            this.imgl48x48n.Images.SetKeyName(21, "task");
            this.imgl48x48n.Images.SetKeyName(22, "operation");
            this.imgl48x48n.Images.SetKeyName(23, "windowsUser");
            this.imgl48x48n.Images.SetKeyName(24, "windowsGroup");
            this.imgl48x48n.Images.SetKeyName(25, "databaseUser");
            this.imgl48x48n.Images.SetKeyName(26, "userForbidden");
            // 
            // imgl24x24n
            // 
            this.imgl24x24n.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl24x24n.ImageStream")));
            this.imgl24x24n.TransparentColor = System.Drawing.Color.Transparent;
            this.imgl24x24n.Images.SetKeyName(0, "warning");
            this.imgl24x24n.Images.SetKeyName(1, "root");
            this.imgl24x24n.Images.SetKeyName(2, "memberships");
            this.imgl24x24n.Images.SetKeyName(3, "user");
            this.imgl24x24n.Images.SetKeyName(4, "store");
            this.imgl24x24n.Images.SetKeyName(5, "storeGroups");
            this.imgl24x24n.Images.SetKeyName(6, "storeGroupLDAP");
            this.imgl24x24n.Images.SetKeyName(7, "storeGroupBasic");
            this.imgl24x24n.Images.SetKeyName(8, "application");
            this.imgl24x24n.Images.SetKeyName(9, "applicationGroups");
            this.imgl24x24n.Images.SetKeyName(10, "items");
            this.imgl24x24n.Images.SetKeyName(11, "roleItems");
            this.imgl24x24n.Images.SetKeyName(12, "taskItems");
            this.imgl24x24n.Images.SetKeyName(13, "operationItems");
            this.imgl24x24n.Images.SetKeyName(14, "authorizations");
            this.imgl24x24n.Images.SetKeyName(15, "taskAuthorizations");
            this.imgl24x24n.Images.SetKeyName(16, "roleAuthorizations");
            this.imgl24x24n.Images.SetKeyName(17, "operationAuthorizations");
            this.imgl24x24n.Images.SetKeyName(18, "task");
            this.imgl24x24n.Images.SetKeyName(19, "role");
            this.imgl24x24n.Images.SetKeyName(20, "operation");
            this.imgl24x24n.Images.SetKeyName(21, "applicationGroupLDAP");
            this.imgl24x24n.Images.SetKeyName(22, "applicationGroupBasic");
            this.imgl24x24n.Images.SetKeyName(23, "windowsUser");
            this.imgl24x24n.Images.SetKeyName(24, "windowsGroup");
            this.imgl24x24n.Images.SetKeyName(25, "databaseUser");
            this.imgl24x24n.Images.SetKeyName(26, "userForbidden");
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(500, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 389);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // imgl32x32n
            // 
            this.imgl32x32n.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl32x32n.ImageStream")));
            this.imgl32x32n.TransparentColor = System.Drawing.Color.Transparent;
            this.imgl32x32n.Images.SetKeyName(0, "warning");
            this.imgl32x32n.Images.SetKeyName(1, "root");
            this.imgl32x32n.Images.SetKeyName(2, "memberships");
            this.imgl32x32n.Images.SetKeyName(3, "user");
            this.imgl32x32n.Images.SetKeyName(4, "store");
            this.imgl32x32n.Images.SetKeyName(5, "storeGroups");
            this.imgl32x32n.Images.SetKeyName(6, "storeGroupLDAP");
            this.imgl32x32n.Images.SetKeyName(7, "storeGroupBasic");
            this.imgl32x32n.Images.SetKeyName(8, "application");
            this.imgl32x32n.Images.SetKeyName(9, "applicationGroups");
            this.imgl32x32n.Images.SetKeyName(10, "items");
            this.imgl32x32n.Images.SetKeyName(11, "roleItems");
            this.imgl32x32n.Images.SetKeyName(12, "taskItems");
            this.imgl32x32n.Images.SetKeyName(13, "operationItems");
            this.imgl32x32n.Images.SetKeyName(14, "authorizations");
            this.imgl32x32n.Images.SetKeyName(15, "roleAuthorizations");
            this.imgl32x32n.Images.SetKeyName(16, "taskAuthorizations");
            this.imgl32x32n.Images.SetKeyName(17, "operationAuthorizations");
            this.imgl32x32n.Images.SetKeyName(18, "role");
            this.imgl32x32n.Images.SetKeyName(19, "task");
            this.imgl32x32n.Images.SetKeyName(20, "operation");
            this.imgl32x32n.Images.SetKeyName(21, "applicationGroupLDAP");
            this.imgl32x32n.Images.SetKeyName(22, "applicationGroupBasic");
            this.imgl32x32n.Images.SetKeyName(23, "windowsUser");
            this.imgl32x32n.Images.SetKeyName(24, "windowsGroup");
            this.imgl32x32n.Images.SetKeyName(25, "databaseUser");
            this.imgl32x32n.Images.SetKeyName(26, "userForbidden");
            // 
            // ctxmMenu
            // 
            this.ctxmMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxmMenu.Name = "ctxmMenu";
            this.ctxmMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // tvieTree
            // 
            this.tvieTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvieTree.HideSelection = false;
            this.tvieTree.ImageIndex = 0;
            this.tvieTree.ImageList = this.imgl16x16n;
            this.tvieTree.Indent = 15;
            this.tvieTree.ItemHeight = 20;
            this.tvieTree.Location = new System.Drawing.Point(0, 0);
            this.tvieTree.ManagerListView = this.lvieNodeDetail;
            this.tvieTree.Name = "tvieTree";
            this.tvieTree.SelectedImageIndex = 0;
            this.tvieTree.Size = new System.Drawing.Size(200, 389);
            this.tvieTree.TabIndex = 0;
            // 
            // lvieNodeDetail
            // 
            this.lvieNodeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvieNodeDetail.FullRowSelect = true;
            this.lvieNodeDetail.GridLines = true;
            this.lvieNodeDetail.HideSelection = false;
            this.lvieNodeDetail.LargeImageList = this.imgl48x48n;
            this.lvieNodeDetail.Location = new System.Drawing.Point(0, 0);
            this.lvieNodeDetail.Name = "lvieNodeDetail";
            this.lvieNodeDetail.ParentNode = null;
            this.lvieNodeDetail.Size = new System.Drawing.Size(500, 389);
            this.lvieNodeDetail.SmallImageList = this.imgl24x24n;
            this.lvieNodeDetail.TabIndex = 33;
            this.lvieNodeDetail.UseCompatibleStateImageBehavior = false;
            this.lvieNodeDetail.View = System.Windows.Forms.View.Details;
            // 
            // ManagerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.splcMain);
            this.Controls.Add(this.stasMain);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ManagerUI";
            this.Text = "ManagerUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splcMain.Panel1.ResumeLayout(false);
            this.splcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splcMain)).EndInit();
            this.splcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolsMain;
		private System.Windows.Forms.StatusStrip stasMain;
		private System.Windows.Forms.SplitContainer splcMain;
		private System.Windows.Forms.ImageList imgl16x16n;
		private System.Windows.Forms.ImageList imgl32x32n;
		private System.Windows.Forms.ContextMenuStrip ctxmMenu;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ImageList imgl24x24n;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private ListView.ManagerListView lvieNodeDetail;
		private Basgosoft.ManagementConsoleLib.BaseTreeView tvieTree;
		private System.Windows.Forms.ImageList imgl48x48n;
	}
}

