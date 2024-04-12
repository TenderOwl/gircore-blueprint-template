# A Template for making GTK applications using gircore

This template is a starting point for making GTK applications using gircore,
and [blueprint](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/index.html).

## Installing

- Install the latest version of the [dotnet sdk](https://dotnet.microsoft.com/en-us/download)
- Install the [blueprint compiler](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/index.html).
- Install this template using `dotnet new install DitherWither.GirCoreTemplates`


## Steps for using

Create a new project using:

```bash
dotnet new gircore-blueprint --app-id com.example.MyApp --display-name "My app"
```

replacing `com.example.MyApp` with an app id follows the reverse domain name format.

See [the documentation for app id on gnome docs](https://developer.gnome.org/documentation/tutorials/application-id.html). Also replace `My app` with the name of your application.



# TODO for this template

- [ ] Add icons and appdata files to the template.
