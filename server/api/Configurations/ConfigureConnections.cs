﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactAdvantage.Data.EntityFramework;

namespace ReactAdvantage.API.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("default");
            services.AddDbContext<ReactAdvantageContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
