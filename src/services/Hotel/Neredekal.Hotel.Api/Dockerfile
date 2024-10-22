# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/Hotel/Neredekal.Hotel.Api/Neredekal.Hotel.Api.csproj", "src/services/Hotel/Neredekal.Hotel.Api/"]
COPY ["src/services/Hotel/Neredekal.Hotel.Application/Neredekal.Hotel.Application.csproj", "src/services/Hotel/Neredekal.Hotel.Application/"]
COPY ["src/common/Neredekal.Common.Domain/Neredekal.Common.Domain.csproj", "src/common/Neredekal.Common.Domain/"]
COPY ["src/services/Hotel/Neredekal.Hotel.Domain/Neredekal.Hotel.Domain.csproj", "src/services/Hotel/Neredekal.Hotel.Domain/"]
COPY ["src/services/Hotel/Neredekal.Hotel.Insfrastructure/Neredekal.Hotel.Insfrastructure.csproj", "src/services/Hotel/Neredekal.Hotel.Insfrastructure/"]
RUN dotnet restore "./src/services/Hotel/Neredekal.Hotel.Api/Neredekal.Hotel.Api.csproj"
COPY . .
WORKDIR "/src/src/services/Hotel/Neredekal.Hotel.Api"
RUN dotnet build "./Neredekal.Hotel.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Neredekal.Hotel.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Neredekal.Hotel.Api.dll"]