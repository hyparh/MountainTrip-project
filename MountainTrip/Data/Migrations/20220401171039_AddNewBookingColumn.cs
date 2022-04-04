using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class AddNewBookingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "PeopleCount",
                table: "Bookings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleCount",
                table: "Bookings");
        }
    }
}
