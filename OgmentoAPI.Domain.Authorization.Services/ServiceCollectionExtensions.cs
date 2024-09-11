using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using TokenDemo.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, string dbConenctionString)
        {
            return services.AddTransient<IAuthorizationContext, AuthorizationDbContext>()
                .AddTransient<IUserContext, AuthorizationDbContext>()
                .AddDbContext<AuthorizationDbContext>(opts => opts.UseSqlServer(dbConenctionString))
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IUserService, UserService>();

        }
    }
}
