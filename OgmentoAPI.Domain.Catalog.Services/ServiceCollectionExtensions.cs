using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Catalog.Infrastructure;

namespace OgmentoAPI.Domain.Catalog.Services
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCatalog(this IServiceCollection services, string dbConnectionString)
		{
			return services.AddDbContext<CatalogDbContext>(opts => opts.UseSqlServer(dbConnectionString));
		}
	}
}
