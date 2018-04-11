# My Service

This repository contains the source code for My Service.

## Development

Run `build.ps1` to compile the source code and package the result in Docker images.
This script takes a version number as an input argument. The source code itself contains no version numbers. Instead version numbers should be determined at build time using [GitVersion](http://gitversion.readthedocs.io/).
The `release` directory contains an [ax Asset Descriptor](https://tfs.inside-axoom.org/tfs/axoom/axoom/_git/Axoom.Provisioning?_a=readme&fullScreen=true) for building releases for production Docker environments.

To build and run locally with Docker Compose:

    cd src
    ./build-dotnet.ps1
    docker-compose up --build

To build and deploy locally with AX (requires [Infrastructure Stack](https://tfs.inside-axoom.org/tfs/axoom/axoom/_git/Axoom.Platform.Stacks.Infrastructure)):

    ./build.ps1 -DeployLocal

## Deploying

### Feed URI

http://assets.axoom.cloud/services/myservice.xml

### External environment

| Name | Default | Description |
| ---- | ------- | ----------- |
|      |         |             |
