ALTER FUNCTION Basgosoft.GetRelativeBranchOfficeIdsString (@UserID INT)
RETURNS NVARCHAR(MAX)
BEGIN
   DECLARE @string NVARCHAR(MAX) ;
	
   SET @string = '' ;
   
   SELECT   @string = @string + CONVERT(NVARCHAR(5), bo.RelativeBranchOfficeId) + CHAR(33)
   FROM     Basgosoft.UserBranchOffice AS ubo
				INNER JOIN Basgosoft.BranchOffice AS bo ON ubo.BranchOfficeId = bo.BranchOfficeId
   WHERE    UserID = @UserID
   ORDER BY ubo.BranchOfficeId ;
   
   IF LEN(@string) > 0 
      SET @string = SUBSTRING(@string, 1, LEN(@string) - 1) ;
	
   RETURN @string ;
   
/*
SELECT Basgosoft.GetBranchOfficeIdsString (0)
SELECT Basgosoft.GetRelativeBranchOfficeIdsString (0)
SELECT Basgosoft.GetBranchOfficeNamesString (0)
*/   
END