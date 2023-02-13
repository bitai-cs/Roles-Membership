using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManGenericMemberCollection : List<AzManGenericMember> {
		public bool Remove(string objectSid) {
			foreach (AzManGenericMember m in this) {
				if (m.sid.StringValue == objectSid) {
					this.Remove(m);
					return true;
				}
			}
			return false;
		}

		public bool ContainsByObjectSid(string objectSid) {
			foreach (AzManGenericMember m in this) {
				if (m.sid.StringValue == objectSid) {
					return true;
				}
			}
			return false;
		}

		public bool ContainsByName(string name) {
			foreach (AzManGenericMember m in this) {
				if (String.Compare(m.Name, name, true) == 0) {
					return true;
				}
			}
			return false;
		}
	}
}
