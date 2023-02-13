using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
	public partial class SqlAzManApplicationGroup
	{
		public IAzManApplicationGroupMember CreateApplicationGroupMemberCustom(IAzManSid sid, WhereDefined whereDefined, bool isMember, string ldapDomain) {
			if (this.groupType != GroupType.Basic)
				throw new InvalidOperationException("Method not supported for LDAP Groups");

			if (this.application.Store.Storage.Mode == NetSqlAzManMode.Administrator && whereDefined == WhereDefined.Local) {
				throw new SqlAzManException("Cannot create Application Group members defined on local in Administrator Mode");
			}
			//Loop detection
			if (whereDefined == WhereDefined.Application) {
				IAzManApplicationGroup applicationGroupToAdd = this.application.GetApplicationGroup(sid);
				if (this.detectLoop(applicationGroupToAdd))
					throw new SqlAzManException(String.Format("Cannot add '{0}'. A loop has been detected.", applicationGroupToAdd.Name));
			}
			int retV = this.db.ApplicationGroupMemberInsertCustom(this.applicationGroupId, sid.BinaryValue, (byte)whereDefined, isMember, this.application.ApplicationId, ldapDomain);
			IAzManApplicationGroupMember result = new SqlAzManApplicationGroupMember(this.db, this, retV, sid, whereDefined, isMember, ldapDomain, this.ens);
			this.raiseApplicationGroupMemberCreated(this, result);
			if (this.ens != null)
				this.ens.AddPublisher(result);
			return result;
		}
	}
}
