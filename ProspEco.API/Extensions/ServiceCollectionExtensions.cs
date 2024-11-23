// ProspEco.API/Extensions/ServiceCollectionExtensions.cs

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProspEco.API.Configuration;
using ProspEco.Database.Contexts;
using Serilog;
using Oracle.EntityFrameworkCore.Infrastructure; // Adicionado para resolver UseOracle

namespace ProspEco.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            // Registre serviços adicionais aqui, se necessário
            // Exemplo:
            // service.AddScoped<IUsuarioUseService, UsuarioUseService>();

            return service;
        }

        public static IServiceCollection AddDBContexts(this IServiceCollection service, AppConfiguration appConfiguration)
        {
            service.AddDbContext<ProspEcoContext>(options =>
            {
                options.UseOracle(appConfiguration.ConnectionStrings.OracleFIAP,
                    oracleOptions => oracleOptions.MigrationsAssembly("ProspEco.Database"));
            });

            /*
            service.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseInMemoryDatabase("UserAccess");
            });
            */

            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            // Registre repositórios adicionais aqui, se necessário
            // Exemplo:
            // service.AddScoped<IRepository<Usuario>, Repository<Usuario>>();

            return service;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection service, AppConfiguration appConfiguration)
        {
            service.AddSwaggerGen(swagger =>
            {
                swagger.TagActionsBy(api =>
                {
                    var identityEndpoints = new[] { "logout", "confirmEmail", "resendConfirmationEmail", "forgotPassword", "resetPassword", "manage", "refresh", "register", "login", "logout", "confirm-email", "forgot-password", "reset-password", "change-password" };

                    if (api.RelativePath != null && identityEndpoints.Any(endpoint => api.RelativePath.Contains(endpoint)))
                    {
                        return new[] { "Autenticação e Autorização" };
                    }

                    return new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] };
                });

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = appConfiguration.Swagger.Title,
                    Version = "v1",
                    Description = appConfiguration.Swagger.Description,
                    Contact = new OpenApiContact()
                    {
                        Email = appConfiguration.Swagger.Email,
                        Name = appConfiguration.Swagger.Name
                    }
                }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);
            });

            return service;
        }


        public static IServiceCollection AddHealthChecks(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services
            .AddHealthChecks()
            .AddOracle(appConfiguration.ConnectionStrings.OracleFIAP);

            return services;
        }

        /*
        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
        {

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddIdentityApiEndpoints<AccessUser>()
                .AddEntityFrameworkStores<AuthorizationDbContext>();

            return services;
        }
        */

    }
}
