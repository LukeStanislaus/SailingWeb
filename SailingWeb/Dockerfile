FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SailingWeb/SailingWeb.csproj SailingWeb/
RUN dotnet restore SailingWeb/SailingWeb.csproj
COPY . .
WORKDIR /src/SailingWeb
RUN dotnet build SailingWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SailingWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SailingWeb.dll"]
