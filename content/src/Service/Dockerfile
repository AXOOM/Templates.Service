FROM mcr.microsoft.com/dotnet/core/runtime:3.1.14
ARG source
WORKDIR /app
EXPOSE 5000
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "MyVendor.MyService.dll"]
