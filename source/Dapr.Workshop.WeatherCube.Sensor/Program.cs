using Dapr.Client;
using Dapr.Workshop.DomainObjects;

Console.WriteLine($"Sensor {Environment.MachineName} is ready.");

using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
try
{
    do
    {
        using var client = new DaprClientBuilder()
            .UseHttpEndpoint("http://127.0.0.1:4001")
            .Build();

        await client.PublishEventAsync("pubsub",
            "sensor-data",
            new Measurement { SensorId = Environment.MachineName, Temperature = 10.4m, Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") },
            cancellationToken: cts.Token);

        await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);

    } while (!cts.IsCancellationRequested);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


Console.WriteLine($"Sensor {Environment.MachineName} is terminated.");
