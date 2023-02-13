using System;
using System.Data;
using NetSqlAzManSnapIn.AddOn.Common.Membership;
using NetSqlAzManSnapIn.AddOn.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace NetSqlAzManSnapIn.AddOn.Membership.Objects
{
	public class BranchOfficeStruct
	{
		#region Constructor

		public BranchOfficeStruct()
			: this(null, null) {
		}

		internal BranchOfficeStruct(string BranchOfficeId, String BranchOfficeName) {
			pvstriBranchOfficeId = BranchOfficeId;
			pvstriBranchOfficeName = BranchOfficeName;
		}

		#endregion

		#region Properties

		private string pvstriBranchOfficeId;
		public string BranchOfficeId {
			get {
				return pvstriBranchOfficeId;
			}
			internal set {
				pvstriBranchOfficeId = value;
			}
		}

		private String pvstriBranchOfficeName;
		public String BranchOfficeName {
			get {
				return pvstriBranchOfficeName;
			}
			set {
				pvstriBranchOfficeName = value;
			}
		}

		#endregion
	}

	public class BranchOffice : NetSqlAzManSnapIn.AddOn.Objects.Base
	{
		#region Private members

		private NetSqlAzManSnapIn.AddOn.Membership.Data.BranchOffice pvdaccProxy;

		#endregion

		#region Constructors

		public BranchOffice(string connectionString) {
			pvdaccProxy = new NetSqlAzManSnapIn.AddOn.Membership.Data.BranchOffice(connectionString);
		}

		#endregion

		#region Implementations

		public override void Dispose() {
			pvdaccProxy.Dispose();
		}

		#endregion

		#region Query members

		public bool GetBranchOffices(out List<Objects.BranchOfficeStruct> list, out Exception hex) {
			Common.Membership.Models.BranchOfficeDataSet dastMaster;
			DataSet dastTemp;

			list = new List<BranchOfficeStruct>();
			hex = null;

			try {
				dastMaster = new Common.Membership.Models.BranchOfficeDataSet();
				dastTemp = (DataSet)dastMaster;

				if (!pvdaccProxy.GetBranchOffices(ref dastTemp, dastMaster.identity_BranchOffice.TableName, out ptexceHandled))
					throw new Exception("Error al obtener los datos de las sucursales.", ptexceHandled);

				foreach (Common.Membership.Models.BranchOfficeDataSet.identity_BranchOfficeRow
 r in dastMaster.identity_BranchOffice)
					list.Add(new BranchOfficeStruct(r.BranchOfficeId, r.BranchOfficeName));

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion
	}
}