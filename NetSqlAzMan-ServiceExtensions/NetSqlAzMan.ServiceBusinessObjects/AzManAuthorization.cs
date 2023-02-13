using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
    /// <summary>
    /// Interfaces interface for all AzManAuthorizations
    /// </summary>
    public class AzManAuthorization
    {
        public AzManAuthorization() {
            Attributes = new List<AzManAuthorizationAttribute>();
        }

        /// <summary>
        /// Gets the authorization id.
        /// </summary>
        /// <value>The authorization id.</value>
        public int AuthorizationId { get; set; } //da implementare esplicitamente

        /// <summary>
        /// Gets the itemName.
        /// </summary>
        /// <value>The itemName.</value>
        public AzManItem Item { get; set; }

        /// <summary>
        /// Gets the Owner.
        /// </summary>
        /// <value>The Owner object owner.</value>
        public AzManSid Owner { get; set; }

        /// <summary>
        /// Gets the where is defined the Owner.
        /// </summary>
        /// <value>The object owner where defined.</value>
        public WhereDefined OwnerSidWhereDefined { get; set; }

        /// <summary>
        /// Gets the object owner.
        /// </summary>
        /// <value>The object owner.</value>
        public AzManSid SID { get; set; }

        /// <summary>
        /// Gets the object owner where defined.
        /// </summary>
        /// <value>The object owner where defined.</value>
        public WhereDefined SidWhereDefined { get; set; }

        /// <summary>
        /// Gets the type of the authorization.
        /// </summary>
        /// <value>The type of the authorization.</value>
        public AuthorizationType AuthorizationType { get; set; }

        /// <summary>
        /// Gets the valid from.
        /// </summary>
        /// <value>The valid from.</value>
        public Nullable<DateTime> ValidFrom { get; set; }

        /// <summary>
        /// Gets the valid to.
        /// </summary>
        /// <value>The valid to.</value>		
        public Nullable<DateTime> ValidTo { get; set; }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public IEnumerable<AzManAuthorizationAttribute> Attributes { get; set; }

        /// <summary>
        /// Domain Profile configuration
        /// </summary>
        public string DomainProfile { get; set; }

        public string samAccountName { get; set; }

        public string cn { get; set; }

        public string displayName { get; set; }

        public string objectSidString { get; set; }

        public string distinguishedName { get; set; }

        public string objectClass { get; set; }

        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>AzManAuthorization</returns>
        public AzManAuthorization Clone() {
            var _clone = new AzManAuthorization() {
                AuthorizationId = this.AuthorizationId,
                AuthorizationType = this.AuthorizationType,
                DomainProfile = this.DomainProfile,
                ValidFrom = this.ValidFrom,
                ValidTo = this.ValidTo,
                Item = this.Item.Clone(),
                Owner = this.Owner.Clone(),
                SID = this.SID.Clone(),
                SidWhereDefined = this.SidWhereDefined,
                OwnerSidWhereDefined = this.OwnerSidWhereDefined,
                samAccountName = this.samAccountName,
                cn = this.cn,
                displayName = this.displayName,
                objectSidString = this.objectSidString,
                distinguishedName = this.distinguishedName,
                objectClass = this.objectClass
            };

            var _attributes = new List<AzManAuthorizationAttribute>();
            foreach (var _att in this.Attributes) {
                _attributes.Add(_att.Clone());
            }
            _clone.Attributes = _attributes;

            return _clone;
        }

        public string GetStoreName() {
            return this.Item.Application.Store.Name;
        }

        public string GetApplicationName() {
            return this.Item.Application.Name;
        }

        public string GetItemName() {
            return this.Item.Name;
        }

        public string LdapDescription {
            get {
                if (string.IsNullOrEmpty(this.DomainProfile))
                    return "Error! No Ldap Entry";
                else {
                    return string.Format("{0}\\{1} ({2})", this.DomainProfile, this.samAccountName, string.IsNullOrEmpty(this.displayName) ? this.cn : this.displayName);
                }
            }
        }

        ///// <summary>
        ///// Occurs after an Attribute object has been Created.
        ///// </summary>
        //public event ENS.AttributeCreatedDelegate<AzManAuthorization> AuthorizationAttributeCreated;

        ///// <summary>
        ///// Occurs after a SqlAzManAuthorization object has been Deleted.
        ///// </summary>
        //public event ENS.AuthorizationDeletedDelegate AuthorizationDeleted;

        ///// <summary>
        ///// Occurs after a SqlAzManAuthorization object has been Updated.
        ///// </summary>
        //public event ENS.AuthorizationUpdatedDelegate AuthorizationUpdated;
    }
}
