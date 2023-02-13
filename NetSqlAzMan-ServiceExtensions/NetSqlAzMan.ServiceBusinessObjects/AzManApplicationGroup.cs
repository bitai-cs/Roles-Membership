using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	/// <summary>
	/// Interfaces interface for all Application Groups.
	/// </summary>
	public class AzManApplicationGroup {
		/// <summary>
		/// Default Constructors
		/// </summary>
		public AzManApplicationGroup() {
			this.Members = new List<AzManApplicationGroupMember>();
		}

		/// <summary>
		/// Gets the application group id.
		/// </summary>
		/// <value>The application group id.</value>
		[Key]
		[Required]
		public int ApplicationGroupId { get; set; } //da implementare esplicitamente

		/// <summary>
		/// Gets the application.
		/// </summary>
		/// <value>The application.</value>
		public AzManApplication Application { get; set; }

		/// <summary>
		/// Gets the object owner.
		/// </summary>
		/// <value>The object owner.</value>
		public AzManSid SID { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Gets the LDAP query.
		/// </summary>
		/// <value>The LDAP query.</value>
		public string LDAPQuery { get; set; }

		/// <summary>
		/// Gets the type of the group.
		/// </summary>
		/// <value>The type of the group.</value>
		[Required]
		public GroupType GroupType { get; set; }

		/// <summary>
		/// Gets the members.
		/// </summary>
		/// <value>The members.</value>
		public IEnumerable<AzManApplicationGroupMember> Members { get; set; }

		/// <summary>
		/// Clone Object
		/// </summary>
		/// <returns></returns>
		public AzManApplicationGroup Clone() {
			var _clone = new AzManApplicationGroup {
				Application = this.Application.Clone(),
				ApplicationGroupId = this.ApplicationGroupId,
				Description = this.Description,
				GroupType = this.GroupType,
				LDAPQuery = this.LDAPQuery,
				Name = this.Name,
				SID = this.SID.Clone()
			};

			var _members = new List<AzManApplicationGroupMember>();
			foreach (var _member in this.Members) {
				_members.Add(_member.Clone());
			}
			_clone.Members = _members;

			return _clone;
		}

		#region Members only for Update
		public IEnumerable<AzManGenericMember> MembersToAdd { get; set; }

		public IEnumerable<AzManGenericMember> MembersToRemove { get; set; }

		public IEnumerable<AzManGenericMember> NonMembersToAdd { get; set; }

		public IEnumerable<AzManGenericMember> NonMembersToRemove { get; set; }
		#endregion
	}
}