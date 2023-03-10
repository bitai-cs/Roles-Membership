-- =============================================
-- Author:		Andrea Ferendeles
-- Create date: 13/04/2006
-- Description:	Get Name From Sid
-- =============================================
CREATE FUNCTION [dbo].[netsqlazman_GetNameFromSid] (@StoreName nvarchar(255), @ApplicationName nvarchar(255), @sid varbinary(85), @SidWhereDefined tinyint)
RETURNS nvarchar(255)
AS
BEGIN

DECLARE @Name nvarchar(255)
SET @Name = NULL

IF (@SidWhereDefined=0) --Store
BEGIN
SET @Name = (SELECT TOP 1 Name FROM dbo.[netsqlazman_StoreGroups]() WHERE objectSid = @sid)
END
ELSE IF (@SidWhereDefined=1) --Application 
BEGIN
SET @Name = (SELECT TOP 1 Name FROM dbo.[netsqlazman_ApplicationGroups]() WHERE objectSid = @sid)
END
ELSE IF (@SidWhereDefined=2 OR @SidWhereDefined=3) --LDAP or LOCAL
BEGIN
SET @Name = (SELECT Suser_Sname(@sid))
END
ELSE IF (@SidWhereDefined=4) --Database
BEGIN
SET @Name = (SELECT DBUserName FROM dbo.[netsqlazman_GetDBUsers](@StoreName, @ApplicationName, @sid, NULL))
END
IF (@Name IS NULL)
BEGIN
	SET @Name = @sid
END
RETURN @Name
END


