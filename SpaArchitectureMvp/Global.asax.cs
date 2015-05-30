using System;
using System.Linq;
using System.Web.Http;

using System.Net.Http; // .RegisterForDispose extension method

using System.Reflection;

using UnitTestFriendlyDal;

namespace SpaArchitectureMvp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        LightInject.IServiceContainer _container;
        

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            _container = new LightInject.ServiceContainer();
                        

            Assembly assembly = typeof(WebApiApplication).Assembly;

            foreach (var controller in assembly.GetTypes().Where(t => typeof(ApiController).IsAssignableFrom(t)))
            {
                _container.Register(controller, new LightInject.PerScopeLifetime()); 
            }

            // LightInject's singleton is PerContainerLifetime
            // http://stackoverflow.com/questions/26948063/resolve-instances-in-static-functions-using-lightinject
            _container.Register<ISampleService, SampleService>(new LightInject.PerContainerLifetime());

            _container.Register<NHibernate.ISessionFactory>(factory => {
                return DomainMapping.Mapper.SessionFactory;
            }, new LightInject.PerContainerLifetime());

            _container.Register<IDomainAccessFactory>(factory => new DomainAccessFactory(_container.GetInstance<NHibernate.ISessionFactory>()), new LightInject.PerContainerLifetime());

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
        LightInject.IServiceContainer _container;

        public ControllerActivator(LightInject.IServiceContainer container)
        {
            _container = container;
        }

        public System.Web.Http.Controllers.IHttpController Create(
            System.Net.Http.HttpRequestMessage request,
            System.Web.Http.Controllers.HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {

            using (_container.BeginScope())
            {
                var controller = (System.Web.Http.Controllers.IHttpController)_container.GetInstance(controllerType);
                return controller;
            }
            
            
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
