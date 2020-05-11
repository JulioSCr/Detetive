using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detetive.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            //services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
