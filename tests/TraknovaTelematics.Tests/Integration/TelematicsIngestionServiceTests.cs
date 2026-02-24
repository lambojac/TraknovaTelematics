using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TraknovaTelematics.Core.Entities;
using TraknovaTelematics.Core.Services;
using TraknovaTelematics.Infrastructure.Data;
using TraknovaTelematics.Infrastructure.Repositories;
using Xunit;

namespace TraknovaTelematics.Tests.Integration;

/// <summary>
/// Tests the full ingestion pipeline (service + repository) using EF InMemory.
/// Verifies records are actually persisted correctly.
/// </summary>
public class TelematicsIngestionServiceTests : IDisposable
{
    private readonly TelematicsDbContext _context;
    private readonly TelematicsIngestionService _service;

    public TelematicsIngestionServiceTests()
    {
        var options = new DbContextOptionsBuilder<TelematicsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        _context = new TelematicsDbContext(options);
        var repository = new TelematicsRepository(_context);
        _service = new TelematicsIngestionService(repository, NullLogger<TelematicsIngestionService>.Instance);
    }

    [Fact]
    public async Task IngestAsync_ValidBatch_PersistsAllRecords()
    {
        var records = new List<TelematicsRecord>
        {
            MakeRecord(101, DateTime.UtcNow.AddMinutes(-10)),
            MakeRecord(102, DateTime.UtcNow.AddMinutes(-5)),
        };

        var result = await _service.IngestAsync(records);

        result.Accepted.Should().Be(2);
        result.Rejected.Should().Be(0);
        _context.TelematicsRecords.Count().Should().Be(2);
    }

    [Fact]
    public async Task IngestAsync_MixedBatch_OnlyPersistsValidRecords()
    {
        var records = new List<TelematicsRecord>
        {
            MakeRecord(101, DateTime.UtcNow.AddMinutes(-1)),      // valid
            new TelematicsRecord { VehicleId = 0, TimeStamp = DateTime.UtcNow } // invalid VehicleId
        };

        var result = await _service.IngestAsync(records);

        result.Accepted.Should().Be(1);
        result.Rejected.Should().Be(1);
        result.Errors.Should().ContainSingle();
        _context.TelematicsRecords.Count().Should().Be(1);
    }

    [Fact]
    public async Task IngestAsync_AllInvalid_PersistsNothing()
    {
        var records = new List<TelematicsRecord>
        {
            new() { VehicleId = -1, TimeStamp = default }
        };

        var result = await _service.IngestAsync(records);

        result.Accepted.Should().Be(0);
        result.Rejected.Should().Be(1);
        _context.TelematicsRecords.Should().BeEmpty();
    }

    [Fact]
    public async Task IngestAsync_RecordWithCrashDetection_PersistsCrashData()
    {
        var record = MakeRecord(103, DateTime.UtcNow.AddMinutes(-2));
        record.CrashDetection = new CrashDetectionRecord
        {
            ImpactMagnitude = 4.2,
            Axis = "X"
        };

        await _service.IngestAsync(new[] { record });

        var saved = _context.TelematicsRecords.Include(r => r.CrashDetection).First();
        saved.CrashDetection.Should().NotBeNull();
        saved.CrashDetection!.ImpactMagnitude.Should().Be(4.2);
        saved.CrashDetection.Axis.Should().Be("X");
    }

    [Fact]
    public async Task IngestAsync_SetsIngestedAtTimestamp()
    {
        var before = DateTime.UtcNow;
        await _service.IngestAsync(new[] { MakeRecord(101, DateTime.UtcNow.AddMinutes(-1)) });
        var after = DateTime.UtcNow;

        var saved = _context.TelematicsRecords.First();
        saved.IngestedAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public async Task IngestAsync_EmptyBatch_ReturnsZeroAccepted()
    {
        var result = await _service.IngestAsync(Enumerable.Empty<TelematicsRecord>());

        result.Accepted.Should().Be(0);
        result.Rejected.Should().Be(0);
    }

    private static TelematicsRecord MakeRecord(int vehicleId, DateTime timestamp) => new()
    {
        VehicleId = vehicleId,
        TimeStamp = timestamp,
        Latitude = 51.5,
        Longitude = -0.1,
        GpsSpeed = 60,
        BatteryVoltage = 12.6,
        EcoScore = 80
    };

    public void Dispose() => _context.Dispose();
}
