using Microsoft.Extensions.Logging;
using TraknovaTelematics.Core.Entities;
using TraknovaTelematics.Core.Interfaces;

namespace TraknovaTelematics.Core.Services;

public class TelematicsIngestionService : ITelematicsIngestionService
{
    private readonly ITelematicsRepository _repository;
    private readonly ILogger<TelematicsIngestionService> _logger;

    public TelematicsIngestionService(
        ITelematicsRepository repository,
        ILogger<TelematicsIngestionService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IngestionResult> IngestAsync(
        IEnumerable<TelematicsRecord> records,
        CancellationToken ct = default)
    {
        var validRecords = new List<TelematicsRecord>();
        var errors = new List<string>();

        foreach (var record in records)
        {
            var validationErrors = Validate(record);
            if (validationErrors.Count > 0)
            {
                var msg = $"VehicleId={record.VehicleId} @ {record.TimeStamp:O}: {string.Join("; ", validationErrors)}";
                _logger.LogWarning("Rejected record: {Reason}", msg);
                errors.Add(msg);
                continue;
            }

            record.IngestedAt = DateTime.UtcNow;
            validRecords.Add(record);
        }

        if (validRecords.Count > 0)
        {
            await _repository.AddRangeAsync(validRecords, ct);
            _logger.LogInformation("Ingested {Count} records successfully.", validRecords.Count);
        }

        return new IngestionResult(validRecords.Count, errors.Count, errors);
    }

    /// <summary>
    /// Domain-level validation rules. Returns a list of violation messages.
    /// Keeping this pure (no DB calls) makes it trivially unit-testable.
    /// </summary>
    internal static List<string> Validate(TelematicsRecord r)
    {
        var errors = new List<string>();

        if (r.VehicleId <= 0)
            errors.Add("VehicleId must be a positive integer.");

        if (r.TimeStamp == default)
            errors.Add("TimeStamp is required.");

        if (r.TimeStamp > DateTime.UtcNow.AddMinutes(5))
            errors.Add("TimeStamp is more than 5 minutes in the future — possible clock skew.");

        if (r.Latitude is < -90 or > 90)
            errors.Add($"Latitude {r.Latitude} is out of range [-90, 90].");

        if (r.Longitude is < -180 or > 180)
            errors.Add($"Longitude {r.Longitude} is out of range [-180, 180].");

        if (r.GpsSpeed is < 0 or > 500)
            errors.Add($"GpsSpeed {r.GpsSpeed} km/h is implausible.");

        if (r.CanEngineRpm is < 0 or > 20000)
            errors.Add($"CanEngineRpm {r.CanEngineRpm} is implausible.");

        if (r.BatteryVoltage is < 0 or > 60)
            errors.Add($"BatteryVoltage {r.BatteryVoltage}V is implausible.");

        if (r.EcoScore is < 0 or > 100)
            errors.Add($"EcoScore {r.EcoScore} must be 0-100.");

        return errors;
    }
}
