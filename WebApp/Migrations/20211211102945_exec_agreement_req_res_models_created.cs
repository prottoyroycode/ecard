using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class exec_agreement_req_res_models_created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "paymentID",
                table: "bKashAgreementExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bKashExecAgreementRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bKashExecAgreementRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bKashExecAgreementResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paymentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerMsisdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payerReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agreementStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentExecuteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trxID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    merchantInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bKashExecAgreementResponses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bKashExecAgreementRequests");

            migrationBuilder.DropTable(
                name: "bKashExecAgreementResponses");

            migrationBuilder.DropColumn(
                name: "paymentID",
                table: "bKashAgreementExceptions");
        }
    }
}
