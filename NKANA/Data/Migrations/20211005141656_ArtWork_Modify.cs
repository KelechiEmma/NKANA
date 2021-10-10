using Microsoft.EntityFrameworkCore.Migrations;

namespace NKANA.Data.Migrations
{
    public partial class ArtWork_Modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtWorkRequests_AspNetUsers_UserId",
                table: "ArtWorkRequests");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ArtWorks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ArtWorkRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtWorkRequests_User_UserId",
                table: "ArtWorkRequests",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtWorkRequests_User_UserId",
                table: "ArtWorkRequests");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ArtWorks");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ArtWorkRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtWorkRequests_AspNetUsers_UserId",
                table: "ArtWorkRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
