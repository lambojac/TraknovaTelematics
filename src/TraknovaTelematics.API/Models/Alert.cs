public class Alert
{
    public long TelemetryId { get; set; }

    public bool? TowingAlert { get; set; }

    public bool? IdlingAlert { get; set; }

    public bool? OverSpeedingAlert { get; set; }

    public bool? UnplugAlert { get; set; }

    public Telemetry Telemetry { get; set; }
}