# AXOOM Service Template

[![NuGet package](https://img.shields.io/nuget/v/Axoom.Templates.Service.svg)](https://www.nuget.org/packages/Axoom.Templates.Service/)
[![Build status](https://img.shields.io/appveyor/ci/AXOOM/templates-service.svg)](https://ci.appveyor.com/project/AXOOM/templates-service)

This template helps you create services for the [AXOOM](http://www.axoom.com/) Platform. In addition to its role as a template this repository also serves as a reference implementation and living documentation for infrastructure concerns such as configuration, logging and monitoring.

The template creates a C# .NET Core project packaged to run in a [Docker](https://www.docker.com/) container.

This template focuses on creating services without RESTful APIs. If this is not your goal one of our other templates may be a better match:
- [AXOOM Library Template](https://github.com/AXOOM/Templates.Library)
- [AXOOM WebService Template](https://github.com/AXOOM/Templates.WebService)
- [AXOOM Portal App Template](https://github.com/AXOOM/Templates.PortalApp)


# Using the template

Download and install the [.NET Core SDK](https://www.microsoft.com/net/download) on your machine. You can then install the template by running the following:

    dotnet new --install Axoom.Templates.Service::*

To use the template to create a new project:

    dotnet new axoom-service --name "MyVendor.MyService" --serviceName myvendor-myservice --team "myteam" --friendlyName "My Service" --description "my description"
    cd MyVendor.MyService
    git init
    git update-index --add --chmod=+x build.sh

In the commands above replace
- `MyVendor.MyService` with the .NET namespace you wish to use,
- `myvendor-myservice` with the name of your company and service using only lowercase letters and hyphens,
- `myteam` with the name of your team within the company using only lowercase letters and hyphens,
- `My Service` with the full name of your service and
- `my description` with a brief (single sentence) description the service.

You can now open the generated project using your favorite IDE. We recommend [Visual Studio](https://www.visualstudio.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/Download) or [Rider](https://www.jetbrains.com/rider/).


# Walkthrough

The following is a detailed walkthrough of the project structure generated by the template. To follow along you can either use the output of `dotnet new` or this repository.

The [`content/`](content/) directory in this repository contains the payload that is instantiated by the template. The file [`content/.template.config/template.json`](content/.template.config/template.json) instructs the templating engine which placeholders to replace. The following descriptions all refer to files and directories below this directory.

## Formatting and encoding

The [`.gitignore`](content/.gitignore) file prevents build artifacts, per-user settings and IDE-specific temp files from being checked into version control.

The [`.gitattributes`](content/.gitattributes) file disable's Git's automatic End of Line (EOL) conversion between Windows and Linux. This ensures that files are binary-identical on all platforms.

We use [EditorConfig](http://editorconfig.org/) to define and maintain consistent coding styles between different editors and IDEs. Our [`.editorconfig`](content/.editorconfig) file also ensures a consistent EOL style and charset encoding based on file type.

## Versioning

We decided to use [GitVersion](http://gitversion.readthedocs.io/) to extract version information from our git commits. 
Hence, we make use of git tags to create a release whereas all other commits are pre-releases by default. The source code itself contains no version numbers.
The [`GitVersion.yml`](content/GitVersion.yml) file configures GitVersion to extract a version number like the following from an untagged commit:
```
0.1.1-pre0045-20180404094115
```
| Version Part     | Description                                                                                               |
| ---------------- | --------------------------------------------------------------------------------------------------------- |
| `0.1.1`          | The latest tag with a bumped patch version                                                                |
| `pre0045`        | Indicates, that it's a prerelease (of `0.1.1`) and 45 commits have been made since the last tag (`0.1.0`) |
| `20180404094115` | The Timestamp _04/04/2018 09:41:15_                                                                       |

Whereas `0.1.1` is extracted from a commit with tag `0.1.1`.

## Project structure

[`src/`](content/src/) contains the C# project structure.

- [`Service`](content/src/Service/): The service project.
- [`UnitTests`](content/src/UnitTests/): Unit tests for the service.

## Building

TODO: Build scripts

[`release/`](content/release/)

[`deploy/`](content/deploy/)

## Configuration

After a `git clone` an asset should be pre-configured and ready to use with defaults for local development and debugging including any dependencies.

When packaged as a release however, assets should ship with defaults ready for production. Only business parameters should be set at deployment-time. Avoid exposing connection strings and other technical details here if possible.

Assets contain some configuration bundled at compile-time. This can be overriden at run-time using environment variables.

The [`appsettings.yml`](content/src/Service/appsettings.yml) file is bundled into the Docker Image during build and sets development defaults.
We decided to use YAML files for configuration purposes.
JSON is for sure a great format for serialization and is as well human-readable, but YAML is furthermore human-writable and supports comments, what you actually want from a configuration format.

The [`docker-compose.override.yml`](content/src/docker-compose.override.yml) file is used to set dummy values and wire up dependencies for local development.
proper volumes optional

The [`asset.yml`](content/release/asset.yml) file is used to generate releases, sets production defaults and maps external environment variables to service-specific environment variables.
proper volumes mandatory
Only business parameters should be set at deployment-time (avoid exposing connection strings, etc.)
