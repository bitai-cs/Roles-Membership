using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSqlAzManSnapIn.AddOn.Objects
{
	public abstract class Base : IDisposable
	{
		#region "Protected  fields"

		protected Exception ptexceHandled;

		#endregion

		#region IDisposable Members

		public abstract void Dispose();

		#endregion
	}
}