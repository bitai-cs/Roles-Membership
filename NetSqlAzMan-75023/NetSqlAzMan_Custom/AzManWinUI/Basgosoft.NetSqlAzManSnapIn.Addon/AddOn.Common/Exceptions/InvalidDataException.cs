using System;
using System.Collections.Generic;

using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Common.Exceptions
{
	[global::System.Serializable]
	public class InvalidDataException : IdentifiedException
	{
		public InvalidDataException()
			: base()
		{
		}

		public InvalidDataException(string message)
			: base(message)
		{
		}


		public InvalidDataException(string message, Exception inner)
			: base(message, inner)
		{
		}


		protected InvalidDataException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}

	}

}
