FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and project files
COPY RecipeShare.sln ./
COPY RecipeShare.Api/RecipeShare.Api.csproj RecipeShare.Api/
COPY RecipeShare.Library/RecipeShare.Library.csproj RecipeShare.Library/

# Restore dependencies
RUN dotnet restore "RecipeShare.sln"

# Copy the rest of the source code
COPY RecipeShare.Api/ RecipeShare.Api/
COPY RecipeShare.Library/ RecipeShare.Library/
WORKDIR /src/RecipeShare.Api

# Build the API project
RUN dotnet build "RecipeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/RecipeShare.Api
RUN dotnet publish "RecipeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecipeShare.Api.dll"]
