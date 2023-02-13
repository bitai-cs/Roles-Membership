namespace AzManWinUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginUI));
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.lablPassword = new System.Windows.Forms.Label();
			this.butnOk = new System.Windows.Forms.Button();
			this.butnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtbPassword
			// 
			this.txtbPassword.Location = new System.Drawing.Point(106, 16);
			this.txtbPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '*';
			this.txtbPassword.Size = new System.Drawing.Size(176, 22);
			this.txtbPassword.TabIndex = 0;
			// 
			// lablPassword
			// 
			this.lablPassword.AutoSize = true;
			this.lablPassword.Location = new System.Drawing.Point(13, 21);
			this.lablPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lablPassword.Name = "lablPassword";
			this.lablPassword.Size = new System.Drawing.Size(85, 17);
			this.lablPassword.TabIndex = 1;
			this.lablPassword.Text = "Contraseña:";
			// 
			// butnOk
			// 
			this.butnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butnOk.Location = new System.Drawing.Point(106, 60);
			this.butnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(84, 30);
			this.butnOk.TabIndex = 2;
			this.butnOk.Text = "&Ok";
			this.butnOk.UseVisualStyleBackColor = true;
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// butnCancel
			// 
			this.butnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butnCancel.Location = new System.Drawing.Point(198, 60);
			this.butnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.butnCancel.Name = "butnCancel";
			this.butnCancel.Size = new System.Drawing.Size(84, 30);
			this.butnCancel.TabIndex = 3;
			this.butnCancel.Text = "&Cancel";
			this.butnCancel.UseVisualStyleBackColor = true;
			// 
			// LoginUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.butnCancel;
			this.ClientSize = new System.Drawing.Size(295, 97);
			this.Controls.Add(this.butnCancel);
			this.Controls.Add(this.butnOk);
			this.Controls.Add(this.lablPassword);
			this.Controls.Add(this.txtbPassword);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Consola seguridad";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginUI_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.Label lablPassword;
		private System.Windows.Forms.Button butnOk;
		private System.Windows.Forms.Button butnCancel;
	}
}