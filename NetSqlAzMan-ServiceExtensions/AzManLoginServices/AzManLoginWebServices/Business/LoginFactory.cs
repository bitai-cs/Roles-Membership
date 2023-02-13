using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AzManLoginWebServices.BusinessLayer.DataLayer;

namespace AzManLoginWebServices.BusinessLayer
{
	public class LoginFactory
	{
		#region data Members

		LoginSql _dataObject = null;

		#endregion

		#region Constructor

		public LoginFactory() {
			_dataObject = new LoginSql();
		}

		#endregion

		#region Private methods

		#endregion

		#region Public Methods

		/// <summary>
		/// Insert new Login
		/// </summary>
		/// <param name="businessObject">Login object</param>
		/// <returns>true for successfully saved</returns>
		public bool Insert(Login businessObject) {
			if (!businessObject.IsValid) {
				throw new InvalidBusinessObjectException(businessObject.BrokenRulesList.ToString());
			}


			return _dataObject.Insert(businessObject);

		}

		/// <summary>
		/// Update existing Login
		/// </summary>
		/// <param name="businessObject">Login object</param>
		/// <returns>true for successfully saved</returns>
		public bool Update(Login businessObject) {
			if (!businessObject.IsValid) {
				throw new InvalidBusinessObjectException(businessObject.BrokenRulesList.ToString());
			}


			return _dataObject.Update(businessObject);
		}

		/// <summary>
		/// get Login by primary key.
		/// </summary>
		/// <param name="keys">primary key</param>
		/// <returns>Student</returns>
		public Login GetByPrimaryKey(LoginKeys keys) {
			return _dataObject.SelectByPrimaryKey(keys);
		}

		/// <summary>
		/// get list of Login by field
		/// </summary>
		/// <param name="fieldName">field name</param>
		/// <param name="value">value</param>
		/// <returns>list</returns>
		public List<Login> GetAllBy(Login.LoginFields fieldName, object value) {
			return _dataObject.SelectByField(fieldName.ToString(), value);
		}

		/// <summary>
		/// delete by primary key
		/// </summary>
		/// <param name="keys">primary key</param>
		/// <returns>true for succesfully deleted</returns>
		public bool Delete(LoginKeys keys) {
			return _dataObject.Delete(keys);
		}

		/// <summary>
		/// delete Login by field.
		/// </summary>
		/// <param name="fieldName">field name</param>
		/// <param name="value">value</param>
		/// <returns>true for successfully deleted</returns>
		public bool Delete(Login.LoginFields fieldName, object value) {
			return _dataObject.DeleteByField(fieldName.ToString(), value);
		}

		//public bool StartLogin(Login bo) {
		//   Exception exceHandled = null;

		//   AzManLoginWebServices.ServiceClients.Basgosoft_NetSqlAzManCacheWS.DBUser _dbUser = null;
		//   bool _pwdIsValid = false;

		//   //Validar el usuario y su contraseña
		//   if (!this.azMan_validateDBUser(userName, pwd, out _dbUser, out _pwdIsValid, out exceHandled))
		//      throw exceHandled;

		//   //La contraseña no es valida
		//   if (!_pwdIsValid)
		//      throw new Exception(string.Format("Usuario y/o contraseña {0} incorrectos.", pwd.ToString()));
		//}

		//public bool CheckExpiration(Login businessObject, out bool expired) {
		//   if (!businessObject.Expired) {
		//      if (businessObject.
		//   }
		//}

		#endregion
	}
}
