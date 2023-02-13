using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	/// <summary>
	/// Represent all AzMan Items (Role, Task, Operation)
	/// </summary>
	public class AzManItem {
		public AzManItem() {
			this.Members = new List<AzManItem>();
			this.ItemsWhereIAmAMember = new List<AzManItem>();
			this.Attributes = new List<AzManItemAttribute>();
			this.Authorizations = new System.Collections.ObjectModel.ReadOnlyCollection<AzManAuthorization>(new List<AzManAuthorization>());
			this.MembersToAddOrRemove = new MemberNamesStringCollections();
		}

		/// <summary>
		/// Gets the itemName id.
		/// </summary>
		/// <value>The itemName id.</value>
		[Key]
		[Required]
		public int ItemId { get; set; }

		/// <summary>
		/// Gets the application.
		/// </summary>
		/// <value>The application.</value>
		public AzManApplication Application { get; set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Gets the biz rule.
		/// </summary>
		/// <value>The biz rule.</value>
		public string BizRuleSource { get; set; }

		/// <summary>
		/// Gets the biz rule script language.
		/// </summary>
		/// <value>The biz rule script language.</value>
		public BizRuleSourceLanguage? BizRuleSourceLanguage { get; set; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>
		/// Is the type of the Item (Role, Task or Operation).
		/// </summary>
		[Required]
		public ItemType ItemType { get; set; }

		/// <summary>
		/// Gets the members.
		/// </summary>
		/// <value>The members.</value>
		public IEnumerable<AzManItem> Members { get; set; }

		/// <summary>
		/// Gets the items where I am A member.
		/// </summary>
		/// <value>The items where I am A member.</value>
		public IEnumerable<AzManItem> ItemsWhereIAmAMember { get; set; }

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <value>The attributes.</value>
		public IEnumerable<AzManItemAttribute> Attributes { get; set; }

		/// <summary>
		/// Gets the authorizations.
		/// </summary>
		/// <value>The authorizations.</value>
		public System.Collections.ObjectModel.ReadOnlyCollection<AzManAuthorization> Authorizations { get; set; }

		/// <summary>
		/// Colecciones de los nombres de los miembros para agregar o remover
		/// </summary>
		public MemberNamesStringCollections MembersToAddOrRemove { get; set; }

		/// <summary>
		/// Clone object
		/// </summary>
		/// <returns></returns>
		public AzManItem Clone() {
			var _clone = new AzManItem() {
				ItemId = this.ItemId,
				Name = this.Name,
				Description = this.Description,
				ItemType = this.ItemType,
				Application = this.Application.Clone(),
				BizRuleSource = this.BizRuleSource,
				BizRuleSourceLanguage = this.BizRuleSourceLanguage
			};

			var _attributes = new List<AzManItemAttribute>();
			foreach (var att in this.Attributes) {
				_attributes.Add(att.Clone());
			}
			_clone.Attributes = _attributes;

			var _members = new List<AzManItem>();
			foreach (var _member in this.Members) {
				_members.Add(_member.Clone());
			}
			_clone.Members = _members;

			var _parentMembers = new List<AzManItem>();
			foreach (var _parentMmember in this.ItemsWhereIAmAMember) {
				_parentMembers.Add(_parentMmember.Clone());
			}
			_clone.ItemsWhereIAmAMember = _parentMembers;

			var _auths = new List<AzManAuthorization>();
			foreach (var _auth in this.Authorizations) {
				_auths.Add(_auth.Clone());
			}
			_clone.Authorizations = new System.Collections.ObjectModel.ReadOnlyCollection<AzManAuthorization>(_auths);

			return _clone;
		}

		/// <summary>
		/// Get tha parent Application name 
		/// </summary>
		/// <returns></returns>
		public string GetApplicationName() {
			return this.Application.Name;
		}

		/// <summary>
		/// Get parent Store name
		/// </summary>
		/// <returns></returns>
		public string GetStoreName() {
			return this.Application.Store.Name;
		}

		///// <summary>
		///// Occurs after an Attribute object has been Created.
		///// </summary>
		//public event ENS.AttributeCreatedDelegate<AzManItem> ItemAttributeCreated;

		/// <summary>
		/// Occurs after a SqlAzManItem object has been Deleted.
		/// </summary>
		public event ENS.ItemDeletedDelegate ItemDeleted;

		/// <summary>
		/// Occurs after a SqlAzManItem object has been Updated.
		/// </summary>
		public event ENS.ItemUpdatedDelegate ItemUpdated;

		/// <summary>
		/// Occurs after a SqlAzManItem Biz Rule has been Updated.
		/// </summary>
		public event ENS.BizRuleUpdatedDelegate BizRuleUpdated;

		/// <summary>
		/// Occurs after a SqlAzManItem object has been Renamed.
		/// </summary>
		public event ENS.ItemRenamedDelegate ItemRenamed;

		/// <summary>
		/// Occurs after an Authorization object has been Created.
		/// </summary>
		public event ENS.AuthorizationCreatedDelegate AuthorizationCreated;

		/// <summary>
		/// Occurs after a Delegated has been Created.
		/// </summary>
		public event ENS.DelegateCreatedDelegate DelegateCreated;

		/// <summary>
		/// Occurs after a Delegate has been Deleted.
		/// </summary>
		public event ENS.DelegateDeletedDelegate DelegateDeleted;

		/// <summary>
		/// Occurs after an Item object has been Added as a member Item.
		/// </summary>
		public event ENS.MemberAddedDelegate MemberAdded;

		/// <summary>
		/// Occurs after an Item object has been Removed as a member Item.
		/// </summary>
		public event ENS.MemberRemovedDelegate MemberRemoved;

		public static byte[] GetSqlBinarySid(byte[] binarySid) {
			//if (binarySid.Count() != 28)
			//	throw new ArgumentOutOfRangeException("binarySid");

			//byte[] binarySid = new byte[sid.BinaryLength];
			//sid.GetBinaryForm(binarySid, 0);

			byte[] result = new byte[85];
			int j = 0;
			for (int i = 85 - binarySid.Length; i < 85; i++) {
				result[i] = binarySid[j++];
			}
			return result;
		}
	}
}
