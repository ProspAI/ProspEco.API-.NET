using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProspEco.ML.MLModels;
using ProspEco.ML.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços
builder.Services.AddSingleton<UsuarioPredictionService>();  // Adicionando o serviço de predição

// Adicionando Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Página interativa do Swagger
}

// Configuração de rotas
app.MapGet("/", () => "API está funcionando!");

// Exemplo de uma rota de predição (adicione outras conforme necessário)
app.MapPost("/predict", (UsuarioData usuarioData, UsuarioPredictionService predictionService) =>
{
    var prediction = predictionService.Predict(usuarioData);
    return Results.Ok(prediction);
});

app.Run();

Console.WriteLine("Hello, World!");