namespace NetSqlAzMan.SnapIn.Forms
{
    partial class frmCheckAccessTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckAccessTest));
			this.imageListItemHierarchyView = new System.Windows.Forms.ImageList(this.components);
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblUPNUser = new System.Windows.Forms.Label();
			this.lblProtocolTransitionWarning = new System.Windows.Forms.Label();
			this.btnBrowseDBUser = new System.Windows.Forms.Button();
			this.btnBrowseWindowsUser = new System.Windows.Forms.Button();
			this.rbDBUser = new System.Windows.Forms.RadioButton();
			this.txtDBUser = new System.Windows.Forms.TextBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.rbWindowsUser = new System.Windows.Forms.RadioButton();
			this.txtWindowsUser = new System.Windows.Forms.TextBox();
			this.picWindowsUser = new System.Windows.Forms.PictureBox();
			this.btnCheckAccessTest = new System.Windows.Forms.Button();
			this.dtValidFor = new System.Windows.Forms.DateTimePicker();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtDetails = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkAccessTestTreeView = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.chkCache = new System.Windows.Forms.CheckBox();
			this.chkStorageCache = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picWindowsUser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageListItemHierarchyView
			// 
			this.imageListItemHierarchyView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItemHierarchyView.ImageStream")));
			this.imageListItemHierarchyView.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListItemHierarchyView.Images.SetKeyName(0, "NetSqlAzMan_16x16.ico");
			this.imageListItemHierarchyView.Images.SetKeyName(1, "Store_16x16.ico");
			this.imageListItemHierarchyView.Images.SetKeyName(2, "Application_16x16.ico");
			this.imageListItemHierarchyView.Images.SetKeyName(3, "Role_16x16.ico");
			this.imageListItemHierarchyView.Images.SetKeyName(4, "Task_16x16.ico");
			this.imageListItemHierarchyView.Images.SetKeyName(5, "Operation_16x16.ico");
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(844, 911);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 28);
			this.btnClose.TabIndex = 46;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(16, 902);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(927, 1);
			this.panel1.TabIndex = 49;
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(12, 879);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(932, 20);
			this.lblMessage.TabIndex = 54;
			this.lblMessage.Text = " ... ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblUPNUser);
			this.groupBox1.Controls.Add(this.lblProtocolTransitionWarning);
			this.groupBox1.Controls.Add(this.btnBrowseDBUser);
			this.groupBox1.Controls.Add(this.btnBrowseWindowsUser);
			this.groupBox1.Controls.Add(this.rbDBUser);
			this.groupBox1.Controls.Add(this.txtDBUser);
			this.groupBox1.Controls.Add(this.pictureBox2);
			this.groupBox1.Controls.Add(this.rbWindowsUser);
			this.groupBox1.Controls.Add(this.txtWindowsUser);
			this.groupBox1.Controls.Add(this.picWindowsUser);
			this.groupBox1.Location = new System.Drawing.Point(5, 5);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(672, 218);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Check Access &Identity";
			// 
			// lblUPNUser
			// 
			this.lblUPNUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.lblUPNUser.Location = new System.Drawing.Point(206, 68);
			this.lblUPNUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUPNUser.Name = "lblUPNUser";
			this.lblUPNUser.Size = new System.Drawing.Size(320, 17);
			this.lblUPNUser.TabIndex = 62;
			this.lblUPNUser.Text = "(otheruser@domain.ext)";
			this.lblUPNUser.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblProtocolTransitionWarning
			// 
			this.lblProtocolTransitionWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProtocolTransitionWarning.ForeColor = System.Drawing.Color.Brown;
			this.lblProtocolTransitionWarning.Location = new System.Drawing.Point(297, 85);
			this.lblProtocolTransitionWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblProtocolTransitionWarning.Name = "lblProtocolTransitionWarning";
			this.lblProtocolTransitionWarning.Size = new System.Drawing.Size(285, 75);
			this.lblProtocolTransitionWarning.TabIndex = 61;
			this.lblProtocolTransitionWarning.Text = "Users rather then current can be selected only from a Windows Server 2003 machine" +
    " in a Windows 2003 Native Domain (Kerberos Protocol Transition).";
			this.lblProtocolTransitionWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnBrowseDBUser
			// 
			this.btnBrowseDBUser.Location = new System.Drawing.Point(613, 164);
			this.btnBrowseDBUser.Margin = new System.Windows.Forms.Padding(4);
			this.btnBrowseDBUser.Name = "btnBrowseDBUser";
			this.btnBrowseDBUser.Size = new System.Drawing.Size(43, 25);
			this.btnBrowseDBUser.TabIndex = 60;
			this.btnBrowseDBUser.Text = "...";
			this.btnBrowseDBUser.UseVisualStyleBackColor = true;
			this.btnBrowseDBUser.Click += new System.EventHandler(this.btnBrowseDBUser_Click);
			// 
			// btnBrowseWindowsUser
			// 
			this.btnBrowseWindowsUser.Location = new System.Drawing.Point(575, 32);
			this.btnBrowseWindowsUser.Margin = new System.Windows.Forms.Padding(4);
			this.btnBrowseWindowsUser.Name = "btnBrowseWindowsUser";
			this.btnBrowseWindowsUser.Size = new System.Drawing.Size(43, 25);
			this.btnBrowseWindowsUser.TabIndex = 57;
			this.btnBrowseWindowsUser.Text = "...";
			this.btnBrowseWindowsUser.UseVisualStyleBackColor = true;
			this.btnBrowseWindowsUser.Click += new System.EventHandler(this.btnBrowseWindowsUser_Click);
			// 
			// rbDBUser
			// 
			this.rbDBUser.AutoSize = true;
			this.rbDBUser.Location = new System.Drawing.Point(87, 167);
			this.rbDBUser.Margin = new System.Windows.Forms.Padding(4);
			this.rbDBUser.Name = "rbDBUser";
			this.rbDBUser.Size = new System.Drawing.Size(128, 21);
			this.rbDBUser.TabIndex = 59;
			this.rbDBUser.Text = "&Database User:";
			this.rbDBUser.UseVisualStyleBackColor = true;
			this.rbDBUser.CheckedChanged += new System.EventHandler(this.rbDBUser_CheckedChanged);
			// 
			// txtDBUser
			// 
			this.txtDBUser.Location = new System.Drawing.Point(281, 164);
			this.txtDBUser.Margin = new System.Windows.Forms.Padding(4);
			this.txtDBUser.Name = "txtDBUser";
			this.txtDBUser.ReadOnly = true;
			this.txtDBUser.Size = new System.Drawing.Size(325, 22);
			this.txtDBUser.TabIndex = 58;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::NetSqlAzMan.SnapIn.Properties.Resources.DBUser_32x32;
			this.pictureBox2.Location = new System.Drawing.Point(36, 149);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(32, 32);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 57;
			this.pictureBox2.TabStop = false;
			// 
			// rbWindowsUser
			// 
			this.rbWindowsUser.AutoSize = true;
			this.rbWindowsUser.Checked = true;
			this.rbWindowsUser.Location = new System.Drawing.Point(76, 36);
			this.rbWindowsUser.Margin = new System.Windows.Forms.Padding(4);
			this.rbWindowsUser.Name = "rbWindowsUser";
			this.rbWindowsUser.Size = new System.Drawing.Size(123, 21);
			this.rbWindowsUser.TabIndex = 57;
			this.rbWindowsUser.TabStop = true;
			this.rbWindowsUser.Text = "&Windows User:";
			this.rbWindowsUser.UseVisualStyleBackColor = true;
			this.rbWindowsUser.CheckedChanged += new System.EventHandler(this.rbWindowsUser_CheckedChanged);
			// 
			// txtWindowsUser
			// 
			this.txtWindowsUser.Location = new System.Drawing.Point(248, 35);
			this.txtWindowsUser.Margin = new System.Windows.Forms.Padding(4);
			this.txtWindowsUser.Name = "txtWindowsUser";
			this.txtWindowsUser.Size = new System.Drawing.Size(319, 22);
			this.txtWindowsUser.TabIndex = 1;
			this.txtWindowsUser.TextChanged += new System.EventHandler(this.txtWindowsUser_TextChanged);
			// 
			// picWindowsUser
			// 
			this.picWindowsUser.Image = global::NetSqlAzMan.SnapIn.Properties.Resources.WindowsUser_32x32;
			this.picWindowsUser.Location = new System.Drawing.Point(36, 25);
			this.picWindowsUser.Margin = new System.Windows.Forms.Padding(4);
			this.picWindowsUser.Name = "picWindowsUser";
			this.picWindowsUser.Size = new System.Drawing.Size(32, 32);
			this.picWindowsUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picWindowsUser.TabIndex = 56;
			this.picWindowsUser.TabStop = false;
			// 
			// btnCheckAccessTest
			// 
			this.btnCheckAccessTest.Enabled = false;
			this.btnCheckAccessTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCheckAccessTest.Location = new System.Drawing.Point(744, 25);
			this.btnCheckAccessTest.Margin = new System.Windows.Forms.Padding(4);
			this.btnCheckAccessTest.Name = "btnCheckAccessTest";
			this.btnCheckAccessTest.Size = new System.Drawing.Size(163, 86);
			this.btnCheckAccessTest.TabIndex = 57;
			this.btnCheckAccessTest.Text = "Start Check Access &Test";
			this.btnCheckAccessTest.UseVisualStyleBackColor = true;
			this.btnCheckAccessTest.Click += new System.EventHandler(this.btnCheckAccessTest_Click);
			// 
			// dtValidFor
			// 
			this.dtValidFor.CustomFormat = "ddd dd MMM yyyy  - hh:mm:ss";
			this.dtValidFor.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
			this.dtValidFor.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtValidFor.Location = new System.Drawing.Point(696, 122);
			this.dtValidFor.Margin = new System.Windows.Forms.Padding(4);
			this.dtValidFor.Name = "dtValidFor";
			this.dtValidFor.ShowCheckBox = true;
			this.dtValidFor.Size = new System.Drawing.Size(248, 22);
			this.dtValidFor.TabIndex = 60;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Location = new System.Drawing.Point(16, 240);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtDetails);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.checkAccessTestTreeView);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Size = new System.Drawing.Size(928, 635);
			this.splitContainer1.SplitterDistance = 445;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 61;
			// 
			// txtDetails
			// 
			this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDetails.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDetails.Location = new System.Drawing.Point(0, 17);
			this.txtDetails.Margin = new System.Windows.Forms.Padding(4);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.ReadOnly = true;
			this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDetails.Size = new System.Drawing.Size(438, 612);
			this.txtDetails.TabIndex = 67;
			this.txtDetails.WordWrap = false;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(4, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(435, 14);
			this.label2.TabIndex = 66;
			this.label2.Text = "Details:";
			// 
			// checkAccessTestTreeView
			// 
			this.checkAccessTestTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkAccessTestTreeView.FullRowSelect = true;
			this.checkAccessTestTreeView.ImageIndex = 5;
			this.checkAccessTestTreeView.ImageList = this.imageListItemHierarchyView;
			this.checkAccessTestTreeView.Location = new System.Drawing.Point(0, 18);
			this.checkAccessTestTreeView.Margin = new System.Windows.Forms.Padding(4);
			this.checkAccessTestTreeView.Name = "checkAccessTestTreeView";
			this.checkAccessTestTreeView.SelectedImageIndex = 0;
			this.checkAccessTestTreeView.ShowNodeToolTips = true;
			this.checkAccessTestTreeView.Size = new System.Drawing.Size(475, 609);
			this.checkAccessTestTreeView.TabIndex = 69;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(472, 15);
			this.label1.TabIndex = 68;
			this.label1.Text = "Check Access Results:";
			// 
			// chkCache
			// 
			this.chkCache.AutoSize = true;
			this.chkCache.Location = new System.Drawing.Point(696, 164);
			this.chkCache.Margin = new System.Windows.Forms.Padding(4);
			this.chkCache.Name = "chkCache";
			this.chkCache.Size = new System.Drawing.Size(169, 21);
			this.chkCache.TabIndex = 62;
			this.chkCache.Text = "UserPermissionCache";
			this.chkCache.UseVisualStyleBackColor = true;
			this.chkCache.CheckedChanged += new System.EventHandler(this.chkCache_CheckedChanged);
			// 
			// chkStorageCache
			// 
			this.chkStorageCache.AutoSize = true;
			this.chkStorageCache.Location = new System.Drawing.Point(696, 192);
			this.chkStorageCache.Margin = new System.Windows.Forms.Padding(4);
			this.chkStorageCache.Name = "chkStorageCache";
			this.chkStorageCache.Size = new System.Drawing.Size(120, 21);
			this.chkStorageCache.TabIndex = 64;
			this.chkStorageCache.Text = "StorageCache";
			this.chkStorageCache.UseVisualStyleBackColor = true;
			this.chkStorageCache.CheckedChanged += new System.EventHandler(this.chkStorageCache_CheckedChanged);
			// 
			// frmCheckAccessTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(961, 865);
			this.Controls.Add(this.chkStorageCache);
			this.Controls.Add(this.chkCache);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.dtValidFor);
			this.Controls.Add(this.btnCheckAccessTest);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimizeBox = false;
			this.Name = "frmCheckAccessTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Check Access Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheckAccessTest_FormClosing);
			this.Load += new System.EventHandler(this.frmCheckAccessTest_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picWindowsUser)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.ImageList imageListItemHierarchyView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picWindowsUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton rbWindowsUser;
        private System.Windows.Forms.TextBox txtWindowsUser;
        private System.Windows.Forms.Button btnBrowseDBUser;
        private System.Windows.Forms.Button btnBrowseWindowsUser;
        private System.Windows.Forms.RadioButton rbDBUser;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Button btnCheckAccessTest;
        private System.Windows.Forms.Label lblProtocolTransitionWarning;
        private System.Windows.Forms.DateTimePicker dtValidFor;
        private System.Windows.Forms.Label lblUPNUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TreeView checkAccessTestTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCache;
        private System.Windows.Forms.CheckBox chkStorageCache;


    }
}