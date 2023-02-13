ALTER TRIGGER [Basgosoft].[PreventAdminBranchOfficeChanges] ON [Basgosoft].UserBranchOffice
   AFTER DELETE, UPDATE
AS
BEGIN	
   SET NOCOUNT ON ;
	
   DECLARE @deleted VARCHAR(255) ;
   --DECLARE @current VARCHAR(255) ;
	
   IF EXISTS ( SELECT   UserID
               FROM     DELETED
               WHERE    UserID = 0 ) 
      BEGIN
         IF UPDATE(UserID)
            OR UPDATE(BranchOfficeId) 
            BEGIN
					--Check UserID
               SELECT   @deleted = UserID
               FROM     DELETED
               WHERE    UserID = 0 ;
					
					/*
               SELECT   @current = UserID
               FROM     iNSERTED
               WHERE    UserID = 0 ;
					*/
					
               IF (@deleted = 0) 
                  BEGIN
                     RAISERROR('No se puede modificar la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;
                  END
               
               /*   
					--Check BranchOfficeId
               SELECT   @deleted = BranchOfficeId
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
               */
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
            END
         ELSE --Es eliminacion de datos
            BEGIN
               IF EXISTS ( SELECT   UserID
                           FROM     DELETED
                           WHERE    UserID = 0 )
                  /*AND NOT EXISTS ( SELECT UserID
                                   FROM   INSERTED
                                   WHERE  UserID = 0 )*/ 
                  BEGIN
                     RAISERROR('No se pueden eliminar datos de la cuenta Admin.',16,1) ;
                     ROLLBACK TRANSACTION ;
                     RETURN ;				
                  END
            END
						
      END	
END