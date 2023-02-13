namespace NetSqlAzMan.SnapIn.Forms
{
	partial class LdapWacDomainProfilesUI
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgvwDomainProfiles = new System.Windows.Forms.DataGridView();
			this.ldapDomainProfileBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.butnNew = new System.Windows.Forms.Button();
			this.txtbDomainProfile = new System.Windows.Forms.TextBox();
			this.panlEdit = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.txtbDomainName = new System.Windows.Forms.TextBox();
			this.butnSave = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.chkbEnabled = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtbWebApiServerProfile = new System.Windows.Forms.TextBox();
			this.txtbWebApiUrl = new System.Windows.Forms.TextBox();
			this.lablDomainProfile = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.domainProfileIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.domainProfileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ldapServerProfileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DomainName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgvwDomainProfiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ldapDomainProfileBindingSource)).BeginInit();
			this.panlEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgvwDomainProfiles
			// 
			this.dgvwDomainProfiles.AllowUserToAddRows = false;
			this.dgvwDomainProfiles.AllowUserToDeleteRows = false;
			this.dgvwDomainProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvwDomainProfiles.AutoGenerateColumns = false;
			this.dgvwDomainProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvwDomainProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.domainProfileIdDataGridViewTextBoxColumn,
            this.domainProfileDataGridViewTextBoxColumn,
            this.ldapProxyWebApiUriDataGridViewTextBoxColumn,
            this.ldapServerProfileDataGridViewTextBoxColumn,
            this.DomainName,
            this.enabledDataGridViewCheckBoxColumn});
			this.dgvwDomainProfiles.DataSource = this.ldapDomainProfileBindingSource;
			this.dgvwDomainProfiles.Location = new System.Drawing.Point(12, 87);
			this.dgvwDomainProfiles.MultiSelect = false;
			this.dgvwDomainProfiles.Name = "dgvwDomainProfiles";
			this.dgvwDomainProfiles.ReadOnly = true;
			this.dgvwDomainProfiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvwDomainProfiles.Size = new System.Drawing.Size(746, 247);
			this.dgvwDomainProfiles.TabIndex = 6;
			this.dgvwDomainProfiles.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvwDomainProfiles_CellMouseClick);
			this.dgvwDomainProfiles.SelectionChanged += new System.EventHandler(this.dgvwDomainProfiles_SelectionChanged);
			this.dgvwDomainProfiles.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvwDomainProfiles_UserDeletingRow);
			// 
			// ldapDomainProfileBindingSource
			// 
			this.ldapDomainProfileBindingSource.DataSource = typeof(NetSqlAzMan.ServiceBusinessObjects.LdapDomainProfile);
			// 
			// butnNew
			// 
			this.butnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butnNew.Location = new System.Drawing.Point(0, 44);
			this.butnNew.Name = "butnNew";
			this.butnNew.Size = new System.Drawing.Size(68, 24);
			this.butnNew.TabIndex = 5;
			this.butnNew.Text = "&Nuevo";
			this.butnNew.UseVisualStyleBackColor = true;
			this.butnNew.Click += new System.EventHandler(this.butnNewDomainProfile_Click);
			// 
			// txtbDomainProfile
			// 
			this.txtbDomainProfile.Location = new System.Drawing.Point(3, 16);
			this.txtbDomainProfile.Name = "txtbDomainProfile";
			this.txtbDomainProfile.Size = new System.Drawing.Size(133, 20);
			this.txtbDomainProfile.TabIndex = 0;
			// 
			// panlEdit
			// 
			this.panlEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panlEdit.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panlEdit.Controls.Add(this.label4);
			this.panlEdit.Controls.Add(this.txtbDomainName);
			this.panlEdit.Controls.Add(this.butnSave);
			this.panlEdit.Controls.Add(this.label3);
			this.panlEdit.Controls.Add(this.chkbEnabled);
			this.panlEdit.Controls.Add(this.butnNew);
			this.panlEdit.Controls.Add(this.label2);
			this.panlEdit.Controls.Add(this.txtbWebApiServerProfile);
			this.panlEdit.Controls.Add(this.txtbWebApiUrl);
			this.panlEdit.Controls.Add(this.lablDomainProfile);
			this.panlEdit.Controls.Add(this.txtbDomainProfile);
			this.panlEdit.Controls.Add(this.label1);
			this.panlEdit.Location = new System.Drawing.Point(12, 12);
			this.panlEdit.Name = "panlEdit";
			this.panlEdit.Size = new System.Drawing.Size(746, 69);
			this.panlEdit.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(476, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Nombre Dominio";
			// 
			// txtbDomainName
			// 
			this.txtbDomainName.Location = new System.Drawing.Point(479, 16);
			this.txtbDomainName.Name = "txtbDomainName";
			this.txtbDomainName.Size = new System.Drawing.Size(133, 20);
			this.txtbDomainName.TabIndex = 11;
			// 
			// butnSave
			// 
			this.butnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butnSave.Location = new System.Drawing.Point(74, 44);
			this.butnSave.Name = "butnSave";
			this.butnSave.Size = new System.Drawing.Size(66, 24);
			this.butnSave.TabIndex = 4;
			this.butnSave.Text = "&Guardar";
			this.butnSave.UseVisualStyleBackColor = true;
			this.butnSave.Click += new System.EventHandler(this.butnSave_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(620, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Habiitado";
			// 
			// chkbEnabled
			// 
			this.chkbEnabled.AutoSize = true;
			this.chkbEnabled.Location = new System.Drawing.Point(639, 20);
			this.chkbEnabled.Name = "chkbEnabled";
			this.chkbEnabled.Size = new System.Drawing.Size(15, 14);
			this.chkbEnabled.TabIndex = 3;
			this.chkbEnabled.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(337, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Perfil Proxy Ldap";
			// 
			// txtbWebApiServerProfile
			// 
			this.txtbWebApiServerProfile.Location = new System.Drawing.Point(340, 16);
			this.txtbWebApiServerProfile.Name = "txtbWebApiServerProfile";
			this.txtbWebApiServerProfile.Size = new System.Drawing.Size(133, 20);
			this.txtbWebApiServerProfile.TabIndex = 2;
			// 
			// txtbWebApiUrl
			// 
			this.txtbWebApiUrl.Location = new System.Drawing.Point(142, 16);
			this.txtbWebApiUrl.Name = "txtbWebApiUrl";
			this.txtbWebApiUrl.Size = new System.Drawing.Size(192, 20);
			this.txtbWebApiUrl.TabIndex = 1;
			// 
			// lablDomainProfile
			// 
			this.lablDomainProfile.AutoSize = true;
			this.lablDomainProfile.Location = new System.Drawing.Point(0, 0);
			this.lablDomainProfile.Name = "lablDomainProfile";
			this.lablDomainProfile.Size = new System.Drawing.Size(74, 13);
			this.lablDomainProfile.TabIndex = 4;
			this.lablDomainProfile.Text = "Perfil Dominio:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(139, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "URL Proxy Ldap";
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Borrar";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Text = "X";
			this.Column1.ToolTipText = "Eliminar registro";
			this.Column1.Width = 45;
			// 
			// domainProfileIdDataGridViewTextBoxColumn
			// 
			this.domainProfileIdDataGridViewTextBoxColumn.DataPropertyName = "DomainProfileId";
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.domainProfileIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.domainProfileIdDataGridViewTextBoxColumn.HeaderText = "Id";
			this.domainProfileIdDataGridViewTextBoxColumn.Name = "domainProfileIdDataGridViewTextBoxColumn";
			this.domainProfileIdDataGridViewTextBoxColumn.ReadOnly = true;
			this.domainProfileIdDataGridViewTextBoxColumn.Width = 30;
			// 
			// domainProfileDataGridViewTextBoxColumn
			// 
			this.domainProfileDataGridViewTextBoxColumn.DataPropertyName = "DomainProfile";
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.domainProfileDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.domainProfileDataGridViewTextBoxColumn.HeaderText = "Perfil Dominio";
			this.domainProfileDataGridViewTextBoxColumn.Name = "domainProfileDataGridViewTextBoxColumn";
			this.domainProfileDataGridViewTextBoxColumn.ReadOnly = true;
			this.domainProfileDataGridViewTextBoxColumn.Width = 150;
			// 
			// ldapProxyWebApiUriDataGridViewTextBoxColumn
			// 
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.DataPropertyName = "LdapProxyWebApiUri";
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.HeaderText = "URL Proxy Ldap";
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.Name = "ldapProxyWebApiUriDataGridViewTextBoxColumn";
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.ReadOnly = true;
			this.ldapProxyWebApiUriDataGridViewTextBoxColumn.Width = 200;
			// 
			// ldapServerProfileDataGridViewTextBoxColumn
			// 
			this.ldapServerProfileDataGridViewTextBoxColumn.DataPropertyName = "LdapServerProfile";
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.ldapServerProfileDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.ldapServerProfileDataGridViewTextBoxColumn.HeaderText = "Perfil Proxy Ldap";
			this.ldapServerProfileDataGridViewTextBoxColumn.Name = "ldapServerProfileDataGridViewTextBoxColumn";
			this.ldapServerProfileDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// DomainName
			// 
			this.DomainName.DataPropertyName = "DomainName";
			this.DomainName.HeaderText = "Nombre Dominio ";
			this.DomainName.Name = "DomainName";
			this.DomainName.ReadOnly = true;
			// 
			// enabledDataGridViewCheckBoxColumn
			// 
			this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			dataGridViewCellStyle5.NullValue = false;
			this.enabledDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.enabledDataGridViewCheckBoxColumn.HeaderText = "Habilitado";
			this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
			this.enabledDataGridViewCheckBoxColumn.ReadOnly = true;
			this.enabledDataGridViewCheckBoxColumn.Width = 70;
			// 
			// LdapWacDomainProfilesUI
			// 
			this.AcceptButton = this.butnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightSteelBlue;
			this.ClientSize = new System.Drawing.Size(770, 346);
			this.Controls.Add(this.panlEdit);
			this.Controls.Add(this.dgvwDomainProfiles);
			this.Name = "LdapWacDomainProfilesUI";
			this.Text = "Perfiles de Dominio Proxy Ldap";
			this.Load += new System.EventHandler(this.LdapWacDomainProfilesUI_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvwDomainProfiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ldapDomainProfileBindingSource)).EndInit();
			this.panlEdit.ResumeLayout(false);
			this.panlEdit.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvwDomainProfiles;
		private System.Windows.Forms.BindingSource ldapDomainProfileBindingSource;
		private System.Windows.Forms.Button butnNew;
		private System.Windows.Forms.TextBox txtbDomainProfile;
		private System.Windows.Forms.Panel panlEdit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkbEnabled;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtbWebApiServerProfile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtbWebApiUrl;
		private System.Windows.Forms.Label lablDomainProfile;
		private System.Windows.Forms.Button butnSave;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtbDomainName;
		private System.Windows.Forms.DataGridViewButtonColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn domainProfileIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn domainProfileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ldapProxyWebApiUriDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ldapServerProfileDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DomainName;
		private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
	}
}