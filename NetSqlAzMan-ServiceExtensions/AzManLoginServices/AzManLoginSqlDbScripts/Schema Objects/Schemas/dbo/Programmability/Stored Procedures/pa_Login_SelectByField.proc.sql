CREATE PROCEDURE [dbo].[pa_Login_SelectByField]
   @FieldName VARCHAR(100),
   @Value VARCHAR(1000)
AS 
BEGIN
   DECLARE @query VARCHAR(2000) ;

   SET @query = 'SELECT [LoginId],[AppName],[UserName], [RemoteIpV4],[LogIn],[Expires],[Timeout],[Expiration],[Expired],[LogOff],[LoggedOut] FROM [dbo].[Login] WHERE ['
      + @FieldName + '] = ''' + @Value + ''''
   EXEC(@query)
END