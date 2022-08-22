FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SpiderWeb.LicensingApi.csproj", "."]
RUN dotnet restore "./SpiderWeb.LicensingApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SpiderWeb.LicensingApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpiderWeb.LicensingApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpiderWeb.LicensingApi.dll"]
