using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bKashCallbacks");

            migrationBuilder.DropTable(
                name: "bKashCreateAgreementRequests");

            migrationBuilder.DropTable(
                name: "bKashCreateAgreementResponses");

            migrationBuilder.DropTable(
                name: "CallbackDatas");

            migrationBuilder.DropTable(
                name: "EmailSMTPConfigurations");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderResponses");

            migrationBuilder.DropTable(
                name: "RockVilleOrderData");

            migrationBuilder.DropTable(
                name: "UserFcmDeviceHistory");

            migrationBuilder.DropTable(
                name: "UserLoginHistory");

            migrationBuilder.DropTable(
                name: "UserPasswordHistory");

            migrationBuilder.DropTable(
                name: "UserPasswordResetCodes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
