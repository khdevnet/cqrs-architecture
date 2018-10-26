using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseReadViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseReadViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineViews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProductId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineViews_OrderViews_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseItemReadViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseItemReadViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseItemReadViews_WarehouseReadViews_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseReadViews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WarehouseReadViews",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6df8744a-d464-4826-91d1-08095ab49d93"), "Naboo" });

            migrationBuilder.InsertData(
                table: "WarehouseReadViews",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6df8744a-d464-4826-91d1-08095ab49d94"), "Tatooine" });

            migrationBuilder.InsertData(
                table: "WarehouseItemReadViews",
                columns: new[] { "Id", "ProductId", "Quantity", "WarehouseId" },
                values: new object[,]
                {
                    { new Guid("aaacc12f-5f0d-4fb6-b71d-0186555ab076"), 1, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d93") },
                    { new Guid("a5310b55-5628-4b81-8ec9-302131acf330"), 2, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d93") },
                    { new Guid("bf5d8b3b-7334-4b9f-b424-fbe409720bee"), 3, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d93") },
                    { new Guid("d90af348-bd20-4d59-8456-1391e894edcb"), 4, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d93") },
                    { new Guid("f7ea3f87-d72a-439a-8203-90df1b5a3e1a"), 5, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d93") },
                    { new Guid("17366bf0-d0a2-463e-b64b-999d2d8651e9"), 1, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d94") },
                    { new Guid("b382ef4e-a9fc-4a6f-9d0c-23a0bff3533f"), 2, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d94") },
                    { new Guid("0e594240-a791-4b38-9e34-e05407091927"), 3, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d94") },
                    { new Guid("f492d3e5-3ca2-430c-91bf-42936fe79931"), 4, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d94") },
                    { new Guid("98d0b98e-f7bc-4fe2-8d20-f9f5647d28a7"), 5, 5000, new Guid("6df8744a-d464-4826-91d1-08095ab49d94") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineViews_OrderId",
                table: "OrderLineViews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItemReadViews_WarehouseId",
                table: "WarehouseItemReadViews",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLineViews");

            migrationBuilder.DropTable(
                name: "WarehouseItemReadViews");

            migrationBuilder.DropTable(
                name: "OrderViews");

            migrationBuilder.DropTable(
                name: "WarehouseReadViews");
        }
    }
}
