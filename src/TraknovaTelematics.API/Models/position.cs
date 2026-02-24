using System;
using System.Collections.Generic;

public class Position
{
    public long TelemetryId { get; set; }

    public double? Altitude { get; set; }

    public int? Angle { get; set; }

    public int? Satellites { get; set; }

    public int? GpsSpeed { get; set; }

    public Telemetry Telemetry { get; set; }
}