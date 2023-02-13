namespace NetSqlAzManSnapIn.AddOn.Membership.UI
{
	partial class UserUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtbUserName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbFirstName = new System.Windows.Forms.TextBox();
			this.butnOk = new System.Windows.Forms.Button();
			this.butnCancel = new System.Windows.Forms.Button();
			this.txtbPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtbConfirmation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkbEnabled = new System.Windows.Forms.CheckBox();
			this.txtbLastName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbbDepartment = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtbMail = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.lstbBranchOffices = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Usuario:";
			// 
			// txtbUserName
			// 
			this.txtbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbUserName.Location = new System.Drawing.Point(89, 18);
			this.txtbUserName.Name = "txtbUserName";
			this.txtbUserName.Size = new System.Drawing.Size(228, 20);
			this.txtbUserName.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Nombre:";
			// 
			// txtbFirstName
			// 
			this.txtbFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbFirstName.Location = new System.Drawing.Point(89, 48);
			this.txtbFirstName.Name = "txtbFirstName";
			this.txtbFirstName.Size = new System.Drawing.Size(228, 20);
			this.txtbFirstName.TabIndex = 1;
			// 
			// butnOk
			// 
			this.butnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butnOk.Location = new System.Drawing.Point(161, 496);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(75, 23);
			this.butnOk.TabIndex = 8;
			this.butnOk.Text = "&Aceptar";
			this.butnOk.UseVisualStyleBackColor = true;
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// butnCancel
			// 
			this.butnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butnCancel.Location = new System.Drawing.Point(242, 496);
			this.butnCancel.Name = "butnCancel";
			this.butnCancel.Size = new System.Drawing.Size(75, 23);
			this.butnCancel.TabIndex = 9;
			this.butnCancel.Text = "&Cerrar";
			this.butnCancel.UseVisualStyleBackColor = true;
			this.butnCancel.Click += new System.EventHandler(this.butnCancel_Click);
			// 
			// txtbPassword
			// 
			this.txtbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbPassword.Location = new System.Drawing.Point(89, 396);
			this.txtbPassword.Name = "txtbPassword";
			this.txtbPassword.PasswordChar = '*';
			this.txtbPassword.Size = new System.Drawing.Size(228, 20);
			this.txtbPassword.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 403);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Password:";
			// 
			// txtbConfirmation
			// 
			this.txtbConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbConfirmation.Location = new System.Drawing.Point(89, 422);
			this.txtbConfirmation.Name = "txtbConfirmation";
			this.txtbConfirmation.PasswordChar = '*';
			this.txtbConfirmation.Size = new System.Drawing.Size(228, 20);
			this.txtbConfirmation.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 429);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Confirmación:";
			// 
			// chkbEnabled
			// 
			this.chkbEnabled.AutoSize = true;
			this.chkbEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkbEnabled.Location = new System.Drawing.Point(12, 460);
			this.chkbEnabled.Name = "chkbEnabled";
			this.chkbEnabled.Size = new System.Drawing.Size(91, 17);
			this.chkbEnabled.TabIndex = 7;
			this.chkbEnabled.Text = "Habilitado:     ";
			this.chkbEnabled.UseVisualStyleBackColor = true;
			// 
			// txtbLastName
			// 
			this.txtbLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbLastName.Location = new System.Drawing.Point(89, 78);
			this.txtbLastName.Name = "txtbLastName";
			this.txtbLastName.Size = new System.Drawing.Size(228, 20);
			this.txtbLastName.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 85);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Apellido:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 151);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(54, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Surcursal:";
			// 
			// cmbbDepartment
			// 
			this.cmbbDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbbDepartment.FormattingEnabled = true;
			this.cmbbDepartment.Location = new System.Drawing.Point(89, 350);
			this.cmbbDepartment.Name = "cmbbDepartment";
			this.cmbbDepartment.Size = new System.Drawing.Size(228, 21);
			this.cmbbDepartment.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 358);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Departamento:";
			// 
			// txtbMail
			// 
			this.txtbMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbMail.Location = new System.Drawing.Point(89, 108);
			this.txtbMail.Name = "txtbMail";
			this.txtbMail.Size = new System.Drawing.Size(228, 20);
			this.txtbMail.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 115);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 15;
			this.label8.Text = "Correo:";
			// 
			// lstbBranchOffices
			// 
			this.lstbBranchOffices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.lstbBranchOffices.Location = new System.Drawing.Point(89, 151);
			this.lstbBranchOffices.Name = "lstbBranchOffices";
			this.lstbBranchOffices.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstbBranchOffices.Size = new System.Drawing.Size(227, 191);
			this.lstbBranchOffices.TabIndex = 16;
			// 
			// UserUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.butnCancel;
			this.ClientSize = new System.Drawing.Size(329, 531);
			this.Controls.Add(this.lstbBranchOffices);
			this.Controls.Add(this.txtbMail);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cmbbDepartment);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtbLastName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.chkbEnabled);
			this.Controls.Add(this.txtbConfirmation);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtbPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butnCancel);
			this.Controls.Add(this.butnOk);
			this.Controls.Add(this.txtbFirstName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtbUserName);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Usuario";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtbUserName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbFirstName;
		private System.Windows.Forms.Button butnOk;
		private System.Windows.Forms.Button butnCancel;		
		private System.Windows.Forms.TextBox txtbPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtbConfirmation;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkbEnabled;
		private System.Windows.Forms.TextBox txtbLastName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbbDepartment;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtbMail;
		private System.Windows.Forms.Label label8;
		private ComponentFactory.Krypton.Toolkit.KryptonListBox lstbBranchOffices;
	}
}