using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OgmentoAPI.Domain.Authorization.Abstractions;
using OgmentoAPI.Domain.Authorization.Infrastructure.Repository;
using OgmentoAPI.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddTransient<IAuthorizationContext, UserRepository>()
                .AddTransient<IUserContext, UserRepository>()
                .AddDbContext<AuthorizationDbContext>(opts => opts.UseSqlServer(dbConnectionString))
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICookieService,CookieService>();
        }
    }
}
