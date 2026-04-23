# JUSToolkit: Installation

**JUS.CLI** is the main application of the project. It's a console application
(no graphical interface).

## Prerequisites

The utility requires the
[.NET 10.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).
You can verify the installation by running `dotnet --list-runtimes` from a
terminal. If it says `Microsoft.NETCore.App` 10.0 or higher is installed, it
should be fine.

## Installation

Download the latest stable version from the
[release page](https://github.com/priverop/JUSToolkit/releases) of the project.

Preview versions can be downloaded from the project
[_Actions pipeline artifacts_](https://github.com/priverop/JUSToolkit/actions/workflows/build-and-release.yml).
But they expire after a few days. You may want to follow the
[source code README file](https://github.com/priverop/JUSToolkit) to build the
latest version yourself.

## How to use

`./JUS.CLI jus [type] [feature] [args]`

- graphics
  - export-dtx
  - export-dig
  - import-dig
- containers
  - export-alar3
  - import-alar3
  - export-alar2
  - import-alar2
  - import
  - export
- batch
  - export-alar-png
  - import-png-alar3
- game
  - import

To get the arguments of a feature you can use:
`./JUS.CLI jus [type] [feature] -h`

Examples:
`./JUS.CLI jus containers export-alar3 --container test.aar --output myDirectory`
`./JUS.CLI jus containers export-alar3 -h`
