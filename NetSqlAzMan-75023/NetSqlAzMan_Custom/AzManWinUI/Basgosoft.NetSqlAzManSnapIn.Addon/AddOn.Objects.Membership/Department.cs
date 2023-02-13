using System;
using System.Data;
using NetSqlAzManSnapIn.AddOn.Common.Membership;
using NetSqlAzManSnapIn.AddOn.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace NetSqlAzManSnapIn.AddOn.Membership.Objects
{
	public class DepartmentStruct
	{
		#region Constructor

		public DepartmentStruct()
			: this(null, null) {
		}

		internal DepartmentStruct(Nullable<Int32> departmentId, String departmentName) {
			pvin32departmentId = departmentId;
			pvstriDepartmentName = departmentName;
		}

		#endregion

		#region Properties

		private Nullable<Int32> pvin32departmentId;
		public Nullable<Int32> DepartmentId {
			get {
				return pvin32departmentId;
			}
			internal set {
				pvin32departmentId = value;
			}
		}

		private String pvstriDepartmentName;
		public String DepartmentName {
			get {
				return pvstriDepartmentName;
			}
			set {
				pvstriDepartmentName = value;
			}
		}

		#endregion
	}

	public class Department : NetSqlAzManSnapIn.AddOn.Objects.Base
	{
		#region Private members

		private NetSqlAzManSnapIn.AddOn.Membership.Data.Department pvdaccProxy;

		#endregion

		#region Constructors

		public Department(string connectionString) {
			pvdaccProxy = new NetSqlAzManSnapIn.AddOn.Membership.Data.Department(connectionString);
		}

		#endregion

		#region Implementations

		public override void Dispose() {
			pvdaccProxy.Dispose();
		}

		#endregion

		#region Query members

		public bool GetDepartments(out List<Objects.DepartmentStruct> list, out Exception hex) {
			Common.Membership.Models.DepartmentDataSet dastMaster;
			DataSet dastTemp;

			list = new List<DepartmentStruct>();
			hex = null;

			try {
				dastMaster = new Common.Membership.Models.DepartmentDataSet();
				dastTemp = (DataSet)dastMaster;

				if (!pvdaccProxy.GetDepartments(ref dastTemp, dastMaster.identity_Department.TableName, out ptexceHandled))
					throw new Exception("Error al obtener los datos de usuarios.", ptexceHandled);

				foreach (Common.Membership.Models.DepartmentDataSet.identity_DepartmentRow
 r in dastMaster.identity_Department)
					list.Add(new DepartmentStruct(r.DepartmentId, r.DepartmentName));

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