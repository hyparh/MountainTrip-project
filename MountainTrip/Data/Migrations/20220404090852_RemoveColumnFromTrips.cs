using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class RemoveColumnFromTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
