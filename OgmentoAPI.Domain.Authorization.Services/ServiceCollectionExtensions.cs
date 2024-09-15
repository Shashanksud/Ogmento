using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddTransient<IAuthorizationContext, AuthorizationDbContext>()
                .AddTransient<IUserContext, AuthorizationDbContext>()
                .AddDbContext<AuthorizationDbContext>(opts => opts.UseSqlServer(dbConnectionString))
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IUserService, UserService>();
                

        }
    }
}
