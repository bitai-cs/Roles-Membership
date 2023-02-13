ALTER PROCEDURE Basgosoft.UpdateUser_1
   @userID INT,
   @userName NVARCHAR(255),
   @password NVARCHAR(50),
   @firstName NVARCHAR(150),
   @lastName NVARCHAR(150),
   @mail NVARCHAR(255),
   @departmentId INT,
   @enabled BIT,
   @_rowVersion ROWVERSION,
   @Updated BIT OUTPUT
AS 
BEGIN

   UPDATE   Basgosoft.[User]
   SET      UserName = @UserName,
            Password = CONVERT(VARBINARY(50), @Password),
            FirstName = @firstName,
            LastName = @lastName,
            Mail = @mail,
            DepartmentId = @departmentId,
            Enabled = @enabled
   WHERE    UserID = @UserId
            AND [_RowVersion] = @_rowVersion ;
			  			  
   IF @@rowcount = 0 
      SET @updated = 0 ;
   ELSE 
      SET @updated = 1 ;
END