using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzMan.LDAPServices
{
	internal static class LDAPConfigUtils
	{
		internal static string GetWebSvcEndpointByDomainProfile(string connString, string ldapDomain) {
			System.Data.SqlClient.SqlConnection _cn = new System.Data.SqlClient.SqlConnection(connString);
			System.Data.SqlClient.SqlCommand _cmd = _cn.CreateCommand();
			_cmd.CommandText = string.Format("SELECT [ldap_client_endpoint] FROM [dbo].[identity_LDAPConfig] WHERE LOWER(ldap_domain)=LOWER('{0}') AND ldap_enabled = 1;", ldapDomain);
			_cmd.CommandType = System.Data.CommandType.Text;
			_cmd.CommandTimeout = 10;
			_cn.Open();
			System.Data.SqlClient.SqlDataReader _reader = _cmd.ExecuteReader();
			_reader.Read();
			string _endPoint = _reader.GetString(0);
			_reader.Close();
			return _endPoint;
		}
	}
}
