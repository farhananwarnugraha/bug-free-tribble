# Stage 1: Base runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HydraAPI.csproj", "./"]
RUN dotnet restore "./HydraAPI.csproj"
#Copy all source code
COPY . . 
WORKDIR "/src"
RUN dotnet build "./HydraAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HydraAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY appsettings.json appsettings.json
COPY appsettings.Development.json appsettings.Development.json
ENTRYPOINT ["dotnet", "HydraAPI.dll"]
