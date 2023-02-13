using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManDBUsersController :BaseApiController
	{
		public AzManDBUsersController() : base() {
		}



		internal NetSqlAzMan.ServiceBusinessObjects.AzManDBUser GetSBOFromDBUser(NetSqlAzMan.Interfaces.IAzManDBUser dbUser) {
			return new NetSqlAzMan.ServiceBusinessObjects.AzManDBUser {
				CustomSid = new NetSqlAzMan.ServiceBusinessObjects.AzManSid(dbUser.CustomSid.StringValue, true),
				UserName = dbUser.UserName,
				DisplayName = dbUser.DisplayName,
				IsLdapEntry = dbUser.IsLdapEntry,
				DomainProfile = dbUser.DomainProfile,
				CustomColumns = dbUser.CustomColumns
			};
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser> GetSBOFromListOfDBUsers(IEnumerable<NetSqlAzMan.Interfaces.IAzManDBUser> dbUsers) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>();

			foreach (var _dbUser in dbUsers)
				_listSBO.Add(GetSBOFromDBUser(_dbUser));

			return _listSBO;
		}



		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>))]
		public async Task<IHttpActionResult> GetAllAsync() {
			try {
				var _dbUsers = await Task.Run(() => _storage.GetDBUsers());
				var _return = this.GetSBOFromListOfDBUsers(_dbUsers);

				return Ok(_return);
			}
			catch (Exception ex) {
				return InternalServerError(ex);
			}
		}

		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManDBUser>))]
		public async Task<IHttpActionResult> GetByUserNameOrId(string userNameOrId, bool isId) {
			var _user = await Task.Run(() => {
				if (!isId)
					return _storage.GetDBUser(userNameOrId);
				else
					return _storage.GetDBUser(new NetSqlAzMan.SqlAzManSID(userNameOrId, true));
			});

			var _return = this.GetSBOFromDBUser(_user);

			return Ok(_return);
		}
	}
}
