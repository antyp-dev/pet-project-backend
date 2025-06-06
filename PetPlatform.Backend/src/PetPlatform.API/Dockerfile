FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetPlatform.API/PetPlatform.API.csproj", "src/PetPlatform.API/"]
COPY ["src/PetPlatform.Application/PetPlatform.Application.csproj", "src/PetPlatform.Application/"]
COPY ["src/PetPlatform.Domain/PetPlatform.Domain.csproj", "src/PetPlatform.Domain/"]
COPY ["src/PetPlatform.Infrastructure/PetPlatform.Infrastructure.csproj", "src/PetPlatform.Infrastructure/"]
RUN dotnet restore "src/PetPlatform.API/PetPlatform.API.csproj"
COPY . .
WORKDIR "/src/src/PetPlatform.API"
RUN dotnet build "./PetPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PetPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetPlatform.API.dll"]
