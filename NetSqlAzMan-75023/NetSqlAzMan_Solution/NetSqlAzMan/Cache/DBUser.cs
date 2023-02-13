using System;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.Cache
{
	/// <summary>
	/// VBastidas: Usuario registrado en la b.d. que sera devuelto por el
	/// servicio de cache del NetSqlAzMan (sin su password por motivos de seguridad)
	/// </summary>
	[DataContract()]
	[Serializable()]
	public class DBUser
	{
		/// <summary>
		/// User id (It'sunique)
		/// </summary>
		[DataMember()]
		public int UserID {
			get;
			set;
		}

		/// <summary>
		/// User name (It's unique)
		/// </summary>
		[DataMember()]
		public string UserName {
			get;
			set;
		}

		/// <summary>
		/// Contains all properties of the user
		/// </summary>
		[DataMember()]
		public string AttributeString {
			get;
			set;
		}
	}
}