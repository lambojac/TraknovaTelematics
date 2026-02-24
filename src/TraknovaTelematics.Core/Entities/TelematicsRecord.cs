namespace TraknovaTelematics.Core.Entities;

/// <summary>
/// Represents one telematics snapshot received from a vehicle device.
/// Optional fields reflect real-world payloads: not all devices report all signals.
/// </summary>
public class TelematicsRecord
{
    public long Id { get; set; }                        
    public int VehicleId { get; set; }
    public DateTime TimeStamp { get; set; }
    public DateTime IngestedAt { get; set; } = DateTime.UtcNow;
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public double? Altitude { get; set; }
    public int? Angle { get; set; }            
    public int? Satellites { get; set; }
    public double? GpsSpeed { get; set; }     
    public double? CanSpeed { get; set; }
    public int? CanEngineRpm { get; set; }
    public double? EngineTemperature { get; set; }
    public long? CanTotalMileageMetres { get; set; }
    public double? CanFuelLevelLitres { get; set; }
    public double? CanFuelPercentage { get; set; }
    public int? ObdEngineRpm { get; set; }
    public double? OBDOEMTotalMileageKM { get; set; }
    public double? ObdOemFuelLevelLitres { get; set; }
    public double? ObdFuelLevelPercent { get; set; }
    public long? TripOdemeter { get; set; }
    public long? TotalOdemeterMetres { get; set; }
    public double? BatteryLevel { get; set; }
    public double? BatteryVoltage { get; set; }
    public double? BatteryCurrent { get; set; }
    public int? GsmSignal { get; set; }
    public int? NetworkType { get; set; }
    public int? DataMode { get; set; }
    public bool? IgnitionActive { get; set; }
    public bool? IsMoving { get; set; }
    public bool? DigitalOutput1 { get; set; }
    public bool? DigitalOutput2 { get; set; }
    public bool? DigitalOutput3 { get; set; }
    public bool? OilIndicatorActive { get; set; }
    public bool? TowingAlert { get; set; }
    public bool? IdlingAlert { get; set; }
    public bool? OverSpeedingAlert { get; set; }
    public bool? UnplugAlert { get; set; }
    public int? EcoScore { get; set; }
    public int? GreenDrivingType { get; set; }
    public long? EcoDurationInMS { get; set; }
    public CrashDetectionRecord? CrashDetection { get; set; }
    public Guid? TripId { get; set; }
}
