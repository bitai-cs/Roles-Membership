using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManStore {
		public AzManStore() {
			this.Attributes = new List<AzManStoreAttribute>();
			this.StoreGroups = new List<AzManStoreGroup>();
			this.Applications = new List<AzManApplication>();
		}

		[Key]
		[Required]
		public int StoreId { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public bool IAmAdmin { get; set; }
		public bool IAmManager { get; set; }
		public bool IAmUser { get; set; }
		public bool IAmReader { get; set; }

		public AzManStorage Storage { get; set; }

		public IEnumerable<AzManApplication> Applications { get; set; }

		public IEnumerable<AzManStoreGroup> StoreGroups { get; set; }

		public IEnumerable<AzManStoreAttribute> Attributes { get; set; }

		public AzManStore Clone() {
			var _clone = new AzManStore() {
				Name = this.Name,
				StoreId = this.StoreId,
				Description = this.Description,
				IAmAdmin = this.IAmAdmin,
				IAmManager = this.IAmManager,
				IAmReader = this.IAmReader,
				IAmUser = this.IAmUser,
				Storage = this.Storage.Clone()
			};

			var _listAttr = new List<AzManStoreAttribute>();
			foreach (var _i in this.Attributes)
				_listAttr.Add(new AzManStoreAttribute {
					AttributeId = _i.AttributeId,
					Key = _i.Key,
					Value = _i.Value,
					Owner = _i.Owner.Clone()
				});

			_clone.Attributes = _listAttr;

			return _clone;
		}
	}
}
