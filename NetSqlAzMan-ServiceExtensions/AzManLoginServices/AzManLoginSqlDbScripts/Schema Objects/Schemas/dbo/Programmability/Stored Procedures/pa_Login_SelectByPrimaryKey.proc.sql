CREATE PROCEDURE [dbo].[pa_Login_SelectByPrimaryKey] @LoginId NVARCHAR(255)
AS 
BEGIN
   SELECT   [LoginId],
            [AppName],
			LDAPDomain,
            [UserName],
            [RemoteIpV4],
            [LogIn],
            [Expires],
            [Timeout],
            [Expiration],
            [Expired],
            [LogOff],
            [LoggedOut],
            [Renovated],
			[Renewal],
			[Expiration-Renewal]
   FROM     [Basgosoft].[Login]
   WHERE    [LoginId] = @LoginId;

END