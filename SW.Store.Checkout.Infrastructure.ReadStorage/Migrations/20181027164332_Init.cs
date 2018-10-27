using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Migrations
{
    public partial class Init : Migration
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
