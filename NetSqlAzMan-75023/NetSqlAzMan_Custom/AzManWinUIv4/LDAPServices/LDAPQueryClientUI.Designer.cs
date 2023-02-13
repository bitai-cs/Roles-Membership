namespace AzManWinUI.LDAPServices
{
	partial class LDAPQueryClientUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LDAPQueryClientUI));
			this.combLDAP = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lvieSelected = new System.Windows.Forms.ListView();
			this.chdrDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chdrUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chdrDomainProfile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chdrUpn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chdrSid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.txtbUsuario = new System.Windows.Forms.TextBox();
			this.butnSearch = new System.Windows.Forms.Button();
			this.butnSelect = new System.Windows.Forms.Button();
			this.lvieConfirm = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.butnConfirm = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.pictWait = new System.Windows.Forms.PictureBox();
			this.timrWait = new System.Windows.Forms.Timer(this.components);
			this.lablWait = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictWait)).BeginInit();
			this.SuspendLayout();
			// 
			// combLDAP
			// 
			this.combLDAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combLDAP.FormattingEnabled = true;
			this.combLDAP.Items.AddRange(new object[] {
            "universiUNIVERSIDAD"});
			this.combLDAP.Location = new System.Drawing.Point(87, 7);
			this.combLDAP.Margin = new System.Windows.Forms.Padding(2);
			this.combLDAP.Name = "combLDAP";
			this.combLDAP.Size = new System.Drawing.Size(127, 21);
			this.combLDAP.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 13);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Perfil Dominio:";
			// 
			// lvieSelected
			// 
			this.lvieSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvieSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrDisplayName,
            this.chdrUserName,
            this.chdrDomainProfile,
            this.chdrUpn,
            this.chdrSid});
			this.lvieSelected.FullRowSelect = true;
			this.lvieSelected.GridLines = true;
			this.lvieSelected.Location = new System.Drawing.Point(8, 32);
			this.lvieSelected.Margin = new System.Windows.Forms.Padding(2);
			this.lvieSelected.Name = "lvieSelected";
			this.lvieSelected.Size = new System.Drawing.Size(720, 178);
			this.lvieSelected.TabIndex = 2;
			this.lvieSelected.UseCompatibleStateImageBehavior = false;
			this.lvieSelected.View = System.Windows.Forms.View.Details;
			// 
			// chdrDisplayName
			// 
			this.chdrDisplayName.Text = "Usuario";
			this.chdrDisplayName.Width = 144;
			// 
			// chdrUserName
			// 
			this.chdrUserName.Text = "Nombre";
			this.chdrUserName.Width = 200;
			// 
			// chdrDomainProfile
			// 
			this.chdrDomainProfile.Text = "Perfil Dominio";
			this.chdrDomainProfile.Width = 148;
			// 
			// chdrUpn
			// 
			this.chdrUpn.Text = "UPN";
			this.chdrUpn.Width = 120;
			// 
			// chdrSid
			// 
			this.chdrSid.Text = "Sid";
			this.chdrSid.Width = 300;
			// 
			// txtbUsuario
			// 
			this.txtbUsuario.Location = new System.Drawing.Point(329, 8);
			this.txtbUsuario.Margin = new System.Windows.Forms.Padding(2);
			this.txtbUsuario.Name = "txtbUsuario";
			this.txtbUsuario.Size = new System.Drawing.Size(112, 20);
			this.txtbUsuario.TabIndex = 3;
			// 
			// butnSearch
			// 
			this.butnSearch.Location = new System.Drawing.Point(445, 6);
			this.butnSearch.Margin = new System.Windows.Forms.Padding(2);
			this.butnSearch.Name = "butnSearch";
			this.butnSearch.Size = new System.Drawing.Size(129, 22);
			this.butnSearch.TabIndex = 4;
			this.butnSearch.Text = "&Buscar en el directorio";
			this.butnSearch.UseVisualStyleBackColor = true;
			this.butnSearch.Click += new System.EventHandler(this.butnSearch_Click);
			// 
			// butnSelect
			// 
			this.butnSelect.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.butnSelect.Location = new System.Drawing.Point(304, 211);
			this.butnSelect.Margin = new System.Windows.Forms.Padding(2);
			this.butnSelect.Name = "butnSelect";
			this.butnSelect.Size = new System.Drawing.Size(128, 24);
			this.butnSelect.TabIndex = 5;
			this.butnSelect.Text = "&Seleccionar usuarios";
			this.butnSelect.UseVisualStyleBackColor = true;
			this.butnSelect.Click += new System.EventHandler(this.butnSelect_Click);
			// 
			// lvieConfirm
			// 
			this.lvieConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvieConfirm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader3});
			this.lvieConfirm.FullRowSelect = true;
			this.lvieConfirm.GridLines = true;
			this.lvieConfirm.Location = new System.Drawing.Point(8, 240);
			this.lvieConfirm.Margin = new System.Windows.Forms.Padding(2);
			this.lvieConfirm.Name = "lvieConfirm";
			this.lvieConfirm.Size = new System.Drawing.Size(720, 171);
			this.lvieConfirm.TabIndex = 6;
			this.lvieConfirm.UseCompatibleStateImageBehavior = false;
			this.lvieConfirm.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Usuario";
			this.columnHeader1.Width = 152;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Nombre";
			this.columnHeader2.Width = 191;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Perfil Dominio";
			this.columnHeader4.Width = 158;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "UPN";
			this.columnHeader5.Width = 117;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Sid";
			this.columnHeader3.Width = 319;
			// 
			// butnConfirm
			// 
			this.butnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.butnConfirm.Location = new System.Drawing.Point(304, 422);
			this.butnConfirm.Margin = new System.Windows.Forms.Padding(2);
			this.butnConfirm.Name = "butnConfirm";
			this.butnConfirm.Size = new System.Drawing.Size(128, 24);
			this.butnConfirm.TabIndex = 7;
			this.butnConfirm.Text = "&Confirmar usuarios";
			this.butnConfirm.UseVisualStyleBackColor = true;
			this.butnConfirm.Click += new System.EventHandler(this.butnConfirm_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(233, 13);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Nombre o Cuenta:";
			// 
			// pictWait
			// 
			this.pictWait.Enabled = false;
			this.pictWait.Image = global::NetSqlAzMan.SnapIn.Properties.Resources.bigRoller;
			this.pictWait.Location = new System.Drawing.Point(577, 4);
			this.pictWait.Name = "pictWait";
			this.pictWait.Size = new System.Drawing.Size(24, 24);
			this.pictWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictWait.TabIndex = 9;
			this.pictWait.TabStop = false;
			this.pictWait.Visible = false;
			// 
			// timrWait
			// 
			this.timrWait.Interval = 1000;
			this.timrWait.Tick += new System.EventHandler(this.timrWait_Tick);
			// 
			// lablWait
			// 
			this.lablWait.AutoSize = true;
			this.lablWait.Location = new System.Drawing.Point(606, 13);
			this.lablWait.Name = "lablWait";
			this.lablWait.Size = new System.Drawing.Size(19, 13);
			this.lablWait.TabIndex = 10;
			this.lablWait.Text = "....";
			this.lablWait.Visible = false;
			// 
			// LDAPQueryClientUI
			// 
			this.AcceptButton = this.butnSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(736, 457);
			this.Controls.Add(this.lablWait);
			this.Controls.Add(this.pictWait);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butnConfirm);
			this.Controls.Add(this.lvieConfirm);
			this.Controls.Add(this.butnSelect);
			this.Controls.Add(this.butnSearch);
			this.Controls.Add(this.txtbUsuario);
			this.Controls.Add(this.lvieSelected);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.combLDAP);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(604, 495);
			this.Name = "LDAPQueryClientUI";
			this.Text = "Búsqueda en Proxies LDAP";
			((System.ComponentModel.ISupportInitialize)(this.pictWait)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox combLDAP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lvieSelected;
		private System.Windows.Forms.ColumnHeader chdrDisplayName;
		private System.Windows.Forms.ColumnHeader chdrUserName;
		private System.Windows.Forms.ColumnHeader chdrSid;
		private System.Windows.Forms.TextBox txtbUsuario;
		private System.Windows.Forms.Button butnSearch;
		private System.Windows.Forms.Button butnSelect;
		private System.Windows.Forms.ListView lvieConfirm;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button butnConfirm;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ColumnHeader chdrDomainProfile;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader chdrUpn;
		private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.PictureBox pictWait;
		private System.Windows.Forms.Timer timrWait;
		private System.Windows.Forms.Label lablWait;
	}
}