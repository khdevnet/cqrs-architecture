using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SW.Checkout.Infrastructure.ReadStorage.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_read_view",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    customer_id = table.Column<int>(nullable: false),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_read_view", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_read_view",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_read_view", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_line_read_view",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    product_id = table.Column<int>(nullable: false),
                    warehouse_id = table.Column<Guid>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    quantity = table.Column<int>(nullable: false),
                    order_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_line_read_view", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_line_read_view_order_read_view_order_id",
                        column: x => x.order_id,
                        principalTable: "order_read_view",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_item_read_view",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    warehouse_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_item_read_view", x => x.id);
                    table.ForeignKey(
                        name: "FK_warehouse_item_read_view_warehouse_read_view_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouse_read_view",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_line_read_view_order_id",
                table: "order_line_read_view",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_item_read_view_warehouse_id",
                table: "warehouse_item_read_view",
                column: "warehouse_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_line_read_view");

            migrationBuilder.DropTable(
                name: "warehouse_item_read_view");

            migrationBuilder.DropTable(
                name: "order_read_view");

            migrationBuilder.DropTable(
                name: "warehouse_read_view");
        }
    }
}
