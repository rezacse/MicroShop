using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MicroShop.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class CounponData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupon_DiscountAmount",
                table: "Coupon");

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "ID", "Code", "DiscountAmount", "EndOn", "IsDiscountPercentage", "MaximumDiscountAmount", "MinimumPurchaseAmount", "NoOfTimeCanBeUsed", "NoTimeOfUsed", "OnlyForFirst", "StartOn" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.00m, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, 20.00m, 1000.00m, 5, 0, false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "13OFF", 10.00m, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, 20.00m, 1000.00m, 5, 0, false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "10OAA", 10.00m, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, 20.00m, 1000.00m, 5, 0, false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "10OBB", 5.00m, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), true, 20.00m, 1000.00m, 5, 0, false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "10OCC", 10.00m, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), true, 20.00m, 1000.00m, 5, 0, false, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_Code",
                table: "Coupon",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coupon_Code",
                table: "Coupon");

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_DiscountAmount",
                table: "Coupon",
                column: "DiscountAmount",
                unique: true);
        }
    }
}
