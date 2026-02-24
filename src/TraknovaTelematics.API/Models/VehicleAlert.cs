using System;
using System.Collections.Generic;

namespace FleetTracking.Models
{
    public class VehicleAlert
    {
        public long Id { get; set; }

        public long TelemetryRecordId { get; set; }

        public bool? TowingAlert { get; set; }

        public bool? IdlingAlert { get; set; }

        public bool? OverSpeedingAlert { get; set; }

        public bool? UnplugAlert { get; set; }

        public TelemetryRecord TelemetryRecord { get; set; }
    }
}