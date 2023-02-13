CREATE PROCEDURE [dbo].[pa_Login_DeleteByPrimaryKey] @LoginId NVARCHAR(255)
AS 
BEGIN
   DELETE   FROM [dbo].[Login]
   WHERE    [LoginId] = @LoginId
END