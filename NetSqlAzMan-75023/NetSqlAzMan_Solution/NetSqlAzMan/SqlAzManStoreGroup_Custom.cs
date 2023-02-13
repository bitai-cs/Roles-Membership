using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
	public partial class SqlAzManStoreGroup
	{
		public IAzManStoreGroupMember CreateStoreGroupMemberCustom(IAzManSid sid, WhereDefined whereDefined, bool isMember, string ldapDomain) {
			if (this.groupType != GroupType.Basic)
				throw new InvalidOperationException("Method not supported for LDAP Groups");

			if (this.store.Storage.Mode == NetSqlAzManMode.Administrator && whereDefined == WhereDefined.Local) {
				throw new SqlAzManException("Cannot create Store Group members defined on local in Administrator Mode");
			}
			//Loop detection
			if (whereDefined == WhereDefined.Store) {
				IAzManStoreGroup storeGroupToAdd = this.store.GetStoreGroup(sid);
				if (this.detectLoop(storeGroupToAdd))
					throw new SqlAzManException(String.Format("Cannot add '{0}'. A loop has been detected.", storeGroupToAdd.Name));
			}
			int retV = this.db.StoreGroupMemberInsertCustom(this.store.StoreId, this.storeGroupId, sid.BinaryValue, (byte)whereDefined, isMember, ldapDomain);
			IAzManStoreGroupMember result = new SqlAzManStoreGroupMember(this.db, this, retV, sid, whereDefined, isMember, this.ens, ldapDomain);
			this.raiseStoreGroupMemberCreated(this, result);
			if (this.ens != null)
				this.ens.AddPublisher(result);
			return result;
		}
	}
}
