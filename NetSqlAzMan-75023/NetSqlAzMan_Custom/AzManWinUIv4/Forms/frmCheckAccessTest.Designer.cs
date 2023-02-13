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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckAccessTest));
			this.imageListItemHierarchyView = new System.Windows.Forms.ImageList(this.components);
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butnBrowseLDAPEntry = new System.Windows.Forms.Button();
			this.radbLdapEntry = new System.Windows.Forms.RadioButton();
			this.txtbLdapEntry = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblUPNUser = new System.Windows.Forms.Label();
			this.lblProtocolTransitionWarning = new System.Windows.Forms.Label();
			this.butnBrowseDBUser = new System.Windows.Forms.Button();
			this.radbDBUser = new System.Windows.Forms.RadioButton();
			this.txtbDBUser = new System.Windows.Forms.TextBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
			this.btnClose.Location = new System.Drawing.Point(796, 690);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
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
			this.panel1.Location = new System.Drawing.Point(12, 683);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(859, 1);
			this.panel1.TabIndex = 49;
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(9, 664);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(862, 16);
			this.lblMessage.TabIndex = 54;
			this.lblMessage.Text = " ... ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butnBrowseLDAPEntry);
			this.groupBox1.Controls.Add(this.radbLdapEntry);
			this.groupBox1.Controls.Add(this.txtbLdapEntry);
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Controls.Add(this.lblUPNUser);
			this.groupBox1.Controls.Add(this.lblProtocolTransitionWarning);
			this.groupBox1.Controls.Add(this.butnBrowseDBUser);
			this.groupBox1.Controls.Add(this.radbDBUser);
			this.groupBox1.Controls.Add(this.txtbDBUser);
			this.groupBox1.Controls.Add(this.pictureBox2);
			this.groupBox1.Location = new System.Drawing.Point(4, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(322, 112);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " Check Access &Identity";
			// 
			// butnBrowseLDAPEntry
			// 
			this.butnBrowseLDAPEntry.Location = new System.Drawing.Point(286, 79);
			this.butnBrowseLDAPEntry.Name = "butnBrowseLDAPEntry";
			this.butnBrowseLDAPEntry.Size = new System.Drawing.Size(25, 20);
			this.butnBrowseLDAPEntry.TabIndex = 66;
			this.butnBrowseLDAPEntry.Text = "...";
			this.butnBrowseLDAPEntry.UseVisualStyleBackColor = true;
			this.butnBrowseLDAPEntry.Click += new System.EventHandler(this.butnBrowseLDAPEntry_Click);
			// 
			// radbLdapEntry
			// 
			this.radbLdapEntry.AutoSize = true;
			this.radbLdapEntry.Checked = true;
			this.radbLdapEntry.Location = new System.Drawing.Point(46, 61);
			this.radbLdapEntry.Name = "radbLdapEntry";
			this.radbLdapEntry.Size = new System.Drawing.Size(81, 17);
			this.radbLdapEntry.TabIndex = 65;
			this.radbLdapEntry.TabStop = true;
			this.radbLdapEntry.Text = "&LDAP User:";
			this.radbLdapEntry.UseVisualStyleBackColor = true;
			this.radbLdapEntry.CheckedChanged += new System.EventHandler(this.radbLdapEntry_CheckedChanged);
			// 
			// txtbLdapEntry
			// 
			this.txtbLdapEntry.Location = new System.Drawing.Point(47, 79);
			this.txtbLdapEntry.Name = "txtbLdapEntry";
			this.txtbLdapEntry.ReadOnly = true;
			this.txtbLdapEntry.Size = new System.Drawing.Size(236, 20);
			this.txtbLdapEntry.TabIndex = 64;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::NetSqlAzMan.SnapIn.Properties.Resources.user_data32x32n;
			this.pictureBox1.Location = new System.Drawing.Point(9, 67);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 63;
			this.pictureBox1.TabStop = false;
			// 
			// lblUPNUser
			// 
			this.lblUPNUser.AutoSize = true;
			this.lblUPNUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.lblUPNUser.Location = new System.Drawing.Point(346, 31);
			this.lblUPNUser.Name = "lblUPNUser";
			this.lblUPNUser.Size = new System.Drawing.Size(119, 13);
			this.lblUPNUser.TabIndex = 62;
			this.lblUPNUser.Text = "(otheruser@domain.ext)";
			this.lblUPNUser.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblProtocolTransitionWarning
			// 
			this.lblProtocolTransitionWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProtocolTransitionWarning.ForeColor = System.Drawing.Color.Brown;
			this.lblProtocolTransitionWarning.Location = new System.Drawing.Point(346, 59);
			this.lblProtocolTransitionWarning.Name = "lblProtocolTransitionWarning";
			this.lblProtocolTransitionWarning.Size = new System.Drawing.Size(125, 44);
			this.lblProtocolTransitionWarning.TabIndex = 61;
			this.lblProtocolTransitionWarning.Text = "Users rather then current can be selected only from a Windows Server 2003 machine" +
    " in a Windows 2003 Native Domain (Kerberos Protocol Transition).";
			this.lblProtocolTransitionWarning.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblProtocolTransitionWarning.Visible = false;
			// 
			// butnBrowseDBUser
			// 
			this.butnBrowseDBUser.Location = new System.Drawing.Point(286, 37);
			this.butnBrowseDBUser.Name = "butnBrowseDBUser";
			this.butnBrowseDBUser.Size = new System.Drawing.Size(25, 20);
			this.butnBrowseDBUser.TabIndex = 60;
			this.butnBrowseDBUser.Text = "...";
			this.butnBrowseDBUser.UseVisualStyleBackColor = true;
			this.butnBrowseDBUser.Click += new System.EventHandler(this.btnBrowseDBUser_Click);
			// 
			// radbDBUser
			// 
			this.radbDBUser.AutoSize = true;
			this.radbDBUser.Location = new System.Drawing.Point(46, 19);
			this.radbDBUser.Name = "radbDBUser";
			this.radbDBUser.Size = new System.Drawing.Size(99, 17);
			this.radbDBUser.TabIndex = 59;
			this.radbDBUser.Text = "&Database User:";
			this.radbDBUser.UseVisualStyleBackColor = true;
			this.radbDBUser.CheckedChanged += new System.EventHandler(this.radbDBUser_CheckedChanged);
			// 
			// txtbDBUser
			// 
			this.txtbDBUser.Enabled = false;
			this.txtbDBUser.Location = new System.Drawing.Point(47, 37);
			this.txtbDBUser.Name = "txtbDBUser";
			this.txtbDBUser.ReadOnly = true;
			this.txtbDBUser.Size = new System.Drawing.Size(236, 20);
			this.txtbDBUser.TabIndex = 58;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::NetSqlAzMan.SnapIn.Properties.Resources.user_data32x32n;
			this.pictureBox2.Location = new System.Drawing.Point(9, 25);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(32, 32);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 57;
			this.pictureBox2.TabStop = false;
			// 
			// btnCheckAccessTest
			// 
			this.btnCheckAccessTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCheckAccessTest.Location = new System.Drawing.Point(332, 14);
			this.btnCheckAccessTest.Name = "btnCheckAccessTest";
			this.btnCheckAccessTest.Size = new System.Drawing.Size(150, 50);
			this.btnCheckAccessTest.TabIndex = 57;
			this.btnCheckAccessTest.Text = "Start Check Access &Test";
			this.btnCheckAccessTest.UseVisualStyleBackColor = true;
			this.btnCheckAccessTest.Click += new System.EventHandler(this.btnCheckAccessTest_Click);
			// 
			// dtValidFor
			// 
			this.dtValidFor.Checked = false;
			this.dtValidFor.CustomFormat = "dd/MMM/yyyy hh:mm";
			this.dtValidFor.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
			this.dtValidFor.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtValidFor.Location = new System.Drawing.Point(332, 71);
			this.dtValidFor.Name = "dtValidFor";
			this.dtValidFor.ShowCheckBox = true;
			this.dtValidFor.Size = new System.Drawing.Size(150, 20);
			this.dtValidFor.TabIndex = 60;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Location = new System.Drawing.Point(4, 125);
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
			this.splitContainer1.Size = new System.Drawing.Size(876, 433);
			this.splitContainer1.SplitterDistance = 417;
			this.splitContainer1.TabIndex = 61;
			// 
			// txtDetails
			// 
			this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDetails.BackColor = System.Drawing.Color.Black;
			this.txtDetails.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDetails.ForeColor = System.Drawing.Color.Lime;
			this.txtDetails.Location = new System.Drawing.Point(0, 14);
			this.txtDetails.Multiline = true;
			this.txtDetails.Name = "txtDetails";
			this.txtDetails.ReadOnly = true;
			this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDetails.Size = new System.Drawing.Size(413, 415);
			this.txtDetails.TabIndex = 67;
			this.txtDetails.WordWrap = false;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(409, 11);
			this.label2.TabIndex = 66;
			this.label2.Text = "Details:";
			// 
			// checkAccessTestTreeView
			// 
			this.checkAccessTestTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkAccessTestTreeView.BackColor = System.Drawing.Color.LemonChiffon;
			this.checkAccessTestTreeView.FullRowSelect = true;
			this.checkAccessTestTreeView.ImageIndex = 5;
			this.checkAccessTestTreeView.ImageList = this.imageListItemHierarchyView;
			this.checkAccessTestTreeView.Location = new System.Drawing.Point(0, 15);
			this.checkAccessTestTreeView.Name = "checkAccessTestTreeView";
			this.checkAccessTestTreeView.SelectedImageIndex = 0;
			this.checkAccessTestTreeView.ShowNodeToolTips = true;
			this.checkAccessTestTreeView.Size = new System.Drawing.Size(452, 413);
			this.checkAccessTestTreeView.TabIndex = 69;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(449, 12);
			this.label1.TabIndex = 68;
			this.label1.Text = "Check Access Results:";
			// 
			// chkCache
			// 
			this.chkCache.AutoSize = true;
			this.chkCache.Location = new System.Drawing.Point(634, 12);
			this.chkCache.Name = "chkCache";
			this.chkCache.Size = new System.Drawing.Size(129, 17);
			this.chkCache.TabIndex = 62;
			this.chkCache.Text = "UserPermissionCache";
			this.chkCache.UseVisualStyleBackColor = true;
			this.chkCache.Visible = false;
			this.chkCache.CheckedChanged += new System.EventHandler(this.chkCache_CheckedChanged);
			// 
			// chkStorageCache
			// 
			this.chkStorageCache.AutoSize = true;
			this.chkStorageCache.Location = new System.Drawing.Point(769, 12);
			this.chkStorageCache.Name = "chkStorageCache";
			this.chkStorageCache.Size = new System.Drawing.Size(94, 17);
			this.chkStorageCache.TabIndex = 64;
			this.chkStorageCache.Text = "StorageCache";
			this.chkStorageCache.UseVisualStyleBackColor = true;
			this.chkStorageCache.Visible = false;
			this.chkStorageCache.CheckedChanged += new System.EventHandler(this.chkStorageCache_CheckedChanged);
			// 
			// frmCheckAccessTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(884, 562);
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
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(900, 600);
			this.Name = "frmCheckAccessTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Check Access Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheckAccessTest_FormClosing);
			this.Load += new System.EventHandler(this.frmCheckAccessTest_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button butnBrowseDBUser;
		private System.Windows.Forms.RadioButton radbDBUser;
		private System.Windows.Forms.TextBox txtbDBUser;
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
		private System.Windows.Forms.Button butnBrowseLDAPEntry;
		private System.Windows.Forms.RadioButton radbLdapEntry;
		private System.Windows.Forms.TextBox txtbLdapEntry;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}