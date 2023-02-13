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
using System.Threading.Tasks;
using NetSqlAzMan.DirectoryServices;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
	/// <summary>
	/// CUSTOM fields and methods
	/// </summary>
	public partial class SqlAzManStorage
	{
		#region Private methods	
		private AuthorizationType internalCheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string sid, byte[] binarySid, DateTime validFor, bool operationsOnly, bool retrieveAttributes, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			var _bl = new NetSqlAzMan.CustomBussinessLogic.LdapWebApiBusinessFactory();
			var _entries = Task.Run(() => _bl.SearchGroupMembershipAsync(domainProfile, true, 0, LdapHelperDTO.KeyEntryAttribute.objectSid, sid, LdapHelperDTO.RequiredEntryAttributes.OnlyObjectSid)).Result;

			List<byte> _tokenList = new List<byte>();

			//El usuario pertenece a grupos
			int userGroupsCount = _entries.Count();
			if (userGroupsCount > 0) {
				_tokenList.AddRange(NetSqlAzMan.ServiceBusinessObjects.AzManItem.GetSqlBinarySid(binarySid));

				foreach (var _groupEntry in _entries) {
					_tokenList.AddRange(NetSqlAzMan.ServiceBusinessObjects.AzManItem.GetSqlBinarySid(_groupEntry.objectSidBytes));
				}
			}
			else {
				_tokenList.AddRange(binarySid);
			}

			SqlConnection _conn = new SqlConnection(this.db.Connection.ConnectionString);
			_conn.Open();
			DataSet _checkAccessResults = new DataSet();
			System.Data.SqlClient.SqlCommand _cmd = new SqlCommand("dbo.netsqlazman_DirectCheckAccess", _conn);
			_cmd.CommandType = CommandType.StoredProcedure;
			_cmd.Parameters.AddWithValue("@STORENAME", storeName);
			_cmd.Parameters.AddWithValue("@APPLICATIONNAME", applicationName);
			_cmd.Parameters.AddWithValue("@ITEMNAME", itemName);
			_cmd.Parameters.AddWithValue("@OPERATIONSONLY", operationsOnly);
			_cmd.Parameters.AddWithValue("@TOKEN", _tokenList.ToArray());
			_cmd.Parameters.AddWithValue("@USERGROUPSCOUNT", (object)userGroupsCount);
			_cmd.Parameters.AddWithValue("@VALIDFOR", validFor);
			_cmd.Parameters.AddWithValue("@LDAPPATH", DirectoryServicesUtils.rootDsePath);
			_cmd.Parameters.AddWithValue("@RETRIEVEATTRIBUTES", retrieveAttributes);
			System.Data.SqlClient.SqlParameter pAuthorizationType = new System.Data.SqlClient.SqlParameter("@AUTHORIZATION_TYPE", (object)0);
			pAuthorizationType.Direction = System.Data.ParameterDirection.InputOutput;
			_cmd.Parameters.Add(pAuthorizationType);
			//BizRule Check CallBack
			SqlDataAdapter checkAccessPartialResultsDataAdapter = new SqlDataAdapter(_cmd);
			try {
				checkAccessPartialResultsDataAdapter.Fill(_checkAccessResults);
			}
			catch (SqlException sqlex) {
				if (sqlex.Message.StartsWith("Store not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.StoreNotFoundException(storeName, sqlex);
				}
				else if (sqlex.Message.StartsWith("Application not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.ApplicationNotFoundException(applicationName, storeName, sqlex);
				}
				else if (sqlex.Message.StartsWith("Item not found", StringComparison.CurrentCultureIgnoreCase)) {
					throw SqlAzManException.ItemNotFoundException(itemName, storeName, applicationName, sqlex);
				}
				else {
					throw SqlAzManException.GenericException(sqlex);
				}
			}
			finally {
				_conn.Close();
			}
			DataTable checkAccessPartialResultsDataTable = _checkAccessResults.Tables[0];
			DataTable checkAccessAttributesDataTable = (retrieveAttributes ? _checkAccessResults.Tables[1] : new DataTable());
			AuthorizationType result;
			if (checkAccessPartialResultsDataTable.Select("BizRuleId IS NOT NULL").Length == 0) {
				//No business rules to check ... just return check access authorizationType
				result = (AuthorizationType)((int)pAuthorizationType.Value);
			}
			else {
				//Transform DataRows into a Hierarchical Tree of Node(s)
				IAzManApplication application = this[storeName][applicationName];
				ItemNode itemNode = this.buildTreeOfNodes(application, checkAccessPartialResultsDataTable, itemName);
				//Execute Biz Rules and Cut Nodes that returns false
				Hashtable ctxParameters = new Hashtable();
				if (contextParameters != null) {
					foreach (KeyValuePair<string, object> kv in contextParameters) {
						ctxParameters.Add(kv.Key, kv.Value);
					}
				}
				//itemNode = this.executeBizRules(itemNode, new SqlAzManSID(windowsIdentity.User), ctxParameters);
				itemNode = this.executeBizRules(itemNode, new SqlAzManSID(sid), ctxParameters);
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
									var store = this.Stores[storeName];
									var application = store.Applications[applicationName];
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

		private AuthorizationType internalCheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, DateTime validFor, bool operationsOnly, bool retrieveAttributes, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			IAzManDBUser _azUser;
			Exception _exce;
			if (!this.GetLDAPUser(domainProfile, userName, out _azUser, out _exce))
				throw _exce;

			return internalCheckAccessLDAP(storeName, applicationName, itemName, domainProfile, _azUser.CustomSid.StringValue, _azUser.CustomSid.BinaryValue, validFor, operationsOnly, retrieveAttributes, out attributes, contextParameters);
		}

		//private AuthorizationType internalCheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, string sid, DateTime validFor, bool operationsOnly, bool retrieveAttributes, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
		//	var _bl = new NetSqlAzMan.CustomBussinessLogic.LdapWebApiBusinessFactory();
		//	var _asyncResult = Task.Run(() => _bl.SearchEntriesAsyncModeAsync(domainProfile, true, 0, LdapHelperDTO.EntryAttribute.objectSid, sid, LdapHelperDTO.RequiredEntryAttributes.ObjectSidAndSAMAccountName)).Result;
		//	if (_asyncResult.Entries.Count().Equals(0))
		//		throw new Exception(string.Format("No se pudo encontrar el objectSid: {0} usando el perfil de dominio {1}.", sid, domainProfile));

		//	var _entry = _asyncResult.Entries.First();
		//	if (!_entry.samAccountName.Equals(userName, StringComparison.OrdinalIgnoreCase))
		//		throw new Exception("El nombre de usuario y su Sid no se corresponden.");

		//	List<byte> _tokenList = new List<byte>();

		//	var _entries = Task.Run(() => _bl.SearchGroupMembershipAsync(domainProfile, true, 0, LdapHelperDTO.KeyEntryAttribute.objectSid, sid, LdapHelperDTO.RequiredEntryAttributes.OnlyObjectSid)).Result;

		//	//El usuario pertenece a grupos
		//	int userGroupsCount = _entries.Count();
		//	if (userGroupsCount > 0) {
		//		_tokenList.AddRange(NetSqlAzMan.ServiceBusinessObjects.AzManItem.GetSqlBinarySid(_entry.objectSidBytes));

		//		foreach (var _groupEntry in _entries) {
		//			_tokenList.AddRange(NetSqlAzMan.ServiceBusinessObjects.AzManItem.GetSqlBinarySid(_groupEntry.objectSidBytes));
		//		}
		//	}
		//	else {
		//		_tokenList.AddRange(_entry.objectSidBytes);
		//	}

		//	SqlConnection _conn = new SqlConnection(this.db.Connection.ConnectionString);
		//	_conn.Open();
		//	DataSet _checkAccessResults = new DataSet();
		//	System.Data.SqlClient.SqlCommand _cmd = new SqlCommand("dbo.netsqlazman_DirectCheckAccess", _conn);
		//	_cmd.CommandType = CommandType.StoredProcedure;
		//	_cmd.Parameters.AddWithValue("@STORENAME", storeName);
		//	_cmd.Parameters.AddWithValue("@APPLICATIONNAME", applicationName);
		//	_cmd.Parameters.AddWithValue("@ITEMNAME", itemName);
		//	_cmd.Parameters.AddWithValue("@OPERATIONSONLY", operationsOnly);
		//	_cmd.Parameters.AddWithValue("@TOKEN", _tokenList.ToArray());
		//	_cmd.Parameters.AddWithValue("@USERGROUPSCOUNT", (object)userGroupsCount);
		//	_cmd.Parameters.AddWithValue("@VALIDFOR", validFor);
		//	_cmd.Parameters.AddWithValue("@LDAPPATH", DirectoryServicesUtils.rootDsePath);
		//	_cmd.Parameters.AddWithValue("@RETRIEVEATTRIBUTES", retrieveAttributes);
		//	System.Data.SqlClient.SqlParameter pAuthorizationType = new System.Data.SqlClient.SqlParameter("@AUTHORIZATION_TYPE", (object)0);
		//	pAuthorizationType.Direction = System.Data.ParameterDirection.InputOutput;
		//	_cmd.Parameters.Add(pAuthorizationType);
		//	//BizRule Check CallBack
		//	SqlDataAdapter checkAccessPartialResultsDataAdapter = new SqlDataAdapter(_cmd);
		//	try {
		//		checkAccessPartialResultsDataAdapter.Fill(_checkAccessResults);
		//	}
		//	catch (SqlException sqlex) {
		//		if (sqlex.Message.StartsWith("Store not found", StringComparison.CurrentCultureIgnoreCase)) {
		//			throw SqlAzManException.StoreNotFoundException(storeName, sqlex);
		//		}
		//		else if (sqlex.Message.StartsWith("Application not found", StringComparison.CurrentCultureIgnoreCase)) {
		//			throw SqlAzManException.ApplicationNotFoundException(applicationName, storeName, sqlex);
		//		}
		//		else if (sqlex.Message.StartsWith("Item not found", StringComparison.CurrentCultureIgnoreCase)) {
		//			throw SqlAzManException.ItemNotFoundException(itemName, storeName, applicationName, sqlex);
		//		}
		//		else {
		//			throw SqlAzManException.GenericException(sqlex);
		//		}
		//	}
		//	finally {
		//		_conn.Close();
		//	}
		//	DataTable checkAccessPartialResultsDataTable = _checkAccessResults.Tables[0];
		//	DataTable checkAccessAttributesDataTable = (retrieveAttributes ? _checkAccessResults.Tables[1] : new DataTable());
		//	AuthorizationType result;
		//	if (checkAccessPartialResultsDataTable.Select("BizRuleId IS NOT NULL").Length == 0) {
		//		//No business rules to check ... just return check access authorizationType
		//		result = (AuthorizationType)((int)pAuthorizationType.Value);
		//	}
		//	else {
		//		//Transform DataRows into a Hierarchical Tree of Node(s)
		//		IAzManApplication application = this[storeName][applicationName];
		//		ItemNode itemNode = this.buildTreeOfNodes(application, checkAccessPartialResultsDataTable, itemName);
		//		//Execute Biz Rules and Cut Nodes that returns false
		//		Hashtable ctxParameters = new Hashtable();
		//		if (contextParameters != null) {
		//			foreach (KeyValuePair<string, object> kv in contextParameters) {
		//				ctxParameters.Add(kv.Key, kv.Value);
		//			}
		//		}
		//		//itemNode = this.executeBizRules(itemNode, new SqlAzManSID(windowsIdentity.User), ctxParameters);
		//		itemNode = this.executeBizRules(itemNode, new SqlAzManSID(sid), ctxParameters);
		//		//Compute final CheckAccess authorizationType
		//		result = this.computeCheckAccessResult(itemNode, ref checkAccessAttributesDataTable);
		//		if (result == AuthorizationType.Deny || result == AuthorizationType.Neutral) {
		//			checkAccessAttributesDataTable = new DataTable(); //no attributes
		//		}
		//		//else
		//		//{
		//		//    DataRow[] rowWithBizRules = checkAccessPartialResultsDataTable.Select("ItemId=" + itemNode.azmanItem.ItemId.ToString());
		//		//    foreach (DataRow dr in rowWithBizRules)
		//		//    {
		//		//        dr["AuthorizationType"] = (int)result;
		//		//    }
		//		//    checkAccessAttributesDataTable.AcceptChanges();
		//		//}
		//	}
		//	//Populate Attributes authorizationType
		//	if (retrieveAttributes) {
		//		attributes = new List<KeyValuePair<string, string>>(checkAccessAttributesDataTable.Rows.Count);
		//		foreach (DataRow dr in checkAccessAttributesDataTable.Rows) {
		//			KeyValuePair<string, string> kvp = new KeyValuePair<string, string>((string)dr[0], (string)dr[1]);
		//			if (!attributes.Contains(kvp)) {
		//				if (dr["ItemId"] == DBNull.Value) {
		//					if (result == AuthorizationType.Allow || result == AuthorizationType.AllowWithDelegation)
		//						attributes.Add(kvp);
		//				}
		//				else if (dr["ItemId"] != DBNull.Value) {
		//					int itemId = (int)dr["ItemId"];
		//					DataRow[] drItems = checkAccessPartialResultsDataTable.Select("ItemId=" + itemId.ToString());
		//					if (drItems.Length > 0) {
		//						DataRow drItem = drItems[0];
		//						AuthorizationType auth = (AuthorizationType)drItem["AuthorizationType"];
		//						if (auth == AuthorizationType.Allow || auth == AuthorizationType.AllowWithDelegation) {
		//							attributes.Add(kvp);
		//						}
		//						else if (auth == AuthorizationType.Neutral) {
		//							var store = this.Stores[storeName];
		//							var application = store.Applications[applicationName];
		//							var items = application.Items;
		//							var itemValues = items.Values.ToArray();
		//							AuthorizationType authParent = this.getParentResult(drItem, checkAccessPartialResultsDataTable, itemValues);
		//							if (authParent == AuthorizationType.Allow || authParent == AuthorizationType.AllowWithDelegation) {
		//								attributes.Add(kvp);
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}
		//	else {
		//		attributes = new List<KeyValuePair<string, string>>();
		//	}
		//	return result;
		//}
		#endregion

		#region Internal methods
		internal Dictionary<string, object> GetCustomPropertiesFromUserData(object userData) {
			Dictionary<string, object> _customColumns = new Dictionary<string, object>();

			var _props = userData.GetType().GetProperties().Where(p => !(p.Name.Equals("dbuserid", StringComparison.OrdinalIgnoreCase) || p.Name.Equals("dbusername", StringComparison.OrdinalIgnoreCase) || p.Name.Equals("fullname", StringComparison.OrdinalIgnoreCase)));

			foreach (var _prop in _props)
				_customColumns.Add(_prop.Name, _prop.GetValue(userData));

			return _customColumns;
		}

		//internal Dictionary<string, object> getCustomColumnsFromDataRow(DataRow row) {
		//	Dictionary<string, object> _customColumns = new Dictionary<string, object>();
		//	foreach (DataColumn dc in row.Table.Columns) {
		//		if (String.Compare(dc.ColumnName, "DBUserSid", true) != 0
		//			 &&
		//			 String.Compare(dc.ColumnName, "DBUserName", true) != 0 && String.Compare(dc.ColumnName, "FullName", true) != 0) {
		//			_customColumns.Add(dc.ColumnName, row[dc]);
		//		}
		//	}

		//	return _customColumns;
		//}


		//internal Dictionary<string, object> getCustomColumnsFromQueryble(LINQ.GetDBUsersResult record) {
		//	Dictionary<string, object> _customColumns = new Dictionary<string, object>();
		//	_customColumns.Add("UserID", record.UserID);
		//	_customColumns.Add("Password", record.Password);
		//	_customColumns.Add("FirstName", record.FirstName);
		//	_customColumns.Add("LastName", record.LastName);
		//	_customColumns.Add("Mail", record.Mail);
		//	_customColumns.Add("DepartmentId", record.DepartmentId);
		//	_customColumns.Add("DepartmentName", record.DepartmentName);
		//	_customColumns.Add("BranchOfficeIds", record.BranchOfficeIds);
		//	_customColumns.Add("BranchOfficeNames", record.BranchOfficeNames);
		//	_customColumns.Add("RelativeBranchOfficeIds", record.RelativeBranchOfficeIds);
		//	_customColumns.Add("Enabled", record.Enabled);
		//	return _customColumns;
		//}
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
				Dictionary<string, object> _customCols = GetCustomPropertiesFromUserData(_record);
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
			if (string.IsNullOrEmpty(userName))
				throw new ArgumentNullException(nameof(userName));

			var _users = this.db.GetDBUsers(null, null, null, userName);
			IAzManDBUser _result;
			if (_users.Count().Equals(0))
				throw SqlAzManException.DBUserNotFoundException(userName, null);
			else {
				var _user = _users.First();
				Dictionary<string, object> _customProperties = GetCustomPropertiesFromUserData(_user);
				_result = new SqlAzManDBUser(new SqlAzManSID(_user.DBUserSid.ToArray(), true), _user.DBUserName, _user.FullName, _customProperties);
			}

			return _result;

			////OLD VERSION
			//var dtDBUsers = this.db.GetDBUsersEx(null, null, null, userName);
			//IAzManDBUser result;
			//if (dtDBUsers.Rows.Count == 0) {
			//	throw SqlAzManException.DBUserNotFoundException(userName, null);
			//}
			//else {
			//	Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
			//	result = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);
			//}
			//return result;
		}

		/// <summary>
		/// Gets the DB users.
		/// </summary>
		/// <returns></returns>
		public IAzManDBUser[] GetDBUsers() {
			var _users = this.db.GetDBUsers(null, null, null, null);
			var _result = new List<IAzManDBUser>(_users.Count());
			foreach (var _user in _users) {
				Dictionary<string, object> _customProperties = GetCustomPropertiesFromUserData(_user);
				_result.Add(new SqlAzManDBUser(new SqlAzManSID(_user.DBUserSid.ToArray(), true), _user.DBUserName, _user.FullName, _customProperties));
			}

			return _result.ToArray();

			////OLD VERSION
			//var dtDBUsers = this.db.GetDBUsersEx(null, null, null, null);
			//IAzManDBUser[] result = new IAzManDBUser[dtDBUsers.Rows.Count];
			//for (int i = 0; i < dtDBUsers.Rows.Count; i++) {
			//	Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[i]);
			//	result[i] = new SqlAzManDBUser(dtDBUsers.Rows[i], _customCols);
			//}
			//return result;
		}

		//public IAzManDBUser GetAzManUser(IAzManSid customSid, bool dummy) {
		//	IQueryable<LINQ.GetDBUsersResult> _users = this.db.GetDBUsers(null, null, customSid.BinaryValue, null);
		//	IAzManDBUser result;
		//	if (_users.Count().Equals(0))
		//		throw SqlAzManException.DBUserNotFoundException(customSid.StringValue, null);
		//	else {
		//		LINQ.GetDBUsersResult _user = _users.First();
		//		Dictionary<string, object> _customCols = getCustomColumnsFromQueryble(_user);
		//		result = new SqlAzManDBUser(new SqlAzManSID(_user.DBUserSid.ToArray(), true), _user.DBUserName, _user.FullName, _customCols);
		//	}
		//	return result;
		//}


		//[Obsolete("Use instead GetAzManUser")]
		//public bool GetAzManDBUser(string userName, out IAzManDBUser user, out Exception hex) {
		//	user = null;
		//	hex = null;
		//	try {
		//		var dtDBUsers = this.db.GetDBUsersEx(null, null, null, userName);

		//		if (dtDBUsers.Rows.Count == 0) {
		//			throw SqlAzManException.DBUserNotFoundException(userName, null);
		//		}
		//		else {
		//			Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
		//			user = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);
		//		}

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		hex = ex;
		//		return false;
		//	}
		//}


		//public IAzManDBUser[] GetAzManUsers(bool dummy) {
		//	var dtDBUsers = this.db.GetDBUsersEx(null, null, null, null);
		//	IAzManDBUser[] result = new IAzManDBUser[dtDBUsers.Rows.Count];
		//	for (int i = 0; i < dtDBUsers.Rows.Count; i++) {
		//		Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[i]);
		//		result[i] = new SqlAzManDBUser(dtDBUsers.Rows[i], _customCols);
		//	}
		//	return result;
		//}


		///// <summary>
		///// VBastidas: Finds the DB user with Password
		///// </summary>
		///// <param name="userName"></param>
		///// <returns></returns>
		//[Obsolete("Usar el metodo GetDBUser")]
		//public IAzManDBUser GetDBUserWithPassword(string userName) {
		//	var _datbDBUsers = this.db.GetDBUsersWithPasswordsEx(null, null, null, userName);
		//	IAzManDBUser result;
		//	if (_datbDBUsers.Rows.Count == 0) {
		//		throw SqlAzManException.DBUserNotFoundException(userName, null);
		//	}
		//	else {
		//		Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(_datbDBUsers.Rows[0]);
		//		result = new SqlAzManDBUser(_datbDBUsers.Rows[0], _customCols);
		//	}
		//	return result;
		//}


		///// <summary>
		///// 
		///// </summary>
		///// <param name="userName"></param>
		///// <param name="password"></param>
		///// <param name="azManUser"></param>
		///// <param name="exceptionType"></param>
		///// <param name="statusMessage"></param>
		///// <param name="stackTrace"></param>
		///// <returns></returns>		
		//public bool GetDBUserByValidatingPassword(string userName, string password, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace) {
		//	azManUser = null;
		//	statusMessage = null;
		//	exceptionType = null;
		//	stackTrace = null;
		//	try {
		//		var dtDBUsers = this.db.GetDBUsersWithPasswordsEx(null, null, null, userName);

		//		if (dtDBUsers.Rows.Count == 0)
		//			throw new MissingMemberException("No se encontró el usuario en la datos.");

		//		if (!dtDBUsers.Rows[0]["Password"].ToString().Equals(password))
		//			throw new AuthenticationException("Usuario y/o contraseña incorrecta.");

		//		Dictionary<string, object> _customCols = getCustomColumnsFromDataRow(dtDBUsers.Rows[0]);
		//		azManUser = new SqlAzManDBUser(dtDBUsers.Rows[0], _customCols);

		//		return true;
		//	}
		//	catch (Exception ex) {
		//		exceptionType = ex.GetType().FullName;
		//		statusMessage = ex.Message;
		//		stackTrace = ex.StackTrace;

		//		return false;
		//	}
		//}


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
		public IAzManDBUser GetLDAPUserByValidatingPassword(string domainProfile, bool useGC, string userName, string password) {
			IAzManDBUser _user = null;

			if (userName.Contains('*'))
				throw new InvalidOperationException("El nombre de usuario no puede contener caracteres comodin.");

			var _bl = new CustomBussinessLogic.LdapWebApiBusinessFactory();

			//Autenticar usuario
			var _authStatus = Task.Run(() => {
				return _bl.AuthenticateUserAsync(domainProfile, useGC, userName, password);
			}).Result;
			if (!_authStatus.IsAuthenticated) {
				throw NetSqlAzMan.SqlAzManException.AuthenticationException(userName, null);
			}

			//Obtener datos del usuarios autenticado
			var _entries = Task.Run(() => {
				return _bl.SearchEntriesAsyncModeAsync(domainProfile, true, 0, LdapHelperDTO.EntryAttribute.sAMAccountName, userName, LdapHelperDTO.RequiredEntryAttributes.Minimun);
			}).Result;
			var _entry = _entries.Entries.First();

			//Instanciar objeto
			_user = new SqlAzManDBUser(true, new SqlAzManSID(_entry.objectSid), _entry.DomainProfile, _entry.samAccountName, _entry.displayName, new Dictionary<string, object>());

			return _user;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainProfile"></param>
		/// <param name="userName"></param>
		/// <param name="azManUser"></param>
		/// <param name="hex"></param>
		/// <returns></returns>
		//public bool GetLDAPUser(string domainProfile, string userName, out IAzManDBUser azManUser, out string exceptionType, out string statusMessage, out string stackTrace) {
		public bool GetLDAPUser(string domainProfile, string userName, out IAzManDBUser azManUser, out Exception hex) {
			azManUser = null;
			hex = null;

			try {
				if (userName.Contains('*'))
					throw new InvalidOperationException("El nombre de usuario no puede contener caracteres comodines.");

				LdapHelperDTO.AsyncResult _asyncResult = null;
				#region Call Custom Logic
				var _bl = new CustomBussinessLogic.LdapWebApiBusinessFactory();
				_asyncResult = Task.Run(() => _bl.SearchEntriesAsyncModeAsync(domainProfile, true, 0, LdapHelperDTO.EntryAttribute.sAMAccountName, userName, LdapHelperDTO.RequiredEntryAttributes.Minimun)).Result;
				#endregion

				if (_asyncResult.Entries.Count().Equals(0))
					throw new ActiveDirectoryObjectNotFoundException(string.Format("No se encontró alguna entrada con el atributo {0}: {1}", LdapHelperDTO.EntryAttribute.sAMAccountName.ToString(), userName));

				if (_asyncResult.Entries.Count() > 1)
					throw new ActiveDirectoryObjectNotFoundException("No se pudo obtener información especifica del usuario en el dominio. Se obtuvieron múltiples resultados.");

				var _entry = _asyncResult.Entries.First();
				azManUser = new SqlAzManDBUser(true, new SqlAzManSID(_entry.objectSid), _entry.DomainProfile, _entry.samAccountName, _entry.displayName, new Dictionary<string, object>());

				return true;
			}
			catch (Exception ex) {
				//exceptionType = ex.GetType().FullName;
				//statusMessage = ex.Message;
				//stackTrace = ex.StackTrace;
				hex = ex;
				return false;
			}
		}

		public AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters) {
			List<KeyValuePair<string, string>> attributes;
			return this.internalCheckAccessLDAP(storeName, applicationName, itemName, domainProfile, userName, validFor, operationsOnly, false, out attributes, contextParameters);
		}

		public AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string userName, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			return this.internalCheckAccessLDAP(storeName, applicationName, itemName, domainProfile, userName, validFor, operationsOnly, true, out attributes, contextParameters);
		}

		public AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string sid, byte[] binarySid, DateTime validFor, bool operationsOnly, params KeyValuePair<string, object>[] contextParameters) {
			List<KeyValuePair<string, string>> attributes;
			return this.internalCheckAccessLDAP(storeName, applicationName, itemName, domainProfile, sid, binarySid, validFor, operationsOnly, false, out attributes, contextParameters);
		}

		public AuthorizationType CheckAccessLDAP(string storeName, string applicationName, string itemName, string domainProfile, string sid, byte[] binarySid, DateTime validFor, bool operationsOnly, out List<KeyValuePair<string, string>> attributes, params KeyValuePair<string, object>[] contextParameters) {
			return this.internalCheckAccessLDAP(storeName, applicationName, itemName, domainProfile, sid, binarySid, validFor, operationsOnly, true, out attributes, contextParameters);
		}

		public ServiceBusinessObjects.AzManStorage GetAzManStorageDTO() {
			return new ServiceBusinessObjects.AzManStorage() {
				DatabaseVesion = this.DatabaseVesion,
				IAmAdmin = this.IAmAdmin,
				Mode = (ServiceBusinessObjects.AzManMode)this.Mode,
				ConnectionString = this.ConnectionString,
				StorageTimeOut = this.StorageTimeOut,
				LogInformations = this.LogInformations,
				LogErrors = this.LogErrors,
				LogWarnings = this.LogWarnings,
				LogOnDb = this.LogOnDb,
				LogOnEventLog = this.LogOnEventLog,
				//Description = Se genera al obtener la propiedad
			};
		}
		//public async Task<IEnumerable<LdapHelperDTO.ILdapEntry>> SearchLDAPUsersAndGroupsAsync(string domainProfile, string criteria) {
		//	try {
		//		IEnumerable<LdapHelperDTO.ILdapEntry> _entries = null;
		//		#region Call WebApi
		//		var _profile = new WebApiClientHelpers.Utils().GetLdapProfileAsync(domainProfile).Result;
		//		var _h = new WebApiClientHelpers.LdapUserAndGroupHelper<IEnumerable<LdapHelperDTO.LdapEntry>>(_profile.LdapProxyWebApiUri);
		//		//Llamar al WebApi
		//		var _return = await Task.Run(() => _h.GetUserAndGroupsByAttributes(_profile.LdapServerProfile, true, LdapHelperDTO.EntryAttribute.sAMAccountName, criteria, false, LdapHelperDTO.EntryAttribute.cn, criteria));
		//		if (_h.IsResponseContentError(_return))
		//			_h.ThrowWebApiRequestException(_return);
		//		else
		//			_entries = _h.GetSBOFromReturnedContent(_return);
		//		#endregion

		//		return _entries;
		//	}
		//	catch (Exception ex) {
		//		throw ex;
		//	}
		//}
		#endregion

		private void storeApplicationItemValidation(string storeName, string applicationName, out IAzManStore store, out IAzManApplication application, out IEnumerable<IAzManItem> allItems) {
			//itemResultCache = new Hashtable();
			//attributesResultCache = new Hashtable();
			storeName = storeName.Trim();
			applicationName = applicationName.Trim();
			store = (from s in this.Stores.Values
						where String.Equals(s.Name, storeName, StringComparison.OrdinalIgnoreCase)
						select s).FirstOrDefault();

			if (store == null)
				throw SqlAzManException.StoreNotFoundException(storeName, null);

			application = (from a in store.Applications.Values
								where String.Equals(a.Name, applicationName, StringComparison.OrdinalIgnoreCase)
								select a).FirstOrDefault();

			if (application == null)
				throw SqlAzManException.ApplicationNotFoundException(applicationName, store, null);

			allItems = from t in application.Items.Values
						  select t;
		}

		/// <summary>
		/// Gets the authorized Items.
		/// </summary>
		/// <param name="storeName">Name of the store.</param>
		/// <param name="applicationName">Name of the application.</param>
		/// <param name="userSSid">The user S sid.</param>
		/// <param name="groupsSSid">The groups S sid.</param>
		/// <param name="validFor">The valid for.</param>
		/// <param name="contextParameters">The context parameters.</param>
		/// <returns></returns>
		public NetSqlAzMan.Cache.AuthorizedItem[] GetAuthorizedItems(string storeName, string applicationName, IAzManDBUser user, DateTime validFor, params KeyValuePair<string, object>[] contextParameters) {
			IAzManStore store;
			IAzManApplication application;
			IEnumerable<IAzManItem> allItems;
			this.storeApplicationItemValidation(storeName, applicationName, out store, out application, out allItems);

			var result = new List<NetSqlAzMan.Cache.AuthorizedItem>();
			foreach (var item in allItems) {
				List<KeyValuePair<string, string>> attributes;
				AuthorizationType auth;
				if (!user.IsLdapEntry) {
					auth = this.internalCheckAccess(store.Name, application.Name, item.Name, user, validFor, false, true, out attributes, contextParameters);
					result.Add(new NetSqlAzMan.Cache.AuthorizedItem() {
						Name = item.Name,
						Authorization = auth,
						Type = item.ItemType,
						Attributes = attributes
					});
				}
				else {
					//auth = this.internalCheckAccessLDAP(store.Name, application.Name, item.Name, user.LdapProfileId, user.UserName, user.CustomSid.StringValue, validFor, false, true, out attributes, contextParameters);
					auth = this.internalCheckAccessLDAP(store.Name, application.Name, item.Name, user.DomainProfile, user.CustomSid.StringValue, user.CustomSid.BinaryValue, validFor, false, true, out attributes, contextParameters);
					result.Add(new NetSqlAzMan.Cache.AuthorizedItem() {
						Name = item.Name,
						Authorization = auth,
						Type = item.ItemType,
						Attributes = attributes
					});
				}
			}

			return result.ToArray();
		}
	}
}
