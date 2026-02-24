namespace TraknovaTelematics.Core.Entities;

/// <summary>
/// Crash event data embedded within a telematics record.
/// Stored as a dependent (owned) entity — same table via EF owned types.
/// </summary>
public class CrashDetectionRecord
{
    public double ImpactMagnitude { get; set; }
    public string? Axis { get; set; }
}
