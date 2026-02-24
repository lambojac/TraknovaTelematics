using System;
using System.Collections.Generic;

namespace FleetTracking.Models
{
    public class Trip
{
    public Guid Id { get; set; }

    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    public ICollection<Telemetry> Telemetries { get; set; }
}
}