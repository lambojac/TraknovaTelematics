using TraknovaTelematics.Core.Entities;

namespace TraknovaTelematics.Core.Interfaces;

public interface ITelematicsRepository
{
    Task AddRangeAsync(IEnumerable<TelematicsRecord> records, CancellationToken ct = default);
    Task<IReadOnlyList<TelematicsRecord>> GetByVehicleAsync(int vehicleId, CancellationToken ct = default);
}
