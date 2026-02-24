public class EcoDriving
{
    public long TelemetryId { get; set; }

    public int? EcoScore { get; set; }

    public int? GreenDrivingType { get; set; }

    public long? EcoDurationInMS { get; set; }

    public Telemetry Telemetry { get; set; }
}