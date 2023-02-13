using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using NetSqlAzMan.Interfaces;

namespace NetSqlAzMan
{
	/// <summary>
	/// SqlAzManDBUser class for custom DB User.
	/// </summary>
	[Serializable()]
	[DataContract(Namespace = "http://NetSqlAzMan/ServiceModel", IsReference = true)]
	public sealed partial class SqlAzManDBUser : IAzManDBUser
	{
		#region Fields
		private IAzManSid _customSid;
		private string _userName;
		private Dictionary<string, object> _customColumns;
		#endregion Fields

		#region Constructors
		internal SqlAzManDBUser(IAzManSid customSid, string userName, string displayName, Dictionary<string, object> customColumns) {
			this.validateCustomColumns(customColumns);

			_customSid = customSid;
			_userName = userName;
			_displayName = displayName;
			_customColumns = customColumns;
		}

		internal SqlAzManDBUser(DataRow DBUserDataRow, Dictionary<string, object> customColumns) {
			this.validateCustomColumns(customColumns);

			this._customSid = new SqlAzManSID((byte[])DBUserDataRow["DBUserSid"], true);
			this._userName = (string)DBUserDataRow["DBUserName"];
			this._displayName = (string)DBUserDataRow["FullName"];
			this._customColumns = customColumns;
		}
		#endregion Constructors

		#region IAzManDBUser Members
		/// <summary>
		/// Custom Unique identifier of the DB User
		/// </summary>
		/// <value></value>
		[DataMember]
		public IAzManSid CustomSid {
			get {
				return this._customSid;
			}
		}

		/// <summary>
		/// Username of the DB User
		/// </summary>
		/// <value></value>
		[DataMember]
		public string UserName {
			get {
				return this._userName;
			}
		}

		/// <summary>
		/// Gets the custom columns.
		/// </summary>
		/// <value>The custom columns.</value>
		[DataMember]
		public Dictionary<string, object> CustomColumns {
			get {
				if (this._customColumns == null)
					this._customColumns = new Dictionary<string, object>();
				return this._customColumns;
			}
		}
		#endregion
	}
}

