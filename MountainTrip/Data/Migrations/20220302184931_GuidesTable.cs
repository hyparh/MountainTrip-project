using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class GuidesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guides_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_GuideId",
                table: "Trips",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_UserId",
                table: "Guides",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Guides_GuideId",
                table: "Trips",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Guides_GuideId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_Trips_GuideId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "Trips");
        }
    }
}
