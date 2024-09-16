using AiConnect.Persistence;
using AiConnect.Repositories;
using AiConnect.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure o DbContext com a string de conexão correta
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configure a injeção de dependência para os repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IInteracaoRepository, InteracaoRepository>();

// Configurar o AppConfigurationManager
builder.Services.AddSingleton<AppConfigurationManager>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    AppConfigurationManager.Initialize(configuration);
    return AppConfigurationManager.Instance;
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AiConnect API",
        Version = "v1",
        Description = "API para gerenciar clientes, leads e interações.",
        Contact = new OpenApiContact
        {
            Name = "Stephany",
            Email = "rm98258@fiap.com.br"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AiConnect API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


