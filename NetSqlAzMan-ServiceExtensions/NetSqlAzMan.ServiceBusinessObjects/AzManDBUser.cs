using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManDBUser {
		/// <summary>
		/// Custom Unique identifier of the DB User
		/// </summary>
		[Display(Name = "Custom Sid")]
		public AzManSid CustomSid {
			get; set;
		}

		/// <summary>
		/// Username of the DB User
		/// </summary>
		[Display(Name = "Usuario")]
		public string UserName {
			get; set;
		}

		/// <summary>
		/// Gets the custom columns.
		/// </summary>
		/// <value>The custom columns.</value>
		[Display(Name = "Datos Personalizados")]
		public Dictionary<string, object> CustomColumns {
			get; set;
		}

		[Display(Name = "Perfil Dominio")]
		public string DomainProfile {
			get; set;
		}

		[Display(Name = "Es una entrada Ldap")]
		public bool IsLdapEntry {
			get; set;
		}

		[Display(Name = "Nombre del Usuario")]
		public string DisplayName {
			get; set;
		}
	}
}
