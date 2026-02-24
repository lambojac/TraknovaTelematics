public class Telemetry
{
    public long Id { get; set; }

    public int VehicleId { get; set; }

    public DateTime TimeStamp { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public bool IgnitionActive { get; set; }

    public bool IsMoving { get; set; }

    public int DataMode { get; set; }

    public int? NetworkType { get; set; }

    public Guid? TripId { get; set; }

    public Vehicle Vehicle { get; set; }

    public Trip Trip { get; set; }

    public Position Position { get; set; }

    public CanData CanData { get; set; }

    public ObdData ObdData { get; set; }

    public FuelData FuelData { get; set; }

    public BatteryData BatteryData { get; set; }

    public Alert Alert { get; set; }

    public EcoDriving EcoDriving { get; set; }

    public CrashEvent CrashEvent { get; set; }
}