namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	partial class DBUserBOTestUI {
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
			this.butnGetAll = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.rowVersionDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.storageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dBUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.butnRegister = new System.Windows.Forms.Button();
			this.dgvwNewUser = new System.Windows.Forms.DataGridView();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.btunUpdate = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dBUserBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvwNewUser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// butnGetAll
			// 
			this.butnGetAll.Location = new System.Drawing.Point(12, 11);
			this.butnGetAll.Name = "butnGetAll";
			this.butnGetAll.Size = new System.Drawing.Size(122, 23);
			this.butnGetAll.TabIndex = 0;
			this.butnGetAll.Text = "Listar Usuarios";
			this.butnGetAll.UseVisualStyleBackColor = true;
			this.butnGetAll.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userIDDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.Password,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.mailDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.rowVersionDataGridViewImageColumn,
            this.Column1,
            this.departmentDataGridViewTextBoxColumn,
            this.storageDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.dBUserBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(12, 38);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(549, 210);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
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
			// Password
			// 
			this.Password.DataPropertyName = "Password";
			this.Password.HeaderText = "Password";
			this.Password.Name = "Password";
			this.Password.ReadOnly = true;
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
			// Column1
			// 
			this.Column1.HeaderText = "Version";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// departmentDataGridViewTextBoxColumn
			// 
			this.departmentDataGridViewTextBoxColumn.DataPropertyName = "Department";
			this.departmentDataGridViewTextBoxColumn.HeaderText = "Department";
			this.departmentDataGridViewTextBoxColumn.Name = "departmentDataGridViewTextBoxColumn";
			this.departmentDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// storageDataGridViewTextBoxColumn
			// 
			this.storageDataGridViewTextBoxColumn.DataPropertyName = "Storage";
			this.storageDataGridViewTextBoxColumn.HeaderText = "Storage";
			this.storageDataGridViewTextBoxColumn.Name = "storageDataGridViewTextBoxColumn";
			this.storageDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// dBUserBindingSource
			// 
			this.dBUserBindingSource.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.DBUser);
			// 
			// butnRegister
			// 
			this.butnRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butnRegister.Location = new System.Drawing.Point(12, 271);
			this.butnRegister.Name = "butnRegister";
			this.butnRegister.Size = new System.Drawing.Size(122, 23);
			this.butnRegister.TabIndex = 2;
			this.butnRegister.Text = "Registrar Usuario";
			this.butnRegister.UseVisualStyleBackColor = true;
			this.butnRegister.Click += new System.EventHandler(this.butnRegister_Click);
			// 
			// dgvwNewUser
			// 
			this.dgvwNewUser.AllowUserToAddRows = false;
			this.dgvwNewUser.AllowUserToDeleteRows = false;
			this.dgvwNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvwNewUser.AutoGenerateColumns = false;
			this.dgvwNewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwNewUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewCheckBoxColumn1});
			this.dgvwNewUser.DataSource = this.bindingSource1;
			this.dgvwNewUser.Location = new System.Drawing.Point(12, 298);
			this.dgvwNewUser.Name = "dgvwNewUser";
			this.dgvwNewUser.ReadOnly = true;
			this.dgvwNewUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwNewUser.Size = new System.Drawing.Size(549, 110);
			this.dgvwNewUser.TabIndex = 3;
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.DBUser);
			// 
			// btunUpdate
			// 
			this.btunUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btunUpdate.Location = new System.Drawing.Point(140, 271);
			this.btunUpdate.Name = "btunUpdate";
			this.btunUpdate.Size = new System.Drawing.Size(122, 23);
			this.btunUpdate.TabIndex = 4;
			this.btunUpdate.Text = "Actualizar Usuario";
			this.btunUpdate.UseVisualStyleBackColor = true;
			this.btunUpdate.Click += new System.EventHandler(this.btunUpdate_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "UserID";
			this.dataGridViewTextBoxColumn1.HeaderText = "UserID";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "UserName";
			this.dataGridViewTextBoxColumn2.HeaderText = "UserName";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Password";
			this.dataGridViewTextBoxColumn4.HeaderText = "Password";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "FirstName";
			this.dataGridViewTextBoxColumn5.HeaderText = "FirstName";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "LastName";
			this.dataGridViewTextBoxColumn6.HeaderText = "LastName";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "FullName";
			this.dataGridViewTextBoxColumn7.HeaderText = "FullName";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "Mail";
			this.dataGridViewTextBoxColumn8.HeaderText = "Mail";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "Department";
			this.dataGridViewTextBoxColumn11.HeaderText = "Department";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
			this.dataGridViewCheckBoxColumn1.HeaderText = "Enabled";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			// 
			// DBUserBOTestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(573, 420);
			this.Controls.Add(this.btunUpdate);
			this.Controls.Add(this.dgvwNewUser);
			this.Controls.Add(this.butnRegister);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.butnGetAll);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "DBUserBOTestUI";
			this.Text = "DBUserBO";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dBUserBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvwNewUser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butnGetAll;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewImageColumn recordVersionDataGridViewImageColumn;
		private System.Windows.Forms.Button butnRegister;
		private System.Windows.Forms.DataGridView dgvwNewUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.DataGridViewTextBoxColumn userBranchOfficesDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordHashDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.BindingSource dBUserBindingSource;
		private System.Windows.Forms.Button btunUpdate;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Password;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mailDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn rowVersionDataGridViewImageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn storageDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
	}
}