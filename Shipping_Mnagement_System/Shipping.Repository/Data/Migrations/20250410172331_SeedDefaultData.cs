using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 1, true, "Default Governorate" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GovernorateId", "IsActive", "Name" },
                values: new object[] { 1, 1, true, "Default City" });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "CityId", "IsActive", "Name" },
                values: new object[] { 1, 1, true, "Default Area" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
