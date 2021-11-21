#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dapr.Workshop.WeatherCube.Station/Dapr.Workshop.WeatherCube.Station.csproj", "Dapr.Workshop.WeatherCube.Station/"]
COPY ["Dapr.Workshop.DomainObjects/Dapr.Workshop.DomainObjects.csproj", "Dapr.Workshop.DomainObjects/"]
RUN dotnet restore "Dapr.Workshop.WeatherCube.Station/Dapr.Workshop.WeatherCube.Station.csproj"
COPY . .
WORKDIR "/src/Dapr.Workshop.WeatherCube.Station"
RUN dotnet build "Dapr.Workshop.WeatherCube.Station.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dapr.Workshop.WeatherCube.Station.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dapr.Workshop.WeatherCube.Station.dll"]