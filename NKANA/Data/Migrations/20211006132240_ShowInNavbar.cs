using Microsoft.EntityFrameworkCore.Migrations;

namespace NKANA.Data.Migrations
{
    public partial class ShowInNavbar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowInNavbar",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowInNavbar",
                table: "Categories");
        }
    }
}
