global using static __APP_NAME__.Constants;

using Gio;
using GObject;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;
using __APP_NAME__.UI;

namespace __APP_NAME__;

public class HostedApplication(
    Adw.Application app,
    IHostApplicationLifetime lifetime,
    ILogger<HostedApplication> logger,
    IServiceProvider serviceProvider
) : IHostedService
{
    private MainWindow? _mainWindow;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Load GTK resources
        LoadResources();

        // Register application event handlers
        app.OnActivate += OnActivate;
        app.OnStartup += OnStartup;

        // Run GTK in a separate thread
        Task.Run(() =>
        {
            try
            {
                app.RunWithSynchronizationContext([]);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GTK application failed");
                lifetime.StopApplication();
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Loads the necessary resources for the application.
    /// This function attempts to load resources from different locations based on the environment.
    /// If the application is running as a Flatpak, it loads resources from the Flatpak environment.
    /// Otherwise, it tries to load the resources from the program directory or standard system paths.
    /// </summary>
    private static void LoadResources()
    {
        var resourcePath = Environment.GetEnvironmentVariable("FLATPAK_ID") != null
            ? RESOURCES_PATH
            : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $"{APP_ID}.gresource"));

        if (!File.Exists(resourcePath)) return;

        var resource = Resource.Load(resourcePath);
        resource.Register();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping GTK application...");

        app.Quit();

        lifetime.StopApplication();

        logger.LogInformation("GTK application stopped");
        return Task.CompletedTask;
    }

    private void OnActivate(Gio.Application application, EventArgs eventArgs)
    {
        // Create a new scope for the MainWindow
        using var scope = serviceProvider.CreateScope();

        // Resolve MainWindow from the scope
        _mainWindow ??= scope.ServiceProvider.GetRequiredService<MainWindow>();

        // Handle window events
        _mainWindow.OnDestroy += (s, e) => logger.LogDebug("Closing main window...");

        _mainWindow.Present();
    }

    private void OnStartup(Gio.Application application, EventArgs eventArgs)
    {
        CreateAction("Quit", (_, _) => { StopAsync(CancellationToken.None); }, ["<Ctrl>Q"]);
        CreateAction("About", (_, _) => { OnAboutAction(); });
        CreateAction("Preferences", (_, _) => { OnPreferencesAction(); }, ["<Ctrl>comma"]);
    }

    private void OnAboutAction()
    {
        var about = Adw.AboutDialog.New();
        about.ApplicationName = "__DISPLAY_NAME__";
        about.ApplicationIcon = "__APP_ID__";
        about.DeveloperName = "__DEVELOPER_NAME__";
        about.Version = "0.1.0";
        about.Developers = ["__DEVELOPER_NAME__"];
        about.Copyright = "© __YEAR__ __DEVELOPER_NAME__";
        about.Present(_mainWindow);
    }

    private void OnPreferencesAction()
    {
        logger.LogInformation("app.preferences action activated");
    }

    private void CreateAction(string name, SignalHandler<SimpleAction, SimpleAction.ActivateSignalArgs> callback,
        string[]? shortcuts = null)
    {
        var lowerName = name.ToLowerInvariant();
        var actionItem = SimpleAction.New(lowerName, null);
        actionItem.OnActivate += callback;
        app.AddAction(actionItem);

        if (shortcuts is { Length: > 0 })
        {
            app.SetAccelsForAction($"app.{lowerName}", shortcuts);
        }
    }
}