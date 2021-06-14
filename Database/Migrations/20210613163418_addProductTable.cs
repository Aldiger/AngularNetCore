using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Architecture.Database.Migrations
{
    public partial class addProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.InsertData(
                schema: "Product",
                table: "Product",
                columns: new[] { "Id", "Name", "Description", "Price", "UserId", "DateAdded", "DateModified" },
                values: new object[] { 1L, "ProductName", "ProductDescription", 100d, 1L, DateTime.UtcNow, DateTime.UtcNow });

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserId",
                schema: "Product",
                table: "Product",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");
        }
    }
}
