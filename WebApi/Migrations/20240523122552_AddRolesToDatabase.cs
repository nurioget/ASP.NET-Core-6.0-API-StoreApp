using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02dfb742-784f-4309-b1bd-476f7c7d5f75", "8bc00bb6-9fae-4885-a230-d01cbe2a58b5", "Admin", "ADMİN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2575b41d-2d18-4677-8017-fed0b655e94f", "9b217cfe-740f-4f26-b83b-060f95332a71", "Editor", "EDİTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f76054f-bdc1-482a-ac60-0c8facc720a7", "6cc313e0-0b38-406f-8bb8-946109d6dbc6", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02dfb742-784f-4309-b1bd-476f7c7d5f75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2575b41d-2d18-4677-8017-fed0b655e94f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f76054f-bdc1-482a-ac60-0c8facc720a7");
        }
    }
}
