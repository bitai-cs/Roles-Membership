namespace AzManLoginWinForms
{
	partial class LoginBlUI
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
			this.picbLogo = new System.Windows.Forms.PictureBox();
			this.butnLogin = new System.Windows.Forms.Button();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.txtbUser = new System.Windows.Forms.TextBox();
			this.labelControl2 = new System.Windows.Forms.Label();
			this.labelControl1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// picbLogo
			// 
			this.picbLogo.BackgroundImage = global::AzManLoginWinForms.Properties.Resources.SISE_FONDOPLOMO;
			this.picbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picbLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.picbLogo.Location = new System.Drawing.Point(0, 0);
			this.picbLogo.Name = "picbLogo";
			this.picbLogo.Size = new System.Drawing.Size(285, 87);
			this.picbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picbLogo.TabIndex = 4;
			this.picbLogo.TabStop = false;
			// 
			// butnLogin
			// 
			this.butnLogin.AutoSize = true;
			this.butnLogin.Location = new System.Drawing.Point(143, 180);
			this.butnLogin.Name = "butnLogin";
			this.butnLogin.Size = new System.Drawing.Size(124, 36);
			this.butnLogin.TabIndex = 4;
			this.butnLogin.Text = "Verificar";
			this.butnLogin.Click += new System.EventHandler(this.butnLogin_Click);
			// 
			// txtbPassword
			// 
			this.txtbPassword.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbPassword.Location = new System.Drawing.Point(120, 137);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '*';
			this.txtbPassword.Size = new System.Drawing.Size(147, 26);
			this.txtbPassword.TabIndex = 3;
			// 
			// txtbUser
			// 
			this.txtbUser.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbUser.Location = new System.Drawing.Point(120, 102);
			this.txtbUser.Name = "txtbUser";
			this.txtbUser.Size = new System.Drawing.Size(147, 26);
			this.txtbUser.TabIndex = 2;
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSize = true;
			this.labelControl2.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl2.Location = new System.Drawing.Point(16, 146);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(96, 17);
			this.labelControl2.TabIndex = 1;
			this.labelControl2.Text = "Contraseña:";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSize = true;
			this.labelControl1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl1.Location = new System.Drawing.Point(16, 111);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(72, 17);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "Usuario:";
			// 
			// LoginBlUI
			// 
			this.AcceptButton = this.butnLogin;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(285, 230);
			this.Controls.Add(this.butnLogin);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.picbLogo);
			this.Controls.Add(this.txtbUser);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.labelControl2);
			this.Name = "LoginBlUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.LoginUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picbLogo;
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.TextBox txtbUser;
		private System.Windows.Forms.Label labelControl2;
		private System.Windows.Forms.Label labelControl1;
		private System.Windows.Forms.Button butnLogin;
	}
}
