using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SpaArchitectureMvp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //config.Formatters.Add(new BrowserJsonFormatter());

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.QueryStringMapping("xml", "true", "application/xml"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

        }
    }


    public class BrowserJsonFormatter : System.Net.Http.Formatting.JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, System.Net.Http.Headers.HttpContentHeaders headers, System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }
    }
}
