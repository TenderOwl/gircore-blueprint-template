global using static __APP_NAME__.Constants;

using Gio;
using GObject;
using Microsoft.Extensions.Logging;
using __APP_NAME__.UI;

namespace __APP_NAME__;

public class Application
{
    private readonly ILogger<Application> _logger;
    private readonly Adw.Application _app;
    private MainWindow? _mainWindow;
    
    public Application(ILogger<Application> logger)
    {
        _logger = logger;
        _app = Adw.Application.New(APP_ID, ApplicationFlags.DefaultFlags);
        _app.OnStartup += OnStartup;
        _app.OnActivate += OnActivate;
    }

    public void Run(string[] args)
    {
        // Run the application
        _app.RunWithSynchronizationContext(args);
    }

    private void OnActivate(Gio.Application application, EventArgs eventArgs)
    {
        // Create a new MainWindow and show it.
        // The application is passed to the MainWindow so that it can be used
        _mainWindow ??= new MainWindow((Adw.Application)application);
        _mainWindow.Present();
    }

    private void OnStartup(Gio.Application application, EventArgs eventArgs)
    {
        CreateAction("Quit", (_, _) => { _app.Quit(); }, ["<Ctrl>Q"]);
        CreateAction("About", (_, _) => { OnAboutAction(); });
        CreateAction("Preferences", (_, _) => { OnAboutAction(); }, ["<Ctrl>comma"]);
    }
    
    private void OnAboutAction()
    {
        var about = Adw.AboutWindow.New();
        about.TransientFor = _app.ActiveWindow;
        about.ApplicationName = "__DISPLAY_NAME__";
        about.ApplicationIcon = "__APP_ID__";
        about.DeveloperName = "__DEVELOPER_NAME__";
        about.Version = "0.1.0";
        about.Developers = ["__DEVELOPER_NAME__"];
        about.Copyright = "© 2024 __DEVELOPER_NAME__";
        about.Present();
    }

    private void OnPreferencesAction() {
        _logger.LogInformation("app.preferences action activated");
    }

    private void CreateAction(string name, SignalHandler<SimpleAction, SimpleAction.ActivateSignalArgs> callback,
        string[]? shortcuts = null)
    {
        var lowerName = name.ToLowerInvariant();
        var actionItem = SimpleAction.New(lowerName, null);
        actionItem.OnActivate += callback;
        _app.AddAction(actionItem);
        
        if (shortcuts is { Length: > 0 })
        {
            _app.SetAccelsForAction($"app.{lowerName}", shortcuts);
        }
    }
}