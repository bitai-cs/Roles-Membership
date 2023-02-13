using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace NetSqlAzMan.Interfaces
{
	/// <summary>
	/// Represents a Security IDentifier (SID)
	/// </summary>
	[ServiceContract(Namespace = "http://NetSqlAzMan/ServiceModel", SessionMode = SessionMode.Required)]
	public interface IAzManSid : IEquatable<IAzManSid>
	{
		/// <summary>
		/// Gets the binary value.
		/// </summary>
		/// <value>The binary value.</value>
		[DataMember]
		[Display(Name = "Sid (Binario)")]
		byte[] BinaryValue {
			get;
		}
		/// <summary>
		/// Gets the string value.
		/// </summary>
		/// <value>The string value.</value>
		[DataMember]
		[Display(Name = "Sid")]
		string StringValue {
			get;
		}
	}
}
