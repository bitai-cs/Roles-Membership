using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
    /// <summary>
    /// Interfaces interface for all Application Group Members.
    /// </summary>	
    public class AzManApplicationGroupMember
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AzManApplicationGroupMember() { }

        /// <summary>
        /// Gets the application group member id.
        /// </summary>
        /// <value>The application group member id.</value>
        [Required]
        public int ApplicationGroupMemberId { get; set; } //da implementare esplicitamente

        /// <summary>
        /// Gets the application group.
        /// </summary>
        /// <value>The application group.</value>
        public AzManApplicationGroup ApplicationGroup { get; set; }

        /// <summary>
        /// Gets the object owner.
        /// </summary>
        /// <value>The object owner.</value>
        [Required]
        public AzManSid SID { get; set; }

        /// <summary>
        /// Gets where member is defined.
        /// </summary>
        /// <value>The where defined.</value>
        [Required]
        public WhereDefined WhereDefined { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is member.
        /// </summary>
        /// <value><c>true</c> if this instance is member; otherwise, <c>false</c>.</value>
        public bool IsMember { get; set; }

        /// <summary>
        /// Domain profile where member was loaded
        /// </summary>
        public string DomainProfile { get; set; }

        public string samAccountName { get; set; }

        public string cn { get; set; }

        public string displayName { get; set; }

        public string objectSidString { get; set; }

        public string distinguishedName { get; set; }

        public string objectClass { get; set; }

        public AzManApplicationGroupMember Clone() {
            var _clone = new AzManApplicationGroupMember {
                ApplicationGroup = this.ApplicationGroup.Clone(),
                ApplicationGroupMemberId = this.ApplicationGroupMemberId,
                IsMember = this.IsMember,
                DomainProfile = this.DomainProfile,
                WhereDefined = this.WhereDefined,
                SID = this.SID.Clone(),
                samAccountName = this.samAccountName,
                cn = this.cn,
                displayName = this.displayName,
                objectSidString = this.objectSidString,
                distinguishedName = this.distinguishedName,
                objectClass = this.objectClass
            };

            return _clone;
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