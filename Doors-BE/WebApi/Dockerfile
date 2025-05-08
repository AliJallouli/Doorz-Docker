FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore "Doors-BE.sln"
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY WebApi/certs/localhost.pfx /app/cert.pfx
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=https://+:7200
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "WebApi.dll"]
