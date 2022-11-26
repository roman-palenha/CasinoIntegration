using AutoMapper;
using CasinoIntegration.API.Extensions;
using CasinoIntegration.BusinessLayer;
using CasinoIntegration.BusinessLayer.Logger;
using CasinoIntegration.BusinessLayer.Logger.Interfaces;
using CasinoIntegration.BusinessLayer.Services;
using CasinoIntegration.BusinessLayer.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.DatabaseSettings.Interfaces;
using CasinoIntegration.DataAccessLayer.Repositories;
using CasinoIntegration.DataAccessLayer.Repositories.Interfaces;
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
    mc.AddProfile(new CasinoIntegrationMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSwaggerDocumentation();
});

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
