using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Authorization.Abstraction;
using TokenDemo.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            return services.AddTransient<IAuthorizationContext, AuthorizationDbContext>();
        }
    }
}
