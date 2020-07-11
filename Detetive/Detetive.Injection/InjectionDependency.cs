using Detetive.Business.Business;
using Detetive.Business.Business.Interfaces;
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
            container.Register<IAnotacaoArmaBusiness, AnotacaoArmaBusiness>(Lifestyle.Scoped);
            container.Register<IAnotacaoLocalBusiness, AnotacaoLocalBusiness>(Lifestyle.Scoped);
            container.Register<IAnotacaoSuspeitoBusiness, AnotacaoSuspeitoBusiness>(Lifestyle.Scoped);
            container.Register<ICrimeBusiness, CrimeBusiness>(Lifestyle.Scoped);
            container.Register<IJogadorBusiness, JogadorBusiness>(Lifestyle.Scoped);
            container.Register<IJogadorSalaBusiness, JogadorSalaBusiness>(Lifestyle.Scoped);
            container.Register<ILocalBusiness, LocalBusiness>(Lifestyle.Scoped);
            container.Register<IPortaLocalBusiness, PortaLocalBusiness>(Lifestyle.Scoped);
            container.Register<ISalaBusiness, SalaBusiness>(Lifestyle.Scoped);
            container.Register<ISuspeitoBusiness, SuspeitoBusiness>(Lifestyle.Scoped);
            container.Register<IArmaBusiness, ArmaBusiness>(Lifestyle.Scoped);
            container.Register<IPartidaBusiness, PartidaBusiness>(Lifestyle.Scoped);
            container.Register<IArmaJogadorSalaBusiness, ArmaJogadorSalaBusiness>(Lifestyle.Scoped);
            container.Register<ILocalJogadorSalaBusiness, LocalJogadorSalaBusiness>(Lifestyle.Scoped);
            container.Register<ISuspeitoJogadorSalaBusiness, SuspeitoJogadorSalaBusiness>(Lifestyle.Scoped);
            container.Register<IHistoricoBusiness, HistoricoBusiness>(Lifestyle.Scoped);

            // Data
            container.Register<IAnotacaoArmaRepository, AnotacaoArmaRepository>(Lifestyle.Scoped);
            container.Register<IAnotacaoLocalRepository, AnotacaoLocalRepository>(Lifestyle.Scoped);
            container.Register<IAnotacaoSuspeitoRepository, AnotacaoSuspeitoRepository>(Lifestyle.Scoped);
            container.Register<IArmaRepository, ArmaRepository>(Lifestyle.Scoped);
            container.Register<ICrimeRepository, CrimeRepository>(Lifestyle.Scoped);
            container.Register<IJogadorRepository, JogadorRepository>(Lifestyle.Scoped);
            container.Register<IJogadorSalaRepository, JogadorSalaRepository>(Lifestyle.Scoped);
            container.Register<ILocalRepository, LocalRepository>(Lifestyle.Scoped);
            container.Register<IPortaLocalRepository, PortaLocalRepository>(Lifestyle.Scoped);
            container.Register<ISalaRepository, SalaRepository>(Lifestyle.Scoped);
            container.Register<ISuspeitoRepository, SuspeitoRepository>(Lifestyle.Scoped);
            container.Register<IArmaJogadorSalaRepository, ArmaJogadorSalaRepository>(Lifestyle.Scoped);
            container.Register<ILocalJogadorSalaRepository, LocalJogadorSalaRepository>(Lifestyle.Scoped);
            container.Register<ISuspeitoJogadorSalaRepository, SuspeitoJogadorSalaRepository>(Lifestyle.Scoped);
            container.Register<IHistoricoRepository, HistoricoRepository>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
