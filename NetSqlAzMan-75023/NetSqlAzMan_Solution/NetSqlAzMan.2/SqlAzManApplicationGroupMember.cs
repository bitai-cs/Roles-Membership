using System;
using System.Runtime.Serialization;
using System.Xml;
using NetSqlAzMan.ENS;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LINQ;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetSqlAzMan.Extensions;

namespace NetSqlAzMan
{
	/// <summary>
	/// SqlAzMan Application Group Member class.
	/// </summary>
	[Serializable()]
	[DataContract(Namespace = "http://NetSqlAzMan/ServiceModel", IsReference = true)]
	public sealed partial class SqlAzManApplicationGroupMember :IAzManApplicationGroupMember, IAzManExport
	{
		#region Fields
		[NonSerialized()]
		private NetSqlAzManStorageDataContext db;
		private int applicationGroupMemberId;
		private IAzManApplicationGroup applicationGroup;
		private IAzManSid sid;
		private bool isMember;
		private WhereDefined whereDefined;
		[NonSerialized()]
		private SqlAzManENS ens;
		#endregion Fields
		#region Events
		/// <summary>
		/// Occurs after a SqlApplicationGroupMember object has been Deleted.
		/// </summary>
		public event ApplicationGroupMemberDeletedDelegate ApplicationGroupMemberDeleted;
		#endregion Events
		#region Private Event Raisers
		private void raiseApplicationGroupMemberDeleted(IAzManApplicationGroup ownerApplicationGroup, IAzManSid sid) {
			if (this.ApplicationGroupMemberDeleted != null)
				this.ApplicationGroupMemberDeleted(ownerApplicationGroup, sid);
		}
		#endregion Private Event Raisers
		#region Constructors
		internal SqlAzManApplicationGroupMember(NetSqlAzManStorageDataContext db, IAzManApplicationGroup applicationGroup, int applicationGroupMemberId, IAzManSid sid, WhereDefined whereDefined, bool isMember, SqlAzManENS ens) {
			this.db = db;
			this.applicationGroup = applicationGroup;
			this.applicationGroupMemberId = applicationGroupMemberId;
			this.sid = sid;
			this.whereDefined = whereDefined;
			this.isMember = isMember;
			this.ens = ens;
		}
		#endregion Constructors
		#region IAzManApplicationGroupMember Members

		/// <summary>
		/// Gets the application group member id.
		/// </summary>
		/// <value>The application group member id.</value>
		public int ApplicationGroupMemberId {
			get {
				return this.applicationGroupMemberId;
			}
		}

		/// <summary>
		/// Gets the application group.
		/// </summary>
		/// <value>The application group.</value>
		public IAzManApplicationGroup ApplicationGroup {
			get {
				return this.applicationGroup;
			}
		}

