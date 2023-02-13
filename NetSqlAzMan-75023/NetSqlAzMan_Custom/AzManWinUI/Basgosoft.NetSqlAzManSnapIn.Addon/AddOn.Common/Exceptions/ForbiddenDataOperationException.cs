using System;
using System.Collections.Generic;

using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Common.Exceptions
{
	[global::System.Serializable]
	public class ForbiddenDataOperationException : IdentifiedException
	{
		public ForbiddenDataOperationException()
			: base()
		{
		}

		public ForbiddenDataOperationException(string message)
			: base(message)
		{
		}

		public ForbiddenDataOperationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected ForbiddenDataOperationException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

	}

}