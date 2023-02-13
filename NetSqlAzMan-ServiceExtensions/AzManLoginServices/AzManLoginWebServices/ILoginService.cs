using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AzManLoginWebServices
{
	[ServiceContract]
	public interface ILoginService
	{
		[OperationContract]
		bool Test(string input, out string output);

		[OperationContract]
		bool StartLogin(string userName, string pwd, string appName, out DirectSvcRef.DBUser user, string store, string app, string requiredItem, out DirectSvcRef.AuthorizationType aut, out LoginInfo login, out string attributeString);

		[OperationContract]
		bool StartLoginEnc(string encodedUserName, string appName, out DirectSvcRef.DBUser user, string store, string app, string requiredItem, out DirectSvcRef.AuthorizationType aut, out LoginInfo login, out string attributeString);

		[OperationContract]
		bool CreateLogin(string userName, string password, string clientApplication, string azManStore, string azManApplication, string azManItem, out DirectSvcRef.DBUser dbUser, out LoginInfo loginInfo, out DirectSvcRef.AuthorizationType authorizationType, out string outputString);

		[OperationContract]
		bool GetLogin(string loginId, string userName, string appName, out DirectSvcRef.DBUser user, out LoginInfo login, out string attributeString);

		[OperationContract]
		bool GetLoginByIdAndUser(string loginId, string userName, out DirectSvcRef.DBUser dbUser, out LoginInfo loginInfo, out string outputString);

		[OperationContract]
		bool RevalidateLogin(string loginId, string userName, string pwd, string appName, out DirectSvcRef.DBUser user, out LoginInfo login, out string attributeString);

		[OperationContract]
		bool StartLogOut(LoginInfo login, out string attributeString);

		[OperationContract]
		bool CheckLoginAccess(string store, string app, string item, LoginInfo loginInfo, out LoginStatusEnum loginStatus, out DirectSvcRef.AuthorizationType aut, out string attributeString);

		[OperationContract]
		bool CheckLoginStatusAndAuthorization(string azManStore, string azManApplication, string azManItem, LoginInfo loginInfo, out LoginStatusEnum loginStatus, out DirectSvcRef.AuthorizationType authorizationType, out string outputString);

		[OperationContract]
		bool ChangePwd(LoginInfo login, string current, string renewed, string confirmation, out string statusMessages);

		[OperationContract]
		bool ChangePwdEx(LoginInfo login, string current, string renewed, string confirmation, out string statusType, out string statusMessage, out string statusTrace);
	}
}
