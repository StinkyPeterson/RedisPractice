using Application;
using Application.Cache.Services;
using Coravel;
using Host.BackgroundService.Job;
using Infrastructure.Postgres;
using Infrastructure.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>()["ConnectionString:PostgresSessionsDb"];
    if (string.IsNullOrEmpty(connectionString))
        throw new ArgumentNullException("ConnectionString:PostgresSessionsDb is not configured!");
    options.UseNpgsql(connectionString);
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp => 
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config["Redis:ConnectionString"];
    try 
    {
        return ConnectionMultiplexer.Connect(connectionString);
    }
    catch (Exception ex)
    {
        throw new Exception($"Failed to connect to Redis: {ex.Message}");
    }
});

builder.Services.AddInfrastructure();
builder.Services.AddRedisCache();
builder.Services.AddApplication();

builder.Services.AddScheduler();
builder.Services.AddTransient<PersonAutofiller>(); 

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<PersonAutofiller>()
        .EveryMinute();
});

app.Run();