ALTER VIEW [Basgosoft].[vw_Users]
AS
SELECT   u.UserID,
         u.UserName,
         CONVERT(NVARCHAR(50), u.Password) AS Password,
         u.FirstName,
         u.LastName,
         u.FullName,
			u.Mail,
         u.DepartmentId,
         d.DepartmentName,         
         Basgosoft.GetBranchOfficeIdsString(u.UserID) AS BranchOfficeIds,
         Basgosoft.GetRelativeBranchOfficeIdsString(u.UserID) AS RelativeBranchOfficeIds,
         Basgosoft.GetBranchOfficeNamesString(u.UserID) AS BranchOfficeNames,
         u.Enabled,
         u._RowVersion
FROM     Basgosoft.[User] AS u
         INNER JOIN Basgosoft.Department AS d ON u.DepartmentId = d.DepartmentId