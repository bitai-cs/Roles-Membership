/*

NO SE EJECUTA ESTE SCRIPT, ES SOLO UN EJEMPLO

*/

CREATE ASSEMBLY 
SMDiagnostics FROM
'C:\Windows\Microsoft.NET\Framework64\v3.0\Windows Communication Foundation\SMDiagnostics.dll'
WITH PERMISSION_SET = UNSAFE

GO
 
CREATE ASSEMBLY 
[System.Web] FROM
'C:\Windows\Microsoft.NET\Framework64\v2.0.50727\System.Web.dll'
WITH PERMISSION_SET = UNSAFE

GO

CREATE ASSEMBLY 
[System.Messaging] FROM
'C:\Windows\Microsoft.NET\Framework64\v2.0.50727\System.Messaging.dll'
WITH PERMISSION_SET = UNSAFE
 
GO

CREATE ASSEMBLY  
[System.IdentityModel] FROM
'C:\Windows\Microsoft.NET\Framework64\v3.0\System.IdentityModel.dll'
WITH PERMISSION_SET = UNSAFE

GO

CREATE ASSEMBLY  
[System.IdentityModel.Selectors] FROM
'C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0\System.IdentityModel.Selectors.dll'
WITH PERMISSION_SET = UNSAFE

GO

CREATE ASSEMBLY -- this will add service modal
[Microsoft.Transactions.Bridge] FROM
'C:\Windows\Microsoft.NET\Framework64\v3.0\Windows Communication Foundation\Microsoft.Transactions.Bridge.dll'
WITH PERMISSION_SET = UNSAFE

GO