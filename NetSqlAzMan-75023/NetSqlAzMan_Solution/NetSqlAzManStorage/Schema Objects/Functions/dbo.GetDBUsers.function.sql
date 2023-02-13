/*     
   NetSqlAzMan GetDBUsers TABLE Function    
   ************************************************************************    
   Creation Date: August, 23  2006    
   Purpose: Retrieve from a DB a list of custom Users (DBUserSid, DBUserName)    
   Author: Andrea Ferendeles     
   Revision: 1.0.0.0    
   Updated by: Victor Bastidas    
   Parameters:     
   
 Use:     
 1)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, NULL, NULL)            -- to retrieve all DB Users    
 2)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, <customsid>, NULL)  -- to retrieve DB User with specified <customsid>    
 3)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, NULL, <username>)  -- to retrieve DB User with specified <username>    
    
   Remarks:     
 -Update this Function with your CUSTOM CODE    
 -Returned DBUserSid must be unique    
 -Returned DBUserName must be unique    
*/    
ALTER FUNCTION [dbo].[netsqlazman_GetDBUsers]
(
 @StoreName NVARCHAR(255),
 @ApplicationName NVARCHAR(255),
 @DBUserSid VARBINARY(85) = NULL,
 @DBUserName NVARCHAR(255) = NULL    
)
RETURNS TABLE
   AS   
   RETURN
   SELECT TOP 100 PERCENT
            CONVERT(VARBINARY(85), u.UserID) AS DBUserSid,
            u.UserName AS DBUserName,
            u.UserID,
            u.FirstName,
            u.LastName,
            u.FullName,
            u.Mail,
            Basgosoft.GetBranchOfficeIdsString(u.UserID) AS BranchOfficeIds,
            Basgosoft.GetBranchOfficeNamesString(u.UserID) AS BranchOfficeNames,
            u.DepartmentId,
            d.DepartmentName,
            u.Enabled,
            u.OtherFields
   FROM     Basgosoft.[User] AS u
            INNER JOIN Basgosoft.Department AS d ON u.DepartmentId = d.DepartmentId
   WHERE    (@DBUserSid IS NOT NULL
             AND CONVERT(VARBINARY(85), UserID) = @DBUserSid
             OR @DBUserSid IS NULL)
            AND (@DBUserName IS NOT NULL
                 AND UserName = @DBUserName
                 OR @DBUserName IS NULL)
   ORDER BY UserName    
--------------------------------------------------------------------------------------------------------------------------  
-- THIS CODE IS JUST FOR AN EXAMPLE: comment this section and customize "INSERT HERE YOUR CUSTOM T-SQL" section below    
--------------------------------------------------------------------------------------------------------------------------