namespace AzManWinUI
{
	partial class ChangePwdUI
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
			this.butnCancel = new System.Windows.Forms.Button();
			this.butnOk = new System.Windows.Forms.Button();
			this.lablPassword = new System.Windows.Forms.Label();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtbNew = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbConfirmation = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butnCancel
			// 
			this.butnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butnCancel.Location = new System.Drawing.Point(185, 116);
			this.butnCancel.Name = "butnCancel";
			this.butnCancel.Size = new System.Drawing.Size(63, 24);
			this.butnCancel.TabIndex = 4;
			this.butnCancel.Text = "&Cancel";
			this.butnCancel.UseVisualStyleBackColor = true;
			// 
			// butnOk
			// 
			this.butnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butnOk.Location = new System.Drawing.Point(116, 116);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(63, 24);
			this.butnOk.TabIndex = 3;
			this.butnOk.Text = "&Ok";
			this.butnOk.UseVisualStyleBackColor = true;
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// lablPassword
			// 
			this.lablPassword.AutoSize = true;
			this.lablPassword.Location = new System.Drawing.Point(12, 19);
			this.lablPassword.Name = "lablPassword";
			this.lablPassword.Size = new System.Drawing.Size(56, 13);
			this.lablPassword.TabIndex = 7;
			this.lablPassword.Text = "Password:";
			// 
			// txtbPassword
			// 
			this.txtbPassword.Location = new System.Drawing.Point(74, 12);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '*';
			this.txtbPassword.Size = new System.Drawing.Size(172, 20);
			this.txtbPassword.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Nuevo:";
			// 
			// txtbNew
			// 
			this.txtbNew.Location = new System.Drawing.Point(74, 53);
			this.txtbNew.Name = "txtbNew";
			this.txtbNew.PasswordChar = '*';
			this.txtbNew.Size = new System.Drawing.Size(172, 20);
			this.txtbNew.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Confirmar:";
			// 
			// txtbConfirmation
			// 
			this.txtbConfirmation.Location = new System.Drawing.Point(74, 79);
			this.txtbConfirmation.Name = "txtbConfirmation";
			this.txtbConfirmation.PasswordChar = '*';
			this.txtbConfirmation.Size = new System.Drawing.Size(172, 20);
			this.txtbConfirmation.TabIndex = 2;
			// 
			// ChangePwdUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.butnCancel;
			this.ClientSize = new System.Drawing.Size(260, 152);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtbConfirmation);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtbNew);
			this.Controls.Add(this.lablPassword);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.butnCancel);
			this.Controls.Add(this.butnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChangePwdUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cambiar contraseña";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butnCancel;
		private System.Windows.Forms.Button butnOk;
		private System.Windows.Forms.Label lablPassword;
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtbNew;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbConfirmation;
	}
}