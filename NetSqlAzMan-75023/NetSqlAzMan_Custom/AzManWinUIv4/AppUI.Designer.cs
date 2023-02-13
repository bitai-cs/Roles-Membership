namespace AzManWinUI
{
	partial class AppUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppUI));
			this.mnusMain = new System.Windows.Forms.MenuStrip();
			this.tsmiConsole = new System.Windows.Forms.ToolStripMenuItem();
			this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiConsoleExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiToolsPreferences = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiToolsOpenConfigFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiToolsChangePassword = new System.Windows.Forms.ToolStripMenuItem();
			this.mnusMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnusMain
			// 
			this.mnusMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mnusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConsole});
			this.mnusMain.Location = new System.Drawing.Point(0, 0);
			this.mnusMain.Name = "mnusMain";
			this.mnusMain.Size = new System.Drawing.Size(984, 24);
			this.mnusMain.TabIndex = 1;
			this.mnusMain.Text = "menuStrip1";
			// 
			// tsmiConsole
			// 
			this.tsmiConsole.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem,
            this.tsmiConsoleExit});
			this.tsmiConsole.Name = "tsmiConsole";
			this.tsmiConsole.Size = new System.Drawing.Size(75, 20);
			this.tsmiConsole.Text = "&Aplicación";
			// 
			// dummyToolStripMenuItem
			// 
			this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
			this.dummyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.dummyToolStripMenuItem.Text = "Test Manager";
			this.dummyToolStripMenuItem.Click += new System.EventHandler(this.dummyToolStripMenuItem_Click);
			// 
			// tsmiConsoleExit
			// 
			this.tsmiConsoleExit.Name = "tsmiConsoleExit";
			this.tsmiConsoleExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.tsmiConsoleExit.Size = new System.Drawing.Size(148, 22);
			this.tsmiConsoleExit.Text = "&Cerrar";
			this.tsmiConsoleExit.Click += new System.EventHandler(this.tsmiConsoleExit_Click);
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiToolsPreferences,
            this.tsmiToolsOpenConfigFile,
            this.tsmiToolsChangePassword});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(90, 20);
			this.tsmiTools.Text = "&Herramientas";
			// 
			// tsmiToolsPreferences
			// 
			this.tsmiToolsPreferences.Name = "tsmiToolsPreferences";
			this.tsmiToolsPreferences.Size = new System.Drawing.Size(235, 22);
			this.tsmiToolsPreferences.Text = "&Preferencias";
			this.tsmiToolsPreferences.Click += new System.EventHandler(this.tsmiToolsPreferences_Click);
			// 
			// tsmiToolsOpenConfigFile
			// 
			this.tsmiToolsOpenConfigFile.Name = "tsmiToolsOpenConfigFile";
			this.tsmiToolsOpenConfigFile.Size = new System.Drawing.Size(235, 22);
			this.tsmiToolsOpenConfigFile.Text = "&Abrir archivo de configuración";
			this.tsmiToolsOpenConfigFile.Click += new System.EventHandler(this.tsmiToolsOpenConfigFile_Click);
			// 
			// tsmiToolsChangePassword
			// 
			this.tsmiToolsChangePassword.Name = "tsmiToolsChangePassword";
			this.tsmiToolsChangePassword.Size = new System.Drawing.Size(235, 22);
			this.tsmiToolsChangePassword.Text = "&Cambiar contraseña";
			this.tsmiToolsChangePassword.Visible = false;
			this.tsmiToolsChangePassword.Click += new System.EventHandler(this.tsmiToolsChangePassword_Click);
			// 
			// AppUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 462);
			this.Controls.Add(this.mnusMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "AppUI";
			this.Text = "AppUI";
			this.Load += new System.EventHandler(this.AppUI_Load);
			this.Shown += new System.EventHandler(this.AppUI_Shown);
			this.mnusMain.ResumeLayout(false);
			this.mnusMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnusMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiConsole;
		private System.Windows.Forms.ToolStripMenuItem tsmiConsoleExit;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiToolsPreferences;
		private System.Windows.Forms.ToolStripMenuItem tsmiToolsOpenConfigFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiToolsChangePassword;
		private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem;
	}
}