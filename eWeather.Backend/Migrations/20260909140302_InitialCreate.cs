using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eWeather.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeerMetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tijdstip = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Station = table.Column<string>(type: "TEXT", nullable: false),
                    Temperature = table.Column<float>(type: "REAL", nullable: false),
                    FeelTemperature = table.Column<float>(type: "REAL", nullable: false),
                    GroundTemperature = table.Column<float>(type: "REAL", nullable: false),
                    SunPower = table.Column<float>(type: "REAL", nullable: false),
                    RainFallLastHour = table.Column<float>(type: "REAL", nullable: false),
                    WindDirection = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeerMetings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeerMetings");
        }
    }
}
