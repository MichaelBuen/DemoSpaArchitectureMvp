using System;
using System.Linq;
using System.Web.Http;

using DryIoc;
using System.Reflection;

namespace SpaArchitectureMvp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        DryIoc.Container _container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            _container = new DryIoc.Container();
                        

            Assembly assembly = typeof(WebApiApplication).Assembly;

            foreach (var controller in assembly.GetTypes().Where(t => typeof(ApiController).IsAssignableFrom(t)))
            {
                _container.Register(controller, DryIoc.Reuse.InResolutionScope);                
            }

            _container.Register<ISampleService, SampleService>(DryIoc.Reuse.Singleton);
            

            GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Dispatcher.IHttpControllerActivator), new ControllerActivator(_container));

        }

        public override void Dispose()
        {
            if (_container != null) _container.Dispose();
            base.Dispose();
        }
    }



    // http://blog.ploeh.dk/2012/09/28/DependencyInjectionandLifetimeManagementwithASP.NETWebAPI/
    // http://blog.ploeh.dk/2012/10/03/DependencyInjectioninASP.NETWebAPIwithCastleWindsor/
    public class ControllerActivator : System.Web.Http.Dispatcher.IHttpControllerActivator
    {
        DryIoc.Container _container;

        public ControllerActivator(DryIoc.Container container)
        {
            _container = container;
        }

        public System.Web.Http.Controllers.IHttpController Create(
            System.Net.Http.HttpRequestMessage request,
            System.Web.Http.Controllers.HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {

            var controller = (System.Web.Http.Controllers.IHttpController)_container.Resolve(controllerType);

            _container.ResolvePropertiesAndFields(controller);

            return controller;
        }       
    }

    public interface ISampleService
    {
        string GetMessage();
    }


    public class SampleService : ISampleService
    {

        string ISampleService.GetMessage()
        {
            return "Hello Kel " + Guid.NewGuid();
        }
    }
}
