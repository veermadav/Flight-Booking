using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAircrafts",
                columns: table => new
                {
                    AircraftCode = table.Column<string>(type: "text", nullable: false),
                    FlightNumber = table.Column<string>(type: "text", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    ArrivalTime = table.Column<string>(type: "text", nullable: false),
                    DepartureTime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAircrafts", x => x.AircraftCode);
                });

            migrationBuilder.CreateTable(
                name: "tblBookings",
                columns: table => new
                {
                    AircraftCode = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    SeatNo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookings", x => x.AircraftCode);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UsrPassword = table.Column<string>(type: "text", nullable: true),
                    UsrEmail = table.Column<string>(type: "text", nullable: true),
                    UsrPhoneNo = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    UsrRole = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.UserName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAircrafts");

            migrationBuilder.DropTable(
                name: "tblBookings");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
