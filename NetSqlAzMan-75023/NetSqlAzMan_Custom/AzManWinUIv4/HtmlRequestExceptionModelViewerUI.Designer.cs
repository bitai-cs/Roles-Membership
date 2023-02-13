namespace AzManWinUI {
	partial class HtmlRequestExceptionModelViewerUI {
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
			this.HTMLViewer = new System.Windows.Forms.WebBrowser();
			this.label1 = new System.Windows.Forms.Label();
			this.txtbMessage = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbURI = new System.Windows.Forms.TextBox();
			this.txtbStatusCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtbReasonPhrase = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// HTMLViewer
			// 
			this.HTMLViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HTMLViewer.Location = new System.Drawing.Point(5, 95);
			this.HTMLViewer.MinimumSize = new System.Drawing.Size(20, 20);
			this.HTMLViewer.Name = "HTMLViewer";
			this.HTMLViewer.Size = new System.Drawing.Size(763, 442);
			this.HTMLViewer.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Message:";
			// 
			// txtbMessage
			// 
			this.txtbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbMessage.BackColor = System.Drawing.SystemColors.Control;
			this.txtbMessage.Location = new System.Drawing.Point(86, 10);
			this.txtbMessage.Name = "txtbMessage";
			this.txtbMessage.ReadOnly = true;
			this.txtbMessage.Size = new System.Drawing.Size(682, 20);
			this.txtbMessage.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Request URI:";
			// 
			// txtbURI
			// 
			this.txtbURI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbURI.BackColor = System.Drawing.SystemColors.Control;
			this.txtbURI.Location = new System.Drawing.Point(86, 36);
			this.txtbURI.Name = "txtbURI";
			this.txtbURI.ReadOnly = true;
			this.txtbURI.Size = new System.Drawing.Size(682, 20);
			this.txtbURI.TabIndex = 5;
			// 
			// txtbStatusCode
			// 
			this.txtbStatusCode.BackColor = System.Drawing.SystemColors.Control;
			this.txtbStatusCode.Location = new System.Drawing.Point(86, 62);
			this.txtbStatusCode.Name = "txtbStatusCode";
			this.txtbStatusCode.ReadOnly = true;
			this.txtbStatusCode.Size = new System.Drawing.Size(54, 20);
			this.txtbStatusCode.TabIndex = 7;
			this.txtbStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Status code:";
			// 
			// txtbReasonPhrase
			// 
			this.txtbReasonPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtbReasonPhrase.BackColor = System.Drawing.SystemColors.Control;
			this.txtbReasonPhrase.Location = new System.Drawing.Point(237, 62);
			this.txtbReasonPhrase.Name = "txtbReasonPhrase";
			this.txtbReasonPhrase.ReadOnly = true;
			this.txtbReasonPhrase.Size = new System.Drawing.Size(531, 20);
			this.txtbReasonPhrase.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(151, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Reason Phrase";
			// 
			// HtmlRequestExceptionModelViewerUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(773, 541);
			this.Controls.Add(this.txtbReasonPhrase);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtbStatusCode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtbURI);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtbMessage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HTMLViewer);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(529, 438);
			this.Name = "HtmlRequestExceptionModelViewerUI";
			this.Text = "Request HTML Response";
			this.Load += new System.EventHandler(this.HtmlRequestExceptionModelViewerUI_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser HTMLViewer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtbMessage;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbURI;
		private System.Windows.Forms.TextBox txtbStatusCode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtbReasonPhrase;
		private System.Windows.Forms.Label label4;
	}
}