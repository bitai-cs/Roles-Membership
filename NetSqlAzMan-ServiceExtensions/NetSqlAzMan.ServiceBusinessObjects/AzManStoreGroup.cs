using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	/// <summary>
	/// Interfaces interface for all Store Groups.
	/// </summary>
	public class AzManStoreGroup {
		public AzManStoreGroup() {
			this.Members = new List<AzManStoreGroupMember>();
		}

		/// <summary>
		/// Gets the store group id.
		/// </summary>
		/// <value>The store group id.</value>
		[Key]
		[Required]
		public int StoreGroupId { get; set; }

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
		public GroupType GroupType { get; set; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public AzManStore Store { get; set; }

		/// <summary>
		/// Gets the members.
		/// </summary>
		/// <value>The members.</value>
		public IEnumerable<AzManStoreGroupMember> Members { get; set; }

		#region Public Methods
		public AzManStoreGroup Clone() {
			var _clone = new AzManStoreGroup {
				Description = this.Description,
				GroupType = this.GroupType,
				LDAPQuery = this.LDAPQuery,
				Name = this.Name,
				SID = this.SID.Clone(),
				Store = this.Store.Clone(),
				StoreGroupId = this.StoreGroupId
			};

			List<AzManStoreGroupMember> _list = new List<AzManStoreGroupMember>();
			foreach (var _i in this.Members)
				_list.Add(_i.Clone());

			_clone.Members = _list;

			return _clone;
		}
		#endregion

		#region Members only for Update
		public IEnumerable<AzManGenericMember> MembersToAdd { get; set; }

		public IEnumerable<AzManGenericMember> MembersToRemove { get; set; }

		public IEnumerable<AzManGenericMember> NonMembersToAdd { get; set; }

		public IEnumerable<AzManGenericMember> NonMembersToRemove { get; set; }
		#endregion
	}
}