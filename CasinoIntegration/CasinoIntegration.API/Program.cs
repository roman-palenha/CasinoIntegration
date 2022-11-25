using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoInegration.Services;
using Microsoft.Extensions.Options;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings.Interfaces;
using AutoMapper;
using CasinoIntegration.BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CasinoIntegrationDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CasinoIntegrationDatabaseSettings)));

builder.Services.AddSingleton<ICasinoIntegrationDatabaseSettings>(provider =>
    provider.GetRequiredService<IOptions<CasinoIntegrationDatabaseSettings>>().Value);

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IMachineService, MachineService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Mappers());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
