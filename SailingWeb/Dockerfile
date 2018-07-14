FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY SailingWeb/*.csproj ./SailingWeb/
RUN dotnet restore SailingWeb/SailingWeb.csproj

# copy everything else and build app
COPY . .
WORKDIR /app/SailingWeb
RUN dotnet build -r linux-arm SailingWeb.csproj


FROM build AS publish
WORKDIR /app/SailingWeb
RUN dotnet publish -r linux-arm SailingWeb.csproj -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/SailingWeb/out ./
ENTRYPOINT ["dotnet", "SailingWeb.dll"]