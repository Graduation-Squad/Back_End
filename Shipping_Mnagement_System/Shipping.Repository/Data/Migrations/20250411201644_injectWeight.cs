using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class injectWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeightSetting_Governorates_GovernorateId",
                table: "WeightSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSetting",
                table: "WeightSetting");

            migrationBuilder.RenameTable(
                name: "WeightSetting",
                newName: "WeightSettings");

            migrationBuilder.RenameIndex(
                name: "IX_WeightSetting_GovernorateId",
                table: "WeightSettings",
                newName: "IX_WeightSettings_GovernorateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightSettings_Governorates_GovernorateId",
                table: "WeightSettings",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeightSettings_Governorates_GovernorateId",
                table: "WeightSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightSettings",
                table: "WeightSettings");

            migrationBuilder.RenameTable(
                name: "WeightSettings",
                newName: "WeightSetting");

            migrationBuilder.RenameIndex(
                name: "IX_WeightSettings_GovernorateId",
                table: "WeightSetting",
                newName: "IX_WeightSetting_GovernorateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightSetting",
                table: "WeightSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightSetting_Governorates_GovernorateId",
                table: "WeightSetting",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
