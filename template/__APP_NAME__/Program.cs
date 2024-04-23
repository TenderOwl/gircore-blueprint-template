// This will make all the constants available in the global namespace, 
// so you can use them without the Constants prefix.
global using static __APP_NAME__.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace __APP_NAME__;

public class Program
{
    public static void Main(string[] args)
    {
        // Required to load DLLs properly
        Gio.Module.Initialize();

        var resource = Gio.Resource.Load(RESOURCES_PATH);
        resource.Register();

        var services = CreateServices();
        
        var app = services.GetRequiredService<Application>();
        app.Run(args);
    }
    
    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            })
            .AddSingleton<Application>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}