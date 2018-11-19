FROM microsoft/dotnet:2.1.6-runtime
ARG source
WORKDIR /app
EXPOSE 5000
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "MyVendor.MyService.dll"]
