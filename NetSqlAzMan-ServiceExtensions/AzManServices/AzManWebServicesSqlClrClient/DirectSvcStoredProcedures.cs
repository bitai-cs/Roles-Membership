using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace AzManWebServicesSqlClrClient
{
	/// <summary>
	/// Conjunto de Stored Procedures para consulta al servicio web de seguridad
	/// </summary>
	public static partial class DirectSvcStoredProcedures
	{
		internal const string APPSETTINGKEY_WsUrl = "AzManWebServices.DirectService.Url";

		/// <summary>
		/// Metodo de prueba 
		/// </summary>
		/// <param name="input">Valor a enviar</param>
		/// <param name="result">Estado de la operación</param>
		/// <param name="output">Respuesta del servicio web</param>
		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzMan_Test(SqlString input, out SqlBoolean result, out SqlString output) {
			const string striMemberName = "AzMan_Test";
			string striSvcUrl = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl).ToString();

				AzManWebServicesClient.DirectSvcRef.DirectService _svc = new AzManWebServicesClient.DirectSvcRef.DirectService();
				_svc.Url = striSvcUrl;

				bool _result;
				bool _dummy1;
				string _output;
				_svc.Test(input.IsNull ? "NULL" : input.ToString(), out _result, out _dummy1, out _output);
				result = _result;
				output = _output;
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error en procedimiento almacenado CLR {0}.", striMemberName), ex);
			}
		}

		/// <summary>
		/// Obtiene la informacion del usuario desde el Web Service de Seguridad
		/// </summary>
		/// <param name="userName">Usuario</param>
		/// <param name="userID">Retorna el id del usuario</param>
		/// <param name="attributeString">Retorna los atributos concatenados del usuario</param>
		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzMan_DirectGetDBUser(SqlString userName, out SqlInt32 userID, out SqlString attributeString) {
			const string striMemberName = "AzMan_DirectGetDBUser";
			string striSvcUrl = null;

			userID = -1;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl).ToString();

				AzManWebServicesClient.DirectSvcRef.DirectService _svc = new AzManWebServicesClient.DirectSvcRef.DirectService();
				_svc.Url = striSvcUrl;

				AzManWebServicesClient.DirectSvcRef.DBUser user = null;
				try {
					user = _svc.DirectGetDBUser(userName.ToString());
				}
				catch (Exception ex) {
					attributeString = string.Concat(striMemberName, ":\n\r", ex.Message, "\n\r", ex.StackTrace);
				}
				finally {
					if (user != null) {
						userID = user.UserID;
						attributeString = user.AttributeString;
					}
				}
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error en procedimiento almacenado CLR {0}.", striMemberName), ex);
			}
			/*
			 DECLARE @return_value INT,
	@userID NVARCHAR(4000),
	@attributeString NVARCHAR(4000)
	
	EXEC @return_value = [dbo].[AzMan_GetDBUser] @userName = N'ADMIaN', @userID = @userID OUTPUT,
	@attributeString = @attributeString OUTPUT ;

	SELECT   @return_value AS N'@return_value',
			@userID AS N'@userID',
			@attributeString AS N'@attributeString' ;
			 */
		}

		/// <summary>
		/// Realiza la comprobacion de la contraseña de un usuario
		/// </summary>
		/// <param name="userName">Usuario</param>
		/// <param name="password">Contraseña del usuario a validar</param>
		/// <param name="isValid">Retorna la validez de la contraseña</param>
		/// <param name="attributeString">Retorna la descripción del error</param>
		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzMan_DirectValidatePassword(SqlString userName, SqlString password, out SqlBoolean isValid, out SqlString attributeString) {
			const string striMemberName = "AzMan_DirectValidatePassword";
			string striSvcUrl = null;

			isValid = false;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl).ToString();

				AzManWebServicesClient.DirectSvcRef.DirectService _svc = new AzManWebServicesClient.DirectSvcRef.DirectService();
				_svc.Url = striSvcUrl;

				bool boolIsValid, boolSpec1;
				try {
					_svc.DirectValidatePassword(userName.ToString(), password.ToString(), out boolIsValid, out boolSpec1);
					isValid = boolIsValid;
				}
				catch (Exception ex) {
					attributeString = string.Concat(striMemberName, ":\n\r", ex.Message, "\n\r", ex.StackTrace);
				}
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error en procedimiento almacenado CLR {0}.", striMemberName), ex);
			}
			/*
			 DECLARE @return_value INT,
	@isValid BIT,
	@attributeString NVARCHAR(4000)

	EXEC @return_value = [dbo].[AzMan_ValidatePassword] @userName = N'ADMsIN', @password = N'1887011981',
	@isValid = @isValid OUTPUT, @attributeString = @attributeString OUTPUT

	SELECT   @return_value AS N'@return_value',
			@isValid AS N'@isValid',
			@attributeString AS N'@attributeString'
			 */
		}

		/// <summary>
		/// Verifica el tipo de acceso que el usuario tiene a un item
		/// </summary>
		/// <param name="store">Nombre del NetSqlAzMan Store</param>
		/// <param name="app">Nombre del NetSqlAzMan Application</param>
		/// <param name="item">Nombre del NetSqlAzMan item</param>
		/// <param name="dBUserSSID">Nombre del usuario</param>
		/// <param name="validFor">Fecha para validar si el permiso esta vigente (en caso se hubiera establecido un rango de vigencia)</param>
		/// <param name="operationsOnly">Solo items de tipo NetSqlAzMan Operation</param>
		/// <param name="aut">Retorna el id del NetSqlAzMan AuthorizationType</param>
		/// <param name="attributeString">Retorna información del proceso cuando aut es -1</param>
		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzMan_DirectCheckAccess(SqlString store, SqlString app, SqlString item, SqlString dBUserSSID, SqlDateTime validFor, SqlBoolean operationsOnly, out SqlInt32 aut, out SqlString attributeString) {
			const string striMemberName = "AzMan_DirectCheckAccess";
			string striSvcUrl = null;

			aut = -1;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings(APPSETTINGKEY_WsUrl).ToString();

				AzManWebServicesClient.DirectSvcRef.DirectService _svc = new AzManWebServicesClient.DirectSvcRef.DirectService();
				_svc.Url = striSvcUrl;

				AzManWebServicesClient.DirectSvcRef.AuthorizationType enumAut;
				bool boolAutSpec;
				try {
					_svc.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve(store.Value, app.Value, item.Value, dBUserSSID.Value, validFor.Value, true, operationsOnly.Value, true, null, out enumAut, out boolAutSpec);

					aut = new SqlInt32(Convert.ToInt32(enumAut));
				}
				catch (Exception ex) {
					attributeString = string.Concat(striMemberName, ":\n\r", ex.Message, "\n\r", ex.StackTrace);
				}
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error en procedimiento almacenado CLR {0}.", striMemberName), ex);
			}
		}

		/// <summary>
		/// Obtiene el valor de una propiedad desde la cadena de atributos
		/// </summary>
		/// <param name="attributeString">Cadena de atributos</param>
		/// <param name="propertyName">Propiedad de la que se quiere saver su valor</param>
		/// <param name="propertyValue">Restorna el valor de la propiedad</param>
		[Microsoft.SqlServer.Server.SqlProcedure]
		public static void AzMan_Util_GetPropertyValueFromAttString(SqlString attributeString, SqlString propertyName, out SqlString propertyValue) {
			propertyValue = null;
			string[] propArray = attributeString.Value.Split(Convert.ToChar(124));
			foreach (string pair in propArray) {
				string[] pairArray = pair.Split(Convert.ToChar(61));
				if (pairArray[0].ToLower().Equals(propertyName.Value.ToLower())) {
					propertyValue = pairArray[1];
					return;
				}
			}
			/*
	DECLARE @return_value INT,
	@userID INT,
	@attributeString NVARCHAR(4000);
   
	EXEC @return_value = [dbo].[AzMan_DirectGetDBUser] @userName = N'RAGUIRRE', @userID = @userID OUTPUT,
	@attributeString = @attributeString OUTPUT ;

	SELECT   @userID AS N'@userID',
			@attributeString AS N'@attributeString' ;

	DECLARE @propertyValue NVARCHAR(4000);
	EXEC @return_value = [dbo].[AzMan_Util_GetPropertyValueFromAttString] @attributeString = @attributeString,
	@propertyName = N'fUlLNaMe', @propertyValue = @propertyValue OUTPUT ;

	SELECT   @propertyValue AS N'@propertyValue' ;

	EXEC @return_value = [dbo].[AzMan_Util_GetPropertyValueFromAttString] @attributeString = @attributeString,
	@propertyName = N'BranchOFFICEids', @propertyValue = @propertyValue OUTPUT ;

	SELECT   @propertyValue AS N'@propertyValue' ;

	EXEC @return_value = [dbo].[AzMan_Util_GetPropertyValueFromAttString] @attributeString = @attributeString,
	@propertyName = N'BranchOFFICENAMes', @propertyValue = @propertyValue OUTPUT ;

	SELECT   @propertyValue AS N'@propertyValue' ;			
			 */
		}

		internal static class ConfigurationManager
		{
			internal static string AppSettings(string key) {
				int _firstcall = System.Configuration.ConfigurationManager.ConnectionStrings.Count;

				return System.Configuration.ConfigurationManager.AppSettings[key];
			}
		}
	}
}