namespace NetSqlAzMan.CustomDataLayer.TestUI
{
	partial class LINQTestUI
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
			this.button1 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.identityUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.passwordHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.recordVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.identityDepartmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.identityUserBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(183, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Obtener Usuarios";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userIDDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.passwordHashDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.mailDataGridViewTextBoxColumn,
            this.departmentIdDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.recordVersionDataGridViewTextBoxColumn,
            this.identityDepartmentDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.identityUserBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(12, 41);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(553, 150);
			this.dataGridView1.TabIndex = 1;
			// 
			// identityUserBindingSource
			// 
			this.identityUserBindingSource.DataSource = typeof(NetSqlAzMan.CustomDataLayer.LINQ.identity_User);
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
			// passwordDataGridViewTextBoxColumn
			// 
			this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
			this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
			this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
			this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// passwordHashDataGridViewTextBoxColumn
			// 
			this.passwordHashDataGridViewTextBoxColumn.DataPropertyName = "PasswordHash";
			this.passwordHashDataGridViewTextBoxColumn.HeaderText = "PasswordHash";
			this.passwordHashDataGridViewTextBoxColumn.Name = "passwordHashDataGridViewTextBoxColumn";
			this.passwordHashDataGridViewTextBoxColumn.ReadOnly = true;
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
			// recordVersionDataGridViewTextBoxColumn
			// 
			this.recordVersionDataGridViewTextBoxColumn.DataPropertyName = "RecordVersion";
			this.recordVersionDataGridViewTextBoxColumn.HeaderText = "RecordVersion";
			this.recordVersionDataGridViewTextBoxColumn.Name = "recordVersionDataGridViewTextBoxColumn";
			this.recordVersionDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// identityDepartmentDataGridViewTextBoxColumn
			// 
			this.identityDepartmentDataGridViewTextBoxColumn.DataPropertyName = "identity_Department";
			this.identityDepartmentDataGridViewTextBoxColumn.HeaderText = "identity_Department";
			this.identityDepartmentDataGridViewTextBoxColumn.Name = "identityDepartmentDataGridViewTextBoxColumn";
			this.identityDepartmentDataGridViewTextBoxColumn.ReadOnly = true;
			this.identityDepartmentDataGridViewTextBoxColumn.Visible = false;
			// 
			// LINQTestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(577, 261);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button1);
			this.Name = "LINQTestUI";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.identityUserBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource identityUserBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordHashDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn mailDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn recordVersionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn identityDepartmentDataGridViewTextBoxColumn;
	}
}

