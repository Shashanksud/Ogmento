using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OgmentoAPI.Domain.Authorization.Services;
using OgmentoAPI.Domain.Client.Services;
using OgmentoAPI.Domain.Common.Services;
using System.Text;
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;


namespace OgmentoAPI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddMvc();
            //services.AddDbContext<UserAuthorization>(opts => opts.UseSqlServer(Configuration["ConnectionString:DefaultConnection"]));
            
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("ServiceConfiguration");
            services.Configure<ServiceConfiguration>(appSettingsSection);

            services.AddAuth(Configuration["ConnectionString:DefaultConnection"])
                    .AddClient(Configuration["ConnectionString:DefaultConnection"])
                    .AddCommon(Configuration["ConnectionString:DefaultConnection"]);

            // configure jwt authentication
            var serviceConfiguration = appSettingsSection.Get<ServiceConfiguration>();
            var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;

            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", UserRoles.Admin.ToString()));
                options.AddPolicy("SupportPolicy", policy => policy.RequireClaim("Role", UserRoles.Support.ToString()));
                options.AddPolicy("MarketingTeamPolicy", policy => policy.RequireClaim("Role", UserRoles.MarketingTeam.ToString()));
            });
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
