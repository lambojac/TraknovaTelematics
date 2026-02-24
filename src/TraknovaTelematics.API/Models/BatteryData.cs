using System;
using System.Collections.Generic;

public class BatteryData
{
    public long TelemetryId { get; set; }

    public int? BatteryLevel { get; set; }

    public double? BatteryVoltage { get; set; }

    public double? BatteryCurrent { get; set; }

    public Telemetry Telemetry { get; set; }
}