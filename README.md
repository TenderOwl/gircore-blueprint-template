# A Template for making GTK applications using Gir.Core

This template is a starting point for making GTK applications using [Gir.Core](https://github.com/gircore/gir.core),
and [Blueprint](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/index.html).

## Installing

- Install the latest version of the [dotnet sdk](https://dotnet.microsoft.com/en-us/download)
- Install the [blueprint compiler](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/index.html).
- Install this template using `dotnet new install TenderOwl.GirCoreTemplate.CSharp`

## Steps for using

Create a new project using:

```bash
dotnet new gnome-gircore \
  --app-id com.example.MyApp \
  --display-name "My app" \
  --developer-name "Tender Owl" \
  -o MyApp
```

## Run with GNOME Builder

- Open folder in Builder
- Press `Shift+Ctrl+Space` or `Run Project` button
