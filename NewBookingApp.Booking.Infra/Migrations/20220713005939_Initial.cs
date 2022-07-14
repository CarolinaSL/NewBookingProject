using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewBookingApp.Booking.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Trip_FlightNumber = table.Column<string>(type: "text", nullable: false),
                    Trip_AircraftId = table.Column<long>(type: "bigint", nullable: false),
                    Trip_DepartureAirportId = table.Column<long>(type: "bigint", nullable: false),
                    Trip_ArriveAirportId = table.Column<long>(type: "bigint", nullable: false),
                    Trip_FlightDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Trip_Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Trip_Description = table.Column<string>(type: "text", nullable: false),
                    Trip_SeatNumber = table.Column<string>(type: "text", nullable: false),
                    PassengerInfo_Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
