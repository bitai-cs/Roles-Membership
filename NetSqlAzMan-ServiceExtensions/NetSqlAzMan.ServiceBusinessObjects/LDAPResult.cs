using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public partial class LDAPResult {
		public LDAPResult() {
			//Nothing
		}

		public string ObjectGuid { get; set; }
		public byte[] ObjectGuidBytes { get; set; }
		public string ObjectSid { get; set; }
		public byte[] ObjectSidBytes { get; set; }
		public string DisplayName { get; set; }
		public string TelephoneNumber { get; set; }
		public string samAccountName { get; set; }
		/// <summary>
		/// Manager: DistinguishedName of the manager
		/// </summary>
		public string Manager { get; set; }
		public string DistinguishedName { get; set; }
		public string Title { get; set; }
		public string Department { get; set; }
		public string GivenName { get; set; }
		public string SN { get; set; }
		public string CN { get; set; }
		public string DomainProfile { get; set; }
		public string DirectoryEntryPath { get; set; }
		public bool? ResultFromGC { get; set; }
		public string UserPrincipalName { get; set; }
		public string C { get; set; }
		public string Company { get; set; }
		public Nullable<DateTime> CreateTimeStamp { get; set; }
		public string defaultClassStore { get; set; }
		public string Description { get; set; }
		public bool? IsDeleted { get; set; }
		public long? LastLogon { get; set; }
		public string ObjectCategory { get; set; }
		public string[] ObjectClass { get; set; }
		public string[] Member { get; set; }
		public string[] MemberOf { get; set; }
		public string Mail { get; set; }

		//#region IComparable Members
		//public int CompareTo(object obj) {
		//	if (obj is LDAPResult) {
		//		LDAPResult u2 = (LDAPResult)obj;
		//		return _displayName.CompareTo(u2.displayName);
		//	}
		//	else
		//		throw new ArgumentException("Object is not a User.");
		//}
		//#endregion
	}
}