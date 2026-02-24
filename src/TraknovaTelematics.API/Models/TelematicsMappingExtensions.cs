using TraknovaTelematics.API.DTOs;
using TraknovaTelematics.Core.Entities;

namespace TraknovaTelematics.API.Models;

public static class TelematicsMappingExtensions
{
    public static TelematicsRecord ToEntity(this TelematicsRecordDto dto) => new()
    {
        VehicleId = dto.VehicleId,
        TimeStamp = dto.TimeStamp,
        Longitude = dto.Longitude,
        Latitude = dto.Latitude,
        Altitude = dto.Altitude,
        Angle = dto.Angle,
        Satellites = dto.Satellites,
        GpsSpeed = dto.GpsSpeed,
        CanSpeed = dto.CanSpeed,
        CanEngineRpm = dto.CanEngineRpm,
        EngineTemperature = dto.EngineTemperature,
        CanTotalMileageMetres = dto.CanTotalMileageMetres,
        CanFuelLevelLitres = dto.CanFuelLevelLitres,
        CanFuelPercentage = dto.CanFuelPercentage,
        ObdEngineRpm = dto.ObdEngineRpm,
        OBDOEMTotalMileageKM = dto.OBDOEMTotalMileageKM,
        ObdOemFuelLevelLitres = dto.ObdOemFuelLevelLitres,
        ObdFuelLevelPercent = dto.ObdFuelLevelPercent,
        TripOdemeter = dto.TripOdemeter,
        TotalOdemeterMetres = dto.TotalOdemeterMetres,
        BatteryLevel = dto.BatteryLevel,
        BatteryVoltage = dto.BatteryVoltage,
        BatteryCurrent = dto.BatteryCurrent,
        GsmSignal = dto.GsmSignal,
        NetworkType = dto.NetworkType,
        DataMode = dto.DataMode,
        IgnitionActive = dto.IgnitionActive,
        IsMoving = dto.IsMoving,
        DigitalOutput1 = dto.DigitalOutput1,
        DigitalOutput2 = dto.DigitalOutput2,
        DigitalOutput3 = dto.DigitalOutput3,
        OilIndicatorActive = dto.OilIndicatorActive,
        TowingAlert = dto.TowingAlert,
        IdlingAlert = dto.IdlingAlert,
        OverSpeedingAlert = dto.OverSpeedingAlert,
        UnplugAlert = dto.UnplugAlert,
        EcoScore = dto.EcoScore,
        GreenDrivingType = dto.GreenDrivingType,
        EcoDurationInMS = dto.EcoDurationInMS,
        TripId = dto.TripId,
        CrashDetection = dto.CrashDetection is null ? null : new CrashDetectionRecord
        {
            ImpactMagnitude = dto.CrashDetection.ImpactMagnitude,
            Axis = dto.CrashDetection.Axis
        }
    };
}
