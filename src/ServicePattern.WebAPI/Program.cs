using Serilog;
using ServicePattern.Application;
using ServicePattern.Infrastructure;
using ServicePattern.WebAPI.Caching.Extensions;
using ServicePattern.WebAPI.Endpoints.Extensions;
using ServicePattern.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddOutputCache(options =>
{
    options.ConfigureCustomPolicies();
});

builder.Services.AddEndpoints();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Host.UseSerilog((_, config) => { config.ReadFrom.Configuration(configuration); });

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

app.UseEndpoints();

app.Run();