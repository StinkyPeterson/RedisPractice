using Application.Contracts.Contracts;
using Infrastructure.Redis.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Cache.Services;

public static class Inject
{
    public static IServiceCollection AddRedisCache(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ICacheService, RedisService>();
    }
}