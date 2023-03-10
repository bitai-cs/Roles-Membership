ALTER PROCEDURE [Basgosoft].[Login_pa_Insert]
   @LoginId NVARCHAR(255),
   @AppName NVARCHAR(255),
   @LDAPDomain NVARCHAR(255) = NULL, 
   @UserName NVARCHAR(255),
   @RemoteIpV4 NVARCHAR(50),
   @LogIn DATETIME,
   @Expires DATETIME,
   @Timeout INT,
   @Expiration DATETIME = NULL,
   @Expired BIT,
   @LogOff DATETIME = NULL,
   @LoggedOut BIT,
   @Renovated BIT,
   @Renewal DATETIME = NULL
AS
BEGIN
   INSERT   [Basgosoft].[Login]
            ([LoginId],
             [AppName],
             [LDAPDomain],
             [UserName],
             [RemoteIpV4],
             [LogIn],
             [Expires],
             [Timeout],
             [Expiration],
             [Expired],
             [LogOff],
             [LoggedOut],
             Renovated,
             Renewal)
   VALUES   (@LoginId,
             @AppName,
             @LDAPDomain,
             @UserName,
             @RemoteIpV4,
             @LogIn,
             @Expires,
             @Timeout,
             @Expiration,
             @Expired,
             @LogOff,
             @LoggedOut,
             @Renovated,
             @Renewal);
END;