IF EXISTS ( SELECT   *
            FROM     sys.triggers
            WHERE    object_id = OBJECT_ID(N'[Basgosoft].[ReplicateUpdatedUserOnCampusDatabase]') ) 
   DROP TRIGGER [Basgosoft].[ReplicateUpdatedUserOnCampusDatabase]
GO

CREATE TRIGGER [Basgosoft].[ReplicateUpdatedUserOnCampusDatabase] ON [Basgosoft].[User]  
   AFTER UPDATE  
AS  
BEGIN  
   SET NOCOUNT ON ;  
  
   UPDATE   t  
   SET      UserName = i.UserName  
   FROM     DELETED AS d  
            INNER JOIN INSERTED AS i ON d.UserID = i.UserID  
            INNER JOIN AzManLogin.dbo.Login AS t ON d.UserName = t.UserName COLLATE Modern_Spanish_100_CS_AI ;  
   
   UPDATE   t  
   SET      orga_username = i.UserName,  
            orga_fullname = i.FullName  
   FROM     CampusVirtual.dbo.ORGA_Usuario AS t  
            INNER JOIN INSERTED AS i ON t.orga_userid = i.UserID ;                 
END