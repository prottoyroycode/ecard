using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Create_cancel_agreement_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "agreementID",
                table: "bKashAgreementExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trxID",
                table: "bKashAgreementExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bKashCancelAgreementRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    agreementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bKashCancelAgreementRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bKashCancelAgreementResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementVoidTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bKashCancelAgreementResponses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bKashCancelAgreementRequests");

            migrationBuilder.DropTable(
                name: "bKashCancelAgreementResponses");

            migrationBuilder.DropColumn(
                name: "agreementID",
                table: "bKashAgreementExceptions");

            migrationBuilder.DropColumn(
                name: "trxID",
                table: "bKashAgreementExceptions");
        }
    }
}
