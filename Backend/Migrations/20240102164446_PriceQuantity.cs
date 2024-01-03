using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBookShop.Migrations
{
    /// <inheritdoc />
    public partial class PriceQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "BookOrders",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "BookOrders",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "BookOrders",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "BookOrders",
                newName: "price");
        }
    }
}
