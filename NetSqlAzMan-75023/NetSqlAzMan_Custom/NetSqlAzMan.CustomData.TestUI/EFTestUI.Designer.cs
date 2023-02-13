namespace NetSqlAzMan.CustomDataLayer.TestUI
{
	partial class EFTestUI
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
			this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.passwordHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeIdsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.relativeBranchOfficeIdsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeNamesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.rowVersionDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.identityvwUsersBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.butnInsert = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.identityvwUsersBindingSource)).BeginInit();
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
			this.gridUsers.AutoGenerateColumns = false;
			this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userIDDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.passwordHashDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.mailDataGridViewTextBoxColumn,
            this.departmentIdDataGridViewTextBoxColumn,
            this.departmentNameDataGridViewTextBoxColumn,
            this.branchOfficeIdsDataGridViewTextBoxColumn,
            this.relativeBranchOfficeIdsDataGridViewTextBoxColumn,
            this.branchOfficeNamesDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.rowVersionDataGridViewImageColumn});
			this.gridUsers.DataSource = this.identityvwUsersBindingSource;
			this.gridUsers.Location = new System.Drawing.Point(12, 41);
			this.gridUsers.Name = "gridUsers";
			this.gridUsers.Size = new System.Drawing.Size(521, 185);
			this.gridUsers.TabIndex = 1;
			// 
			// userIDDataGridViewTextBoxColumn
			// 
			this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
			this.userIDDataGridViewTextBoxColumn.HeaderText = "UserID";
			this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
			// 
			// userNameDataGridViewTextBoxColumn
			// 
			this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
			this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
			this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
			// 
			// passwordDataGridViewTextBoxColumn
			// 
			this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
			this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
			this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
			// 
			// passwordHashDataGridViewTextBoxColumn
			// 
			this.passwordHashDataGridViewTextBoxColumn.DataPropertyName = "PasswordHash";
			this.passwordHashDataGridViewTextBoxColumn.HeaderText = "PasswordHash";
			this.passwordHashDataGridViewTextBoxColumn.Name = "passwordHashDataGridViewTextBoxColumn";
			// 
			// firstNameDataGridViewTextBoxColumn
			// 
			this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
			// 
			// lastNameDataGridViewTextBoxColumn
			// 
			this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
			this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
			this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
			// 
			// fullNameDataGridViewTextBoxColumn
			// 
			this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
			this.fullNameDataGridViewTextBoxColumn.HeaderText = "FullName";
			this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
			// 
			// mailDataGridViewTextBoxColumn
			// 
			this.mailDataGridViewTextBoxColumn.DataPropertyName = "Mail";
			this.mailDataGridViewTextBoxColumn.HeaderText = "Mail";
			this.mailDataGridViewTextBoxColumn.Name = "mailDataGridViewTextBoxColumn";
			// 
			// departmentIdDataGridViewTextBoxColumn
			// 
			this.departmentIdDataGridViewTextBoxColumn.DataPropertyName = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.HeaderText = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.Name = "departmentIdDataGridViewTextBoxColumn";
			// 
			// departmentNameDataGridViewTextBoxColumn
			// 
			this.departmentNameDataGridViewTextBoxColumn.DataPropertyName = "DepartmentName";
			this.departmentNameDataGridViewTextBoxColumn.HeaderText = "DepartmentName";
			this.departmentNameDataGridViewTextBoxColumn.Name = "departmentNameDataGridViewTextBoxColumn";
			// 
			// branchOfficeIdsDataGridViewTextBoxColumn
			// 
			this.branchOfficeIdsDataGridViewTextBoxColumn.DataPropertyName = "BranchOfficeIds";
			this.branchOfficeIdsDataGridViewTextBoxColumn.HeaderText = "BranchOfficeIds";
			this.branchOfficeIdsDataGridViewTextBoxColumn.Name = "branchOfficeIdsDataGridViewTextBoxColumn";
			// 
			// relativeBranchOfficeIdsDataGridViewTextBoxColumn
			// 
			this.relativeBranchOfficeIdsDataGridViewTextBoxColumn.DataPropertyName = "RelativeBranchOfficeIds";
			this.relativeBranchOfficeIdsDataGridViewTextBoxColumn.HeaderText = "RelativeBranchOfficeIds";
			this.relativeBranchOfficeIdsDataGridViewTextBoxColumn.Name = "relativeBranchOfficeIdsDataGridViewTextBoxColumn";
			// 
			// branchOfficeNamesDataGridViewTextBoxColumn
			// 
			this.branchOfficeNamesDataGridViewTextBoxColumn.DataPropertyName = "BranchOfficeNames";
			this.branchOfficeNamesDataGridViewTextBoxColumn.HeaderText = "BranchOfficeNames";
			this.branchOfficeNamesDataGridViewTextBoxColumn.Name = "branchOfficeNamesDataGridViewTextBoxColumn";
			// 
			// enabledDataGridViewCheckBoxColumn
			// 
			this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
			this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
			// 
			// rowVersionDataGridViewImageColumn
			// 
			this.rowVersionDataGridViewImageColumn.DataPropertyName = "RowVersion";
			this.rowVersionDataGridViewImageColumn.HeaderText = "RowVersion";
			this.rowVersionDataGridViewImageColumn.Name = "rowVersionDataGridViewImageColumn";
			this.rowVersionDataGridViewImageColumn.Visible = false;
			// 
			// identityvwUsersBindingSource
			// 
			this.identityvwUsersBindingSource.DataSource = typeof(NetSqlAzMan.CustomDataLayer.EF.identity_vw_Users);
			// 
			// butnInsert
			// 
			this.butnInsert.Location = new System.Drawing.Point(12, 266);
			this.butnInsert.Name = "butnInsert";
			this.butnInsert.Size = new System.Drawing.Size(127, 23);
			this.butnInsert.TabIndex = 2;
			this.butnInsert.Text = "Insertar Usuario";
			this.butnInsert.UseVisualStyleBackColor = true;
			this.butnInsert.Click += new System.EventHandler(this.butnInsert_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 318);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(189, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Insertar UserBranchOffice";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// EFTestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(545, 385);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butnInsert);
			this.Controls.Add(this.gridUsers);
			this.Controls.Add(this.butnListUsers);
			this.Name = "EFTestUI";
			this.Text = "EFTestUI";
			this.Load += new System.EventHandler(this.EFTestUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.identityvwUsersBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butnListUsers;
		private System.Windows.Forms.DataGridView gridUsers;
		private System.Windows.Forms.BindingSource identityvwUsersBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordHashDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mailDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeIdsDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn relativeBranchOfficeIdsDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeNamesDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn rowVersionDataGridViewImageColumn;
		private System.Windows.Forms.Button butnInsert;
		private System.Windows.Forms.Button button1;
	}
}