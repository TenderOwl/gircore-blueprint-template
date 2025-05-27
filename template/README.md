# **__DISPLAY_NAME__**

This readme was generated automatically by the template generator.

## Build

To run this project, run the following commands:

```bash
dotnet run
```

To generate a flatpak, run the following commands:

```bash
chmod +x build_flatpak.sh
./build_flatpak.sh
```

The flatpak will be stored in a local repository inside `flatpak_build/repo`.

## Project Structure

The file structure of this project is as follows:

- `blueprints/`: Contains the blueprints for the project. Is scanned by the build system for blueprints.
  - `MainWindow.blp`: The blueprint for the main window. This is used inside the `MainWindow.cs` file.
- `ui/`: Contains the UI files for the project. Put any classes that show UI here.
  - `MainWindow.cs`: The main window for the application. This is shown when the application starts.
- `data/`: Contains the data files for the project. These files are copied to the flatpak build directory. Put any files used in the flatpak manifest here.
  - `__APP_ID__.desktop`: The desktop file for the flatpak. This is used to define the application id, name, icon, etc. and is used to create the application menu entry.
  - `__APP_ID__.yml`: The flatpak manifest. This is used to define the flatpak build. You should set permissions, dependencies, etc. here.
  - `run.sh`: The script that runs the application. This is used to set the environment variables for the application.
- `Program.cs`: The entry point for the project. This is where the application starts.
- `Constants.cs`: Contains the constants for the project. This is where things like the application id are defined.
- `build_flatpak.sh`: The script that builds the flatpak. Run this to build the flatpak.
- `build/`: Contains the compiled bluprints. All ui files here are added as a embedded resource to the project.

## Note when creating blueprints

Make sure that the blueprints are stored in the `blueprints` directory, and end with `.blp`. Files that do not end with `.blp` are ignored by the build system. For example, `MainWindow.blp` is a valid blueprint, but `MainWindow.foo` is not.
