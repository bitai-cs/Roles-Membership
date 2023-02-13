--GRANT  EXTERNAL ACCESS  ASSEMBLY  TO campus 
/********************************************************************************
 ¿QUÉ HACE ESTE SCRIPT?
 
 1ro) Elimina (si es que existe) cada uno de los procedimientos
      almacenados CLR que ejecuta un método en el assembly AzManWebServicesSqlClrClient.dll
 2do) Elimina (si es que existen en la base de datos) los assemblies
      AzManWebServicesClient.dll, AzManWebServicesClient.XmlSerializers.dll, 
      AzManWebServicesSqlClrClient.dll y los instala en la base de datos cargandolos 
      desde la ruta C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\
 3ro) Crea los procedimientos almacenados CLR que ejecuta c/u de los métodos
	  en el assembly AzManWebServicesSqlClrClient.dll
*********************************************************************************/

/*********************************************************************/
/* ELIMINAR SP CLR SI EXISTEN															*/
/*********************************************************************/
IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_DirectTest]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_DirectTest]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_Test]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_Test]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_DirectGetDBUser]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_DirectGetDBUser]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_DirectValidatePassword]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_DirectValidatePassword]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_DirectCheckAccess]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_DirectCheckAccess]
GO

IF EXISTS ( SELECT   *
            FROM     sys.objects
            WHERE    object_id = OBJECT_ID(N'[dbo].[AzMan_Util_GetPropertyValueFromAttString]')
                     AND type IN (N'P', N'PC') ) 
   DROP PROCEDURE [dbo].[AzMan_Util_GetPropertyValueFromAttString]
GO

/********************************************************************
 Eliminar los ensamblados .NET
********************************************************************/
IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManWebServicesSqlClrClient'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManWebServicesSqlClrClient]
GO

IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManWebServicesClient.XmlSerializers'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManWebServicesClient.XmlSerializers]
GO

IF EXISTS ( SELECT   *
            FROM     sys.assemblies asms
            WHERE    asms.name = N'AzManWebServicesClient'
                     AND is_user_defined = 1 ) 
   DROP ASSEMBLY [AzManWebServicesClient]
GO

/********************************************************************
 Registra los ensamblados .NET
********************************************************************/
CREATE ASSEMBLY [AzManWebServicesClient]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\AzManWebServicesClient.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

ALTER ASSEMBLY [AzManWebServicesClient]
DROP FILE ALL
ADD FILE FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\AzManWebServicesClient.pdb' AS N'AzManWebServicesClient.pdb' ;
GO

CREATE ASSEMBLY [AzManWebServicesClient.XmlSerializers]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\AzManWebServicesClient.XmlSerializers.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

CREATE ASSEMBLY [AzManWebServicesSqlClrClient]
AUTHORIZATION [dbo]
FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\AzManWebServicesSqlClrClient.dll'
WITH PERMISSION_SET = EXTERNAL_ACCESS
GO

ALTER ASSEMBLY [AzManWebServicesSqlClrClient]
DROP FILE ALL
ADD FILE FROM 'C:\Cliente NetSqlAzMan-ServiceExtensions\AzManWebServicesSqlClrClient\AzManWebServicesSqlClrClient.pdb' AS N'AzManWebServicesSqlClrClient.pdb' ;
GO

/********************************************************************
 Crear Sps CLR
********************************************************************/
CREATE PROCEDURE [dbo].[AzMan_Test]
   @input NVARCHAR(255),
   @result BIT OUTPUT,
   @output NVARCHAR(MAX) OUTPUT
AS EXTERNAL NAME 
   [AzManWebServicesSqlClrClient].[AzManWebServicesSqlClrClient.DirectSvcStoredProcedures].[AzMan_Test]
GO

CREATE PROCEDURE [dbo].[AzMan_DirectGetDBUser]
   @userName NVARCHAR(255),
   @userID INT OUTPUT,
   @attributeString NVARCHAR(MAX) OUTPUT
AS EXTERNAL NAME 
   [AzManWebServicesSqlClrClient].[AzManWebServicesSqlClrClient.DirectSvcStoredProcedures].[AzMan_DirectGetDBUser]
GO

CREATE PROCEDURE [dbo].[AzMan_DirectValidatePassword]
   @userName NVARCHAR(255),
   @password NVARCHAR(255),
   @isValid BIT OUTPUT,
   @attributeString NVARCHAR(MAX) OUTPUT
AS EXTERNAL NAME 
   [AzManWebServicesSqlClrClient].[AzManWebServicesSqlClrClient.DirectSvcStoredProcedures].[AzMan_DirectValidatePassword]
GO

CREATE PROCEDURE [dbo].[AzMan_DirectCheckAccess]
   @store NVARCHAR(255),
   @app NVARCHAR(255),
   @item NVARCHAR(4000),
   @dBUserSSID NVARCHAR(4000),
   @validFor DATETIME,
   @operationsOnly BIT,
   @aut INT OUTPUT,
   @attributeString NVARCHAR(4000) OUTPUT
AS EXTERNAL NAME 
   [AzManWebServicesSqlClrClient].[AzManWebServicesSqlClrClient.DirectSvcStoredProcedures].[AzMan_DirectCheckAccess]
GO

CREATE PROCEDURE [dbo].[AzMan_Util_GetPropertyValueFromAttString]
   @attributeString NVARCHAR(MAX),
   @propertyName NVARCHAR(255),
   @propertyValue NVARCHAR(4000) OUTPUT
AS EXTERNAL NAME 
   [AzManWebServicesSqlClrClient].[AzManWebServicesSqlClrClient.DirectSvcStoredProcedures].[AzMan_Util_GetPropertyValueFromAttString]
