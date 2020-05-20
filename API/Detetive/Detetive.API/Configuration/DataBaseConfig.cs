using Detetive.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detetive.API.Configuration
{
    public static class DataBaseConfig
    {
        public static IServiceCollection AddDataBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DetetiveContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
