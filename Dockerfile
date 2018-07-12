FROM microsoft/dotnet:2.1-runtime-deps-stretch-slim-arm32v7 as base
WORKDIR /app
EXPOSE 80


FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY SailingWeb.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish SailingWeb.csproj -c Release -r linux-arm -o out

# Build runtime image
FROM microsoft/dotnet:2.1-runtime-deps-stretch-slim-arm32v7
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SailingWeb.dll"]