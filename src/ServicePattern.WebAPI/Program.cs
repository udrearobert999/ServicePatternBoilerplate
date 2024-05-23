using Serilog;
using ServicePattern.Application;
using ServicePattern.Infrastructure;
using ServicePattern.Presentation;
using ServicePattern.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration)
    .AddPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(configuration); });

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
app.UseRouting();

app.UseOutputCache();

app.UseAuthorization(); 

app.MapControllers();

app.Run();