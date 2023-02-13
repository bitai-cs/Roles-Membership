namespace AzManWebServicesTest
{
	partial class PwdSvcTestUI
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
			this.lablPrompt = new System.Windows.Forms.Label();
			this.txtbCurrent = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtbNew = new System.Windows.Forms.TextBox();
			this.txtbConfirmation = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txtbStatus = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lablPrompt
			// 
			this.lablPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lablPrompt.Location = new System.Drawing.Point(8, 5);
			this.lablPrompt.Name = "lablPrompt";
			this.lablPrompt.Size = new System.Drawing.Size(236, 44);
			this.lablPrompt.TabIndex = 0;
			this.lablPrompt.Text = "Usuario: {0}";
			// 
			// txtbCurrent
			// 
			this.txtbCurrent.Location = new System.Drawing.Point(123, 61);
			this.txtbCurrent.Name = "txtbCurrent";
			this.txtbCurrent.Size = new System.Drawing.Size(121, 20);
			this.txtbCurrent.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 40);
			this.label1.TabIndex = 2;
			this.label1.Text = "Contraseña actual:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 30);
			this.label2.TabIndex = 3;
			this.label2.Text = "Nueva Contraseña:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 30);
			this.label3.TabIndex = 4;
			this.label3.Text = "Confirmación de nueva Contraseña:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtbNew
			// 
			this.txtbNew.Location = new System.Drawing.Point(123, 92);
			this.txtbNew.Name = "txtbNew";
			this.txtbNew.Size = new System.Drawing.Size(121, 20);
			this.txtbNew.TabIndex = 1;
			// 
			// txtbConfirmation
			// 
			this.txtbConfirmation.Location = new System.Drawing.Point(123, 128);
			this.txtbConfirmation.Name = "txtbConfirmation";
			this.txtbConfirmation.Size = new System.Drawing.Size(121, 20);
			this.txtbConfirmation.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(12, 171);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(232, 33);
			this.button1.TabIndex = 3;
			this.button1.Text = "AzMan_ChangePassword";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtbStatus
			// 
			this.txtbStatus.BackColor = System.Drawing.Color.Black;
			this.txtbStatus.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtbStatus.ForeColor = System.Drawing.Color.Lime;
			this.txtbStatus.Location = new System.Drawing.Point(260, 5);
			this.txtbStatus.Multiline = true;
			this.txtbStatus.Name = "txtbStatus";
			this.txtbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtbStatus.Size = new System.Drawing.Size(696, 207);
			this.txtbStatus.TabIndex = 5;
			this.txtbStatus.TabStop = false;
			this.txtbStatus.WordWrap = false;
			// 
			// PwdSvcTestUI
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(962, 216);
			this.Controls.Add(this.txtbStatus);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtbConfirmation);
			this.Controls.Add(this.txtbNew);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtbCurrent);
			this.Controls.Add(this.lablPrompt);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PwdSvcTestUI";
			this.Text = "PwdSvcTestUI";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lablPrompt;
		private System.Windows.Forms.TextBox txtbCurrent;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtbNew;
		private System.Windows.Forms.TextBox txtbConfirmation;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtbStatus;
	}
}