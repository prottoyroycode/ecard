using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class orderresnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    count = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_id = table.Column<double>(type: "float", nullable: false),
                    created_time = table.Column<long>(type: "bigint", nullable: false),
                    unit_price = table.Column<double>(type: "float", nullable: false),
                    is_completed = table.Column<bool>(type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    responseCode = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderResponses_OrderedProducts_productId",
                        column: x => x.productId,
                        principalTable: "OrderedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderResponses_productId",
                table: "OrderResponses",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderResponses");

            migrationBuilder.DropTable(
                name: "OrderedProducts");
        }
    }
}
