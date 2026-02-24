namespace FleetTracking.Models
{
    public class CrashEvent
    {
        public long Id { get; set; }

        public long TelemetryRecordId { get; set; }

        public double ImpactMagnitude { get; set; }

        public string Axis { get; set; }

        public TelemetryRecord TelemetryRecord { get; set; }
    }
}