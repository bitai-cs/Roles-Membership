using System;
using System.DirectoryServices;
using System.Security.Principal;

namespace NetSqlAzMan.SnapIn.DirectoryServices.ADObjectPicker
{
	/// <summary>
	/// Summary description for ADObject.
	/// </summary>
	public struct ADObject
	{
		private string className;
		private string name;
		private string upn;
		private string aDSPath;

		/// <summary>
		/// Gets the name of the class.
		/// </summary>
		/// <value>The name of the class.</value>
		public string ClassName {
			get {
				return className;
			}
			set {
				className = value;
			}
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		/// <summary>
		/// Gets the UPN.
		/// </summary>
		/// <value>The UPN.</value>
		public string UPN {
			get {
				return upn;
			}
			set {
				upn = value;
			}
		}

		/// <summary>
		/// Gets the ADS path.
		/// </summary>
		/// <value>The ADS path.</value>
		public string ADSPath {
			get {
				return this.aDSPath;
			}
			set {
				this.aDSPath = value;
			}
		}

		/// <summary>
		/// Gets the object owner.
		/// </summary>
		/// <value>The object owner.</value>
		public string Sid {
			get {
				if (!string.IsNullOrEmpty(_ldapSid))
					return _ldapSid;

				//System.Windows.Forms.MessageBox.Show(string.Format("{0}-{1}-{2}-{3}", this.ClassName, this.Name, this.UPN, this.ADSPath));
				if (!String.IsNullOrEmpty(this.upn)) {
					NTAccount account = new NTAccount(this.upn);
					SecurityIdentifier sid = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
					return sid.Value;
				}
				else if (!String.IsNullOrEmpty(this.aDSPath)) {
					if (this.aDSPath.StartsWith("WinNT://NT AUTHORITY/", StringComparison.CurrentCultureIgnoreCase)) {
						//System.Windows.Forms.MessageBox.Show(this.aDSPath.Remove(0, 21));
						NTAccount account = new NTAccount(this.aDSPath.Remove(0, 21)); //Remove "WinNT://NT AUTHORITY/"
						SecurityIdentifier sid = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
						return sid.Value;
					}
					else if (this.aDSPath.StartsWith("WinNT://", StringComparison.CurrentCultureIgnoreCase) && this.aDSPath.Substring(8).IndexOf('/') == -1) {
						//System.Windows.Forms.MessageBox.Show(this.aDSPath.Substring(8).Replace("/", "\\"));
						NTAccount account = new NTAccount(this.aDSPath.Substring(8).Replace("/", "\\"));
						SecurityIdentifier sid = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
						return sid.Value;
					}
					//System.Windows.Forms.MessageBox.Show(this.aDSPath);
					DirectoryEntry de = new DirectoryEntry(this.aDSPath);
					de.RefreshCache();
					return new SecurityIdentifier((byte[])de.Properties["objectSid"][0], 0).Value;
				}
				else // if (!String.IsNullOrEmpty(this.name))
                {
					System.Windows.Forms.MessageBox.Show(this.name);
					NTAccount account = new NTAccount(this.name);
					SecurityIdentifier sid = (SecurityIdentifier)account.Translate(typeof(SecurityIdentifier));
					return sid.Value;
				}
			}
		}

		#region ---PERSONALIZADO---
		private string _ldapSid;
		public string LDAPSid {
			set {
				_ldapSid = value;
			}
			get {
				return _ldapSid;
			}
		}

		private string _ldapDomain;
		public string LDAPDomain {
			set {
				_ldapDomain = value;
			}
			get {
				return _ldapDomain;
			}
		}

		public bool IsGroup {
			get {
				if (this.Name.ToLower().Equals("group"))
					return true;
				else
					return false;
			}
		}
		#endregion
	}
}
