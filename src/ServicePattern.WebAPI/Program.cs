using Serilog;
using ServicePattern.Application;
using ServicePattern.Infrastructure;
using ServicePattern.Presentation;
using ServicePattern.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
configuration.AddEnvironmentVariables();

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration)
    .AddPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseGlobalExceptionHandler();
app.UseCors();
app.UseOutputCache();

app.UseRouting();

app.UseAuthorization(); 

app.MapControllers();

app.Run();