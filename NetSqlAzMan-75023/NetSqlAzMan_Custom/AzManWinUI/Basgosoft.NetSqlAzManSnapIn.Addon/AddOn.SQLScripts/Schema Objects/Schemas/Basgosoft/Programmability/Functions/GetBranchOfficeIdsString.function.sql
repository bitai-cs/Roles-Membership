ALTER FUNCTION Basgosoft.GetBranchOfficeIdsString (@UserID INT)
RETURNS NVARCHAR(MAX)
BEGIN
   DECLARE @string NVARCHAR(MAX) ;
	
   SET @string = '' ;
   
   SELECT   @string = @string + CONVERT(NVARCHAR(5), BranchOfficeId) + CHAR(33)
   FROM     Basgosoft.UserBranchOffice
   WHERE    UserID = @UserID
   ORDER BY BranchOfficeId ;
   
   IF LEN(@string) > 0 
      SET @string = SUBSTRING(@string, 1, LEN(@string) - 1) ;
	
   RETURN @string ;
END