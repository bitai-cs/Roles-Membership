namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	partial class BranchOfficeBOTestUI {
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.dgvwAll = new System.Windows.Forms.DataGridView();
			this.branchOfficeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.relativeBranchOfficeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.branchOfficeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvwAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.branchOfficeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(74, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "GetAllAsync";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dgvwAll
			// 
			this.dgvwAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvwAll.AutoGenerateColumns = false;
			this.dgvwAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwAll.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.branchOfficeIdDataGridViewTextBoxColumn,
            this.branchOfficeNameDataGridViewTextBoxColumn,
            this.relativeBranchOfficeIdDataGridViewTextBoxColumn});
			this.dgvwAll.DataSource = this.branchOfficeBindingSource;
			this.dgvwAll.Location = new System.Drawing.Point(12, 42);
			this.dgvwAll.MultiSelect = false;
			this.dgvwAll.Name = "dgvwAll";
			this.dgvwAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwAll.Size = new System.Drawing.Size(355, 229);
			this.dgvwAll.TabIndex = 1;
			// 
			// branchOfficeIdDataGridViewTextBoxColumn
			// 
			this.branchOfficeIdDataGridViewTextBoxColumn.DataPropertyName = "BranchOfficeId";
			this.branchOfficeIdDataGridViewTextBoxColumn.HeaderText = "BranchOfficeId";
			this.branchOfficeIdDataGridViewTextBoxColumn.Name = "branchOfficeIdDataGridViewTextBoxColumn";
			// 
			// branchOfficeNameDataGridViewTextBoxColumn
			// 
			this.branchOfficeNameDataGridViewTextBoxColumn.DataPropertyName = "BranchOfficeName";
			this.branchOfficeNameDataGridViewTextBoxColumn.HeaderText = "BranchOfficeName";
			this.branchOfficeNameDataGridViewTextBoxColumn.Name = "branchOfficeNameDataGridViewTextBoxColumn";
			// 
			// relativeBranchOfficeIdDataGridViewTextBoxColumn
			// 
			this.relativeBranchOfficeIdDataGridViewTextBoxColumn.DataPropertyName = "RelativeBranchOfficeId";
			this.relativeBranchOfficeIdDataGridViewTextBoxColumn.HeaderText = "RelativeBranchOfficeId";
			this.relativeBranchOfficeIdDataGridViewTextBoxColumn.Name = "relativeBranchOfficeIdDataGridViewTextBoxColumn";
			// 
			// branchOfficeBindingSource
			// 
			this.branchOfficeBindingSource.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.BranchOffice);
			// 
			// dataGridView2
			// 
			this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView2.AutoGenerateColumns = false;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
			this.dataGridView2.DataSource = this.bindingSource1;
			this.dataGridView2.Location = new System.Drawing.Point(12, 323);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.Size = new System.Drawing.Size(355, 123);
			this.dataGridView2.TabIndex = 2;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "BranchOfficeId";
			this.dataGridViewTextBoxColumn1.HeaderText = "BranchOfficeId";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "BranchOfficeName";
			this.dataGridViewTextBoxColumn2.HeaderText = "BranchOfficeName";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "RelativeBranchOfficeId";
			this.dataGridViewTextBoxColumn3.HeaderText = "RelativeBranchOfficeId";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.BranchOffice);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(12, 294);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(138, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "GetAllByUserIDAsync";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Location = new System.Drawing.Point(156, 297);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(40, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "75";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(103, 12);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(84, 24);
			this.button3.TabIndex = 5;
			this.button3.Text = "RegisterAsync";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(193, 12);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(84, 24);
			this.button4.TabIndex = 6;
			this.button4.Text = "UpdateAsync";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(283, 12);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(79, 24);
			this.button5.TabIndex = 7;
			this.button5.Text = "DeleteAsync";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// BranchOfficeBOTestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 457);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this.dgvwAll);
			this.Controls.Add(this.button1);
			this.Name = "BranchOfficeBOTestUI";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dgvwAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.branchOfficeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dgvwAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn branchOfficeNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn relativeBranchOfficeIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource branchOfficeBindingSource;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
	}
}

