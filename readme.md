# axoom-service Template

This repository contains a dotnet-new template for AXOOM Services using .NET Core and Docker.

Run `build.ps1` to package the template as a NuGet package.
This script takes a version number as an input argument. The source code itself contains no version numbers. Instead version numbers should be determined at build time using [GitVersion](gitversion.readthedocs.io).

To install the template run `dotnet new --install axoom-service:*`.
To use the template run `dotnet new axoom-service`.
