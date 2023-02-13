namespace AzManWinUI
{
	partial class SettingsUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsUI));
			this.tabcSettings = new System.Windows.Forms.TabControl();
			this.tabpGeneral = new System.Windows.Forms.TabPage();
			this.combListViewType = new System.Windows.Forms.ComboBox();
			this.lablListViewType = new System.Windows.Forms.Label();
			this.combTreeNodeSize = new System.Windows.Forms.ComboBox();
			this.lablTreeNodeSize = new System.Windows.Forms.Label();
			this.tabpLanguage = new System.Windows.Forms.TabPage();
			this.radbEspañol = new System.Windows.Forms.RadioButton();
			this.radbEnglish = new System.Windows.Forms.RadioButton();
			this.tabpView = new System.Windows.Forms.TabPage();
			this.combAuthorizationsView = new System.Windows.Forms.ComboBox();
			this.lablViewAuthorization = new System.Windows.Forms.Label();
			this.lablViewStructure = new System.Windows.Forms.Label();
			this.combStructureView = new System.Windows.Forms.ComboBox();
			this.butnApply = new System.Windows.Forms.Button();
			this.butnCancel = new System.Windows.Forms.Button();
			this.butnOk = new System.Windows.Forms.Button();
			this.tabcSettings.SuspendLayout();
			this.tabpGeneral.SuspendLayout();
			this.tabpLanguage.SuspendLayout();
			this.tabpView.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabcSettings
			// 
			this.tabcSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabcSettings.Controls.Add(this.tabpGeneral);
			this.tabcSettings.Controls.Add(this.tabpLanguage);
			this.tabcSettings.Controls.Add(this.tabpView);
			this.tabcSettings.Location = new System.Drawing.Point(12, 12);
			this.tabcSettings.Name = "tabcSettings";
			this.tabcSettings.SelectedIndex = 0;
			this.tabcSettings.Size = new System.Drawing.Size(276, 131);
			this.tabcSettings.TabIndex = 0;
			// 
			// tabpGeneral
			// 
			this.tabpGeneral.Controls.Add(this.combListViewType);
			this.tabpGeneral.Controls.Add(this.lablListViewType);
			this.tabpGeneral.Controls.Add(this.combTreeNodeSize);
			this.tabpGeneral.Controls.Add(this.lablTreeNodeSize);
			this.tabpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabpGeneral.Name = "tabpGeneral";
			this.tabpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabpGeneral.Size = new System.Drawing.Size(268, 105);
			this.tabpGeneral.TabIndex = 0;
			this.tabpGeneral.Text = "General";
			this.tabpGeneral.UseVisualStyleBackColor = true;
			// 
			// combListViewType
			// 
			this.combListViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.combListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combListViewType.FormattingEnabled = true;
			this.combListViewType.Location = new System.Drawing.Point(142, 58);
			this.combListViewType.Name = "combListViewType";
			this.combListViewType.Size = new System.Drawing.Size(108, 21);
			this.combListViewType.TabIndex = 3;
			// 
			// lablListViewType
			// 
			this.lablListViewType.AutoSize = true;
			this.lablListViewType.Location = new System.Drawing.Point(15, 66);
			this.lablListViewType.Name = "lablListViewType";
			this.lablListViewType.Size = new System.Drawing.Size(121, 13);
			this.lablListViewType.TabIndex = 2;
			this.lablListViewType.Text = "Tipo de vista del listado:\r\n";
			// 
			// combTreeNodeSize
			// 
			this.combTreeNodeSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.combTreeNodeSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combTreeNodeSize.FormattingEnabled = true;
			this.combTreeNodeSize.Location = new System.Drawing.Point(142, 23);
			this.combTreeNodeSize.Name = "combTreeNodeSize";
			this.combTreeNodeSize.Size = new System.Drawing.Size(108, 21);
			this.combTreeNodeSize.TabIndex = 1;
			// 
			// lablTreeNodeSize
			// 
			this.lablTreeNodeSize.AutoSize = true;
			this.lablTreeNodeSize.Location = new System.Drawing.Point(14, 31);
			this.lablTreeNodeSize.Name = "lablTreeNodeSize";
			this.lablTreeNodeSize.Size = new System.Drawing.Size(103, 13);
			this.lablTreeNodeSize.TabIndex = 0;
			this.lablTreeNodeSize.Text = "Elementos del Arbol:";
			// 
			// tabpLanguage
			// 
			this.tabpLanguage.Controls.Add(this.radbEspañol);
			this.tabpLanguage.Controls.Add(this.radbEnglish);
			this.tabpLanguage.Location = new System.Drawing.Point(4, 22);
			this.tabpLanguage.Name = "tabpLanguage";
			this.tabpLanguage.Padding = new System.Windows.Forms.Padding(3);
			this.tabpLanguage.Size = new System.Drawing.Size(268, 105);
			this.tabpLanguage.TabIndex = 1;
			this.tabpLanguage.Text = "Idioma";
			this.tabpLanguage.UseVisualStyleBackColor = true;
			// 
			// radbEspañol
			// 
			this.radbEspañol.AutoSize = true;
			this.radbEspañol.Location = new System.Drawing.Point(22, 26);
			this.radbEspañol.Name = "radbEspañol";
			this.radbEspañol.Size = new System.Drawing.Size(63, 17);
			this.radbEspañol.TabIndex = 1;
			this.radbEspañol.TabStop = true;
			this.radbEspañol.Text = "Español";
			this.radbEspañol.UseVisualStyleBackColor = true;
			// 
			// radbEnglish
			// 
			this.radbEnglish.AutoSize = true;
			this.radbEnglish.Location = new System.Drawing.Point(22, 60);
			this.radbEnglish.Name = "radbEnglish";
			this.radbEnglish.Size = new System.Drawing.Size(59, 17);
			this.radbEnglish.TabIndex = 0;
			this.radbEnglish.TabStop = true;
			this.radbEnglish.Text = "English";
			this.radbEnglish.UseVisualStyleBackColor = true;
			// 
			// tabpView
			// 
			this.tabpView.Controls.Add(this.combAuthorizationsView);
			this.tabpView.Controls.Add(this.lablViewAuthorization);
			this.tabpView.Controls.Add(this.lablViewStructure);
			this.tabpView.Controls.Add(this.combStructureView);
			this.tabpView.Location = new System.Drawing.Point(4, 22);
			this.tabpView.Name = "tabpView";
			this.tabpView.Padding = new System.Windows.Forms.Padding(3);
			this.tabpView.Size = new System.Drawing.Size(268, 105);
			this.tabpView.TabIndex = 2;
			this.tabpView.Text = "Vista";
			this.tabpView.UseVisualStyleBackColor = true;
			// 
			// combAuthorizationsView
			// 
			this.combAuthorizationsView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combAuthorizationsView.FormattingEnabled = true;
			this.combAuthorizationsView.Location = new System.Drawing.Point(91, 61);
			this.combAuthorizationsView.Name = "combAuthorizationsView";
			this.combAuthorizationsView.Size = new System.Drawing.Size(53, 21);
			this.combAuthorizationsView.TabIndex = 3;
			// 
			// lablViewAuthorization
			// 
			this.lablViewAuthorization.AutoSize = true;
			this.lablViewAuthorization.Location = new System.Drawing.Point(17, 69);
			this.lablViewAuthorization.Name = "lablViewAuthorization";
			this.lablViewAuthorization.Size = new System.Drawing.Size(68, 13);
			this.lablViewAuthorization.TabIndex = 2;
			this.lablViewAuthorization.Text = "Autorización:";
			// 
			// lablViewStructure
			// 
			this.lablViewStructure.AutoSize = true;
			this.lablViewStructure.Location = new System.Drawing.Point(17, 26);
			this.lablViewStructure.Name = "lablViewStructure";
			this.lablViewStructure.Size = new System.Drawing.Size(58, 13);
			this.lablViewStructure.TabIndex = 1;
			this.lablViewStructure.Text = "Estructura:";
			// 
			// combStructureView
			// 
			this.combStructureView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combStructureView.FormattingEnabled = true;
			this.combStructureView.Location = new System.Drawing.Point(91, 18);
			this.combStructureView.Name = "combStructureView";
			this.combStructureView.Size = new System.Drawing.Size(53, 21);
			this.combStructureView.TabIndex = 0;
			// 
			// butnApply
			// 
			this.butnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnApply.Location = new System.Drawing.Point(213, 149);
			this.butnApply.Name = "butnApply";
			this.butnApply.Size = new System.Drawing.Size(75, 25);
			this.butnApply.TabIndex = 1;
			this.butnApply.Text = "&Aplicar";
			this.butnApply.UseVisualStyleBackColor = true;
			this.butnApply.Click += new System.EventHandler(this.butnApply_Click);
			// 
			// butnCancel
			// 
			this.butnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butnCancel.Location = new System.Drawing.Point(132, 149);
			this.butnCancel.Name = "butnCancel";
			this.butnCancel.Size = new System.Drawing.Size(75, 25);
			this.butnCancel.TabIndex = 2;
			this.butnCancel.Text = "&Cancelsr";
			this.butnCancel.UseVisualStyleBackColor = true;
			// 
			// butnOk
			// 
			this.butnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butnOk.Location = new System.Drawing.Point(51, 149);
			this.butnOk.Name = "butnOk";
			this.butnOk.Size = new System.Drawing.Size(75, 25);
			this.butnOk.TabIndex = 3;
			this.butnOk.Text = "&Aceptar";
			this.butnOk.UseVisualStyleBackColor = true;
			this.butnOk.Click += new System.EventHandler(this.butnOk_Click);
			// 
			// SettingsUI
			// 
			this.AcceptButton = this.butnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.butnCancel;
			this.ClientSize = new System.Drawing.Size(300, 184);
			this.Controls.Add(this.butnOk);
			this.Controls.Add(this.butnCancel);
			this.Controls.Add(this.butnApply);
			this.Controls.Add(this.tabcSettings);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(316, 222);
			this.Name = "SettingsUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preferencias";
			this.tabcSettings.ResumeLayout(false);
			this.tabpGeneral.ResumeLayout(false);
			this.tabpGeneral.PerformLayout();
			this.tabpLanguage.ResumeLayout(false);
			this.tabpLanguage.PerformLayout();
			this.tabpView.ResumeLayout(false);
			this.tabpView.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabcSettings;
		private System.Windows.Forms.TabPage tabpGeneral;
		private System.Windows.Forms.Button butnApply;
		private System.Windows.Forms.Button butnCancel;
		private System.Windows.Forms.Button butnOk;
		private System.Windows.Forms.ComboBox combTreeNodeSize;
		private System.Windows.Forms.Label lablTreeNodeSize;
		private System.Windows.Forms.ComboBox combListViewType;
		private System.Windows.Forms.Label lablListViewType;
		private System.Windows.Forms.TabPage tabpLanguage;
		private System.Windows.Forms.RadioButton radbEspañol;
		private System.Windows.Forms.RadioButton radbEnglish;
		private System.Windows.Forms.TabPage tabpView;
		private System.Windows.Forms.ComboBox combAuthorizationsView;
		private System.Windows.Forms.Label lablViewAuthorization;
		private System.Windows.Forms.Label lablViewStructure;
		private System.Windows.Forms.ComboBox combStructureView;		
	}
}