using AutoMapper;
using CasinoIntegration.API;
using CasinoIntegration.BusinessLayer;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Services;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Logger;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Logger.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings.Interfaces;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Repositories;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.Configure<CasinoIntegrationDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CasinoIntegrationDatabaseSettings)));

builder.Services.AddSingleton<ICasinoIntegrationDatabaseSettings>(provider =>
    provider.GetRequiredService<IOptions<CasinoIntegrationDatabaseSettings>>().Value);

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IMachineService, MachineService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Mappers());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureCustomExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
