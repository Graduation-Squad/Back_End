using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVillageDeliveryToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVillageDelivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVillageDelivery",
                table: "Orders");
        }
    }
}
