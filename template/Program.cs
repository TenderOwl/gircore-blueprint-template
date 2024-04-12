// This will make all the constants available in the global namespace, 
// so you can use them without the Constants prefix.
global using static __APP_NAME__.Constants;
using Microsoft.Extensions.Logging;

namespace __APP_NAME__;

public class Program
{
    public static void Main(string[] args)
    {
        
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger<Application> logger = factory.CreateLogger<Application>();
        
        var app = new Application(logger);
        app.Run(args);
    }
}