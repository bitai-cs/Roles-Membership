using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basgosoft.ManagementConsoleLib;
using System.Windows.Forms;
using NetSqlAzMan.SnapIn.Globalization;
using AzManWinUI.Nodes;

namespace AzManWinUI.ListView {
	public class ManagerListView : BaseListView {
		#region Contructors

		public ManagerListView()
			: base() {
		}

		public ManagerListView(short columns)
			: base(columns) {
		}

		#endregion

		#region Protected Overriden members

		/// <summary>
		/// Se configura las columnas del listado.
		/// Tomar en cuenta que este metodo es llamado al asignar el nodo padre del listado
		/// </summary>
		/// <param name="hex">El error que puede ocurrir</param>
		/// <returns>True si se completo el metodo o False si se disparó un error</returns>
		protected override bool configColumns(out Exception hex) {
			ColumnHeader colhColumn;

			hex = null;

			try {
				if (this.ParentNode == null)
					return true;

				if (typeof(Nodes.StorageNode).Equals(this.ParentNode.GetType())) {
					#region StorageNode's ListView

					if (!this.setColumns(3, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_StoreName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 200;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_StoreID");
					colhColumn.Width = 80;

					#endregion

					//this.KeyDown += delegate(object sender, KeyEventArgs e) {
					//   switch (e.KeyCode) {
					//      case Keys.F5:
					//         this.ParentNode.GetActionButton(MultilanguageResource.GetString("frmStorageConnection_btnRefreshDataSources.Text")).Execute();
					//         break;
					//   }
					//};
				}

				if (typeof(Nodes.MembershipNode).Equals(this.ParentNode.GetType())) {
					#region Membership's ListView

					if (!this.setColumns(6, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = "Id.";
					colhColumn.Width = 50;

					colhColumn = this.Columns[1];
					colhColumn.Text = "Usuario";
					colhColumn.Width = 150;

					colhColumn = this.Columns[2];
					colhColumn.Text = "Nombre";
					colhColumn.Width = 200;

					colhColumn = this.Columns[3];
					colhColumn.Text = "Departamento";
					colhColumn.Width = 200;

					colhColumn = this.Columns[4];
					colhColumn.Text = "Surcursal";
					colhColumn.Width = 200;

					colhColumn = this.Columns[5];
					colhColumn.Text = "Habilitado";
					colhColumn.Width = 70;

					#endregion
				}

				if (typeof(Nodes.StoreNode).Equals(this.ParentNode.GetType())) {
					#region StoreNode's ListView

					if (!this.setColumns(2, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ApplicationName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					#endregion
				}

				if (typeof(Nodes.StoreGroupsNode).Equals(this.ParentNode.GetType())) {
					#region StoreGroupsNode's ListView

					if (!this.setColumns(4, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_StoreGroupName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_GroupType");
					colhColumn.Width = 100;

					colhColumn = this.Columns[3];
					colhColumn.Text = "Sid";
					colhColumn.Width = 300;

					#endregion
				}

				if (typeof(Nodes.StoreGroupNode).Equals(this.ParentNode.GetType())) {
					#region StoreGroupNode's ListView

					switch (((StoreGroupNode)this.ParentNode).GroupType) {
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:

							if (!this.setColumns(4, out this.ptexceError))
								throw this.ptexceError;

							colhColumn = this.Columns[0];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Name");
							colhColumn.Width = 200;

							colhColumn = this.Columns[1];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_WhereDefined");
							colhColumn.Width = 100;

							colhColumn = this.Columns[2];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_MemberNonMember");
							colhColumn.Width = 150;

							colhColumn = this.Columns[3];
							colhColumn.Text = "Sid";
							colhColumn.Width = 300;
							break;

						case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:

							if (!this.setColumns(3, out this.ptexceError))
								throw this.ptexceError;

							colhColumn = this.Columns[0];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Name");
							colhColumn.Width = 200;

							colhColumn = this.Columns[1];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
							colhColumn.Width = 250;

							colhColumn = this.Columns[2];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_LDAPQuery");
							colhColumn.Width = 400;
							break;
					}

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ApplicationNode))) {
					#region ApplicationNode's ListView

					if (!this.setColumns(1, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_MemberName");
					colhColumn.Width = 250;

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ApplicationGroupsNode))) {
					#region ApplicationGroupsNode's ListView

					if (!this.setColumns(4, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ApplicationGroupName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_GroupType");
					colhColumn.Width = 100;

					colhColumn = this.Columns[3];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ObjectSID");
					colhColumn.Width = 300;

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ApplicationGroupNode))) {
					#region ApplicationGroupNode's ListView

					switch (((ApplicationGroupNode)this.ParentNode).GroupType) {
						case NetSqlAzMan.ServiceBusinessObjects.GroupType.Basic:
							if (!this.setColumns(4, out this.ptexceError))
								throw this.ptexceError;

							colhColumn = this.Columns[0];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Name");
							colhColumn.Width = 200;

							colhColumn = this.Columns[1];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_WhereDefined");
							colhColumn.Width = 100;

							colhColumn = this.Columns[2];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_MemberNonMember");
							colhColumn.Width = 150;

							colhColumn = this.Columns[3];
							colhColumn.Text = "Sid";
							colhColumn.Width = 300;
							break;

						case NetSqlAzMan.ServiceBusinessObjects.GroupType.LDapQuery:
							if (!this.setColumns(3, out this.ptexceError))
								throw this.ptexceError;

							colhColumn = this.Columns[0];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Name");
							colhColumn.Width = 200;

							colhColumn = this.Columns[1];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
							colhColumn.Width = 250;

							colhColumn = this.Columns[2];
							colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_LDAPQuery");
							colhColumn.Width = 400;

							break;
					}

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ItemDefinitionsNode)) || this.ParentNode.GetType().Equals(typeof(Nodes.ItemAuthorizationsNode))) {
					#region ItemDefinitionsNode's ListView Or ItemAuthorizationsNode's ListView

					if (!this.setColumns(2, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_MemberName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.RoleDefinitionsNode)) || this.ParentNode.GetType().Equals(typeof(Nodes.TaskDefinitionsNode)) || this.ParentNode.GetType().Equals(typeof(Nodes.OperationDefinitionsNode)) || this.ParentNode.GetType().Equals(typeof(Nodes.RoleAuthorizationsNode)) ||
					this.ParentNode.GetType().Equals(typeof(Nodes.TaskAuthorizationsNode)) || this.ParentNode.GetType().Equals(typeof(Nodes.OperationAuthorizationsNode))) {
					#region DefinitionsNode's ListView or AuthorizationsNode's ListView

					if (!this.setColumns(3, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ItemName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ItemID");
					colhColumn.Width = 100;

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ItemDefinitionNode))) {
					#region ItemDefinitionNode's ListView

					if (!this.setColumns(4, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_MemberName");
					colhColumn.Width = 200;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Type");
					colhColumn.Width = 100;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Description");
					colhColumn.Width = 300;

					colhColumn = this.Columns[3];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ItemID");
					colhColumn.Width = 100;

					#endregion
				}

				if (this.ParentNode.GetType().Equals(typeof(Nodes.ItemAuthorizationNode))) {
					#region ItemAuthorization's ListView

					if (!this.setColumns(7, out this.ptexceError))
						throw this.ptexceError;

					colhColumn = this.Columns[0];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Name");
					colhColumn.Width = 250;

					colhColumn = this.Columns[1];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_AuthorizationType");
					colhColumn.Width = 150;

					colhColumn = this.Columns[2];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_WhereDefined");
					colhColumn.Width = 100;

					colhColumn = this.Columns[3];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_Owner");
					colhColumn.Width = 250;

					colhColumn = this.Columns[4];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ValidFrom");
					colhColumn.Width = 180;

					colhColumn = this.Columns[5];
					colhColumn.Text = MultilanguageResource.GetString("ColumnHeader_ValidTo");
					colhColumn.Width = 180;

					colhColumn = this.Columns[6];
					colhColumn.Text = "Sid";
					colhColumn.Width = 300;

					#endregion
				}

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
