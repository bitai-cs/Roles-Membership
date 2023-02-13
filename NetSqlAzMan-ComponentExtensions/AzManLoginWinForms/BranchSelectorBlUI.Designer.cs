namespace AzManLoginWinForms
{
	partial class BranchSelectorBlUI
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
			this.listBranchOffice = new System.Windows.Forms.ListBox();
			this.butnOk = new System.Windows.Forms.Button();
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
			this.picbLogo.Size = new System.Drawing.Size(300, 97);
			this.picbLogo.TabIndex = 5;
			this.picbLogo.TabStop = false;
			// 
			// listBranchOffice
			// 
			this.listBranchOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBranchOffice.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBranchOffice.ItemHeight = 18;
			this.listBranchOffice.Location = new System.Drawing.Point(13, 109);
			this.listBranchOffice.Name = "listBranchOffice";
			this.listBranchOffice.Size = new System.Drawing.Size(273, 166);
			this.listBranchOffice.TabIndex = 6;
			this.listBranchOffice.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBranchOffice_MouseDoubleClick);
			// 
			// butnOk
			// 
			this.butnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.butnOk.Location = new System.Drawing.Point(67, 284);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(167, 36);
			this.butnOk.TabIndex = 7;
			this.butnOk.Text = "Seleccionar";
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// BranchSelectorBlUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.ClientSize = new System.Drawing.Size(300, 329);
			this.Controls.Add(this.butnOk);
			this.Controls.Add(this.listBranchOffice);
			this.Controls.Add(this.picbLogo);
			this.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MinimumSize = new System.Drawing.Size(300, 287);
			this.Name = "BranchSelectorBlUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BranchSelectorUI";
			this.Load += new System.EventHandler(this.BranchSelectorUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.picbLogo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picbLogo;
		private System.Windows.Forms.ListBox listBranchOffice;
		private System.Windows.Forms.Button butnOk;
	}
}