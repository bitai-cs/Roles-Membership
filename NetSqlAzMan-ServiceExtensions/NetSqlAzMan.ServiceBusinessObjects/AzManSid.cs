using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManSid : IEquatable<AzManSid> {
		private SecurityIdentifier securityIdentifier = null;
		private Guid guid = Guid.Empty;
		private byte[] customSid = null;

		private byte[] _deserializedByinaryValue = null;
		private string _deserializedStringValue = null;

		/// <summary>
		/// Constructor para el deserializador
		/// </summary>
		public AzManSid() {
		}

		/// <summary>
		/// Constructor que usa el metodo Clone
		/// </summary>
		/// <param name="binaryValue"></param>
		/// <param name="stringValue"></param>
		public AzManSid(byte[] binaryValue, string stringValue) {
			_deserializedByinaryValue = binaryValue;
			_deserializedStringValue = stringValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SqlAzManSID"/> class.
		/// </summary>
		/// <param name="sddlForm">The SDDL form.</param>
		public AzManSid(string sddlForm) {
			if (sddlForm.StartsWith("S-1"))
				this.securityIdentifier = new SecurityIdentifier(sddlForm);
			else
				guid = new Guid(sddlForm);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SqlAzManSID"/> class.
		/// </summary>
		/// <param name="sddlForm">The SDDL form.</param>
		/// <param name="customSid">if set to <c>true</c> [custom sid].</param>
		public AzManSid(string sddlForm, bool customSid) {
			Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
			if (customSid) {
				Guid g;
				if (sddlForm.StartsWith("S-1")) {
					this.securityIdentifier = new SecurityIdentifier(sddlForm);
				}
				else if (IsGuid(sddlForm, out g)) {
					this.customSid = g.ToByteArray();
				}
				else {
					int discarded;
					this.customSid = Utilities.HexEncoding.GetBytes(sddlForm, out discarded);
				}
			}
			else {
				if (sddlForm.StartsWith("S-1"))
					this.securityIdentifier = new SecurityIdentifier(sddlForm);
				else
					guid = new Guid(sddlForm);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SqlAzManSID"/> class.
		/// </summary>
		/// <param name="binaryForm">The binary form.</param>
		/// <remarks>Valid only for SecurityIdentifier(s) and Guid(s)</remarks>
		public AzManSid(byte[] binaryForm) {
			if (binaryForm == null) {
				this.guid = Guid.Empty;
			}
			else {
				if (binaryForm.Length == 16 && binaryForm[0] != 1)
					this.guid = new Guid(binaryForm);
				else
					this.securityIdentifier = new SecurityIdentifier(binaryForm, 0);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SqlAzManSID"/> class.
		/// </summary>
		/// <param name="binaryForm">The binary form.</param>
		/// <param name="customSid">if set to <c>true</c> [custom sid].</param>
		/// <remarks>Valid only for custom Sid (DB Users)</remarks>
		public AzManSid(byte[] binaryForm, bool customSid) {
			if (customSid) {
				if (binaryForm.Length > 3 && binaryForm[0] == 48 && binaryForm[1] == 0 && binaryForm[2] == 120 && binaryForm[3] == 0) {
					//Comes from SqlBinary: 0xFFFFFFFF
					byte[] sid = new byte[binaryForm.Length - 4];
					for (int i = 4; i < binaryForm.Length; i++) {
						sid[i - 4] = binaryForm[i];
					}
					this.customSid = sid;
				}
				else {
					this.customSid = binaryForm;
				}
			}
			else {
				if (binaryForm.Length == 16 && binaryForm[0] != 1)
					this.guid = new Guid(binaryForm);
				else
					this.securityIdentifier = new SecurityIdentifier(binaryForm, 0);
			}
		}

		/// <summary>
		/// Gets the binary value.
		/// </summary>
		/// <value>The binary value.</value>
		[Display(Name = "Sid (Binario)")]
		public byte[] BinaryValue {
			get {
				if (_deserializedByinaryValue != null) { //AzManSid fue creado por el deserializador JSON
					return _deserializedByinaryValue;
				}

				if (this.securityIdentifier != null) {
					byte[] result = new byte[this.securityIdentifier.BinaryLength];
					this.securityIdentifier.GetBinaryForm(result, 0);
					return result;
				}
				else if (this.guid != Guid.Empty) {
					return this.guid.ToByteArray();
				}
				else {
					return this.customSid;
				}
			}
			set { /*Solo para ser llamado por el deserializador JSON*/
				_deserializedByinaryValue = value;
			}
		}

		/// <summary>
		/// Gets the string value.
		/// </summary>
		/// <value>The string value.</value>
		[Display(Name = "Sid")]
		public string StringValue {
			get {
				if (_deserializedStringValue != null) { //AzManSid fue creado por el deserializador JSON
					return _deserializedStringValue;
				}

				if (this.securityIdentifier != null)
					return this.securityIdentifier.Value;
				else if (this.guid != Guid.Empty)
					return this.guid.ToString();
				else
					return Utilities.HexEncoding.ToString(this.customSid);
			}
			set {  /*Solo para ser llamado por el deserializador JSON*/
				_deserializedStringValue = value;
			}
		}

		/// <summary>
		/// News the SQL az man owner.
		/// </summary>
		/// <returns></returns>
		public static AzManSid NewAzManSid() {
			Guid guid;
			bool isGood = false;
			AzManSid result = null;
			while (!isGood) {
				try {
					do {
						guid = Guid.NewGuid();
					} while (guid.ToByteArray()[0] == 1);
					result = new AzManSid(Guid.NewGuid().ToByteArray());
					isGood = true;
				}
				catch {
					isGood = false;
				}
			}
			return result;
		}

		public string BinaryValueToBase64String() {
			return Convert.ToBase64String(this.BinaryValue);
		}

		internal bool IsGuid(string candidate, out Guid output) {
			Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
			bool isValid = false;
			output = Guid.Empty;
			if (candidate != null) {
				if (isGuid.IsMatch(candidate)) {
					output = new Guid(candidate);
					isValid = true;
				}
			}
			return isValid;
		}

		/// <summary>
		/// Operator == for SqlAzManSID.
		/// </summary>
		/// <param name="sid">The sid.</param>
		/// <param name="binaryForm">The binary form.</param>
		/// <returns></returns>
		public static bool operator ==(AzManSid sid, byte[] binaryForm) {
			byte[] binarySid = sid.BinaryValue;
			if (binaryForm.Length != binarySid.Length) {
				return false;
			}
			for (int i = 0; i < binaryForm.Length; i++) {
				if (binarySid[i] != binaryForm[i])
					return false;
			}
			return true;
		}

		/// <summary>
		/// Operator != for SqlAzManSID.
		/// </summary>
		/// <param name="sid">The owner.</param>
		/// <param name="binaryForm">The binary form.</param>
		/// <returns></returns>
		public static bool operator !=(AzManSid sid, byte[] binaryForm) {
			return !(sid == binaryForm);
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="sid1">The sid1.</param>
		/// <param name="sid2">The sid2.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(AzManSid sid1, AzManSid sid2) {
			return sid1.Equals(sid2.BinaryValue);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="sid1">The sid1.</param>
		/// <param name="sid2">The sid2.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(AzManSid sid1, AzManSid sid2) {
			return !(sid1.Equals(sid2.BinaryValue));
		}

		/// <summary>
		/// Implicit operators the specified windows identity.
		/// </summary>
		/// <param name="windowsIdentity">The windows identity.</param>
		/// <returns></returns>
		//public static implicit operator AzManSid(WindowsIdentity windowsIdentity) {
		//	return new AzManSid(windowsIdentity.User);
		//}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
		/// </returns>
		public override bool Equals(object obj) {
			if (Object.ReferenceEquals(obj, null))
				return false;
			AzManSid sqlazmansid = obj as AzManSid;
			if (!object.ReferenceEquals(sqlazmansid, null))
				return this.Equals(sqlazmansid);
			else
				return base.Equals(obj);
		}

		/// <summary>
		/// Equalses the specified sqlazman sid.
		/// </summary>
		/// <param name="sqlazmanSid">The sqlazman sid.</param>
		/// <returns></returns>
		public bool Equals(AzManSid sqlazmanSid) {
			if (Object.ReferenceEquals(sqlazmanSid, null))
				return false;
			if (this.securityIdentifier != null)
				return this.securityIdentifier.Equals(sqlazmanSid.securityIdentifier);
			else if (this.guid != Guid.Empty)
				return this.guid.Equals(sqlazmanSid.guid);
			else {
				if (this.customSid.Length != sqlazmanSid.customSid.Length)
					return false;
				for (int i = 0; i < this.customSid.Length; i++) {
					if (this.customSid[i] != sqlazmanSid.customSid[i])
						return false;
				}
				return true;
			}
		}

		/// <summary>
		/// Equalses the specified GUID.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <returns></returns>
		public bool Equals(Guid guid) {
			return this.guid.Equals(guid);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override int GetHashCode() {
			return this.StringValue.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString() {
			return this.StringValue;
		}

		public AzManSid Clone() {
			return new AzManSid(this.BinaryValue, this.StringValue);
		}
	}
}
