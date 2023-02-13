using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public enum AzManMode {
		/// <summary>
		/// Administrator Mode
		/// </summary>
		Administrator = 0,
		/// <summary>
		/// Developer Mode
		/// </summary>
		Developer = 1
	}

	public enum GroupType {
		/// <summary>
		/// Basic group
		/// </summary>
		Basic = 0,
		/// <summary>
		/// Dynamic Group (LDAP query)
		/// </summary>
		LDapQuery = 1
	}

	public enum WhereDefined : byte {
		/// <summary>
		/// Defined at store-level
		/// </summary>
		Store = 0,

		/// <summary>
		/// Defined at application-level
		/// </summary>
		Application = 1,

		/// <summary>
		/// Defined at LDAP-level
		/// </summary>
		LDAP = 2,

		/// <summary>
		/// Defined on Local machine
		/// </summary>
		Local = 3,

		/// <summary>
		/// Defined on a Database
		/// </summary>
		Database = 4,
	}

	public enum MemberType {
		StoreGroup,
		ApplicationGroup,
		WindowsNTGroup,
		WindowsNTUser,
		AnonymousSID,
		DatabaseUser
	}

	/// <summary>
	/// Authorization Type
	/// </summary>
	public enum AuthorizationType : byte {
		/// <summary>
		/// Neutral.
		/// </summary>
		Neutral = 0,
		/// <summary>
		/// Allow.
		/// </summary>
		Allow = 1,
		/// <summary>
		/// Deny.
		/// </summary>
		Deny = 2,
		/// <summary>
		/// Allow with delegation
		/// </summary>
		AllowWithDelegation = 3
	}

	/// <summary>
	/// Is the Item Type categorization for Items. An itemName can be of one of these types.
	/// </summary>
	public enum ItemType {
		/// <summary>
		/// A Role itemName can contain: Roles, Tasks, Operations.
		/// </summary>
		/// <remarks>Administrative purpose only. Do not use in the Applications.</remarks>
		Role,
		/// <summary>
		/// A Task itemName can contain: Tasks, Operations.
		/// </summary>
		/// <remarks>Administrative purpose only. Do not use in the Applications.</remarks>
		Task,
		/// <summary>
		/// An Operation can contain: Operations.
		/// </summary>
		/// <remarks>Administrative and Developer purpose. Invoke Operations CheckAccess in the Applications.</remarks>
		Operation,
		/// <summary>
		/// It is not a type. Only to filter queries
		/// </summary>
		All
	}

	/// <summary>
	/// Source Code Language for Biz Rules
	/// </summary>
	public enum BizRuleSourceLanguage : byte {
		/// <summary>
		/// JavaScript
		/// </summary>
		CSharp,
		/// <summary>
		/// Visual Basic Script
		/// </summary>
		VBNet
	}

	/// <summary>
	/// Restricted Authorization Type
	/// </summary>
	public enum RestrictedAuthorizationType : byte {
		/// <summary>
		/// Allow.
		/// </summary>
		Allow = 1,
		/// <summary>
		/// Deny.
		/// </summary>
		Deny = 2
	}	
}
