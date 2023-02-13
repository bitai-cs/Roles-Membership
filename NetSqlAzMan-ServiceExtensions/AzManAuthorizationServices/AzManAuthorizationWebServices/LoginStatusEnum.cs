using System.Runtime.Serialization;

namespace AzManLoginWebServices
{
	[DataContract]
	public enum LoginStatusEnum
	{
		[EnumMember]
		Unknown = 0,

		[EnumMember]
		LoggedIn = 1,

		[EnumMember]
		Expired = 2,

		[EnumMember]
		LoggedOut = 3
	}
}
