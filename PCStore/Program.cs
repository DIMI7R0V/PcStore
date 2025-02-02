using FluentValidation.AspNetCore;
using FluentValidation;
using Mapster;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using PcStoreStore.MappsterConfig;
using PCStore.Validation;
using PCStore.HealthChecks;
using PcStore.ServiceExtentions;
using PcStoreBL;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
    .RegisterDataLayer()
    .RegisterBusinessLayer();

MappsterConfig.Configure();
builder.Services.AddMapster();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ManufacturerValidator>();

builder.Services.AddHealthChecks()
.AddCheck<CustomHealthCheck>("Sample");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/Sample");

app.MapControllers();

app.Run();
