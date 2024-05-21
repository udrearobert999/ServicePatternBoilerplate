FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ServicePattern.WebAPI/ServicePattern.WebAPI.csproj", "src/ServicePattern.WebAPI/"]
COPY ["src/ServicePattern.Infrastructure/ServicePattern.Infrastructure.csproj", "src/ServicePattern.Infrastructure/"]
COPY ["src/ServicePattern.Application/ServicePattern.Application.csproj", "src/ServicePattern.Application/"]
COPY ["src/ServicePattern.Domain/ServicePattern.Domain.csproj", "src/ServicePattern.Domain/"]
COPY ["src/ServicePattern.Presentation/ServicePattern.Presentation.csproj", "src/ServicePattern.Presentation/"]
RUN dotnet restore "./src/ServicePattern.WebAPI/ServicePattern.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/ServicePattern.WebAPI"
RUN dotnet build "./ServicePattern.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServicePattern.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicePattern.WebAPI.dll"]