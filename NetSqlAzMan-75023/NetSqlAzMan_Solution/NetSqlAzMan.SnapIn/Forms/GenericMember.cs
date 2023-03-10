using System;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan.SnapIn.Forms
{
    internal class GenericMember
    {
        internal string Name;
        internal IAzManSid sid;
        internal string Description;
        internal AuthorizationType authorizationType;
        internal DateTime? validFrom;
        internal DateTime? validTo;

        internal WhereDefined WhereDefined;

        internal GenericMember(string name, IAzManSid sid, WhereDefined whereDefined)
        {
            this.Name = name;
            this.sid = sid;
            this.WhereDefined = whereDefined;
            this.Description = String.Empty;
        }

        internal GenericMember(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        internal GenericMember(IAzManSid sid, WhereDefined whereDefined, AuthorizationType authorizationType, DateTime? validFrom, DateTime? validTo)
        {
            this.sid = sid;
            this.WhereDefined = whereDefined;
            this.authorizationType = authorizationType;
            this.validFrom = validFrom;
            this.validTo = validTo;
        }
    }
}
