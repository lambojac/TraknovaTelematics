using Microsoft.EntityFrameworkCore;
using TraknovaTelematics.Core.Entities;

namespace TraknovaTelematics.Infrastructure.Data;

public class TelematicsDbContext : DbContext
{
    public TelematicsDbContext(DbContextOptions<TelematicsDbContext> options) : base(options) { }

    public DbSet<TelematicsRecord> TelematicsRecords => Set<TelematicsRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TelematicsRecord>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                  .UseIdentityColumn();

            entity.HasIndex(e => new { e.VehicleId, e.TimeStamp })
                  .HasDatabaseName("IX_TelematicsRecords_VehicleId_TimeStamp");

            entity.HasIndex(e => e.TripId)
                  .HasDatabaseName("IX_TelematicsRecords_TripId");

            entity.Property(e => e.TimeStamp)
                  .HasColumnType("datetime2");

            entity.Property(e => e.IngestedAt)
                  .HasColumnType("datetime2");

            entity.Property(e => e.TripId)
                  .HasColumnType("uniqueidentifier");

            entity.OwnsOne(e => e.CrashDetection, crash =>
            {
                crash.Property(c => c.ImpactMagnitude)
                     .HasColumnName("CrashImpactMagnitude");
                crash.Property(c => c.Axis)
                     .HasColumnName("CrashAxis")
                     .HasMaxLength(10);
            });

            entity.Property(e => e.Longitude).HasColumnType("decimal(10,7)");
            entity.Property(e => e.Latitude).HasColumnType("decimal(10,7)");
        });
    }
}
