FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY SpiderWeb.FrontEnd.csproj .
RUN dotnet restore SpiderWeb.FrontEnd.csproj
COPY . .
RUN dotnet build SpiderWeb.FrontEnd.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SpiderWeb.FrontEnd.csproj -c Release -o /app/publish

# FROM nginx AS final
# COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
# COPY default.conf /etc/nginx/conf.d/default.conf