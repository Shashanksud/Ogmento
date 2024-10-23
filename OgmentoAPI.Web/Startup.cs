using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OgmentoAPI.Domain.Authorization.Services;
using OgmentoAPI.Domain.Client.Services;
using OgmentoAPI.Domain.Common.Services;
using OgmentoAPI.Domain.Catalog.Services;
using System.Text;
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions;
using OgmentoAPI.Domain.Common.Abstractions.Helpers;
using Serilog;
using OgmentoAPI.Middlewares;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using static OgmentoAPI.Middlewares.ExceptionHandler;
using System.Net;
using Mapster;
using OgmentoAPI.Domain.Authorization.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Dto;
using System;
using OgmentoAPI.MapperConfig;


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

			services.AddMvc();
			MappingConfiguration.ConfigureMappings();

					   // configure strongly typed settings objects
					   var appSettingsSection = Configuration.GetSection("ServiceConfiguration");
			services.Configure<ServiceConfiguration>(appSettingsSection);
			services.Configure<FilePaths>(Configuration.GetSection("FilePaths"));
			string dbConnectionString = Configuration["ConnectionString:DefaultConnection"];
			services.AddAuth(dbConnectionString)
					.AddClient(dbConnectionString)
					.AddCommon(dbConnectionString)
					.AddCatalog(dbConnectionString);

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
				x.Events = new JwtBearerEvents
				{
					//OnMessageReceived = context =>
					//{
					//	context.Token = context.Request.Cookies["Auth"];
					//	return Task.CompletedTask;
					//},
					OnAuthenticationFailed = context =>
					{
						context.NoResult();
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						context.Response.ContentType = "application/json";
						return context.Response.WriteAsync(new ExceptionResponse(nameof(UnauthorizedAccessException),HttpStatusCode.Unauthorized, context.Exception.Message).ToString());
					}
				};

			});
			services.AddAuthorizationBuilder()
				.AddPolicy(PolicyNames.Administrator, policy => policy.RequireClaim(CustomClaimTypes.Role, UserRoles.Admin.ToString()))
				.AddPolicy(PolicyNames.Support, policy => policy.RequireClaim(CustomClaimTypes.Role, UserRoles.Support.ToString()))
				.AddPolicy(PolicyNames.Marketing, policy => policy.RequireClaim(CustomClaimTypes.Role, UserRoles.MarketingTeam.ToString()));
			services.AddHttpContextAccessor();
		
			services.AddCors(options =>
			{
#if DEBUG
				options.AddPolicy("CorsPolicy", builder =>
						builder.AllowAnyOrigin()
						.WithOrigins("http://localhost:5173")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials()
						);
#else
				options.AddPolicy("CorsPolicy", builder =>
				builder.WithOrigins("https://localhost:5174")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials()
				);
#endif
			});

		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("CorsPolicy");
			if (!env.IsDevelopment())
			{
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseRouting();
		
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();
			app.UseSerilogRequestLogging();
			app.UseMiddleware<ExceptionHandler>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			
		}
	}
}
