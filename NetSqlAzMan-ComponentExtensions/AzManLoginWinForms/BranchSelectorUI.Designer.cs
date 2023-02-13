namespace AzManLoginWinForms
{
	partial class BranchSelectorUI
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
			this.listBranchOffice = new System.Windows.Forms.ListBox();
			this.butnOk = new System.Windows.Forms.Button();
			this.picbLogo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// listBranchOffice
			// 
			this.listBranchOffice.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBranchOffice.FormattingEnabled = true;
			this.listBranchOffice.ItemHeight = 18;
			this.listBranchOffice.Location = new System.Drawing.Point(7, 106);
			this.listBranchOffice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.listBranchOffice.Name = "listBranchOffice";
			this.listBranchOffice.Size = new System.Drawing.Size(276, 184);
			this.listBranchOffice.TabIndex = 0;
			this.listBranchOffice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBranchOffice_MouseDoubleClick);
			// 
			// butnOk
			// 
			this.butnOk.Location = new System.Drawing.Point(65, 303);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(159, 34);
			this.butnOk.TabIndex = 1;
			this.butnOk.Text = "Seleccionar";
			this.butnOk.UseVisualStyleBackColor = true;
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// picbLogo
			// 
			this.picbLogo.BackgroundImage = global::AzManLoginWinForms.Properties.Resources.SISE_FONDOPLOMO;
			this.picbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.picbLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.picbLogo.Location = new System.Drawing.Point(0, 0);
			this.picbLogo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.picbLogo.Name = "picbLogo";
			this.picbLogo.Size = new System.Drawing.Size(291, 95);
			this.picbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picbLogo.TabIndex = 6;
			this.picbLogo.TabStop = false;
			// 
			// BranchSelectorUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(291, 347);
			this.Controls.Add(this.listBranchOffice);
			this.Controls.Add(this.picbLogo);
			this.Controls.Add(this.butnOk);
			this.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BranchSelectorUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Seleccionar Sede";
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBranchOffice;
		private System.Windows.Forms.Button butnOk;
		private System.Windows.Forms.PictureBox picbLogo;
	}
}