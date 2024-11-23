using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProspEco.API.Configuration;
using ProspEco.API.Extensions;
using ProspEco.Model.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configuração do aplicativo
IConfiguration configuration = builder.Configuration;
AppConfiguration appConfiguration = new AppConfiguration();
configuration.Bind(appConfiguration);

appConfiguration.ConnectionStrings = new ConnectionStrings
{
    OracleFIAP = configuration.GetSection("ConnectionStrings:OracleFIAP").Value,
    DefaultConnection = configuration.GetSection("ConnectionStrings:DefaultConnection").Value
};


// Registrar configurações e serviços no container de injeção de dependência
builder.Services.Configure<AppConfiguration>(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDBContexts(appConfiguration);
builder.Services.AddSwagger(appConfiguration);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddHealthChecks(appConfiguration);
builder.Services.AddAuthenticationAndAuthorization();

var app = builder.Build();

// Configuração do pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Certifique-se de adicionar isso antes da autorização
app.UseAuthorization();

app.MapControllers();

// Endpoint para Health Checks
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health-check", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = HealthCheckExtensions.WriteResponse
    });
});

// Endpoint para logout
app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok("Logout realizado com sucesso.");
});

// Executar o aplicativo
app.Run();
