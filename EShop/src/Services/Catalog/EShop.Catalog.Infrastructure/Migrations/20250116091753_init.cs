using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4511), "Elektronik Ürünler", null, "Elektronik" },
                    { 2, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4515), "Giyim Ürünleri", null, "Giyim" },
                    { 3, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4516), "Kozmetik Ürünler", null, "Kozmetik" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "LastModifiedDate", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("243d1cd2-876e-4094-8e21-79b1d257219c"), 2, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4696), "LCW ", "noImage.png", null, "Gömlek", 750m, 150 },
                    { new Guid("3b27030c-40d9-41ab-b528-7274d115d8b9"), 1, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4694), "Samsung S21", "noImage.png", null, "Samsung S21", 9000m, 100 },
                    { new Guid("ba170801-bbe2-4117-98e1-31da7ccb050f"), 1, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4662), "Apple Iphone 12", "noImage.png", null, "Iphone 12", 10000m, 100 },
                    { new Guid("c8293825-eeac-4e83-8efc-09ea5d0416c7"), 2, new DateTime(2025, 1, 16, 9, 17, 52, 738, DateTimeKind.Utc).AddTicks(4698), "DeFacto", "noImage.png", null, "Pantolon", 1000m, 150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
