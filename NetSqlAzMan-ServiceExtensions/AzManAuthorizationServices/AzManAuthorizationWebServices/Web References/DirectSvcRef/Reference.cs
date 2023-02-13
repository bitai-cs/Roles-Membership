﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18444.
// 
#pragma warning disable 1591

namespace AzManLoginWebServices.DirectSvcRef {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IDirectService", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(KeyValuePairOfstringanyType[]))]
    public partial class DirectService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback DirectTestOperationCompleted;
        
        private System.Threading.SendOrPostCallback DirectGetDBUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback DirectValidatePasswordOperationCompleted;
        
        private System.Threading.SendOrPostCallback DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public DirectService() {
            this.Url = "http://logindemo.sise.edu.pe:8181/AzManWebServices/DirectService.svc";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event DirectTestCompletedEventHandler DirectTestCompleted;
        
        /// <remarks/>
        public event DirectGetDBUserCompletedEventHandler DirectGetDBUserCompleted;
        
        /// <remarks/>
        public event DirectValidatePasswordCompletedEventHandler DirectValidatePasswordCompleted;
        
        /// <remarks/>
        public event DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventHandler DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IDirectService/DirectTest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DirectTest([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string input, out bool DirectTestResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool DirectTestResultSpecified, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] out string output) {
            object[] results = this.Invoke("DirectTest", new object[] {
                        input});
            DirectTestResult = ((bool)(results[0]));
            DirectTestResultSpecified = ((bool)(results[1]));
            output = ((string)(results[2]));
        }
        
        /// <remarks/>
        public void DirectTestAsync(string input) {
            this.DirectTestAsync(input, null);
        }
        
        /// <remarks/>
        public void DirectTestAsync(string input, object userState) {
            if ((this.DirectTestOperationCompleted == null)) {
                this.DirectTestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDirectTestOperationCompleted);
            }
            this.InvokeAsync("DirectTest", new object[] {
                        input}, this.DirectTestOperationCompleted, userState);
        }
        
