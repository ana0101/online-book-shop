using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineBookShop.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2df9dbdb-9734-4425-b7a9-508a059e1fdb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "478123f9-900a-4f51-9bd4-e612164a1904");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "567347b1-c6bb-4e72-a9af-510484aa3381");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2df9dbdb-9734-4425-b7a9-508a059e1fdb", "3", "Visitor", "VISITOR" },
                    { "478123f9-900a-4f51-9bd4-e612164a1904", "1", "Admin", "ADMIN" },
                    { "567347b1-c6bb-4e72-a9af-510484aa3381", "2", "User", "USER" }
                });
        }
    }
}
