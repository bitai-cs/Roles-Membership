namespace NetSqlAzMan_WinTest
{
    partial class Form1
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnStoreManipulate = new System.Windows.Forms.Button();
			this.btnItemManipulate = new System.Windows.Forms.Button();
			this.btnADShowDialog = new System.Windows.Forms.Button();
			this.btnCheckAccess = new System.Windows.Forms.Button();
			this.btnEventHandling = new System.Windows.Forms.Button();
			this.btnImportFromAzMan = new System.Windows.Forms.Button();
			this.btnTestForm = new System.Windows.Forms.Button();
			this.btnTestImport = new System.Windows.Forms.Button();
			this.btnTestDomain = new System.Windows.Forms.Button();
			this.btnTestADShowDialog2 = new System.Windows.Forms.Button();
			this.btnCheckAccessTemplate = new System.Windows.Forms.Button();
			this.btnGenerateCheckAccessHelper = new System.Windows.Forms.Button();
			this.btnDBGetUsers = new System.Windows.Forms.Button();
			this.btnCheckAccessTest = new System.Windows.Forms.Button();
			this.btnIsAMemberOf = new System.Windows.Forms.Button();
			this.btnIHV = new System.Windows.Forms.Button();
			this.btnApplicationPermissions = new System.Windows.Forms.Button();
			this.btnCacheTest = new System.Windows.Forms.Button();
			this.txtOp = new System.Windows.Forms.TextBox();
			this.btnCheckStoreAccess = new System.Windows.Forms.Button();
			this.btnCacheServiceTest = new System.Windows.Forms.Button();
			this.btnSerializeUserPermissionCache = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnCreateItemsFromAFolder = new System.Windows.Forms.Button();
			this.btnStorageCacheAuthorizedItems = new System.Windows.Forms.Button();
			this.btnCreateALotOfItems = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.butnChangeDBUserPassword = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(20, 280);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(1132, 544);
			this.textBox1.TabIndex = 1;
			// 
			// btnStoreManipulate
			// 
			this.btnStoreManipulate.Location = new System.Drawing.Point(16, 15);
			this.btnStoreManipulate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnStoreManipulate.Name = "btnStoreManipulate";
			this.btnStoreManipulate.Size = new System.Drawing.Size(161, 28);
			this.btnStoreManipulate.TabIndex = 10;
			this.btnStoreManipulate.Text = "Store Manipulate";
			this.btnStoreManipulate.UseVisualStyleBackColor = true;
			this.btnStoreManipulate.Click += new System.EventHandler(this.btnStoreManipulate_Click);
			// 
			// btnItemManipulate
			// 
			this.btnItemManipulate.Location = new System.Drawing.Point(185, 15);
			this.btnItemManipulate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnItemManipulate.Name = "btnItemManipulate";
			this.btnItemManipulate.Size = new System.Drawing.Size(161, 28);
			this.btnItemManipulate.TabIndex = 11;
			this.btnItemManipulate.Text = "Item Manipulate";
			this.btnItemManipulate.UseVisualStyleBackColor = true;
			this.btnItemManipulate.Click += new System.EventHandler(this.btnItemManipulate_Click);
			// 
			// btnADShowDialog
			// 
			this.btnADShowDialog.Location = new System.Drawing.Point(355, 15);
			this.btnADShowDialog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnADShowDialog.Name = "btnADShowDialog";
			this.btnADShowDialog.Size = new System.Drawing.Size(163, 28);
			this.btnADShowDialog.TabIndex = 12;
			this.btnADShowDialog.Text = "AD Show Dialog";
			this.btnADShowDialog.UseVisualStyleBackColor = true;
			this.btnADShowDialog.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnCheckAccess
			// 
			this.btnCheckAccess.Location = new System.Drawing.Point(525, 15);
			this.btnCheckAccess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCheckAccess.Name = "btnCheckAccess";
			this.btnCheckAccess.Size = new System.Drawing.Size(240, 28);
			this.btnCheckAccess.TabIndex = 13;
			this.btnCheckAccess.Text = "CheckAccess (Detect Differences)";
			this.btnCheckAccess.UseVisualStyleBackColor = true;
			this.btnCheckAccess.Click += new System.EventHandler(this.btnACL_Click);
			// 
			// btnEventHandling
			// 
			this.btnEventHandling.Location = new System.Drawing.Point(825, 15);
			this.btnEventHandling.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnEventHandling.Name = "btnEventHandling";
			this.btnEventHandling.Size = new System.Drawing.Size(161, 28);
			this.btnEventHandling.TabIndex = 14;
			this.btnEventHandling.Text = "Event Handling";
			this.btnEventHandling.UseVisualStyleBackColor = true;
			this.btnEventHandling.Click += new System.EventHandler(this.btnEventHandling_Click);
			// 
			// btnImportFromAzMan
			// 
			this.btnImportFromAzMan.Location = new System.Drawing.Point(16, 49);
			this.btnImportFromAzMan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnImportFromAzMan.Name = "btnImportFromAzMan";
			this.btnImportFromAzMan.Size = new System.Drawing.Size(161, 28);
			this.btnImportFromAzMan.TabIndex = 15;
			this.btnImportFromAzMan.Text = "Import From AzMan";
			this.btnImportFromAzMan.UseVisualStyleBackColor = true;
			this.btnImportFromAzMan.Click += new System.EventHandler(this.btnImportFromAzMan_Click);
			// 
			// btnTestForm
			// 
			this.btnTestForm.Location = new System.Drawing.Point(185, 49);
			this.btnTestForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnTestForm.Name = "btnTestForm";
			this.btnTestForm.Size = new System.Drawing.Size(161, 28);
			this.btnTestForm.TabIndex = 16;
			this.btnTestForm.Text = "Test Form";
			this.btnTestForm.UseVisualStyleBackColor = true;
			this.btnTestForm.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// btnTestImport
			// 
			this.btnTestImport.Location = new System.Drawing.Point(355, 50);
			this.btnTestImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnTestImport.Name = "btnTestImport";
			this.btnTestImport.Size = new System.Drawing.Size(163, 28);
			this.btnTestImport.TabIndex = 17;
			this.btnTestImport.Text = "Test Import";
			this.btnTestImport.UseVisualStyleBackColor = true;
			this.btnTestImport.Click += new System.EventHandler(this.btnTestImport_Click);
			// 
			// btnTestDomain
			// 
			this.btnTestDomain.Location = new System.Drawing.Point(525, 49);
			this.btnTestDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnTestDomain.Name = "btnTestDomain";
			this.btnTestDomain.Size = new System.Drawing.Size(128, 28);
			this.btnTestDomain.TabIndex = 18;
			this.btnTestDomain.Text = "Test Domain";
			this.btnTestDomain.UseVisualStyleBackColor = true;
			this.btnTestDomain.Click += new System.EventHandler(this.btnTestDomain_Click);
			// 
			// btnTestADShowDialog2
			// 
			this.btnTestADShowDialog2.Location = new System.Drawing.Point(661, 49);
			this.btnTestADShowDialog2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnTestADShowDialog2.Name = "btnTestADShowDialog2";
			this.btnTestADShowDialog2.Size = new System.Drawing.Size(161, 28);
			this.btnTestADShowDialog2.TabIndex = 19;
			this.btnTestADShowDialog2.Text = "AD Show Dialog 2";
			this.btnTestADShowDialog2.UseVisualStyleBackColor = true;
			this.btnTestADShowDialog2.Click += new System.EventHandler(this.btnTestADShowDialog2_Click);
			// 
			// btnCheckAccessTemplate
			// 
			this.btnCheckAccessTemplate.Location = new System.Drawing.Point(16, 85);
			this.btnCheckAccessTemplate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCheckAccessTemplate.Name = "btnCheckAccessTemplate";
			this.btnCheckAccessTemplate.Size = new System.Drawing.Size(331, 28);
			this.btnCheckAccessTemplate.TabIndex = 20;
			this.btnCheckAccessTemplate.Text = "Check Access Template";
			this.btnCheckAccessTemplate.UseVisualStyleBackColor = true;
			this.btnCheckAccessTemplate.Click += new System.EventHandler(this.btnCheckAccessTemplate_Click);
			// 
			// btnGenerateCheckAccessHelper
			// 
			this.btnGenerateCheckAccessHelper.Location = new System.Drawing.Point(355, 86);
			this.btnGenerateCheckAccessHelper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnGenerateCheckAccessHelper.Name = "btnGenerateCheckAccessHelper";
			this.btnGenerateCheckAccessHelper.Size = new System.Drawing.Size(331, 28);
			this.btnGenerateCheckAccessHelper.TabIndex = 21;
			this.btnGenerateCheckAccessHelper.Text = "Generate Check Access Helper";
			this.btnGenerateCheckAccessHelper.UseVisualStyleBackColor = true;
			this.btnGenerateCheckAccessHelper.Click += new System.EventHandler(this.btnGenerateCheckAccessHelper_Click);
			// 
			// btnDBGetUsers
			// 
			this.btnDBGetUsers.Location = new System.Drawing.Point(16, 121);
			this.btnDBGetUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnDBGetUsers.Name = "btnDBGetUsers";
			this.btnDBGetUsers.Size = new System.Drawing.Size(125, 28);
			this.btnDBGetUsers.TabIndex = 22;
			this.btnDBGetUsers.Text = "DBGetUsers";
			this.btnDBGetUsers.UseVisualStyleBackColor = true;
			this.btnDBGetUsers.Click += new System.EventHandler(this.btnDBGetUsers_Click);
			// 
			// btnCheckAccessTest
			// 
			this.btnCheckAccessTest.Location = new System.Drawing.Point(149, 121);
			this.btnCheckAccessTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCheckAccessTest.Name = "btnCheckAccessTest";
			this.btnCheckAccessTest.Size = new System.Drawing.Size(197, 28);
			this.btnCheckAccessTest.TabIndex = 23;
			this.btnCheckAccessTest.Text = "CheckAccessTest";
			this.btnCheckAccessTest.UseVisualStyleBackColor = true;
			this.btnCheckAccessTest.Click += new System.EventHandler(this.btnCheckAccessTest_Click);
			// 
			// btnIsAMemberOf
			// 
			this.btnIsAMemberOf.Location = new System.Drawing.Point(355, 122);
			this.btnIsAMemberOf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnIsAMemberOf.Name = "btnIsAMemberOf";
			this.btnIsAMemberOf.Size = new System.Drawing.Size(163, 28);
			this.btnIsAMemberOf.TabIndex = 24;
			this.btnIsAMemberOf.Text = "IsAMemberOf";
			this.btnIsAMemberOf.UseVisualStyleBackColor = true;
			this.btnIsAMemberOf.Click += new System.EventHandler(this.btnIsAMemberOf_Click);
			// 
			// btnIHV
			// 
			this.btnIHV.Location = new System.Drawing.Point(523, 121);
			this.btnIHV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnIHV.Name = "btnIHV";
			this.btnIHV.Size = new System.Drawing.Size(163, 28);
			this.btnIHV.TabIndex = 25;
			this.btnIHV.Text = "Item H V";
			this.btnIHV.UseVisualStyleBackColor = true;
			this.btnIHV.Click += new System.EventHandler(this.btnIHV_Click);
			// 
			// btnApplicationPermissions
			// 
			this.btnApplicationPermissions.Location = new System.Drawing.Point(693, 121);
			this.btnApplicationPermissions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnApplicationPermissions.Name = "btnApplicationPermissions";
			this.btnApplicationPermissions.Size = new System.Drawing.Size(215, 28);
			this.btnApplicationPermissions.TabIndex = 26;
			this.btnApplicationPermissions.Text = "Application Permissions";
			this.btnApplicationPermissions.UseVisualStyleBackColor = true;
			this.btnApplicationPermissions.Click += new System.EventHandler(this.button1_Click_2);
			// 
			// btnCacheTest
			// 
			this.btnCacheTest.Location = new System.Drawing.Point(916, 121);
			this.btnCacheTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCacheTest.Name = "btnCacheTest";
			this.btnCacheTest.Size = new System.Drawing.Size(124, 28);
			this.btnCacheTest.TabIndex = 27;
			this.btnCacheTest.Text = "Cache Test";
			this.btnCacheTest.UseVisualStyleBackColor = true;
			this.btnCacheTest.Click += new System.EventHandler(this.btnCacheTest_Click);
			// 
			// txtOp
			// 
			this.txtOp.Location = new System.Drawing.Point(1048, 121);
			this.txtOp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtOp.Name = "txtOp";
			this.txtOp.Size = new System.Drawing.Size(68, 22);
			this.txtOp.TabIndex = 28;
			this.txtOp.Text = "Op1";
			// 
			// btnCheckStoreAccess
			// 
			this.btnCheckStoreAccess.Location = new System.Drawing.Point(693, 85);
			this.btnCheckStoreAccess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCheckStoreAccess.Name = "btnCheckStoreAccess";
			this.btnCheckStoreAccess.Size = new System.Drawing.Size(347, 28);
			this.btnCheckStoreAccess.TabIndex = 29;
			this.btnCheckStoreAccess.Text = "Check Store/Application Access";
			this.btnCheckStoreAccess.UseVisualStyleBackColor = true;
			this.btnCheckStoreAccess.Click += new System.EventHandler(this.btnCheckStoreAccess_Click);
			// 
			// btnCacheServiceTest
			// 
			this.btnCacheServiceTest.Location = new System.Drawing.Point(20, 156);
			this.btnCacheServiceTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCacheServiceTest.Name = "btnCacheServiceTest";
			this.btnCacheServiceTest.Size = new System.Drawing.Size(327, 28);
			this.btnCacheServiceTest.TabIndex = 30;
			this.btnCacheServiceTest.Text = "Cache Service Test (WCF)";
			this.btnCacheServiceTest.UseVisualStyleBackColor = true;
			this.btnCacheServiceTest.Click += new System.EventHandler(this.btnCacheServiceTest_Click);
			// 
			// btnSerializeUserPermissionCache
			// 
			this.btnSerializeUserPermissionCache.Location = new System.Drawing.Point(355, 155);
			this.btnSerializeUserPermissionCache.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnSerializeUserPermissionCache.Name = "btnSerializeUserPermissionCache";
			this.btnSerializeUserPermissionCache.Size = new System.Drawing.Size(331, 28);
			this.btnSerializeUserPermissionCache.TabIndex = 31;
			this.btnSerializeUserPermissionCache.Text = "Serialize UserPermissionCache";
			this.btnSerializeUserPermissionCache.UseVisualStyleBackColor = true;
			this.btnSerializeUserPermissionCache.Click += new System.EventHandler(this.btnSerializeUserPermissionCache_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(693, 155);
			this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(163, 28);
			this.btnExport.TabIndex = 32;
			this.btnExport.Text = "Test Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnCreateItemsFromAFolder
			// 
			this.btnCreateItemsFromAFolder.Location = new System.Drawing.Point(864, 155);
			this.btnCreateItemsFromAFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCreateItemsFromAFolder.Name = "btnCreateItemsFromAFolder";
			this.btnCreateItemsFromAFolder.Size = new System.Drawing.Size(253, 28);
			this.btnCreateItemsFromAFolder.TabIndex = 33;
			this.btnCreateItemsFromAFolder.Text = "CreateItemsFromAFolder";
			this.btnCreateItemsFromAFolder.UseVisualStyleBackColor = true;
			this.btnCreateItemsFromAFolder.Click += new System.EventHandler(this.btnCreateItemsFromAFolder_Click);
			// 
			// btnStorageCacheAuthorizedItems
			// 
			this.btnStorageCacheAuthorizedItems.Location = new System.Drawing.Point(20, 192);
			this.btnStorageCacheAuthorizedItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnStorageCacheAuthorizedItems.Name = "btnStorageCacheAuthorizedItems";
			this.btnStorageCacheAuthorizedItems.Size = new System.Drawing.Size(327, 28);
			this.btnStorageCacheAuthorizedItems.TabIndex = 34;
			this.btnStorageCacheAuthorizedItems.Text = "StorageCache AuthorizedItems";
			this.btnStorageCacheAuthorizedItems.UseVisualStyleBackColor = true;
			this.btnStorageCacheAuthorizedItems.Click += new System.EventHandler(this.btnStorageCacheAuthorizedItems_Click);
			// 
			// btnCreateALotOfItems
			// 
			this.btnCreateALotOfItems.Location = new System.Drawing.Point(355, 191);
			this.btnCreateALotOfItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnCreateALotOfItems.Name = "btnCreateALotOfItems";
			this.btnCreateALotOfItems.Size = new System.Drawing.Size(331, 28);
			this.btnCreateALotOfItems.TabIndex = 35;
			this.btnCreateALotOfItems.Text = "Create A Lot Of Items";
			this.btnCreateALotOfItems.UseVisualStyleBackColor = true;
			this.btnCreateALotOfItems.Click += new System.EventHandler(this.btnCreateALotOfItems_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 242);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(201, 30);
			this.button1.TabIndex = 36;
			this.button1.Text = "Verificar usuario ADMIN";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_3);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(225, 242);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(215, 30);
			this.button2.TabIndex = 37;
			this.button2.Text = "storage.CheckAccess";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// butnChangeDBUserPassword
			// 
			this.butnChangeDBUserPassword.Location = new System.Drawing.Point(448, 242);
			this.butnChangeDBUserPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.butnChangeDBUserPassword.Name = "butnChangeDBUserPassword";
			this.butnChangeDBUserPassword.Size = new System.Drawing.Size(215, 30);
			this.butnChangeDBUserPassword.TabIndex = 38;
			this.butnChangeDBUserPassword.Text = "Change DBUser password";
			this.butnChangeDBUserPassword.UseVisualStyleBackColor = true;
			this.butnChangeDBUserPassword.Click += new System.EventHandler(this.butnChangeDBUserPassword_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(711, 244);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(197, 28);
			this.button3.TabIndex = 39;
			this.button3.Text = "storage.CheckAccessLDAP";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(920, 209);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(197, 28);
			this.button4.TabIndex = 40;
			this.button4.Text = "storage.GetLDAPUser";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(919, 242);
			this.button5.Margin = new System.Windows.Forms.Padding(4);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(197, 28);
			this.button5.TabIndex = 41;
			this.button5.Text = "storage.GetDBUser";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1165, 753);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.butnChangeDBUserPassword);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnCreateALotOfItems);
			this.Controls.Add(this.btnStorageCacheAuthorizedItems);
			this.Controls.Add(this.btnCreateItemsFromAFolder);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnSerializeUserPermissionCache);
			this.Controls.Add(this.btnCacheServiceTest);
			this.Controls.Add(this.btnCheckStoreAccess);
			this.Controls.Add(this.txtOp);
			this.Controls.Add(this.btnCacheTest);
			this.Controls.Add(this.btnApplicationPermissions);
			this.Controls.Add(this.btnIHV);
			this.Controls.Add(this.btnIsAMemberOf);
			this.Controls.Add(this.btnCheckAccessTest);
			this.Controls.Add(this.btnDBGetUsers);
			this.Controls.Add(this.btnGenerateCheckAccessHelper);
			this.Controls.Add(this.btnCheckAccessTemplate);
			this.Controls.Add(this.btnTestADShowDialog2);
			this.Controls.Add(this.btnTestDomain);
			this.Controls.Add(this.btnTestImport);
			this.Controls.Add(this.btnTestForm);
			this.Controls.Add(this.btnImportFromAzMan);
			this.Controls.Add(this.btnEventHandling);
			this.Controls.Add(this.btnCheckAccess);
			this.Controls.Add(this.btnADShowDialog);
			this.Controls.Add(this.btnItemManipulate);
			this.Controls.Add(this.btnStoreManipulate);
			this.Controls.Add(this.textBox1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStoreManipulate;
        private System.Windows.Forms.Button btnItemManipulate;
        private System.Windows.Forms.Button btnADShowDialog;
        private System.Windows.Forms.Button btnCheckAccess;
        private System.Windows.Forms.Button btnEventHandling;
        private System.Windows.Forms.Button btnImportFromAzMan;
        private System.Windows.Forms.Button btnTestForm;
        private System.Windows.Forms.Button btnTestImport;
        private System.Windows.Forms.Button btnTestDomain;
        private System.Windows.Forms.Button btnTestADShowDialog2;
        private System.Windows.Forms.Button btnCheckAccessTemplate;
        private System.Windows.Forms.Button btnGenerateCheckAccessHelper;
        private System.Windows.Forms.Button btnDBGetUsers;
        private System.Windows.Forms.Button btnCheckAccessTest;
        private System.Windows.Forms.Button btnIsAMemberOf;
        private System.Windows.Forms.Button btnIHV;
        private System.Windows.Forms.Button btnApplicationPermissions;
        private System.Windows.Forms.Button btnCacheTest;
        private System.Windows.Forms.TextBox txtOp;
        private System.Windows.Forms.Button btnCheckStoreAccess;
        private System.Windows.Forms.Button btnCacheServiceTest;
        private System.Windows.Forms.Button btnSerializeUserPermissionCache;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCreateItemsFromAFolder;
        private System.Windows.Forms.Button btnStorageCacheAuthorizedItems;
        private System.Windows.Forms.Button btnCreateALotOfItems;
		  private System.Windows.Forms.Button button1;
		  private System.Windows.Forms.Button button2;
		  private System.Windows.Forms.Button butnChangeDBUserPassword;
		  private System.Windows.Forms.Button button3;
		  private System.Windows.Forms.Button button4;
		  private System.Windows.Forms.Button button5;
    }
}

