namespace AzManLoginWinForms
{
	partial class LoginUI
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
			this.labelControl2 = new System.Windows.Forms.Label();
			this.labelControl1 = new System.Windows.Forms.Label();
			this.txtbUser = new System.Windows.Forms.TextBox();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.butnLogin = new System.Windows.Forms.Button();
			this.pictConfig = new System.Windows.Forms.PictureBox();
			this.picbLogo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictConfig)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.Location = new System.Drawing.Point(13, 149);
			this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(107, 17);
			this.labelControl2.TabIndex = 7;
			this.labelControl2.Text = "Contraseña:";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.Location = new System.Drawing.Point(13, 111);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(80, 17);
			this.labelControl1.TabIndex = 6;
			this.labelControl1.Text = "Usuario:";
			// 
			// txtbUser
			// 
			this.txtbUser.AutoCompleteCustomSource.AddRange(new string[] {
            "VBASTIDAS",
            "ADMCAMPUS",
            "ADMIN"});
			this.txtbUser.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbUser.Location = new System.Drawing.Point(130, 102);
			this.txtbUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtbUser.Name = "txtbUser";
			this.txtbUser.Size = new System.Drawing.Size(138, 26);
			this.txtbUser.TabIndex = 8;
			// 
			// txtbPassword
			// 
			this.txtbPassword.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbPassword.Location = new System.Drawing.Point(130, 140);
			this.txtbPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '*';
			this.txtbPassword.Size = new System.Drawing.Size(138, 26);
			this.txtbPassword.TabIndex = 9;
			// 
			// butnLogin
			// 
			this.butnLogin.AutoSize = true;
			this.butnLogin.Location = new System.Drawing.Point(129, 187);
			this.butnLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.butnLogin.Name = "butnLogin";
			this.butnLogin.Size = new System.Drawing.Size(139, 32);
			this.butnLogin.TabIndex = 10;
			this.butnLogin.Text = "Verificar";
			this.butnLogin.Click += new System.EventHandler(this.butnLogin_Click);
			// 
			// pictConfig
			// 
			this.pictConfig.Image = global::AzManLoginWinForms.Properties.Resources.configuration2;
			this.pictConfig.Location = new System.Drawing.Point(12, 187);
			this.pictConfig.Name = "pictConfig";
			this.pictConfig.Size = new System.Drawing.Size(32, 32);
			this.pictConfig.TabIndex = 11;
			this.pictConfig.TabStop = false;
			this.pictConfig.Click += new System.EventHandler(this.pictConfig_Click);
			// 
			// picbLogo
			// 
			this.picbLogo.BackgroundImage = global::AzManLoginWinForms.Properties.Resources.SISE_FONDOPLOMO;
			this.picbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picbLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.picbLogo.Location = new System.Drawing.Point(0, 0);
			this.picbLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picbLogo.Name = "picbLogo";
			this.picbLogo.Size = new System.Drawing.Size(285, 85);
			this.picbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picbLogo.TabIndex = 5;
			this.picbLogo.TabStop = false;
			// 
			// LoginUI
			// 
			this.AcceptButton = this.butnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(285, 231);
			this.Controls.Add(this.pictConfig);
			this.Controls.Add(this.butnLogin);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.txtbUser);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.picbLogo);
			this.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Autenticación";
			this.Load += new System.EventHandler(this.LoginUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictConfig)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picbLogo;
		private System.Windows.Forms.Label labelControl2;
		private System.Windows.Forms.Label labelControl1;
		private System.Windows.Forms.TextBox txtbUser;
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.Button butnLogin;
		private System.Windows.Forms.PictureBox pictConfig;

	}
}