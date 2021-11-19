using Dapr.Workshop.DomainObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/sensor-data", async (context) =>
{
    var measurement = await context.Request.ReadFromJsonAsync<Measurement>();

    Console.WriteLine($"received sensor: {measurement?.SensorId} timestamp: {measurement?.Timestamp}");
})
.WithTopic("pubsub", "sensor-data");

app.MapGet("/ping", () => "pong");

await app.RunAsync();
