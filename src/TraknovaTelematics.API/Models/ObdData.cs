using System;
using System.Collections.Generic;

public class ObdData
{
    public long TelemetryId { get; set; }

    public int? EngineRpm { get; set; }

    public int? TotalMileageKm { get; set; }

    public double? FuelLevelLitres { get; set; }

    public double? FuelPercent { get; set; }

    public Telemetry Telemetry { get; set; }
}