ALTER PROCEDURE Basgosoft.GetUser_2 @userId INT
AS 
BEGIN
   SELECT   UserID,
            UserName,
            Password,
            FirstName,
            LastName,
            FullName,
				Mail,
            DepartmentId,
            DepartmentName,
            BranchOfficeIds,
				RelativeBranchOfficeIds
            BranchOfficeNames,
            Enabled,
            [_RowVersion]
   FROM     Basgosoft.vw_Users
   WHERE    UserID = @userId ;       
END