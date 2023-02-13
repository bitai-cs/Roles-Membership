using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetSqlAzMan.Interfaces
{
	/// <summary>
	/// Common Interface for All Database Custom Users
	/// </summary>	 
	[ServiceContract(Namespace = "http://NetSqlAzMan/ServiceModel", SessionMode = SessionMode.Required)]
	public partial interface IAzManDBUser
	{
		/// <summary>
		/// Custom Unique identifier of the DB User
		/// </summary>
		[DataMember]
		[Display(Name = "Custom Sid")]
		IAzManSid CustomSid {
			get;
		}
		/// <summary>
		/// Username of the DB User
		/// </summary>
		[DataMember]
		[Display(Name = "Usuario")]
		string UserName {
			get;
		}
		/// <summary>
		/// Gets the custom columns.
		/// </summary>
		/// <value>The custom columns.</value>
		[DataMember]
		[Display(Name = "Datos Personalizados")]
		Dictionary<string, object> CustomColumns {
			get;
		}
	}
}
