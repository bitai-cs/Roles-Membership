ALTER TRIGGER [Basgosoft].[PreventAdminAccountChanges] ON [Basgosoft].[User]
   AFTER DELETE, UPDATE
AS
BEGIN	
   SET NOCOUNT ON ;
	
   DECLARE @deleted VARCHAR(255) ;
   DECLARE @current VARCHAR(255) ;
	
   IF EXISTS ( SELECT   UserID
               FROM     DELETED
               WHERE    UserID = 0 ) 
      BEGIN
         IF UPDATE(UserName)
            OR UPDATE(DepartmentId)
            --OR UPDATE(BranchOfficeIds) 
            BEGIN
					--Check UserName
               SELECT   @deleted = UserName
               FROM     DELETED
               WHERE    UserID = 0 ;
					
               SELECT   @current = UserName
               FROM     iNSERTED
               WHERE    UserID = 0 ;
					
               IF (@deleted <> @current) 
                  BEGIN
                     RAISERROR('No se puede modificar la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;
                  END
                  
					--Check DepartmentId
               SELECT   @deleted = DepartmentId
               FROM     DELETED
               WHERE    UserID = 0 ;
					
               SELECT   @current = DepartmentId
               FROM     iNSERTED
               WHERE    UserID = 0 ;
					
               IF (@deleted <> @current) 
                  BEGIN
                     RAISERROR('No se puede modificar el area o departamento de la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;
                  END                  
               
					/*   
					--Check BranchOfficeIds
               SELECT   @deleted = BranchOfficeIds
               FROM     DELETED
               WHERE    UserID = 0 ;
					
               SELECT   @current = BranchOfficeIds
               FROM     iNSERTED
               WHERE    UserID = 0 ;
					
               IF (@deleted <> @current) 
                  BEGIN
                     RAISERROR('No se puede modificar la sucursal de la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;
                  END       
               */
					   
					--Check Enabled                  
               IF EXISTS ( SELECT   UserID
                           FROM     INSERTED
                           WHERE    UserID = 0
                                    AND Enabled = 0 ) 
                  BEGIN
                     RAISERROR('No se puede deshabilitarla cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;
                  END       
										                             
            END
         ELSE --Es eliminacion de datos
            BEGIN
               IF EXISTS ( SELECT   UserID
                           FROM     DELETED
                           WHERE    UserID = 0 )
                  AND NOT EXISTS ( SELECT UserID
                                   FROM   INSERTED
                                   WHERE  UserID = 0 ) 
                  BEGIN
                     RAISERROR('No se puede eliminar la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;				
                  END
            END
						
      END	
END