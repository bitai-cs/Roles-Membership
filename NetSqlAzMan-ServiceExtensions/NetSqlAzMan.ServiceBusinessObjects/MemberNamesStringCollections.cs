using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class MemberNamesStringCollections {
		public MemberNamesStringCollections() {
			MembersToAdd = new StringCollection();
			MembersToRemove = new StringCollection();
		}

		public StringCollection MembersToAdd;
		public StringCollection MembersToRemove;
	}
}
