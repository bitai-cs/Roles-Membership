using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AzManCommon;
using AzManLoginHelper.AzManLoginSvcRef;

namespace AzManWinFormApps
{
	public partial class BaseForm : BorderlessForm
	{
		#region Shared fields
		protected Exception _exce = null;
		protected DBUser _dbUser = null;
		protected LoginInfo _loginInfo = null;
		protected LoginStatusEnum _loginStatus = LoginStatusEnum.Unknown;
		protected AuthorizationType _authorizationType = AuthorizationType.Neutral;
		protected Enum _azManItem = null;
		protected string _outputString = null;
		protected bool _notified = false;
		#endregion

		#region Protected events
		protected event AzManCommon.RequestStatusNotificationEvenHandler RequestStatusNotification;
		#endregion

		#region Constructors
		public BaseForm() {
		}

		protected BaseForm(Enum azManItem, WinFormSession session)
			: base() {
			InitializeComponent();

			_azManItem = azManItem;
			_session = session;
		}
		#endregion

		#region Properties
		private WinFormSession _session;
		public WinFormSession Session {
			get {
				return _session;
			}
		}
		#endregion

		#region Protected methods
		protected void OnRequestStatusNotification(AzManCommon.RequestStatusNotificationEventArgs args) {
			if (RequestStatusNotification != null) {
				RequestStatusNotification(this, args);
			}
		}
		#endregion

		#region Private methods
		private void preLoad() {
			#region Verificar la sesión
			if (WinFormApplication.IsNewSessionOrEmptySession(_session)) {
				//Si la sesión esta limpia
				OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.SesionFinalizada, "No ha iniciado sesión. Vuelva a ingresar por favor."));
				return;
			}
			#endregion

			#region Consultar el estado del Login y la Autorizacion
			//Si no se puede validar el estado ni el acceso del Login			
			if (_azManItem == null) {
				OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.ErrorGeneral, "Debe de establecer _azManItem antes del evento Load."));
				return;
			}

			//Obtener el Login de la sesion
			_loginInfo = WinFormApplication.GetLoginInfoFromSession(_session);
			if (!AzManLoginHelper.LoginHelper.CheckLoginStatusAndAuthorization(_loginInfo, WinFormApplication.ConfigValue_AzManStore, WinFormApplication.ConfigValue_AzManApplication, _azManItem.ToString(), out _loginStatus, out _authorizationType, out _outputString)) {
				OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.FalloConsultadoLoginAutorizacion, string.Format("Error al consultar el estado del login y la autorización a la operación [{0}].", _azManItem.ToString())));
				return;
			}
			#endregion

			#region Verificar el estado del Login
			switch (_loginStatus) {
				case LoginStatusEnum.Unknown:
					OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.LoginNoDisponible, "El estado del Login es desconocido.", LoginStatusEnum.Unknown));
					return;
				case LoginStatusEnum.Expired:
					OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.LoginNoDisponible, "El Login ha expirado. Vuelva a ingresar ", LoginStatusEnum.Expired));
					return;
				case LoginStatusEnum.LoggedOut:
					OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.LoginNoDisponible, "El Login ha terminado. Vuelva a ingresar ", LoginStatusEnum.LoggedOut));
					return;
				case LoginStatusEnum.LoggedIn:
					break;
			}
			#endregion

			#region Verificar la Autorizacion
			if (!(_authorizationType == AuthorizationType.Allow || _authorizationType == AuthorizationType.AllowWithDelegation)) {
				OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.AutorizacionDenegada, String.Format("Ud. no tiene acceso a la operación {0}. Consulte con el administrador del sistema.", _azManItem.ToString())));
				return;
			}
			#endregion
		}
		#endregion

		#region Event handlers
		protected void BaseForm_Load(object sender, EventArgs e) {
			try {
				if (!this.DesignMode) {
					preLoad();
				}
			}
			catch (Exception ex) {
				OnRequestStatusNotification(new RequestStatusNotificationEventArgs(RequestStatusTypeEnum.ErrorGeneral, string.Format("Error al realizar la verificación inicial del login y su acceso a la operación.\n\r{0}\n\r{1}", ex.Message, ex.StackTrace)));
			}
		}
		#endregion
	}
}
