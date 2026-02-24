using System;
using System.Collections.Generic;

public class CanData
{
    public long TelemetryId { get; set; }

    public int? Speed { get; set; }

    public int? EngineRpm { get; set; }

    public int? EngineTemperature { get; set; }

    public long? TotalMileageMetres { get; set; }

    public Telemetry Telemetry { get; set; }
}