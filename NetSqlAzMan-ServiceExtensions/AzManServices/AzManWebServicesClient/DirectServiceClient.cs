using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace AzManWebServicesClient
{
	public class DirectServiceClient
	{
		internal const string _AppSetting_WsUrl = "Basgosoft.NetSqlAzManCacheWS.Url";

		public static void AzMan_DirectGetDBUser(string userName, out int userID, out string attributeString) {
			const string striMemberName = "AzMan_DirectGetDBUser";
			string striSvcUrl = null;

			userID = -1;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings[_AppSetting_WsUrl].ToString();

				DirectSvcRef.DirectService c = new DirectSvcRef.DirectService();
				c.Url = striSvcUrl;

				DirectSvcRef.DBUser user = null;
				try {
					user = c.DirectGetDBUser(userName.ToString());
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
				throw new Exception(string.Format("Error en {0}.", striMemberName), ex);
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

		public static void AzMan_DirectValidatePassword(string userName, string password, out bool isValid, out string attributeString) {
			const string striMemberName = "AzMan_DirectValidatePassword";
			string striSvcUrl = string.Empty;

			isValid = false;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings[_AppSetting_WsUrl].ToString();

				DirectSvcRef.DirectService c = new DirectSvcRef.DirectService();

				c.Url = striSvcUrl;

				bool boolSpec1;
				try {
					c.DirectValidatePassword(userName, password, out isValid, out boolSpec1);
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

		public static void AzMan_DirectCheckAccess(string store, string app, string item, string dBUserSSID, DateTime validFor, bool operationsOnly, out int aut, out string attributeString) {
			const string striMemberName = "AzMan_DirectCheckAccess";
			string striSvcUrl = null;

			aut = -1;
			attributeString = null;

			try {
				striSvcUrl = ConfigurationManager.AppSettings[_AppSetting_WsUrl].ToString();

				DirectSvcRef.DirectService c = new DirectSvcRef.DirectService();

				c.Url = striSvcUrl;

				DirectSvcRef.AuthorizationType enumAut;
				bool boolAutSpec;
				try {
					c.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve(store, app, item, dBUserSSID, validFor, true, operationsOnly, true, null, out enumAut, out boolAutSpec);

					aut = Convert.ToInt32(enumAut);
				}
				catch (Exception ex) {
					attributeString = string.Concat(striMemberName, ":\n\r", ex.Message, "\n\r", ex.StackTrace);
				}
			}
			catch (Exception ex) {
				throw new Exception(string.Format("Error en procedimiento almacenado CLR {0}.", striMemberName), ex);
			}
		}
	}
}