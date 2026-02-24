using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using TraknovaTelematics.Infrastructure.Data;

#nullable disable

namespace TraknovaTelematics.Infrastructure.Migrations
{
    [DbContext(typeof(TelematicsDbContext))]
    partial class TelematicsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TraknovaTelematics.Core.Entities.TelematicsRecord", b =>
            {
                b.Property<long>("Id").ValueGeneratedOnAdd().HasColumnType("bigint");
                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                b.Property<int>("VehicleId").HasColumnType("int");
                b.Property<DateTime>("TimeStamp").HasColumnType("datetime2");
                b.Property<DateTime>("IngestedAt").HasColumnType("datetime2");
                b.Property<Guid?>("TripId").HasColumnType("uniqueidentifier");

                b.Property<decimal?>("Longitude").HasColumnType("decimal(10,7)");
                b.Property<decimal?>("Latitude").HasColumnType("decimal(10,7)");
                b.Property<double?>("Altitude").HasColumnType("float");
                b.Property<int?>("Angle").HasColumnType("int");
                b.Property<int?>("Satellites").HasColumnType("int");
                b.Property<double?>("GpsSpeed").HasColumnType("float");

                b.Property<double?>("CanSpeed").HasColumnType("float");
                b.Property<int?>("CanEngineRpm").HasColumnType("int");
                b.Property<double?>("EngineTemperature").HasColumnType("float");
                b.Property<long?>("CanTotalMileageMetres").HasColumnType("bigint");
                b.Property<double?>("CanFuelLevelLitres").HasColumnType("float");
                b.Property<double?>("CanFuelPercentage").HasColumnType("float");

                b.Property<int?>("ObdEngineRpm").HasColumnType("int");
                b.Property<double?>("OBDOEMTotalMileageKM").HasColumnType("float");
                b.Property<double?>("ObdOemFuelLevelLitres").HasColumnType("float");
                b.Property<double?>("ObdFuelLevelPercent").HasColumnType("float");

                b.Property<long?>("TripOdemeter").HasColumnType("bigint");
                b.Property<long?>("TotalOdemeterMetres").HasColumnType("bigint");
                b.Property<double?>("BatteryLevel").HasColumnType("float");
                b.Property<double?>("BatteryVoltage").HasColumnType("float");
                b.Property<double?>("BatteryCurrent").HasColumnType("float");

                b.Property<int?>("GsmSignal").HasColumnType("int");
                b.Property<int?>("NetworkType").HasColumnType("int");
                b.Property<int?>("DataMode").HasColumnType("int");

                b.Property<bool?>("IgnitionActive").HasColumnType("bit");
                b.Property<bool?>("IsMoving").HasColumnType("bit");
                b.Property<bool?>("DigitalOutput1").HasColumnType("bit");
                b.Property<bool?>("DigitalOutput2").HasColumnType("bit");
                b.Property<bool?>("DigitalOutput3").HasColumnType("bit");

                b.Property<bool?>("OilIndicatorActive").HasColumnType("bit");
                b.Property<bool?>("TowingAlert").HasColumnType("bit");
                b.Property<bool?>("IdlingAlert").HasColumnType("bit");
                b.Property<bool?>("OverSpeedingAlert").HasColumnType("bit");
                b.Property<bool?>("UnplugAlert").HasColumnType("bit");

                b.Property<int?>("EcoScore").HasColumnType("int");
                b.Property<int?>("GreenDrivingType").HasColumnType("int");
                b.Property<long?>("EcoDurationInMS").HasColumnType("bigint");

                b.HasKey("Id");
                b.HasIndex("TripId").HasDatabaseName("IX_TelematicsRecords_TripId");
                b.HasIndex("VehicleId", "TimeStamp").HasDatabaseName("IX_TelematicsRecords_VehicleId_TimeStamp");
                b.ToTable("TelematicsRecords");

                b.OwnsOne("TraknovaTelematics.Core.Entities.CrashDetectionRecord", "CrashDetection", b1 =>
                {
                    b1.Property<double>("ImpactMagnitude").HasColumnName("CrashImpactMagnitude").HasColumnType("float");
                    b1.Property<string>("Axis").HasColumnName("CrashAxis").HasMaxLength(10).HasColumnType("nvarchar(10)");
                    b1.WithOwner().HasForeignKey("Id");
                    b1.ToTable("TelematicsRecords");
                });
            });
#pragma warning restore 612, 618
        }
    }
}
