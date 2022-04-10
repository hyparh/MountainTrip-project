using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class NewColumnForBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Bookings");
        }
    }
}
