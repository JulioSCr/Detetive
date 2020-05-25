using SimpleInjector;

namespace Detetive.Injection
{
    public static class Injector
    {
        private static Container container;

        public static Container Container
        {
            get
            {
                if (container == null)
                    container = new Container();
                return container;
            }
        }

        public static TInstance GetInstance<TInstance>() where TInstance : class
        {
            return Container.GetInstance<TInstance>();
        }
    }
}
