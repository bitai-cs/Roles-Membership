using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AzManStructureMgtWebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config) {
			//Remover el serializador XML
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			//Configurar el Serializador JSON
			//NO SE DEBE DE USAR LA SGTE. CONFIGURACION. SE TRATO DE USAR CUANDO 
			//SE QUERIA SERIALIZAR ENTIDADES DE ENTITYFRAMEWORK, LO CUAL NO ES ÓPTIMO
			//config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;	

			// Rutas de API web
			config.MapHttpAttributeRoutes();

			//Registering GlobalExceptionHandler
			config.Services.Replace(typeof(IExceptionHandler), new Handlers.GlobalExceptionHandler());

			//Register model validation filter
			config.Filters.Add(new Filters.ValidateModelStateFilter());

			config.Routes.MapHttpRoute(
				 name: "DefaultApi",
				 routeTemplate: "api/{controller}/{*id}",
				 defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
