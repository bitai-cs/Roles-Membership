using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
    public class AzManGenericMember
    {
        public string Name;
        public AzManSid sid;
        public string Description;
        public AuthorizationType authorizationType;
        public DateTime? validFrom;
        public DateTime? validTo;
        public WhereDefined WhereDefined;

        /// <summary>
        /// Constructor used by the serializer
        /// </summary>
        public AzManGenericMember() {
        }

        public AzManGenericMember(string name, AzManSid sid, WhereDefined whereDefined) {
            this.Name = name;
            this.sid = sid;
            this.WhereDefined = whereDefined;
            this.Description = String.Empty;
        }

        public AzManGenericMember(string name, string description) {
            this.Name = name;
            this.Description = description;
        }

        public AzManGenericMember(AzManSid sid, WhereDefined whereDefined, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo) {
            this.sid = sid;
            this.WhereDefined = whereDefined;
            this.authorizationType = authorizationType;
            this.validFrom = validFrom;
            this.validTo = validTo;
        }

        #region ***PERSONALIZADO***
        public string DomainProfile;
        public string samAccountName;
        public string cn;
        public string displayName;
        public string objectSidString;
        public string distinguishedName;
        public string objectClass;

        public AzManGenericMember(AzManSid sid, WhereDefined whereDefined, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo) {
            this.sid = sid;
            this.WhereDefined = whereDefined;
            this.authorizationType = authorizationType;
            this.validFrom = validFrom;
            this.validTo = validTo;
            this.DomainProfile = domainProfile;
            this.samAccountName = samAccountName;
            this.cn = cn;
            this.displayName = displayName;
            this.objectSidString = objectSidString;
            this.distinguishedName = distinguishedName;
            this.objectClass = objectClass;
        }

        public AzManGenericMember(string name, AzManSid sid, WhereDefined whereDefined, string domainProfile, string samAccountName, string cn, string displayName, string objectSidString, string distinguishedName, string objectClass)
            : this(name, sid, whereDefined) {
            this.DomainProfile = domainProfile;
            this.samAccountName = samAccountName;
            this.cn = cn;
            this.displayName = displayName;
            this.objectSidString = objectSidString;
            this.distinguishedName = distinguishedName;
            this.objectClass = objectClass;
        }
        #endregion
    }
}
