namespace Dapr.Workshop.DomainObjects;

public record Measurement
{
    public string SensorId { get; init; } = null!;

    public decimal Temperature { get; init; }

    public string Format { get; init; } = null!;

    public string Timestamp { get; init; } = null!;

}
