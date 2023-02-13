ALTER PROCEDURE dbo.pa_Job_SetExpiredLogins
AS 
BEGIN
   DECLARE @now DATETIME ;
   SET @now = GETDATE() ;
	
   UPDATE   dbo.Login
   SET      Expiration = @now,
            Expired = 1
   WHERE    LoggedOut = 0 --Este en linea
            AND Expired = 0 --No haya expirado
            AND GETDATE() > Expires --Ya sobrepasó el limite				
END