namespace AzManLoginWinFormsTest
{
	partial class TestLoginUI
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
			this.combStore = new System.Windows.Forms.ComboBox();
			this.combApplication = new System.Windows.Forms.ComboBox();
			this.combItem = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butnLogin = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.gridView1 = new System.Windows.Forms.DataGridView();
			this.lablBranch = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// combStore
			// 
			this.combStore.FormattingEnabled = true;
			this.combStore.Location = new System.Drawing.Point(71, 12);
			this.combStore.Name = "combStore";
			this.combStore.Size = new System.Drawing.Size(108, 28);
			this.combStore.TabIndex = 0;
			// 
			// combApplication
			// 
			this.combApplication.FormattingEnabled = true;
			this.combApplication.Location = new System.Drawing.Point(282, 12);
			this.combApplication.Name = "combApplication";
			this.combApplication.Size = new System.Drawing.Size(254, 28);
			this.combApplication.TabIndex = 1;
			// 
			// combItem
			// 
			this.combItem.FormattingEnabled = true;
			this.combItem.Location = new System.Drawing.Point(593, 12);
			this.combItem.Name = "combItem";
			this.combItem.Size = new System.Drawing.Size(186, 28);
			this.combItem.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Store:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(185, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Application:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(542, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Item:";
			// 
			// butnLogin
			// 
			this.butnLogin.Location = new System.Drawing.Point(17, 50);
			this.butnLogin.Name = "butnLogin";
			this.butnLogin.Size = new System.Drawing.Size(80, 30);
			this.butnLogin.TabIndex = 6;
			this.butnLogin.Text = "Login";
			this.butnLogin.UseVisualStyleBackColor = true;
			this.butnLogin.Click += new System.EventHandler(this.butnLogin_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(127, 50);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 29);
			this.button1.TabIndex = 7;
			this.button1.Text = "Seleccionar Sede";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// gridView1
			// 
			this.gridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridView1.Location = new System.Drawing.Point(17, 86);
			this.gridView1.Name = "gridView1";
			this.gridView1.RowTemplate.Height = 28;
			this.gridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridView1.Size = new System.Drawing.Size(762, 170);
			this.gridView1.TabIndex = 8;
			this.gridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			// 
			// lablBranch
			// 
			this.lablBranch.AutoSize = true;
			this.lablBranch.Location = new System.Drawing.Point(307, 55);
			this.lablBranch.Name = "lablBranch";
			this.lablBranch.Size = new System.Drawing.Size(21, 20);
			this.lablBranch.TabIndex = 9;
			this.lablBranch.Text = "...";
			// 
			// TestLoginUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(808, 268);
			this.Controls.Add(this.lablBranch);
			this.Controls.Add(this.gridView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butnLogin);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.combItem);
			this.Controls.Add(this.combApplication);
			this.Controls.Add(this.combStore);
			this.Name = "TestLoginUI";
			this.Text = "TestLoginUI";
			this.Load += new System.EventHandler(this.TestLoginUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox combStore;
		private System.Windows.Forms.ComboBox combApplication;
		private System.Windows.Forms.ComboBox combItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butnLogin;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView gridView1;
		private System.Windows.Forms.Label lablBranch;
	}
}