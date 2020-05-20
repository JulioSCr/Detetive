using Detetive.Business.Data.Interfaces;
using Detetive.Data.Context;
using Detetive.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Detetive.Injection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DetetiveContext>();
            services.AddScoped<ISuspeitoRepository, SuspeitoRepository>();

            return services;
        }
    }
}
