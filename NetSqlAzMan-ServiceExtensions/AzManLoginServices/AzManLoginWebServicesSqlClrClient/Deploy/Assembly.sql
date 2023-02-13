/********************************************************************************
 ¿QUÉ HACE ESTE SCRIPT?
 
 1ro) Elimina (si es que existe) cada uno de los procedimientos
      almacenados CLR que ejecuta un método en el assembly AzManLoginWebServicesSqlClrClient.dll
 2do) Elimina (si es que existen en la base de datos) los assemblies
      AzManLoginWebServicesClient.dll, AzManLoginWebServicesClient.XmlSerializers.dll, 
      AzManLoginWebServicesSqlClrClient.dll y los instala en la base de datos cargandolos 
      desde la ruta C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\
 3ro) Crea los procedimientos almacenados CLR que ejecuta c/u de los métodos
	  en el assembly AzManLoginWebServicesSqlClrClient.dll

 Nota: Para dar acceso al usuario campus 
       GRANT EXTERNAL ACCESS ASSEMBLY TO campus
*********************************************************************************/


/*********************************************************************/
/* ELIMINAR SP CLR SI EXISTEN															*/
/*********************************************************************/
IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzManLogin_Test]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzManLogin_Test]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzManLogin_GetLogin]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzManLogin_GetLogin]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzManLogin_GetUserPropertyValueFromAttString]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzManLogin_GetUserPropertyValueFromAttString]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzManLogin_CheckLoginAccess]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzManLogin_CheckLoginAccess]
GO


/********************************************************************
 Elimina (si es que existieran) los ensamblados .NET
********************************************************************/
IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManLoginWebServicesSqlClrClient'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManLoginWebServicesSqlClrClient]
GO

IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManLoginWebServicesClient.XmlSerializers'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManLoginWebServicesClient.XmlSerializers]
GO

IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManLoginWebServicesClient'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManLoginWebServicesClient]
GO


/********************************************************************
 Registra los ensamblados .NET
********************************************************************/
CREATE ASSEMBLY [AzManLoginWebServicesClient]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\AzManLoginWebServicesClient.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

ALTER ASSEMBLY [AzManLoginWebServicesClient]
DROP FILE ALL
ADD FILE FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\AzManLoginWebServicesClient.pdb' AS N'AzManLoginWebServicesClient.pdb' ;
GO

CREATE ASSEMBLY [AzManLoginWebServicesClient.XmlSerializers]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\AzManLoginWebServicesClient.XmlSerializers.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

CREATE ASSEMBLY [AzManLoginWebServicesSqlClrClient]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\AzManLoginWebServicesSqlClrClient.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

ALTER ASSEMBLY [AzManLoginWebServicesSqlClrClient]
DROP FILE ALL
ADD FILE FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManLoginWebServicesSqlClrClient\AzManLoginWebServicesSqlClrClient.pdb' AS N'AzManLoginWebServicesSqlClrClient.pdb' ;
GO

/********************************************************************
 CREAR LOS RESPECTIVO CLR STORED PROCEDURES
********************************************************************/
CREATE PROCEDURE [dbo].[AzManLogin_Test]
   @input [nvarchar](255),
   @output [nvarchar](MAX) OUTPUT,
   @result [bit] OUTPUT,
   @message [nvarchar](MAX) OUTPUT
   WITH EXECUTE AS CALLER
AS EXTERNAL NAME 
   [AzManLoginWebServicesSqlClrClient].[AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures].[AzManLogin_Test]
GO

CREATE PROCEDURE [dbo].[AzManLogin_GetLogin]
   @token [nvarchar](255),
   @user [nvarchar](255),
   @appName [nvarchar](255),
   @userId [int] OUTPUT,
   @attributeString [nvarchar](MAX) OUTPUT,
   @loginStatus [int] OUTPUT,
   @result [bit] OUTPUT,
   @message [nvarchar](MAX) OUTPUT
   WITH EXECUTE AS CALLER
AS EXTERNAL NAME 
   [AzManLoginWebServicesSqlClrClient].[AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures].[AzManLogin_GetLogin]
GO

CREATE PROCEDURE [dbo].[AzManLogin_GetUserPropertyValueFromAttString]
   @attributeString [nvarchar](MAX),
   @userProperty [int],
   @propertyValue [nvarchar](4000) OUTPUT,
   @result [bit] OUTPUT,
   @message [nvarchar](MAX) OUTPUT
   WITH EXECUTE AS CALLER
AS EXTERNAL NAME 
   [AzManLoginWebServicesSqlClrClient].[AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures].[AzManLogin_GetUserPropertyValueFromAttString]
GO

CREATE PROCEDURE [dbo].[AzManLogin_CheckLoginAccess]
   @store [nvarchar](255),
   @app [nvarchar](255),
   @item [nvarchar](255),
   @loginId [nvarchar](255),
   @userName [nvarchar](255),
   @loginStatus [int] OUTPUT,
   @aut [int] OUTPUT,
   @result [bit] OUTPUT,
   @message [nvarchar](MAX) OUTPUT
   WITH EXECUTE AS CALLER
AS EXTERNAL NAME 
   [AzManLoginWebServicesSqlClrClient].[AzManLoginWebServicesSqlClrClient.LoginSvcStoredProcedures].[AzManLogin_CheckLoginAccess]
GO