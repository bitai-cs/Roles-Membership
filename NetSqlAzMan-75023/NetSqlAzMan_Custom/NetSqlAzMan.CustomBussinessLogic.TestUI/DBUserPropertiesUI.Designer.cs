namespace NetSqlAzMan.CustomBussinessLogic.TestUI {
	partial class DBUserPropertiesUI {
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbNew = new System.Windows.Forms.TextBox();
			this.txtbConfirmation = new System.Windows.Forms.TextBox();
			this.butnChange = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.Location = new System.Drawing.Point(1, 2);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(367, 397);
			this.propertyGrid1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 414);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Nueva contraseña:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 440);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Confirmar contraseña:";
			// 
			// txtbNew
			// 
			this.txtbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtbNew.Location = new System.Drawing.Point(128, 407);
			this.txtbNew.Name = "txtbNew";
			this.txtbNew.Size = new System.Drawing.Size(100, 20);
			this.txtbNew.TabIndex = 3;
			// 
			// txtbConfirmation
			// 
			this.txtbConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtbConfirmation.Location = new System.Drawing.Point(128, 433);
			this.txtbConfirmation.Name = "txtbConfirmation";
			this.txtbConfirmation.Size = new System.Drawing.Size(100, 20);
			this.txtbConfirmation.TabIndex = 4;
			// 
			// butnChange
			// 
			this.butnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butnChange.Location = new System.Drawing.Point(234, 407);
			this.butnChange.Name = "butnChange";
			this.butnChange.Size = new System.Drawing.Size(75, 46);
			this.butnChange.TabIndex = 5;
			this.butnChange.Text = "Cambiar contraseña";
			this.butnChange.UseVisualStyleBackColor = true;
			this.butnChange.Click += new System.EventHandler(this.butnChange_Click);
			// 
			// DBUserPropertiesUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 465);
			this.Controls.Add(this.butnChange);
			this.Controls.Add(this.txtbConfirmation);
			this.Controls.Add(this.txtbNew);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "DBUserPropertiesUI";
			this.Text = "DBUserPropertiesUI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbNew;
		private System.Windows.Forms.TextBox txtbConfirmation;
		private System.Windows.Forms.Button butnChange;
	}
}