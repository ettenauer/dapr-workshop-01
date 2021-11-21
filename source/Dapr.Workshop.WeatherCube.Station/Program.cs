using Dapr.Workshop.DomainObjects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(configure => configure.AddConsole());
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapGet("/ping", () => "pong");

app.MapPost("/sensor-data", async (HttpContext context, ILogger<Program> logger) =>
{
    var measurement = await context.Request.ReadFromJsonAsync<Measurement>();

    logger.LogInformation($"received sensor: {measurement?.SensorId} timestamp: {measurement?.Timestamp}");
})
.WithTopic("pubsub", "sensor-data");

await app.RunAsync();
