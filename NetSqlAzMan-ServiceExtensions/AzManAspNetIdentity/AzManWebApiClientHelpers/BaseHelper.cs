using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzManAspNetIdentity.AzManWebApiClientHelpers
{
	public class BaseHelper<BSO>
	{
		protected string WebApiUri;

		protected BaseHelper(string webApiUri) {
			if (string.IsNullOrEmpty(webApiUri))
				throw new InvalidOperationException("Debe de especificar el URI del Web Api.");

			WebApiUri = webApiUri;
		}

		private HttpWebApiRequestException getHttpWebApiRequestException(string requestUri, HttpResponseMessage responseMessage) {
			//Validar StatusCode entre 200 y 299
			if (responseMessage.StatusCode >= System.Net.HttpStatusCode.OK && responseMessage.StatusCode < System.Net.HttpStatusCode.MultipleChoices)
				throw new InvalidOperationException("El código de estado de la respuesta es satisfactorio (200-299). No se puede generar el objeto Exception.");

			var _statusInfo = string.Format("{0}: {1}", Convert.ToInt32(responseMessage.StatusCode).ToString(), responseMessage.ReasonPhrase);

			string _mediaType;
			if (responseMessage.Content.Headers.ContentLength.Value.Equals(0))
				_mediaType = Common.MimeType_NoContent;
			else
				_mediaType = responseMessage.Content.Headers.ContentType.MediaType;

			switch (_mediaType) {
				case Common.MimeType_NoContent:
					var _viewModelForEmptyContent = new Models.EmptyContentResponseHttpRequestExceptionModel() {
						WebServer = responseMessage.Headers.Server.ToArray()[0].Product.ToString(),
						Date = responseMessage.Headers.Date.HasValue ? responseMessage.Headers.Date.Value.LocalDateTime.ToString() : "?"
					};

					return new HttpWebApiRequestException("Error al realizar la solicitud web.", requestUri, responseMessage.StatusCode, responseMessage.ReasonPhrase, _viewModelForEmptyContent);

				case Common.MimeType_AppJson:
					var _jsonContent = Task.Run(() => responseMessage.Content.ReadAsStringAsync()).Result;
					var _deserializedContent = JsonConvert.DeserializeObject<Models.AppJsonResponseHttpRequestExceptionModel>(_jsonContent);

					return new HttpWebApiRequestException("Error al realizar la solicitud web.", requestUri, responseMessage.StatusCode, responseMessage.ReasonPhrase, _deserializedContent);

				case Common.MimeType_TextHtml:
					var _viewModelForHtml = new Models.TextHtmlResponseHttpRequestExceptionModel(Task.Run(() => responseMessage.Content.ReadAsStringAsync()).Result);

					return new HttpWebApiRequestException("Error al realizar la solicitud web.", requestUri, responseMessage.StatusCode, responseMessage.ReasonPhrase, _viewModelForHtml);

				default: //En caso de llegar a este caso, se debería implementar el manejo para el MIME desconocido. Por ahora se dispara un error informando el caso.
					throw new NotSupportedException(string.Format("No se puede devolver una vista para las respuestas de error de solicitud http cuyo contenido en la respuesta sea el tipo \"{0}\". Se debe de implementar el soporte para ese tipo MIME.", _mediaType));
			}
		}

		protected Dictionary<string, IEnumerable<object>> GetStoredResponseError(string requestUri, HttpResponseMessage respMsg) {
			var _exception = getHttpWebApiRequestException(requestUri, respMsg);

			return new Dictionary<string, IEnumerable<object>> {
				{ "error", new object[] { _exception } }
			};
		}

		public bool IsResponseContentError(Dictionary<string, IEnumerable<object>> responseContent) {
			return responseContent.ContainsKey("error");
		}

		protected async Task<Dictionary<string, IEnumerable<object>>> GetStoredResponseEnumerableContentAsync(string jsonContent) {
			var _contentData = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<BSO>>(jsonContent));

			return new Dictionary<string, IEnumerable<object>> {
				{ "contentData", new object[] { _contentData } }
			};
		}

		protected async Task<Dictionary<string, IEnumerable<object>>> GetStoredResponseEnumerableContentAsync(HttpResponseMessage respMsg) {
			string _jsonContent = await respMsg.Content.ReadAsStringAsync();

			return await GetStoredResponseEnumerableContentAsync(_jsonContent);
		}

		protected async Task<Dictionary<string, IEnumerable<object>>> GetStoredResponseContentAsync(string jsonContent) {
			var _contentData = await Task.Run(() => JsonConvert.DeserializeObject<BSO>(jsonContent));

			return new Dictionary<string, IEnumerable<object>> {
				{ "contentData", new object[] { _contentData } }
			};
		}

		protected async Task<Dictionary<string, IEnumerable<object>>> GetStoredResponseContentAsync(HttpResponseMessage respMsg) {
			string _jsonContent = await respMsg.Content.ReadAsStringAsync();

			return await GetStoredResponseContentAsync(_jsonContent);
		}

		public void ThrowWebApiRequestException(Dictionary<string, IEnumerable<object>> responseContent) {
			throw GetWebApiRequestException(responseContent);
		}

		public HttpWebApiRequestException GetWebApiRequestException(Dictionary<string, IEnumerable<object>> responseContent) {
			var _values = responseContent["error"].ToArray();
			var _exception = (HttpWebApiRequestException)_values[0];

			return _exception;
		}

		public BSO GetSBOFromReturnedContent(Dictionary<string, IEnumerable<object>> responseContent) {
			var _values = responseContent["contentData"].ToArray();
			return (BSO)_values[0];
		}

		public IEnumerable<BSO> GetEnumerableSBOFromReturnedContent(Dictionary<string, IEnumerable<object>> responseContent) {
			var _values = responseContent["contentData"].ToArray();
			return (IEnumerable<BSO>)_values[0];
		}
	}
}