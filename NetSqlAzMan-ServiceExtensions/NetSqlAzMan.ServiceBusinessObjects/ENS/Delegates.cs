using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects.ENS
{
	#region Attribute<OWNER> Delegates
	/// <summary>
	/// Delegate for AttributeCreated events.
	/// </summary>
	/// <param name="owner">The Owner.</param>
	/// <param name="attributeCreated">The Attribute created.</param>
	//public delegate void AttributeCreatedDelegate<OWNER>(OWNER owner, AzManAttribute<OWNER> attributeCreated);

	/// <summary>
	/// Delegate for AttributeDeleted events.
	/// </summary>
	/// <param name="owner">The owner Authorization.</param>
	/// <param name="key">The key of the AuthorizationAttribute.</param>
	public delegate void AttributeDeletedDelegate<OWNER>(OWNER owner, string key);

	/// <summary>
	/// Delegate for AttributeUpdated events.
	/// </summary>
	/// <param name="attribute">The attribute object.</param>
	/// <param name="oldKey">The old key.</param>
	/// <param name="oldValue">The old value.</param>
	//public delegate void AttributeUpdatedDelegate<OWNER>(AzManAttribute<OWNER> attribute, string oldKey, string oldValue);
	#endregion Attribute<OWNER> Delegates	

	#region Authorization Delegates
	/// <summary>
	/// Delegate for Authorization Deleted events.
	/// </summary>
	/// <param name="ownerItem">The owner Item.</param>
	/// <param name="owner">Owner Sid of the authorization.</param>
	/// <param name="sid">Sid of the authorization.</param>
	public delegate void AuthorizationDeletedDelegate(AzManItem ownerItem, AzManSid owner, AzManSid sid);
	/// <summary>
	/// Delegate for Authorization Updated events.
	/// </summary>
	/// <param name="authorization">The authorization updated object.</param>
	/// <param name="oldOwner">The old Owner.</param>
	/// <param name="oldOwnerSidWhereDefined">The old OwnerSidWhereDefined.</param>
	/// <param name="oldSid">The old Sid.</param>
	/// <param name="oldSidWhereDefined">The old SidWhereDefined.</param>
	/// <param name="oldAuthorizationType">The old AuthorizationType.</param>
	/// <param name="oldValidFrom">The old ValidFrom.</param>
	/// <param name="oldValidTo">The old ValidTo.</param>
	public delegate void AuthorizationUpdatedDelegate(AzManAuthorization authorization, AzManSid oldOwner, WhereDefined oldOwnerSidWhereDefined, AzManSid oldSid, WhereDefined oldSidWhereDefined, AuthorizationType oldAuthorizationType, DateTime? oldValidFrom, DateTime? oldValidTo);
	#endregion Authorization Delegates

	#region Item Delegates
	/// <summary>
	/// Delegate for Item Deleted events.
	/// </summary>
	/// <param name="applicationContainer">The Application Container.</param>
	/// <param name="itemName">Name of the itemName.</param>
	/// <param name="itemType">Type of the itemName.</param>
	public delegate void ItemDeletedDelegate(AzManApplication applicationContainer, string itemName, ItemType itemType);
	/// <summary>
	/// Delegate for Item Renamed events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="oldName">The old name.</param>
	public delegate void ItemRenamedDelegate(AzManItem item, string oldName);
	/// <summary>
	/// Delegate for Item Updated events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="oldDescription">The old description.</param>
	public delegate void ItemUpdatedDelegate(AzManItem item, string oldDescription);
	/// <summary>
	/// Delegate for Biz Rule Updated events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="oldBizRule">The old Biz Rule.</param>
	public delegate void BizRuleUpdatedDelegate(AzManItem item, string oldBizRule);
	/// <summary>
	/// Delegate for AuthorizationCreated events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="authorizationCreated">The Authorization created.</param>
	public delegate void AuthorizationCreatedDelegate(AzManItem item, AzManAuthorization authorizationCreated);
	/// <summary>
	/// Delegate for DelegationCreated events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="delegationCreated">The Delegation created.</param>
	public delegate void DelegateCreatedDelegate(AzManItem item, AzManAuthorization delegationCreated);
	/// <summary>
	/// Delegate for DelegationDeleted events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="delegatingUserSid">The delegating User.</param>
	/// <param name="delegatedUserSid">The delegated User.</param>
	/// <param name="authorizationType">The authorization Type.</param>
	public delegate void DelegateDeletedDelegate(AzManItem item, AzManSid delegatingUserSid, AzManSid delegatedUserSid, RestrictedAuthorizationType authorizationType);
	/// <summary>
	/// Delegate for MemberAdded events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="memberAdded">The member added.</param>
	public delegate void MemberAddedDelegate(AzManItem item, AzManItem memberAdded);
	/// <summary>
	/// Delegate for MemberRemoved events.
	/// </summary>
	/// <param name="item">The itemName.</param>
	/// <param name="memberRemoved">The member removed.</param>
	public delegate void MemberRemovedDelegate(AzManItem item, AzManItem memberRemoved);
	#endregion Item Delegates
}