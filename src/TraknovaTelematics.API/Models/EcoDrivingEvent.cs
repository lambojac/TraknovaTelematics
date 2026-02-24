namespace FleetTracking.Models
{
    public class EcoDrivingEvent
    {
        public long Id { get; set; }

        public long TelemetryRecordId { get; set; }

        public int? GreenDrivingType { get; set; }

        public long? EcoDurationInMS { get; set; }

        public TelemetryRecord TelemetryRecord { get; set; }
    }
}