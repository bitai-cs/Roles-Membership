using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
    public partial class SqlAzManItem
    {
        /// <summary>
        /// Creates the authorization.
        /// </summary>
        /// <param name="owner">The owner owner.</param>
        /// <param name="ownerSidWhereDefined">The owner sid where defined.</param>
        /// <param name="sid">The object owner.</param>
        /// <param name="sidWhereDefined">The object owner where defined.</param>
        /// <param name="authorizationType">Type of the authorization.</param>
        /// <param name="validFrom">The valid from.</param>
        /// <param name="validTo">The valid to.</param>
        /// <returns>IAzManAuthorization</returns>
        public IAzManAuthorization CreateAuthorizationCustom(IAzManSid owner, WhereDefined ownerSidWhereDefined, IAzManSid sid, WhereDefined sidWhereDefined, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass) {
            //DateTime range check
            if (validFrom.HasValue && validTo.HasValue) {
                if (validFrom.Value > validTo.Value)
                    throw new InvalidOperationException("ValidFrom cannot be greater then ValidTo if supplied.");
            }
            if (this.application.Store.Storage.Mode == NetSqlAzManMode.Administrator && sidWhereDefined == WhereDefined.Local) {
                throw new SqlAzManException("Cannot create an Authorization on members defined on local in Administrator Mode");
            }
            var existing = (from aut in this.db.Authorizations()
                            where aut.ItemId == this.itemId && aut.OwnerSid == owner.BinaryValue && aut.OwnerSidWhereDefined == (byte)ownerSidWhereDefined && aut.ObjectSid == sid.BinaryValue && aut.AuthorizationType == (byte)authorizationType && aut.ValidFrom == validFrom && aut.ValidTo == validTo
                            select aut).FirstOrDefault();
            if (existing == null) {
                int id = this.db.AuthorizationInsertCustom(this.itemId, owner.BinaryValue, (byte)ownerSidWhereDefined, sid.BinaryValue, (byte)sidWhereDefined, (byte)authorizationType, (validFrom.HasValue ? validFrom.Value : new DateTime?()), (validTo.HasValue ? validTo.Value : new DateTime?()), this.application.ApplicationId, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass);
                IAzManAuthorization result = new SqlAzManAuthorization(this.db, this, id, owner, ownerSidWhereDefined, sid, sidWhereDefined, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass, authorizationType, validFrom, validTo, this.ens);
                this.raiseAuthorizationCreated(this, result);
                if (this.ens != null)
                    this.ens.AddPublisher(result);
                this.authorizations = null; //Force cache refresh
                return result;
            }
            else {
                IAzManAuthorization result = new SqlAzManAuthorization(this.db, this, existing.ItemId.Value, new SqlAzManSID(existing.OwnerSid.ToArray()), (WhereDefined)existing.OwnerSidWhereDefined, new SqlAzManSID(existing.ObjectSid.ToArray()), (WhereDefined)existing.ObjectSidWhereDefined, domainProfile, samAccountName, cn, displayName, objectSidString, distinguishedName, objectClass, (AuthorizationType)existing.AuthorizationType.Value, existing.ValidFrom, existing.ValidTo, this.ens);
                return result;
            }
        }
    }
}