using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class ModifyTripTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Bookings_BookingId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_BookingId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BookingId",
                table: "Trips",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Bookings_BookingId",
                table: "Trips",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
