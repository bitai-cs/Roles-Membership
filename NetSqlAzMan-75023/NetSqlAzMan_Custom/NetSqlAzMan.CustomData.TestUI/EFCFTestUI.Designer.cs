namespace NetSqlAzMan.CustomDataLayer.TestUI
{
	partial class EFCFTestUI
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
			this.butnListUsers = new System.Windows.Forms.Button();
			this.gridUsers = new System.Windows.Forms.DataGridView();
			this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.passwordDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.rowVersionDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.identityDepartmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.identityUserValidationRequestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.identityUserBranchOfficeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.identityUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.butnInsert = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dgvwLdapProfiles = new System.Windows.Forms.DataGridView();
			this.domainProfileIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.domainProfileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ldapServerProfileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ldapwacServerBaseDNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ldapwacDomainProfileBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.button3 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.identityUserBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvwLdapProfiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ldapwacDomainProfileBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// butnListUsers
			// 
			this.butnListUsers.Location = new System.Drawing.Point(12, 12);
			this.butnListUsers.Name = "butnListUsers";
			this.butnListUsers.Size = new System.Drawing.Size(127, 23);
			this.butnListUsers.TabIndex = 0;
			this.butnListUsers.Text = "Listar Usuarios";
			this.butnListUsers.UseVisualStyleBackColor = true;
			this.butnListUsers.Click += new System.EventHandler(this.butnListUsers_Click);
			// 
			// gridUsers
			// 
			this.gridUsers.AllowUserToAddRows = false;
			this.gridUsers.AllowUserToDeleteRows = false;
			this.gridUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridUsers.AutoGenerateColumns = false;
			this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userIDDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewImageColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.mailDataGridViewTextBoxColumn,
            this.departmentIdDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.rowVersionDataGridViewImageColumn,
            this.identityDepartmentDataGridViewTextBoxColumn,
            this.identityUserValidationRequestDataGridViewTextBoxColumn,
            this.identityUserBranchOfficeDataGridViewTextBoxColumn});
			this.gridUsers.DataSource = this.identityUserBindingSource;
			this.gridUsers.Location = new System.Drawing.Point(12, 41);
			this.gridUsers.Name = "gridUsers";
			this.gridUsers.ReadOnly = true;
			this.gridUsers.Size = new System.Drawing.Size(521, 169);
			this.gridUsers.TabIndex = 1;
			// 
			// userIDDataGridViewTextBoxColumn
			// 
			this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
			this.userIDDataGridViewTextBoxColumn.HeaderText = "UserID";
			this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
			this.userIDDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// userNameDataGridViewTextBoxColumn
			// 
			this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
			this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
			this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
			this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// passwordDataGridViewImageColumn
			// 
			this.passwordDataGridViewImageColumn.DataPropertyName = "Password";
			this.passwordDataGridViewImageColumn.HeaderText = "Password";
			this.passwordDataGridViewImageColumn.Name = "passwordDataGridViewImageColumn";
			this.passwordDataGridViewImageColumn.ReadOnly = true;
			this.passwordDataGridViewImageColumn.Visible = false;
			// 
			// firstNameDataGridViewTextBoxColumn
			// 
			this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
			this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// lastNameDataGridViewTextBoxColumn
			// 
			this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
			this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
			this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
			this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// fullNameDataGridViewTextBoxColumn
			// 
			this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
			this.fullNameDataGridViewTextBoxColumn.HeaderText = "FullName";
			this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
			this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// mailDataGridViewTextBoxColumn
			// 
			this.mailDataGridViewTextBoxColumn.DataPropertyName = "Mail";
			this.mailDataGridViewTextBoxColumn.HeaderText = "Mail";
			this.mailDataGridViewTextBoxColumn.Name = "mailDataGridViewTextBoxColumn";
			this.mailDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// departmentIdDataGridViewTextBoxColumn
			// 
			this.departmentIdDataGridViewTextBoxColumn.DataPropertyName = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.HeaderText = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.Name = "departmentIdDataGridViewTextBoxColumn";
			this.departmentIdDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// enabledDataGridViewCheckBoxColumn
			// 
			this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
			this.enabledDataGridViewCheckBoxColumn.ReadOnly = true;
			// 
			// rowVersionDataGridViewImageColumn
			// 
			this.rowVersionDataGridViewImageColumn.DataPropertyName = "RowVersion";
			this.rowVersionDataGridViewImageColumn.HeaderText = "RowVersion";
			this.rowVersionDataGridViewImageColumn.Name = "rowVersionDataGridViewImageColumn";
			this.rowVersionDataGridViewImageColumn.ReadOnly = true;
			this.rowVersionDataGridViewImageColumn.Visible = false;
			// 
			// identityDepartmentDataGridViewTextBoxColumn
			// 
			this.identityDepartmentDataGridViewTextBoxColumn.DataPropertyName = "identity_Department";
			this.identityDepartmentDataGridViewTextBoxColumn.HeaderText = "identity_Department";
			this.identityDepartmentDataGridViewTextBoxColumn.Name = "identityDepartmentDataGridViewTextBoxColumn";
			this.identityDepartmentDataGridViewTextBoxColumn.ReadOnly = true;
			this.identityDepartmentDataGridViewTextBoxColumn.Visible = false;
			// 
			// identityUserValidationRequestDataGridViewTextBoxColumn
			// 
			this.identityUserValidationRequestDataGridViewTextBoxColumn.DataPropertyName = "identity_UserValidationRequest";
			this.identityUserValidationRequestDataGridViewTextBoxColumn.HeaderText = "identity_UserValidationRequest";
			this.identityUserValidationRequestDataGridViewTextBoxColumn.Name = "identityUserValidationRequestDataGridViewTextBoxColumn";
			this.identityUserValidationRequestDataGridViewTextBoxColumn.ReadOnly = true;
			this.identityUserValidationRequestDataGridViewTextBoxColumn.Visible = false;
			// 
			// identityUserBranchOfficeDataGridViewTextBoxColumn
			// 
			this.identityUserBranchOfficeDataGridViewTextBoxColumn.DataPropertyName = "identity_UserBranchOffice";
			this.identityUserBranchOfficeDataGridViewTextBoxColumn.HeaderText = "identity_UserBranchOffice";
			this.identityUserBranchOfficeDataGridViewTextBoxColumn.Name = "identityUserBranchOfficeDataGridViewTextBoxColumn";
			this.identityUserBranchOfficeDataGridViewTextBoxColumn.ReadOnly = true;
			this.identityUserBranchOfficeDataGridViewTextBoxColumn.Visible = false;
			// 
			// identityUserBindingSource
			// 
			this.identityUserBindingSource.DataSource = typeof(NetSqlAzMan.CustomDataLayer.EFCF.identity_User);
			// 
			// butnInsert
			// 
			this.butnInsert.Location = new System.Drawing.Point(145, 12);
			this.butnInsert.Name = "butnInsert";
			this.butnInsert.Size = new System.Drawing.Size(127, 23);
			this.butnInsert.TabIndex = 2;
			this.butnInsert.Text = "Insertar Usuario";
			this.butnInsert.UseVisualStyleBackColor = true;
			this.butnInsert.Click += new System.EventHandler(this.butnInsert_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 216);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(189, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Insertar UserBranchOffice";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(6, 245);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(189, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "LdapProfile_SelectAllAsync";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dgvwLdapProfiles
			// 
			this.dgvwLdapProfiles.AllowUserToAddRows = false;
			this.dgvwLdapProfiles.AllowUserToDeleteRows = false;
			this.dgvwLdapProfiles.AutoGenerateColumns = false;
			this.dgvwLdapProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwLdapProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.domainProfileIdDataGridViewTextBoxColumn,
            this.domainProfileDataGridViewTextBoxColumn,
            this.ldapProxyWebApiUriDataGridViewTextBoxColumn,
            this.ldapServerProfileDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn1,
            this.ldapwacServerBaseDNDataGridViewTextBoxColumn});
			this.dgvwLdapProfiles.DataSource = this.ldapwacDomainProfileBindingSource;
			this.dgvwLdapProfiles.Location = new System.Drawing.Point(6, 274);
			this.dgvwLdapProfiles.Name = "dgvwLdapProfiles";
			this.dgvwLdapProfiles.ReadOnly = true;
			this.dgvwLdapProfiles.Size = new System.Drawing.Size(533, 131);
			this.dgvwLdapProfiles.TabIndex = 5;
			// 
			// domainProfileIdDataGridViewTextBoxColumn
			// 
			this.domainProfileIdDataGridViewTextBoxColumn.DataPropertyName = "DomainProfileId";
			this.domainProfileIdDataGridViewTextBoxColumn.HeaderText = "DomainProfileId";
			this.domainProfileIdDataGridViewTextBoxColumn.Name = "domainProfileIdDataGridViewTextBoxColumn";
			this.domainProfileIdDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// domainProfileDataGridViewTextBoxColumn
			// 
			this.domainProfileDataGridViewTextBoxColumn.DataPropertyName = "DomainProfile";
			this.domainProfileDataGridViewTextBoxColumn.HeaderText = "DomainProfile";
			this.domainProfileDataGridViewTextBoxColumn.Name = "domainProfileDataGridViewTextBoxColumn";
			this.domainProfileDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// ldapProxyWebApiUriDataGridViewTextBoxColumn
			// 
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.DataPropertyName = "LdapProxyWebApiUri";
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.HeaderText = "LdapProxyWebApiUri";
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.Name = "ldapProxyWebApiUriDataGridViewTextBoxColumn";
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// ldapServerProfileDataGridViewTextBoxColumn
			// 
			this.ldapServerProfileDataGridViewTextBoxColumn.DataPropertyName = "LdapServerProfile";
			this.ldapServerProfileDataGridViewTextBoxColumn.HeaderText = "LdapServerProfile";
			this.ldapServerProfileDataGridViewTextBoxColumn.Name = "ldapServerProfileDataGridViewTextBoxColumn";
			this.ldapServerProfileDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// enabledDataGridViewCheckBoxColumn1
			// 
			this.enabledDataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
			this.enabledDataGridViewCheckBoxColumn1.HeaderText = "Enabled";
			this.enabledDataGridViewCheckBoxColumn1.Name = "enabledDataGridViewCheckBoxColumn1";
			this.enabledDataGridViewCheckBoxColumn1.ReadOnly = true;
			// 
			// ldapwacServerBaseDNDataGridViewTextBoxColumn
			// 
			this.ldapwacServerBaseDNDataGridViewTextBoxColumn.DataPropertyName = "ldapwac_ServerBaseDN";
			this.ldapwacServerBaseDNDataGridViewTextBoxColumn.HeaderText = "ldapwac_ServerBaseDN";
			this.ldapwacServerBaseDNDataGridViewTextBoxColumn.Name = "ldapwacServerBaseDNDataGridViewTextBoxColumn";
			this.ldapwacServerBaseDNDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// ldapwacDomainProfileBindingSource
			// 
			this.ldapwacDomainProfileBindingSource.DataSource = typeof(NetSqlAzMan.CustomDataLayer.EFCF.ldapwac_DomainProfile);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(6, 411);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(189, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "ldapwac_fn_DomainProfileIsInUse";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// EFCFTestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(545, 440);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.dgvwLdapProfiles);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butnInsert);
			this.Controls.Add(this.gridUsers);
			this.Controls.Add(this.butnListUsers);
			this.Name = "EFCFTestUI";
			this.Text = "EFCF.identity_User TestUI";
			this.Load += new System.EventHandler(this.EFTestUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.identityUserBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvwLdapProfiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ldapwacDomainProfileBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butnListUsers;
		private System.Windows.Forms.DataGridView gridUsers;
		private System.Windows.Forms.Button butnInsert;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.BindingSource identityUserBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn passwordDataGridViewImageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordHashDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mailDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn rowVersionDataGridViewImageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn identityDepartmentDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn identityUserValidationRequestDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn identityUserBranchOfficeDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridView dgvwLdapProfiles;
		private System.Windows.Forms.DataGridViewTextBoxColumn profileIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn domainProfileIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn domainProfileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ldapProxyWebApiUriDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ldapServerProfileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn ldapwacServerBaseDNDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource ldapwacDomainProfileBindingSource;
		private System.Windows.Forms.Button button3;
	}
}