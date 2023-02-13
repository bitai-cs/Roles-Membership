IF EXISTS ( SELECT   *
            FROM     sys.triggers
            WHERE    object_id = OBJECT_ID(N'[Basgosoft].[ReplicateNewUserOnCampusDatabase]') ) 
   DROP TRIGGER [Basgosoft].[ReplicateNewUserOnCampusDatabase]
GO

CREATE TRIGGER [Basgosoft].[ReplicateNewUserOnCampusDatabase] ON [Basgosoft].[User]
   AFTER INSERT
AS
BEGIN	
   SET NOCOUNT ON ;

   INSERT   INTO CampusVirtual.dbo.ORGA_Usuario
            (orga_userid,
             orga_username,
             orga_fullname)
            SELECT   UserID,
                     UserName,
                     FullName
            FROM     INSERTED ; 	          	         
END