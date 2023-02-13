namespace AzManLoginWebServicesTest
{
	partial class TestUI
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
			this.AzManLogin_GetLogin = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbUsuario = new System.Windows.Forms.TextBox();
			this.txtbContrasena = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// AzManLogin_GetLogin
			// 
			this.AzManLogin_GetLogin.Location = new System.Drawing.Point(8, 74);
			this.AzManLogin_GetLogin.Margin = new System.Windows.Forms.Padding(4);
			this.AzManLogin_GetLogin.Name = "AzManLogin_GetLogin";
			this.AzManLogin_GetLogin.Size = new System.Drawing.Size(326, 35);
			this.AzManLogin_GetLogin.TabIndex = 0;
			this.AzManLogin_GetLogin.Text = "AzManLoginWebServices_StartLogin";
			this.AzManLogin_GetLogin.UseVisualStyleBackColor = true;
			this.AzManLogin_GetLogin.Click += new System.EventHandler(this.butnAzManLoginGetLogin_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 118);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Usuario:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(158, 118);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Clave:";
			// 
			// txtbUsuario
			// 
			this.txtbUsuario.Location = new System.Drawing.Point(77, 112);
			this.txtbUsuario.Name = "txtbUsuario";
			this.txtbUsuario.Size = new System.Drawing.Size(66, 22);
			this.txtbUsuario.TabIndex = 3;
			this.txtbUsuario.Text = "admin";
			// 
			// txtbContrasena
			// 
			this.txtbContrasena.Location = new System.Drawing.Point(216, 112);
			this.txtbContrasena.Name = "txtbContrasena";
			this.txtbContrasena.PasswordChar = '?';
			this.txtbContrasena.Size = new System.Drawing.Size(118, 22);
			this.txtbContrasena.TabIndex = 4;
			this.txtbContrasena.Text = "17011981";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 9);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(326, 35);
			this.button1.TabIndex = 5;
			this.button1.Text = "AzManLoginWebServices_Test";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(8, 205);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(326, 52);
			this.button2.TabIndex = 6;
			this.button2.Text = "AzManLoginWebServices_ChangePassword";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(5, 156);
			this.button3.Margin = new System.Windows.Forms.Padding(4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(326, 35);
			this.button3.TabIndex = 7;
			this.button3.Text = "AzManLoginWebServices_CheckLoginAccess";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// TestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 270);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtbContrasena);
			this.Controls.Add(this.txtbUsuario);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.AzManLogin_GetLogin);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "TestUI";
			this.Text = "AzManLoginWebServicesTest";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button AzManLogin_GetLogin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbUsuario;
		private System.Windows.Forms.TextBox txtbContrasena;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}

