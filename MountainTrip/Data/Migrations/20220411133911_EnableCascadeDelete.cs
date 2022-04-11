using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MountainTrip.Data.Migrations
{
    public partial class EnableCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guides_AspNetUsers_UserId",
                table: "Guides");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_AspNetUsers_UserId",
                table: "Guides",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guides_AspNetUsers_UserId",
                table: "Guides");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_AspNetUsers_UserId",
                table: "Guides",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
