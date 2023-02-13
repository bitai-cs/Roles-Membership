using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManStorage {
		public AzManStorage() {
			this.Stores = new List<AzManStore>();
		}

		private string _connectionString;
		[Required]
		public string ConnectionString {
			get => _connectionString;
			set {
				_connectionString = value;

				if (!_connectionString.Contains(';'))
					return;

				var _dict = _connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
										 .Select(p => p.Split('='))
										 .ToDictionary(split => split[0].ToLower(), split => split[1]);
				if (_dict.Keys.Contains("user"))
					_dict.Remove("user");
				if (_dict.Keys.Contains("user id"))
					_dict.Remove("user id");
				if (_dict.Keys.Contains("password"))
					_dict.Remove("password");
				if (_dict.Keys.Contains("persist security info"))
					_dict.Remove("persist security info");
				if (_dict.Keys.Contains("integrated security"))
					_dict.Remove("integrated security");

				_connectionString = string.Empty;
				foreach (var _i in _dict) {
					_connectionString += string.Format("{0}={1}; ", _i.Key.ToUpper(), _i.Value);
				}
				_connectionString = _connectionString.Substring(0, _connectionString.Length - 2);
			}
		}

		public string Description {
			get {
				var _desc = string.Format("{0} | ", this.ConnectionString);
				_desc += string.Format("{0}: {1} | ", "DB VER.", this.DatabaseVesion);
				_desc += string.Format("{0}: {1} | ", "MODE", this.Mode.ToString());
				_desc = _desc.Substring(0, _desc.Length - 3);

				return _desc;
			}
			set {
				//Requerido SOLO para serialización.
			}
		}

		[Required]
		public string DatabaseVesion { get; set; }

		[Required]
		public int StorageTimeOut { get; set; }

		[Required]
		public bool IAmAdmin { get; set; }

		[Required]
		public bool LogInformations { get; set; }

		[Required]
		public bool LogErrors { get; set; }

		[Required]
		public bool LogWarnings { get; set; }

		[Required]
		public bool LogOnDb { get; set; }

		[Required]
		public bool LogOnEventLog { get; set; }

		[Required]
		public AzManMode Mode { get; set; }

		public IEnumerable<AzManStore> Stores { get; set; }

		#region Overriden methods
		public override string ToString() {
			return this.Description;
		}
		#endregion

		public AzManStorage Clone() {
			return new AzManStorage() {
				DatabaseVesion = this.DatabaseVesion,
				IAmAdmin = this.IAmAdmin,
				LogErrors = this.LogErrors,
				LogInformations = this.LogInformations,
				LogOnDb = this.LogOnDb,
				LogOnEventLog = this.LogOnEventLog,
				LogWarnings = this.LogWarnings,
				Mode = this.Mode,
				ConnectionString = this.ConnectionString,
				StorageTimeOut = this.StorageTimeOut, 
				//Description = Se genera al obtener la propiedad
			};
		}
	}
}