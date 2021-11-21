using Dapr.Client;
using Dapr.Workshop.DomainObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

const string ENDPOINT = "http://localhost:4001";

var serviceCollection = new ServiceCollection()
    .AddLogging(configure => configure.AddConsole());

var serviceProvider = serviceCollection.BuildServiceProvider();
var logger = serviceProvider
        .GetService<ILoggerFactory>()?
        .CreateLogger<Program>() ?? throw new ArgumentNullException(nameof(serviceProvider));

logger.LogInformation($"Sensor {Environment.MachineName} is ready. PubSub: {ENDPOINT}");

using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

try
{
    var random = new Random();
    using var client = new DaprClientBuilder()
    .UseHttpEndpoint(ENDPOINT)
    .Build();

    do
    {
        await Task.Delay(TimeSpan.FromSeconds(5), cts.Token);

        try
        {
            var measurement = new Measurement
            {
                SensorId = Environment.MachineName,
                Temperature = random.Next(-10, 40),
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await client.PublishEventAsync("pubsub", "sensor-data", measurement, cancellationToken: cts.Token);

            logger.LogInformation($"{measurement.SensorId}: send at {measurement.Timestamp}");
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "error during publishing");
        }

    } while (!cts.IsCancellationRequested);
}
catch (Exception ex)
{
    logger.LogError(ex, "error:");
}

logger.LogInformation($"Sensor {Environment.MachineName} is terminated.");
