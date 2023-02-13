using System;

namespace AzManCommon
{
	public class RequestStatusNotificationEventArgs : EventArgs
	{
		public RequestStatusNotificationEventArgs(RequestStatusTypeEnum tipo, string descripcion) {
			this.Tipo = tipo;
			this.Descripcion = descripcion;
			this.EstadoLogin = null;
		}

		public RequestStatusNotificationEventArgs(RequestStatusTypeEnum tipo, string descripcion, AzManLoginHelper.AzManLoginSvcRef.LoginStatusEnum estadoLogin) {
			this.Tipo = tipo;
			this.Descripcion = descripcion;
			this.EstadoLogin = estadoLogin;
		}

		public RequestStatusTypeEnum Tipo {
			get;
			internal set;
		}

		public string Descripcion {
			get;
			internal set;
		}

		public Nullable<AzManLoginHelper.AzManLoginSvcRef.LoginStatusEnum> EstadoLogin {
			get;
			internal set;
		}
	}
}
