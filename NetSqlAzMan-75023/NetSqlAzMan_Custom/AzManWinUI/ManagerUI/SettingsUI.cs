using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManWinUI.Properties;
using NetSqlAzMan.SnapIn.Globalization;

namespace AzManWinUI
{
	public partial class SettingsUI : Form
	{
		#region Private fields

		private Exception pvexceError;
		private Properties.Settings pvsettSettings;

		#endregion

		#region Constructors

		internal SettingsUI(ref Properties.Settings settings) {
			pvsettSettings = settings;

			InitializeComponent();

			this.initializeUI();
			this.showCurrentSettings();
		}

		#endregion

		#region Private members

		private void initializeUI() {
			#region Cargar los textos según el lenguaje

			this.Text = MultilanguageResource.GetString("SettingsUI.Text");

			//General
			tabpGeneral.Text = MultilanguageResource.GetString("SettingsUI.tabpGeneral.Text");
			lablTreeNodeSize.Text = MultilanguageResource.GetString("SettingsUI.lablTreeNodeSize.Text");
			lablListViewType.Text = MultilanguageResource.GetString("SettingsUI.lablListViewType.Text");

			//Idioma
			tabpLanguage.Text = MultilanguageResource.GetString("SettingsUI.tabpLanguage.Text");
			radbEspañol.Text = MultilanguageResource.GetString("SettingsUI.radbEspañol.Text");
			radbEnglish.Text = MultilanguageResource.GetString("SettingsUI.radbEnglish.Text");

			//Vista
			tabpView.Text = MultilanguageResource.GetString("SettingsUI.tabpView.Text");
			lablViewStructure.Text = MultilanguageResource.GetString("SettingsUI.lablViewStructure.Text");
			lablViewAuthorization.Text = MultilanguageResource.GetString("SettingsUI.lablViewAuthorization.Text");

			#endregion

			#region Cargar los origen de datos de las lista y combos

			TreeNodeSizeList listTreeNodeSize = new TreeNodeSizeList();
			combTreeNodeSize.ValueMember = "Id";
			combTreeNodeSize.DisplayMember = "Name";
			combTreeNodeSize.DataSource = listTreeNodeSize;

			ListViewTypeList listListViewType = new ListViewTypeList();
			combListViewType.ValueMember = "Id";
			combListViewType.DisplayMember = "Name";
			combListViewType.DataSource = listListViewType;

			StructureViewList listStructureView = new StructureViewList();
			combStructureView.ValueMember = "Id";
			combStructureView.DisplayMember = "Name";
			combStructureView.DataSource = listStructureView;

			AuthorizationViewList listAuthorizationView = new AuthorizationViewList();
			combAuthorizationsView.ValueMember = "Id";
			combAuthorizationsView.DisplayMember = "Name";
			combAuthorizationsView.DataSource = listAuthorizationView;

			#endregion
		}

		private void showCurrentSettings() {
			//General
			combTreeNodeSize.SelectedValue = pvsettSettings.ManagerUI_TreeNodeSize;
			combListViewType.SelectedValue = pvsettSettings.ManagerUI_ListViewType;

			//Language
			switch (pvsettSettings.ManagerUI_Culture) {
				case "en":
					radbEnglish.Checked = true;
					break;

				case "es":
					radbEspañol.Checked = true;
					break;

				default:
					MessageBox.Show(String.Format("No se puede marcar las opciones para la cultura {0}", pvsettSettings.ManagerUI_Culture));
					break;
			}

			//View
			combStructureView.SelectedValue = pvsettSettings.ManagerUI_StructureView;
			combAuthorizationsView.SelectedValue = pvsettSettings.ManagerUI_AuthorizationView;
		}

