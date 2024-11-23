using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProspEco.API.Configuration;
using ProspEco.Database;
using ProspEco.Repository;
using ProspEco.Service;
using Microsoft.AspNetCore.Identity;
using ProspEco.Model.Entities;

namespace ProspEco.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            // Registra os serviços no contêiner de dependência
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IAparelhoService, AparelhoService>();
            service.AddScoped<IBandeiraTarifariaService, BandeiraTarifariaService>();
            service.AddScoped<IConquistaService, ConquistaService>();
            service.AddScoped<IMetaService, MetaService>();
            service.AddScoped<INotificacaoService, NotificacaoService>();
            service.AddScoped<IRecomendacaoService, RecomendacaoService>();
            service.AddScoped<IRegistroConsumoService, RegistroConsumoService>();

            return service;
        }

        public static IServiceCollection AddDBContexts(this IServiceCollection service, AppConfiguration appConfiguration)
        {
            // Configuração do contexto principal
            service.AddDbContext<ProspEcoDbContext>(options =>
            {
                options.UseOracle(appConfiguration.ConnectionStrings.OracleFIAP,
                    builder => builder.MigrationsAssembly("ProspEco.Database"));
            });

            // Configuração do contexto para Identity
            service.AddDbContext<AuthorizationDbContext>(options =>
            {
                options.UseInMemoryDatabase("UserAccess");
            });

            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            // Registra os repositórios genéricos no contêiner de dependência
            service.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            service.AddScoped<IRepository<Aparelho>, Repository<Aparelho>>();
            service.AddScoped<IRepository<BandeiraTarifaria>, Repository<BandeiraTarifaria>>();
            service.AddScoped<IRepository<Conquista>, Repository<Conquista>>();
            service.AddScoped<IRepository<Meta>, Repository<Meta>>();
            service.AddScoped<IRepository<Notificacao>, Repository<Notificacao>>();
            service.AddScoped<IRepository<Recomendacao>, Repository<Recomendacao>>();
            service.AddScoped<IRepository<RegistroConsumo>, Repository<RegistroConsumo>>();

            return service;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection service, AppConfiguration appConfiguration)
        {
            // Configura o Swagger para documentação da API
            service.AddSwaggerGen(swagger =>
            {
                swagger.TagActionsBy(api =>
                {
                    var identityEndpoints = new[]
                    {
                        "logout", "confirmEmail", "resendConfirmationEmail",
                        "forgotPassword", "resetPassword", "manage", "refresh",
                        "register", "login", "confirm-email", "forgot-password",
                        "reset-password", "change-password"
                    };

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
                    Contact = new OpenApiContact
                    {
                        Email = appConfiguration.Swagger.Email,
                        Name = appConfiguration.Swagger.Name
                    }
                });

                // Adiciona os comentários XML para melhor documentação
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);
            });

            return service;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            // Adiciona verificações de saúde para o banco de dados Oracle
            services.AddHealthChecks()
                .AddOracle(appConfiguration.ConnectionStrings.OracleFIAP);

            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
        {
            // Configura autenticação e autorização
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddIdentityCore<IdentityUser>(options => { })
                .AddEntityFrameworkStores<AuthorizationDbContext>();

            return services;
        }
    }
}
