# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy csproj first for better layer caching
COPY ["ConcurrentJobProcessor.csproj", "./"]

RUN dotnet restore "ConcurrentJobProcessor.csproj"

# Copy everything else
COPY . .

RUN dotnet build "ConcurrentJobProcessor.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

# Publish stage
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "ConcurrentJobProcessor.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# Final stage
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ConcurrentJobProcessor.dll"]