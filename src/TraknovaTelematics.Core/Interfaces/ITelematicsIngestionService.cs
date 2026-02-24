using TraknovaTelematics.Core.Entities;

namespace TraknovaTelematics.Core.Interfaces;

public interface ITelematicsIngestionService
{
    /// <summary>
    /// Validates and persists a batch of telematics records.
    /// Returns a summary of how many succeeded or failed.
    /// </summary>
    Task<IngestionResult> IngestAsync(IEnumerable<TelematicsRecord> records, CancellationToken ct = default);
}

public record IngestionResult(int Accepted, int Rejected, IReadOnlyList<string> Errors);
