using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Common.Exceptions
{
	[global::System.Serializable]
	public class IdentifiedException: Exception 
	{
		public IdentifiedException()
			: base()
		{
		}

		public IdentifiedException(string message)
			: base(message)
		{
		}

		public IdentifiedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected IdentifiedException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
