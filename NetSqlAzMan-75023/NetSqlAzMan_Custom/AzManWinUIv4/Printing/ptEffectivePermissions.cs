using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Principal;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NetSqlAzMan.SnapIn.Printing
{
	[System.ComponentModel.DesignTimeVisible(false)]
	public class ptEffectivePermissions :PrintDocumentBase
	{
		private readonly string _webApiUri = null;
		private readonly ServiceBusinessObjects.AzManStore _store = null;
		private readonly IEnumerable<ServiceBusinessObjects.AzManApplication> _applications = null;

		//private StorageCache storageCache;
		private string[] userUPNs;
		private List<WindowsIdentity> userIdenities = null;
		//private List<IAzManDBUser> dbUserIdentities = null;
		private IEnumerable<ServiceBusinessObjects.AzManDBUser> dbUserIdentities = null;
		private List<object> _alreadyPrinted;
		private Pen _linePen;

		public ptEffectivePermissions(string webApiUri, ServiceBusinessObjects.AzManStore store, IEnumerable<ServiceBusinessObjects.AzManApplication> applications) {
			_webApiUri = webApiUri;
			_store = store;
			_applications = applications;

			//Validar si las aplicaciones ya tienen los Items cargado
			var _ih = new AzManWebApiClientHelpers.AzManItemsHelper<ServiceBusinessObjects.AzManItem>(_webApiUri);
			foreach (ServiceBusinessObjects.AzManApplication _app in this._applications) {
				if (_app.Items.Any())
					continue;

				IEnumerable<ServiceBusinessObjects.AzManItem> _items = null;
				#region Call Web Api
				var _ihResult = Task.Run(() => _ih.GetItemsAsync(_app.Store.Name, _app.Name, ServiceBusinessObjects.ItemType.All, false, true, true, false)).Result;
				if (_ih.IsResponseContentError(_ihResult))
					_ih.ThrowWebApiRequestException(_ihResult);
				else
					_items = _ih.GetEnumerableSBOFromReturnedContent(_ihResult);
				#endregion
				_app.Items = _items;
			}

			this.Title = Globalization.MultilanguageResource.GetString("rptTit30");
			this.TopIcon = Properties.Resources.ItemAuthorization_32x32;
			this._alreadyPrinted = new List<object>();

			this._linePen = new Pen(Brushes.Black, 1F);
			this._linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		}

		protected override void OnEndPrint(PrintEventArgs e) {
			base.OnEndPrint(e);
			this._alreadyPrinted.Clear();
		}

		/// <summary>
		/// Prints the body.
		/// </summary>
		/// <param name="e">The <see cref="T:System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
		protected override bool PrintPageBody(PrintPageEventArgs e) {
			if (this._applications == null || this._applications.Count().Equals(0))
				return false;

			float parentStoreX;
			float parentStoreY;

			////Build Storage Cache
			//this.storageCache = new StorageCache(this.applications[0].Store.Storage.ConnectionString);
			//this.storageCache.BuildStorageCache(this.applications[0].Store.Name);

			//Get All Domain Users
			//this.userUPNs = NetSqlAzMan.DirectoryServices.DirectoryServicesUtils.GetAllDomainUsers();

			base.SkipLines(e, 1);
			if (!this._alreadyPrinted.Contains(_store)) {
				this._alreadyPrinted.Add(_store);
				base.WriteLineString("\t", Properties.Resources.Store_16x16, String.Format("{0}{1}", _store.Name, (String.IsNullOrEmpty(_store.Description) ? String.Empty : String.Format(" - {0}", _store.Description))), e);
			}
			parentStoreX = base.lastX + Properties.Resources.Store_16x16.Size.Width / 2;
			parentStoreY = base.lastY + Properties.Resources.Store_16x16.Size.Height + 3;
			if (base.EOP) return true;
			float parentApplicationX = 0.0F;
			float parentApplicationY = 0.0F;
			foreach (var _app in this._applications) {
				if (!this._alreadyPrinted.Contains(_app)) {
					base.WriteLineString("\t\t", Properties.Resources.Application_16x16, String.Format("{0}{1}", _app.Name, (String.IsNullOrEmpty(_app.Description) ? String.Empty : String.Format(" - {0}", _app.Description))), e);
					parentApplicationX = base.lastX + Properties.Resources.Application_16x16.Width / 2;
					parentApplicationY = base.lastY + Properties.Resources.Application_16x16.Height + 3;
					this.currentY += 3;
					e.Graphics.DrawLine(base.pen, e.Graphics.MeasureString("\t\t", this.font).Width + Properties.Resources.Application_16x16.Size.Width + 3, this.currentY, this.right, this.currentY);
					this.currentY += 3;
					this._alreadyPrinted.Add(_app);
					if (base.EOP) return true;
				}

				//Roles
				foreach (var _role in _app.Items.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Role)) {
					if (this.PrintItem(e, _role, 3, parentApplicationX, parentApplicationY))
						return true;
				}
				//Tasks
				foreach (var _task in _app.Items.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Task)) {
					if (this.PrintItem(e, _task, 3, parentApplicationX, parentApplicationY))
						return true;
				}
				//Operations
				foreach (var _operation in _app.Items.Where(i => i.ItemType == ServiceBusinessObjects.ItemType.Operation)) {
					if (this.PrintItem(e, _operation, 3, parentApplicationX, parentApplicationY))
						return true;
				}
			}
			return false;
		}

		private bool PrintItem(PrintPageEventArgs e, ServiceBusinessObjects.AzManItem item, int indentLevel, float parentItemX, float parentItemY) {
			Icon _itemIcon = null;
			switch (item.ItemType) {
				case ServiceBusinessObjects.ItemType.Role:
					_itemIcon = Properties.Resources.Role_16x16;
					break;
				case ServiceBusinessObjects.ItemType.Task:
					_itemIcon = Properties.Resources.Task_16x16;
					break;
				case ServiceBusinessObjects.ItemType.Operation:
					_itemIcon = Properties.Resources.Operation_16x16;
					break;
			}

			float parentParentItemX = 0.0F;
			float parentParentItemY = 0.0F;

			if (!this._alreadyPrinted.Contains(item.ItemId)) {
				base.WriteLineString(new String('\t', indentLevel), _itemIcon, String.Format("{0}{1}", item.Name, (String.IsNullOrEmpty(item.Description) ? String.Empty : String.Format(" - {0}", item.Description))), e);
				if (parentItemX == 0 || parentItemY == 0) {
					parentItemX = e.Graphics.MeasureString(new String('\t', indentLevel - 1), base.font).Width + _itemIcon.Size.Width / 2;
					parentItemY = base.lastY - 1.5F;
				}
				parentParentItemX = base.lastX + _itemIcon.Width / 2;
				parentParentItemY = base.lastY + _itemIcon.Height + 3;
				this._alreadyPrinted.Add(item.ItemId);
				if (base.EOP) return true;
			}

			//Effective Permissions
			var _authType = ServiceBusinessObjects.AuthorizationType.AllowWithDelegation;

			while (true) {
				if (!this._alreadyPrinted.Contains(item.ItemId.ToString() + _authType.ToString())) {
					Image _imageType = null;
					string _authResume = String.Empty;
					string _authTypeResume = String.Empty;
					switch (_authType) {
						case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation: _authTypeResume = Globalization.MultilanguageResource.GetString("Domain_AllowWithDelegation"); _imageType = Properties.Resources.AllowForDelegation; break;
						case ServiceBusinessObjects.AuthorizationType.Allow: _authTypeResume = Globalization.MultilanguageResource.GetString("Domain_Allow"); _imageType = Properties.Resources.Allow; break;
						case ServiceBusinessObjects.AuthorizationType.Deny: _authTypeResume = Globalization.MultilanguageResource.GetString("Domain_Deny"); _imageType = Properties.Resources.Deny; break;
							//case AuthorizationType.Neutral: sAuthType = Globalization.MultilanguageResource.GetString("Domain_Neutral"); imageType = Properties.Resources.Neutral; break;
					}

					////VBASTIDAS: migrar para usuar con Servicio LDAP
					//if (this.userIdenities == null) {
					//	this.userIdenities = new List<WindowsIdentity>();
					//	foreach (string userUpn in this.userUPNs) {
					//		try {
					//			WindowsIdentity winId = new WindowsIdentity(userUpn);
					//			this.userIdenities.Add(winId);
					//		}
					//		catch {
					//			//Invalid user (expired, locked, disabled, etc...)
					//			//Do not add
					//		}
					//	}
					//}

					if (this.dbUserIdentities == null) {
						IEnumerable<ServiceBusinessObjects.AzManDBUser> _dbUsers = null;
						#region Call Web Api
						var _uh = new AzManWebApiClientHelpers.AzManDBUsersHelper<ServiceBusinessObjects.AzManDBUser>(_webApiUri);
						var _uhResult = Task.Run(() => _uh.GetAllAsync()).Result;
						if (_uh.IsResponseContentError(_uhResult))
							_uh.ThrowWebApiRequestException(_uhResult);
						else
							_dbUsers = _uh.GetEnumerableSBOFromReturnedContent(_uhResult);
						#endregion

						//this.dbUserIdentities = new List<IAzManDBUser>(item.Application.Store.Storage.GetDBUsers());
						this.dbUserIdentities = _dbUsers;
					}

					////VBASTIDAS: Migrar para usarcon el Servicio LDAP
					////Windows Users
					//foreach (WindowsIdentity wid in this.userIdenities) {
					//	try {
					//		AuthorizationType effectiveAuthorization =
					//			 this.storageCache.CheckAccess(item.Application.Store.Name, item.Application.Name,
					//			 item.Name, wid.GetUserBinarySSid(), wid.GetGroupsBinarySSid(),
					//			 DateTime.Now, false);
					//		if (effectiveAuthorization == authType) {
					//			sAuthz += wid.Name + ", ";
					//		}
					//	}
					//	catch {
					//		//Do nothing
					//	}
					//}

					//DB Users
					ServiceBusinessObjects.AzManAuthorizationInfo _authInfo = null;
					foreach (var _dbUser in this.dbUserIdentities) {
						try {
							#region Call Web Api
							var _ah = new AzManWebApiClientHelpers.AzManStorageAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationInfo>(_webApiUri);
							var _ahResult = Task.Run(() => _ah.CheckAccessAsync(_store.Name, item.Application.Name, item.Name, null, _dbUser.UserName)).Result;
							if (_ah.IsResponseContentError(_ahResult))
								_ah.ThrowWebApiRequestException(_ahResult);
							else
								_authInfo = _ah.GetSBOFromReturnedContent(_ahResult);
							#endregion

							//AuthorizationType effectiveAuthorization =
							//	 this.storageCache.CheckAccess(item.Application.Store.Name, item.Application.Name,
							//	 item.Name, _dbUser.CustomSid.StringValue,
							//	 DateTime.Now, false);
							if (_authInfo.AuthorizationType == _authType)
								_authResume += _dbUser.UserName + ", ";
						}
						catch {
							//Do nothing
						}
					}

					if (_authResume.EndsWith(", ")) _authResume = _authResume.Remove(_authResume.Length - 2);

					if (!String.IsNullOrEmpty(_authResume)) {
						base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel + 1), base.font).Width;
						RectangleF rect = new RectangleF(this.currentX, this.currentY, e.PageBounds.Width - this.currentX - e.PageBounds.Left, e.PageBounds.Height - e.PageBounds.Top);
						StringFormat sf = new StringFormat();
						sf.FormatFlags = StringFormatFlags.NoClip;
						sf.Trimming = StringTrimming.Word;
						if (this.currentY + this.meauseMultiLines(_authResume, this.font, rect, sf, e) + this.spaceBetweenLines > e.PageBounds.Bottom - 70) {
							//all authz in the next page
							return true;
						}
						base.WriteLineString(new string('\t', indentLevel + 1), _imageType, _authTypeResume.ToUpper(), e);
						base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel + 1), base.font).Width;
						base.WriteLineString(_authResume, e);
						this._alreadyPrinted.Add(item.ItemId.ToString() + _authType.ToString());
						if (base.EOP) return true;
						base.WriteLineString(" ", e);
						if (base.EOP) return true;
					}
				}

				bool stop = false;
				switch (_authType) {
					case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation:
						_authType = ServiceBusinessObjects.AuthorizationType.Allow;
						break;
					case ServiceBusinessObjects.AuthorizationType.Allow:
						_authType = ServiceBusinessObjects.AuthorizationType.Deny;
						break;
					case ServiceBusinessObjects.AuthorizationType.Deny:
						_authType = ServiceBusinessObjects.AuthorizationType.Neutral;
						stop = true;
						break;
						//case AuthorizationType.Neutral: stop = true; break;
				}

				if (stop) break;


				//OLD
				//IAzManAuthorization[] authz = new IAzManAuthorization[item.Authorizations.Count];
				//string sAuthz = String.Empty;
				//Image imageType = null;
				//item.Authorizations.CopyTo(authz, 0); ;
				//if (authz.Length > 0)
				//{
				//    if (!this.alreadyPrinted.Contains(item.ItemId.ToString() + authType.ToString()))
				//    {
				//        string sAuthType = String.Empty;
				//        switch (authType)
				//        {
				//            case AuthorizationType.AllowWithDelegation: sAuthType = Globalization.MultilanguageResource.GetString("Domain_AllowWithDelegation"); imageType = Properties.Resources.AllowForDelegation; break;
				//            case AuthorizationType.Allow: sAuthType = Globalization.MultilanguageResource.GetString("Domain_Allow"); imageType = Properties.Resources.Allow; break;
				//            case AuthorizationType.Deny: sAuthType = Globalization.MultilanguageResource.GetString("Domain_Deny"); imageType = Properties.Resources.Deny; break;
				//            case AuthorizationType.Neutral: sAuthType = Globalization.MultilanguageResource.GetString("Domain_Neutral"); imageType = Properties.Resources.Neutral; break;
				//        }
				//        foreach (IAzManAuthorization auth in authz)
				//        {
				//            if (auth.AuthorizationType == authType)
				//            {
				//                string displayName = String.Empty;
				//                MemberType mt = auth.GetMemberInfo(out displayName);
				//                sAuthz += displayName + ", ";
				//            }
				//        }
				//        if (sAuthz.EndsWith(", ")) sAuthz = sAuthz.Remove(sAuthz.Length - 2);
				//        if (!String.IsNullOrEmpty(sAuthz))
				//        {
				//            base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel+1), base.font).Width;
				//            RectangleF rect = new RectangleF(this.currentX, this.currentY, e.PageBounds.Width - this.currentX - e.PageBounds.Left, e.PageBounds.Height - e.PageBounds.Top);
				//            StringFormat sf = new StringFormat();
				//            sf.FormatFlags = StringFormatFlags.NoClip;
				//            sf.Trimming = StringTrimming.Word;
				//            if (this.currentY + this.meauseMultiLines(sAuthz, this.font, rect, sf, e) + this.spaceBetweenLines > e.PageBounds.Bottom - 70)
				//            {
				//                //all authz in the next page
				//                return true;
				//            }
				//            base.WriteLineString(new string('\t', indentLevel + 1), imageType, sAuthType.ToUpper(), e);
				//            base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel + 1), base.font).Width;
				//            base.WriteLineString(sAuthz, e);
				//            this.alreadyPrinted.Add(item.ItemId.ToString() + authType.ToString());
				//            if (base.EOP) return true;
				//            base.WriteLineString(" ", e);
				//            if (base.EOP) return true;
				//        }
				//    }
				//}
				//bool stop = false;
				//switch (authType)
				//{
				//    case AuthorizationType.AllowWithDelegation: authType = AuthorizationType.Allow; break;
				//    case AuthorizationType.Allow: authType = AuthorizationType.Deny; break;
				//    case AuthorizationType.Deny: authType = AuthorizationType.Neutral; break;
				//    case AuthorizationType.Neutral: stop = true; break;
				//}
				//if (stop) break;
			}
			return false;
		}
	}
}