using HiloGame.UI.Console;
using HiLoGame.Application;
using HiLoGame.Infrastructure;
using HiLoGame.UI;
using HiLoGame.UI.Console;
using Microsoft.Extensions.DependencyInjection;

try
{
    var serviceProvider = RegistryServices();
    var gameLauncherService = serviceProvider.GetRequiredService<GameLauncher>();
    gameLauncherService.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

ServiceProvider RegistryServices()
{
   return new ServiceCollection()
            .AddServices()
            .AddRepositories()
            .AddSingleton<IConsoleIO, ConsoleIO>()
            .AddScoped<GameLauncher>()
            .BuildServiceProvider();
}