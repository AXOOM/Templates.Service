FROM microsoft/dotnet:2.0.5-runtime
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Axoom.MyService.dll"]
