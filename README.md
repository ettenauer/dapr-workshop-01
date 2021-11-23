# dapr-workshop-01

This repository includes demo material for DAPR Workshop 01.

## Prerequisites
- install `dotnet 6` -> https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu
- install `docker` -> https://docs.docker.com/engine/install/ubuntu/
- install `dapr cli` -> https://docs.dapr.io/getting-started/install-dapr-cli/
- [Optional] `azure cli` -> https://docs.microsoft.com/en-us/cli/azure/install-azure-cli

## Get Started - Dapr Cli
- setup dapr: `dapr init -s`
- start brokers: `docker-compose --project-directory ./local-infrastructure up -d`
- choose redis broker: `export NAMESPACE=redis`
- choose kafka broker: `export NAMESPACE=kafka`
- run station: `dapr run --app-id weathercube_station --dapr-http-port 5002 --app-port 4002 --components-path "$PWD/../../dapr-cli" -- dotnet run`
- test station: `curl http://localhost:5002/v1.0/invoke/weather_station/method/ping -X GET`
- run sensor: `dapr run --app-id weathercube_sensor --dapr-http-port 5001 --app-port 4001 --components-path "$PWD/../../dapr-cli" -- dotnet run` 

## Get Started - Dapr Docker-Compose
- review dependencies: `./docker-compose/docker-compose.yaml`
- review dapr components: `./docker-compose/components`
- run: `docker-compose --project-directory ./docker-compose up -d`

## [Optional] Dapr Azure Container Apps - Feature Preview
- check out here  https://docs.microsoft.com/en-us/azure/container-apps/microservices-dapr