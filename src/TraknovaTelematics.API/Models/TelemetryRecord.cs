using System;

namespace FleetTracking.Models
{
    public class TelemetryRecord
    {
        public long Id { get; set; }

        public int VehicleId { get; set; }

        public Guid? TripId { get; set; }

        public DateTime TimeStamp { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public double? Altitude { get; set; }

        public int? Angle { get; set; }

        public int? Satellites { get; set; }

        public int? GpsSpeed { get; set; }

        public int? CanSpeed { get; set; }

        public int? EngineRpm { get; set; }

        public int? EngineTemperature { get; set; }

        public long? TotalMileageMetres { get; set; }

        public long? TripOdometer { get; set; }

        public double? FuelLevelLitres { get; set; }

        public double? FuelPercentage { get; set; }

        public int? BatteryLevel { get; set; }

        public double? BatteryVoltage { get; set; }

        public double? BatteryCurrent { get; set; }

        public int? GsmSignal { get; set; }

        public bool? IgnitionActive { get; set; }

        public bool? IsMoving { get; set; }

        public bool? DigitalOutput1 { get; set; }

        public bool? DigitalOutput2 { get; set; }

        public bool? DigitalOutput3 { get; set; }

        public bool? OilIndicatorActive { get; set; }

        public int? EcoScore { get; set; }

        public int? DataMode { get; set; }

        public int? NetworkType { get; set; }

        public Vehicle Vehicle { get; set; }

        public Trip Trip { get; set; }

        public VehicleAlert Alert { get; set; }

        public CrashEvent CrashEvent { get; set; }

        public EcoDrivingEvent EcoDrivingEvent { get; set; }
    }
}