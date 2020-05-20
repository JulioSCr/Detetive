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
            //services.AddScoped<MeuDbContext>();
            //services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
