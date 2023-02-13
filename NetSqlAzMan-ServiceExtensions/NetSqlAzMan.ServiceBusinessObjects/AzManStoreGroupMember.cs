using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
    /// <summary>
    /// Class for all Store Group Members.
    /// </summary>
    public class AzManStoreGroupMember
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AzManStoreGroupMember() { }

        /// <summary>
        /// Gets the store group member id.
        /// </summary>
        /// <value>The store group member id.</value>
        [Key]
        [Required]
        public int StoreGroupMemberId {
            get; set;
        }

        /// <summary>
        /// Gets the store group.
        /// </summary>
        /// <value>The store group.</value>
        public AzManStoreGroup StoreGroup {
            get; set;
        }

        /// <summary>
        /// Gets the object owner.
        /// </summary>
        /// <value>The object owner.</value>
        [Required]
        public AzManSid SID {
            get; set;
        }

        /// <summary>
        /// Gets where member is defined.
        /// </summary>
        /// <value>The where defined.</value>
        [Required]
        public WhereDefined WhereDefined {
            get; set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is member.
        /// </summary>
        /// <value><c>true</c> if this instance is member; otherwise, <c>false</c>.</value>
        public bool IsMember {
            get; set;
        }

        /// <summary>
        /// Domain profile where object was loaded
        /// </summary>
        public string DomainProfile {
            get; set;
        }

        public string samAccountName { get; set; }

        public string cn { get; set; }

        public string displayName { get; set; }

        public string objectSidString { get; set; }

        public string distinguishedName { get; set; }

        public string objectClass { get; set; }

        public AzManStoreGroupMember Clone() {
            return new AzManStoreGroupMember {
                StoreGroupMemberId = this.StoreGroupMemberId,
                SID = this.SID.Clone(),
                StoreGroup = this.StoreGroup.Clone(),
                IsMember = this.IsMember,
                DomainProfile = this.DomainProfile,
                WhereDefined = this.WhereDefined,
                samAccountName = this.samAccountName,
                cn = this.cn,
                displayName = this.displayName,
                objectSidString = this.objectSidString,
                distinguishedName = this.distinguishedName,
                objectClass = this.objectClass
            };
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
    }
}
