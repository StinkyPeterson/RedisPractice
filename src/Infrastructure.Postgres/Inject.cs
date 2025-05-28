using Application.Infrastructure.Contracts.Contracts;
using Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Postgres;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IPersonRepository, PersonRepository>();
    }
}