namespace AzManWinUI
{
	partial class SplashUI
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
			this.lablTitle = new System.Windows.Forms.Label();
			this.lablProduct = new System.Windows.Forms.Label();
			this.lablVersion = new System.Windows.Forms.Label();
			this.lablCopyright = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lablTitle
			// 
			this.lablTitle.AutoSize = true;
			this.lablTitle.BackColor = System.Drawing.Color.Transparent;
			this.lablTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lablTitle.Location = new System.Drawing.Point(19, 56);
			this.lablTitle.Name = "lablTitle";
			this.lablTitle.Size = new System.Drawing.Size(78, 25);
			this.lablTitle.TabIndex = 0;
			this.lablTitle.Text = "[Title]";
			// 
			// lablProduct
			// 
			this.lablProduct.AutoSize = true;
			this.lablProduct.BackColor = System.Drawing.Color.Transparent;
			this.lablProduct.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lablProduct.Location = new System.Drawing.Point(21, 88);
			this.lablProduct.Name = "lablProduct";
			this.lablProduct.Size = new System.Drawing.Size(71, 16);
			this.lablProduct.TabIndex = 1;
			this.lablProduct.Text = "[Product]";
			// 
			// lablVersion
			// 
			this.lablVersion.AutoSize = true;
			this.lablVersion.BackColor = System.Drawing.Color.Transparent;
			this.lablVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lablVersion.Location = new System.Drawing.Point(21, 111);
			this.lablVersion.Name = "lablVersion";
			this.lablVersion.Size = new System.Drawing.Size(59, 13);
			this.lablVersion.TabIndex = 2;
			this.lablVersion.Text = "[Version]";
			// 
			// lablCopyright
			// 
			this.lablCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lablCopyright.BackColor = System.Drawing.Color.Transparent;
			this.lablCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lablCopyright.Location = new System.Drawing.Point(12, 156);
			this.lablCopyright.Name = "lablCopyright";
			this.lablCopyright.Size = new System.Drawing.Size(294, 32);
			this.lablCopyright.TabIndex = 3;
			this.lablCopyright.Text = "[Copyright]";
			this.lablCopyright.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// SplashUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = global::NetSqlAzMan.SnapIn.Properties.Resources.NetSqlAzManWinUI_Splash;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(318, 197);
			this.Controls.Add(this.lablCopyright);
			this.Controls.Add(this.lablVersion);
			this.Controls.Add(this.lablProduct);
			this.Controls.Add(this.lablTitle);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashUI";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lablTitle;
		private System.Windows.Forms.Label lablProduct;
		private System.Windows.Forms.Label lablVersion;
		private System.Windows.Forms.Label lablCopyright;
	}
}