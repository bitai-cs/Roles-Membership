using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AzManStructureMgtWebApi.Controllers
{
	public class AzManItemsController :BaseApiController
	{
		public AzManItemsController() : base() {
		}


		#region Private methods
		internal NetSqlAzMan.ServiceBusinessObjects.AzManItem getSBOFromItem(NetSqlAzMan.Interfaces.IAzManItem item, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			var _items = new List<NetSqlAzMan.Interfaces.IAzManItem>() { item };

			return getSBOFromListOfItems(_items, item.Application, loadAttributes, loadMembers, loadItemsWhereIAmAMember, loadAuthorizations).First();
		}

		internal IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem> getSBOFromListOfItems(IEnumerable<NetSqlAzMan.Interfaces.IAzManItem> items, NetSqlAzMan.Interfaces.IAzManApplication parentApplication, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			var _listSBO = new List<NetSqlAzMan.ServiceBusinessObjects.AzManItem>();

			var _sboParentApplication = new AzManApplicationsController().getSBOFromApplication(parentApplication, false, false, false);

			foreach (var _item in items) {
				var _sboItem = new NetSqlAzMan.ServiceBusinessObjects.AzManItem {
					ItemId = _item.ItemId,
					Name = _item.Name,
					Description = _item.Description,
					Application = _sboParentApplication,
					BizRuleSource = _item.BizRuleSource,
					BizRuleSourceLanguage = _item.BizRuleSourceLanguage == NetSqlAzMan.BizRuleSourceLanguage.CSharp ? NetSqlAzMan.ServiceBusinessObjects.BizRuleSourceLanguage.CSharp : NetSqlAzMan.ServiceBusinessObjects.BizRuleSourceLanguage.VBNet,
					ItemType = _item.ItemType == NetSqlAzMan.Interfaces.ItemType.Operation ? NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation : (_item.ItemType == NetSqlAzMan.Interfaces.ItemType.Task ? NetSqlAzMan.ServiceBusinessObjects.ItemType.Task : NetSqlAzMan.ServiceBusinessObjects.ItemType.Role)
					/*Members = Se cargan después*/
					/*ItemsWhereIAmAMember = Se cargan después*/
					/*Authorizations = Se cargan después*/
				};

				if (loadAttributes) {
					var _sboAttributes = new List<NetSqlAzMan.ServiceBusinessObjects.AzManItemAttribute>();
					foreach (var _attribute in _item.GetAttributes()) {
						_sboAttributes.Add(new NetSqlAzMan.ServiceBusinessObjects.AzManItemAttribute() {
							AttributeId = _attribute.AttributeId,
							Key = _attribute.Key,
							Value = _attribute.Value,
							Owner = _sboItem.Clone()
						});
					}

					_sboItem.Attributes = _sboAttributes;
				}

				//Load Members
				if (loadMembers) {
					var _sboItemMembers = getSBOFromListOfItems(_item.GetMembers(), parentApplication, loadAttributes, false, false, loadAuthorizations);
					_sboItem.Members = _sboItemMembers;
				}

				//Load ItemsWhereIAmAMember
				if (loadItemsWhereIAmAMember) {
					var _sboParents = getSBOFromListOfItems(_item.GetItemsWhereIAmAMember(), parentApplication, loadAttributes, false, false, loadAuthorizations);
					_sboItem.ItemsWhereIAmAMember = _sboParents;
				}

				//Load Authorizations
				if (loadAuthorizations) {
					var _sboAuthortizations = new AzManAuthorizationsController().getSBOFromListOfAuthorizations(_item.GetAuthorizations(), _item, loadAttributes);
					var _tmp = new System.Collections.ObjectModel.ReadOnlyCollection<NetSqlAzMan.ServiceBusinessObjects.AzManAuthorization>(_sboAuthortizations.ToList());
					_sboItem.Authorizations = _tmp;
				}

				_listSBO.Add(_sboItem);
			}

			return _listSBO;
		}
		#endregion


		/// <summary>
		/// Obtiene un item por su id (name)
		/// </summary>
		/// <param name="id">Nombre del ITem</param>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="loadAttributes"></param>
		/// <param name="loadMembers"></param>
		/// <param name="loadItemsWhereIAmAMember"></param>
		/// <param name="loadAuthorizations"></param>
		/// <returns>En el body un AzManItem</returns>
		[HttpGet]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManItem))]
		public async Task<IHttpActionResult> Get(string id, string store, string application, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			var _parentApplication = await Task.Run(() => _storage.Stores[store].Applications[application]);

			var _item = await Task.Run(() => _storage.Stores[store].Applications[application].GetItem(id));

			if (_item == null)
				return NotFound();

			var _sbo = getSBOFromItem(_item, loadAttributes, loadMembers, loadItemsWhereIAmAMember, loadAuthorizations);

			return Ok(_sbo);
		}


		/// <summary>
		/// Obtiene todos los Items de un Application, de acuerdo al ItemType: Role, Task, Operation
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="itemType"></param>
		/// <param name="loadAttributes"></param>
		/// <param name="loadMembers"></param>
		/// <param name="loadItemsWhereIAmAMember"></param>
		/// <param name="loadAuthorizations"></param>
		/// <returns>En el body un AzManItem</returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem>))]
		public async Task<IHttpActionResult> Get(string store, string application, NetSqlAzMan.ServiceBusinessObjects.ItemType itemType, bool loadAttributes, bool loadMembers, bool loadItemsWhereIAmAMember, bool loadAuthorizations) {
			var _parentApplication = await Task.Run(() => _storage.Stores[store].Applications[application]);

			var _items = await Task.Run(() => {
				NetSqlAzMan.Interfaces.ItemType? _type = null;
				switch (itemType) {
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Operation:
						_type = NetSqlAzMan.Interfaces.ItemType.Operation;
						break;
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Task:
						_type = NetSqlAzMan.Interfaces.ItemType.Task;
						break;
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.Role:
						_type = NetSqlAzMan.Interfaces.ItemType.Role;
						break;
					case NetSqlAzMan.ServiceBusinessObjects.ItemType.All:
						_type = null;
						break;
				}

				if (_type == null)
					return _storage.Stores[store].Applications[application].GetItems();
				else
					return _storage.Stores[store].Applications[application].GetItems(_type.Value);
			});

			var _return = getSBOFromListOfItems(_items, _parentApplication, loadAttributes, loadMembers, loadItemsWhereIAmAMember, loadAuthorizations);

			return Ok(_return);
		}


		/// <summary>
		/// Obtiene de un Item, todos los Items (members) que anida, de acuerdo 
		/// al ItemType: Role, Task, Operation, All  
		/// </summary>
		/// <param name="store"></param>
		/// <param name="application"></param>
		/// <param name="Item"></param>
		/// <param name="itemMemberType"></param>
		/// <param name="loadItemMemberAttributes"></param>
		/// <param name="loadItemMemberMembers"></param>
		/// <param name="loadItemsWhereItemMemberIsMember"></param>
		/// <param name="loadItemMemberAuthorizations"></param>
		/// <returns>En el body IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem></returns>
		[HttpGet]
		[ResponseType(typeof(IEnumerable<NetSqlAzMan.ServiceBusinessObjects.AzManItem>))]
		public async Task<IHttpActionResult> Get(string store, string application, string Item, NetSqlAzMan.ServiceBusinessObjects.ItemType itemMemberType, bool loadItemMemberAttributes, bool loadItemMemberMembers, bool loadItemsWhereItemMemberIsMember, bool loadItemMemberAuthorizations) {
			var _parentApplication = await Task.Run(() => _storage.Stores[store].Applications[application]);

			var _itemMembers = await Task.Run(() => {
				if (itemMemberType != NetSqlAzMan.ServiceBusinessObjects.ItemType.All)
					return _storage.Stores[store].Applications[application].Items[Item].GetMembers().Where(f => f.ItemType == (ItemType)itemMemberType);
				else
					return _storage.Stores[store].Applications[application].Items[Item].GetMembers();
			});

			var _return = getSBOFromListOfItems(_itemMembers, _parentApplication, loadItemMemberAttributes, loadItemMemberMembers, loadItemsWhereItemMemberIsMember, loadItemMemberAuthorizations);

			return Ok(_return);
		}


		[HttpPost]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManItem))]
		public async Task<IHttpActionResult> Post(string store, string application, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManItem data) {
			if (!this.ModelState.IsValid)
				return BadRequest(this.ModelState);

			IAzManItem _newItem;
			try {
				/*Calling to NetSqlAzMan BL*/
				_newItem = await Task.Run(() => {
					//Iniciar transacción
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);
					//Crear Item
					var _item = _storage.Stores[store].Applications[application].CreateItem(data.Name, data.Description, (ItemType)data.ItemType);
					//Agregar miembros
					foreach (string _name in data.MembersToAddOrRemove.MembersToAdd) {
						IAzManItem _newMember = _storage.Stores[store].Applications[application].GetItem(_name);
						_item.AddMember(_newMember);
					}
					//Remover miembros
					foreach (string _name in data.MembersToAddOrRemove.MembersToRemove) {
						IAzManItem _removedMember = _storage.Stores[store].Applications[application].GetItem(_name);
						_item.RemoveMember(_removedMember);
					}
					//Confirmar transacción
					_storage.CommitTransaction();

					return _item;
				});
			}
			catch {
				_storage.RollBackTransaction();
				throw;
			}

			var _sbo = getSBOFromItem(_newItem, false, false, false, false);

			return CreatedAtRoute("DefaultApi", new { id = _sbo.Name }, _sbo);
		}


		[HttpPut]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManItem))]
		public async Task<HttpResponseMessage> Put(string id, string store, string application, [FromBody]NetSqlAzMan.ServiceBusinessObjects.AzManItem bdo) {
			if (!this.ModelState.IsValid)
				return GetResponseMessageForInvalidModel(this.ModelState);

			var _item = _storage.Stores[store].Applications[application].GetItem(id);
			if (_item == null)
				return GetResponseMessageForNotFoundId(id);

			try {
				/*Calling to NetSqlAzMan BL*/
				await Task.Run(() => {
					//Iniciar transacción
					_storage.BeginTransaction(NetSqlAzMan.Interfaces.AzManIsolationLevel.ReadUncommitted);
					//Modificar datos
					_item.Rename(bdo.Name);
					_item.Update(bdo.Description);
					//Agregar miembros
					foreach (string _name in bdo.MembersToAddOrRemove.MembersToAdd) {
						IAzManItem _newMember = _storage.Stores[store].Applications[application].GetItem(_name);
						_item.AddMember(_newMember);
					}
					//Remover miembros
					foreach (string _name in bdo.MembersToAddOrRemove.MembersToRemove) {
						IAzManItem _removedMember = _storage.Stores[store].Applications[application].GetItem(_name);
						_item.RemoveMember(_removedMember);
					}
					//Confirmar transacción
					_storage.CommitTransaction();
				});
			}
			catch {
				_storage.RollBackTransaction();
				throw;
			}

			var _updatedBso = getSBOFromItem(_item, false, false, false, false);

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _updatedBso);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se actualizó los datos del Item: {0}/{1}/{2}.", store, application, id));

			return _respMsg;
		}


		[HttpDelete]
		[ResponseType(typeof(NetSqlAzMan.ServiceBusinessObjects.AzManItem))]
		public async Task<HttpResponseMessage> Delete(string id, string store, string application) {
			var _item = _storage.Stores[store].Applications[application].GetItem(id);
			if (_item == null)
				return GetResponseMessageForNotFoundId(id);

			var _sbo = getSBOFromItem(_item, false, false, false, false);

			/*Calling to NetSqlAzMan BL*/
			await Task.Run(() => {
				_item.Delete();
			});

			var _respMsg = this.Request.CreateResponse(HttpStatusCode.OK, _sbo);
			_respMsg.Headers.Add(Global.RESPONSE_HEADER_WebApiMessage, string.Format("Se eliminó el Item: {0}/{1}/{2}.", store, application, id));

			return _respMsg;
		}
	}
}
