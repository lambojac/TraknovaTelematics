using System;
using System.Collections.Generic;

public class FuelData
{
    public long TelemetryId { get; set; }

    public double? FuelLevelLitres { get; set; }

    public double? FuelPercentage { get; set; }

    public Telemetry Telemetry { get; set; }
}