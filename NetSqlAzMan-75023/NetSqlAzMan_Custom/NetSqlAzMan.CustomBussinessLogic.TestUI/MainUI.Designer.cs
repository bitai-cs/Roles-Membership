namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	partial class MainUI {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.departamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.branchOfficeUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dBUserUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lDAPClientUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(930, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.departamentoToolStripMenuItem,
            this.branchOfficeUIToolStripMenuItem,
            this.dBUserUIToolStripMenuItem,
            this.lDAPClientUIToolStripMenuItem});
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.testToolStripMenuItem.Text = "&Test";
			// 
			// departamentoToolStripMenuItem
			// 
			this.departamentoToolStripMenuItem.Name = "departamentoToolStripMenuItem";
			this.departamentoToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.departamentoToolStripMenuItem.Text = "DepartmentUI";
			this.departamentoToolStripMenuItem.Click += new System.EventHandler(this.departamentoToolStripMenuItem_Click);
			// 
			// branchOfficeUIToolStripMenuItem
			// 
			this.branchOfficeUIToolStripMenuItem.Name = "branchOfficeUIToolStripMenuItem";
			this.branchOfficeUIToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.branchOfficeUIToolStripMenuItem.Text = "BranchOfficeUI";
			this.branchOfficeUIToolStripMenuItem.Click += new System.EventHandler(this.branchOfficeUIToolStripMenuItem_Click);
			// 
			// dBUserUIToolStripMenuItem
			// 
			this.dBUserUIToolStripMenuItem.Name = "dBUserUIToolStripMenuItem";
			this.dBUserUIToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.dBUserUIToolStripMenuItem.Text = "DBUserUI";
			this.dBUserUIToolStripMenuItem.Click += new System.EventHandler(this.dBUserUIToolStripMenuItem_Click);
			// 
			// lDAPClientUIToolStripMenuItem
			// 
			this.lDAPClientUIToolStripMenuItem.Name = "lDAPClientUIToolStripMenuItem";
			this.lDAPClientUIToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.lDAPClientUIToolStripMenuItem.Text = "LDAPClientUI";
			this.lDAPClientUIToolStripMenuItem.Click += new System.EventHandler(this.lDAPClientUIToolStripMenuItem_Click);
			// 
			// MainUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(930, 452);
			this.Controls.Add(this.menuStrip1);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainUI";
			this.Text = "MainUI";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem departamentoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem branchOfficeUIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dBUserUIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lDAPClientUIToolStripMenuItem;
	}
}