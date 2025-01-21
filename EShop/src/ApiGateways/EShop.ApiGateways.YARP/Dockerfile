# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateways/EShop.ApiGateways.YARP/EShop.ApiGateways.YARP.csproj", "src/ApiGateways/EShop.ApiGateways.YARP/"]
RUN dotnet restore "./src/ApiGateways/EShop.ApiGateways.YARP/EShop.ApiGateways.YARP.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/EShop.ApiGateways.YARP"
RUN dotnet build "./EShop.ApiGateways.YARP.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShop.ApiGateways.YARP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.ApiGateways.YARP.dll"]