//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetSqlAzMan.CustomDataLayer.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class identity_LDAPConfig
    {
        public string ldap_domain { get; set; }
        public string ldap_description { get; set; }
        public string ldap_client_endpoint { get; set; }
        public bool ldap_enabled { get; set; }
        public byte[] RowVersion { get; set; }
    }
}