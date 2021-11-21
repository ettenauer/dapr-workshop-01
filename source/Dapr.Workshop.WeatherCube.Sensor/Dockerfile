#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dapr.Workshop.WeatherCube.Sensor/Dapr.Workshop.WeatherCube.Sensor.csproj", "Dapr.Workshop.WeatherCube.Sensor/"]
COPY ["Dapr.Workshop.DomainObjects/Dapr.Workshop.DomainObjects.csproj", "Dapr.Workshop.DomainObjects/"]
RUN dotnet restore "Dapr.Workshop.WeatherCube.Sensor/Dapr.Workshop.WeatherCube.Sensor.csproj"
COPY . .
WORKDIR "/src/Dapr.Workshop.WeatherCube.Sensor"
RUN dotnet build "Dapr.Workshop.WeatherCube.Sensor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dapr.Workshop.WeatherCube.Sensor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dapr.Workshop.WeatherCube.Sensor.dll"]