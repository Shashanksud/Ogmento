using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using OgmentoAPI.Domain.Client.Infrastructure;
using OgmentoAPI.Domain.Client.Infrastructure.Repository;



namespace OgmentoAPI.Domain.Client.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClient(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddDbContext<ClientDBContext>(opts => opts.UseSqlServer(dbConnectionString))
                           .AddTransient<ISalesCenterService, SalesCenterService>()
                           .AddTransient<ISalesCenterRepository,SalesCenterRepository>();
        }
    }
}
