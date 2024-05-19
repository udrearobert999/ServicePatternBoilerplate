FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServicePattern.WebAPI/ServicePattern.Domain.csproj", "ServicePattern.Domain/"]
COPY ["ServicePattern.WebAPI/ServicePattern.Application.csproj", "ServicePattern.Application/"]
COPY ["ServicePattern.WebAPI/ServicePattern.Persistance.csproj", "ServicePattern.Persistance/"]
COPY ["ServicePattern.WebAPI/ServicePattern.Infrastructure.csproj", "ServicePattern.Infrastructure/"]
COPY ["ServicePattern.WebAPI/ServicePattern.Presentation.csproj", "ServicePattern.Presentation/"]
COPY ["ServicePattern.WebAPI/ServicePattern.WebAPI.csproj", "ServicePattern.WebAPI/"]
RUN dotnet restore "./ServicePattern.WebAPI/ServicePattern.WebAPI.csproj"
COPY . .
WORKDIR "/src/ServicePattern.WebAPI"
RUN dotnet build "./ServicePattern.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServicePattern.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicePattern.WebAPI.dll"]