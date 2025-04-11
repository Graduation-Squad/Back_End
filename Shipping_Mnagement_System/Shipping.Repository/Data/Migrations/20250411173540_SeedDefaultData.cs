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
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employee_CreatedById",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employee_EmployeeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_AppUserId",
                table: "Employees",
                newName: "IX_Employees_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_CreatedById",
                table: "Orders",
                column: "CreatedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_AppUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_CreatedById",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_EmployeeId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

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

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_AppUserId",
                table: "Employee",
                newName: "IX_Employee_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employee_CreatedById",
                table: "Orders",
                column: "CreatedById",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employee_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
