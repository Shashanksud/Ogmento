using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Common.Abstractions.Repository;
using OgmentoAPI.Domain.Common.Abstractions.Services;
using OgmentoAPI.Domain.Common.Infrastructure;
using OgmentoAPI.Domain.Common.Infrastructure.Repository;

namespace OgmentoAPI.Domain.Common.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommon(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddDbContext<CommonDBContext>(opts => opts.UseSqlServer(dbConnectionString))
				.AddTransient<IPictureRepository, PictureRepository>()
				.AddTransient<IPictureService, PictureServices>();
        }
    }
}
