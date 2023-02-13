namespace AzManWebServicesTest
{
	partial class DirectSvcTestUI
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
			this.txtbOutput = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.butnAzMan_GetDBUser = new System.Windows.Forms.Button();
			this.txtbUserName = new System.Windows.Forms.TextBox();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.txtbItemName = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txtbUseName2 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtbOutput
			// 
			this.txtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbOutput.BackColor = System.Drawing.Color.Black;
			this.txtbOutput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbOutput.ForeColor = System.Drawing.Color.Lime;
			this.txtbOutput.Location = new System.Drawing.Point(16, 138);
			this.txtbOutput.Margin = new System.Windows.Forms.Padding(4);
			this.txtbOutput.Multiline = true;
			this.txtbOutput.Name = "txtbOutput";
			this.txtbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtbOutput.Size = new System.Drawing.Size(1256, 500);
			this.txtbOutput.TabIndex = 7;
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.Location = new System.Drawing.Point(307, 15);
			this.button3.Margin = new System.Windows.Forms.Padding(4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(283, 26);
			this.button3.TabIndex = 6;
			this.button3.Text = "AzMan_ValidatePassword";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(597, 15);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(280, 26);
			this.button2.TabIndex = 5;
			this.button2.Text = "AzMan_CheckAccess";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// butnAzMan_GetDBUser
			// 
			this.butnAzMan_GetDBUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.butnAzMan_GetDBUser.Location = new System.Drawing.Point(16, 15);
			this.butnAzMan_GetDBUser.Margin = new System.Windows.Forms.Padding(4);
			this.butnAzMan_GetDBUser.Name = "butnAzMan_GetDBUser";
			this.butnAzMan_GetDBUser.Size = new System.Drawing.Size(283, 26);
			this.butnAzMan_GetDBUser.TabIndex = 4;
			this.butnAzMan_GetDBUser.Text = "AzMan_GetDBUser";
			this.butnAzMan_GetDBUser.UseVisualStyleBackColor = true;
			this.butnAzMan_GetDBUser.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtbUserName
			// 
			this.txtbUserName.Location = new System.Drawing.Point(16, 49);
			this.txtbUserName.Margin = new System.Windows.Forms.Padding(4);
			this.txtbUserName.Name = "txtbUserName";
			this.txtbUserName.Size = new System.Drawing.Size(281, 20);
			this.txtbUserName.TabIndex = 8;
			this.txtbUserName.Text = "sise\\vbastidas";
			// 
			// txtbPassword
			// 
			this.txtbPassword.Location = new System.Drawing.Point(307, 48);
			this.txtbPassword.Margin = new System.Windows.Forms.Padding(4);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '?';
			this.txtbPassword.Size = new System.Drawing.Size(281, 20);
			this.txtbPassword.TabIndex = 9;
			this.txtbPassword.Text = "17011981";
			// 
			// txtbItemName
			// 
			this.txtbItemName.Location = new System.Drawing.Point(597, 48);
			this.txtbItemName.Margin = new System.Windows.Forms.Padding(4);
			this.txtbItemName.Name = "txtbItemName";
			this.txtbItemName.Size = new System.Drawing.Size(279, 20);
			this.txtbItemName.TabIndex = 10;
			this.txtbItemName.Text = "Sesion";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(991, 15);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(283, 43);
			this.button1.TabIndex = 11;
			this.button1.Text = "AzMan_ChangePassword";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// txtbUseName2
			// 
			this.txtbUseName2.Location = new System.Drawing.Point(16, 110);
			this.txtbUseName2.Margin = new System.Windows.Forms.Padding(4);
			this.txtbUseName2.Name = "txtbUseName2";
			this.txtbUseName2.Size = new System.Drawing.Size(225, 20);
			this.txtbUseName2.TabIndex = 13;
			this.txtbUseName2.Text = "sise\\vbastidas";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button4.Location = new System.Drawing.Point(16, 77);
			this.button4.Margin = new System.Windows.Forms.Padding(4);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(225, 25);
			this.button4.TabIndex = 12;
			this.button4.Text = "AzMan_GetDBUser";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// DirectSvcTestUI
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1289, 654);
			this.Controls.Add(this.txtbUseName2);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtbItemName);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.txtbUserName);
			this.Controls.Add(this.txtbOutput);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.butnAzMan_GetDBUser);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "DirectSvcTestUI";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.TestUI_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtbOutput;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button butnAzMan_GetDBUser;
		private System.Windows.Forms.TextBox txtbUserName;
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.TextBox txtbItemName;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtbUseName2;
		private System.Windows.Forms.Button button4;
	}
}

