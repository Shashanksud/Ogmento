﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Common.Infrastructure;


namespace OgmentoAPI.Domain.Common.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommon(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddDbContext<CommonDBContext>(opts => opts.UseSqlServer(dbConnectionString));
        }

    }
}