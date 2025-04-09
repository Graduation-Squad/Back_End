using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class missingDbSetsAndFixIssueInOrderConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_City_CityId",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Area_AreaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_City_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Governorate_GovernorateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Governorate_GovernorateId",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMan_AspNetUsers_AppUserId",
                table: "DeliveryMan");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_AspNetUsers_AppUserId",
                table: "Merchant");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Area_AreaId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_CreatedById",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_City_CityId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryMan_DeliveryAgentId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Employee_EmployeeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Governorate_GovernorateId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Merchant_MerchantId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShippingType_ShippingTypeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTracking_AspNetUsers_UserId",
                table: "OrderTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTracking_Order_OrderId",
                table: "OrderTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTracking_RejectionReason_RejectionReasonId",
                table: "OrderTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranch_AspNetUsers_UserId",
                table: "UserBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranch_Branch_BranchId",
                table: "UserBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightSetting_Governorate_GovernorateId",
                table: "WeightSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBranch",
                table: "UserBranch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingType",
                table: "ShippingType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTracking",
                table: "OrderTracking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Governorate",
                table: "Governorate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMan",
                table: "DeliveryMan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.RenameTable(
                name: "UserBranch",
                newName: "UserBranches");

            migrationBuilder.RenameTable(
                name: "ShippingType",
                newName: "ShippingTypes");

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                newName: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "OrderTracking",
                newName: "OrderTrackings");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Merchant",
                newName: "Merchants");

            migrationBuilder.RenameTable(
                name: "Governorate",
                newName: "Governorates");

            migrationBuilder.RenameTable(
                name: "DeliveryMan",
                newName: "DeliveryMen");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branches");

            migrationBuilder.RenameTable(
                name: "Area",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_UserBranch_BranchId",
                table: "UserBranches",
                newName: "IX_UserBranches_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTracking_UserId",
                table: "OrderTrackings",
                newName: "IX_OrderTrackings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTracking_RejectionReasonId",
                table: "OrderTrackings",
                newName: "IX_OrderTrackings_RejectionReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTracking_OrderId",
                table: "OrderTrackings",
                newName: "IX_OrderTrackings_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ShippingTypeId",
                table: "Orders",
                newName: "IX_Orders_ShippingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Orders",
                newName: "IX_Orders_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_MerchantId",
                table: "Orders",
                newName: "IX_Orders_MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_GovernorateId",
                table: "Orders",
                newName: "IX_Orders_GovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_EmployeeId",
                table: "Orders",
                newName: "IX_Orders_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryAgentId",
                table: "Orders",
                newName: "IX_Orders_DeliveryAgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CreatedById",
                table: "Orders",
                newName: "IX_Orders_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CityId",
                table: "Orders",
                newName: "IX_Orders_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_BranchId",
                table: "Orders",
                newName: "IX_Orders_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AreaId",
                table: "Orders",
                newName: "IX_Orders_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Merchant_AppUserId",
                table: "Merchants",
                newName: "IX_Merchants_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryMan_AppUserId",
                table: "DeliveryMen",
                newName: "IX_DeliveryMen_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_City_GovernorateId",
                table: "Cities",
                newName: "IX_Cities_GovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_Area_CityId",
                table: "Areas",
                newName: "IX_Areas_CityId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DiscountType",
                table: "DeliveryMen",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBranches",
                table: "UserBranches",
                columns: new[] { "UserId", "BranchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingTypes",
                table: "ShippingTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Governorates",
                table: "Governorates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMen",
                table: "DeliveryMen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Cities_CityId",
                table: "Areas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Areas_AreaId",
                table: "AspNetUsers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Governorates_GovernorateId",
                table: "AspNetUsers",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Governorates_GovernorateId",
                table: "Cities",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMen_AspNetUsers_AppUserId",
                table: "DeliveryMen",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_AspNetUsers_AppUserId",
                table: "Merchants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Areas_AreaId",
                table: "Orders",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedById",
                table: "Orders",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branches_BranchId",
                table: "Orders",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMen_DeliveryAgentId",
                table: "Orders",
                column: "DeliveryAgentId",
                principalTable: "DeliveryMen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employee_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Governorates_GovernorateId",
                table: "Orders",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Merchants_MerchantId",
                table: "Orders",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingTypes_ShippingTypeId",
                table: "Orders",
                column: "ShippingTypeId",
                principalTable: "ShippingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackings_AspNetUsers_UserId",
                table: "OrderTrackings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackings_Orders_OrderId",
                table: "OrderTrackings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTrackings_RejectionReason_RejectionReasonId",
                table: "OrderTrackings",
                column: "RejectionReasonId",
                principalTable: "RejectionReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranches_AspNetUsers_UserId",
                table: "UserBranches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranches_Branches_BranchId",
                table: "UserBranches",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightSetting_Governorates_GovernorateId",
                table: "WeightSetting",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Cities_CityId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Areas_AreaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Governorates_GovernorateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Governorates_GovernorateId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMen_AspNetUsers_AppUserId",
                table: "DeliveryMen");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_AspNetUsers_AppUserId",
                table: "Merchants");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Areas_AreaId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreatedById",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branches_BranchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cities_CityId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMen_DeliveryAgentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employee_EmployeeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Governorates_GovernorateId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Merchants_MerchantId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingTypes_ShippingTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackings_AspNetUsers_UserId",
                table: "OrderTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackings_Orders_OrderId",
                table: "OrderTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTrackings_RejectionReason_RejectionReasonId",
                table: "OrderTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranches_AspNetUsers_UserId",
                table: "UserBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBranches_Branches_BranchId",
                table: "UserBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightSetting_Governorates_GovernorateId",
                table: "WeightSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBranches",
                table: "UserBranches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingTypes",
                table: "ShippingTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTrackings",
                table: "OrderTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchants",
                table: "Merchants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Governorates",
                table: "Governorates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMen",
                table: "DeliveryMen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "UserBranches",
                newName: "UserBranch");

            migrationBuilder.RenameTable(
                name: "ShippingTypes",
                newName: "ShippingType");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "PaymentMethod");

            migrationBuilder.RenameTable(
                name: "OrderTrackings",
                newName: "OrderTracking");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Merchants",
                newName: "Merchant");

            migrationBuilder.RenameTable(
                name: "Governorates",
                newName: "Governorate");

            migrationBuilder.RenameTable(
                name: "DeliveryMen",
                newName: "DeliveryMan");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branch");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_UserBranches_BranchId",
                table: "UserBranch",
                newName: "IX_UserBranch_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTrackings_UserId",
                table: "OrderTracking",
                newName: "IX_OrderTracking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTrackings_RejectionReasonId",
                table: "OrderTracking",
                newName: "IX_OrderTracking_RejectionReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTrackings_OrderId",
                table: "OrderTracking",
                newName: "IX_OrderTracking_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShippingTypeId",
                table: "Order",
                newName: "IX_Order_ShippingTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Order",
                newName: "IX_Order_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MerchantId",
                table: "Order",
                newName: "IX_Order_MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_GovernorateId",
                table: "Order",
                newName: "IX_Order_GovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EmployeeId",
                table: "Order",
                newName: "IX_Order_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryAgentId",
                table: "Order",
                newName: "IX_Order_DeliveryAgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CreatedById",
                table: "Order",
                newName: "IX_Order_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CityId",
                table: "Order",
                newName: "IX_Order_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BranchId",
                table: "Order",
                newName: "IX_Order_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AreaId",
                table: "Order",
                newName: "IX_Order_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Merchants_AppUserId",
                table: "Merchant",
                newName: "IX_Merchant_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryMen_AppUserId",
                table: "DeliveryMan",
                newName: "IX_DeliveryMan_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_GovernorateId",
                table: "City",
                newName: "IX_City_GovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_CityId",
                table: "Area",
                newName: "IX_Area_CityId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountType",
                table: "DeliveryMan",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBranch",
                table: "UserBranch",
                columns: new[] { "UserId", "BranchId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingType",
                table: "ShippingType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTracking",
                table: "OrderTracking",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchant",
                table: "Merchant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Governorate",
                table: "Governorate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMan",
                table: "DeliveryMan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_City_CityId",
                table: "Area",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Area_AreaId",
                table: "AspNetUsers",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_City_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Governorate_GovernorateId",
                table: "AspNetUsers",
                column: "GovernorateId",
                principalTable: "Governorate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Governorate_GovernorateId",
                table: "City",
                column: "GovernorateId",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMan_AspNetUsers_AppUserId",
                table: "DeliveryMan",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_AspNetUsers_AppUserId",
                table: "Merchant",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Area_AreaId",
                table: "Order",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_CreatedById",
                table: "Order",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_City_CityId",
                table: "Order",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryMan_DeliveryAgentId",
                table: "Order",
                column: "DeliveryAgentId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Employee_EmployeeId",
                table: "Order",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Governorate_GovernorateId",
                table: "Order",
                column: "GovernorateId",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Merchant_MerchantId",
                table: "Order",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShippingType_ShippingTypeId",
                table: "Order",
                column: "ShippingTypeId",
                principalTable: "ShippingType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTracking_AspNetUsers_UserId",
                table: "OrderTracking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTracking_Order_OrderId",
                table: "OrderTracking",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTracking_RejectionReason_RejectionReasonId",
                table: "OrderTracking",
                column: "RejectionReasonId",
                principalTable: "RejectionReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranch_AspNetUsers_UserId",
                table: "UserBranch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBranch_Branch_BranchId",
                table: "UserBranch",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightSetting_Governorate_GovernorateId",
                table: "WeightSetting",
                column: "GovernorateId",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
