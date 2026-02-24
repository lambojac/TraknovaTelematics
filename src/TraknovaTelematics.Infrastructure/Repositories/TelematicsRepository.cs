using Microsoft.EntityFrameworkCore;
using TraknovaTelematics.Core.Entities;
using TraknovaTelematics.Core.Interfaces;
using TraknovaTelematics.Infrastructure.Data;

namespace TraknovaTelematics.Infrastructure.Repositories;

public class TelematicsRepository : ITelematicsRepository
{
    private readonly TelematicsDbContext _context;

    public TelematicsRepository(TelematicsDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<TelematicsRecord> records, CancellationToken ct = default)
    {
        await _context.TelematicsRecords.AddRangeAsync(records, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<TelematicsRecord>> GetByVehicleAsync(int vehicleId, CancellationToken ct = default)
    {
        return await _context.TelematicsRecords
            .Where(r => r.VehicleId == vehicleId)
            .OrderByDescending(r => r.TimeStamp)
            .ToListAsync(ct);
    }
}
