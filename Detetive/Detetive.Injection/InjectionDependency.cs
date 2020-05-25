using Detetive.Business.Data.Interfaces;
using Detetive.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace Detetive.Injection
{
    public static class InjectionDependency
    {
        public static void Register()
        {
            Container container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Business

            // Data
            container.Register<ISuspeitoRepository, SuspeitoRepository>(Lifestyle.Scoped);


            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
