using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSqlAzMan.ServiceBusinessObjects.Utilities
{
	public class ObjectGenerator
	{
		public AppJsonResponseHttpRequestExceptionModel GetJsonResponseModelFromException(string topMessage, Exception exception)
		{
			if (exception == null)
				throw new ArgumentNullException(nameof(exception));

			Exception _currentException = exception;

			var _topModel = new NetSqlAzMan.ServiceBusinessObjects.AppJsonResponseHttpRequestExceptionModel()
			{
				Message = topMessage,
				ExceptionMessage = _currentException.Message,
				ExceptionType = _currentException.GetType().FullName,
				StackTrace = _currentException.StackTrace
			};

			var _currentModel = _topModel;

			_currentException = _currentException.InnerException;

			var _pos = 0;
			while (_currentException != null)
			{
				_pos++;

				_currentModel.InnerException = new AppJsonResponseHttpRequestExceptionModel();

				_currentModel = _currentModel.InnerException;

				_currentModel.Message = string.Format("Error anidado #{0}", _pos);
				_currentModel.ExceptionMessage = _currentException.Message;
				_currentModel.ExceptionType = _currentException.GetType().FullName;
				_currentModel.StackTrace = _currentException.StackTrace;

				_currentException = _currentException.InnerException;
			}

			return _topModel;
		}
	}
}
