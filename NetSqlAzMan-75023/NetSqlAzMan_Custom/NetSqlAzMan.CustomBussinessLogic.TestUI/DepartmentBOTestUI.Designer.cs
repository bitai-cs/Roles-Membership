namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	partial class DepartmentBOTestUI {
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
			this.butnGetAllAsync = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.departmentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.butnGetByUserId = new System.Windows.Forms.Button();
			this.txtbUserID = new System.Windows.Forms.TextBox();
			this.butnUpdateAsync = new System.Windows.Forms.Button();
			this.txtbNewDepName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butnInsertAsync = new System.Windows.Forms.Button();
			this.butnDeleteAsync = new System.Windows.Forms.Button();
			this.txtbDepNameFilter = new System.Windows.Forms.TextBox();
			this.butnGetById = new System.Windows.Forms.Button();
			this.txtbId = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// butnGetAllAsync
			// 
			this.butnGetAllAsync.Location = new System.Drawing.Point(12, 12);
			this.butnGetAllAsync.Name = "butnGetAllAsync";
			this.butnGetAllAsync.Size = new System.Drawing.Size(81, 26);
			this.butnGetAllAsync.TabIndex = 0;
			this.butnGetAllAsync.Text = "Listar todo";
			this.butnGetAllAsync.UseVisualStyleBackColor = true;
			this.butnGetAllAsync.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.departmentIdDataGridViewTextBoxColumn,
            this.departmentNameDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.departmentBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(12, 76);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(364, 195);
			this.dataGridView1.TabIndex = 1;
			// 
			// departmentIdDataGridViewTextBoxColumn
			// 
			this.departmentIdDataGridViewTextBoxColumn.DataPropertyName = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.HeaderText = "DepartmentId";
			this.departmentIdDataGridViewTextBoxColumn.Name = "departmentIdDataGridViewTextBoxColumn";
			this.departmentIdDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// departmentNameDataGridViewTextBoxColumn
			// 
			this.departmentNameDataGridViewTextBoxColumn.DataPropertyName = "DepartmentName";
			this.departmentNameDataGridViewTextBoxColumn.HeaderText = "DepartmentName";
			this.departmentNameDataGridViewTextBoxColumn.Name = "departmentNameDataGridViewTextBoxColumn";
			this.departmentNameDataGridViewTextBoxColumn.Width = 200;
			// 
			// departmentBindingSource
			// 
			this.departmentBindingSource.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.Department);
			this.departmentBindingSource.CurrentItemChanged += new System.EventHandler(this.departmentBindingSource_CurrentItemChanged);
			// 
			// butnGetByUserId
			// 
			this.butnGetByUserId.Location = new System.Drawing.Point(153, 44);
			this.butnGetByUserId.Name = "butnGetByUserId";
			this.butnGetByUserId.Size = new System.Drawing.Size(118, 26);
			this.butnGetByUserId.TabIndex = 2;
			this.butnGetByUserId.Text = "Buscar x Id Usuario";
			this.butnGetByUserId.UseVisualStyleBackColor = true;
			this.butnGetByUserId.Click += new System.EventHandler(this.button2_Click);
			// 
			// txtbUserID
			// 
			this.txtbUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbUserID.Location = new System.Drawing.Point(277, 46);
			this.txtbUserID.Name = "txtbUserID";
			this.txtbUserID.Size = new System.Drawing.Size(37, 21);
			this.txtbUserID.TabIndex = 4;
			this.txtbUserID.Text = "13";
			this.txtbUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// butnUpdateAsync
			// 
			this.butnUpdateAsync.Location = new System.Drawing.Point(12, 277);
			this.butnUpdateAsync.Name = "butnUpdateAsync";
			this.butnUpdateAsync.Size = new System.Drawing.Size(140, 26);
			this.butnUpdateAsync.TabIndex = 5;
			this.butnUpdateAsync.Text = "Actualizar registro actual";
			this.butnUpdateAsync.UseVisualStyleBackColor = true;
			this.butnUpdateAsync.Click += new System.EventHandler(this.button3_Click);
			// 
			// txtbNewDepName
			// 
			this.txtbNewDepName.Location = new System.Drawing.Point(127, 328);
			this.txtbNewDepName.Name = "txtbNewDepName";
			this.txtbNewDepName.Size = new System.Drawing.Size(166, 20);
			this.txtbNewDepName.TabIndex = 6;
			this.txtbNewDepName.Text = "Departamento Temporal";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 335);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Nuevo Departamento";
			// 
			// butnInsertAsync
			// 
			this.butnInsertAsync.Location = new System.Drawing.Point(299, 325);
			this.butnInsertAsync.Name = "butnInsertAsync";
			this.butnInsertAsync.Size = new System.Drawing.Size(75, 23);
			this.butnInsertAsync.TabIndex = 8;
			this.butnInsertAsync.Text = "Registrar";
			this.butnInsertAsync.UseVisualStyleBackColor = true;
			this.butnInsertAsync.Click += new System.EventHandler(this.butnInsertAsync_Click);
			// 
			// butnDeleteAsync
			// 
			this.butnDeleteAsync.Location = new System.Drawing.Point(242, 277);
			this.butnDeleteAsync.Name = "butnDeleteAsync";
			this.butnDeleteAsync.Size = new System.Drawing.Size(134, 26);
			this.butnDeleteAsync.TabIndex = 9;
			this.butnDeleteAsync.Text = "Eliminar registro actual";
			this.butnDeleteAsync.UseVisualStyleBackColor = true;
			this.butnDeleteAsync.Click += new System.EventHandler(this.button5_Click);
			// 
			// txtbDepNameFilter
			// 
			this.txtbDepNameFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbDepNameFilter.Location = new System.Drawing.Point(99, 14);
			this.txtbDepNameFilter.Name = "txtbDepNameFilter";
			this.txtbDepNameFilter.Size = new System.Drawing.Size(215, 21);
			this.txtbDepNameFilter.TabIndex = 10;
			this.txtbDepNameFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// butnGetById
			// 
			this.butnGetById.Location = new System.Drawing.Point(12, 44);
			this.butnGetById.Name = "butnGetById";
			this.butnGetById.Size = new System.Drawing.Size(81, 26);
			this.butnGetById.TabIndex = 11;
			this.butnGetById.Text = "Buscar x Id";
			this.butnGetById.UseVisualStyleBackColor = true;
			this.butnGetById.Click += new System.EventHandler(this.butnGetById_Click);
			// 
			// txtbId
			// 
			this.txtbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbId.Location = new System.Drawing.Point(99, 46);
			this.txtbId.Name = "txtbId";
			this.txtbId.Size = new System.Drawing.Size(37, 21);
			this.txtbId.TabIndex = 12;
			this.txtbId.Text = "45";
			this.txtbId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// DepartmentBOTestUI
			// 
			this.AcceptButton = this.butnGetAllAsync;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(386, 366);
			this.Controls.Add(this.txtbId);
			this.Controls.Add(this.butnGetById);
			this.Controls.Add(this.txtbDepNameFilter);
			this.Controls.Add(this.butnDeleteAsync);
			this.Controls.Add(this.butnInsertAsync);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtbNewDepName);
			this.Controls.Add(this.butnUpdateAsync);
			this.Controls.Add(this.txtbUserID);
			this.Controls.Add(this.butnGetByUserId);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.butnGetAllAsync);
			this.Name = "DepartmentBOTestUI";
			this.Text = "DepartmentBOTestUI";
			this.Load += new System.EventHandler(this.DepartmentBOTestUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.departmentBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butnGetAllAsync;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewImageColumn recordVersionDataGridViewImageColumn;
		private System.Windows.Forms.BindingSource departmentBindingSource;
		private System.Windows.Forms.Button butnGetByUserId;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.TextBox txtbUserID;
		private System.Windows.Forms.Button butnUpdateAsync;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn departmentNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.TextBox txtbNewDepName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butnInsertAsync;
		private System.Windows.Forms.Button butnDeleteAsync;
		private System.Windows.Forms.TextBox txtbDepNameFilter;
		private System.Windows.Forms.Button butnGetById;
		private System.Windows.Forms.TextBox txtbId;
	}
}