FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install prerequisites
USER root
RUN apt-get update && apt-get install -y curl
CMD /bin/bash

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Sail-MockApi.Api/Sail-MockApi.Api.csproj", "Sail-MockApi.Api/"]
RUN dotnet restore "Sail-MockApi.Api/Sail-MockApi.Api.csproj"
COPY . .
WORKDIR "/src/Sail-MockApi.Api"
RUN dotnet build "Sail-MockApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Sail-MockApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sail-MockApi.Api.dll"]
