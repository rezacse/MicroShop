using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class CouponInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(6,2)", nullable: false),
                    IsDiscountPercentage = table.Column<bool>(type: "bit", nullable: false),
                    MinimumPurchaseAmount = table.Column<decimal>(type: "DECIMAL(6,0)", nullable: false),
                    MaximumDiscountAmount = table.Column<decimal>(type: "DECIMAL(4,0)", nullable: false),
                    OnlyForFirst = table.Column<bool>(type: "bit", nullable: false),
                    NoOfTimeCanBeUsed = table.Column<int>(type: "int", nullable: false),
                    NoTimeOfUsed = table.Column<int>(type: "int", nullable: false),
                    StartOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_DiscountAmount",
                table: "Coupon",
                column: "DiscountAmount",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");
        }
    }
}
