using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using NetSqlAzMan.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NetSqlAzMan.SnapIn.Printing
{
	[System.ComponentModel.DesignTimeVisible(false)]
	public class ptItemAuthorizations :PrintDocumentBase
	{
		private string _webApiUri;
		private ServiceBusinessObjects.AzManStore _store;
		private IEnumerable<ServiceBusinessObjects.AzManApplication> _applications;

		private List<object> _alreadyPrinted;
		private Pen _linePen;

		public ptItemAuthorizations(string webApiUri, ServiceBusinessObjects.AzManStore store, IEnumerable<ServiceBusinessObjects.AzManApplication> applications) {
			this._webApiUri = webApiUri;
			this._store = store;
			this._applications = applications;

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

			this.Title = Globalization.MultilanguageResource.GetString("Folder_Msg20");
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
			float _parentStoreX;
			float _parentStoreY;

			if (this._applications == null || this._applications.Count().Equals(0)) {
				return false;
			}

			base.SkipLines(e, 1);
			if (!this._alreadyPrinted.Contains(_store)) {
				this._alreadyPrinted.Add(_store);
				base.WriteLineString("\t", Properties.Resources.Store_16x16, String.Format("{0}{1}", _store.Name, (String.IsNullOrEmpty(_store.Description) ? String.Empty : String.Format(" - {0}", _store.Description))), e);
			}
			_parentStoreX = base.lastX + Properties.Resources.Store_16x16.Size.Width / 2;
			_parentStoreY = base.lastY + Properties.Resources.Store_16x16.Size.Height + 3;
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

				foreach (var _role in _app.Items) {
					if (_role.ItemType == ServiceBusinessObjects.ItemType.Role) {
						if (this.PrintItem(e, _role, 3, parentApplicationX, parentApplicationY))
							return true;
					}
				}
				//Tasks
				foreach (var _task in _app.Items) {
					if (_task.ItemType == ServiceBusinessObjects.ItemType.Task) {
						if (this.PrintItem(e, _task, 3, parentApplicationX, parentApplicationY))
							return true;
					}
				}
				//Operations
				foreach (var _operation in _app.Items) {
					if (_operation.ItemType == ServiceBusinessObjects.ItemType.Operation) {
						if (this.PrintItem(e, _operation, 3, parentApplicationX, parentApplicationY))
							return true;
					}
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

			var _authType = ServiceBusinessObjects.AuthorizationType.AllowWithDelegation;
			while (true) {
				IEnumerable<ServiceBusinessObjects.AzManAuthorization> _auths = null;
				#region Call Web Api
				var _ah = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorization>(_webApiUri);
				var _ahResult = Task.Run(() => _ah.GetAllByItemAsync(_store.Name, item.Application.Name, item.Name, false)).Result;
				if (_ah.IsResponseContentError(_ahResult))
					_ah.ThrowWebApiRequestException(_ahResult);
				else
					_auths = _ah.GetEnumerableSBOFromReturnedContent(_ahResult);
				#endregion

				string _authResume = String.Empty;
				Image _imageType = null;
				if (_auths.Count() > 0) {
					if (!this._alreadyPrinted.Contains(item.ItemId.ToString() + _authType.ToString())) {
						string _authTypeName = String.Empty;
						switch (_authType) {
							case ServiceBusinessObjects.AuthorizationType.AllowWithDelegation: _authTypeName = Globalization.MultilanguageResource.GetString("Domain_AllowWithDelegation"); _imageType = Properties.Resources.AllowForDelegation; break;
							case ServiceBusinessObjects.AuthorizationType.Allow: _authTypeName = Globalization.MultilanguageResource.GetString("Domain_Allow"); _imageType = Properties.Resources.Allow; break;
							case ServiceBusinessObjects.AuthorizationType.Deny: _authTypeName = Globalization.MultilanguageResource.GetString("Domain_Deny"); _imageType = Properties.Resources.Deny; break;
							case ServiceBusinessObjects.AuthorizationType.Neutral: _authTypeName = Globalization.MultilanguageResource.GetString("Domain_Neutral"); _imageType = Properties.Resources.Neutral; break;
						}

						ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo _displayInfo = null;
						foreach (var auth in _auths) {
							if (auth.AuthorizationType == _authType) {
								#region Call Web Api
								var _ah2 = new AzManWebApiClientHelpers.AzManAuthorizationsHelper<ServiceBusinessObjects.AzManAuthorizationMemberDisplayInfo>(_webApiUri);
								var _ah2Result = Task.Run(() => _ah2.GetMemberOrOwnerInfoAsync(_store.Name, item.Application.Name, item.Name, auth.AuthorizationId, true)).Result;
								if (_ah2.IsResponseContentError(_ah2Result))
									_ah2.ThrowWebApiRequestException(_ah2Result);
								else
									_displayInfo = _ah2.GetSBOFromReturnedContent(_ah2Result);
								#endregion

								_authResume += _displayInfo.DisplayName + ", ";
								////OLD VERSION
								//string displayName = String.Empty;
								//MemberType mt = auth.GetMemberInfo(out displayName);
								//sAuthz += displayName + ", ";
							}
						}

						if (_authResume.EndsWith(", ")) _authResume = _authResume.Remove(_authResume.Length - 2);

						if (!string.IsNullOrEmpty(_authResume)) {
							base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel + 1), base.font).Width;
							RectangleF rect = new RectangleF(this.currentX, this.currentY, e.PageBounds.Width - this.currentX - e.PageBounds.Left, e.PageBounds.Height - e.PageBounds.Top);
							var _sf = new StringFormat();
							_sf.FormatFlags = StringFormatFlags.NoClip;
							_sf.Trimming = StringTrimming.Word;
							if (this.currentY + this.meauseMultiLines(_authResume, this.font, rect, _sf, e) + this.spaceBetweenLines > e.PageBounds.Bottom - 70) {
								//all authz in the next page
								return true;
							}
							base.WriteLineString(new string('\t', indentLevel + 1), _imageType, _authTypeName.ToUpper(), e);
							base.currentX = e.Graphics.MeasureString(new string('\t', indentLevel + 1), base.font).Width;
							base.WriteLineString(_authResume, e);
							this._alreadyPrinted.Add(item.ItemId.ToString() + _authType.ToString());
							if (base.EOP) return true;
							base.WriteLineString(" ", e);
							if (base.EOP) return true;
						}
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
						break;
					case ServiceBusinessObjects.AuthorizationType.Neutral:
						stop = true;
						break;
				}
				if (stop) break;
			}
			return false;
		}
	}
}