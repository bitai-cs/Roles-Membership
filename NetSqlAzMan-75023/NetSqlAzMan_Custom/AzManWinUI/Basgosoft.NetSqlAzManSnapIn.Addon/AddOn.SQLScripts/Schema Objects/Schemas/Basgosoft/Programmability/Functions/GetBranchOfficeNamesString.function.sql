ALTER FUNCTION Basgosoft.GetBranchOfficeNamesString (@User_ID INT)
RETURNS NVARCHAR(MAX)
BEGIN
   DECLARE @string NVARCHAR(MAX) ;
	
   SET @string = '' ;
   
   SELECT   @string = @string + bo.BranchOfficeName + CHAR(33)
   FROM     Basgosoft.UserBranchOffice AS ubo
            INNER JOIN Basgosoft.BranchOffice AS bo ON ubo.BranchOfficeId = bo.BranchOfficeId
   WHERE    ubo.UserID = @User_ID
   ORDER BY ubo.BranchOfficeId ;
	
   IF LEN(@string) > 0 
      SET @string = SUBSTRING(@string, 1, LEN(@string) - 1) ;
	
   RETURN @string ;
END
/*
SELECT Basgosoft.GetBranchOfficeNamesString (0)
*/