using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
    public partial class SqlAzManApplicationGroup
    {
        /// <summary>
        /// Gets the application group member by id.
        /// </summary>
        /// <returns></returns>
        public IAzManApplicationGroupMember GetApplicationGroupMemberById(int id) {
            if (this.groupType != GroupType.Basic)
                throw new InvalidOperationException("Method not supported for LDAP Groups");

            var agam = (from f in this.db.ApplicationGroupMembers()
                        where (this.application.Store.Storage.Mode == NetSqlAzManMode.Administrator && f.WhereDefined != (byte)WhereDefined.Local || this.application.Store.Storage.Mode != NetSqlAzManMode.Administrator) && f.ApplicationGroupId == this.applicationGroupId && f.ApplicationGroupMemberId == id
                        select f).FirstOrDefault();

            IAzManApplicationGroupMember applicationGroupMember = null;
            if (agam != null) {
                #region ***PERSONALIZADO***
                applicationGroupMember = new SqlAzManApplicationGroupMember(this.db, this, agam.ApplicationGroupMemberId.Value, new SqlAzManSID(agam.ObjectSid.ToArray(), agam.WhereDefined == (byte)(WhereDefined.Database)), (WhereDefined)agam.WhereDefined, agam.IsMember.Value, agam.DomainProfile, agam.samAccountName, agam.cn, agam.displayName, agam.objectSidString, agam.distinguishedName, agam.objectClass, this.ens);
                #endregion
                if (this.ens != null)
                    this.ens.AddPublisher(applicationGroupMember);
            }

            return applicationGroupMember;
        }


        public IAzManApplicationGroupMember CreateApplicationGroupMemberCustom(IAzManSid sid, WhereDefined whereDefined, bool isMember, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass) {
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
            int retV = this.db.ApplicationGroupMemberInsertCustom(this.applicationGroupId, sid.BinaryValue, (byte)whereDefined, isMember, this.application.ApplicationId, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
            IAzManApplicationGroupMember result = new SqlAzManApplicationGroupMember(this.db, this, retV, sid, whereDefined, isMember, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass, this.ens);
            this.raiseApplicationGroupMemberCreated(this, result);
            if (this.ens != null)
                this.ens.AddPublisher(result);
            return result;
        }
    }
}
