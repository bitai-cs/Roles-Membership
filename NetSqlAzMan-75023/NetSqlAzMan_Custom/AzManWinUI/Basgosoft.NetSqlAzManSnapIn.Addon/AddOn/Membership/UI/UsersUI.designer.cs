namespace NetSqlAzManSnapIn.AddOn.Membership.UI
{
	partial class UsersUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersUI));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tstpOptions = new System.Windows.Forms.ToolStrip();
			this.tsbtNew = new System.Windows.Forms.ToolStripButton();
			this.tsbtProperties = new System.Windows.Forms.ToolStripButton();
			this.tsbtCloseOnSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtDisable = new System.Windows.Forms.ToolStripButton();
			this.tsbtRefresh = new System.Windows.Forms.ToolStripButton();
			this.dgvwUsers = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
			this.userStructBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.userIdDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fullNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.mailDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.passwordDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeIdsDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RelativeBranchOfficeIds = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeNamesDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentIdDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.rowVersionDataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
			this.tstpOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvwUsers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userStructBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tstpOptions
			// 
			this.tstpOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.tstpOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtNew,
            this.tsbtProperties,
            this.tsbtCloseOnSave,
            this.toolStripSeparator1,
            this.tsbtDisable,
            this.tsbtRefresh});
			this.tstpOptions.Location = new System.Drawing.Point(0, 0);
			this.tstpOptions.Name = "tstpOptions";
			this.tstpOptions.Size = new System.Drawing.Size(675, 25);
			this.tstpOptions.TabIndex = 1;
			this.tstpOptions.Text = "toolStrip1";
			// 
			// tsbtNew
			// 
			this.tsbtNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtNew.Name = "tsbtNew";
			this.tsbtNew.Size = new System.Drawing.Size(44, 22);
			this.tsbtNew.Text = "Nuevo";
			this.tsbtNew.Click += new System.EventHandler(this.new_OnClick);
			// 
			// tsbtProperties
			// 
			this.tsbtProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtProperties.Name = "tsbtProperties";
			this.tsbtProperties.Size = new System.Drawing.Size(75, 22);
			this.tsbtProperties.Text = "Propiedades";
			this.tsbtProperties.Click += new System.EventHandler(this.properties_OnClick);
			// 
			// tsbtCloseOnSave
			// 
			this.tsbtCloseOnSave.Checked = true;
			this.tsbtCloseOnSave.CheckOnClick = true;
			this.tsbtCloseOnSave.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbtCloseOnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtCloseOnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtCloseOnSave.Image")));
			this.tsbtCloseOnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtCloseOnSave.Name = "tsbtCloseOnSave";
			this.tsbtCloseOnSave.Size = new System.Drawing.Size(228, 22);
			this.tsbtCloseOnSave.Text = "Cerrar al terminar Creación o Modificación";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtDisable
			// 
			this.tsbtDisable.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtDisable.Name = "tsbtDisable";
			this.tsbtDisable.Size = new System.Drawing.Size(91, 22);
			this.tsbtDisable.Text = "Cambiar estado";
			this.tsbtDisable.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsbtDisable.Click += new System.EventHandler(this.changeStatus_Click);
			// 
			// tsbtRefresh
			// 
			this.tsbtRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbtRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtRefresh.Image")));
			this.tsbtRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtRefresh.Name = "tsbtRefresh";
			this.tsbtRefresh.Size = new System.Drawing.Size(74, 22);
			this.tsbtRefresh.Text = "Refrescar";
			this.tsbtRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsbtRefresh.Click += new System.EventHandler(this.refreshData_OnClick);
			// 
			// dgvwUsers
			// 
			this.dgvwUsers.AllowUserToAddRows = false;
			this.dgvwUsers.AllowUserToDeleteRows = false;
			this.dgvwUsers.AutoGenerateColumns = false;
			this.dgvwUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvwUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userIdDataGridViewTextBoxColumn1,
            this.userNameDataGridViewTextBoxColumn1,
            this.firstNameDataGridViewTextBoxColumn1,
            this.lastNameDataGridViewTextBoxColumn1,
            this.fullNameDataGridViewTextBoxColumn1,
            this.mailDataGridViewTextBoxColumn1,
            this.passwordDataGridViewTextBoxColumn1,
            this.branchOfficeIdsDataGridViewTextBoxColumn1,
            this.RelativeBranchOfficeIds,
            this.branchOfficeNamesDataGridViewTextBoxColumn1,
            this.departmentIdDataGridViewTextBoxColumn1,
            this.departmentNameDataGridViewTextBoxColumn1,
            this.enabledDataGridViewTextBoxColumn,
            this.rowVersionDataGridViewImageColumn1});
			this.dgvwUsers.DataSource = this.userStructBindingSource;
			this.dgvwUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvwUsers.Location = new System.Drawing.Point(0, 25);
			this.dgvwUsers.Name = "dgvwUsers";
			this.dgvwUsers.ReadOnly = true;
			this.dgvwUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwUsers.Size = new System.Drawing.Size(675, 351);
			this.dgvwUsers.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
			this.dgvwUsers.StateCommon.DataCell.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvwUsers.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvwUsers.TabIndex = 4;
			// 
			// userStructBindingSource
			// 
			this.userStructBindingSource.AllowNew = false;
			this.userStructBindingSource.DataSource = typeof(NetSqlAzManSnapIn.AddOn.Membership.Objects.UserStruct);
			// 
			// userIdDataGridViewTextBoxColumn1
			// 
			this.userIdDataGridViewTextBoxColumn1.DataPropertyName = "UserId";
			this.userIdDataGridViewTextBoxColumn1.Frozen = true;
			this.userIdDataGridViewTextBoxColumn1.HeaderText = "Id";
			this.userIdDataGridViewTextBoxColumn1.Name = "userIdDataGridViewTextBoxColumn1";
			this.userIdDataGridViewTextBoxColumn1.ReadOnly = true;
			this.userIdDataGridViewTextBoxColumn1.Width = 50;
			// 
			// userNameDataGridViewTextBoxColumn1
			// 
			this.userNameDataGridViewTextBoxColumn1.DataPropertyName = "UserName";
			this.userNameDataGridViewTextBoxColumn1.Frozen = true;
			this.userNameDataGridViewTextBoxColumn1.HeaderText = "Usuario";
			this.userNameDataGridViewTextBoxColumn1.Name = "userNameDataGridViewTextBoxColumn1";
			this.userNameDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// firstNameDataGridViewTextBoxColumn1
			// 
			this.firstNameDataGridViewTextBoxColumn1.DataPropertyName = "FirstName";
			this.firstNameDataGridViewTextBoxColumn1.HeaderText = "FirstName";
			this.firstNameDataGridViewTextBoxColumn1.Name = "firstNameDataGridViewTextBoxColumn1";
			this.firstNameDataGridViewTextBoxColumn1.ReadOnly = true;
			this.firstNameDataGridViewTextBoxColumn1.Visible = false;
			// 
			// lastNameDataGridViewTextBoxColumn1
			// 
			this.lastNameDataGridViewTextBoxColumn1.DataPropertyName = "LastName";
			this.lastNameDataGridViewTextBoxColumn1.HeaderText = "LastName";
			this.lastNameDataGridViewTextBoxColumn1.Name = "lastNameDataGridViewTextBoxColumn1";
			this.lastNameDataGridViewTextBoxColumn1.ReadOnly = true;
			this.lastNameDataGridViewTextBoxColumn1.Visible = false;
			// 
			// fullNameDataGridViewTextBoxColumn1
			// 
			this.fullNameDataGridViewTextBoxColumn1.DataPropertyName = "FullName";
			this.fullNameDataGridViewTextBoxColumn1.HeaderText = "Nombre y Apellido";
			this.fullNameDataGridViewTextBoxColumn1.Name = "fullNameDataGridViewTextBoxColumn1";
			this.fullNameDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// mailDataGridViewTextBoxColumn1
			// 
			this.mailDataGridViewTextBoxColumn1.DataPropertyName = "Mail";
			this.mailDataGridViewTextBoxColumn1.HeaderText = "Correo";
			this.mailDataGridViewTextBoxColumn1.Name = "mailDataGridViewTextBoxColumn1";
			this.mailDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// passwordDataGridViewTextBoxColumn1
			// 
			this.passwordDataGridViewTextBoxColumn1.DataPropertyName = "Password";
			this.passwordDataGridViewTextBoxColumn1.HeaderText = "Contraseña";
			this.passwordDataGridViewTextBoxColumn1.Name = "passwordDataGridViewTextBoxColumn1";
			this.passwordDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// branchOfficeIdsDataGridViewTextBoxColumn1
			// 
			this.branchOfficeIdsDataGridViewTextBoxColumn1.DataPropertyName = "BranchOfficeIds";
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.branchOfficeIdsDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
			this.branchOfficeIdsDataGridViewTextBoxColumn1.HeaderText = "BranchOfficeIds";
			this.branchOfficeIdsDataGridViewTextBoxColumn1.Name = "branchOfficeIdsDataGridViewTextBoxColumn1";
			this.branchOfficeIdsDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// RelativeBranchOfficeIds
			// 
			this.RelativeBranchOfficeIds.DataPropertyName = "RelativeBranchOfficeIds";
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.RelativeBranchOfficeIds.DefaultCellStyle = dataGridViewCellStyle2;
			this.RelativeBranchOfficeIds.HeaderText = "RelativeBranchOfficeIds";
			this.RelativeBranchOfficeIds.Name = "RelativeBranchOfficeIds";
			this.RelativeBranchOfficeIds.ReadOnly = true;
			// 
			// branchOfficeNamesDataGridViewTextBoxColumn1
			// 
			this.branchOfficeNamesDataGridViewTextBoxColumn1.DataPropertyName = "BranchOfficeNames";
			this.branchOfficeNamesDataGridViewTextBoxColumn1.HeaderText = "Sucursales";
			this.branchOfficeNamesDataGridViewTextBoxColumn1.Name = "branchOfficeNamesDataGridViewTextBoxColumn1";
			this.branchOfficeNamesDataGridViewTextBoxColumn1.ReadOnly = true;
			this.branchOfficeNamesDataGridViewTextBoxColumn1.Width = 150;
			// 
			// departmentIdDataGridViewTextBoxColumn1
			// 
			this.departmentIdDataGridViewTextBoxColumn1.DataPropertyName = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn1.HeaderText = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn1.Name = "departmentIdDataGridViewTextBoxColumn1";
			this.departmentIdDataGridViewTextBoxColumn1.ReadOnly = true;
			this.departmentIdDataGridViewTextBoxColumn1.Visible = false;
			// 
			// departmentNameDataGridViewTextBoxColumn1
			// 
			this.departmentNameDataGridViewTextBoxColumn1.DataPropertyName = "DepartmentName";
			this.departmentNameDataGridViewTextBoxColumn1.HeaderText = "Departamento";
			this.departmentNameDataGridViewTextBoxColumn1.Name = "departmentNameDataGridViewTextBoxColumn1";
			this.departmentNameDataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// enabledDataGridViewTextBoxColumn
			// 
			this.enabledDataGridViewTextBoxColumn.DataPropertyName = "Enabled";
			this.enabledDataGridViewTextBoxColumn.HeaderText = "Habilitado";
			this.enabledDataGridViewTextBoxColumn.Name = "enabledDataGridViewTextBoxColumn";
			this.enabledDataGridViewTextBoxColumn.ReadOnly = true;
			this.enabledDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.enabledDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// rowVersionDataGridViewImageColumn1
			// 
			this.rowVersionDataGridViewImageColumn1.DataPropertyName = "_RowVersion";
			this.rowVersionDataGridViewImageColumn1.HeaderText = "[Ver.]";
			this.rowVersionDataGridViewImageColumn1.Name = "rowVersionDataGridViewImageColumn1";
			this.rowVersionDataGridViewImageColumn1.ReadOnly = true;
			this.rowVersionDataGridViewImageColumn1.Visible = false;
			// 
			// UsersUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(675, 376);
			this.Controls.Add(this.dgvwUsers);
			this.Controls.Add(this.tstpOptions);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "UsersUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Usuarios";
			this.tstpOptions.ResumeLayout(false);
			this.tstpOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvwUsers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userStructBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tstpOptions;
		private System.Windows.Forms.ToolStripButton tsbtDisable;
		private System.Windows.Forms.ToolStripButton tsbtRefresh;
		private System.Windows.Forms.ToolStripButton tsbtNew;
		private System.Windows.Forms.ToolStripButton tsbtProperties;
		private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvwUsers;
		private System.Windows.Forms.BindingSource userStructBindingSource;
		private System.Windows.Forms.ToolStripButton tsbtCloseOnSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn mailDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeIdsDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn RelativeBranchOfficeIds;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeNamesDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentIdDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentNameDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn rowVersionDataGridViewImageColumn1;
	}
}

