# __DISPLAY_NAME__

GTK application using [Gir.Core](https://github.com/gircore/gir.core) prepared for use
with [Blueprint](https://jwestman.pages.gitlab.gnome.org/blueprint-compiler/index.html) and packaged using
[Flatpak](https://flatpak.org/)

## Hacking

To run this project, run the following commands:

```bash
dotnet run
```

### Project Structure

The file structure of this project is as follows:

- `ui/`: Contains the UI files for the project. Put any classes that show UI here.
  - `MainWindow.cs`: The main window for the application. This is shown when the application starts.
- `data/`: Contains the data files for the project. These files are copied to the flatpak build directory. Put any files
  used in the flatpak manifest here.
  - `icons/`: Contains the icons for the project.
  - `ui/`: Contains the Blueprints for the project. Is scanned by the build system for Blueprints.
  - `MainWindow.blp`: The blueprint for the main window. This is used inside the `MainWindow.cs` file.
  - `__APP_ID__.desktop`: The desktop file for the flatpak. This is used to define the application id, name, icon,
    etc. and is used to create the application menu entry.
- `build-aux/`: Contains auxiliary files for the project.
    - `__APP_ID__.json`: The flatpak manifest. This is used to define the flatpak build. You should set permissions,
      dependencies, etc. here.
- `Program.cs`: The entry point for the project. This is where the application starts.
- `Constants.cs`: Contains the constants for the project. This is where things like the application id are defined.
- `build/`: Contains the compiled Blueprints. All ui files here are added as embedded resource to the project.

### Note when creating Blueprints

Make sure that the Blueprints are stored in the `ui` directory, and end with `.blp`. Files that do not end with `.blp`
are ignored by the build system. For example, `MainWindow.blp` is a valid blueprint, but `MainWindow.foo` is not.