		/// <summary>
		/// Gets the object owner.
		/// </summary>
		/// <value>The object owner.</value>
		public IAzManSid SID {
			get {
				return this.sid;
			}
		}
		/// <summary>
		/// Gets where member is defined.
		/// </summary>
		/// <value>The where defined.</value>
		public WhereDefined WhereDefined {
			get {
				return this.whereDefined;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is member.
		/// </summary>
		/// <value><c>true</c> if this instance is member; otherwise, <c>false</c>.</value>
		public bool IsMember {
			get {
				return this.isMember;
			}
		}

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		public void Delete() {
			this.db.ApplicationGroupMemberDelete(this.applicationGroupMemberId, this.applicationGroup.Application.ApplicationId);
			this.raiseApplicationGroupMemberDeleted(this.applicationGroup, this.sid);
		}
		/// <summary>
		/// Gets the member info.
		/// </summary>
		/// <param name="displayName">Name of the display.</param>
		/// <returns></returns>
		public MemberType GetMemberInfo(out string displayName) {
			try {
				switch (this.whereDefined) {
					case WhereDefined.Store:
						displayName = this.applicationGroup.Application.Store.GetStoreGroup(this.sid).Name;
						return MemberType.StoreGroup;
					case WhereDefined.Application:
						displayName = this.applicationGroup.Application.GetApplicationGroup(this.sid).Name;
						return MemberType.ApplicationGroup;
					case WhereDefined.LDAP:
						#region ***PERSONALIZADO***
						if (!string.IsNullOrEmpty(this.DomainProfile)) {
							LdapHelperDTO.AsyncResult _asynResult = null;
							var _bl = new CustomBussinessLogic.LdapWebApiBusinessFactory();
							try {
								//Call Custom Logic
								_asynResult = Task.Run(() => _bl.SearchEntriesAsyncModeAsync(this.DomainProfile, true, 0, LdapHelperDTO.EntryAttribute.objectSid, this.SID.StringValue, LdapHelperDTO.RequiredEntryAttributes.Minimun)).Result;
								//Evaluar resultados descargados
								if (_asynResult.Entries.Count().Equals(1)) {
									var _entry = _asynResult.Entries.First();
									if (!string.IsNullOrEmpty(_entry.displayName))
										displayName = string.Format("{0} ({1}\\{2})", _entry.displayName, _entry.DomainProfile, _entry.samAccountName);
									else if (!string.IsNullOrEmpty(_entry.cn))
										displayName = string.Format("{0} ({1}\\{2})", _entry.cn, _entry.DomainProfile, _entry.samAccountName);
									else
										displayName = string.Format("{0}\\{1}", _entry.DomainProfile, _entry.samAccountName);

									return _entry.GetIsGroupByObjectClass() ? MemberType.WindowsNTGroup : MemberType.WindowsNTUser;
								}
								else {
									displayName = string.Format("{0} {1}\\{2}", "NO IDENTIFICADO!", this.DomainProfile, this.SID.StringValue);

									return MemberType.AnonymousSID;
								}
							}
							catch (Exception ex) {
								displayName = string.Format("{0} {1}\\{2} [3]", "ERROR!", this.DomainProfile, this.SID.StringValue, ex.GetType().Name);

								return MemberType.AnonymousSID;
							}
						}
						else {
							////Ya no se admite usuarios agregados desde el componente 
							////selector de Windows. Todos los usuarios LDAP deben provenir desde el 
							////servicio Web Api Ldap
							//bool isAnLdapGroup;
							//bool isLocal;
							//DirectoryServices.DirectoryServicesUtils.GetMemberInfo(this.sid.StringValue, out displayName, out isAnLdapGroup, out isLocal);
							//return isAnLdapGroup ? MemberType.WindowsNTGroup : MemberType.WindowsNTUser;

							displayName = string.Format("{0} {1}", "NO SOPORTADO!", this.SID.StringValue);

							return MemberType.AnonymousSID;
						}
					#endregion
					case WhereDefined.Local:
						bool isALocalGroup;
						bool isLocal2;
						DirectoryServices.DirectoryServicesUtils.GetMemberInfo(this.sid.StringValue, out displayName, out isALocalGroup, out isLocal2);
						return isALocalGroup ? MemberType.WindowsNTGroup : MemberType.WindowsNTUser;
					case WhereDefined.Database:
						try {
							displayName = this.applicationGroup.Application.GetDBUser(this.sid).UserName;
						}
						catch (NetSqlAzMan.SqlAzManException) {
							displayName = this.sid.StringValue;
						}
						return MemberType.DatabaseUser;
				}
				displayName = this.sid.StringValue;
				return MemberType.AnonymousSID;
			}
			catch {
				displayName = this.sid.StringValue;
				return MemberType.AnonymousSID;
			}
		}
		#endregion
		#region IAzManImportExport Members

		/// <summary>
		/// Exports the specified XML writer.
		/// </summary>
		/// <param name="xmlWriter">The XML writer.</param>
		/// <param name="includeWindowsUsersAndGroups">if set to <c>true</c> [include windows users and groups].</param>
		/// <param name="includeDBUsers">if set to <c>true</c> [include DB users].</param>
		/// <param name="includeAuthorizations">if set to <c>true</c> [include authorizations].</param>
		/// <param name="ownerOfExport">The owner of export.</param>
		public void Export(System.Xml.XmlWriter xmlWriter, bool includeWindowsUsersAndGroups, bool includeDBUsers, bool includeAuthorizations, object ownerOfExport) {
			xmlWriter.WriteStartElement("ApplicationGroupMember");
			xmlWriter.WriteAttributeString("Sid", this.sid.StringValue);
			xmlWriter.WriteAttributeString("WhereDefined", this.whereDefined.ToString());
			xmlWriter.WriteAttributeString("IsMember", this.isMember.ToString());
			xmlWriter.WriteEndElement();
		}

		/// <summary>
		/// Imports the specified XML reader.
		/// </summary>
		/// <param name="xmlNode">The XML node.</param>
		/// <param name="includeWindowsUsersAndGroups">if set to <c>true</c> [include windows users and groups].</param>
		/// <param name="includeAuthorizations">if set to <c>true</c> [include authorizations].</param>
		/// <returns></returns>
		public void ImportChildren(XmlNode xmlNode, bool includeWindowsUsersAndGroups, bool includeAuthorizations) {
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
		#region Object Members
		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString() {
			return String.Format("Application Group Member ID: {0}\r\nSID: {1}\r\nWhere Defined: {2}\r\nIs Member: {3}",
				 this.applicationGroupMemberId, this.sid, this.whereDefined, this.isMember);
		}
		#endregion Object Members
	}
}
