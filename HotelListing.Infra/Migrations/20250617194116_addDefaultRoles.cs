using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b9d7ca9-5737-4b6b-8298-33cde20cfc05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c5e84a9-45dd-4c24-8332-c92102bc3f59");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a111111-1111-1111-1111-111111111111", null, "Administrator", "ADMINISTRATOR" },
                    { "2b222222-2222-2222-2222-222222222222", null, "Normal User", "NORMAL USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b222222-2222-2222-2222-222222222222");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b9d7ca9-5737-4b6b-8298-33cde20cfc05", null, "Administrator", "ADMINISTRATOR" },
                    { "7c5e84a9-45dd-4c24-8332-c92102bc3f59", null, "Normal User", "NORMAL USER" }
                });
        }
    }
}
