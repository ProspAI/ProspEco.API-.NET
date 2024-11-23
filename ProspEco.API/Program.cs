using Microsoft.EntityFrameworkCore;
using ProspEco.API.Configuration;
using ProspEco.Database.Contexts;
using ProspEco.Repository.Implementations;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Implementations;
using ProspEco.Service.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Adicionar serviços ao contêiner
builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("AppConfiguration"));

// Configurar o DbContext com a string de conexão Oracle
var connectionString = builder.Configuration.GetSection("AppConfiguration:ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<ProspEcoContext>(options =>
    options.UseOracle(connectionString));

// Registrar repositórios específicos
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAparelhoRepository, AparelhoRepository>();
builder.Services.AddScoped<IBandeiraTarifariaRepository, BandeiraTarifariaRepository>();
builder.Services.AddScoped<IConquistaRepository, ConquistaRepository>();
builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
builder.Services.AddScoped<IRecomendacaoRepository, RecomendacaoRepository>();
builder.Services.AddScoped<IRegistroConsumoRepository, RegistroConsumoRepository>();

// Registrar serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAparelhoService, AparelhoService>();
builder.Services.AddScoped<IBandeiraTarifariaService, BandeiraTarifariaService>();
builder.Services.AddScoped<IConquistaService, ConquistaService>();
builder.Services.AddScoped<IMetaService, MetaService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<IRecomendacaoService, RecomendacaoService>();
builder.Services.AddScoped<IRegistroConsumoService, RegistroConsumoService>();

// Adicionar AutoMapper e configurar perfis
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Adicionar controllers
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var appConfig = builder.Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = appConfig.Swagger.Title,
        Description = appConfig.Swagger.Description,
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = appConfig.Swagger.Name,
            Email = appConfig.Swagger.Email
        },
        Version = "v1"
    });

    // Remover configuração de autenticação JWT no Swagger
});

// Configurar CORS (opcional)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

// Remover middleware de tratamento de exceções
// app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

// Usar CORS (opcional)
app.UseCors("AllowAll");

// Remover autenticação e autorização
// app.UseAuthentication(); // Se estiver usando autenticação
// app.UseAuthorization();

app.MapControllers();

app.Run();
