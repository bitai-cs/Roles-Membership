SET NOCOUNT ON;

BEGIN TRY
   PRINT 'Sembrando datos...'
   TRUNCATE TABLE dbo.identity_LDAPConfig;
   TRUNCATE TABLE dbo.identity_Login
   TRUNCATE TABLE dbo.identity_UserValidationRequest;
   TRUNCATE TABLE dbo.identity_UserBranchOffice
   DELETE FROM dbo.identity_BranchOffice
   DELETE FROM dbo.identity_User
   DELETE FROM dbo.identity_Department

   BEGIN TRANSACTION;
--------------------------------------------------------------
   INSERT   INTO dbo.identity_LDAPConfig
            (ldap_domain,
             ldap_description,
             ldap_client_endpoint,
             ldap_enabled)
            SELECT   ldap_domain,
                     ldap_description,
                     ldap_client_endpoint,
                     ldap_enabled
            FROM     AzMan_MS_CI_AI.dbo.identity_LDAPConfig;
--------------------------------------------------------------
   INSERT   INTO dbo.identity_Login
            (LoginId,
             AppName,
             LDAPDomain,
             UserName,
             RemoteIpV4,
             LogIn,
             Expires,
             Timeout,
             Expiration,
             Expired,
             LogOff,
             LoggedOut,
             Renovated,
             Renewal,
             Expiration_Renewal)
            SELECT   LoginId,
                     AppName,
                     LDAPDomain,
                     UserName,
                     RemoteIpV4,
                     LogIn,
                     Expires,
                     Timeout,
                     Expiration,
                     Expired,
                     LogOff,
                     LoggedOut,
                     Renovated,
                     Renewal,
                     Expiration_Renewal
            FROM     AzMan_MS_CI_AI.dbo.identity_Login;
--------------------------------------------------------------
   SET IDENTITY_INSERT dbo.identity_Department ON;

   INSERT   INTO dbo.identity_Department
            (DepartmentId,
             DepartmentName)
            SELECT   DepartmentId,
                     DepartmentName
            FROM     AzMan_MS_CI_AI.dbo.identity_Department;

   SET IDENTITY_INSERT dbo.identity_Department OFF;
--------------------------------------------------------------
   INSERT   INTO dbo.identity_BranchOffice
            (BranchOfficeId,
             BranchOfficeName,
             RelativeBranchOfficeId)
            SELECT   BranchOfficeId,
                     BranchOfficeName,
                     RelativeBranchOfficeId
            FROM     AzMan_MS_CI_AI.dbo.identity_BranchOffice;
--------------------------------------------------------------
   SET IDENTITY_INSERT dbo.identity_User ON;

   INSERT   INTO dbo.identity_User
            (UserID,
             UserName,
             Password,
             PasswordHash,
             FirstName,
             LastName,
             FullName,
             Mail,
             DepartmentId,
             Enabled)
            SELECT   UserID,
                     UserName,
                     Password,
                     PasswordHash,
                     FirstName,
                     LastName,
                     FullName,
                     Mail,
                     DepartmentId,
                     Enabled
            FROM     AzMan_MS_CI_AI.dbo.identity_User;

   SET IDENTITY_INSERT dbo.identity_User OFF;
--------------------------------------------------------------
   INSERT   INTO dbo.identity_UserBranchOffice
            (UserID,
             BranchOfficeId)
            SELECT   UserID,
                     BranchOfficeId
            FROM     AzMan_MS_CI_AI.dbo.identity_UserBranchOffice;
--------------------------------------------------------------
   INSERT   INTO dbo.identity_UserValidationRequest
            (userValRequestId,
             valRequestDate,
             ldapDomain,
             userName,
             clientApplication,
             azManStore,
             azManAplication,
             azManItem,
             UserID)
            SELECT   userValRequestId,
                     valRequestDate,
                     ldapDomain,
                     userName,
                     clientApplication,
                     azManStore,
                     azManAplication,
                     azManItem,
                     UserID
            FROM     AzMan_MS_CI_AI.dbo.identity_UserValidationRequest;
--------------------------------------------------------------

	UPDATE   dbo.identity_User
	SET	   PasswordHash = CONVERT(NVARCHAR(50), Password)

   COMMIT TRANSACTION;
   PRINT 'Completado.'
END TRY
BEGIN CATCH
   IF (@@TRANCOUNT > 0)
      ROLLBACK TRANSACTION;
		
   DECLARE @msg NVARCHAR(MAX);
   SELECT   @msg = 'Error al sembrar datos: '
            + ERROR_MESSAGE();

   RAISERROR(@msg, 16 ,1);
END CATCH