using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using NetSqlAzMan.DirectoryServices;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan {
	/// <summary>
	/// CUSTOM fields and methods
	/// </summary>
	public partial class SqlAzManStorage {
		#region Private methods
		internal Dictionary<string, object> getCustomColumnsFromDataRow(DataRow row) {
			Dictionary<string, object> _customColumns = new Dictionary<string, object>();
			foreach (DataColumn dc in row.Table.Columns) {
				if (String.Compare(dc.ColumnName, "DBUserSid", true) != 0
					 &&
					 String.Compare(dc.ColumnName, "DBUserName", true) != 0 && String.Compare(dc.ColumnName, "FullName", true) != 0) {
					_customColumns.Add(dc.ColumnName, row[dc]);
				}
			}

			return _customColumns;
		}

		internal Dictionary<string, object> getCustomColumnsFromQueryble(LINQ.GetDBUsersResult record) {
			Dictionary<string, object> _customColumns = new Dictionary<string, object>();
			_customColumns.Add("UserID", record.UserID);
			_customColumns.Add("Password", record.Password);
			_customColumns.Add("FirstName", record.FirstName);
			_customColumns.Add("LastName", record.LastName);
			_customColumns.Add("Mail", record.Mail);
			_customColumns.Add("DepartmentId", record.DepartmentId);
			_customColumns.Add("DepartmentName", record.DepartmentName);
			_customColumns.Add("BranchOfficeIds", record.BranchOfficeIds);
			_customColumns.Add("BranchOfficeNames", record.BranchOfficeNames);
			_customColumns.Add("RelativeBranchOfficeIds", record.RelativeBranchOfficeIds);
			_customColumns.Add("Enabled", record.Enabled);
			return _customColumns;
		}

		private AuthorizationType internalCheckAccessLDAP(string StoreName, string ApplicationName, string ItemName, string DomainProfile, string User, string Sid, DateTime ValidFor, bool OperationsOnly, bool retrieveAttributes, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			LDAPHelperSvcRef.LDAPHelperClient _wsc = new LDAPHelperSvcRef.LDAPHelperClient(LDAPServices.LDAPConfigUtils.GetWebSvcEndpointByDomainProfile(this.ConnectionString, DomainProfile));
			LDAPHelperSvcRef.LDAPSearchResult[] _sr;
			LDAPHelperSvcRef.StatusInfo _si;

			//Buscar usuario
			if (!_wsc.SearchBySid(out _sr, out _si, DomainProfile, Sid, false)) {
				throw new Exception(string.Format("{0}:{1}\n\r{2}", _si.ExceptionType, _si.StatusMessage, _si.StackTrace));
			}

			if (_sr == null)
				throw new Exception("No se pudo encontrar el identificador del usuario en el servicio de directorio.");

			NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult _userObject = _sr[0];
			if (!_userObject.samAccountName.ToLower().Equals(User.ToLower()))
				throw new Exception("Las credenciales del usuario son incoherentes.");

			List<byte> _tokenList = new List<byte>();

			//Get bytes from Sid Object
			SecurityIdentifier _secId = new SecurityIdentifier(_userObject.objectSid);
			byte[] _sidBytes = new byte[_secId.BinaryLength];
			_secId.GetBinaryForm(_sidBytes, 0);
			_tokenList.AddRange(_sidBytes);

			//El usuario pertenece a grupos
			int userGroupsCount = 0;
			if (_userObject.memberOf != null && _userObject.memberOf.Count() > 0) {
				userGroupsCount = _userObject.memberOf.Count();
				NetSqlAzMan.LDAPHelperSvcRef.LDAPSearchResult _groupObject;
				foreach (string _groupCn in _userObject.memberOf) {
					if (_wsc.SearchUsersAndGroups(out _sr, out _si, DomainProfile, LDAPHelperSvcRef.LDAPHelperADProperties.distinguishedName, _groupCn, true)) {
						_groupObject = _sr[0];
						_secId = new SecurityIdentifier(_groupObject.objectSid);
						_tokenList.AddRange(NetSqlAzMan.SqlAzManItem.getSqlBinarySid(_secId));
					}
					else {
						throw new Exception(string.Format("{0}:{1}\n\r{2}", _si.ExceptionType, _si.StatusMessage, _si.StackTrace));
					}
				}
			}

			SqlConnection conn = new SqlConnection(this.db.Connection.ConnectionString);
			conn.Open();
			DataSet checkAccessResults = new DataSet();
			System.Data.SqlClient.SqlCommand cmd = new SqlCommand("dbo.netsqlazman_DirectCheckAccess", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@STORENAME", StoreName);
			cmd.Parameters.AddWithValue("@APPLICATIONNAME", ApplicationName);
			cmd.Parameters.AddWithValue("@ITEMNAME", ItemName);
			cmd.Parameters.AddWithValue("@OPERATIONSONLY", OperationsOnly);
			cmd.Parameters.AddWithValue("@TOKEN", _tokenList.ToArray());
			cmd.Parameters.AddWithValue("@USERGROUPSCOUNT", (object)userGroupsCount);
			cmd.Parameters.AddWithValue("@VALIDFOR", ValidFor);
			cmd.Parameters.AddWithValue("@LDAPPATH", DirectoryServicesUtils.rootDsePath);
			cmd.Parameters.AddWithValue("@RETRIEVEATTRIBUTES", retrieveAttributes);
			System.Data.SqlClient.SqlParameter pAuthorizationType = new System.Data.SqlClient.SqlParameter("@AUTHORIZATION_TYPE", (object)0);
			pAuthorizationType.Direction = System.Data.ParameterDirection.InputOutput;
			cmd.Parameters.Add(pAuthorizationType);
			//BizRule Check CallBack
			SqlDataAdapter checkAccessPartialResultsDataAdapter = new SqlDataAdapter(cmd);
			try {
				checkAccessPartialResultsDataAdapter.Fill(checkAccessResults);
			}
			catch (SqlException sqlex) {
				if (sqlex.Message.StartsWith("Store not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.StoreNotFoundException(StoreName, sqlex);
				}
				else if (sqlex.Message.StartsWith("Application not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.ApplicationNotFoundException(ApplicationName, StoreName, sqlex);
				}
				else if (sqlex.Message.StartsWith("Item not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.ItemNotFoundException(ItemName, StoreName, ApplicationName, sqlex);
				}
				else {
					throw SqlAzManException.GenericException(sqlex);
				}
			}
			finally {
				conn.Close();
			}
			DataTable checkAccessPartialResultsDataTable = checkAccessResults.Tables[0];
			DataTable checkAccessAttributesDataTable = (retrieveAttributes ? checkAccessResults.Tables[1] : new DataTable());
			AuthorizationType result;
			if (checkAccessPartialResultsDataTable.Select("BizRuleId IS NOT NULL").Length == 0) {
				//No business rules to check ... just return check access authorizationType
				result = (AuthorizationType)((int)pAuthorizationType.Value);
			}
			else {
				//Transform DataRows into a Hierarchical Tree of Node(s)
				IAzManApplication application = this[StoreName][ApplicationName];
				ItemNode itemNode = this.buildTreeOfNodes(application, checkAccessPartialResultsDataTable, ItemName);
				//Execute Biz Rules and Cut Nodes that returns false
				Hashtable ctxParameters = new Hashtable();
				if (contextParameters != null) {
					foreach (KeyValuePair<string, object> kv in contextParameters) {
						ctxParameters.Add(kv.Key, kv.Value);
					}
				}
				//itemNode = this.executeBizRules(itemNode, new SqlAzManSID(windowsIdentity.User), ctxParameters);
				itemNode = this.executeBizRules(itemNode, new SqlAzManSID(Sid), ctxParameters);
				//Compute final CheckAccess authorizationType
				result = this.computeCheckAccessResult(itemNode, ref checkAccessAttributesDataTable);
				if (result == AuthorizationType.Deny || result == AuthorizationType.Neutral) {
					checkAccessAttributesDataTable = new DataTable(); //no attributes
				}
				//else
				//{
				//    DataRow[] rowWithBizRules = checkAccessPartialResultsDataTable.Select("ItemId=" + itemNode.azmanItem.ItemId.ToString());
				//    foreach (DataRow dr in rowWithBizRules)
				//    {
				//        dr["AuthorizationType"] = (int)result;
				//    }
				//    checkAccessAttributesDataTable.AcceptChanges();
				//}
			}
			//Populate Attributes authorizationType
			if (retrieveAttributes) {
				attributes = new List<KeyValuePair<string, string>>(checkAccessAttributesDataTable.Rows.Count);
				foreach (DataRow dr in checkAccessAttributesDataTable.Rows) {
					KeyValuePair<string, string> kvp = new KeyValuePair<string, string>((string)dr[0], (string)dr[1]);
					if (!attributes.Contains(kvp)) {
						if (dr["ItemId"] == DBNull.Value) {
							if (result == AuthorizationType.Allow || result == AuthorizationType.AllowWithDelegation)
								attributes.Add(kvp);
						}
						else if (dr["ItemId"] != DBNull.Value) {
							int itemId = (int)dr["ItemId"];
							DataRow[] drItems = checkAccessPartialResultsDataTable.Select("ItemId=" + itemId.ToString());
							if (drItems.Length > 0) {
								DataRow drItem = drItems[0];
								AuthorizationType auth = (AuthorizationType)drItem["AuthorizationType"];
								if (auth == AuthorizationType.Allow || auth == AuthorizationType.AllowWithDelegation) {
									attributes.Add(kvp);
								}
								else if (auth == AuthorizationType.Neutral) {
									var store = this.Stores[StoreName];
									var application = store.Applications[ApplicationName];
									var items = application.Items;
									var itemValues = items.Values.ToArray();
									AuthorizationType authParent = this.getParentResult(drItem, checkAccessPartialResultsDataTable, itemValues);
									if (authParent == AuthorizationType.Allow || authParent == AuthorizationType.AllowWithDelegation) {
										attributes.Add(kvp);
									}
								}
							}
						}
					}
				}
			}
			else {
				attributes = new List<KeyValuePair<string, string>>();
			}
			return result;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Finds the DB user.
		/// </summary>
		/// <param name="customSid">The custom sid.</param>
		/// <returns></returns>
		public IAzManDBUser GetDBUser(IAzManSid customSid) {
			var dtDBUsers = this.db.GetDBUsers(null, null, customSid.BinaryValue, null);
			IAzManDBUser result;
			if (dtDBUsers.Count() == 0) {
				throw SqlAzManException.DBUserNotFoundException(customSid.StringValue, null);
			}
			else {
				LINQ.GetDBUsersResult _record = dtDBUsers.First();
				Dictionary<string, object> _customCols = getCustomColumnsFromQueryble(_record);
				result = new SqlAzManDBUser(new SqlAzManSID(_record.DBUserSid.ToArray(), true), _record.DBUserName, _record.FullName, _customCols);
			}
			return result;
		}
		/// <summary>
		/// Finds the DB user.
		/// </summary>
		/// <param name="userName">The custom sid.</param>
		/// <returns></returns>
		public IAzManDBUser GetDBUser(string userName) {
			var dtDBUsers = this.db.GetDBUsersEx(null, null, null, userName);
			IAzManDBUser result;
			if (dtDBUsers.Rows.Count == 0) {
				throw SqlAzManException.DBUserNotFoundException(userName, null);
			}
			else {
				Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
				result = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);
			}
			return result;
		}

		/// <summary>
		/// Gets the DB users.
		/// </summary>
		/// <returns></returns>
		public IAzManDBUser[] GetDBUsers() {
			var dtDBUsers = this.db.GetDBUsersEx(null, null, null, null);
			IAzManDBUser[] result = new IAzManDBUser[dtDBUsers.Rows.Count];
			for (int i = 0; i < dtDBUsers.Rows.Count; i++) {
				Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[i]);
				result[i] = new SqlAzManDBUser(dtDBUsers.Rows[i], _customCols);
			}
			return result;
		}

		public IAzManDBUser GetAzManUser(IAzManSid customSid, bool dummy) {
			IQueryable<LINQ.GetDBUsersResult> dtDBUsers = this.db.GetDBUsers(null, null, customSid.BinaryValue, null);
			IAzManDBUser result;
			if (dtDBUsers.Count() == 0) {
				throw SqlAzManException.DBUserNotFoundException(customSid.StringValue, null);
			}
			else {
				LINQ.GetDBUsersResult _record = dtDBUsers.First();
				Dictionary<string, object> _customCols = getCustomColumnsFromQueryble(_record);
				result = new SqlAzManDBUser(new SqlAzManSID(_record.DBUserSid.ToArray(), true), _record.DBUserName, _record.FullName, _customCols);
			}
			return result;
		}

		public bool GetAzManDBUser(string userName, out IAzManDBUser user, out Exception hex) {
			user = null;
			hex = null;
			try {
				var dtDBUsers = this.db.GetDBUsersEx(null, null, null, userName);

				if (dtDBUsers.Rows.Count == 0) {
					throw SqlAzManException.DBUserNotFoundException(userName, null);
				}
				else {
					Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
					user = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);
				}

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		public IAzManDBUser[] GetAzManUsers(bool dummy) {
			var dtDBUsers = this.db.GetDBUsersEx(null, null, null, null);
			IAzManDBUser[] result = new IAzManDBUser[dtDBUsers.Rows.Count];
			for (int i = 0; i < dtDBUsers.Rows.Count; i++) {
				Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[i]);
				result[i] = new SqlAzManDBUser(dtDBUsers.Rows[i], _customCols);
			}
			return result;
		}

		/// <summary>
		/// VBastidas: Finds the DB user with Password
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		[Obsolete("Usar el metodo GetDBUser")]
		public IAzManDBUser GetDBUserWithPassword(string userName) {
			var _datbDBUsers = this.db.GetDBUsersWithPasswordsEx(null, null, null, userName);
			IAzManDBUser result;
			if (_datbDBUsers.Rows.Count == 0) {
				throw SqlAzManException.DBUserNotFoundException(userName, null);
			}
			else {
				Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(_datbDBUsers.Rows[0]);
				result = new SqlAzManDBUser(_datbDBUsers.Rows[0], _customCols);
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="azManUser"></param>
		/// <param name="exceptionType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>		
		public bool GetDBUserByValidatingPassword(string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace) {
			azManUser = null;
			statusMessage = null;
			exceptionType = null;
			stackTrace = null;
			try {
				var dtDBUsers = this.db.GetDBUsersWithPasswordsEx(null, null, null, userName);

				if (dtDBUsers.Rows.Count == 0)
					throw new MissingMemberException("No se encontró el usuario en la datos.");

				if (!dtDBUsers.Rows[0]["Password"].ToString().Equals(password))
					throw new AuthenticationException("Usuario y/o contraseña incorrecta.");

				Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
				azManUser = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);

				return true;
			}
			catch (Exception ex) {
				exceptionType = ex.GetType().FullName;
				statusMessage = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainProfile"></param>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="azManUser"></param>
		/// <param name="exceptionType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>		
		public bool GetLDAPUserByValidatingPassword(string domainProfile, string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace) {
			azManUser = null;
			exceptionType = null;
			statusMessage = null;
			stackTrace = null;
			try {
				LDAPHelperSvcRef.LDAPHelperClient _wsc = new LDAPHelperSvcRef.LDAPHelperClient(LDAPServices.LDAPConfigUtils.GetWebSvcEndpointByDomainProfile(this.ConnectionString, domainProfile));
				bool _isAuthenticated;
				LDAPHelperSvcRef.LDAPSearchResult _userProperties;
				LDAPHelperSvcRef.StatusInfo _statusInfo;
				bool _result = _wsc.AuthenticateUser(out _isAuthenticated, out _userProperties, out _statusInfo, domainProfile, userName, password, true);

				if (!_result) {
					exceptionType = _statusInfo.ExceptionType;
					statusMessage = _statusInfo.StatusMessage;
					stackTrace = _statusInfo.StackTrace;
					return false;
				}

				if (!_isAuthenticated)
					throw new Exception("Usuario y/o contraseña incorrectos.");

				azManUser = new SqlAzManDBUser(true, new SqlAzManSID(_userProperties.objectSid), _userProperties.DomainProfile, _userProperties.samAccountName, _userProperties.displayName, new Dictionary<string, object>());

				return true;
			}
			catch (Exception ex) {
				exceptionType = ex.GetType().FullName;
				statusMessage = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="azManUser"></param>
		/// <param name="exceptionType"></param>
		/// <param name="statusMessage"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public bool GetLDAPUser(string ldapDomain, string userName, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace) {
			azManUser = null;
			exceptionType = null;
			statusMessage = null;
			stackTrace = null;
			try {
				string _endPointName = LDAPServices.LDAPConfigUtils.GetWebSvcEndpointByDomainProfile(this.ConnectionString, ldapDomain);
				LDAPHelperSvcRef.LDAPHelperClient _wsc = new LDAPHelperSvcRef.LDAPHelperClient(_endPointName);
				LDAPHelperSvcRef.LDAPSearchResult[] _searchResult;
				LDAPHelperSvcRef.StatusInfo _statusInfo;
				var _result = _wsc.SearchUsersAndGroups(out _searchResult, out _statusInfo, ldapDomain, LDAPHelperSvcRef.LDAPHelperADProperties.samAccountName, userName, true);
				if (!_result) {
					exceptionType = _statusInfo.ExceptionType;
					statusMessage = _statusInfo.StatusMessage;
					stackTrace = _statusInfo.StackTrace;
					return false;
				}

				if (_searchResult == null || _searchResult.GetLength(0).Equals(0))
					throw new ActiveDirectoryObjectNotFoundException("El usuario no es válido o no existe.");
				if (_searchResult.GetLength(0) > 1) /*Se encontró mas de un objeto*/
					throw new ActiveDirectoryObjectNotFoundException("No se pudo obtener información especifica del usuario en el dominio. Se obtuvieron múltiples resultados.");

				LDAPHelperSvcRef.LDAPSearchResult _userProperties = _searchResult[0];

				azManUser = new SqlAzManDBUser(true, new SqlAzManSID(_userProperties.objectSid), _userProperties.DomainProfile, _userProperties.samAccountName, _userProperties.displayName, new Dictionary<string, object>());

				return true;
			}
			catch (Exception ex) {
				exceptionType = ex.GetType().FullName;
				statusMessage = ex.Message;
				stackTrace = ex.StackTrace;
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="storeName"></param>
		/// <param name="applicationName"></param>
		/// <param name="itemName"></param>
		/// <param name="ldapDomain"></param>
		/// <param name="userName"></param>
		/// <param name="sid"></param>
		/// <param name="validFor"></param>
		/// <param name="operationsOnly"></param>
		/// <param name="contextParameters"></param>
		/// <returns></returns>
		public AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string ldapDomain, string userName, string sid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters) {
			List<KeyValuePair<string, string>> attributes;
			return this.internalCheckAccessLDAP(storeName, applicationName, itemName, ldapDomain, userName, sid, validFor, operationsOnly, false, out attributes, contextParameters);
		}

		public AuthorizationType CheckAccessLDAP(string StoreName, string ApplicationName, string ItemName, string LDAPDomain, string User, string Sid, DateTime ValidFor, bool OperationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			return this.internalCheckAccessLDAP(StoreName, ApplicationName, ItemName, LDAPDomain, User, Sid, ValidFor, OperationsOnly, true, out attributes, contextParameters);
		}

		public bool ChangeDBUserPassword(NetSqlAzMan.Cache.DBUser user, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace) {
			SqlConnection conn = new SqlConnection(this.db.Connection.ConnectionString);

			exceptionType = null;
			statusMessage = null;
			stackTrace = null;

			try {
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.[identity_sp_ChangeDBUserPassword]", conn) {
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.AddWithValue("@userId", user.UserID);
				cmd.Parameters.AddWithValue("@currentPassword", currentPassword);
				cmd.Parameters.AddWithValue("@renewedPassword", renewedPassword);
				cmd.Parameters.AddWithValue("@passwordConfirmation", passwordConfirmation);

				cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex) {
				exceptionType = ex.GetType().FullName;
				statusMessage = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
			finally {
				if (conn.State == ConnectionState.Open)
					conn.Close();
			}
		}

		public bool ChangeDBUserPasswordById(int userId, string currentPassword, string renewedPassword, string passwordConfirmation, out string exceptionType, out string statusMessage, out string stackTrace) {
			SqlConnection conn = new SqlConnection(this.db.Connection.ConnectionString);

			exceptionType = null;
			statusMessage = null;
			stackTrace = null;

			try {
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.[identity_sp_ChangeDBUserPassword]", conn) {
					CommandType = CommandType.StoredProcedure
				};
				cmd.Parameters.AddWithValue("@userId", userId);
				cmd.Parameters.AddWithValue("@currentPassword", currentPassword);
				cmd.Parameters.AddWithValue("@renewedPassword", renewedPassword);
				cmd.Parameters.AddWithValue("@passwordConfirmation", passwordConfirmation);

				cmd.ExecuteNonQuery();

				return true;
			}
			catch (Exception ex) {
				exceptionType = ex.GetType().FullName;
				statusMessage = ex.Message;
				stackTrace = ex.StackTrace;

				return false;
			}
			finally {
				if (conn.State == ConnectionState.Open)
					conn.Close();
			}
		}
		#endregion
	}
}
