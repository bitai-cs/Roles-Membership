namespace AzManLoginWebServicesSqlClrClientTest
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
			this.butnAzManLogin_Test = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.chkbTestSqlClrAssembly = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butnAzManLogin_Test
			// 
			this.butnAzManLogin_Test.Location = new System.Drawing.Point(13, 43);
			this.butnAzManLogin_Test.Margin = new System.Windows.Forms.Padding(4);
			this.butnAzManLogin_Test.Name = "butnAzManLogin_Test";
			this.butnAzManLogin_Test.Size = new System.Drawing.Size(251, 47);
			this.butnAzManLogin_Test.TabIndex = 0;
			this.butnAzManLogin_Test.Text = "AzManLoginWs LoginSvc Test";
			this.butnAzManLogin_Test.UseVisualStyleBackColor = true;
			this.butnAzManLogin_Test.Click += new System.EventHandler(this.butnAzManLogin_Test_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 98);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(251, 47);
			this.button1.TabIndex = 16;
			this.button1.Text = "AzManLoginWs LoginSvc AzManLogin_CheckLoginAccess";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// chkbTestSqlClrAssembly
			// 
			this.chkbTestSqlClrAssembly.AutoSize = true;
			this.chkbTestSqlClrAssembly.Checked = true;
			this.chkbTestSqlClrAssembly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkbTestSqlClrAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkbTestSqlClrAssembly.Location = new System.Drawing.Point(13, 13);
			this.chkbTestSqlClrAssembly.Margin = new System.Windows.Forms.Padding(4);
			this.chkbTestSqlClrAssembly.Name = "chkbTestSqlClrAssembly";
			this.chkbTestSqlClrAssembly.Size = new System.Drawing.Size(377, 22);
			this.chkbTestSqlClrAssembly.TabIndex = 17;
			this.chkbTestSqlClrAssembly.Text = "Test SQL CLR Assembly installed in Database";
			this.chkbTestSqlClrAssembly.UseVisualStyleBackColor = true;
			// 
			// TestUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(416, 294);
			this.Controls.Add(this.chkbTestSqlClrAssembly);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.butnAzManLogin_Test);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "TestUI";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butnAzManLogin_Test;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox chkbTestSqlClrAssembly;
	}
}

