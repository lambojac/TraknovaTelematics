using FluentAssertions;
using TraknovaTelematics.Core.Entities;
using TraknovaTelematics.Core.Services;
using Xunit;

namespace TraknovaTelematics.Tests.Unit;

/// <summary>
/// Tests the pure validation logic in TelematicsIngestionService.
/// No DB or HTTP stack needed — fast, isolated, reliable.
/// </summary>
public class TelematicsValidationTests
{
    private static TelematicsRecord ValidRecord() => new()
    {
        VehicleId = 101,
        TimeStamp = DateTime.UtcNow.AddMinutes(-1),
        Latitude = 51.5074,
        Longitude = -0.1278,
        GpsSpeed = 60,
        BatteryVoltage = 12.6,
        EcoScore = 85
    };

    [Fact]
    public void Validate_ValidRecord_ReturnsNoErrors()
    {
        var errors = TelematicsIngestionService.Validate(ValidRecord());
        errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-999)]
    public void Validate_InvalidVehicleId_ReturnsError(int vehicleId)
    {
        var record = ValidRecord();
        record.VehicleId = vehicleId;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("VehicleId"));
    }

    [Fact]
    public void Validate_DefaultTimestamp_ReturnsError()
    {
        var record = ValidRecord();
        record.TimeStamp = default;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("TimeStamp"));
    }

    [Fact]
    public void Validate_FutureTimestamp_ReturnsError()
    {
        var record = ValidRecord();
        record.TimeStamp = DateTime.UtcNow.AddHours(1);

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("future"));
    }

    [Theory]
    [InlineData(91)]
    [InlineData(-91)]
    [InlineData(180)]
    public void Validate_LatitudeOutOfRange_ReturnsError(double lat)
    {
        var record = ValidRecord();
        record.Latitude = lat;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("Latitude"));
    }

    [Theory]
    [InlineData(181)]
    [InlineData(-181)]
    public void Validate_LongitudeOutOfRange_ReturnsError(double lng)
    {
        var record = ValidRecord();
        record.Longitude = lng;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("Longitude"));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(501)]
    public void Validate_ImplausibleGpsSpeed_ReturnsError(double speed)
    {
        var record = ValidRecord();
        record.GpsSpeed = speed;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("GpsSpeed"));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(20001)]
    public void Validate_ImplausibleEngineRpm_ReturnsError(int rpm)
    {
        var record = ValidRecord();
        record.CanEngineRpm = rpm;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("CanEngineRpm"));
    }

    [Fact]
    public void Validate_EcoScoreAbove100_ReturnsError()
    {
        var record = ValidRecord();
        record.EcoScore = 101;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().ContainSingle(e => e.Contains("EcoScore"));
    }

    [Fact]
    public void Validate_MultipleErrors_ReturnsAllErrors()
    {
        var record = ValidRecord();
        record.VehicleId = 0;
        record.Latitude = 200;
        record.GpsSpeed = -99;

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().HaveCount(3);
    }

    [Fact]
    public void Validate_NullOptionalFields_Passes()
    {
        // A minimal record with only required fields should pass
        var record = new TelematicsRecord
        {
            VehicleId = 42,
            TimeStamp = DateTime.UtcNow.AddSeconds(-30)
        };

        var errors = TelematicsIngestionService.Validate(record);

        errors.Should().BeEmpty();
    }
}
