using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addnewpricefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "main_max_price",
                table: "ECardVouchers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "main_min_price",
                table: "ECardVouchers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "main_max_price",
                table: "ECardVouchers");

            migrationBuilder.DropColumn(
                name: "main_min_price",
                table: "ECardVouchers");
        }
    }
}
