using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
    public partial class SqlAzManStoreGroup
    {
        public IAzManStoreGroupMember GetStoreGroupMemberById(int id) {
            if (this.groupType != GroupType.Basic)
                throw new InvalidOperationException("Method not supported for LDAP Groups");

            var sgm = (from f in this.db.StoreGroupMembers()
                       where (this.store.Storage.Mode == NetSqlAzManMode.Administrator && f.WhereDefined != (byte)WhereDefined.Local || this.store.Storage.Mode == NetSqlAzManMode.Developer) &&
                       f.StoreGroupId == this.storeGroupId && f.StoreGroupMemberId == id
                       select f).FirstOrDefault();

            IAzManStoreGroupMember storeGroupMember = null;
            if (sgm != null) {
                #region ***PERSONALIZADO***
                storeGroupMember = new SqlAzManStoreGroupMember(this.db, this, sgm.StoreGroupMemberId.Value, new SqlAzManSID(sgm.ObjectSid.ToArray(), sgm.WhereDefined == (byte)(WhereDefined.Database)), (WhereDefined)sgm.WhereDefined, sgm.IsMember.Value, this.ens, sgm.DomainProfile, sgm.samAccountName, sgm.cn, sgm.displayName, sgm.objectSidString, sgm.distinguishedName, sgm.objectClass);
                #endregion
                if (this.ens != null)
                    this.ens.AddPublisher(storeGroupMember);
            }

            return storeGroupMember;
        }

        public IAzManStoreGroupMember CreateStoreGroupMemberCustom(IAzManSid sid, WhereDefined whereDefined, bool isMember, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass) {
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
            int retV = this.db.StoreGroupMemberInsertCustom(this.store.StoreId, this.storeGroupId, sid.BinaryValue, (byte)whereDefined, isMember, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            IAzManStoreGroupMember result = new SqlAzManStoreGroupMember(this.db, this, retV, sid, whereDefined, isMember, this.ens, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            this.raiseStoreGroupMemberCreated(this, result);
            if (this.ens != null)
                this.ens.AddPublisher(result);
            return result;
        }
    }
}
