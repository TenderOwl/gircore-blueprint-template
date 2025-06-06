// This will make all the constants available in the global namespace, 
// so you can use them without the Constants prefix.

global using static __APP_NAME__.Constants;
using Gio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using __APP_NAME__;
using __APP_NAME__.UI;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Регистрируем GTK Application
        services.AddLogging();
        services.AddSingleton<Adw.Application>(s =>
        {
            var app = Adw.Application.New(APP_ID, ApplicationFlags.DefaultFlags);
            return app;
        });

        // Регистрируем главное окно
        services.AddTransient<MainWindow>();

        // Регистрируем кастомный сервис
        services.AddHostedService<HostedApplication>();
    })
    .UseConsoleLifetime(opts => opts.SuppressStatusMessages = true);

var host = builder.Build();

await host.RunAsync();