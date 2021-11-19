# dapr-workshop-01

This repository includes demo material for DAPR Workshop 01.

## Scope:
- DotNet SDK
- PubSub somponent
- How use PubSub Component based on namepsace

## Prerequisites
- dotnet 6
- docker
- dapr cli
- type cli

## Get Started
- dapr run --app-id weathercube_station --dapr-http-port 5002 --app-port 4002 -- dotnet run --components-path "$PWD/../../dapr-components"
- dapr run --app-id weathercube_sensor --dapr-http-port 5001 --app-port 4001-- dotnet run --components-path "$PWD/../../dapr-components"