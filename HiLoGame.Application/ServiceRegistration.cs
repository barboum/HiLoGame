using Microsoft.Extensions.DependencyInjection;

namespace HiLoGame.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        return services;
    }
}