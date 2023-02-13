CREATE PROCEDURE Basgosoft.[ChangeDBUserPassword]
   @userId INT,
   @currentPassword NVARCHAR(50),
   @renewedPassword NVARCHAR(50),
   @passwordConfirmation NVARCHAR(50)
AS 
BEGIN	
   IF NOT EXISTS ( SELECT  UserID
                   FROM    Basgosoft.[User]
                   WHERE   UserID = @userId ) 
      BEGIN
         RAISERROR('No se puede ubicar el usuario con id=%d',16,1,@userId) ;
         RETURN ;
      END
	
   DECLARE @password NVARCHAR(50) ;
   SELECT   @password = CONVERT(NVARCHAR(50), Password)
   FROM     Basgosoft.[User] AS u
   WHERE    UserID = @userId ; 
	
   IF @password <> @currentPassword 
      BEGIN
         RAISERROR('La contraseña actual no coincide. no se puede realizarla operación.',16,1) ;
         RETURN ;
      END	
			
   IF @renewedPassword <> @passwordConfirmation 
      BEGIN
         RAISERROR('La nueva contraseña y la confirmación de la contraseña no coinciden. no se puede continuar con la operacion.',16,1) ;
         RETURN ;	
      END
	
   UPDATE   Basgosoft.[User]
   SET      Password = CONVERT(VARBINARY(50), @renewedPassword)
   WHERE    UserID = @userId
            AND Password = CONVERT(VARBINARY(50), @currentPassword)
   
   IF @@ROWCOUNT = 0 
      BEGIN
         RAISERROR('no se puedo actualizar la contraseña. Ha sido modificado por otro usuario o proceso.', 16,1) ;
         RETURN
      END
END