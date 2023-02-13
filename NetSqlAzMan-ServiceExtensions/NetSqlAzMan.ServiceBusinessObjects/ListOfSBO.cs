using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class ListOfSBO<T> : List<T> {
		public override string ToString() {
			if (this.Count.Equals(0)) {
				return string.Empty;
			}
			else {
				string _return = null;

				foreach (T item in this)
					_return += item.ToString() + " | ";

				_return.Substring(0, _return.Length - 3);

				return _return;
			}
		}
	}
}
