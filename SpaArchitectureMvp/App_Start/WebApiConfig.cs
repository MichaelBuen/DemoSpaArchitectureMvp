
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;


using SpaArchitectureMvp.App_Start.OtherComponents;


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

#if UseJil
            // We remove the built in Json serializer
            config.Formatters.Remove(config.Formatters.JsonFormatter);
            // and use Jil, fastest JSON Serializer
            config.Formatters.Insert(0, new JilFormatter());
#else
            // text/html allows us to view JSON directly in browser even we don't use tools such as REST Console
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
#endif


            var jsonFormatter = config.Formatters.OfType<System.Net.Http.Formatting.JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();



        }
    }


    //public class BrowserJsonFormatter : System.Net.Http.Formatting.JsonMediaTypeFormatter
    //{
    //    public BrowserJsonFormatter()
    //    {
    //        this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
    //    }

    //    public override void SetDefaultContentHeaders(Type type, System.Net.Http.Headers.HttpContentHeaders headers, System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
    //    {
    //        base.SetDefaultContentHeaders(type, headers, mediaType);
    //        headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
    //    }
    //}


    

}
