namespace NetSqlAzManSnapIn.AddOn.Test
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
			this.ShowMembershipUI = new System.Windows.Forms.LinkLabel();
			this.linkNewUser = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// ShowMembershipUI
			// 
			this.ShowMembershipUI.AutoSize = true;
			this.ShowMembershipUI.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ShowMembershipUI.Location = new System.Drawing.Point(12, 58);
			this.ShowMembershipUI.Name = "ShowMembershipUI";
			this.ShowMembershipUI.Size = new System.Drawing.Size(186, 16);
			this.ShowMembershipUI.TabIndex = 0;
			this.ShowMembershipUI.TabStop = true;
			this.ShowMembershipUI.Text = "Mostrar listado de usuarios";
			this.ShowMembershipUI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShowMembershipUI_LinkClicked);
			// 
			// linkNewUser
			// 
			this.linkNewUser.AutoSize = true;
			this.linkNewUser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkNewUser.Location = new System.Drawing.Point(12, 21);
			this.linkNewUser.Name = "linkNewUser";
			this.linkNewUser.Size = new System.Drawing.Size(100, 16);
			this.linkNewUser.TabIndex = 1;
			this.linkNewUser.TabStop = true;
			this.linkNewUser.Text = "Nuevo Usuario";
			this.linkNewUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNewUser_LinkClicked);
			// 
			// TestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(235, 99);
			this.Controls.Add(this.linkNewUser);
			this.Controls.Add(this.ShowMembershipUI);
			this.MaximizeBox = false;
			this.Name = "TestUI";
			this.Text = "Test";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel ShowMembershipUI;
		private System.Windows.Forms.LinkLabel linkNewUser;
	}
}

