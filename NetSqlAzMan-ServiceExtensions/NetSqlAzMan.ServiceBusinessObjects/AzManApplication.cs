using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects {
	public class AzManApplication {
		public AzManApplication() {
			this.ApplicationGroups = new List<AzManApplicationGroup>();
			this.Attributes = new List<AzManApplicationAttribute>();
			this.Items = new List<AzManItem>();
		}

		[Key]
		[Required]
		public int ApplicationId { get; set; }

		public AzManStore Store { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public IEnumerable<AzManApplicationAttribute> Attributes { get; set; }

		public IEnumerable<AzManItem> Items { get; set; }

		public IEnumerable<AzManApplicationGroup> ApplicationGroups { get; set; }

		public bool IAmAdmin { get; set; }

		public bool IAmManager { get; set; }

		public bool IAmUser { get; set; }

		public bool IAmReader { get; set; }

		/// <summary>
		/// Clone object
		/// </summary>
		/// <returns></returns>
		public AzManApplication Clone() {
			var _clone = new AzManApplication() {
				ApplicationId = this.ApplicationId,
				Name = this.Name,
				Description = this.Description,
				IAmAdmin = this.IAmAdmin,
				IAmUser = this.IAmUser,
				IAmReader = this.IAmReader,
				IAmManager = this.IAmManager,
				Store = this.Store.Clone()
			};

			var _attributes = new List<AzManApplicationAttribute>();
			foreach (var _att in this.Attributes) {
				_attributes.Add(_att.Clone());
			}
			_clone.Attributes = _attributes;

			var _appGroups = new List<AzManApplicationGroup>();
			foreach (var _appGroup in this.ApplicationGroups) {
				_appGroups.Add(_appGroup.Clone());
			}
			_clone.ApplicationGroups = _appGroups;

			var _items = new List<AzManItem>();
			foreach (var _item in this.Items) {
				_items.Add(_item.Clone());
			}
			_clone.Items = _items;

			return _clone;
		}

		public string GetStoreName() {
			return this.Store.Name;
		}
	}
}
