using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects
{
	public class AzManAuthorizationInfo
	{
		public AuthorizationType AuthorizationType { get; set; }
		public IEnumerable<KeyValuePair<string, string>> AuthorizationAttributes { get; set; }
	}
}
