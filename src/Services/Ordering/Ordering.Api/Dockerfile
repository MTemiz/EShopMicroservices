FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
#HTTP PORT
EXPOSE 8000

#HTTPS PORT
EXPOSE 8001

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/Ordering.Api/Ordering.Api.csproj", "Services/Ordering/Ordering.Api/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Ordering.Application/"]
COPY ["Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Ordering.Infrastructure/"]
COPY ["Services/Ordering/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Ordering.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
COPY ["BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "BuildingBlocks/BuildingBlocks.Messaging/"]

RUN dotnet restore "Services/Ordering/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ordering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]