        private void OnDirectTestOperationCompleted(object arg) {
            if ((this.DirectTestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DirectTestCompleted(this, new DirectTestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IDirectService/DirectGetDBUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public DBUser DirectGetDBUser([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string userName) {
            object[] results = this.Invoke("DirectGetDBUser", new object[] {
                        userName});
            return ((DBUser)(results[0]));
        }
        
        /// <remarks/>
        public void DirectGetDBUserAsync(string userName) {
            this.DirectGetDBUserAsync(userName, null);
        }
        
        /// <remarks/>
        public void DirectGetDBUserAsync(string userName, object userState) {
            if ((this.DirectGetDBUserOperationCompleted == null)) {
                this.DirectGetDBUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDirectGetDBUserOperationCompleted);
            }
            this.InvokeAsync("DirectGetDBUser", new object[] {
                        userName}, this.DirectGetDBUserOperationCompleted, userState);
        }
        
        private void OnDirectGetDBUserOperationCompleted(object arg) {
            if ((this.DirectGetDBUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DirectGetDBUserCompleted(this, new DirectGetDBUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IDirectService/DirectValidatePassword", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DirectValidatePassword([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string userName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string password, out bool DirectValidatePasswordResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool DirectValidatePasswordResultSpecified) {
            object[] results = this.Invoke("DirectValidatePassword", new object[] {
                        userName,
                        password});
            DirectValidatePasswordResult = ((bool)(results[0]));
            DirectValidatePasswordResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void DirectValidatePasswordAsync(string userName, string password) {
            this.DirectValidatePasswordAsync(userName, password, null);
        }
        
        /// <remarks/>
        public void DirectValidatePasswordAsync(string userName, string password, object userState) {
            if ((this.DirectValidatePasswordOperationCompleted == null)) {
                this.DirectValidatePasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDirectValidatePasswordOperationCompleted);
            }
            this.InvokeAsync("DirectValidatePassword", new object[] {
                        userName,
                        password}, this.DirectValidatePasswordOperationCompleted, userState);
        }
        
        private void OnDirectValidatePasswordOperationCompleted(object arg) {
            if ((this.DirectValidatePasswordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DirectValidatePasswordCompleted(this, new DirectValidatePasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IDirectService/DirectCheckAccessForDatabaseUsersWithoutAttribu" +
            "tesRetrieve", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string store, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string app, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string item, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string DBuserSSid, System.DateTime validFor, [System.Xml.Serialization.XmlIgnoreAttribute()] bool validForSpecified, bool operationsOnly, [System.Xml.Serialization.XmlIgnoreAttribute()] bool operationsOnlySpecified, [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.datacontract.org/2004/07/System.Collections.Generic", IsNullable=false)] KeyValuePairOfstringanyType[] contextParameters, out AuthorizationType DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResultSpecified) {
            object[] results = this.Invoke("DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve", new object[] {
                        store,
                        app,
                        item,
                        DBuserSSid,
                        validFor,
                        validForSpecified,
                        operationsOnly,
                        operationsOnlySpecified,
                        contextParameters});
            DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResult = ((AuthorizationType)(results[0]));
            DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveAsync(string store, string app, string item, string DBuserSSid, System.DateTime validFor, bool validForSpecified, bool operationsOnly, bool operationsOnlySpecified, KeyValuePairOfstringanyType[] contextParameters) {
            this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveAsync(store, app, item, DBuserSSid, validFor, validForSpecified, operationsOnly, operationsOnlySpecified, contextParameters, null);
        }
        
        /// <remarks/>
        public void DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveAsync(string store, string app, string item, string DBuserSSid, System.DateTime validFor, bool validForSpecified, bool operationsOnly, bool operationsOnlySpecified, KeyValuePairOfstringanyType[] contextParameters, object userState) {
            if ((this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted == null)) {
                this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted);
            }
            this.InvokeAsync("DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieve", new object[] {
                        store,
                        app,
                        item,
                        DBuserSSid,
                        validFor,
                        validForSpecified,
                        operationsOnly,
                        operationsOnlySpecified,
                        contextParameters}, this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted, userState);
        }
        
        private void OnDirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveOperationCompleted(object arg) {
            if ((this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompleted(this, new DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/NetSqlAzMan.Cache")]
    public partial class DBUser {
        
        private string attributeStringField;
        
        private int userIDField;
        
        private bool userIDFieldSpecified;
        
        private string userNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string AttributeString {
            get {
                return this.attributeStringField;
            }
            set {
                this.attributeStringField = value;
            }
        }
        
        /// <remarks/>
        public int UserID {
            get {
                return this.userIDField;
            }
            set {
                this.userIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UserIDSpecified {
            get {
                return this.userIDFieldSpecified;
            }
            set {
                this.userIDFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/System.Collections.Generic")]
    public partial class KeyValuePairOfstringanyType {
        
        private string keyField;
        
        private object valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public object value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://NetSqlAzMan/ServiceModel")]
    public enum AuthorizationType {
        
        /// <remarks/>
        Neutral,
        
        /// <remarks/>
        Allow,
        
        /// <remarks/>
        Deny,
        
        /// <remarks/>
        AllowWithDelegation,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void DirectTestCompletedEventHandler(object sender, DirectTestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DirectTestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DirectTestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool DirectTestResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool DirectTestResultSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string output {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void DirectGetDBUserCompletedEventHandler(object sender, DirectGetDBUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DirectGetDBUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DirectGetDBUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DBUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DBUser)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void DirectValidatePasswordCompletedEventHandler(object sender, DirectValidatePasswordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DirectValidatePasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DirectValidatePasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool DirectValidatePasswordResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool DirectValidatePasswordResultSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventHandler(object sender, DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AuthorizationType DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AuthorizationType)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool DirectCheckAccessForDatabaseUsersWithoutAttributesRetrieveResultSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
}

#pragma warning restore 1591