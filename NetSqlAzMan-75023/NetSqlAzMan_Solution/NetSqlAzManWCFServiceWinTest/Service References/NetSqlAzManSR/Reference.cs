//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetSqlAzManWCFServiceWinTest.NetSqlAzManSR {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NetSqlAzManSR.INetSqlAzManWCFService", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface INetSqlAzManWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INetSqlAzManWCFService/CreateStorageInstance", ReplyAction="http://tempuri.org/INetSqlAzManWCFService/CreateStorageInstanceResponse")]
        NetSqlAzMan.SqlAzManStorage CreateStorageInstance(string connectionString);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INetSqlAzManWCFServiceChannel : NetSqlAzManWCFServiceWinTest.NetSqlAzManSR.INetSqlAzManWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NetSqlAzManWCFServiceClient : System.ServiceModel.ClientBase<NetSqlAzManWCFServiceWinTest.NetSqlAzManSR.INetSqlAzManWCFService>, NetSqlAzManWCFServiceWinTest.NetSqlAzManSR.INetSqlAzManWCFService {
        
        public NetSqlAzManWCFServiceClient() {
        }
        
        public NetSqlAzManWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NetSqlAzManWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetSqlAzManWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NetSqlAzManWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public NetSqlAzMan.SqlAzManStorage CreateStorageInstance(string connectionString) {
            return base.Channel.CreateStorageInstance(connectionString);
        }
    }
}
