using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraknovaTelematics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelematicsRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),

                    // GPS
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Angle = table.Column<int>(type: "int", nullable: true),
                    Satellites = table.Column<int>(type: "int", nullable: true),
                    GpsSpeed = table.Column<double>(type: "float", nullable: true),

                    // CAN
                    CanSpeed = table.Column<double>(type: "float", nullable: true),
                    CanEngineRpm = table.Column<int>(type: "int", nullable: true),
                    EngineTemperature = table.Column<double>(type: "float", nullable: true),
                    CanTotalMileageMetres = table.Column<long>(type: "bigint", nullable: true),
                    CanFuelLevelLitres = table.Column<double>(type: "float", nullable: true),
                    CanFuelPercentage = table.Column<double>(type: "float", nullable: true),

                    // OBD
                    ObdEngineRpm = table.Column<int>(type: "int", nullable: true),
                    OBDOEMTotalMileageKM = table.Column<double>(type: "float", nullable: true),
                    ObdOemFuelLevelLitres = table.Column<double>(type: "float", nullable: true),
                    ObdFuelLevelPercent = table.Column<double>(type: "float", nullable: true),

                    // Odometry
                    TripOdemeter = table.Column<long>(type: "bigint", nullable: true),
                    TotalOdemeterMetres = table.Column<long>(type: "bigint", nullable: true),

                    // Battery
                    BatteryLevel = table.Column<double>(type: "float", nullable: true),
                    BatteryVoltage = table.Column<double>(type: "float", nullable: true),
                    BatteryCurrent = table.Column<double>(type: "float", nullable: true),

                    // Connectivity
                    GsmSignal = table.Column<int>(type: "int", nullable: true),
                    NetworkType = table.Column<int>(type: "int", nullable: true),
                    DataMode = table.Column<int>(type: "int", nullable: true),

                    // Flags
                    IgnitionActive = table.Column<bool>(type: "bit", nullable: true),
                    IsMoving = table.Column<bool>(type: "bit", nullable: true),
                    DigitalOutput1 = table.Column<bool>(type: "bit", nullable: true),
                    DigitalOutput2 = table.Column<bool>(type: "bit", nullable: true),
                    DigitalOutput3 = table.Column<bool>(type: "bit", nullable: true),

                    // Alerts
                    OilIndicatorActive = table.Column<bool>(type: "bit", nullable: true),
                    TowingAlert = table.Column<bool>(type: "bit", nullable: true),
                    IdlingAlert = table.Column<bool>(type: "bit", nullable: true),
                    OverSpeedingAlert = table.Column<bool>(type: "bit", nullable: true),
                    UnplugAlert = table.Column<bool>(type: "bit", nullable: true),

                    // Eco
                    EcoScore = table.Column<int>(type: "int", nullable: true),
                    GreenDrivingType = table.Column<int>(type: "int", nullable: true),
                    EcoDurationInMS = table.Column<long>(type: "bigint", nullable: true),

                    // Crash (owned type — flat columns)
                    CrashImpactMagnitude = table.Column<double>(type: "float", nullable: true),
                    CrashAxis = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelematicsRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelematicsRecords_VehicleId_TimeStamp",
                table: "TelematicsRecords",
                columns: new[] { "VehicleId", "TimeStamp" });

            migrationBuilder.CreateIndex(
                name: "IX_TelematicsRecords_TripId",
                table: "TelematicsRecords",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TelematicsRecords");
        }
    }
}