		private bool validateSettings(out Exception hex) {
			hex = null;

			try {
				#region View settings

				StructureViewEnum selectedStructure = (StructureViewEnum)combStructureView.SelectedValue;
				AuthorizationViewEnum selectedAuthorization = (AuthorizationViewEnum)combAuthorizationsView.SelectedValue;

				switch (selectedStructure) {
					case StructureViewEnum.RoleTask:
						//Do nothing
						break;

					case StructureViewEnum.Role:
						if (selectedAuthorization == AuthorizationViewEnum.RoleTask)
							throw new InvalidEnumArgumentException("La vista de estructura elegida no es compatible con la vista de autorización seleccionada.");
						break;

					default:
						throw new InvalidEnumArgumentException("La vista de estructura no se puede validar debido a que no está implmentada.");
				}

				#endregion

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		private bool saveSettings(out Exception hex) {
			hex = null;

			try {
				//General
				pvsettSettings.ManagerUI_TreeNodeSize = (TreeNodeSizeEnum)combTreeNodeSize.SelectedValue;
				pvsettSettings.ManagerUI_ListViewType = (ListViewTypeEnum)combListViewType.SelectedValue;

				//Language
				if (radbEnglish.Checked)
					pvsettSettings.ManagerUI_Culture = "en";
				else if (radbEspañol.Checked)
					pvsettSettings.ManagerUI_Culture = "es";

				//View				
				pvsettSettings.ManagerUI_StructureView = (StructureViewEnum)combStructureView.SelectedValue;
				pvsettSettings.ManagerUI_AuthorizationView =(AuthorizationViewEnum)combAuthorizationsView.SelectedValue;

				//Save
				pvsettSettings.Save();

				return true;
			}
			catch (Exception ex) {
				hex = ex;
				return false;
			}
		}

		#endregion

		#region Event handlers

		private void butnOk_Click(object sender, EventArgs e) {
			if (!this.validateSettings(out pvexceError)) {
				MessageBox.Show(this, pvexceError.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.saveSettings(out pvexceError))
				MessageBox.Show("Algunos cambios en las preferencias se asumiran cuando de vuelva a iniciar la aplicación", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				throw pvexceError;

			this.DialogResult = DialogResult.OK;
		}

		private void butnApply_Click(object sender, EventArgs e) {
			if (!this.validateSettings(out pvexceError)) {
				MessageBox.Show(this, pvexceError.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.saveSettings(out pvexceError))
				MessageBox.Show(this, "Algunos cambios en las preferencias se asumiran cuando de vuelva a iniciar la aplicación", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				throw pvexceError;
		}

		#endregion
	}

	public class TreeNodeSize
	{
		#region Constructor

		public TreeNodeSize(TreeNodeSizeEnum id) {
			pvin16Id = id;
		}

		#endregion

		#region Properties

		private readonly TreeNodeSizeEnum pvin16Id;
		public TreeNodeSizeEnum Id {
			get {
				return pvin16Id;
			}
		}

		public string Name {
			get {
				switch (this.Id) {
					case TreeNodeSizeEnum.Small:
						return MultilanguageResource.GetString("SettingsUI.combTreeNodeSize.Small");
					case TreeNodeSizeEnum.Large:
						return MultilanguageResource.GetString("SettingsUI.combTreeNodeSize.Large");
					default:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.???");
				}
			}
		}

		#endregion
	}

	public class ListViewType
	{
		#region Constructor

		public ListViewType(ListViewTypeEnum id) {
			pvin16Id = id;
		}

		#endregion

		#region Properties

		private readonly ListViewTypeEnum pvin16Id;
		public ListViewTypeEnum Id {
			get {
				return pvin16Id;
			}
		}

		public string Name {
			get {
				switch (this.Id) {
					case ListViewTypeEnum.Details:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.Details");
					case ListViewTypeEnum.List:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.List");
					case ListViewTypeEnum.LargeIcons:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.LargeIcons");
					case ListViewTypeEnum.SmallIcons:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.SmallIcons");
					case ListViewTypeEnum.Tile:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.Tiles");
					default:
						return MultilanguageResource.GetString("SettingsUI.combListViewType.???");
				}
			}
		}

		#endregion
	}

	public class StructureView
	{
		#region Constructor

		public StructureView(StructureViewEnum id) {
			pvenumId = id;
		}

		#endregion

		#region Properties

		private StructureViewEnum pvenumId;
		public StructureViewEnum Id {
			get { return pvenumId; }
		}

		public string Name {
			get {
				string striName;

				switch (pvenumId) {
					case StructureViewEnum.RoleTask:
						striName = "R-T";
						break;

					case StructureViewEnum.Role:
						striName = "R";
						break;

					default:
						throw new NotImplementedException("El tipo de vista de estructura de seguridad NO está implementado.");
				}

				return striName;
			}
		}

		#endregion
	}

	public class AuthorizationView
	{
		#region Constructors

		public AuthorizationView(AuthorizationViewEnum id) {
			pvenumId = id;
		}

		#endregion

		#region Properties

		private AuthorizationViewEnum pvenumId;
		public AuthorizationViewEnum Id {
			get { return pvenumId; }
		}

		public string Name {
			get {
				string striName;

				switch (pvenumId) {
					case AuthorizationViewEnum.RoleTask:
						striName = "R-T";
						break;

					case AuthorizationViewEnum.Role:
						striName = "R";
						break;

					default:
						throw new NotImplementedException("El tipo de vista de autorizaciones de seguridad NO está implementado.");
				}
				return striName;
			}
		}

		#endregion
	}

	public class TreeNodeSizeList : Component, IListSource
	{
		private BindingList<TreeNodeSize> pvbindlList;

		#region Constructors

		public TreeNodeSizeList() {
		}

		public TreeNodeSizeList(IContainer container)
			: this() {
			container.Add(this);
		}

		#endregion

		#region IListSource Members

		public bool ContainsListCollection {
			get { return false; }
		}

		public System.Collections.IList GetList() {
			pvbindlList = new BindingList<TreeNodeSize>();

			if (!this.DesignMode) {
				pvbindlList.Add(new TreeNodeSize(TreeNodeSizeEnum.Small));
				pvbindlList.Add(new TreeNodeSize(TreeNodeSizeEnum.Large));
			}

			return pvbindlList;
		}

		#endregion
	}

	public class ListViewTypeList : Component, IListSource
	{
		private BindingList<ListViewType> pvbindlList;

		#region Constructors

		public ListViewTypeList() {
		}

		public ListViewTypeList(IContainer container)
			: this() {
			container.Add(this);
		}

		#endregion

		#region IListSource Members

		public bool ContainsListCollection {
			get { return false; }
		}

		public System.Collections.IList GetList() {
			pvbindlList = new BindingList<ListViewType>();

			if (!this.DesignMode) {
				pvbindlList.Add(new ListViewType(ListViewTypeEnum.Details));
				pvbindlList.Add(new ListViewType(ListViewTypeEnum.LargeIcons));
				pvbindlList.Add(new ListViewType(ListViewTypeEnum.List));
				pvbindlList.Add(new ListViewType(ListViewTypeEnum.SmallIcons));
				pvbindlList.Add(new ListViewType(ListViewTypeEnum.Tile));
			}

			return pvbindlList;
		}

		#endregion
	}

	public class StructureViewList : Component, IListSource
	{
		private BindingList<StructureView> pvbindlList;

		#region Constructors

		public StructureViewList() {
		}

		public StructureViewList(IContainer container)
			: this() {
			container.Add(this);
		}

		#endregion

		#region IListSource Members

		public bool ContainsListCollection {
			get { return false; }
		}

		public System.Collections.IList GetList() {
			pvbindlList = new BindingList<StructureView>();

			if (!this.DesignMode) {
				pvbindlList.Add(new StructureView(StructureViewEnum.RoleTask));
				pvbindlList.Add(new StructureView(StructureViewEnum.Role));
			}

			return pvbindlList;
		}

		#endregion
	}

	public class AuthorizationViewList : Component, IListSource
	{
		private BindingList<AuthorizationView> pvbindlList;

		#region Constructors

		public AuthorizationViewList() {
		}

		public AuthorizationViewList(IContainer container)
			: this() {
			container.Add(this);
		}

		#endregion

		#region IListSource Members

		public bool ContainsListCollection {
			get { return false; }
		}

		public System.Collections.IList GetList() {
			pvbindlList = new BindingList<AuthorizationView>();

			if (!this.DesignMode) {
				pvbindlList.Add(new AuthorizationView(AuthorizationViewEnum.RoleTask));
				pvbindlList.Add(new AuthorizationView(AuthorizationViewEnum.Role));
			}

			return pvbindlList;
		}

		#endregion
	}
}
