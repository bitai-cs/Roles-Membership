using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManList<T> : System.Collections.Generic.List<T> {
		public override string ToString() {
			return string.Format("{0} {1}", this.Count, typeof(T).Name);
		}
	}
}
