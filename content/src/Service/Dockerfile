FROM mcr.microsoft.com/dotnet/core/runtime:2.2.8
ARG source
WORKDIR /app
EXPOSE 5000
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "MyVendor.MyService.dll"]
