ALTER PROCEDURE [dbo].[identity_sp_InitializeDatabaseData]
   @rootUser NVARCHAR(255) = N'admin',
   @rootPassword NVARCHAR(50) = N'123456',
   @rootFirstName NVARCHAR(150) = N'Super',
   @rootLastName NVARCHAR(150) = N'User',
   @rootMail NVARCHAR(255) = NULL
AS
BEGIN TRY 
   SET NOCOUNT ON;
	
   PRINT '*************************************';
   PRINT 'Inicializando datos...';

   IF @rootUser IS NULL
      OR LEN(RTRIM(LTRIM(@rootUser))) = 0
      RAISERROR('@rootUser no puede ser nulo o en blanco.', 16, 1);

   IF @rootPassword IS NULL
      OR LEN(RTRIM(LTRIM(@rootPassword))) = 0
      RAISERROR('@rootPassword no puede ser nulo o en blanco.', 16, 1);

   IF @rootFirstName IS NULL
      OR LEN(RTRIM(LTRIM(@rootFirstName))) = 0
      RAISERROR('@rootFirstName no puede ser nulo o en blanco.', 16, 1);

   IF @rootLastName IS NULL
      OR LEN(RTRIM(LTRIM(@rootLastName))) = 0
      RAISERROR('@rootLastName no puede ser nulo o en blanco.', 16, 1);
	
   IF EXISTS ( SELECT TOP 1
                        DepartmentId
               FROM     dbo.identity_Department )
      RAISERROR('Ya existen datos en identity_department. No se puede conrinuar.', 16, 1);

   IF EXISTS ( SELECT TOP 1
                        UserID
               FROM     dbo.identity_User )
      RAISERROR('Ya existen datos en identity_User. No se puede conrinuar.', 16, 1);

   BEGIN TRANSACTION;
   --Registrar departamentos predeterminados
   INSERT   INTO dbo.identity_Department
            (DepartmentName)
   VALUES   (N'(No asignado)');
   INSERT   INTO dbo.identity_Department
            (DepartmentName)
   VALUES   (N'(Global)');

	--Registrar Usuario ROOT
   INSERT   INTO dbo.identity_User
            (UserName,
             Password,
             PasswordHash,
             FirstName,
             LastName,
             Mail,
             DepartmentId,
             Enabled)
   VALUES   (@rootUser, -- UserName - nvarchar(255)
             CONVERT(VARBINARY(50), @rootPassword), -- Password - varbinary(50)
             NULL, -- PasswordHash - nvarchar(2048)
             @rootFirstName, -- FirstName - nvarchar(150)
             @rootLastName, -- LastName - nvarchar(150)
             @rootMail, -- Mail - nvarchar(255)
             0, -- DepartmentId - int
             1 -- Enabled - bit
             );

   COMMIT TRANSACTION;

   PRINT 'Inicialización completada.'
   PRINT '*************************************'
END TRY 
BEGIN CATCH
   IF @@TRANCOUNT > 0
      ROLLBACK TRANSACTION;

   DECLARE @ErrorNumber INT;
   DECLARE @ErrorState INT;
   DECLARE @ErrorSeverity INT;
   DECLARE @ErrorLine INT;
   DECLARE @ErrorProc VARCHAR(126);
   DECLARE @ErrorMesg VARCHAR(2048);
   DECLARE @vUserName VARCHAR(128);
   DECLARE @vHostName VARCHAR(128);

   SELECT   @ErrorNumber = ERROR_NUMBER(),
            @ErrorState = ERROR_STATE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorLine = ERROR_LINE(),
            @ErrorProc = ERROR_PROCEDURE(),
            @ErrorMesg = ERROR_MESSAGE(),
            @vUserName = SUSER_SNAME(),
            @vHostName = HOST_NAME();
	
   PRINT 'Error en la inicialización de datos...'
   PRINT 'Error: ' + @ErrorMesg;
   PRINT 'Error Number: '
      + CONVERT(NVARCHAR(30), @ErrorNumber);
   PRINT 'State: ' + CONVERT(NVARCHAR(30), @ErrorState);
   PRINT 'Severity: ' + CONVERT(NVARCHAR(30), @ErrorSeverity);
   PRINT '*************************************'
END CATCH
