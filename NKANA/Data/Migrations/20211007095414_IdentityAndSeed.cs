using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NKANA.Data.Migrations
{
    public partial class IdentityAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtWorkRequests_User_UserId",
                table: "ArtWorkRequests");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1135f23e-fcafce7b94ae-2eaf44ef830d", "0858238f-f3b3-42f7-a34b-d43d54e02462", "Admin", "ADMIN" },
                    { "bede-69a91d0e771d-4f974cf1-bc02-4aa7", "6954fc97-7e60-45ed-a80f-e319a40399b0", "Artist", "ARTIST" },
                    { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "c6d848c3-4885-4ca6-b88c-a5e93187bac8", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4f974cf1-bc02-4aa7-bede-69a91d0e771d", 0, "fa7ff9e8-fae8-4a2e-8664-a32dce6d7332", "admin@acdte.com", true, null, null, false, null, "ADMIN@ACDTE.COM", "ADMIN", "AQAAAAEAACcQAAAAEOXP8qq84anuzQkszIvCVMnQYrgE6iKWHkppA6JH6w8/116oC+X5VKvPOw941/asYQ==", null, false, "fa7ff9e8-fae8-4a2e-8664-a32dce6d7332", false, "Admin" },
                    { "0e20a2de-342e-4b9d-a153-1c180e7f6435", 0, "66e5e17c-7391-4937-9a82-bc36661a2f7e", "superadmin@acdte.com", true, null, null, false, null, "SUPERADMIN@ACDTE.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEELp6FqpcHmlQMXwrflJ4BAsAa3jwm133/AWr99VcuOQoBvMHGz78IRt3EOtXUfIyg==", null, false, "66e5e17c-7391-4937-9a82-bc36661a2f7e", false, "SuperAdmin" },
                    { "1135f23e-2eaf-44ef-830d-fcafce7b94ae", 0, "fa7ff9e8-fae8-4a2e-a32dce6d7332-8664", "system@acdte.com", true, null, null, false, null, "SYSTEM@ACDTE.COM", "SYSTEM", "AQAAAAEAACcQAAAAEOXP8qq84anuzQkszIvCVMnQYrgE6iKWHkppA6JH6w8/116oC+X5VKvPOw941/asYQ==", null, false, "fa7ff9e8-fae8-4a2e-a32dce6d7332-8664", false, "System" },
                    { "1135f23e-44ef-2eaf-830d-7b94aefcafce", 0, "7391-66e5e17c-4937-9a82-bc36661a2f7e", "africhina@acdte.com", true, null, null, false, null, "AFRICHINA@ACDTE.COM", "AFRICHINA", "AQAAAAEAACcQAAAAEELp6FqpcHmlQMXwrflJ4BAsAa3jwm133/AWr99VcuOQoBvMHGz78IRt3EOtXUfIyg==", null, false, "7391-66e5e17c-4937-9a82-bc36661a2f7e", false, "Africhina" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1135f23e-fcafce7b94ae-2eaf44ef830d", "4f974cf1-bc02-4aa7-bede-69a91d0e771d" },
                    { "1135f23e-fcafce7b94ae-2eaf44ef830d", "0e20a2de-342e-4b9d-a153-1c180e7f6435" },
                    { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "0e20a2de-342e-4b9d-a153-1c180e7f6435" },
                    { "1135f23e-fcafce7b94ae-2eaf44ef830d", "1135f23e-2eaf-44ef-830d-fcafce7b94ae" },
                    { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "1135f23e-2eaf-44ef-830d-fcafce7b94ae" },
                    { "1135f23e-fcafce7b94ae-2eaf44ef830d", "1135f23e-44ef-2eaf-830d-7b94aefcafce" },
                    { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "1135f23e-44ef-2eaf-830d-7b94aefcafce" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtWorkRequests_AspNetUsers_UserId",
                table: "ArtWorkRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtWorkRequests_AspNetUsers_UserId",
                table: "ArtWorkRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bede-69a91d0e771d-4f974cf1-bc02-4aa7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "0e20a2de-342e-4b9d-a153-1c180e7f6435" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-fcafce7b94ae-2eaf44ef830d", "0e20a2de-342e-4b9d-a153-1c180e7f6435" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "1135f23e-2eaf-44ef-830d-fcafce7b94ae" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-fcafce7b94ae-2eaf44ef830d", "1135f23e-2eaf-44ef-830d-fcafce7b94ae" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-44ef-2eaf-830d-7b94aefcafce", "1135f23e-44ef-2eaf-830d-7b94aefcafce" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-fcafce7b94ae-2eaf44ef830d", "1135f23e-44ef-2eaf-830d-7b94aefcafce" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1135f23e-fcafce7b94ae-2eaf44ef830d", "4f974cf1-bc02-4aa7-bede-69a91d0e771d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1135f23e-44ef-2eaf-830d-7b94aefcafce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1135f23e-fcafce7b94ae-2eaf44ef830d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e20a2de-342e-4b9d-a153-1c180e7f6435");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1135f23e-2eaf-44ef-830d-fcafce7b94ae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1135f23e-44ef-2eaf-830d-7b94aefcafce");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f974cf1-bc02-4aa7-bede-69a91d0e771d");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtWorkRequests_User_UserId",
                table: "ArtWorkRequests",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
