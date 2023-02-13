using System;
using System.Collections.Generic;
using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Common.Exceptions
{
	[global::System.Serializable]
	public class NoRecordAffectedException : IdentifiedException
	{
		public NoRecordAffectedException()
		{
		}
		public NoRecordAffectedException(string message)
			: base(message)
		{
		}
		public NoRecordAffectedException(string message, Exception inner)
			: base(message, inner)
		{
		}
		protected NoRecordAffectedException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
