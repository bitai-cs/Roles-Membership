namespace Basgosoft.NetSqlAzManCacheWSSqlClrClientTest
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.txtbItemName = new System.Windows.Forms.TextBox();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.txtbUserName = new System.Windows.Forms.TextBox();
			this.txtbOutput = new System.Windows.Forms.TextBox();
			this.chkbTestSqlClrAssembly = new System.Windows.Forms.CheckBox();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(126, 33);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(153, 35);
			this.button1.TabIndex = 0;
			this.button1.Text = "AzMan_GetDBUser";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(503, 33);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(170, 35);
			this.button2.TabIndex = 1;
			this.button2.Text = "AzMan_CheckAccess";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.Location = new System.Drawing.Point(285, 33);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(212, 35);
			this.button3.TabIndex = 2;
			this.button3.Text = "AzMan_ValidatePassword";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// txtbItemName
			// 
			this.txtbItemName.Location = new System.Drawing.Point(448, 74);
			this.txtbItemName.Name = "txtbItemName";
			this.txtbItemName.Size = new System.Drawing.Size(210, 20);
			this.txtbItemName.TabIndex = 13;
			this.txtbItemName.Text = "Sesion";
			// 
			// txtbPassword
			// 
			this.txtbPassword.Location = new System.Drawing.Point(230, 74);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '?';
			this.txtbPassword.Size = new System.Drawing.Size(212, 20);
			this.txtbPassword.TabIndex = 12;
			this.txtbPassword.Text = "17011981";
			// 
			// txtbUserName
			// 
			this.txtbUserName.Location = new System.Drawing.Point(12, 74);
			this.txtbUserName.Name = "txtbUserName";
			this.txtbUserName.Size = new System.Drawing.Size(212, 20);
			this.txtbUserName.TabIndex = 11;
			this.txtbUserName.Text = "admin";
			// 
			// txtbOutput
			// 
			this.txtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbOutput.Location = new System.Drawing.Point(12, 100);
			this.txtbOutput.Multiline = true;
			this.txtbOutput.Name = "txtbOutput";
			this.txtbOutput.Size = new System.Drawing.Size(659, 300);
			this.txtbOutput.TabIndex = 3;
			// 
			// chkbTestSqlClrAssembly
			// 
			this.chkbTestSqlClrAssembly.AutoSize = true;
			this.chkbTestSqlClrAssembly.Checked = true;
			this.chkbTestSqlClrAssembly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkbTestSqlClrAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkbTestSqlClrAssembly.Location = new System.Drawing.Point(12, 8);
			this.chkbTestSqlClrAssembly.Name = "chkbTestSqlClrAssembly";
			this.chkbTestSqlClrAssembly.Size = new System.Drawing.Size(319, 19);
			this.chkbTestSqlClrAssembly.TabIndex = 14;
			this.chkbTestSqlClrAssembly.Text = "Test SQL CLR Assembly installed in Database";
			this.chkbTestSqlClrAssembly.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button4.Location = new System.Drawing.Point(12, 33);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(107, 35);
			this.button4.TabIndex = 15;
			this.button4.Text = "AzMan_Test";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// TestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(683, 412);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.chkbTestSqlClrAssembly);
			this.Controls.Add(this.txtbItemName);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.txtbUserName);
			this.Controls.Add(this.txtbOutput);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.MinimumSize = new System.Drawing.Size(679, 372);
			this.Name = "TestUI";
			this.Text = "Test Basgosoft.NetSqlAzManCacheWSSqlClrClient";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox txtbItemName;
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.TextBox txtbUserName;
		private System.Windows.Forms.TextBox txtbOutput;
		private System.Windows.Forms.CheckBox chkbTestSqlClrAssembly;
		private System.Windows.Forms.Button button4;
	}
}

