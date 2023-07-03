using HiLoGame.Application;
using HiLoGame.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace HiLoGame.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Game>, InMemoryRepository<Game>>();
        return services;
    }
}